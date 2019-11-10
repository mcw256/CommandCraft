using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.FileOperations.Loaders
{
    abstract class Loader<TOutput>
    {
        public abstract TOutput Output { get; protected set; }
        public abstract Response Load();
    }

    abstract class Loader<TOutput, TInput1>
    {
        public abstract TOutput Output { get; protected set; }
        public abstract Response Load(TInput1 a);
    }

    abstract class Loader<TOutput, TInput1, TInput2>
    {
        public abstract TOutput Output { get; protected set; }
        public abstract Response Load(TInput1 a, TInput2 b);
    }

    abstract class Loader<TOutput, TInput1, TInput2, TInput3>
    {
        public abstract TOutput Output { get; protected set; }
        public abstract Response Load(TInput1 a, TInput2 b, TInput3 c);
    }

}
