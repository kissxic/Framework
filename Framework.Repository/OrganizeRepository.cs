using DapperExtensions;
using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Repository
{
    public partial class OrganizeRepository : BaseRepository<Sys_Organize>, IOrganizeRepository
    {
        /// <summary>
        /// 获取所有组织机构列表。
        /// </summary>
        /// <returns></returns>
        public List<Sys_Organize> GetList()
        {
            var sort = new List<ISort> { Predicates.Sort<Sys_Organize>(f => f.SortCode, false) };
            return GetList(c => c.IsDeleted == false, sort).ToList();
        }
        /// <summary>
        /// 分页获取组织机构列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">编码或名称</param>
        /// <returns></returns>
        public Page<Sys_Organize> GetList(int pageIndex, int pageSize, string keyWord)
        {
            Page<Sys_Organize> pager = new Page<Sys_Organize>();
            var sort = new List<ISort> { Predicates.Sort<Sys_Organize>(f => f.SortCode, false) };
            return GetPageData(pager, c => c.IsDeleted == false && c.FullName.Contains(keyWord) || c.EnCode.Contains(keyWord), sort);
        }
        /// <summary>
        /// 获取子级机构数量。
        /// </summary>
        /// <param name="parentId">父级机构ID</param>
        /// <returns></returns>
        public int GetChildCount(string parentId)
        {
            return GetCount(c => c.ParentId == parentId);
        }
    }
}
