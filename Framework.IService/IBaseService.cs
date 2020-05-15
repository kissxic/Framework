using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.IService
{
    /// <summary>
    /// 业务逻辑层父接口。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IBaseService<TEntity> where TEntity : class
    {
        #region 数据操作

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Insert(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新实体指定字段数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        bool Update(object id, object prams);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Update(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(object key);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool Delete(IEnumerable<object> keys);

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteLogic(object key);

        #endregion

        #region 数据查询
        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// 获取数据表总项数
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        long GetCount(Expression<Func<TEntity, bool>> expression = null);

        /// <summary>
        /// 获取结果集第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity GetFist(Expression<Func<TEntity, bool>> expression = null);

        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="sql">e.g.: select * from TbUser where Id = @Id</param>
        /// <param name="param">e.g.: new { Id = 10 }</param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(string sql, dynamic param = null);
        /// <summary>
        /// 查看指定的数据是否存在
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 根据条件获取表数据
        /// </summary>
        /// <param name="expression">linq表达式</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression, object sortList = null);

        /// <summary>
        /// 数据表分页
        /// </summary>
        /// <param name="pager">页数信息</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        Page<TEntity> GetPageData(Page<TEntity> pager, Expression<Func<TEntity, bool>> expression = null, object sortList = null);

        /// <summary>
        /// 数据表 分页
        /// </summary>
        /// <param name="pageNum">指定页数 索引从0开始</param>
        /// <param name="pageSize">指定每页多少项</param>
        ///<param name="outTotal">输出当前表的总项数</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPageData(int pageNum, int pageSize, out long outTotal,
           Expression<Func<TEntity, bool>> expression = null, object sortList = null);


        #endregion
    }
}
