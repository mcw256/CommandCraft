using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class BlockMInfo : BlockInfo
    {
        public BlockMInfo(string name, List<string> atributes, bool isMismatched = false)
        {
            Name = name;
            Attributes = atributes;
            IsMismatched = isMismatched;
        }

        public sealed override string Info
        {
            get
            {
                if (Attributes.Count == 0) return Name;

                StringBuilder result = new StringBuilder();
                result.Append($"{Name}[");
                for (int i = 0; i <= Attributes.Count - 2; i++)
                {
                    result.Append($"{Attributes[i]},");
                }
                result.Append($"{Attributes[Attributes.Count-1]}]");
                return result.ToString();
            }

            protected set => Info = value; // obligatory code
        }

        public override string Name { get; protected set; }

        public override List<string> Attributes { get; protected set; }

        public bool IsMismatched { get; set; }
    }
}
