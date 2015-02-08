using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webApiRedis.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Text;

namespace webApiRedis.Business
{
    public interface IRoleBLL
    {
        IEnumerable<Role> GetRole();
     
    }
    public class RoleBLL
    {
        public IEnumerable<Role> GetRole()
        {
            string host = "localhost";
            List<Role> lst = new List<Role>();
            using (var client = new RedisClient(host))
            {
                HashSet<string> items = client.GetAllItemsFromSet("srole");
                foreach (var item in items)
                {
                    Dictionary<string, string> hItem = client.GetAllEntriesFromHash(item);
                    lst.Add(new Role {Key = item, Id = Convert.ToInt64(hItem["Id"]), RoleNM = hItem["RoleNM"], RoleDesc = hItem["RoleDesc"] });
                }
            }
            return lst.OrderByDescending(o => o.Id);
        }


    }
}