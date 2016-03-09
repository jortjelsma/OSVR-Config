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
        public void Post([FromQuery] bool isAmd)
        {
            var renderManagerPath = this.config.Get<string>("OSVR_RENDERMANAGER", null);
            var enableDirectModePath = System.IO.Path.Combine(
                renderManagerPath, isAmd ? "EnableOSVRDirectModeAMD.exe" : "EnableOSVRDirectMode.exe");
            Process.Start(enableDirectModePath);
        }
    }
}
