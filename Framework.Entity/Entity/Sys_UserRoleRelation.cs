﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entity
{
    public partial class Sys_UserRoleRelation 
    {
         public string Id { get; set; }
         public string UserId { get; set; }
         public string RoleId { get; set; }
         public string CreateUser { get; set; }
         public DateTime? CreateTime { get; set; }
    }
}
