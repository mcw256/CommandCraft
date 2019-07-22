using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Savers.SaveMFunctionUtils
{
    static class Utils
    {
        public static void CreatePackmcmetaFile(string path)
        {
            string content = @" {
           ""pack"": {
           ""pack_format"": 3,
           ""description"": ""Test""
            }
            }";

            File.WriteAllText(path, content);
        }
    }
}
