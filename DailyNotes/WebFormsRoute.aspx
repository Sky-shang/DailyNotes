<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormsRoute.aspx.cs" Inherits="DailyNotes.WebFormsRoute" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="<%$RouteValue:Term %>"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%$RouteURL:Term=Chai %>">HyperLink</asp:HyperLink>
        </div>
    </form>
</body>
</html>
