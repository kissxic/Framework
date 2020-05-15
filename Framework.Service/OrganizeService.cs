using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IService;
using System;
using System.Collections.Generic;

namespace Framework.Service
{
    public partial class OrganizeService : BaseService<Sys_Organize>, IOrganizeService
    {
        private readonly IOrganizeRepository _organizeRepository;

        public OrganizeService(IOrganizeRepository organizeRepository)
        {
            _organizeRepository = organizeRepository;
        }

        public List<Sys_Organize> GetList()
        {
            return _organizeRepository.GetList();
        }

        public Page<Sys_Organize> GetList(int pageIndex, int pageSize, string keyWord)
        {
            return _organizeRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public new Sys_Organize Insert(Sys_Organize model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Layer = _organizeRepository.GetById(model.ParentId).Layer += 1;
            model.IsDeleted = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _organizeRepository.Insert(model);
        }

        public new bool Update(Sys_Organize model)
        {
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            return _organizeRepository.Update(model.Id, new
            {
                model.ParentId,
                model.Layer,
                model.EnCode,
                model.FullName,
                model.Type,
                model.ManagerId,
                model.TelePhone,
                model.WeChat,
                model.Fax,
                model.Email,
                model.Address,
                model.SortCode,
                model.IsEnabled,
                model.Remark,
                model.ModifyUser,
                model.ModifyTime
            });
        }

        public int GetChildCount(object parentId)
        {
            return _organizeRepository.GetChildCount(parentId);
        }
    }
}
