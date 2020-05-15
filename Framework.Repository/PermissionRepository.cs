using DapperExtensions;
using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Repository
{
    public partial class PermissionRepository : BaseRepository<Sys_Permission>, IPermissionRepository
    {
        public List<Sys_Permission> GetList()
        {
            var sort = new List<ISort> { Predicates.Sort<Sys_Permission>(f => f.SortCode, false) };
            return GetList(c => c.IsDeleted == false, sort).ToList();
        }
        public Page<Sys_Permission> GetList(int pageIndex, int pageSize, string keyWord)
        {
            Expression<Func<Sys_Permission, bool>> expression = c => c.IsDeleted ==false && (c.Name.Contains(keyWord) || c.EnCode.Contains(keyWord));
            var sort = new List<ISort> { Predicates.Sort<Sys_Permission>(f => f.SortCode, true) };
            Page<Sys_Permission> pager = new Page<Sys_Permission>() { PageIndex = pageIndex, PageSize = pageSize };
            return GetPageData(pager, expression, sort);
        }


        public bool Delete(params string[] primaryKeys)
        {
            return base.Delete(primaryKeys.ToArray());
        }

        public int GetChildCount(string parentId)
        {
            return GetCount(c => c.ParentId == parentId);
        }
    }
}
