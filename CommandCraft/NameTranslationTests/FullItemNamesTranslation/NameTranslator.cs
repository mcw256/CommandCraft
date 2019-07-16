using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameTranslationTests.FullItemNamesTranslation
{
    class NameTranslator
    {
        Dictionary<string, string> blockNamesDictionary;
        Dictionary<string, string> blockAtributesDictionary;

        public NameTranslator(Dictionary<string,string> _blockNamesDictionary, Dictionary<string, string> _blockAtributesDictionary)
        {
            blockNamesDictionary = _blockNamesDictionary;
            blockAtributesDictionary = _blockAtributesDictionary;
        }

        public (bool isError, string output, string error) Translate(string block)
        {
            throw new NotImplementedException();

            //if (AreThereAnyAttributes() == false)
            //{
            //    TranslateName(block)

            //}
            //else
            //{



            //}


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




    }
}
