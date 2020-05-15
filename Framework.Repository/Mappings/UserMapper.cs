using DapperExtensions.Mapper;
using Framework.Entity;

namespace Framework.Repository.Mappings
{
    public class UserMapper : ClassMapper<Sys_User>
    {
        public UserMapper()
        {
            base.Table("User");
            //Map(f => f.UserId).Key(KeyType.Guid);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            //Map(f => f.UserImg).Ignore();//设置忽略

            AutoMap();
        }
    }
}
