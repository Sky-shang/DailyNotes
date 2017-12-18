using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace DailyNotes
{
    public partial class XMLDynamicTreeView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            /**
             * TreeNodeBinding类在TreeView控件中定义数据项与该数据项绑定到的节点之间的关系。
             * 该类的DataMember属性指定在节点显示的数据源对应XML的节点。
             * ValueField属性对应TreeNode对象的Value属性。
             * TreeNodeBinding类的Text属性指定向用户显示的文本， 如果该属性没有指定，则默认与ValueField属性相同。
             * */

            this.treeT.ShowLines = false;//显示连接子节点和父节点之间的线条
            TreeNodeBinding Area = new TreeNodeBinding();
            Area.DataMember = "Area";//指定绑定的成员
            Area.ValueField = "cAreaName";//取值的字段
            this.treeT.DataBindings.Add(Area);
            TreeNodeBinding Province = new TreeNodeBinding();
            Province.DataMember = "Province";//添加与"省份"绑定
            Province.ValueField = "cAreaName";
            this.treeT.DataBindings.Add(Province);
            TreeNodeBinding City = new TreeNodeBinding();
            City.DataMember = "City";//添加与"城市"绑定
            City.ValueField = "cAreaName";
            this.treeT.DataBindings.Add(City);
        }
    }
}