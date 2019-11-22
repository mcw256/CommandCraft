using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;

namespace CommandCraft.Model.Downloaders
{
    abstract class Downloader<TOutput, TInput>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Download(TInput a);

    }
}
