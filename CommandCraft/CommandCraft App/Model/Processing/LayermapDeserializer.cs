using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.Processing.LayermapDeserializerUtils;
using CommandCraft_App.Model.Processing.LayermapDeserializerUtils.DataTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing
{
    class LayermapDeserializer : Processor<CoordsListAndInterval, string>
    {
        public override CoordsListAndInterval Output { get; protected set; }

        public override Response Process(string layermap)
        {
            try
            {
                layermap = Regex.Replace(layermap, @"^.+=\s*", "");
                Dictionary<string, List<RawGBlock>> rawData = JsonConvert.DeserializeObject<Dictionary<string, List<RawGBlock>>>(layermap);

                Utils.GetRidOffUnecessaryData(rawData);

                Output.Interval = (rawData.Values.First().First().S) - 1;

                foreach (var dicItem in rawData)
                {
                    int z = int.Parse(dicItem.Key);

                    foreach (var listItem in dicItem.Value)
                    {
                        Output.Coords.Add(new Coords(listItem.X, listItem.Y, z));
                    }
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
