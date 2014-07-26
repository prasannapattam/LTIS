using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Lib.HttpModules
{
    public class MonitorModule: IHttpModule
    {
        public void Init(HttpApplication httpApp)
        {
            httpApp.BeginRequest += OnBeginRequest;
            httpApp.Init();
        }

        public void OnBeginRequest(Object sender, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)sender;
            if (httpApp.Request.UrlReferrer == null || httpApp.Request.Url.Host != httpApp.Request.UrlReferrer.Host)
            {
                httpApp.Response.Redirect("/APFW/Home/HomeShell.aspx");
            }            
        }
        
        public void Dispose() 
        { 

        }
    }
}