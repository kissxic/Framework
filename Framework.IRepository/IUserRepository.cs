﻿using Framework.Entity.Entity;
using Framework.Infrastructure;

namespace Framework.IRepository
{
    public partial interface IUserRepository : IBaseRepository<Sys_User>
    {

        /// <summary>
        /// 根据账号获取用户。
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        Sys_User GetByAccount(string account);

        /// <summary>
        /// 分页获取用户列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">角色编码或名称</param>
        /// <returns></returns>
        Page<Sys_User> GetList(int pageIndex, int pageSize, string keyWord);

        /// <summary>
        /// 批量删除用户。
        /// </summary>
        /// <param name="primaryKeys">主键集合</param>
        /// <returns></returns>
        bool Delete(string[] primaryKeys);

    }
}
