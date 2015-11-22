<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.MainForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主頁面</title>
</head>
<frameset id="Frameset" cols="*,80%" frameborder="no" border="0" framespacing="0"
    rows="*" bordercolor="DimGray">
	<frame scrolling="no" frameborder="no"  src="SystemManage/SystemSafety/LeftMenu.aspx"   width="10%" name="outlook" id="frameMenu">
	<frame frameborder="no"  width="90%"  name="frameMain" id="fMain" src="Index.aspx">
    <noframes>
        <body>
            <p>
                此網頁使用了框架，但您的瀏覽器不支持框架。</p>
        </body>
    </noframes>
</frameset>
</html>
