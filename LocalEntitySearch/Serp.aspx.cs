using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalEntitySearch
{
    public partial class Serp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var url =
                "http://www.bing.com/search?q=purple+cafe+kirkland&qs=n&form=QBLH&pq=purple+cafe+ki&sc=8-14&sp=-1&sk=&ghc=2&cvid=f65a98e1b69848b0b26e8a5188a961d0";

            myIframe.Src = url;

            QRcode.ImageUrl = string.Format("http://www.esponce.com/api/v3/generate?content={0}&format=png", HttpUtility.UrlEncode("http://www.bing.com/local?lid=YN873x119742427"));

            var display = Request.QueryString["d"];
            if (display == "y")
            {
                QRcode.Visible = true;
            }
            else
            {
                QRcode.Visible = false;
            }
        }
    }
}