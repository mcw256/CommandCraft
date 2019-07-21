using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class BlockGInfo : BlockInfo
    {
        public BlockGInfo(string info)
        {
            Info = info;
        }

        public override string Name
        {
            get
            {
                if (IsValid == false) return ""; // well, I could throw exception but nah

                return Regex.Replace(Info, @"\s\([\w\s\-,]+\)", "");
            }

            set => Name = value; // obligatory code

        }
        public override List<string> Attributes
        {
            get
            {
                List<string> result = new List<string>();
                if (IsValid == false) return result; // well, I could throw exception but nah

                foreach (var item in Regex.Matches(Info, @"[\w\s\-]+[,\)]"))
                {
                    string helper = item.ToString();
                    helper = Regex.Replace(helper, @"^\s", "");
                    helper = Regex.Replace(helper, @"[,\)]$", "");
                    result.Add(helper);
                }
                return result;
            }
            set => Attributes = value; // obligatory code
        }

        public bool IsValid
        {
            get
            {
                return Regex.IsMatch(Info, @"^[\w\s']+\s(\([\w\s\-,]+\))?$");
            }
        }

        public bool HasAttributes
        {
            get
            {
                if (Attributes.Count == 0) return false;
                return true;
            }
        }
    }
}
