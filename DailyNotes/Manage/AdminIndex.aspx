<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminIndex.aspx.cs" Inherits="DailyNotes.Manage.AdminIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台功能</title>
</head>
<frameset id="frame" framespacing="0" border="false" cols="180,*" frameborder="0">
	<frame name="left"  scrolling="auto" marginwidth="0" marginheight="0" src="Left.aspx" noresize  >
	<frameset framespacing="0" border="false" rows="35,*" frameborder="0" >
		<frame name="top" scrolling="no" src="Top.aspx">
		<frame name="right" scrolling="auto" src="Main.aspx">
	</frameset>
</frameset>
</html>

