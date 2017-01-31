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
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OSVR.Config.Models;
using OSVR.Config.Common;

namespace OSVR.Config.Controllers
{
    [Route("api/[controller]")]
    public class AvailableUserProfilesController : Controller
    {
        private readonly IConfiguration config;
        public AvailableUserProfilesController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/availableuserprofiles
        [HttpGet]
        public IEnumerable<UserProfile> Get()
        {
            string serverPath = this.config.GetOSVRServerDirectory();
            return UserProfiles.GetAvailableUserProfiles(serverPath);
        }

        // POST: api/availableuserprofiles (UserProfile in body)
        [HttpPost]
        public UserProfile Post([FromBody]UserProfile newProfile)
        {
            string serverPath = this.config.GetOSVRServerDirectory();
            return UserProfiles.SaveUserProfile(newProfile, serverPath);
        }

        // DELETE: api/availableuserprofiles/name
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            string serverPath = this.config.GetOSVRServerDirectory();
            UserProfiles.DeleteUserProfile(name, serverPath);
        }
    }
}
