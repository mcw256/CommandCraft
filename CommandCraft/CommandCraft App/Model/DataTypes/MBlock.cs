using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class MBlock : Block
    {
        public override BlockInfo BlockInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Mismatch { get; set; }

    }
}
