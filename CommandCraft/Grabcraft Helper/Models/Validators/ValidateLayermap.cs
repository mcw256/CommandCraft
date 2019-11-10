using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Grabcraft_Helper.Model.DataTypes;

namespace Grabcraft_Helper.Model.Validators
{
    class ValidateLayermap : Validator<string>
    {
        public override Response Validate(string layermap)
        {
            try
            {
                string myValidation = @"([\w\s]+=\s*)\{.+\}";
                string myValidation2 = @"([\w\s]+=\s*)\[\]";

                if (Regex.IsMatch(layermap, myValidation) == false)
                {
                    if (Regex.IsMatch(layermap, myValidation2))
                        return new Response(true, "Sorry, that building is not avaliable!");

                    return new Response(true, "Error");
                }
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
