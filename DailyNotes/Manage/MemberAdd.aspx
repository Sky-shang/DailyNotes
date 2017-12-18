<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="DailyNotes.Manage.MemberAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body style="font-size: 9pt; font-family: 宋体;">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="1" cellpadding="1" width="640" align="center" border="0">
                <tr>
                    <td height="25" align="left">&nbsp;&nbsp; 添加管理员
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tableBorder" cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
                            <tr>
                                <td align="left" style="height: 24px">管理员名：
                                </td>
                                <td style="height: 24px">
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px">管理员密码：
                                </td>
                                <td style="height: 25px">
                                    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <br>
                                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click"></asp:Button></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

