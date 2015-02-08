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
    public interface IPersonBLL
    {
        IEnumerable<Person> GetPerson();
        void Create(Person p_Person);
        void Delete(string p_Key);
        void Update(Person p_Person);
    }
    public class PersonBLL
    {
        public IEnumerable<Person> GetPerson()
        {
            string host = "localhost";
            List<Person> lst = new List<Person>();
            using (var client = new RedisClient(host))
            {
                HashSet<string> items = client.GetAllItemsFromSet("sperson");
                foreach (var item in items.Where(w => w.Contains("person")))
                {
                    Dictionary<string, string> hItem = client.GetAllEntriesFromHash(item);
                    lst.Add(new Person {Key = item, Id = Convert.ToInt64(hItem["Id"]), FirstName = hItem["FirstName"], LastName = hItem["LastName"], RoleKey = hItem["RoleKey"] });
                }
            }
            return lst.OrderByDescending(o => o.Id);
        }

        public void Create(Person p_Person)
        {
            string host = "localhost";
            using (var client = new RedisClient(host))
            {
                string key = "person_" + p_Person.Id;
                client.HSet(key, Encoding.ASCII.GetBytes("Id"), Encoding.ASCII.GetBytes(p_Person.Id.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("FirstName"), Encoding.ASCII.GetBytes(p_Person.FirstName.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("LastName"), Encoding.ASCII.GetBytes(p_Person.LastName.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("RoleKey"), Encoding.ASCII.GetBytes(p_Person.RoleKey));
                var set = client.Sets["sperson"];
                set.Add(key);
                
                
            }

        }
        public void Update(Person p_Person)
        {
            string host = "localhost";
            using (var client = new RedisClient(host))
            {
                string key = "person_" + p_Person.Id;
                client.HSet(key, Encoding.ASCII.GetBytes("Id"), Encoding.ASCII.GetBytes(p_Person.Id.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("FirstName"), Encoding.ASCII.GetBytes(p_Person.FirstName.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("LastName"), Encoding.ASCII.GetBytes(p_Person.LastName.ToString()));
                client.HSet(key, Encoding.ASCII.GetBytes("RoleKey"), Encoding.ASCII.GetBytes(p_Person.RoleKey));
                var set = client.Sets["sperson"];
                set.Add(key);
               

            }

        }

        public void SyncUser(IList<Person> p_LstPerson)
        {
            string host = "localhost";
            using (var client = new RedisClient(host))
            {
                foreach (var item in p_LstPerson)
                {

                    client.HSet(item.Key, Encoding.ASCII.GetBytes("Id"), Encoding.ASCII.GetBytes(item.Id.ToString()));
                    client.HSet(item.Key, Encoding.ASCII.GetBytes("FirstName"), Encoding.ASCII.GetBytes(item.FirstName.ToString()));
                    client.HSet(item.Key, Encoding.ASCII.GetBytes("LastName"), Encoding.ASCII.GetBytes(item.LastName.ToString()));
                    client.HSet(item.Key, Encoding.ASCII.GetBytes("RoleKey"), Encoding.ASCII.GetBytes(item.RoleKey));
                    var set = client.Sets["sperson"];
                    set.Add(item.Key);
                }
            }
              
        }
        public void Delete(string p_Key)
        {
            string host = "localhost";
            using (var client = new RedisClient(host))
            {
                client.Del(p_Key);
                client.SRem("sperson", Encoding.ASCII.GetBytes(p_Key));
            }
        }


        public IsUserExists IsUserExists(string p_Key)
        {
           
            string host = "localhost";
            using(var client = new RedisClient(host))
            {

                return
                    new IsUserExists
                    {
                        UserExists = client.Exists("person_"+ p_Key) == 1 ? true : false
                    };
                    
            }
        }

    }
}