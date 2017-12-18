<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlDynamicTreeView.aspx.cs" Inherits="DailyNotes.SqlDynamicTreeView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sql动态TreeView</title>
    <script type="text/javascript">
        function show(msg) {
            alert(msg);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="treeT" runat="server">
            </asp:TreeView>
        </div>
    </form>
</body>
</html>