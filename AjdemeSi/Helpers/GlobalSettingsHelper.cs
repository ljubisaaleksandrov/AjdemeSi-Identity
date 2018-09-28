using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjdemeSi.Helpers
{
    public static class GlobalSettingsHelper
    {
        public static string SessionUserGroups { get { return System.Configuration.ConfigurationManager.AppSettings["Session.UserGroups"]; }}
    }
}