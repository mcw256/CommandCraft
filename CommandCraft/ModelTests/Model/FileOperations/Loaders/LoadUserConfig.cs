using CommandCraft_App.Model.DataTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Loaders
{
    class LoadUserConfig : Loader<UserConfig>
    {
        public static readonly string fileName = "User config.json";
        public override UserConfig Output { get; protected set; } = new UserConfig();

        /// <summary>
        /// Loads and deserializes user config
        /// </summary>
        /// <returns></returns>
        public override Response Load()
        {
            try
            {
                string rawJson = File.ReadAllText(fileName);
                Output = JsonConvert.DeserializeObject<UserConfig>(rawJson);

            }
            catch (FileNotFoundException)
            {
                return new Response(true, "File not found");
            }
            catch (System.Security.SecurityException)
            {
                return new Response(true, "No permission to file");
            }
            catch (IOException)
            {
                return new Response(true, "Opening file error");
            }

            catch (JsonReaderException)
            {
                return new Response(true, "Reading file error(deserialization error)");
            }

            catch (Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");
        }
    }
}
