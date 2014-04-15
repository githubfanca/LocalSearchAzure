using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocalEntitySearch.ResourceAccess;
using LocalEntitySearch.ResourceAccess.Model;

namespace LocalEntitySearch
{
    public partial class Diff : System.Web.UI.Page
    {
        private EntitySearchDataAccess dataAccess = new EntitySearchDataAccess();
        private DataColumn[] dataColumns = new DataColumn[]
                                      {
                                          new DataColumn("YpID"),
                                          new DataColumn("EntityName"),
                                          new DataColumn("CombinedAddress"),
                                          new DataColumn("LatLong"),
                                          new DataColumn("PhoneNumber"),
                                          new DataColumn("Website"),
                                          new DataColumn("Odptitle"),
                                          new DataColumn("Odpdescription"),
                                          new DataColumn("FeedsMulti8"),
                                          new DataColumn("FeedsMulti9"),
                                          new DataColumn("AUTB"),
                                      };
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchResultTable.Width = 1024;

            if (IsPostBack)
            {
                return;
            }

            ViewEntityDiff();
        }

        protected void Search_Click(object sender, EventArgs e)
        {


            int diffStatus = 0;
            int bit = 2;
            foreach (var item in columnsCheckBoxList.Items.Cast<ListItem>())
            {
                if (item.Selected)
                {
                    diffStatus |= bit;
                }
                bit <<= 1;
            }
            
            var market = Market.SelectedValue;
            int random = cbRandom.Checked ? 1 : 0;
            Response.Redirect(string.Format("/Diff.aspx?diffStatus={0}&market={1}&pageIndex=1&itemPerPage=100&random={2}", diffStatus, market, random));
        }

        private void ViewEntityDiff()
        {
            int pageIndex = 1;
            int itemPerPage = 100;
            int diffStatus = 0;
            string market = null;
            int random = 0;
            
            GetPageIndex(out diffStatus, out market, out pageIndex, out itemPerPage, out random);

            SearchResult<DataTable> result = new SearchResult<DataTable>();

            if (diffStatus > 0)
            {
                result = dataAccess.GetEntityDiff(diffStatus, random, market, pageIndex, itemPerPage, () => new DataTable());
            }

            if (result.SearchResultList != null && result.SearchResultList.Count > 0)
            {
                Init();
                RefineTable(result, diffStatus, market);
                //SearchResultTable.DataSource = newTable;
                //SearchResultTable.DataBind();
                int beginIdx = (pageIndex - 1)*itemPerPage + 1;
                int endIdx = beginIdx + itemPerPage - 1;
                if (endIdx > (int) result.SearchResultCnt)
                {
                    endIdx = (int) result.SearchResultCnt;
                }
                SearchResultCnt.Text = string.Format("{0} - {1} of <span style='Font-Style:Bold;'>{2:N0}</span> results ({3} seconds), <span style='color:blue;'>Blue</span> - latest, <span style='color:red;'>Red</span> - LKG) ",
                    beginIdx,
                    endIdx, 
                    result.SearchResultCnt,
                    result.DurationInSecond);

                #region Next Page
                int previous = pageIndex - 1;
                if (previous < 1)
                {
                    previous = 1;
                }

                int begin = 1;

                int next = pageIndex + 1;
                int total = (int)result.SearchResultCnt / itemPerPage;
                total += (int)result.SearchResultCnt % itemPerPage > 0 ? 1 : 0;
                if (total < 1)
                {
                    total = 1;
                }
                if (next > total)
                {
                    next = total;
                }

                int end = total;

                Previous.NavigateUrl = string.Format("/Diff.aspx?diffStatus={0}&market={1}&pageIndex={2}&itemPerPage={3}&random={4}",
                    diffStatus, market, previous, itemPerPage, random);
                Next.NavigateUrl = string.Format("/Diff.aspx?diffStatus={0}&market={1}&pageIndex={2}&itemPerPage={3}&random={4}", diffStatus, market, next, itemPerPage, random);
                Begin.NavigateUrl = string.Format("/Diff.aspx?diffStatus={0}&market={1}&pageIndex={2}&itemPerPage={3}&random={4}", diffStatus, market, begin, itemPerPage, random);
                End.NavigateUrl = string.Format("/Diff.aspx?diffStatus={0}&market={1}&pageIndex={2}&itemPerPage={3}&random={4}", diffStatus, market, end, itemPerPage, random);
                Previous.Visible = true;
                Next.Visible = true;
                Begin.Visible = true;
                End.Visible = true;
                #endregion

            }
            else
            {
                SearchResultCnt.Text = "Please select some above columns.";
            }
        
            Market.SelectedValue = market;
            
            int bit = 2;
            foreach (var item in columnsCheckBoxList.Items.Cast<ListItem>())
            {
                item.Selected = false;
                if ((diffStatus & bit) > 0)
                {
                    item.Selected = true;
                }
                bit <<= 1;
            }
            cbRandom.Checked = random == 1 ? true : false;
        }

