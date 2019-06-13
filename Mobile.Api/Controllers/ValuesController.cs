using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mobile.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Value1()
        {
            return new string[] {"value1", "value2"};
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Value2()
        {
            //var user = HttpContext.User;
            //Console.WriteLine(user.Identity.Name);
            return new[] { "value3", "values4"};
        }
    }
}