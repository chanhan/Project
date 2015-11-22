<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebAppForAppCan.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script>
        $(function () {
            $('input').click(show);
        });
        function show(){
            alert(this.id);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input  type="button" value="点击" id="hello"/>
    <input  type="button" value="点击" id="hello2"/>


    </div>
    </form>
</body>
</html>
