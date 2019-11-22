using CommandCraft.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;

namespace CommandCraft.Model.Validators
{
    abstract class Validator<TInput1>
    {
        public abstract Response Validate(TInput1 a);
    }

}
