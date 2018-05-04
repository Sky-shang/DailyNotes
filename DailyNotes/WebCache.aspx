<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCache.aspx.cs" Inherits="DailyNotes.WebCache" %>
<%@ OutputCache Duration="10" VaryByParam="none" %>  
<!---添加上这一句代码意思是，添加此页面缓存十秒，这十秒之内，刷新页面将读缓存起来页面的值，不再执行Page_Load方法。
     Page_Load方法是每十秒执行一次。
    <%@ OutputCache Duration="10" VaryByParam="none" %> 这条指令标签为该页面添加缓存，
    Duration这个参数指定页面缓存时间为10秒，VaryByParam这个指定页面参数，
    也就是这样子的，打个比方，例如这样一个页面http://www.cnblogs.com/knowledgesea/admin/EditPosts.aspx?postid=2536603&update=1，
    那么他的参数也就是postid和update，如果这样页面我们可以把指令标签写为<%@ OutputCache Duration="10" VaryByParam="postid;update" %> 参数与参数之间用分号隔开，
    这样子也就吧每个单独的页面缓存起来了，他缓存的就是postid=2536603&update=1或者postid=1&update=2等等不一样的参数页面全部缓存起来。
    这里可以使用一个简便的方法，就是<%@ OutputCache Duration="10" VaryByParam="*" %>，缓存起来所有当前的页面下参数不一样的页面。
    ASP.NET不会再执行页面的生命周期和相关代码而是直接使用缓存的页面，简单点理解也就是我注释中介绍的。  
    -->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
