using Dapper;
using Framework.Entity.Entity;
using Framework.IRepository;
using System;
using System.Text;

namespace Framework.Repository
{
    public partial class UserLogOnRepository : BaseRepository<Sys_UserLogOn>, IUserLogOnRepository
    {
        /// <summary>
        /// 根据用户ID获取用户登陆实体。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public Sys_UserLogOn GetByAccount(string userId)
        {
            return GetFist(c => c.UserId == userId);
        }
        /// <summary>
        /// 批量删除用户登陆实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        public bool Delete(params string[] userIds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE");
            for (int i = 0; i < userIds.Length - 1; i++)
            {
                sb.Append(string.Format(" UserId={0} OR", userIds[i]));
            }
            sb.Append(string.Format(" UserId=@0", userIds[userIds.Length - 1]));
            var sql = string.Format("Delete From {0}{1}", "Sys_UserLogOn", sb.ToString());
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
                tran.Rollback();
                throw ex;
            }
            finally
            {
                conn.CloseIfOpen();
            }
        }
    }
}
