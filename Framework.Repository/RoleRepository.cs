using Dapper;
using DapperExtensions;
using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    public partial class RoleRepository : BaseRepository<Sys_Role>, IRoleRepository
    {
        public Page<Sys_Role> GetList(int pageIndex, int pageSize, string keyWord)
        {
            //Sql sql = Sql.Builder
            //   .Select("r.*, o.FullName")
            //   .From("Sys_Role r")
            //   .LeftJoin("Sys_Organize o")
            //   .On("r.OrganizeId=o.Id")
            //   .Where("r.DeleteMark=0 and r.Name like @0 or r.EnCode like @1", '%' + keyWord + '%', '%' + keyWord + '%')
            //   .OrderBy("r.SortCode");

            //var list = Db.PageJoin<Sys_Role, Sys_Organize, Sys_Role>((role, dept) =>
            //{
            //    role.DeptName = dept.FullName;
            //    return role;
            //}, pageIndex, pageSize, sql);

            //return list;
            return null;
        }

        public bool Delete(params string[] primaryKeys)
        {
            return base.Delete(primaryKeys.ToArray());
            //StringBuilder sb = new StringBuilder();
            //sb.Append(" WHERE");
            //for (int i = 0; i < primaryKeys.Length - 1; i++)
            //{
            //    sb.Append(string.Format(" Id={0} OR", primaryKeys[i]));
            //}
            //sb.Append(string.Format(" Id=@0", primaryKeys[primaryKeys.Length - 1]));
            //var sql = string.Format("Delete From {0}{1}", "Sys_Role", sb.ToString());
            //var conn = DbHandle.CreateConnectionAndOpen();
            //var tran = conn.BeginTransaction();
            //try
            //{
            //    conn.Execute(sql, transaction: tran);
            //    tran.Commit();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    throw ex;
            //}
            //finally
            //{
            //    conn.CloseIfOpen();
            //}
        }

        public List<Sys_Role> GetList()
        {
            var sort = new List<ISort> { Predicates.Sort<Sys_Role>(f => f.SortCode, true) };
            return GetList(c => c.IsDeleted == false, sort).ToList();
        }
    }
}
