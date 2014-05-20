using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalEntitySearch
{
    public partial class Local : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["lid"];
            if(string.IsNullOrWhiteSpace(id))
            {
                id = "YN873x2752735941963594177";
            }
            var url = string.Format("http://www.bing.com/local?lid={0}", id);

            myIframe.Src = url;

            QRcode.ImageUrl = string.Format("http://www.esponce.com/api/v3/generate?content={0}&format=png", HttpUtility.UrlEncode(url));
        }
    }
}