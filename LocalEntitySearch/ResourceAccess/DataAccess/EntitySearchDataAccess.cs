using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;
using LocalEntitySearch.ResourceAccess.Infrastructure;
using LocalEntitySearch.ResourceAccess.Model;

namespace LocalEntitySearch.ResourceAccess
{
    public class EntitySearchDataAccess
    {
        public SearchResult<T> GetEntityByName<T>(string name, string market, int pageIndex, int itemPerPage, Func<T> ctor) where T : Entity
        {
            using (var connection = new SqlConnection(SqlDataAccessConfiguration.ServiceConnectionString))
            {
                var searchResult = new SearchResult<T>();
                searchResult.SearchResultList = new List<T>();
                searchResult.DurationInSecond = SqlHelper.GetDurationInSecond(() =>
                {

                    connection.Open();
                    // create the database command which will be used by the delegate
                    using (SqlCommand cmd = SqlHelper.CreateSPCommand(connection, "dbo.Service_SearchEntitiesByName"))
                    {
                        int index = SqlHelper.GetItemIndex(pageIndex, itemPerPage);
                        //AddParameter
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "name", "%" + name + "%");
                        SqlHelper.AddParameter(cmd, SqlDbType.VarChar, "market", market);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "index", index);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "count", itemPerPage);
                        var overallCnt = SqlHelper.AddParameter(cmd, SqlDbType.Int, "overallCnt", ParameterDirection.Output);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                searchResult.SearchResultList = new List<T>();
                                while (reader.Read())
                                {
                                    var entity = ctor();
                                    entity.Identifiers = Convert.ToString(reader[0], CultureInfo.InvariantCulture);
                                    entity.MasterId = Convert.ToString(reader[1], CultureInfo.InvariantCulture);
                                    entity.ExternalId = Convert.ToString(reader[2], CultureInfo.InvariantCulture);
                                    entity.OdpTitle = Convert.ToString(reader[3], CultureInfo.InvariantCulture);
                                    entity.OdpDescription = Convert.ToString(reader[4], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti8 = Convert.ToString(reader[5], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti9 = Convert.ToString(reader[6], CultureInfo.InvariantCulture);
                                    entity.RowNumber = Convert.ToInt32(reader[7], CultureInfo.InvariantCulture).ToString();
                                    searchResult.SearchResultList.Add(entity);
                                }

                            }
                        }
                        searchResult.SearchResultCnt = Convert.ToUInt32(overallCnt.Value);
                    }
                });

