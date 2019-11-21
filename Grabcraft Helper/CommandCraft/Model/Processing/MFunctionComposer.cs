using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing
{
    class MFunctionComposer : Processor<MFunction, List<MBlock>, HowToHandleMismatch, string>
    {
        public override MFunction Output { get; protected set; } = new MFunction();

        public override Response Process(List<MBlock> mBlocks, HowToHandleMismatch howToHandleMismatch, string buildingName)
        {
            try
            {
                Output.Name = buildingName != "" ? buildingName : throw new Exception();

                foreach (var item in mBlocks)
                {
                    string line;
                    if (item.Info.IsMismatched)
                    {
                        switch (howToHandleMismatch)
                        {
                            case HowToHandleMismatch.Ignore:
                                break;
                            case HowToHandleMismatch.RedWool:
                                line = $"setblock ~{item.Coords.X} ~{item.Coords.Z} ~{item.Coords.Y} minecraft:red_wool replace";
                                Output.Content.Add(line);
                                break;
                            case HowToHandleMismatch.SignWithText:
                                line = $@"setblock ~{item.Coords.X} ~{item.Coords.Z} ~{item.Coords.Y} minecraft:oak_sign" + @"{Text1:""{\""text\"":\""" + $"{item.Info.Name}" + @"\""}""} replace";
                                Output.Content.Add(line);
                                break;

                            default:
                                throw new Exception();

                        }
                    }
                    else
                    {
                        StringBuilder helper = new StringBuilder($@"setblock ~{item.Coords.X} ~{item.Coords.Z} ~{item.Coords.Y} {item.Info.Name}");
                        if (item.Info.HasAttributes)
                        {
                            helper.Append("[");
                            for (int i = 0; i <= item.Info.Attributes.Count - 2; i++)
                                helper.Append($"{item.Info.Attributes[i]},");

                            helper.Append($"{item.Info.Attributes[item.Info.Attributes.Count - 1]}]");
                        }
                        helper.Append(" replace");
                        Output.Content.Add(helper.ToString());
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
