using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.Processing.CoordsNormalizerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing
{
    class CoordsNormalizer : Processor<List<Coords>, CoordsListAndInterval>
    {
        public override List<Coords> Output { get; protected set; }

        public override Response Process(CoordsListAndInterval coordsListAndInterval)
        {
            try
            {
                int yOrigin = Utils.CalculateYOrigin(coordsListAndInterval);
                int xOrigin = 5;

                foreach (var item in coordsListAndInterval.Coords)
                {
                    int newX = (item.X - xOrigin) / coordsListAndInterval.Interval;
                    int newY = (item.Y - yOrigin) / coordsListAndInterval.Interval;
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
