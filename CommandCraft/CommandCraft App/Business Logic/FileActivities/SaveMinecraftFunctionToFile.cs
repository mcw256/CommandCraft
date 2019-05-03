using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Business_Logic.FileActivities
{
    static class SaveMinecraftFuntionToFile
        {
            public static void Save(List<string> _minecraftFunction, string _path)
            {
                File.WriteAllLines(_path, _minecraftFunction);
            }
        }
    
}
