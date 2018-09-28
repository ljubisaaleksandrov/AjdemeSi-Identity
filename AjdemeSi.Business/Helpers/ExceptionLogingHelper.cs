using log4net;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace AjdemeSi.Business.Helpers
{
    public static class ExceptionLogingHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ExceptionLogingHelper));
        public static void LogException(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            {
                DbEntityEntry entry = item.Entry;
                string entityTypeName = entry.Entity.GetType().Name;

                foreach (DbValidationError subItem in item.ValidationErrors)
                {
                    string message = string.Format("Error '{0}' occurred in {1} at {2}", subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                    _logger.Error(message);
                }
            }
        }

        public static void LogException(Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }
    }
}
