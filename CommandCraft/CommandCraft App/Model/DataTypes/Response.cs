using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class Response
    {
        public Response(bool isError, string errorMsg)
        {
            IsError = isError;
            ErrorMsg = errorMsg ?? throw new ArgumentNullException(nameof(errorMsg));
        }

        public bool IsError { get; private set; }

        public string ErrorMsg { get; private set; }

    }
}
