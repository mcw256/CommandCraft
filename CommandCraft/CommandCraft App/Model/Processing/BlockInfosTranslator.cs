using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing
{
    class BlockInfosTranslator : Processor<List<MBlock>, List<GBlock>>
    {
        Dictionary<string, string> namesDictionary;
        Dictionary<string, string> attributesDictionary;
        
        public override List<MBlock> Output { get; protected set; }

        public BlockInfosTranslator(Dictionary<string, string> _namesDictionary, Dictionary<string, string> _attributesDictionary)
        {
            namesDictionary = _namesDictionary;
            attributesDictionary = _attributesDictionary;
        }

        public override Response Process(List<GBlock> gBlocks)
        {
            try
            {
                foreach (var item in gBlocks)
                {
                    if (item.Info.isValid == false || IsExceptional(item.Info))
                    {
                        ServeExceptionalBlockInfos(item.Info, Output);
                        continue;
                    }
                    
                    if(item.Info.Attributes.Count == 0)
                    {
                        var dic = new SearchNamesDictionary(namesDictionary)

                        if (dic.Search(item.Info.Name) == false)
                        {
                            Output.Add(new MBlock(item.X, item.Y, item.Z, new BlockMInfo(item.Info.Name, new List<string>(), true)));
                            continue;
                        }
                        else
                        {
                            Output.Add(new MBlock(item.X, item.Y, item.Z, new BlockMInfo(dic.Output, new List<string>())));
                            continue;
                        }
                    }
                    else
                    {
                        var ndic = SearchNamesDictionary(namesDictionary);
                        var adic = SearchAttributesDictionary(attributesDictionary);

                        if (ndic.Serach(item.Info.Name) == false)
                        {
                            Output.Add(new MBlock(item.X, item.Y, item.Z, new BlockMInfo(item.Info.Info, new List<string>(), true)));
                            continue;
                        }
     
                        if(adic.Serach(attributesDictionary) == false)
                        {
                            Output.Add(new MBlock(item.X, item.Y, item.Z, new BlockMInfo(item.Info.Info, new List<string>(), true)));
                            continue;
                        }
                        else
                        {
                            Output.Add(new MBlock(item.X, item.Y, item.Z, new BlockMInfo(ndic.Output, adic.Output)));
                            continue;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return new Response(false, "");
        }
    }
}