                return searchResult;
            }
            
        }

        public SearchResult<T> GetEntityByNameAddress<T>(string name, string address, string market, int pageIndex, int itemPerPage, Func<T> ctor) where T : Entity
        {
            using (var connection = new SqlConnection(SqlDataAccessConfiguration.ServiceConnectionString))
            {
                var searchResult = new SearchResult<T>();
                searchResult.SearchResultList = new List<T>();
                searchResult.DurationInSecond = SqlHelper.GetDurationInSecond(() =>
                {

                    connection.Open();
                    // create the database command which will be used by the delegate
                    using (SqlCommand cmd = SqlHelper.CreateSPCommand(connection, "dbo.Service_SearchEntitiesByNameAddress"))
                    {
                        int index = SqlHelper.GetItemIndex(pageIndex, itemPerPage);
                        //AddParameter
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "name", "%" + name + "%");
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "address", "%" + address + "%");
                        SqlHelper.AddParameter(cmd, SqlDbType.VarChar, "market", market);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "index", index);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "count", itemPerPage);
                        var overallCnt = SqlHelper.AddParameter(cmd, SqlDbType.Int, "overallCnt", ParameterDirection.Output);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                searchResult.SearchResultList = new List<T>();
                                while (reader.Read())
                                {
                                    var entity = ctor();
                                    entity.Identifiers = Convert.ToString(reader[0], CultureInfo.InvariantCulture);
                                    entity.MasterId = Convert.ToString(reader[1], CultureInfo.InvariantCulture);
                                    entity.ExternalId = Convert.ToString(reader[2], CultureInfo.InvariantCulture);
                                    entity.OdpTitle = Convert.ToString(reader[3], CultureInfo.InvariantCulture);
                                    entity.OdpDescription = Convert.ToString(reader[4], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti8 = Convert.ToString(reader[5], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti9 = Convert.ToString(reader[6], CultureInfo.InvariantCulture);
                                    entity.RowNumber = Convert.ToInt32(reader[7], CultureInfo.InvariantCulture).ToString();
                                    searchResult.SearchResultList.Add(entity);
                                }

                            }
                        }
                        searchResult.SearchResultCnt = Convert.ToUInt32(overallCnt.Value);
                    }
                });

                return searchResult;
            }

        }

        public SearchResult<T> GetEntityByNameCategory<T>(string name, string category, string market, int pageIndex, int itemPerPage, Func<T> ctor) where T : Entity
        {
            using (var connection = new SqlConnection(SqlDataAccessConfiguration.ServiceConnectionString))
            {
                var searchResult = new SearchResult<T>();
                searchResult.SearchResultList = new List<T>();
                searchResult.DurationInSecond = SqlHelper.GetDurationInSecond(() =>
                {

                    connection.Open();
                    // create the database command which will be used by the delegate
                    using (SqlCommand cmd = SqlHelper.CreateSPCommand(connection, "dbo.Service_SearchEntitiesByNameCategory"))
                    {
                        int index = SqlHelper.GetItemIndex(pageIndex, itemPerPage);
                        //AddParameter
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "name", "%" + name + "%");
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "category", "%" + category + "%");
                        SqlHelper.AddParameter(cmd, SqlDbType.VarChar, "market", market);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "index", index);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "count", itemPerPage);
                        var overallCnt = SqlHelper.AddParameter(cmd, SqlDbType.Int, "overallCnt", ParameterDirection.Output);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                searchResult.SearchResultList = new List<T>();
                                while (reader.Read())
                                {
                                    var entity = ctor();
                                    entity.Identifiers = Convert.ToString(reader[0], CultureInfo.InvariantCulture);
                                    entity.MasterId = Convert.ToString(reader[1], CultureInfo.InvariantCulture);
                                    entity.ExternalId = Convert.ToString(reader[2], CultureInfo.InvariantCulture);
                                    entity.OdpTitle = Convert.ToString(reader[3], CultureInfo.InvariantCulture);
                                    entity.OdpDescription = Convert.ToString(reader[4], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti8 = Convert.ToString(reader[5], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti9 = Convert.ToString(reader[6], CultureInfo.InvariantCulture);
                                    entity.RowNumber = Convert.ToInt32(reader[7], CultureInfo.InvariantCulture).ToString();
                                    searchResult.SearchResultList.Add(entity);
                                }

                            }
                        }
                        searchResult.SearchResultCnt = Convert.ToUInt32(overallCnt.Value);
                    }
                });

                return searchResult;
            }

        }

        public SearchResult<T> GetEntity<T>(string name, string address, string category, string market, int pageIndex, int itemPerPage, Func<T> ctor) where T : Entity
        {
            using (var connection = new SqlConnection(SqlDataAccessConfiguration.ServiceConnectionString))
            {
                var searchResult = new SearchResult<T>();
                searchResult.SearchResultList = new List<T>();
                searchResult.DurationInSecond = SqlHelper.GetDurationInSecond(() =>
                {

                    connection.Open();
                    // create the database command which will be used by the delegate
                    using (SqlCommand cmd = SqlHelper.CreateSPCommand(connection, "dbo.Service_SearchEntities"))
                    {
                        int index = SqlHelper.GetItemIndex(pageIndex, itemPerPage);
                        //AddParameter
                        if (name != string.Empty)
                        {
                            name = "%" + name + "%";
                        }
                        if (address != string.Empty)
                        {
                            address = "%" + address + "%";
                        }
                        if (category != string.Empty)
                        {
                            category = "%" + category + "%";
                        }
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "name", name);
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "address", address);
                        SqlHelper.AddParameter(cmd, SqlDbType.NVarChar, "category", category);
                        SqlHelper.AddParameter(cmd, SqlDbType.VarChar, "market", market);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "index", index);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "count", itemPerPage);
                        var overallCnt = SqlHelper.AddParameter(cmd, SqlDbType.Int, "overallCnt", ParameterDirection.Output);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                searchResult.SearchResultList = new List<T>();
                                while (reader.Read())
                                {
                                    var entity = ctor();
                                    entity.Identifiers = Convert.ToString(reader[0], CultureInfo.InvariantCulture);
                                    entity.MasterId = Convert.ToString(reader[1], CultureInfo.InvariantCulture);
                                    entity.ExternalId = Convert.ToString(reader[2], CultureInfo.InvariantCulture);
                                    entity.OdpTitle = Convert.ToString(reader[3], CultureInfo.InvariantCulture);
                                    entity.OdpDescription = Convert.ToString(reader[4], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti8 = Convert.ToString(reader[5], CultureInfo.InvariantCulture);
                                    entity.FeedsMulti9 = Convert.ToString(reader[6], CultureInfo.InvariantCulture);
                                    entity.RowNumber = Convert.ToInt32(reader[7], CultureInfo.InvariantCulture).ToString();
                                    searchResult.SearchResultList.Add(entity);
                                }

                            }
                        }
                        searchResult.SearchResultCnt = Convert.ToUInt32(overallCnt.Value);
                    }
                });

                return searchResult;
            }

        }

        public SearchResult<T> GetEntityDiff<T>(int diffStatus, int random, string market, int pageIndex, int itemPerPage, Func<T> ctor) where T : DataTable
        {
            

            using (var connection = new SqlConnection(SqlDataAccessConfiguration.ServiceConnectionString))
            {
                var searchResult = new SearchResult<T>();
                searchResult.SearchResultList = new List<T>();
                searchResult.DurationInSecond = SqlHelper.GetDurationInSecond(() =>
                {

                    connection.Open();



                    // create the database command which will be used by the delegate
                    using (SqlCommand cmd = SqlHelper.CreateSPCommand(connection, "dbo.Service_SearchEntityDiffByStatusRandom"))
                    {
                        int index = SqlHelper.GetItemIndex(pageIndex, itemPerPage);
                        //AddParameter
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "diffStatus", diffStatus);
                        SqlHelper.AddParameter(cmd, SqlDbType.VarChar, "market", market);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "index", index);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "count", itemPerPage);
                        SqlHelper.AddParameter(cmd, SqlDbType.Int, "random", random);
                        var overallCnt = SqlHelper.AddParameter(cmd, SqlDbType.Int, "overallCnt", ParameterDirection.Output);
                        //using (var reader = cmd.ExecuteReader())
                        //{
                        //    if (reader.HasRows)
                        //    {

                        //        searchResult.SearchResultList = new List<T>();
                        //        while (reader.Read())
                        //        {
                        //            var entity = ctor();
                        //            entity.Identifiers = Convert.ToString(reader[0], CultureInfo.InvariantCulture);
                        //            entity.MasterId = Convert.ToString(reader[1], CultureInfo.InvariantCulture);
                        //            entity.ExternalId = Convert.ToString(reader[2], CultureInfo.InvariantCulture);
                        //            entity.OdpTitle = Convert.ToString(reader[3], CultureInfo.InvariantCulture);
                        //            entity.OdpDescription = Convert.ToString(reader[4], CultureInfo.InvariantCulture);
                        //            entity.FeedsMulti8 = Convert.ToString(reader[5], CultureInfo.InvariantCulture);
                        //            entity.FeedsMulti9 = Convert.ToString(reader[6], CultureInfo.InvariantCulture);
                        //            entity.RowNumber = Convert.ToInt32(reader[7], CultureInfo.InvariantCulture).ToString();
                        //            searchResult.SearchResultList.Add(entity);
                        //        }

                        //    }
                        //}

                        using (SqlDataAdapter adpter = new SqlDataAdapter(cmd))
                        {
                            var result = ctor();
                            adpter.Fill(result);
                            searchResult.SearchResultList = new List<T>() {result};
                        }
                        searchResult.SearchResultCnt = Convert.ToUInt32(overallCnt.Value);
                    }
                });

                return searchResult;
            }

        }
    }
}