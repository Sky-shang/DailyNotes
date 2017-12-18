<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaticTreeView.aspx.cs" Inherits="DailyNotes.StaticTreeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>静态TreeView</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="treeT" runat="server">
            <Nodes>
                <asp:TreeNode  NavigateUrl="#" Text="城市" Expanded="True">
                    <asp:TreeNode NavigateUrl="#" Text="北京市"></asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="上海市"></asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="天津市"></asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="重庆市"></asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="湖北省">
                        <asp:TreeNode NavigateUrl="#" Text="武汉市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="黄冈市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="荆州市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="武穴市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="十堰市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="黄石市"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="河北省">
                        <asp:TreeNode NavigateUrl="#" Text="石家庄市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="唐山市"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode NavigateUrl="#" Text="山西省">
                        <asp:TreeNode NavigateUrl="#" Text="太原市"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="#" Text="大同市"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
