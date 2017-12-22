using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.HttpHandler
{
    public class SampleHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string responseString = @"<!DOCTYPE HTML> 
                                        <html>
                                        <head>
                                            <meta charset='UTF-8'>
                                            <title>Sample Handler</title>
                                        </head>
                                        <body>
                                            <h1>Hello from the custom handler</h1>
                                            <div>{0}</div>
                                        </body>
                                        </html>";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/html";
            response.Write(string.Format(responseString, request.UserAgent));
        }

        public bool IsReusable => true;
    }
}