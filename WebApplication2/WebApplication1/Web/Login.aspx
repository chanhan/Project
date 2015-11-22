<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Maticsoft.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
           background-color: White;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background:url(Images/top02.gif) ">
        <tr>
            <td height="147">
            </td>
        </tr>
    </table>
    <table width="562" border="0" align="center" cellpadding="0" cellspacing="0" class="right-table03">
        <tr>
            <td width="221">
                <table width="95%" border="0" cellpadding="0" cellspacing="0" class="login-text01">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="login-text01">
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="40" align="center">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <img src="Images/line01.gif" width="5" height="292" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="31%" height="35" class="login-text02">
                            用户名：<br />
                        </td>
                        <td width="69%">
                            <asp:TextBox ID="username" Width="200px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" class="login-text02">
                            密 码：<br />
                        </td>
                        <td>
                            <asp:TextBox ID="password" Width="200px" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="35">
                            &nbsp;
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="登 陆" onclick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
