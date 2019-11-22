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
    class ValidateBuildingUrl : Validator<string>
    {
        public override Response Validate(string buildingUrl)
        {
            try
            {
                string standardUrlValidation = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$"; //https://www.regextester.com/94502
                string myValidation = @"grabcraft\.com/minecraft";

                if ( (Regex.IsMatch(buildingUrl, standardUrlValidation) || Regex.IsMatch(buildingUrl, myValidation)) == false )
                    return new Response(true, "Invalid URL error");
            }
            catch (Exception)
            {             
                    return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
