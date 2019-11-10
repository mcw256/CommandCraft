using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing.BlockInfosTranslatorUtils
{
    static class TranslateExtraordinaryBlocks
    {
        static readonly Dictionary<string, NameAndAttributes> extraordinaryInfosDictionary = new Dictionary<string, NameAndAttributes>
        {
            #region Redstone Torch
            { "Redstone Torch",
            new NameAndAttributes
            (
                "minecraft:redstone_torch",
                new List<string>{}
            )
            },

            {"Redstone Torch (Facing Up)",
            new NameAndAttributes
            (
                "minecraft:redstone_torch",
                new List<string>{}
            )
            },

            {"Redstone Torch (Facing East)",
            new NameAndAttributes
            (
                "minecraft:redstone_wall_torch",
                new List<string>{ "facing=east" }
            )
            },

            {"Redstone Torch (Facing West)",
            new NameAndAttributes
            (
                "minecraft:redstone_wall_torch",
                new List<string>{ "facing=west" }
            )
            },

            {"Redstone Torch (Facing North)",
            new NameAndAttributes
            (
                "minecraft:redstone_wall_torch",
                new List<string>{ "facing=north" }
            )
            },

            {"Redstone Torch (Facing South)",
            new NameAndAttributes
            (
                "minecraft:redstone_wall_torch",
                new List<string>{ "facing=south" }
            )
            },
            #endregion Redstone Torch

            #region Torch
            { "Torch",
            new NameAndAttributes
            (
                "minecraft:torch",
                new List<string>{}
            )
            },

            {"Torch (Facing Up)",
            new NameAndAttributes
            (
                "minecraft:torch",
                new List<string>{}
            )
            },

            {"Torch (Facing East)",
            new NameAndAttributes
            (
                "minecraft:wall_torch",
                new List<string>{ "facing=east" }
            )
            },

            {"Torch (Facing West)",
            new NameAndAttributes
            (
                "minecraft:wall_torch",
                new List<string>{ "facing=west" }
            )
            },

            {"Torch (Facing North)",
            new NameAndAttributes
            (
                "minecraft:wall_torch",
                new List<string>{ "facing=north" }
            )
            },

            {"Torch (Facing South)",
            new NameAndAttributes
            (
                "minecraft:wall_torch",
                new List<string>{ "facing=south" }
            )
            }
            #endregion Torch
        };

        public static BlockMInfo Translate(string info)
        {
            if (extraordinaryInfosDictionary.ContainsKey(info))
            {
                var result = extraordinaryInfosDictionary[info];

                return new BlockMInfo(result.Name, result.Attributes);
            }
            else
                return new BlockMInfo(info, new List<string>(), true);
        }

        class NameAndAttributes
        {
            public NameAndAttributes(string name, List<string> attributes)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            }
            public string Name { get; set; }
            public List<string> Attributes { get; set; }
        }
    }

}
