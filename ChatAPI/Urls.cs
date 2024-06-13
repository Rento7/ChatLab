using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI
{
    // they should be configurable but for demo/debug purpose let it be
    public static class Urls
    {
        public const string BaseUrl = "http://localhost:5177/";
        public const string ChatUrl = BaseUrl + "/chat";
        public const string LoginUrl = BaseUrl + "/login";
    }
}
