using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocalEntitySearch.ResourceAccess;
using LocalEntitySearch.ResourceAccess.Model;

namespace LocalEntitySearch
{
    public partial class Test : System.Web.UI.Page
    {
        private EntitySearchDataAccess dataAccess = new EntitySearchDataAccess();

        private static readonly Regex pageIndexRegex = new Regex("query=(?<query>.+?)?&address=(?<address>.+?)?&category=(?<category>.+?)?&market=(?<market>.+?)&pageIndex=(?<pageIndex>.+?)&itemPerPage=(?<itemPerPage>.+)", RegexOptions.Compiled);

        

        protected void Page_Load(object sender, EventArgs e)
        {
            EntityName.Focus();
            SearchResultTable.Width = 1024;

            if (IsPostBack)
            {
                return;
            }
            //Response.Write("Url: " + Request.Url.Query + "\r\n");
            if(!string.IsNullOrWhiteSpace(Request.Url.Query))
            {
                SearchEntity();
            }
        }

        private String Replace(Match m)
        {
            return "<span style='background-color:yellow;color:black;'>" + m.Value + "</span>";
        }
        private void SearchEntity()
        {
            

            

            int pageIndex = 1;
            int itemPerPage = 10;
            string query = null;
            string address = null;
            string market = null;
            string category = null;
            GetPageIndex(out query, out address, out category, out market, out pageIndex, out itemPerPage);

            //Response.Write(query + " " + pageIndex + " " + itemPerPage);


            SearchResult<Entity> result = null;

            if (string.IsNullOrWhiteSpace(query))
            {
                query = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                address = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(category))
            {
                category = string.Empty;
            }

            if (!(query == string.Empty && address == string.Empty && category == string.Empty))
            {
                result = dataAccess.GetEntity(query, address, category, market, pageIndex, itemPerPage, () => new Entity());
            }

            if (result.SearchResultList != null && result.SearchResultList.Count > 0)
            {
                Init();

                //hightlight
                Regex queryRegex = new Regex(query, RegexOptions.IgnoreCase);
                Regex addressRegex = new Regex(address, RegexOptions.IgnoreCase);
                Regex categoryRegex = new Regex(category, RegexOptions.IgnoreCase);

                int idx = 0;
                foreach (var item in result.SearchResultList)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell();

                    HyperLink link = new HyperLink();
                    link.NavigateUrl = string.Format("http://ldp-prod.binginternal.com/LocalProbe/Search/Details?q={1}&id={2}&market={0}&env=gdp-prod&run=latest", market, item.MasterId, item.MasterId.Substring(2));
                    link.Text = item.MasterId;
                    link.Target = "_blank";
                    cell.Controls.Add(link);


                    var odpTitle = item.OdpTitle;
                    var odpDescription = item.OdpDescription;
                    var feedsMulti8 = item.FeedsMulti8;
                    var feedsMulti9 = item.FeedsMulti9;

                    if (query != string.Empty)
                    {
                        odpTitle = queryRegex.Replace(odpTitle, new MatchEvaluator(Replace));
                        
                    }

                    if (address != string.Empty)
                    {
                        feedsMulti8 = addressRegex.Replace(feedsMulti8, new MatchEvaluator(Replace));
                        
                    }

                    if (category != string.Empty)
                    {
                        odpDescription = categoryRegex.Replace(odpDescription, new MatchEvaluator(Replace));
                        

                        feedsMulti9 = categoryRegex.Replace(feedsMulti9, new MatchEvaluator(Replace));
                        
                    }

                    odpTitle = odpTitle.Replace("|", " <span style='color:blue;'>|</span> ");
                    feedsMulti8 = feedsMulti8.Replace("|", " <span style='color:blue;'>|</span> ");
                    odpDescription = odpDescription.Replace("|", " <span style='color:blue;'>|</span> ");
                    feedsMulti9 = feedsMulti9.Replace("|", " <span style='color:blue;'>|</span> ");

                    row.Cells.AddRange(new TableCell[]
                                   {
                                       new TableCell(){Text = item.RowNumber, HorizontalAlign = HorizontalAlign.Center},
                                       cell,
                                       //new TableCell(){Text = item.Identifiers},
                                       new TableCell(){Text = odpTitle},
                                       new TableCell(){Text = odpDescription},
                                       new TableCell(){Text = feedsMulti8},
                                       new TableCell(){Text = feedsMulti9}
                                       
                                   });
                    if ((idx & 1) == 1)
                    {
                        row.BackColor = Color.Azure;
                    }
                    SearchResultTable.Rows.Add(row);
                    idx++;
                }

                SearchResultCnt.Text = string.Format("{0} - {1} of {2} results ({3} seconds),  ",
                    result.SearchResultList[0].RowNumber,
                    result.SearchResultList[result.SearchResultList.Count - 1].RowNumber, result.SearchResultCnt,
                    result.DurationInSecond);

                int previous = pageIndex - 1;
                if (previous < 1)
                {
                    previous = 1;
                }

                int begin = 1;

                int next = pageIndex + 1;
                int total = (int) result.SearchResultCnt/itemPerPage;
                total += (int) result.SearchResultCnt%itemPerPage > 0 ? 1 : 0;
                if (total < 1)
                {
                    total = 1;
                }
                if (next > total)
                {
                    next = total;
                }

                int end = total;

                Previous.NavigateUrl = string.Format("/Test.aspx?query={0}&address={1}&category={2}&market={3}&pageIndex={4}&itemPerPage=10",
                    query, address, category,  market, previous);
                Next.NavigateUrl = string.Format("/Test.aspx?query={0}&address={1}&category={2}&market={3}&pageIndex={4}&itemPerPage=10", query, address, category,
                    market, next);
                Begin.NavigateUrl = string.Format("/Test.aspx?query={0}&address={1}&category={2}&market={3}&pageIndex={4}&itemPerPage=10", query, address, category,
                    market, begin);
                End.NavigateUrl = string.Format("/Test.aspx?query={0}&address={1}&category={2}&market={3}&pageIndex={4}&itemPerPage=10", query, address, category,
                    market, end);
                Previous.Visible = true;
                Next.Visible = true;
                Begin.Visible = true;
                End.Visible = true;
            }
            else
            {
                SearchResultCnt.Text = "No result!";
            }
            EntityName.Text = query;
            Market.SelectedValue = market;
            EntityAddress.Text = address;
            EntityCategory.Text = category;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            var query = EntityName.Text.Trim();
            query = HttpUtility.UrlEncode(query);

