using Framework.Entity.Entity;
using Framework.Infrastructure;
using Framework.IRepository;
using Framework.IService;
using System;
using System.Collections.Generic;

namespace Framework.Service
{
    public partial class RoleService : BaseService<Sys_Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Page<Sys_Role> GetList(int pageIndex, int pageSize, string keyWord)
        {
            return _roleRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public new Sys_Role Insert(Sys_Role model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.IsEnabled = model.IsEnabled;
            model.AllowEdit = model.AllowEdit == null ? false : true;
            model.IsDeleted = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _roleRepository.Insert(model);
        }

        public new bool Update(Sys_Role model)
        {
            model.IsEnabled = model.IsEnabled;
            model.AllowEdit = model.AllowEdit == null ? false : true;
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            return _roleRepository.Update(model.Id, new
            {
                model.OrganizeId,
                model.EnCode,
                model.Type,
                model.Name,
                model.AllowEdit,
                model.IsEnabled,
                model.Remark,
                model.SortCode,
                model.ModifyUser,
                model.ModifyTime
            });
        }

        public bool Delete(string[] primaryKeys)
        {
            return _roleRepository.Delete(primaryKeys);
        }

        public List<Sys_Role> GetList()
        {
            return _roleRepository.GetList();
        }
    }
}
