using Dapper;
using Framework.Entity.Entity;
using Framework.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Repository
{
    public partial class RoleAuthorizeRepository : BaseRepository<Sys_RoleAuthorize>, IRoleAuthorizeRepository
    {

        public List<Sys_RoleAuthorize> GetList()
        {
            return GetList();
        }
        /// <summary>
        /// 根据角色ID查询授权信息。
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public List<Sys_RoleAuthorize> GetList(string roleId)
        {
            return GetList(c => c.RoleId == roleId, null).ToList();
        }
        /// <summary>
        /// 根据权限ID删除角色授权信息。
        /// </summary>
        /// <param name="moduleIds">权限ID集合</param>
        /// <returns></returns>
        public bool Delete(params string[] moduleIds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE");
            for (int i = 0; i < moduleIds.Length - 1; i++)
            {
                sb.Append(string.Format(" ModuleId={0} OR", moduleIds[i]));
            }
            sb.Append(string.Format(" ModuleId=@0", moduleIds[moduleIds.Length - 1]));
            var sql = string.Format("Delete From {0}{1}", "Sys_RoleAuthorize", sb.ToString());
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
