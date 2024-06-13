using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Utility
{
    public class LoginEventArgs : EventArgs
    {
        public bool SuccessfulConnection { get; init; }
        public HttpStatusCode StatusCode { get; init; }
    }
}
