using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //注册按钮
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");//跳转到用户注册页面
        }
        //登录按钮
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //实例化公共类对象
            DB db = new DB();
            string userName = this.txtUserName.Text.Trim();
            string passWord = db.GetMD5(this.txtPwd.Text.Trim());//对密码进行加密处理
            string num = this.txtValidateNum.Text.Trim();          
            if (Session["ValidateNum"].ToString().ToLower() == num.ToLower())
            {
                //获取用户信息
                SqlDataReader dr = db.reDr("select * from tb_User where UserName='" + userName + "' and PassWord='" + passWord + "'");
                dr.Read();
                if (dr.HasRows)//通过dr中是否包含行判断用户是否通过身份验证
                {
                    Session["UserID"] = dr.GetValue(0);//将该用户的ID存入Session["UserID"]中
                    Session["Role"] = dr.GetValue(4);//将该用户的权限存入Session["Role"]中
                    Response.Redirect("~/UserManagement.aspx");//跳转到主页
                }
                else
                {
                    Response.Write("<script>alert('登录失败！请返回查找原因');location='Login.aspx'</script>");
                }
                dr.Close();
            }
            else
            {
                Response.Write("<script>alert('验证码输入错误！');location='Login.aspx'</script>");
            }
        }
    }
}