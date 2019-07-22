using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing.CoordsNormalizerUtils
{
    static class Utils
    {
        public static int CalculateYOrigin(List<Coords> coords, int interval)
        {
            int maxZ = coords.Max(x => x.Z);
            return (maxZ * interval) + 11;
        }

    }
}
