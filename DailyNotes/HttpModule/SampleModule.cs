using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DailyNotes.HttpModule
{
    public class SampleModule : IHttpModule
    {
        private const string AllowedAddressesFile = "/HttpModule/AllowedAddresses.txt";
        private List<string> allowedAddresses;
        public void Init(HttpApplication context)
        {
            context.LogRequest += new EventHandler(OnLogRequest);
            context.BeginRequest += BeginRequest;
            context.PreRequestHandlerExecute += PreRequestHandlerExecute;
        }
        private void BeginRequest(object sender, EventArgs e)
        {
            LoadAddresses((sender as HttpApplication)?.Context);
        }

        private void LoadAddresses(HttpContext context)
        {
            if (allowedAddresses == null)
            {
                string path = context.Server.MapPath(AllowedAddressesFile);
                allowedAddresses = File.ReadLines(path).ToList();
            }
        }

        private void PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpRequest req = app.Context.Request;
            if (!allowedAddresses.Contains(req.UserHostAddress))
            {
                throw new HttpException(403, "IP address denied");
            }
        }

        private void OnLogRequest(object sender, EventArgs e)
        {
            //custom logging logic can go here
        }
        public void Dispose()
        {

        }
    }
}