using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class WebFormsRoute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string term = RouteData.Values["term"].ToString();

            Label1.Text = $@"Search Results for:{Server.HtmlEncode(term)}";
            Label3.Text = Page.GetRouteUrl("product-search",new { term="chai"});
        }
    }
}