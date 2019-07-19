using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Downloaders
{
    abstract class Downloader<T>
    {
        public abstract T Output { get; protected set; }

        public abstract Response Download(string);

    }
}
