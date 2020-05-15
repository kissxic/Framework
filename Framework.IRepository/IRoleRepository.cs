using Framework.Entity.Entity;
using Framework.Infrastructure;
using System.Collections.Generic;

namespace Framework.IRepository
{
    public partial interface IRoleRepository : IBaseRepository<Sys_Role>
    {
        /// <summary>
        /// 获取所有角色列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Role> GetList();
        Page<Sys_Role> GetList(int pageIndex, int pageSize, string keyWord);
        bool Delete(params string[] primaryKeys);
    }
}
