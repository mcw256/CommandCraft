using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.DataTypes
{
    public class MyInt
    {
        public int Value { get; set; }

        public MyInt(int _num)
        {
            Value = _num;
        }

        public MyInt(MyInt _ohter)
        {
            Value = _ohter.Value;
        }
    }
}
