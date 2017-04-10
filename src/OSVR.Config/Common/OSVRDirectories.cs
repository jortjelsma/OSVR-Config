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
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OSVR.Config.Common
{
    /// <summary>
    /// A utility class to get OSVR-Specific directories.
    /// </summary>
    public static class OSVRDirectories
    {
        const string OSVRDirectoryName = "OSVR";

        /// <summary>
        /// Get the current user-specific OSVR settings root directory, if it exists,
        /// else a global folder.
        /// </summary>
        /// <param name="serverPath">The OSVR server root directory.</param>
        /// <param name="create">Pass true (default) if the directory should be created,
        /// if it doesn't exist.</param>
        /// <returns>The user-specific OSVR settings root directory, else a global
        /// folder.</returns>
        public static string GetUserDirectory(string serverPath, bool create = true)
        {
            if (serverPath == null) { throw new ArgumentNullException("serverPath"); }
            string osvrUserDirectory = null;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // @todo: test on Windows 7/8/10
                // Windows 7: ???
                // Windows 8: ???
                // Windows 10: Tested, works.
                osvrUserDirectory = Environment.GetEnvironmentVariable("LocalAppData");
                if (String.IsNullOrWhiteSpace(osvrUserDirectory))
                {
                    osvrUserDirectory = "C:\\";
                }

                osvrUserDirectory = Path.Combine(osvrUserDirectory, OSVRDirectoryName);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var xdg_cache_dir = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
                if(!String.IsNullOrWhiteSpace(xdg_cache_dir))
                {
                    osvrUserDirectory = xdg_cache_dir;
                }
                else
                {
                    var homeDir = Environment.GetEnvironmentVariable("HOME");
                    osvrUserDirectory = Path.Combine(homeDir, ".cache");
                }
                osvrUserDirectory = Path.Combine(osvrUserDirectory, OSVRDirectoryName);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                osvrUserDirectory = Environment.GetEnvironmentVariable("HOME");
                if(String.IsNullOrWhiteSpace(osvrUserDirectory))
                {
                    throw new Exception("HOME should be defined on OSX. It is not. This is unexpected.");
                }
                osvrUserDirectory = Path.Combine(osvrUserDirectory, "Library", "Application Support", OSVRDirectoryName);
            }
            
            if(String.IsNullOrWhiteSpace(osvrUserDirectory))
            {
                osvrUserDirectory = serverPath;
            }

            if(create && !Directory.Exists(osvrUserDirectory))
            {
                Directory.CreateDirectory(osvrUserDirectory);
            }
            return osvrUserDirectory;
        }

        static string GetUserSubdirectory(string serverPath, string subdirectoryName, bool create = true)
        {
            if (serverPath == null) { throw new ArgumentNullException("serverPath"); }
            if (subdirectoryName == null) { throw new ArgumentNullException("subdirectoryName"); }

            string osvrUserDirectory = GetUserDirectory(serverPath, create);
            string subdirectory = Path.Combine(osvrUserDirectory, subdirectoryName);
            if (create && !Directory.Exists(subdirectory))
            {
                Directory.CreateDirectory(subdirectory);
            }
            return subdirectory;
        }

        /// <summary>
        /// Get the OSVR user profile root directory.
        /// </summary>
        /// <param name="serverPath">The OSVR server root path.</param>
        /// <param name="create">Pass true to create the directories, if
        /// not already there. true by default.</param>
        /// <returns>The current user profile directory.</returns>
        public static string GetUserProfileDirectory(string serverPath, bool create = true)
        {
            return GetUserSubdirectory(serverPath, "profiles", create);
        }

        /// <summary>
        /// Get the OSVR user config root directory.
        /// </summary>
        /// <param name="serverPath">The OSVR server root path.</param>
        /// <param name="create">Pass true to create the directories, if
        /// not already there. true by default.</param>
        /// <returns>The current user config directory.</returns>
        public static string GetUserConfigDirectory(string serverPath, bool create = true)
        {
            return GetUserSubdirectory(serverPath, "config", create);
        }
    }
}
