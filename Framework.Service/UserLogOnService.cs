using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using Framework.IService;
using System;

namespace Framework.Service
{
    public class UserLogOnService : BaseService<Sys_UserLogOn>, IUserLogOnService
    {
        private readonly IUserLogOnRepository _userLogOnRepository;

        public UserLogOnService(IUserLogOnRepository userLogOnRepository)
        {
            _userLogOnRepository = userLogOnRepository;
        }

        public new Sys_UserLogOn Insert(Sys_UserLogOn model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.SecretKey = model.Id.DESEncrypt().Substring(0, 8);
            model.Password = SecurityHelper.MD5_Encrypt(model.Password.DESEncrypt(model.SecretKey), true);
            model.LoginCount = 0;
            model.IsOnLine = false;
            return _userLogOnRepository.Insert(model);
        }

        public Sys_UserLogOn GetByAccount(string userId)
        {
            return _userLogOnRepository.GetByAccount(userId);
        }

        public bool UpdateInfo(Sys_UserLogOn model)
        {
            return _userLogOnRepository.Update(model.Id, new { model.AllowMultiUserOnline, model.Question, model.AnswerQuestion, model.CheckIPAddress, model.Language, model.Theme });
        }

        public bool UpdateLogin(Sys_UserLogOn model)
        {
            model.IsOnLine = true;
            model.LastVisitTime = DateTime.Now;
            model.PrevVisitTime = model.LastVisitTime;
            model.LoginCount += 1;
            return _userLogOnRepository.Update(model.Id, new { model.IsOnLine,model.PrevVisitTime,model.LastVisitTime,model.LoginCount});
        }

        public bool Delete(params string[] userIds)
        {
            return _userLogOnRepository.Delete(userIds);
        }

        public bool ModifyPwd(Sys_UserLogOn model)
        {
            model.ChangePwdTime = DateTime.Now;
            return _userLogOnRepository.Update(model.Id, new { model.Password,model.ChangePwdTime});
        }
    }
}
