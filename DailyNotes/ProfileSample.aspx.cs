using System;
using System.Web.Profile;
using System.Web.Script.Serialization;
using DailyNotes.Models;

namespace DailyNotes
{
    public partial class ProfileSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic p = this.Context.Profile;
            p.Color = "Red";
            p.UserInfo.Name = "Chrinstian";
            var cart = new ShoppingCart();
            cart.Items.Add(new Item { Description = "Sample1", Cost = 20.30M });
            cart.Items.Add(new Item { Description = "Sample2", Cost = 14.30M });
            p.ShoppingCart = cart;
            p.Save();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dynamic profile = Context.Profile;
            var strProfile = $@"Color:{profile.Color}
                                  <br />
                                  Name:{profile.UserInfo.Name}
                                  <br />
                                  {profile.ShoppingCart.Items[0].Description}:{profile.ShoppingCart.Items[0].Cost}
                                  <br />
                                  {profile.ShoppingCart.Items[1].Description}:{profile.ShoppingCart.Items[1].Cost}
                                  <br />
                                  TotalCost:{profile.ShoppingCart.TotalCost}
                                  <br />";
            Label1.Text = strProfile;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var inactiveProfiles = ProfileManager.GetAllInactiveProfiles(ProfileAuthenticationOption.Anonymous,DateTime.Now.AddYears(-1));
            Label1.Text = inactiveProfiles.Count.ToString();
            //删除从去年开始就不活动的匿名Profile
            //ProfileManager.DeleteInactiveProfiles(ProfileAuthenticationOption.Anonymous, DateTime.Now.AddYears(-1));
            //Label1.Text = new JavaScriptSerializer().Serialize(inactiveProfiles);

        }
    }
}