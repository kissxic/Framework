using Dapper;
using DapperExtensions;
using Framework.Infrastructure;
using Framework.IRepository;
using Framework.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbHandleBase DbHandle { get; private set; }
        public BaseRepository()
        {
            //初始化数据库操作对象
            DbHandle = new SqlServerHandle(ConfigurationHelper.ConnectionString("connStr"));
        }
        #region 数据操作

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            DbHandle.Insert(entity);
            return entity;
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Insert(IEnumerable<TEntity> entitys)
        {
            var conn = DbHandle.CreateConnectionAndOpen();
            var tran = conn.BeginTransaction();
            try
            {
                conn.Insert(entitys, transaction: tran);
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            var flag = DbHandle.Update(entity);
            return flag ? entity : null;
        }

        /// <summary>
        /// 更新实体指定字段数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public bool Update(object id, object prams)
        {
            var tableName = typeof(TEntity).Name;//获取当前要更新的表名称
            if (typeof(TEntity).Name == prams.GetType().Name) throw new ArgumentException("参数不能是当前实体的强类型,否则更新会覆盖所有未赋值的字段");

            //获取指定的更新字段
            PropertyInfo[] fields = prams.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            //构建Sql语句
            var sqlBuilder = new StringBuilder("Update\0" + tableName + "\0set\0");

            foreach (var f in fields)
            {
                //验证指定更新字段是否在表中存在    
                var exist = typeof(TEntity).GetProperty(f.Name);
                if (exist == null) throw new ArgumentException("指定的更新的字段在表中不存在,请检查!");

                sqlBuilder.Append(f.Name + "=@" + f.Name + "\0");
                if (fields.Count() > 1 && fields.Last() != f)
                    sqlBuilder.Append(",");
            }

            sqlBuilder.Append("where Id=" + "'" + id + "'");
            var sql = sqlBuilder.ToString();
            IDbConnection conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                var succ = conn.Execute(sql, prams);
                return succ > 0;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }


        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Update(IEnumerable<TEntity> entitys)
        {
            var conn = DbHandle.CreateConnectionAndOpen();
            var tran = conn.BeginTransaction();
            try
            {
                foreach (var item in entitys)
                {
                    conn.Update(item, transaction: tran);
                }
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();//事物回滚
                return false;
            }
            finally
            {
                tran.Dispose();
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(object key)
        {
            var conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                TEntity item = conn.Get<TEntity>(key);
                return conn.Delete(item);
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool Delete(IEnumerable<object> keys)
        {
            var tblName = typeof(TEntity).Name;
            keys = keys.Select(k => string.Format("'{0}'", k));
            var sql = string.Format("Delete From {0} where Id in ({1})", tblName, string.Join(",", keys));
            var conn = DbHandle.CreateConnectionAndOpen();
            var tran = conn.BeginTransaction();
            try
            {
                conn.Execute(sql, transaction: tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();//事物回滚
                throw ex;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }


        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteLogic(object key)
        {
            return Update(key, new { IsDeleted = true });
        }

        #endregion

        #region 数据查询
        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            var conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                var item = conn.Get<TEntity>(id);
                return item;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 获取数据表总项数
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        public int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            var conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                return conn.Count<TEntity>(predicate);
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 获取结果集第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity GetFist(Expression<Func<TEntity, bool>> expression = null)
        {
            var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
            var conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                var data = conn.GetList<TEntity>(predicate);
                return data.FirstOrDefault();
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }
        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="sql">e.g.: select * from TbUser where Id = @Id</param>
        /// <param name="param">e.g.: new { Id = 10 }</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query(string sql, dynamic param = null)
        {
            return DbHandle.Query<TEntity>(sql, param);
        }

        /// <summary>
        /// 查看指定的数据是否存在
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            var ct = GetCount(expression);
            return ct > 0;
        }

        /// <summary>
        /// 根据条件获取表数据
        /// </summary>
        /// <param name="expression">linq表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            IList<ISort> sort = SortConvert(sortList);//转换排序接口
            var conn = DbHandle.CreateConnectionAndOpen();
            try
            {
                if (expression == null)
                {
                    //允许脏读
                    return conn.GetList<TEntity>(null, sort, transaction: conn.BeginTransaction(IsolationLevel.ReadUncommitted));//如果条件为Null 就查询所有数据
                }
                else
                {
                    var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                    return conn.GetList<TEntity>(predicate, sort, transaction: conn.BeginTransaction(IsolationLevel.ReadUncommitted));
                }
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }


        /// <summary>
        /// 数据表 分页
        /// </summary>
        /// <param name="pager">页数信息</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        public Page<TEntity> GetPageData(Page<TEntity> pager, Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            int commandTimeout = 1800;
            IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression); //转换Linq表达式
            IList<ISort> sort = SortConvert(sortList);//转换排序接口
            var conn = DbHandle.CreateConnectionAndOpen();
            var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var entities = conn.GetPage<TEntity>(predicate, sort, pager.PageIndex, pager.PageSize, tran);
                var list = entities.ToList();
                pager.TotalCount = conn.Count<TEntity>(predicate, tran, commandTimeout);
                return pager;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }

        /// <summary>
        /// 数据表 分页
        /// </summary>
        /// <param name="pageNum">指定页数 索引从0开始</param>
        /// <param name="pageSize">指定每页多少项</param>
        ///<param name="outTotal">输出当前表的总项数</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPageData(int pageNum, int pageSize, out long outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression); //转换Linq表达式
            IList<ISort> sort = SortConvert(sortList);//转换排序接口
            var conn = DbHandle.CreateConnectionAndOpen();
            var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
            try
            {
                var entities = conn.GetPage<TEntity>(predicate, sort, pageNum, pageSize, transaction: conn.BeginTransaction(IsolationLevel.ReadUncommitted));
                outTotal = conn.Count<TEntity>(null);
                return entities;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }


        #endregion

        #region 辅助方法
        /// <summary>
        /// 转换成Dapper排序方式
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
        private static IList<ISort> SortConvert(object sortList)
        {
            IList<ISort> sorts = new List<ISort>();
            if (sortList == null)
            {
                sorts.Add(Predicates.Sort<TEntity>(null));//默认以开始时间 最早创建的时间 asc=flase 降序
                return sorts;
            }

            Type obj = sortList.GetType();
            var fields = obj.GetRuntimeFields();
            Sort s = null;
            foreach (FieldInfo f in fields)
            {
                s = new Sort();
                var mt = Regex.Match(f.Name, @"^\<(.*)\>.*$");
                s.PropertyName = mt.Groups[1].Value;
                s.Ascending = f.GetValue(sortList) == null ? true : (bool)f.GetValue(sortList);
                sorts.Add(s);
            }

            return sorts;
        }

        #endregion
    }
}
