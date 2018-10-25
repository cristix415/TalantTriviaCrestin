using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace Talant
{
    /// <summary>
    /// Summary description for ws
    /// </summary>
    public class ws : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            if (context.IsWebSocketRequest)

                context.AcceptWebSocketRequest(new TestWebSocketHandler());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}