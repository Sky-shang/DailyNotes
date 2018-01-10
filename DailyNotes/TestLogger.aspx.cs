using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DailyNotes.Models;

namespace DailyNotes
{
    public partial class TestLogger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (var i=0;i<10000;i++)
            {
                Logger.Write(DateTime.Now.ToLocalTime()+"\t行号："+(i+1)+ "\tTest Write log!\r\n");
            }
        }
    }
}