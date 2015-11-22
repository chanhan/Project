<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.SendMessage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SendMessage</title>

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

</head>

<script>
$(function(){
   $("#tr_edit").toggle(
    function(){
        $("#div_1").hide();
        $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
        
    },
    function(){
      $("#div_1").show();
        $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
   
    }
)}) 
</script>

<body>
    <form runat="server">
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_1">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="15%" align="center">
                        &nbsp;
                        <asp:Label ID="lblSendTo" runat="server">SendTo:</asp:Label>
                    </td>
                    <td width="85%" align="center">
                        <asp:DropDownList ID="ddlSendTo" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;
                        <asp:Label ID="lblSendWorkNo" runat="server" align="center">WorkNo:</asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;
                        <asp:Label ID="lblMSG" runat="server">MSG:</asp:Label>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtMSG" runat="server" Width="100%" TextMode="MultiLine" Rows="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="btnSendMsg" class="button_1" runat="server" Text="SendMsg" OnClick="btnSendMsg_Click">
                            </asp:Button>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>

<script>
function load()
{
     $(".input_textBox").css("border-style", "none");
     return true;
}
</script>

</html>