            var address = EntityAddress.Text.Trim();
            var category = EntityCategory.Text.Trim();
            address = HttpUtility.UrlEncode(address);
            category = HttpUtility.UrlEncode(category);
            var market = Market.SelectedValue;
            Response.Redirect(string.Format("/Test.aspx?query={0}&address={1}&category={2}&market={3}&pageIndex=1&itemPerPage=10", query, address, category, market));

        }

        private void GetPageIndex(out string q, out string address, out string category, out string market, out int index, out int itemPerPage)
        {
            string query = Request.Url.Query;
            var result = pageIndexRegex.Match(query);

            var queryRaw = result.Groups["query"];
            var addressRaw = result.Groups["address"];
            var categoryRaw = result.Groups["category"];
            var marketRaw = result.Groups["market"];
            var pageIndexRaw = result.Groups["pageIndex"];
            var itemPerPageRaw = result.Groups["itemPerPage"];

            index = -1;
            itemPerPage = -1;
            q = string.Empty;
            market = string.Empty;
            address = string.Empty;
            category = string.Empty;
            
            if (pageIndexRaw != null)
            {
                int.TryParse(pageIndexRaw.Value, out index);
            }
            
            if (itemPerPageRaw != null)
            {
                int.TryParse(itemPerPageRaw.Value, out itemPerPage);
                if (itemPerPage > 50)
                {
                    itemPerPage = 50;
                }
                if (itemPerPage < 1)
                {
                    itemPerPage = 1;
                }
            }
            if (queryRaw != null)
            {
                q = HttpUtility.UrlDecode(queryRaw.Value);
            }

            if (addressRaw != null)
            {
                address = HttpUtility.UrlDecode(addressRaw.Value);
            }

            if (categoryRaw != null)
            {
                category = HttpUtility.UrlDecode(categoryRaw.Value);
            }
            if (marketRaw != null)
            {
                market = marketRaw.Value;
            }
            if (index > 0 && itemPerPage > 0 && !string.IsNullOrWhiteSpace(market))
            {
                return;
            }

            index = 1;
            itemPerPage = 10;
            q = "7-11";
            address = "香港";
            market = "zh-HK";
            category = string.Empty;
        }

        public void Init()
        {
            SearchResultTable.Visible = true;
            SearchResultTable.Rows.Clear();

            TableRow row = new TableRow();
            row.Cells.AddRange(
                new TableCell[]
                                   {
                                       new TableCell(){Text = "item#", HorizontalAlign = HorizontalAlign.Center},
                                       new TableCell(){Text = "Master ID"},
                                       //new TableCell(){Text = "Identifiers"},
                                       new TableCell(){Text = "Name(OdpTitle)"},
                                       new TableCell(){Text = "Category Synonyms(OdpDescription)"},
                                       new TableCell(){Text = "Address(FeedsMulti8)"},
                                       new TableCell(){Text = "Raw Category(FeedsMulti9)"},
                                       
                                       
                                   });
            row.BackColor = Color.Chartreuse;
            SearchResultTable.Rows.Add(row
                );

            
            
        }
    }
}