<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SigningScheduleQuery.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.SigningScheduleQuery" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WFMBillQueryForm</title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckAll() {
            var sValue = false;
            var chk = document.getElementById("UltraWebGridBill_ctl00_CheckBoxAll");
            if (chk.checked) {
                sValue = true;
            }
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            for (i = 0; i < gRows.length; i++) {
                if (!igtbl_getElementById("UltraWebGridBill_ci_0_0_" + i + "_CheckBoxCell").disabled) {
                    igtbl_getElementById("UltraWebGridBill_ci_0_0_" + i + "_CheckBoxCell").checked = sValue;
                }
            }
        }
        function ShowBillDetail(BillNo, BillTypeCode) 
        {
            var ModuleCode = igtbl_getElementById("ModuleCode").value;
            var width = screen.width * 0.95;
            var height = screen.height * 0.8;
            switch (BillTypeCode) {
                case "LeaveTypeAE":
                case "LeaveTypeAF":
                case "LeaveTypeAG":
                case "LeaveTypeAH":
                case "LeaveTypeAI":
                case "LeaveTypeAJ":
                case "LeaveTypeAK":
                case "LeaveTypeM":
                case "LeaveTypeN":
                case "LeaveTypeL":
                    openEditWin("WFMBillCenterLeaveApproveForm.aspx?SFlag=Y&BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, "WFMBillDetail", width, height);
                    break;
                default:
                    openEditWin("WFMBillCenterApproveForm.aspx?SFlag=Y&BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, "WFMBillDetail", width, height);
                    break;
            }
            //openEditWin("WFMBillDetailForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
        }
        function openEditWin(strUrl, strName, winWidth, winHeight) {
            var newWindow;
            newWindow = window.open(strUrl, strName, 'width=' + winWidth + ', height=' + winHeight + ',left=' + (screen.width - winWidth) / 2.2 + ', top=' + (screen.height - winHeight) / 2.2 + ',resizable=yes, help=no, menubar=no,scrollbars=yes,status=yes,toolbar=no');
            newWindow.oponer = window;
            newWindow.focus();
        }
        function ShowChangeAuditFlow() {
            var ModuleCode = igtbl_getElementById("ModuleCode").value;
            var BillNo = "";
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    if (gRows.getRow(i).getCellFromKey("Status").getValue() != "0") {
                        alert(BillNo + Message.wfm_message_checkchangeauditflow);
                        return false;
                    }
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
//            var width = screen.width * 0.7;
//            var height = screen.height * 0.6;

            //alert("BillNo:" + BillNo + ";ModuleCode:" + ModuleCode);
            //openEditWin("WFMBillChangeAuditFlowForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, "WFMBillDetail", width, height);
            //openEditWin("WorkFlowBillChangeAuditFlowForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, "WFMBillDetail");
            //openEditWin("WorkFlowBillChangeAuditFlowForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, "WFMBillDetail", width, height);
            //document.getElementById("iframeEdit").src = "WorkFlowBillChangeAuditFlowForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode;
            var windowWidth = 800;
            var windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            window.showModalDialog("WorkFlowBillChangeAuditFlowForm.aspx?ModuleCode=" + ModuleCode + "&BillNo=" +
	          BillNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            
            //document.getElementById("iframeEdit").src = "WorkFlowBillChangeAuditFlowForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode;
            //document.getElementById("topTable").style.display = "none";
            //document.getElementById("divEdit").style.display = "block";

            return false;
        }

        function CheckBatchDisAudit() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGridBill_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    if (gRows.getRow(i).getCellFromKey("Status").getValue() != "0") {
                        alert(BillNo + Message.wfm_message_checkbatchdisaudit);
                        return false;
                    }
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
            if (confirm(Message.common_message_data_return)) {
                //FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }

        function CheckSendNotes() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGridBill_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    if (gRows.getRow(i).getCellFromKey("Status").getValue() != "0") {
                        alert(BillNo + Message.wfm_message_checksendnotes);
                        return false;
                    }
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
            if (confirm(Message.common_message_data_return)) {
                //FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }

        function CheckReSendAudit() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGridBill_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    if (gRows.getRow(i).getCellFromKey("Status").getValue() != "2") {
                        alert(BillNo + Message.wfm_message_checkresendaudit);
                        return false;
                    }
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
            if (confirm(Message.common_message_data_return)) {
                //FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function copyToClipboard() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
            clipboardData.setData('Text', BillNo);
            alert(Message.billno_copy_success);
            return false;
        }
        //調用放大鏡
        function setSelector(ctrlCode, ctrlName,flag, moduleCode) {
            var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;
        }
        function GetSignMap() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    Count += 1;
                }
            } 
            if (Count == 0) {
                alert(Message.choose_defaultvalue);
                return false;
            }
            var windowWidth = 600;  
            var windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("SignLogAndMap.aspx?Doc=" +
	          BillNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
        }
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td class="tr_table_title" colspan="2">
                                        <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                        <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                                            <td style="width: 100%;">
                                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                                    background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                                    font-size: 13px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCondition" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 22px;">
                                                                <div id="Div1">
                                                                    <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%" id="TABLE1" class="table_data_area">
                                                <tr>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lbl_doctype" runat="server">BillTypeCode:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="23%">
                                                        <cc1:DropDownCheckList ID="ddlBillTypeCode" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                                                            Width="250" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                                                            TextWhenNoneChecked="" DisplayTextWidth="250" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                                                            runat="server">
                                                        </cc1:DropDownCheckList>
                                                    </td>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDepcode" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="23%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                        Style="display: none"></asp:TextBox>
                                                                </td>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td style="cursor: hand">
                                                                    <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="gvHeadupdateuser" runat="server" Text="ApplyMan"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="textBoxApplyMan" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="lbllblBillNo" runat="server" Text="BillNo"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="textBoxBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="gvHeaderStatusName" runat="server" Text="Status"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_label" width="8%">
                                                        &nbsp;
                                                        <asp:Label ID="labelApplyDate" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="28%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="textBoxApplyDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="textBoxApplyDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="100%">
                                                                 <asp:Panel ID="pnlShowPanel" runat="server">
                                                                    <asp:Button ID="ButtonQuery" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                                                        CommandName="Query" OnClick="ButtonQuery_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" ToolTip="Authority Code:Reset"
                                                                        CommandName="Reset" OnClick="ButtonReset_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Button ID="ButtonExport" runat="server" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                                        OnClick="ButtonExport_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Button ID="ButtonBatchDisAudit" runat="server" Text="BatchDisAudit" CommandName="BatchDisAudit"
                                                                        ToolTip="Authority Code:BatchDisAudit" OnClick="ButtonBatchDisAudit_Click" OnClientClick="return CheckBatchDisAudit()"
                                                                        CssClass="button_large"></asp:Button>
                                                                    <asp:Button ID="ButtonChangeAuditFlow" runat="server" Text="ChangeAuditFlow" CommandName="ChangeAuditFlow"
                                                                        ToolTip="Authority Code:ChangeAuditFlow" OnClientClick="return ShowChangeAuditFlow()"
                                                                        CssClass="button_large"></asp:Button>
                                                                    <asp:Button ID="ButtonSendNotes" runat="server" Text="SendNotes" CommandName="SendNotes"
                                                                        ToolTip="Authority Code:SendNotes" OnClick="ButtonSendNotes_Click" OnClientClick="return CheckSendNotes()"
                                                                        CssClass="button_large"></asp:Button>
                                                                    <asp:Button ID="ButtonReSendAudit" runat="server" Text="ReSendAudit" CommandName="ReSendAudit"
                                                                        ToolTip="Authority Code:ReSendAudit" OnClick="ButtonReSendAudit_Click" OnClientClick="return CheckReSendAudit()"
                                                                        CssClass="button_morelarge"></asp:Button>
                                                                    <asp:Button ID="ButtonCopyBillNo" runat="server" Text="CopyBillNo" CommandName="Copy"
                                                                        OnClientClick="return copyToClipboard()" ToolTip="Authority Code:Copy" CssClass="button_large" />
                                                                        </asp:Panel>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" class="table_title_area">
                                    <tr style="cursor: hand">
                                        <td class="tr_table_title" width="100%" colspan="3">
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;">
                                                    <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                            font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="tr_title_center" style="width: 300px;">
                                                        <div>
                                                            <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                                HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                                                ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                                ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                            </ess:AspNetPager>
                                                        </div>
                                                    </td>
                                                    <td style="width: 22px;" id="td_show_2">
                                                        <img id="div_img2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">

                                            <script language="JavaScript" type="text/javascript">                                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 59 / 100 + "'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%"
                                                OnDataBound="UltraWebGridBill_DataBound">
                                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridBill" TableLayout="Fixed"
                                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#6699ff" BorderStyle="Solid"
                                                        CssClass="tr_data1">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand BaseTableName="WFM_Bill" Key="WFM_Bill">
                                                        <Columns>
                                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                HeaderClickAction="Select" Width="4%" Key="CheckBoxAll">
                                                                <CellTemplate>
                                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                </CellTemplate>
                                                                <HeaderTemplate>
                                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                </HeaderTemplate>
                                                                <Header ClickAction="Select" Fixed="True">
                                                                </Header>
                                                            </igtbl:TemplatedColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BILLTYPENAME" IsBound="false" Key="BILLTYPENAME"
                                                                Width="10%">
                                                                <Header Caption="<%$Resources:ControlText,BillTypeName%>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" HeaderText="BillNo" IsBound="false"
                                                                Key="BillNo" Width="22%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgName" HeaderText="OrgName" IsBound="false"
                                                                Key="OrgName" Width="32%">
                                                                <Header Caption="<%$Resources:ControlText,gvOrgName%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyManName" IsBound="false" Key="ApplyManName"
                                                                Width="8%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeaderApplyMan%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyDate" IsBound="false" Key="ApplyDate"
                                                                Width="20%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeaderApplyDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                                Key="StatusName" Width="8%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeaderStatusName%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                HeaderClickAction="Select" Width="8%" Key="CheckBoxAll">
                                                                <CellTemplate>
                                                                    <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server">查看</asp:LinkButton>
                                                                </CellTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                                </HeaderTemplate>
                                                                <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                                </Header>
                                                            </igtbl:TemplatedColumn>
                                                            <%--                                                            <igtbl:UltraGridColumn BaseColumnName="FlowMap" HeaderText="FlowMap" IsBound="false"
                                                                Key="FlowMap" Width="45">
                                                                <Header Caption="<%$Resources:ControlText,FlowMap%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>--%>
                                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="false"
                                                                Key="Status" Hidden="true">
                                                                <Header Caption="Status">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgCode" HeaderText="OrgCode" IsBound="false"
                                                                Key="OrgCode" Hidden="true">
                                                                <Header Caption="OrgCode">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillTypeCode" HeaderText="BillTypeCode" IsBound="false"
                                                                Key="BillTypeCode" Hidden="true">
                                                                <Header Caption="BillTypeCode">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">                                                document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript">        document.write("<div id='divEdit'style='display:none;height:" + document.body.clientHeight * 84 / 100 + "'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" ></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">        document.write("</div>");</script>

    </form>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server"
        OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
    </igtblexp:UltraWebGridExcelExporter>
</body>
</html>
