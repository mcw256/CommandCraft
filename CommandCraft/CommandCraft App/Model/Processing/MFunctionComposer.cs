﻿using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing
{
    class MFunctionComposer : Processor<MFunction, List<MBlock>, HowToHandleMismatch, string>
    {
        public override MFunction Output { get; protected set; }

        public override Response Process(List<MBlock> mBlocks, HowToHandleMismatch howToHandleMismatch, string buildingName)
        {
            try
            {




            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }


            return new Response(false, "");
        }
    }
}
