using Framework.Entity.Entity;
using System.Collections.Generic;

namespace Framework.IRepository
{
    public partial interface IUserRoleRelationRepository : IBaseRepository<Sys_UserRoleRelation>
    {
        /// <summary>
        /// 获取指定用户ID所有用户角色关系实体。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<Sys_UserRoleRelation> GetList(string userId);

        /// <summary>
        /// 批量删除用户角色关系实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        bool Delete(params string[] userIds);
    }
}
