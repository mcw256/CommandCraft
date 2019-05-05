using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Business_Logic.DataTypes
{
    public class MyInt
    {
        public int Value { get; set; }

        public MyInt()
        {

        }

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
