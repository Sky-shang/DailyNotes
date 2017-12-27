<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLDynamicTreeView.aspx.cs" Inherits="DailyNotes.TreeView.XMLDynamicTreeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XML动态TreeView</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--TreeView控件的DataSourceID属性指定数据源控件源ID。--%>
            <asp:TreeView ID="treeT" runat="server" DataSourceID="XmlDataSource1">
            </asp:TreeView>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="Area.xml"></asp:XmlDataSource>
        </div>
    </form>
</body>
</html>
