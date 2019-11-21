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
        public UserConfig() { }

        public UserConfig(string minecraftPath, string defaultGameSave, HowToHandleMismatch defaultMismatchOption)
        {
            MinecraftPath = minecraftPath ?? throw new ArgumentNullException(nameof(minecraftPath));
            DefaultGameSave = defaultGameSave ?? throw new ArgumentNullException(nameof(defaultGameSave));
            DefaultMismatchOption = defaultMismatchOption;
        }

        private string _minecraftPath;
        public string MinecraftPath
        {
            get => _minecraftPath.Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            set => _minecraftPath = value;
        }
        public string DefaultGameSave { get; set; }
        public HowToHandleMismatch DefaultMismatchOption { get; set; }

    }
}
