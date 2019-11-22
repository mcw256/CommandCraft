using CommandCraft.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;

namespace CommandCraft.Model.Extractors
{
    abstract class Extractor<TOutput, TInput1>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Extract(TInput1 a);

    }
    abstract class Extractor<TOutput, TInput1, TInput2>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Extract(TInput1 a, TInput2 b);

    }
    abstract class Extractor<TOutput, TInput1, TInput2, TInput3>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Extract(TInput1 a, TInput2 b, TInput3 c);

    }
}
