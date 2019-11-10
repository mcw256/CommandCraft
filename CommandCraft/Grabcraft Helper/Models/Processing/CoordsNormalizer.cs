using Grabcraft_Helper.Model.DataTypes;
using Grabcraft_Helper.Model.Processing.CoordsNormalizerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing
{
    class CoordsNormalizer : Processor<List<Coords>, List<Coords>, int>
    {
        public override List<Coords> Output { get; protected set; } = new List<Coords>();

        public override Response Process(List<Coords> coords, int interval)
        {
            try
            {
                int yOrigin = Utils.CalculateYOrigin(coords, interval);
                int xOrigin = 5;

                foreach (var item in coords)
                {
                    int newX = (item.X - xOrigin) / interval;
                    int newY = (item.Y - yOrigin) / interval;
                    int newZ = item.Z - 1;

                    Output.Add(new Coords(newX, newY, newZ));
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
