using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace LocalEntitySearch.ResourceAccess.Infrastructure
{
    public static class SqlHelper
    {

        /// <summary>
        /// Creates sql command for stored procedure.
        /// </summary>
        /// <param name="connection">The db connection</param>
        /// <param name="commandText">The command text</param>
        /// <returns>The sql command</returns>
        public static SqlCommand CreateSPCommand(SqlConnection connection, string commandText)
        {
            SqlCommand command = (SqlCommand)connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = commandText;
            command.CommandTimeout = SqlDataAccessConfiguration.CommandTimeoutInSecond;
            return command;
        }

        /// <summary>
        /// Add parameters to sql command.
        /// </summary>
        /// <param name="command">The sql command</param>
        /// <param name="dbType">The type of parameter</param>
        /// <param name="name">The name of parameter</param>
        /// <param name="value">The value of parameter</param>
        public static void AddParameter(SqlCommand command, SqlDbType dbType, string name, object value)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.SqlDbType = dbType;
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = (value is DateTime && ((DateTime)value == DateTime.MinValue)) ? DBNull.Value : value;
            }
            parameter.ParameterName = name;
            command.Parameters.Add(parameter);
        }

        public static SqlParameter AddParameter(SqlCommand command, SqlDbType dbType, string name, ParameterDirection direction)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.SqlDbType = dbType;
            parameter.ParameterName = name;
            parameter.Direction = direction;
            command.Parameters.Add(parameter);
            return parameter;
        }

        public static double GetDurationInSecond(Action action)
        {
            DateTime startTime;
            startTime = DateTime.Now;
            action();
            return (DateTime.Now - startTime).TotalSeconds;
        }

        public static int GetItemIndex(int pageIndex, int itemPerPage)
        {
            return (pageIndex - 1) * itemPerPage + 1;
        }
        
    }
}
