﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="语音识别.aspx.cs" Inherits="DailyNotes.语音识别" %>

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
    <script src="//cdnjs.cloudflare.com/ajax/libs/annyang/2.6.1/annyang.min.js"></script>
    <script>
        if (annyang) {
            // Let's define a command.
            var commands = {
                'hello': function () {
                    alert('Hello world!');
                },
                '你好': function () {
                    alert('Hello world!');
                }
            };

            // Add our commands to annyang
            annyang.addCommands(commands);

            // Start listening.
            annyang.start();
        }
    </script>
</body>
</html>
