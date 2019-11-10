using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing
{
    abstract class Processor<TOutput, TInput1>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Process(TInput1 a);

    }
    abstract class Processor<TOutput, TInput1, TInput2>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Process(TInput1 a, TInput2 b);

    }
    abstract class Processor<TOutput, TInput1, TInput2, TInput3>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Process(TInput1 a, TInput2 b, TInput3 c);

    }
}
