using Framework.Infrastructure;
using Framework.IRepository;
using Framework.IService;
using Framework.Repository;
using Framework.Repository.Mappings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Service
{
    /// <summary>
    /// 业务逻辑层父类。
    /// </summary>
    public partial class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private IBaseRepository<TEntity> repository;

        public BaseService()
        {
            if (repository == null)
            {
                repository = new BaseRepository<TEntity>();
            }
            Mappings.Initialize();
        }

        #region 数据操作

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            return repository.Insert(entity);
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Insert(IEnumerable<TEntity> entitys)
        {
            return repository.Insert(entitys);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            return repository.Update(entity);
        }

        /// <summary>
        /// 更新实体指定字段数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public bool Update(object id, object prams)
        {
            return repository.Update(id, prams);
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Update(IEnumerable<TEntity> entitys)
        {
            return repository.Update(entitys);
        }

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(object key)
        {
            return repository.Delete(key);
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool Delete(IEnumerable<object> keys)
        {
            return repository.Delete(keys);
        }


        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteLogic(object key)
        {
            return repository.DeleteLogic(key);
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
            return repository.GetById(id);
        }

        /// <summary>
        /// 获取数据表总项数
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        public long GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            return repository.GetCount(expression);
        }

        /// <summary>
        /// 获取结果集第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity GetFist(Expression<Func<TEntity, bool>> expression = null)
        {
            return repository.GetFist(expression);
        }

        /// <summary>
        /// 查看指定的数据是否存在
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return repository.Exists(expression);
        }

        /// <summary>
        /// 根据条件获取表数据
        /// </summary>
        /// <param name="expression">linq表达式</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            return repository.GetList(expression, sortList);
        }
        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="sql">e.g.: select * from TbUser where Id = @Id</param>
        /// <param name="param">e.g.: new { Id = 10 }</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Query(string sql, dynamic param = null)
        {
            return repository.Query(sql, param);
        }

        /// <summary>
        /// 数据表分页
        /// </summary>
        /// <param name="pager">页数信息</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        public Page<TEntity> GetPageData(Page<TEntity> pager, Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            return repository.GetPageData(pager, expression, sortList);
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
            return repository.GetPageData(pageNum, pageSize, out outTotal, expression = null, sortList = null);
        }


        #endregion
    }
}
