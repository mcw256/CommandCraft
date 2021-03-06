﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCraft.Model.DataTypes
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

            set {}

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

        public bool IsValid => (Regex.IsMatch(Info, @"^[\w\s']+\s(\([\w\s\-,]+\))?$") || Regex.IsMatch(Info, @"^\w[\w\s']+$"));
    }
}
