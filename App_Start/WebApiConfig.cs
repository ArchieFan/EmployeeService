using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace EmployeeService
{
    //public class CustomJsonFormatter : JsonMediaTypeFormatter
    //{
    //    public CustomJsonFormatter()
    //    {
    //        this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
    //    }

    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new MyHttpControllerRouteHandler();

            //config.Filters.Add(new RequireHttpsAttribute());

            //config.Filters.Add(new AuthorizeAttribute());



            // CORS
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //JSONP
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //// (Approach 2) How to return JSON instead of XML from ASP.NET Web API Service when a request is made from the browser.
            //config.Formatters.Add(new CustomJsonFormatter());


            //// (Approach 1) How to return JSON instead of XML from ASP.NET Web API Service when a request is made from the browser.
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //// How to return only XML from ASP.NET web API service.
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //// to properly indent and use camel case instead of pascal case for property names modify the serialization setting of JSON formatter.
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
