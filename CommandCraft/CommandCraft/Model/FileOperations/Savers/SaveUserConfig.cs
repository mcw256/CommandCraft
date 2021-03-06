﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;
using CommandCraft.Model.FileOperations.Loaders;

namespace CommandCraft.Model.FileOperations.Savers
{
    class SaveUserConfig : Saver<UserConfig>
    {
        public override Response Save(UserConfig userConfig)
        {  
            try
            {
                string serializedConfig = JsonConvert.SerializeObject(userConfig); 
                File.WriteAllText($@"programdata\{LoadUserConfig.FileName}", serializedConfig); //LoadUserConfig.fileName is a static readonly resource.
            }
            catch (DirectoryNotFoundException)
            {
                return new Response(true, "Invalid directory error");
            }
            catch (UnauthorizedAccessException)
            {
                return new Response(true, "No permission to write in this directory error");
            }
            catch (System.Security.SecurityException)
            {
                return new Response(true, "No permission to write in this directory error");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");

        }
    }
}
