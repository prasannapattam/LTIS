using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Act.Framework;
using System.Configuration;

namespace LTIS.Lib.Act
{
    public class ACTConnection : IDisposable
    {
        private ActFramework fm; 
        public ACTConnection()
        {
            string actPad = ConfigurationManager.AppSettings["ActPad"];
            string actUserName = ConfigurationManager.AppSettings["ActUserName"];
            string actPassword = ConfigurationManager.AppSettings["ActPassword"];
            ActFramework ACTFM = new ActFramework();
            ACTFM.LogOn(actPad, actUserName, actPassword);
            
        }

        public ActFramework Framework
        {
            get { return fm; }
        }

        public void Dispose()
        {
            fm.Dispose();
        }
    }
}