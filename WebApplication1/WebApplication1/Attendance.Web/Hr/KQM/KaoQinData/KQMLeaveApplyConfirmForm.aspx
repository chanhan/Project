<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveApplyConfirmForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData.KQMLeaveApplyConfirmForm" %>

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

    <script type="text/javascript">
    <!--
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
  function  closeWindow()
    {
    window.close();
    return false;
    }
    function GetLVTotal(){
        var sStartDate=document.getElementById("txtStartDate").value;
        var sEndDate=document.getElementById("txtEndDate").value;
        var sStartTime=document.getElementById("txtStartTime").value;
        var sEndTime=document.getElementById("txtEndTime").value;
        var sLVTypeCode=document.getElementById("hidLVTypeCode").value;
        var sWorkNo=document.getElementById("txtEmployeeNo").value;
        if(sWorkNo!=""&&sStartDate!=""&&sEndDate!=""&&sStartTime!=""&&sEndTime!=""&&sLVTypeCode!="")
        {
            if(!validateCNDate(sStartDate)||!validateCNDate(sEndDate))
	        {		       
	           return;		       
	        } 
       		             $.ajax({
                                 type: "post", url: "KQMLeaveApplyConfirmForm.aspx", dateType: "text", data: {workno: sWorkNo,startDate:sStartDate+" "+sStartTime,endDate:sEndDate+" "+sEndTime,typecode:sLVTypeCode},
                                 success: function(msg) {
                                                                                     if(msg !="")
        {
            igedit_getById("txtLVTotal").setValue(msg);
        }
        else
        {
            igedit_getById("txtLVTotal").setValue("0");
        }
                                                     }
                           });
        }
    }     
    function onChangeLVTypeCode(strValue)
    {
        document.getElementById("hidLVTypeCode").value=strValue;
        GetLVTotal();
    }
              function validateCNDate(strValue) 
           { 
             var theStr=strValue.replace("-","/");
             var pattern = /^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
             return pattern.exec(theStr);
         }
   -->
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="hidBillNo" type="hidden" name="hidBillNo" runat="server">
    <input id="hidLVTypeCode" type="hidden" name="hidLVTypeCode" runat="server">
    <input id="hidLVTotal" type="hidden" name="hidLVTotal" runat="server">
    <input id="hidBoolYearMonth" type="hidden" name="hidBoolYearMonth" runat="server">
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
    <div id="tr_edit">
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="td_label_view" style="width: 15%">
                    &nbsp;
                    <asp:Label ID="lblEmployeeNo" runat="server">EmployeeNo:</asp:Label>
                </td>
                <td class="td_input" style="width: 18%">
                    <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
                <td class="td_label_view" style="width: 18%">
                    &nbsp;
                    <asp:Label ID="lblName" runat="server">Name:</asp:Label>
                </td>
                <td class="td_input" style="width: 20%">
                    <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
                <td class="td_label_view" style="width: 15%">
                    &nbsp;
                    <asp:Label ID="lbllBillNo" runat="server">BillNo:</asp:Label>
                </td>
                <td class="td_input" style="width: 14%">
                    <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lbllStartDate" runat="server">StartDate:</asp:Label>
                </td>
                <td class="td_input">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="80%">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox"
                                    onkeydown="if(event.keyCode==13) event.keyCode=9"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <igtxt:WebDateTimeEdit ID="txtStartTime" runat="server" CssClass="input_textBox"
                                    Width="40px" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                    <ClientSideEvents ValueChange="GetLVTotal" />
                                </igtxt:WebDateTimeEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lbllEndDate" runat="server">EndDate:</asp:Label>
                </td>
                <td class="td_input">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="80%">
                                <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox"
                                    onkeydown="if(event.keyCode==13) event.keyCode=9"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <igtxt:WebDateTimeEdit ID="txtEndTime" runat="server" CssClass="input_textBox" Width="40px"
                                    onkeydown="if(event.keyCode==13) event.keyCode=9">
                                    <ClientSideEvents ValueChange="GetLVTotal" />
                                </igtxt:WebDateTimeEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblLVTotal" runat="server">LVTotal(H):</asp:Label>
                </td>
                <td class="td_input" style="height: 22px">
                    <igtxt:WebNumericEdit ID="txtLVTotal" runat="server" Width="100%" CssClass="input_textBox"
                        HorizontalAlign="Left" MinValue="0">
                    </igtxt:WebNumericEdit>
                </td>
            </tr>
            <tr>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lbllLVTypeCode" runat="server">LVTypeCode:</asp:Label>
                </td>
                <td class="td_input">
                    <asp:DropDownList ID="ddlLVTypeCode" runat="server" Width="100%" CssClass="input_textBox">
                    </asp:DropDownList>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblReason" runat="server">Reason:</asp:Label>
                </td>
                <td class="td_input" colspan="3">
                    <asp:TextBox ID="txtReason" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblProxy" runat="server">Proxy:</asp:Label>
                </td>
                <td class="td_input">
                    <asp:TextBox ID="txtProxy" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                </td>
                <td class="td_input" colspan="3">
                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                </td>
            </tr>
            <tr id="divLeaveYear" runat="server" visible="false">
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblAbleRest" runat="server">AbleRest:</asp:Label>
                </td>
                <td class="td_input">
                    <asp:TextBox ID="txtAbleRest" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lbAlreadyReset" runat="server">AlreadyRest:</asp:Label>
                </td>
                <td class="td_input">
                    <asp:TextBox ID="txtAlreadyRest" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                </td>
                <td class="td_label">
                    &nbsp;
                    <asp:Label ID="lblLeaveReset" runat="server">LeaveReset:</asp:Label>
                </td>
                <td class="td_input">
                    <asp:TextBox ID="txtLeaveReset" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_label" colspan="6">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" ToolTip="Authority Code:Save" CssClass="button_1"
                                    OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnReturn" runat="server" CssClass="button_1" ToolTip="Authority Code:Return"
                                    OnClientClick="return closeWindow();" />
                                <asp:Label ID="lblReachLeaveRemark" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript"><!--
            document.getElementById("txtBillNo").readOnly=true;
            document.getElementById("txtEmployeeNo").readOnly=true;
            document.getElementById("txtName").readOnly=true;
            //document.getElementById("textBoxLVTypeName").readOnly=true;
            document.getElementById("txtReason").readOnly=true;
            document.getElementById("txtProxy").readOnly=true;
            igedit_getById("txtLVTotal").setReadOnly(true);
            document.getElementById("txtStartDate").readOnly=true;
            igedit_getById("txtStartTime").setReadOnly(true);
            if(document.getElementById("hidLVTypeCode").value=="Y")
            {
                document.getElementById("txtStartDate").readOnly=true;
                igedit_getById("txtStartTime").setReadOnly(true);
                document.getElementById("txtAbleRest").readOnly=true;
                document.getElementById("txtAlreadyRest").readOnly=true;
                document.getElementById("txtLeaveReset").readOnly=true;
            }
            if(document.getElementById("hidBoolYearMonth").value=="Y")
            {
                document.getElementById("txtStartDate").readOnly=false;
                igedit_getById("txtStartTime").setReadOnly(false);
            }
	--></script>

</body>
</html>
