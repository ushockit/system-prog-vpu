using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Middlewares
{
    public class AuthUserMiddleware
    {
        readonly RequestDelegate _next;

        public AuthUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm='localhost'");
                // context.Response.WriteAsync("");
            }
            else
            {
                string authorizationHeader = context.Request.Headers["Authorization"].ToString();
                string base64Data = authorizationHeader.Split(' ')[1];
                string authDataStr = Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
                string[] authData = authDataStr.Split(':');
                string login = authData[0];
                string pswd = authData[1];

                if (!login.Equals("admin") && !pswd.Equals("admin"))
                {
                    context.Response.StatusCode = 401;
                    context.Response.Headers.Add("WWW-Authenticate", "Basic realm='localhost'");
                }
                await _next.Invoke(context);
            }
        }
    }
}
