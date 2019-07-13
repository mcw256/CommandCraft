using LayermapParser.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayermapParser.DataProcess
{
    static class SecondPhaseProcess
    {
        public static List<BlockRawNamed> Process(List<FirstPhaseData> data)
        {
            List<BlockRawNamed> output = new List<BlockRawNamed>();
            int interval = CalcualteInterval(data);
            int startingY = CalculateStartingY(data, interval);

            foreach (var item in data)
            {
                int newX = (item.X - 5) / interval; // starting from 0
                int newY = (item.Y - startingY) / interval; //starting from 0
                int newZ = item.PlaneNo - 1; //starting from 0
                string newName;
                if (item.Name.StartsWith(" "))
                    newName = item.Name.Substring(1);
                else
                    newName = item.Name;
                output.Add(new BlockRawNamed(newX, newY, newZ, newName));
            }

            return output;
        }

        private static int CalculateStartingY(List<FirstPhaseData> data, int interval)
        {
            int numberOfPlanes = data.Max(x => x.PlaneNo);
            return (numberOfPlanes * interval) + 11;

        }

        private static int CalcualteInterval(List<FirstPhaseData> data)
        {
            return data[0].SideLength - 1;
        }

    }
}
