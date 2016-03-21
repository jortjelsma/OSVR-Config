/// OSVR-Config
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>
/// 
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
