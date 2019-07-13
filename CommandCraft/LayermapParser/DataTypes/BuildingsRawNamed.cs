using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayermapParser.DataTypes
{
    public class BuildingsRawNamed
    {
        public BuildingsRawNamed(int x, int y, int z, string name)
        {
            X = x;
            Y = y;
            Z = z;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; } 
        public string Name { get; set; }
    }
}
