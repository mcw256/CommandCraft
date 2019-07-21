using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing.LayermapDeserializerUtils.DataTypes
{
    class RawGBlock
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int S { get; set; }
        public string H { get; set; }
        public Nullable<int> Y1 { get; set; }
        public Nullable<int> X2 { get; set; }
    }
}
