<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="DailyNotes.Manage.Top" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Top</title>
    <style type="text/css">
        a:link {
            color: #000000;
            text-decoration: none
        }

        a:hover {
            color: #666666;
        }

        a:visited {
            color: #000000;
            text-decoration: none
        }

        td {
            font-size: 9pt;
            filter: dropshadow(color=#FFFFFF,offx=1,offy=1);
            color: #000000;
        }

        img {
            filter: alpha(opacity:100);
            chroma(color=#FFFFFF);
        }
    </style>
    <base target="right" />
    <script type="text/javascript" language="javascript">
    var dvnews_majorVer = 3.2;
    var dvnews_minorVer = 1216;
    var dvnews_beta = false;

    function preloadImg(src) {
        var img = new Image();
        img.src = src;
    }
    preloadImg("../Image/admin_top_open.gif");


    var displayBar = true;
    function switchBar(obj) {
        if (displayBar) {
            parent.frame.cols = "0,*";
            displayBar = false;
            obj.src = "../Image/admin_top_open.gif";
            obj.title = "打开左边管理菜单";
        } else {
            parent.frame.cols = "180,*";
            displayBar = true;
            obj.src = "../Image/admin_top_close.gif";
            obj.title = "关闭左边管理菜单";
        }
    }
    </script>
</head>
<body background="../Image/admin_top_bg.gif" style="margin: 0px">
    <table height="100%" width="95%" border="0" cellpadding="0" cellspacing="0">
        <tr valign="middle">
            <td width="50">
                <img onclick="switchBar(this)" src="../Image/admin_top_close.gif" alt="关闭左边管理菜单" style="cursor: hand" />
            </td>
        </tr>
    </table>
</body>
</html>

