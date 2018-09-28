using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjdemeSi.Helpers
{
    public static class SessionHelper
    {
        public static void AddToSession(string key, object obj)
        {
            HttpContext.Current.Session.Add(key, obj);
        }

        public static object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static void RemoveFromSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}