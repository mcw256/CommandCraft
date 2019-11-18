using Grabcraft_Helper.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.DataTypes
{
    class UserConfig
    {
        public string MinecraftPath { get; set; }
        public string DefaultGameSave { get; set; }
        public HowToHandleMismatch DefaultMismatchOption { get; set; }

    }
}
