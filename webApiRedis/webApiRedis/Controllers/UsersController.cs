using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApiRedis.Business;
using webApiRedis.Models;
namespace webApiRedis.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        //private readonly IPersonBLL iPersonBLL;
        //public UsersController(IPersonBLL p_IPerson)
        //{
        //    iPersonBLL = p_IPerson;
        //}
        [Route("getuser")]
        public IHttpActionResult GetUsers()
        {
            IEnumerable<Person> lst = new PersonBLL().GetPerson();
            if (lst == null || lst.Count() <= 0)
            {
                return NotFound(); // Returns a NotFoundResult
            }
            return Ok(lst);
        }
        
        [HttpPost]
        [Route("postuser")]
        public void PostUser(Person p_Person)
        {

            new PersonBLL().Create(p_Person);
        }

        [HttpPut]
        [Route("putuser")]
        public void PutUser(Person p_Person)
        {

            new PersonBLL().Update(p_Person);
        }


        [HttpPost]
        [Route("syncuser")]
        public void SyncUser(IList<Person> p_Person)
        {
            new PersonBLL().SyncUser(p_Person);
        }

        [HttpDelete]
        [Route("deleteuser/{p_Key}")]
        public void DeleteUser(string p_Key)
        {
            new PersonBLL().Delete(p_Key.ToString());
        }
        
        [HttpGet]
        [Route("isuserexists/{p_Key}")]
        public IHttpActionResult IsUserExists(string p_Key)
        {
            
            return Ok(new PersonBLL().IsUserExists(p_Key));
        }

        [HttpGet]
        [Route("getrole")]
        public IHttpActionResult GetRole()
        {
            IEnumerable<Role> lst = new RoleBLL().GetRole();
            if (lst == null || lst.Count() <= 0)
            {
                return NotFound(); // Returns a NotFoundResult
            }
            return Ok(lst);
        }

    }
}
