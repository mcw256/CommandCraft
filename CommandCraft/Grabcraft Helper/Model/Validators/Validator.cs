using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Validators
{
    abstract class Validator<TInput1>
    {
        public abstract Response Validate(TInput1 a);
    }

}
