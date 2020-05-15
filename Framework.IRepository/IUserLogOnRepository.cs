using Framework.Entity.Entity;

namespace Framework.IRepository
{
    public partial interface IUserLogOnRepository : IBaseRepository<Sys_UserLogOn>
    {
        /// <summary>
        /// 根据用户ID获取用户登陆实体。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Sys_UserLogOn GetByAccount(string userId);

        /// <summary>
        /// 批量删除用户登陆实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        bool Delete(params string[] userIds);

    }
}
