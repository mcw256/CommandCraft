using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Validators
{
    abstract class Validator<TInput1>
    {
        public abstract Response Validate(TInput1 a);
    }

}
