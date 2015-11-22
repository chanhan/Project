<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveTestifyForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData.KQMLeaveTestifyForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script>
$(function(){
                   $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
})
    </script>

</head>
<base  target="_self"/>
<body>
    <form id="form1" runat="server">
    <input id="hidID" type="hidden" name="hidID" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td colspan="2">
                <div id="tr_edit">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="td_label_view" style="width: 13%">
                                &nbsp;
                                <asp:Label ID="lblEmployeeNo" runat="server">EmployeeNo:</asp:Label>
                            </td>
                            <td class="td_input" style="width: 20%">
                                <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                            </td>
                            <td class="td_label_view" style="width: 13%">
                                &nbsp;
                                <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                            </td>
                            <td class="td_input" style="width: 20%">
                                <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                            </td>
                            <td class="td_label_view" style="width: 13%">
                                &nbsp;
                                <asp:Label ID="lblLeaveTypeName" runat="server">LeaveTypeName:</asp:Label>
                            </td>
                            <td class="td_input" style="width: 20%">
                                <asp:TextBox ID="txtLeaveTypeName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" colspan="6">
                                &nbsp;
                                <asp:CheckBox ID="chkFlag" runat="server" />
                                <asp:Label ID="lblTestifyFlag" runat="server" Text="Label" ForeColor="red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr id="testifyfile" style="display: none">
                            <td class="td_label" colspan="6">
                                &nbsp;
                                <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" Width="400" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" colspan="6">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp; &nbsp;
                                            <asp:Button ID="btnSave" runat="server" ToolTip="Authority Code:Save" CssClass="button_1"
                                                OnClick="btnSave_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript"><!--
        onChangeFlag(document.all("chkFlag"));
		function onChangeFlag(obj)   
        {   
          if(obj.checked)
          {
	        document.getElementById("testifyfile").style.display="";
          }
          else
          {
	        document.getElementById("testifyfile").style.display="none";    
          } 
        }
    --></script>

</body>
</html>
