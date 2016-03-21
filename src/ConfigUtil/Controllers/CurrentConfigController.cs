using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ConfigUtil.Models;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class CurrentConfigController : Controller
    {
        private readonly IConfiguration config;

        public CurrentConfigController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/currentconfig
        [HttpGet]
        public OSVRConfig Get()
        {
            return OSVRConfig.GetCurrent(config);
        }

        // POST api/currentconfig
        [HttpPost]
        public void Post([FromBody]OSVRConfig value)
        {
        }
    }
}
