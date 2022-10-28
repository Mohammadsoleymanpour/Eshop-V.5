using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Security
{
    public static class HttpContextManger
    {
        public static string GetIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress.ToString();
        }
    }
}
