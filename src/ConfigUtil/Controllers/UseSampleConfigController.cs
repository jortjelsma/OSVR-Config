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
    public class UseSampleConfigController : Controller
    {
        private readonly IConfiguration config;

        public UseSampleConfigController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/usesampleconfig
        [HttpPost]
        public void Post([FromQuery]string sampleFileName)
        {
            var serverPath = config.Get<string>("OSVR_SERVER_ROOT");
            var sampleConfigPath = System.IO.Path.Combine(serverPath, "sample-configs");
            var presetConfigPath = System.IO.Path.Combine(sampleConfigPath, sampleFileName);
            var serverConfigPath = System.IO.Path.Combine(serverPath, "osvr_server_config.json");
            System.IO.File.Copy(presetConfigPath, serverConfigPath, true);
        }
    }
}
