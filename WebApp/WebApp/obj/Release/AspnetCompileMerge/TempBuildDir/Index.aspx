<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script>
        function newurl() {
            window.location.href='/ldashkladshkldsadasdsadasdas?u=RedirectUrl&p=value1|100-value2|test';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input  type="button" value="重写url(URL伪静态)" onclick="newurl()"/>
    </div>
    </form>
</body>
</html>
