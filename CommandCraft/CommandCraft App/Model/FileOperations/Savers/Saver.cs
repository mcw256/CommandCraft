using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.FileOperations.Savers
{
    abstract class Saver<TInput1>
    {
        public abstract Response Save(TInput1 a);  
    }

    abstract class Saver<TInput1, TInput2>
    {
        public abstract Response Save(TInput1 a, TInput2 b);
    }

    abstract class Saver<TInput1, TInput2, TInput3>
    {
        public abstract Response Save(TInput1 a, TInput2 b, TInput3 c);
    }

}
