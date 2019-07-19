using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class MFunction
    {
        public string Name { get; set; }

        public List<string> Content { get; set; }

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
