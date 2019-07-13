using LayermapParser.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LayermapParser.DataProcess;
using System.Text.RegularExpressions;

namespace LayermapParser
{
    /// <summary>
    /// This class provides one method Parse() which returns parsed layermap data
    /// </summary>
    public static class LayerMapJsonParser
    {
        /// <summary>
        ///  Method that parses layermap json and returns new List of BuildingsRawNamed
        /// </summary>
        /// <param name="json">Raw layermap parameter with "var layermap = " at the begining</param>
        /// <returns>
        /// List of Buildings Raw Named. Note that all coords starts from 0!!
        /// </returns>
        public static List<BlockRawNamed> Parse(string json)
        {
            json =  Regex.Replace(json, @"^.+=\s*", "");

            Dictionary<string, List<RawItem>> rawData = JsonConvert.DeserializeObject<Dictionary<string, List<RawItem>>>(json);
            List<FirstPhaseData> firstPhaseDatas = FirstPhaseProcess.Process(rawData);

            List<BlockRawNamed> output = SecondPhaseProcess.Process(firstPhaseDatas);
            return output;
        }
    }
}
