using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using ConfigUtil.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class DisableDirectModeController : Controller
    {
        private readonly IConfiguration config;

        public DisableDirectModeController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            var renderManagerPath = this.config.GetOSVRServerDirectory();
            var enableDirectModePath = System.IO.Path.Combine(renderManagerPath, "DisableOSVRDirectMode.exe");
            var enableDirectModePathAMD = System.IO.Path.Combine(renderManagerPath, "DisableOSVRDirectModeAMD.exe");
            Process.Start(enableDirectModePath);
            Process.Start(enableDirectModePathAMD);
        }
    }
}
