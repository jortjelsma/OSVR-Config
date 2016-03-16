using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class EnableDirectModeController : Controller
    {
        private readonly IConfiguration config;

        public EnableDirectModeController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            var renderManagerPath = this.config.Get<string>("OSVR_SERVER_ROOT", null);
            var enableDirectModePath = System.IO.Path.Combine(renderManagerPath, "EnableOSVRDirectMode.exe");
            var enableDirectModePathAMD = System.IO.Path.Combine(renderManagerPath, "EnableOSVRDirectModeAMD.exe");
            Process.Start(enableDirectModePath);
            Process.Start(enableDirectModePathAMD);
        }
    }
}
