using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using System.Linq;

namespace Framework.Repository
{
    public partial class UserRepository : BaseRepository<Sys_User>, IUserRepository
    {
        /// <summary>
        /// 根据账号获取用户。
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public Sys_User GetByAccount(string account)
        {
            return GetFist(c => c.Account == account);
        }
        /// <summary>
        /// 分页获取用户列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">角色编码或名称</param>
        public Page<Sys_User> GetList(int pageIndex, int pageSize, string keyWord)
        {
            //Sql sql = Sql.Builder
            //     .Select("u.*, o.FullName")
            //     .From("Sys_User u")
            //     .LeftJoin("Sys_Organize o")
            //     .On("u.DepartmentId=o.Id")
            //     .Where("u.DeleteMark=0 and u.Account like @0 or u.RealName like @1", '%' + keyWord + '%', '%' + keyWord + '%')
            //     .OrderBy("u.SortCode");

            //var list = Db.PageJoin<Sys_User, Sys_Organize, Sys_User>((user, dept) =>
            //{
            //    user.DeptName = dept.FullName;
            //    return user;
            //}, pageIndex, pageSize, sql);

            //return list;
            return null;
        }
        /// <summary>
        /// 批量删除用户。
        /// </summary>
        /// <param name="primaryKeys">主键集合</param>
        /// <returns></returns>
        public bool Delete(string[] primaryKeys)
        {
            return base.Delete(primaryKeys.ToArray());
        }
    }
}
