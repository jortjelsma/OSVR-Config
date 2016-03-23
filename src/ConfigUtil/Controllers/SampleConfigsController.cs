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
using ConfigUtil.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConfigUtil.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigUtil.Controllers
{
    [Route("api/[controller]")]
    public class SampleConfigsController : Controller
    {
        private readonly IConfiguration config;

        public SampleConfigsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/sampleconfigs
        [HttpGet]
        public IEnumerable<ServerConfigPreset> Get()
        {
            var serverPath = this.config.GetOSVRServerDirectory();
            var displaysPath = System.IO.Path.Combine(serverPath, "sample-configs");

            return from displayFile in System.IO.Directory.EnumerateFiles(displaysPath)
                   where displayFile.Contains("osvr_server_config")
                   select new ServerConfigPreset()
                   {
                       FileName = System.IO.Path.GetFileName(displayFile),
                       Body = OSVRConfig.Read(displayFile, config),
                   };
        }
    }
}
