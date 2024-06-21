using System;
using System.Net;

namespace ChatClient.Utility;

public class LoginEventArgs : EventArgs
{
    public bool SuccessfulConnection { get; init; }
    public HttpStatusCode StatusCode { get; init; }
}
