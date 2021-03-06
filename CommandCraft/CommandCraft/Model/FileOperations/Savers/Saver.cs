﻿using CommandCraft.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;

namespace CommandCraft.Model.FileOperations.Savers
{
    abstract class Saver<TInput1>
    {
        public abstract Response Save(TInput1 a);  
    }

    abstract class Saver<TInput1, TInput2>
    {
        public abstract Response Save(TInput1 a, TInput2 b);
    }

    abstract class Saver<TInput1, TInput2, TInput3>
    {
        public abstract Response Save(TInput1 a, TInput2 b, TInput3 c);
    }

}
