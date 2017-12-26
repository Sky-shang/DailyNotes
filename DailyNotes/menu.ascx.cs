using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Add(key: "cache1",
                value: txtKey.Text,
                dependencies: null,
                absoluteExpiration: Cache.NoAbsoluteExpiration,
                slidingExpiration: TimeSpan.FromMinutes(30), 
                priority: CacheItemPriority.Normal,
                onRemoveCallback:null);
            this.labDate.Text = "今天是：" + System.DateTime.Now.ToString("yyyy年MM月dd日");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["tool"] = "新闻－＞站内查询(" + this.ddlStyle.SelectedValue.Trim() + ")----输入关键字为＂" + this.txtKey.Text.Trim() + "＂";
            string strSql = "select * from tb_News where style='" + this.ddlStyle.SelectedValue.ToString() + "'";
            strSql += " and (( content like '%" + this.txtKey.Text + "%')";
            strSql += " or (Title like '%" + this.txtKey.Text + "%'))";
            //strSql += "and issueDate='" + DateTime.Today.ToString() + "'";
            Session["search"] = strSql;
            Response.Redirect("search.aspx");
        }
    }
}