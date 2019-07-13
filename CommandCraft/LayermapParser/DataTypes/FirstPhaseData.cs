using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayermapParser.DataTypes
{
    /// <summary>
    /// Container for data after first phase of processing
    /// </summary>
    class FirstPhaseData
    {
        public FirstPhaseData(int planeNo, int x, int y, int sideLength, string name)
        {
            PlaneNo = planeNo;
            X = x;
            Y = y;
            SideLength = sideLength;
            Name = name;
        }

        public int PlaneNo { get; set; } // string from Dictionary 
        public int X { get; set; }
        public int Y { get; set; }
        public int SideLength { get; set; } // s from RawItem
        public string Name { get; set; } // h from RawItem
    }
}
