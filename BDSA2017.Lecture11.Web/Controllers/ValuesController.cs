using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BDSA2017.Lecture11.Web.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(42);
        }
    }
}