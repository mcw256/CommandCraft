using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Validators
{
    abstract class Validator
    {
        public virtual string ValidatorName => GetType().Name;
        public abstract bool Validate();
    }
}
