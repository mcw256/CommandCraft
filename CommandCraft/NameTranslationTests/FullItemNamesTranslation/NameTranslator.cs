using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NameTranslationTests.FullItemNamesTranslation.Utils.Utils;

namespace NameTranslationTests.FullItemNamesTranslation
{
    class NameTranslator
    {
        Dictionary<string, string> blockNamesDictionary;
        Dictionary<string, string> blockAtributesDictionary;

        public NameTranslator(Dictionary<string, string> _blockNamesDictionary, Dictionary<string, string> _blockAtributesDictionary)
        {
            blockNamesDictionary = _blockNamesDictionary;
            blockAtributesDictionary = _blockAtributesDictionary;
        }

        public (bool isError, string output, string error) Translate(string block)
        {
            if (AreThereAnyAttributes(block))
            {
                if (CutAtributes(block).Contains("Torch"))
                    return HandleExceptionaryItems(block);

                if (ExtractAttributesList(block).Contains("On The Floor") || ExtractAttributesList(block).Contains("Normal")
                    || ExtractAttributesList(block).Contains("Inactive") || ExtractAttributesList(block).Contains("Unactive"))
                    return TranslateName(CutAtributes(block));

                var nameRes = TranslateName(CutAtributes(block));
                var attRes = TranslateAtributes(ExtractAttributesList(block));

                if (nameRes.isError == false && attRes.isError == false)
                {
                    //minecraft:acacia_door[facing=east,half=lower]
                    StringBuilder result = new StringBuilder();
                    result.Append($"{nameRes.output}[");

                    for (int i = 0; i <= attRes.output.Count - 2; i++)
                    {
                        result.Append($"{attRes.output[i]},");
                    }
                    result.Append($"{attRes.output[attRes.output.Count - 1]}]");

                    return (false, result.ToString(), "");
                }
                else return (true, "", nameRes.isError ? $"Block name mismatch:{nameRes.error} in block:{block}" : $"Attribute name mismatch:{attRes.error} in block:{block}");
            }
            else
            {
                var nameRes = TranslateName(block);

                if (nameRes.isError == false)
                    return (false, nameRes.output, "");
                else return (true, "", $"Block name mismatch:{nameRes.error} in block:{block} attributes not found!");

            }
        }

        private (bool isError, string output, string error) TranslateName(string block)
        {
            //check if name appears in dictionary
            if (blockNamesDictionary.ContainsKey(block))
                return (false, blockNamesDictionary[block], "");

            else
                return (true, "", block);
        }

        private (bool isError, List<string> output, string error) TranslateAtributes(List<string> attributes)
        {
            List<string> result = new List<string>();
            foreach (var item in attributes)
            {
                if (blockAtributesDictionary.ContainsKey(item))
                    result.Add(blockAtributesDictionary[item]);
                else
                    return (true, null, item);
            }
            return (false, result, "");
        }

        private (bool isError, string output, string error) HandleExceptionaryItems(string block)
        {
            if (CutAtributes(block).Contains("Torch"))
            {
                if (ExtractAttributesList(block).Contains("Facing Up"))
                    return TranslateName(CutAtributes(block));

                else
                {
                    var nameRes = TranslateName(CutAtributes($"Wall {block}"));
                    var attRes = TranslateAtributes(ExtractAttributesList(block));

                    if (nameRes.isError == false && attRes.isError == false)
                    {
                        StringBuilder result = new StringBuilder();
                        result.Append($"{nameRes.output}[");

                        for (int i = 0; i <= attRes.output.Count - 2; i++)
                        {
                            result.Append($"{attRes.output[i]},");
                        }
                        result.Append($"{attRes.output[attRes.output.Count - 1]}]");

                        return (false, result.ToString(), "");
                    }
                    else return (true, "", nameRes.isError ? $"Block name mismatch:{nameRes.error} in block:{block}" : $"Attribute name mismatch:{attRes.error} in block:{block}");
                }
            }

            return (true, "", "Error in function HandleExceptionaryNames. Basically this should never be returned"); // i'm so ashamed of this code
        }

    }

}
