using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.DataTypes
{
    class MFunction
    {
        //public string Name { get => $"{Name}.mcfunction"; set => Name = value; }      This line causes infinite recursion and in result stack overflow exception

        private string _name;
        public string Name
        {
            get
            {
                string result = _name.ToLower();
                result = Regex.Replace(result, @"\s", "_");
                result = Regex.Replace(result, @"[^a-z_0-9]", "");

                return result;
            }
            set { _name = value; }
        }



        public List<string> Content { get; set; } = new List<string>();

        /// <summary>
        /// Overrided
        /// </summary>
        /// <returns>Content list elements separated by new line \r\n</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i <= Content.Count-2; i++)
            {
                stringBuilder.Append(Content[i]);
                stringBuilder.Append("\r\n");
            }
            stringBuilder.Append(Content[Content.Count - 1]);

            return stringBuilder.ToString();
        }

    }
}
