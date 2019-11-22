using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;

namespace CommandCraft.Model.Validators
{
    class ValidateMinecraftPath : Validator<string>
    {
        public override Response Validate(string minecraftPath)
        {
            try
            {
                string standardPathValidation = @"^[a-zA-Z]:\\[\\\S|*\S]?.*$"; //https://www.regextester.com/96741
                string myValidation = @"\\AppData\\Roaming\\.minecraft";

                if ( (Regex.IsMatch(minecraftPath, myValidation) || Regex.IsMatch(minecraftPath, standardPathValidation)) == false)
                    return new Response(true, "Invalid path error");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
