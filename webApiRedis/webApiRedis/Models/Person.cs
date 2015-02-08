using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiRedis.Models
{
    public class Person
    {
        public string Key { get; set; }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleKey { get; set; }
    }
}