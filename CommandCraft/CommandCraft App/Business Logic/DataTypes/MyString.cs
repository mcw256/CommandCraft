using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Business_Logic.DataTypes
{

    /// <summary>
    /// Created this wrapper to enable storing reference to string
    /// </summary>
    public class MyString
    {
        public string Value { get; set; }

        public MyString()
        {

        }

        public MyString( string _text)
        {
            Value = _text;
        }

        public MyString( MyString _ohter)
        {
            Value = _ohter.Value;
        }

    }
}
