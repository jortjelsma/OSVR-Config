using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSVR.Config.Common;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OSVR.Config.Models
{
    public class UserProfile
    {
        public UserProfile()
        {
            Name = "User";
            IPD = 0.0635f;
            StandingEyeHeight = 1.62052f;
            SeatedEyeHeight = StandingEyeHeight / 2f;
            FilePath = "";
        }

        public UserProfile(UserProfile copyFrom)
        {
            this.Name = copyFrom.Name;
            this.IPD = copyFrom.IPD;
            this.StandingEyeHeight = copyFrom.StandingEyeHeight;
            this.SeatedEyeHeight = copyFrom.SeatedEyeHeight;
            this.FilePath = copyFrom.FilePath;
        }

        public string Name { get; set; }
        public float IPD { get; set; }
        public float StandingEyeHeight { get; set; }
        public float SeatedEyeHeight { get; set; }
        public string FilePath { get; set; }
    }

    public static class UserProfiles
    {
        private static readonly List<UserProfile> userProfiles = new List<UserProfile>
            {
                new UserProfile() { Name = "User1" },
                new UserProfile() { Name = "User2", IPD = 0.065f },
                new UserProfile() { Name = "User3", StandingEyeHeight = 1.65f, SeatedEyeHeight = 1.0f, FilePath = "C:\\Sample\\File\\Path\\file.json" },
            };

        private static UserProfile ReadProfileFromFile(string filePath)
        {
            using (var profileReader = File.OpenText(filePath))
            using (var jr = new JsonTextReader(profileReader))
            {
                var body = (JObject)JObject.ReadFrom(jr);
                var ret = body.ToObject<UserProfile>();
                return ret;
            }
        }

        public static IEnumerable<UserProfile> GetAvailableUserProfiles(string serverPath)
        {
            string userProfileDirectory = OSVRDirectories.GetUserProfileDirectory(serverPath, true);
            var profiles = from file in Directory.EnumerateFiles(userProfileDirectory)
                           where String.CompareOrdinal(Path.GetExtension(file), ".json") == 0
                           select ReadProfileFromFile(file);

            return profiles;
        }

        //public static UserProfile GetUserProfile()
        //{
        //    return new UserProfile();
        //}

        private static string GetUserProfileFileName(string name)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }

            return $"{name}.profile.json";
        }

        public static UserProfile SaveUserProfile(UserProfile userProfile, string serverPath)
        {
            if (userProfile == null) { throw new ArgumentNullException(nameof(userProfile)); }
            if (serverPath == null) { throw new ArgumentNullException(nameof(serverPath)); }

            if(!String.IsNullOrWhiteSpace(userProfile.FilePath) && File.Exists(userProfile.FilePath))
            {
                File.Delete(userProfile.FilePath);
            }

            string userProfileRoot = OSVRDirectories.GetUserProfileDirectory(serverPath, true);
            userProfile.FilePath = Path.Combine(userProfileRoot, GetUserProfileFileName(userProfile.Name));

            using (var fs = File.CreateText(userProfile.FilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(fs, userProfile);
            }
            return userProfile;
        }

        public static void DeleteUserProfile(string name, string serverPath)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            if (serverPath == null) { throw new ArgumentNullException(nameof(serverPath)); }

            string userProfileRoot = OSVRDirectories.GetUserProfileDirectory(serverPath);
            var profilePath = Path.Combine(userProfileRoot, GetUserProfileFileName(name));
            // We're idempotent. Deleting a non-existant file is OK, just do nothing.
            if(File.Exists(profilePath))
            {
                File.Delete(profilePath);
            }
        }
    }
}
