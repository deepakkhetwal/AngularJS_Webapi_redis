using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiRedis.Models
{
    public class Role
    {
        public string Key { get; set; }
        public long Id { get; set; }
        public string RoleNM { get; set; }
        public string RoleDesc { get; set; }
    }
}