<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestLogger.aspx.cs" Inherits="DailyNotes.TestLogger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script>
        function jsonP(data) {
            console.log(data);
        }

        $.ajax({
            url: "http://app.weichaish.com:803/yfzx/api/TeamWork/HR/EmployeeInfo",
            type: "GET",
            dataType: "jsonp",  
            jsonp: "data",   
            jsonpCallback: "jsonP", 
            success: function (data) {
                console.info("调用success");

            }
        });
    </script>
</body>
</html>
