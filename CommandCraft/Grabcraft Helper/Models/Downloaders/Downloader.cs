using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Downloaders
{
    abstract class Downloader<TOutput, TInput>
    {
        public abstract TOutput Output { get; protected set; }

        public abstract Response Download(TInput a);

    }
}
