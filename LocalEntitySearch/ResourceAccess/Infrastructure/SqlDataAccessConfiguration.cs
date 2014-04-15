using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LocalEntitySearch.ResourceAccess.Infrastructure
{
    public static class SqlDataAccessConfiguration
    {
        private class MessageTemplate
        {
            public const string ConnectionStringNameMessage = "can`t find connection string for name:{0}";
        }

        private class ConnectionStringName
        {
            
            public const string ServiceTripPlanConnectionString = "ServiceTripPlanDBConnectionString";

            public const string ServiceMicroBlogConnectionString = "ServiceMicroBlogDBConnectionString";

            public const string ServiceConnectionString = "ServiceConnectionString";
        }


        /// <summary>
        /// The default timeout limit in transaction.
        /// </summary>
        public const int TransactionTimeOutInMinutes = 10;

        /// <summary>
        /// The default timeout limit in sql command.
        /// </summary>
        public const int CommandTimeoutInSecond = 10 * 60;

        /// <summary>
        /// The maximum length of string.
        /// </summary>
        public const int MAX_CONTEXT_LENGTH = 320;

        /// <summary>
        /// The maximum entity return count in service.
        /// </summary>
        public const int MaxReturnCount = 1024;


        

        public static string ServicesTripPlanConnectionString
        {
            get
            {
                return FindConnectionStringByName(ConnectionStringName.ServiceTripPlanConnectionString);
            }
        }

        public static string ServicesMicroBlogConnectionString
        {
            get
            {
                return FindConnectionStringByName(ConnectionStringName.ServiceMicroBlogConnectionString);
            }
        }

        public static string ServiceConnectionString
        {
            get
            {
                return FindConnectionStringByName(ConnectionStringName.ServiceConnectionString);
            }
        }


        private static string FindConnectionStringByName(string name)
        {
            var conString = ConfigurationManager.ConnectionStrings[name];

            if (conString == null)
            {
                var ex = new ArgumentException(string.Format(MessageTemplate.ConnectionStringNameMessage, name));
              //  Infrastructure.Instrumentation.Logger.LogException(ex);
                throw ex;
            }
            else
            {
                return conString.ConnectionString;
            }
        }
    }
}