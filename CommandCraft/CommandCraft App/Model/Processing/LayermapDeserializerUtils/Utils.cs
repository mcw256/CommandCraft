using CommandCraft_App.Model.Processing.LayermapDeserializerUtils.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing.LayermapDeserializerUtils
{
    static class Utils
    {
        public static void GetRidOffUnecessaryData(Dictionary<string, List<RawGBlock>> rawBlocks)
        {
            foreach (var item in rawBlocks.Values)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].X2 != null || item[i].Y1 != null)
                        item.RemoveAt(i);
                }
            }
        }

    }
}
