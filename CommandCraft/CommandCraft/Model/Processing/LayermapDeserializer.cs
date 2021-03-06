﻿using CommandCraft.Model.Processing.LayermapDeserializerUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;
using CommandCraft.Model.Processing.LayermapDeserializerUtils.DataTypes;

namespace CommandCraft.Model.Processing
{
    class LayermapDeserializer : Processor<DeserializedLayermap, string>
    {
        public override DeserializedLayermap Output { get; protected set; } = new DeserializedLayermap();

        public override Response Process(string layermap)
        {
            try
            {
                layermap = Regex.Replace(layermap, @"^.+=\s*", "");
                Dictionary<string, List<RawGBlock>> rawData = JsonConvert.DeserializeObject<Dictionary<string, List<RawGBlock>>>(layermap);

                Output.Interval = (rawData.Values.First().First().S) - 1;

                foreach (var dicItem in rawData)
                {
                    int z = int.Parse(dicItem.Key);

                    foreach (var listItem in dicItem.Value)
                        Output.GBlocks.Add(new GBlock(new Coords(listItem.X, listItem.Y, z), new BlockGInfo(listItem.H)));
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
