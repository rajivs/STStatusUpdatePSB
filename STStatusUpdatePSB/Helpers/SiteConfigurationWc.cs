using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace STStatusUpdatePSB.Helpers
{
   public class SiteConfigurationWc
    {
        public static int ProcessCount
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ProcessCount"]);
            }
        }
    }
}
