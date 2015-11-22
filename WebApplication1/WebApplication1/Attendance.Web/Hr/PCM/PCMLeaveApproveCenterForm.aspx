<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMLeaveApproveCenterForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMLeaveApproveCenterForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--
        function CheckDisApprove()//拒簽判斷
        {
//            var ApRemark = document.getElementById("textBoxApRemark").value;
            if (document.getElementById("textBoxApRemark").value == "") {
                alert(Message.wfm_message_data_disapprove);
//                document.getElementById("textBoxApRemark").focus();
                return false;
            }
            else {
                if (confirm(Message.wfm_message_disaudit)) {
                    //FormSubmit("<%=sAppPath%>");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function CheckApprove() {
            if (confirm(Message.common_message_data_return)) {
                //FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function CheckClose()//關閉
        {
            window.close();
            return false;
        }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server" />
    <input id="HiddenBillTypeNo" type="hidden" name="HiddenBillTypeNo" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center"
        class="table_data_area">
        <tr>
            <td>
                <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand">
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr2">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lan_mx" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div id="Div2">
                                            <img id="img1" class="img1" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <script language="javascript">                                document.write("<DIV id='div_6' style='height:" + document.body.clientHeight * 45 / 100 + "'>");</script>

                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="td_label" style="width: 13%">
                                        &nbsp;
                                        <asp:Label ID="labelEmployeeNo" runat="server">EmployeeNo:</asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 20%">
                                        <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 13%">
                                        &nbsp;
                                        <asp:Label ID="labelName" runat="server">Name:</asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 20%">
                                        <asp:TextBox ID="textBoxName" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 13%">
                                        &nbsp;
                                        <asp:Label ID="lbllblBillNo" runat="server">KQLBillNo:</asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 20%">
                                        <asp:TextBox ID="textBoxKQLBillNo" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblSex" runat="server">Sex:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxSex" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label" width="13%">
                                        &nbsp;
                                        <asp:Label ID="labelDepcode" runat="server">Department:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxDPcode" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lbllblJoinDate" runat="server">JoinDate:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxJoinDate" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblLevelCode" runat="server">LevelCode:</asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 11%">
                                        <asp:TextBox ID="textBoxLevelCode" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblManager" runat="server">Manager:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxManager" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label" width="13%">
                                        &nbsp;
                                        <asp:Label ID="lblComeYears" runat="server">ComeYears:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxComeYears" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 10px">
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <div id="divEmpLeave" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 10px">
                                </tr>
                                <tr>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="gvLVTypeName" runat="server">LVTypeName:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxLVTypeName" runat="server" Width="100%" CssClass="input_textBox"
                                            ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lbllblStartDate" runat="server">StartDate:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="textBoxStartDate" runat="server" Width="100%" CssClass="input_textBox"
                                                        ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="textBoxStartTime" runat="server" Width="100%" CssClass="input_textBox"
                                                        ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lbllblEndDate" runat="server">EndDate:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="textBoxEndDate" runat="server" Width="100%" CssClass="input_textBox"
                                                        ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="textBoxEndTime" runat="server" Width="100%" CssClass="input_textBox"
                                                        ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblApplyType" runat="server">ApplyType:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxApplyType" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblReason" runat="server">Reason:</asp:Label>
                                    </td>
                                    <td class="td_input" colspan="3">
                                        <asp:TextBox ID="textBoxReason" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblProxy" runat="server">Proxy:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxProxy" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblLVTotalDays" runat="server">LVTotal:</asp:Label>
                                    </td>
                                    <td class="td_input" style="height: 22px">
                                        <asp:TextBox ID="textBoxLVTotal" runat="server" Width="100%" CssClass="input_textBox"
                                            ForeColor="red" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="td_label">
                                        &nbsp;
                                        <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                    </td>
                                    <td class="td_input">
                                        <asp:TextBox ID="textBoxRemark" runat="server" Width="100%" CssClass="input_textBox"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                            <script language="JavaScript" type="text/javascript">                                document.write("</DIV>");</script>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="ApproveBTN" runat="server">
            <td>
                <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr1">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSignCenter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div id="Div1">
                                            <img id="img2" class="img1" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="td_label" width="12%">
                            &nbsp;
                            <asp:Label ID="labelApRemark" runat="server">ApRemark:</asp:Label>
                        </td>
                        <td class="td_input" width="88%">
                            <asp:TextBox ID="textBoxApRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <br />
                            <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="ButtonApproveAgree" runat="server" Text="Approve" ToolTip="Authority Code:Approve"
                                CommandName="Approve" OnClick="ButtonApprove_Click" OnClientClick="return CheckApprove()"
                                CssClass="button_1"></asp:Button>
                            <asp:Button ID="ButtonDisApprove" runat="server" Text="DisApprove" ToolTip="Authority Code:DisApprove"
                                CommandName="DisApprove" OnClick="ButtonDisApprove_Click" OnClientClick="return CheckDisApprove()"
                                CssClass="button_1"></asp:Button>
                            <asp:Button ID="ButtonClose" runat="server" Text="Close" ToolTip="Authority Code:Close"
                                CommandName="Close" OnClientClick="return CheckClose()" CssClass="button_1">
                            </asp:Button>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
