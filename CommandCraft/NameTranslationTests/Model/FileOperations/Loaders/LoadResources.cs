using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Loaders
{
    abstract class LoadResources
    {
        public virtual string ResourceName => GetType().Name;
        public abstract bool Load();
    }

}
