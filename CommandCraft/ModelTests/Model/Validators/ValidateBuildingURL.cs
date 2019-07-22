using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft_App.Model.DataTypes;

namespace CommandCraft_App.Model.Validators
{
    class ValidateBuildingURL : Validator<string>
    {
        public override Response Validate(string buildingURL)
        {
            try
            {
                string standardUrlValidation = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$"; //https://www.regextester.com/94502
                string myValidation = @"grabcraft\.com/minecraft";

                if ( (Regex.IsMatch(buildingURL, standardUrlValidation) || Regex.IsMatch(buildingURL, myValidation)) == false )
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
