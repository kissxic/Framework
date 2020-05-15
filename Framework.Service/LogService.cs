using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using Framework.IService;
using System;

namespace Framework.Service
{
    public partial class LogService : BaseService<Sys_Log>, ILogService
    {

        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository _logRepository)
        {
            this._logRepository = _logRepository;
        }
        
        public Page<Sys_Log> GetList(int pageIndex, int pageSize, DateTime limitDate, string keyWord)
        {
            return _logRepository.GetList(pageIndex, pageSize, limitDate, keyWord);
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="keepDate">日志保留时间</param>
        /// <returns></returns>
        public int Delete(DateTime keepDate)
        {
            return _logRepository.Delete(keepDate);
        }
    }
}
