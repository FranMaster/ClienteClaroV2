using System;
using System.Collections.Generic;
using System.Text;

namespace ClaroClient2.Helpers
{
    public interface IServiceCaller
    {
        void MakeCall(string phone, string monto, string pin);
    }
}
