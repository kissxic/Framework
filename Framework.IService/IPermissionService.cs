using Framework.Entity.Entity;
using Framework.Infrastructure;
using System.Collections.Generic;

namespace Framework.IService
{
    public partial interface IPermissionService : IBaseService<Sys_Permission>
    {
        /// <summary>
        /// 获取所有权限列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Permission> GetList();

        /// <summary>
        /// 根据用户ID获取权限列表。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<Sys_Permission> GetList(string userId);

        /// <summary>
        /// 分页获取权限列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">权限编码或名称</param>
        /// <returns></returns>
        Page<Sys_Permission> GetList(int pageIndex, int pageSize, string keyWord);

        /// <summary>
        /// 验证用户是否有访问当前页面权限。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="action">请求URL</param>
        /// <returns></returns>
        bool ActionValidate(string userId, string action);

        /// <summary>
        /// 批量删除权限。
        /// </summary>
        /// <param name="primaryKeys">权限ID集合</param>
        /// <returns></returns>
        bool Delete(params string[] primaryKeys);

        /// <summary>
        /// 获取子级权限数量。
        /// </summary>
        /// <param name="parentId">父级权限ID</param>
        /// <returns></returns>
        int GetChildCount(string parentId);
    }
}
