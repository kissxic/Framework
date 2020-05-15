using DapperExtensions;
using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Repository
{
    public partial class LogRepository : BaseRepository<Sys_Log>, ILogRepository
    {
        /// <summary>
        /// 分页获取指定用户操作记录。
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="limitDate">限制日期</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <returns></returns>
        public Page<Sys_Log> GetList(int pageIndex, int pageSize, DateTime limitDate, string keyWord)
        {
            Expression<Func<Sys_Log, bool>> expression = c => c.CreateTime > limitDate && (c.Account.Contains(keyWord) || c.RealName.Contains(keyWord));
            var sort = new List<ISort> { Predicates.Sort<Sys_Log>(f => f.CreateTime, false) };
            Page<Sys_Log> pager = new Page<Sys_Log>() { PageIndex = pageIndex, PageSize = pageSize };
            return GetPageData(pager, expression, sort);
        }
        /// <summary>
        /// 根据时间删除日志。
        /// </summary>
        /// <param name="keepDate">日志保留时间</param>
        /// <returns></returns>
        public int Delete(DateTime keepDate)
        {
            return DbHandle.ExecuteNonQuery("delete from Sys_Log where CreateTime<= @CreateTime", new { CreateTime = keepDate.ToString() });
        }
    }
}
