using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.Model.DataTypes;
using Newtonsoft.Json;

namespace Grabcraft_Helper.Model.FileOperations.Loaders
{

    class LoadBlockNamesDictionary : Loader<Dictionary<string, string>>
    {
        public static readonly string fileName = "block names dictionary.json";

        public override Dictionary<string, string> Output { get; protected set; } = new Dictionary<string, string>();

        /// <summary>
        /// Loads and deserializes dictionary
        /// </summary>
        /// <returns></returns>
        public override Response Load()
        {
            try
            {
                string rawJson = File.ReadAllText(fileName);
                Output = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawJson);

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
