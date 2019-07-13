using LayermapParser.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayermapParser.DataProcess
{
    /// <summary>
    /// Returns FirstPhaseData with method Process(). 
    /// Okay, so basically this class could be skipped, if I knew how to deserialize conditionally from json. 
    /// Precisely when x2 or y1 occurs in json, just skip that list item.
    /// </summary>
    static class FirstPhaseProcess
    {
        public static List<FirstPhaseData> Process( Dictionary<string, List<RawItem>> data)
        {
            List<FirstPhaseData> output = new List<FirstPhaseData>();

            foreach (var a in data)
            {
                string plane = a.Key;

                foreach (var b in a.Value)
                {
                    if (b.X2 != null || b.Y1 != null)
                    {
                        output.Add(new FirstPhaseData(int.Parse(plane), b.X, b.Y, b.S, b.H));
                    }
                }
            }
            return output;
        }

    }
}
