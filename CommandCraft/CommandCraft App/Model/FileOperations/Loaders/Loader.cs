using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Loaders
{
    abstract class Loader<Resource>
    {
        public abstract Resource Output { get; protected set; }

        public abstract Response Load();
    }

}