        private void RefineTable(SearchResult<DataTable> result, int diffStatus, string market)
        {
            var dataTable = result.SearchResultList[0];


            SearchResultTable.Visible = true;
            SearchResultTable.Rows.Clear();
            

            //Header
            int bit = 2;
            TableRow tableRow = new TableRow();
            tableRow.Cells.Add(new TableCell() { Text = "item#" });
            tableRow.Cells.Add(new TableCell(){Text = dataColumns[0].ColumnName});
            for (int i = 0; i < dataColumns.Length - 1; i++)
            {
                if ((bit & diffStatus) > 0 || (i == 0 || i == 1 || i == 3))
                {
                    tableRow.Cells.Add(new TableCell(){Text = dataColumns[i + 1].ColumnName});
                }
                bit <<= 1;
                
            }
            tableRow.BackColor = Color.Chartreuse;
            SearchResultTable.Rows.Add(tableRow);

            //Body

            int idx = 0;
            foreach (var row in dataTable.Rows)
            {
                tableRow = new TableRow();

                tableRow.Cells.Add(new TableCell() { Text = ((DataRow)row)[dataColumns.Length * 2 + 1].ToString() });

                TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                string id = ((DataRow) row)["YpID2"].ToString();
                string firstId = id.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries)[0];
                link.NavigateUrl = string.Format("http://ldp-prod.binginternal.com/LocalProbe/Search/Details?q={1}&id={2}&market={0}&env=gdp-prod&run=latest", market, "YN" + firstId, firstId);
                if (id.Length > 50)
                {
                    id = id.Substring(0, 50) + "...";
                }
                link.Text = id;
                link.Target = "_blank";
                cell.Controls.Add(link);


                tableRow.Cells.Add(cell);

                int j = 1;
                bit = 2;
                for (int i = 0; i < dataColumns.Length; i++)
                {
                    if ((bit & diffStatus) > 0)
                    {
                        tableRow.Cells.Add(new TableCell() { Text =  FormatDiff(((DataRow) row)[dataColumns.Length + i + 1].ToString(), ((DataRow) row)[i + 1].ToString())});
                        
                    }
                    else if( i == 0 || i == 1 || i == 3)
                    {
                        tableRow.Cells.Add(new TableCell() { Text = ((DataRow)row)[dataColumns.Length + i + 1].ToString() });
                    }
                    bit <<= 1;
                }
                if ((idx & 1) == 1)
                {
                    tableRow.BackColor = Color.Azure;
                }
                idx++;
                SearchResultTable.Rows.Add(tableRow);
            }


        }

        private string FormatDiff(string p1, string p2)
        {
            if (p1 == p2)
            {
                return p1;
            }
            {
                p1 = string.Format("<span style='color:blue;'>{0}</span>", string.IsNullOrWhiteSpace(p1)?"Empty":p1);
                p2 = string.Format("<span style='color:red;'>{0}</span>", string.IsNullOrWhiteSpace(p2) ? "Empty" : p2);
            }
            return p1 + "<br/>" + p2;
        }

        private void GetPageIndex(out int diffStatus, out string market, out int pageIndex, out int itemPerPage, out int random)
        {
            if (!int.TryParse(Request.QueryString["diffStatus"], out diffStatus))
            {
                diffStatus = 0;
            }

            market = Request.QueryString["market"];
            
            if (!int.TryParse(Request.QueryString["pageIndex"], out pageIndex))
            {
                pageIndex = 0;
            }

            if (!int.TryParse(Request.QueryString["itemPerPage"], out itemPerPage))
            {
                itemPerPage = 0;
            }

            if (!int.TryParse(Request.QueryString["random"], out random))
            {
                random = 0;
            }

            if (itemPerPage > 100)
            {
                itemPerPage = 100;
            }
            if (itemPerPage < 1)
            {
                itemPerPage = 1;
            }

            
            if (pageIndex > 0 && itemPerPage > 0 && !string.IsNullOrWhiteSpace(market))
            {
                return;
            }

            pageIndex = 1;
            itemPerPage = 10;
            market = "zh-HK";
            diffStatus = 0;

        }

        public void Init()
        {
            SearchResultTable.Visible = true;
            
        }
    }
}