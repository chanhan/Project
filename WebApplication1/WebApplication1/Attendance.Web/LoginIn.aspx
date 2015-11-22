<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginIn.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.LoginIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登陸介面</title>
    <link href="CSS/hrlogin.css" type="text/css" rel="stylesheet">
  <%--  <script type="text/javascript">
      $("#<%=imgbtnLogin.ClientID %>").click(function() {
                var userId = $.trim($("#<%=txtUserId.ClientID %>").val()); var password = $.trim($("#<%=txtPassword.ClientID %>").val());
                if (userId && password) {
                    $("#divMessage").text("登陸中，請稍候").dialog({ width: 260, height: 80, modal: true });
                    $.ajax({
                        type: "POST", url: "LoginIn.aspx", dataType: "text", data: { UserId: userId, Password: password},
                        success: function() {
                             window.location.replace("MainForm.aspx"); 
                        }
                    });
                }
                else {
                    $("#<%=txtUserId.ClientID %>,#<%=txtPassword.ClientID %>").each(function() {
                        if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { $(this).css("border-color", "#ff6666"); }
                    });
                }
                return false;
            });
    </script>--%>

    <style type="text/css">
        .style1
        {
            width: 72px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table width="699" border="0" align="center" valign="center" cellpadding="0" cellspacing="0"
            class="top_table">
            <tr>
                <td colspan="5" style="height: 186px">
                    <img src="CSS/Images/12_01.jpg" width="699" height="186" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    <img src="CSS/Images/12_02.jpg" width="699" height="87" border="0" usemap="#Map" /></td>
            </tr>
            <tr>
                <td width="254" height="85" rowspan="6">
                    <img src="CSS/Images/12_03.jpg" width="254" height="104" /></td>
                <td width="85" background="CSS/Images/12_12.jpg">
                <span class="wenzi01">
                        <asp:Label ID="lblUserId" Text="用 戶 名：" runat="server" onmouseover="if ( this.clientWidth < this.scrollWidth ) this.title = this.innerText; else this.title = '';"
                            Style="width: 85; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"></asp:Label></span>
                    </td>
                <td width="138" >
                    <asp:TextBox ID="txtUserId" CssClass="wenbenyu1" runat="server" TabIndex="2"></asp:TextBox>
                </td>
                <td width="72" rowspan="6">
                    <img src="CSS/Images/12_06.jpg" width="72" height="104" /></td>
                <td width="150" rowspan="6">
                    <img src="CSS/Images/12_07.jpg" width="150" height="104" /></td>
            </tr>
            <tr>
                <td style="height:5px" background="CSS/Images/12_12.jpg">
                    <img src="CSS/Images/12_12.jpg" width="85" height="5" />
                    </td>
                <td style="height:5px" background="CSS/Images/12_13.jpg">
                    <img src="CSS/Images/12_13.jpg" width="138" height="5" /></td>
            </tr>
            <tr>
                <td background="CSS/Images/12_12.jpg">
                 <span class="wenzi01">
                        <asp:Label ID="lblPassword" Text="密  碼：" runat="server" onmouseover="if ( this.clientWidth < this.scrollWidth ) this.title = this.innerText; else this.title = '';"
                            Style="width: 85; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"></asp:Label></span>
                   </td>
                <td>
                    <asp:TextBox ID="txtPassword" CssClass="wenbenyu1" runat="server" TextMode="Password"
                        TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td background="CSS/Images/12_12.jpg" style="height:5px">
                    
                    </td>
                <td background="CSS/Images/12_13.jpg" style="height:5px">
                    </td>
            </tr>
            <tr>
                <td width="85" background="CSS/Images/12_14.jpg">
                <asp:ImageButton ID="imgbtnLogin" ImageUrl="CSS/Images/12_16.jpg" Width="72" Height="37"
                        border="0" runat="server" OnClick="imgbtnLogin_Click" TabIndex="4"  />
                    </td>
                <td background="CSS/Images/12_13.jpg">
                   </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td background="CSS/Images/12_17.jpg">
                    <img src="CSS/Images/12_17.jpg" width="85" height="5" /></td>
                <td background="CSS/Images/12_18.jpg">
                    <img src="CSS/Images/12_18.jpg" width="138" height="5" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    <img src="CSS/Images/12_21.jpg" width="699" height="47" /></td>
            </tr>
            <tr style="background-color: #fff;">
            <td colspan="5" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="420px"></asp:Label>
            </td>
        </tr>
        </table>
     
    <div id="divMessage" title="Message" style="text-align: center; display: none;">
    </div>
    </form>
</body>
</html>
