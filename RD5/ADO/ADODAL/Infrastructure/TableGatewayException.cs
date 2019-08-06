using System;
using System.Collections.Generic;
using System.Text;

namespace ADODAL.Infrastructure
{
    public class TableGatewayException : Exception
    {
        public TableGatewayException(string name): base(name) { }
    }
}
