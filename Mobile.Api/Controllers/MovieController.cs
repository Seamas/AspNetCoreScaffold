using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobile.Api.Models;

namespace Mobile.Api.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class MovieController : ControllerBase
    {
        //IDataProtector
        private readonly ITimeLimitedDataProtector dataProtector;

        public MovieController(IDataProtectionProvider provider)
        {
            this.dataProtector = provider.CreateProtector("abcdefgh")
                .ToTimeLimitedDataProtector();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = Movie.GetAllMovies();
            var outputModel = movies.Select(item => new
            {
                Id = this.dataProtector.Protect(item.Id.ToString(), TimeSpan.FromSeconds(60)),
                item.Name
            });
            return Ok(outputModel);
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{
        //    var originalId = int.Parse(this.dataProtector.Unprotect(id));
        //    var model = Movie.GetAllMovies()
        //        .Where(item => item.Id == originalId)
        //        .Select(item => new
        //        {
        //            Id = this.dataProtector.Protect(item.Id.ToString(), TimeSpan.FromSeconds(60)),
        //            item.Name
        //        });
        //    return Ok(model);
        //}

        
        [HttpGet("{id}")]
        // 使用特性
        [DecryptId]
        public IActionResult Get(int id)
        {
            var model = Movie.GetAllMovies()
                .Where(item => item.Id == id)
                .Select(item => new
                {
                    Id = this.dataProtector.Protect(item.Id.ToString(), TimeSpan.FromSeconds(60)),
                    item.Name
                });
            return Ok(model);
        }
    }


    public class DecryptIdAttribute : TypeFilterAttribute
    {
        public DecryptIdAttribute() : base(typeof(DecryptIdFilter)) { }
    }

    public class DecryptIdFilter : IActionFilter
    {
        private readonly ITimeLimitedDataProtector protector;

        public DecryptIdFilter(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("abcdefgh")
                .ToTimeLimitedDataProtector();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object param = context.RouteData.Values["id"]?.ToString();
            if (param != null)
            {
                var id = int.Parse(this.protector.Unprotect(param.ToString()));
                context.ActionArguments["id"] = id;
            }

        }
    }
}