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
    [RoutePrefix("roles")]
    public class RoleController : ApiController
    {
       
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
