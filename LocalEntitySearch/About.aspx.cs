using LobalSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalEntitySearch
{
    public partial class About : Page
    {
        public static Information Info;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["op"] == "get")
            {
                var response = Json.Serialize<Information>(Info);
                Page.Response.Write(response);
            }
            else
            {
                var input = Request.QueryString["info"];
                if(string.IsNullOrWhiteSpace(input))
                {
                    Page.Response.Write("Hello!");
                    return;
                }
                var response = HttpUtility.UrlDecode(input);
                Info = Json.Deserialize<Information>(response);
            }

        }
    }
}