<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorityDetailForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.AuthorityDetailForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Pragma" content="no-cache" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script>
$(document).ready(function()
{
$('#<%=txtModuleCode.ClientID %>').attr('readonly',true);
$('#<%=txtAllFunctionList.ClientID %>').attr('readonly',true);
$('#<%=txtFunctionDesc.ClientID %>').attr('readonly',true);
$('#<%=txtFunctionList.ClientID %>').focus();
//var esrc =$('#<%=txtFunctionList.ClientID %>');
//if(esrc==null){
//   esrc=event.srcElement;
//}
//var rtextRange =esrc.createTextRange();
//rtextRange.moveStart('character',esrc.value.length);
//rtextRange.collapse(true);
//rtextRange.select();

    $('#<%=btnSave.ClientID %>').click(function(){
    var  moduleCode=$('#<%=txtModuleCode.ClientID %>').val();
    var  functionList=$('#<%=txtFunctionList.ClientID %>').val();
     window.returnValue = { pmoduleCode: moduleCode, pfunctionList: functionList };
     window.close();
     return false;
    });
     
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
}
);
    </script>

</head>
<body id="detial">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
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
                    <div id="Div1">
                        <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="tr_edit">
        <table width="100%">
            <asp:Panel ID="pnlContent" runat="server">
                <tr>
                    <td class="td_label" width="15%">
                        <asp:Label ID="lbllblModuleCode" runat="server">Module:</asp:Label>
                    </td>
                    <td class="td_input" width="85%">
                        <asp:TextBox ID="txtModuleCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        <asp:Label ID="lblFunctionList" runat="server">Function:</asp:Label>
                    </td>
                    <td class="td_input">
                        <asp:TextBox ID="txtAllFunctionList" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        <asp:Label ID="lblAuthorizedFunctionList" runat="server">Authorized Function:</asp:Label>
                    </td>
                    <td class="td_input">
                        <asp:TextBox ID="txtFunctionList" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        <asp:Label ID="lblFunctionDesc" runat="server">Function Desc:</asp:Label>
                    </td>
                    <td class="td_input">
                        <asp:TextBox ID="txtFunctionDesc" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="button_1"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
