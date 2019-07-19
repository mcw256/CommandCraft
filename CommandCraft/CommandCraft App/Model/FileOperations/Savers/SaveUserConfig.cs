using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.FileOperations.Loaders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Savers
{
    class SaveUserConfig : Saver<UserConfig>
    {
        public override Response Save(UserConfig userConfig, string path = "")
        {  
            try
            {
                string serializedConfig = JsonConvert.SerializeObject(userConfig); 
                File.WriteAllText(path == "" ? LoadUserConfig.fileName : $@"{path}\{LoadUserConfig.fileName}", serializedConfig);
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
