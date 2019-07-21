using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class CoordsListAndInterval
    {
        public CoordsListAndInterval(List<Coords> coords, int interval)
        {
            Coords = coords ?? throw new ArgumentNullException(nameof(coords));
            Interval = interval;
        }
        public List<Coords> Coords { get; set; }
        public int Interval { get; set; }

    }
}
