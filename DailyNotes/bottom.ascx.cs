﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class bottom : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.open('Manage/Login.aspx','','width=455,height=255')</script>");
        }
    }
}