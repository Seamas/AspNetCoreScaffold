using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.Api.Controllers
{
    // 使用版本路由时，将无法使用默认版本，不推荐使用
    [ApiVersion("1.0")]
    [Route("api/version")]
    //[Route("api/{v:apiVersion}/version")]
    [ApiController]
    public class Value1Controller: ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Value1 from Version 1", "value2 from Version 1" };
        }
    }

    [ApiVersion("2.0")]
    [Route("api/version")]
    //[Route("api/{v:apiVersion}/version")]
    [ApiController]
    public class Value2Controller : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Value1 from Version 2", "value2 from Version 2" };
        }
    }
}
