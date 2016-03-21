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
using ConfigUtil.Common;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class StartServerController : Controller
    {
        private readonly IConfiguration config;
        public StartServerController(IConfiguration config)
        {
            this.config = config;
        }

        // POST api/startserver
        [HttpPost]
        public void Post()
        {
            var osvrServerPath = this.config.GetOSVRServerDirectory();
            var osvrServerExe = System.IO.Path.Combine(osvrServerPath, "osvr_server.exe");
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = osvrServerExe;
            startInfo.WorkingDirectory = osvrServerPath;
            Process.Start(startInfo);
        }
    }
}
