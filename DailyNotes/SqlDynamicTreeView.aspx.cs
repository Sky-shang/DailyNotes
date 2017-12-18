using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace DailyNotes
{
    public partial class SqlDynamicTreeView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddTree(0, null);
        }

        private void AddTree(int pid, TreeNode pNode)
        {
            string sqlStr = "SELECT Item_id,Item_name,Parent_ID FROM Para_item";
            DataTable dt = SqlHelper.ExecuteQuery(sqlStr, CommandType.Text);
            if (dt.Rows.Count <= 0) return;
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点 ParentID为父节点ID
            dv.RowFilter = "[Parent_ID] = " + pid;
            //循环递归
            foreach (DataRowView row in dv)
            {
                //声明节点
                TreeNode node = new TreeNode
                {
                    NavigateUrl = $"javascript:show('{row["Item_Name"].ToString()}')"
                };
                //绑定超级链接
                //开始递归
                if (pNode == null)
                {
                    //添加根节点
                    node.Text = row["Item_Name"].ToString();
                    treeT.Nodes.Add(node);
                    node.Expanded = true; //节点状态展开
                    AddTree(int.Parse(row["Item_ID"].ToString()), node);    //再次递归
                }
                else
                {
                    //添加当前节点的子节点
                    node.Text = row["Item_Name"].ToString();
                    pNode.ChildNodes.Add(node);
                    node.Expanded = true; //节点状态展开
                    AddTree(int.Parse(row["Item_ID"].ToString()), node);     //再次递归
                }
            }
        }
    }
}