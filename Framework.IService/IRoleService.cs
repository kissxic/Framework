using Framework.Entity.Entity;
using Framework.Infrastructure;
using System.Collections.Generic;

namespace Framework.IService
{
    public partial interface IRoleService : IBaseService<Sys_Role>
    {

        /// <summary>
        /// 获取所有角色列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Role> GetList();

        /// <summary>
        /// 分页获取角色列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">角色编码或名称</param>
        /// <returns></returns>
        Page<Sys_Role> GetList(int pageIndex, int pageSize, string keyWord);

        /// <summary>
        /// 批量删除角色。
        /// </summary>
        /// <param name="primaryKeys">主键集合</param>
        /// <returns></returns>
        bool Delete(string[] primaryKeys);

    }
}
