using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SharkDevelop.GeoIp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //enable cors cross browsing to allow calls from localhost to localhost
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();            
        }
    }
}
