using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Savers
{
    abstract class Saver<Resource>
    {
        public abstract Response Save(Resource a, string path);  
    }
}
