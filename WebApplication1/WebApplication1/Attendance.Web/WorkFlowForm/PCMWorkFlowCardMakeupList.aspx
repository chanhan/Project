﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMWorkFlowCardMakeupList.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlowForm.PCMWorkFlowCardMakeupList" %>


<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CardMakeupForm</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../WorkFlow/css/MyStyles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--
        function UpProgress() {
            document.getElementById("ButtonImportSave").style.display = "none";
            document.getElementById("ButtonImport").disabled = "disabled";
            document.getElementById("ButtonExport").disabled = "disabled";
            document.getElementById("imgWaiting").style.display = "";
            document.getElementById("labelupload").innerText = "<%=Resources.ControlText.common_message_uploading%>";
            FormSubmit("<%=sAppPath%>");
        }
        function CheckAll() {
            var sValue = false;
            var chk = document.getElementById("UltraWebGridMakeup_ctl00_CheckBoxAll");
            if (chk.checked) {
                sValue = true;
            }
            var grid = igtbl_getGridById('UltraWebGridMakeup');
            var gRows = grid.Rows;
            for (i = 0; i < gRows.length; i++) {
                if (!igtbl_getElementById("UltraWebGridMakeup_ci_0_0_" + i + "_CheckBoxCell").disabled) {
                    igtbl_getElementById("UltraWebGridMakeup_ci_0_0_" + i + "_CheckBoxCell").checked = sValue;
                }
            }
        }
        function AfterSelectChange(gridName, id) {
            var row = igtbl_getRowById(id);
            DisplayRowData(row);
            return 0;
        }

        function UltraWebGridMakeup_InitializeLayoutHandler(gridName) {
            var row = igtbl_getActiveRow(gridName);
            DisplayRowData(row);
        }
        function DisplayRowData(row) {

            if (row != null) {
                document.getElementById("HiddenWorkNo").value = row.getCell(2).getValue() == null ? "" : row.getCell(2).getValue();
                document.getElementById("HiddenBillNo").value = row.getCell(13).getValue() == null ? "" : row.getCell(13).getValue();
                document.getElementById("HiddenID").value = row.getCell(22).getValue() == null ? "" : row.getCell(22).getValue();
                //alert("ddddddd" + document.getElementById("HiddenID").value + "/r"+row.getCell(21).getValue());
            }
        }
        function DblClick(gridName, id)//雙擊
        {
            var ProcessFlag = "Modify";

            OpenEdit(ProcessFlag)
            return 0;
        }

        //新增或修改
        //ProcessFlag:標志位：Add表示新增
        function OpenEdit(ProcessFlag) {
            var EmployeeNo = igtbl_getElementById("HiddenWorkNo").value;
            var ID = igtbl_getElementById("HiddenID").value;
            var ModuleCode = igtbl_getElementById("ModuleCode").value;

            igtbl_getElementById("ProcessFlag").value = ProcessFlag;
            if (ProcessFlag == "Modify") {

                var grid = igtbl_getGridById('UltraWebGridMakeup');
                var gRows = grid.Rows;
                var Count = 0;

                for (i = 0; i < gRows.length; i++) {
                    if (gRows.getRow(i).getSelected()) {
                        Count += 1;
                    }
                }
                if (Count == 0) {
                    alert("<%=Resources.Message.common_message_data_select%>");
                    return false;
                }
            }
            // alert("WorkFlowMakeupEditForm.aspx?EmployeeNo="+EmployeeNo+"&ID="+ID+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode);
            document.getElementById("iframeEdit").src = "WorkFlowMakeupEditForm.aspx?EmployeeNo=" + EmployeeNo + "&ID=" + ID + "&ProcessFlag=" + ProcessFlag + "&ModuleCode=" + ModuleCode;
            document.getElementById("divEdit").style.display = "";
            document.getElementById("topTable").style.display = "none";
            document.getElementById("div_2").style.display = "none";
            

            return false;
        } 
        function CheckDelete() {
            var grid = igtbl_getGridById('UltraWebGridMakeup');
            var gRows = grid.Rows;
            var Count = 0;
            var State = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGridMakeup_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                    State = gRows.getRow(i).getCellFromKey("Status").getValue();
                    switch (State) {
                        case "0":
                        case "3":
                            break;
                        default:
                            alert("<%=Resources.Message.DeleteApplyovertimeEnd%>");
                            return false;
                            break;
                    }
                }
            }
            if (Count == 0) {
                alert("<%=Resources.Message.common_message_data_select%>");
                return false;
            }
            if (confirm("<%=Resources.Message.ConfirmBatchConfirm%>")) {
                FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        } 
        function OpenAuditStatus()//查看簽核進度
        {
            var grid = igtbl_getGridById('UltraWebGridMakeup');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert("<%=Resources.Message.common_message_data_select%>");
                return false;
            }
            var ModuleCode = "<%//=this.moduleCode %>";
            var BillNo = igtbl_getElementById("HiddenBillNo").value;
            var width = 550;
            var height = 150;
            openEditWin("../../PCM/PCMAuditStatusForm.aspx?ModuleCode=" + ModuleCode + "&BillNo=" + BillNo, "AuditStatus", width, height);
            return false;
        }

        function OrgAudit()//選擇組織送簽
        {
            document.getElementById("HiddenOrgCode").value = "";
            var grid = igtbl_getGridById('UltraWebGridMakeup');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (document.getElementById("UltraWebGridMakeup_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert("<%=Resources.Message.common_message_data_select%>");
                return false;
            }
            var windowWidth = 500, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("<%=sAppPath%>/SystemManage/TreeDataPickForm.aspx?DataType=Department&condition=&modulecode=" +
	          document.getElementById("ModuleCode").value, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            if (Revalue != undefined) {
                document.getElementById("HiddenOrgCode").value = Revalue.split(';')[0];
            }
            if (document.getElementById("HiddenOrgCode").value.length > 0) {
                document.getElementById("ButtonSendAudit").click();
            }
            return false;
        }
         
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display = "";
            document.all("PanelBatchWorkNo").style.top = document.all("textBoxEmployeeNo").style.top;
            document.getElementById("textBoxBatchEmployeeNo").style.display = "";
            document.getElementById("textBoxEmployeeNo").value = "";
            document.getElementById("textBoxBatchEmployeeNo").value = "";
            document.getElementById("textBoxBatchEmployeeNo").focus();
            return false;
        }
        function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display = "none";
            document.getElementById("textBoxBatchEmployeeNo").style.display = "none";
            document.getElementById("textBoxBatchEmployeeNo").value = "";
        }

        function setSelector(ctrlCode, ctrlName, moduleCode) {
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
            var grid = igtbl_getGridById('UltraWebGridMakeup');
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
            //alert("BillNo:"+BillNo)
            if (BillNo == null) {
                alert(Message.wfm_nosign_message);
                return false;
            }
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2; 
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("../WorkFlow/SignLogAndMap.aspx?Doc=" +
	          BillNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
        }
        
		--></script>

    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        .style2
        {
            height: 20px;
        }
    </style>

</head>
<body class="color_Body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server" />
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="1" cellpadding="0" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="table_title_area" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand" onclick="turnit('div_1','div_img_1','<%=sAppPath%>');">
                                    <td style="width: 100%;" class="tr_title_center">
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
                                        <div id="img_edit">
                                            <img id="div_img" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                                <tr>
                                                   
                                                  
                                                    <td class="td_label" style="width:15%">
                                                        &nbsp;
                                                        <asp:Label ID="LabelBillNo" runat="server" Text="<%$Resources:ControlText,gvHeadBillNo%>"></asp:Label>
                                                    </td>
                                                    <td class="td_input" style="width:20%;">
                                                        <asp:TextBox ID="textBoxBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" style="width:15%">
                                                        &nbsp;
                                                        <asp:Label ID="workFlowDecSalary" runat="server" Text="DecSalary"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlDecSalary" runat="server" Width="100%" CssClass="input_textBox">
                                                            <asp:ListItem Value="" Text="    "></asp:ListItem>
                                                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                                                            <asp:ListItem Value="N" Text=" N "></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                     <td class="td_label" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="gvKQDate" runat="server" Text="KQDate"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="25%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td id="KQDateFlag" width="50%">
                                                                    <asp:TextBox ID="textBoxKQDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                                <td id="hiddenflag">
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="textBoxKQDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td class="style2">
                                                        &nbsp;
                                                        <asp:Label ID="labelReasonType" runat="server" Text="<%$Resources:ControlText,cardMakeupType%>"></asp:Label>
                                                    </td>
                                                    <td class="style2">
                                                        <cc1:DropDownCheckList ID="ddlReasonType" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 100px;overflow: scroll;"
                                                            Width="400" RepeatColumns="3" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                                                            TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                                                            runat="server">
                                                        </cc1:DropDownCheckList>
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;
                                                        <asp:Label ID="labelStatus" runat="server" Text="<%$Resources:ControlText,gvApproveFlagName %>"></asp:Label>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="input_textBox">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="13">
                                                        <asp:Button ID="btnFormQuery" runat="server" Text="<%$Resources:ControlText,btnFormQuery %>"
                                                            CssClass="input_linkbutton WF_input_Button2" ToolTip="Authority Code:Query" OnClick="ButtonQuery_Click"
                                                            BorderWidth="0px"></asp:Button>
                                                        <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                            ToolTip="Authority Code:Reset" OnClick="ButtonReset_Click" CssClass="input_linkbutton WF_input_Button2"
                                                            BorderWidth="0px"></asp:Button>
                                                        <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                            ToolTip="Authority Code:Add" OnClientClick="return OpenEdit('Add')" CssClass="input_linkbutton WF_input_Button2"
                                                            BorderWidth="0px"></asp:Button>
                                                        <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                            ToolTip="Authority Code:Modify" OnClientClick="return OpenEdit('Modify')" CssClass="input_linkbutton WF_input_Button2"
                                                            BorderWidth="0px"></asp:Button>
                                                        <asp:Button ID="Btn_delete" runat="server" Text="<%$Resources:ControlText,Btn_delete %>"
                                                            ToolTip="Authority Code:Delete" OnClientClick="return CheckDelete()" OnClick="ButtonDelete_Click"
                                                            CssClass="input_linkbutton WF_input_Button2" BorderWidth="0px"></asp:Button>
 
                                                          
                                                    </td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td colspan="3">

                                <script language="javascript">                                    document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 65 / 100 + ";'>");</script>

                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;">
                                        <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
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
                                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                                    ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
                                                </ess:AspNetPager>
                                            </div>
                                        </td>
                                        <td style="width: 22px;" id="td_show_2">
                                            <img id="div_img2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                                        </td>
                                    </tr>
                                </table>
                                <igtbl:UltraWebGrid ID="UltraWebGridMakeup" runat="server" Width="100%" Height="100%"
                                    OnDataBound="UltraWebGridMakeup_DataBound">
                                    <DisplayLayout UseFixedHeaders="True" Name="UltraWebGridMakeup" CompactRendering="False"
                                        RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" AutoGenerateColumns="false"
                                        TableLayout="Fixed" StationaryMargins="Header">
                                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <FrameStyle Width="100%" Height="100%">
                                        </FrameStyle>
                                        <ClientSideEvents InitializeLayoutHandler="UltraWebGridMakeup_InitializeLayoutHandler"
                                            AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="DblClick"></ClientSideEvents>
                                        <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                        </SelectedRowStyleDefault>
                                        <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                        </RowAlternateStyleDefault>
                                        <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                            CssClass="tr_data">
                                            <Padding Left="3px"></Padding>
                                            <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                        </RowStyleDefault>
                                        <ActivationObject BorderColor="" BorderWidth="">
                                        </ActivationObject>
                                    </DisplayLayout>
                                    <Bands>
                                        <igtbl:UltraGridBand BaseTableName="GDS_ATT_MAKEUP" Key="GDS_ATT_MAKEUP">
                                            <Columns>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                    <CellTemplate>
                                                        <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                    </CellTemplate>
                                                    <HeaderTemplate>
                                                        <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                    </HeaderTemplate>
                                                    <Header Caption="CheckBox" ClickAction="Select" Fixed="true">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DName" Key="DName" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkNo %>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                    Width="170px">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQDate" Key="KQDate" IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvKQDate %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="CardTime" Key="CardTime" IsBound="false" Width="70">
                                                    <Header Caption="<%$Resources:ControlText,gvOTMRealCardTime%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MakeupTypeName" Key="MakeupTypeName" IsBound="false"
                                                    Width="80px">
                                                    <Header Caption="<%$Resources:ControlText,cardMakeupType%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DecSalary" Key="DecSalary" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,workFlowDecSalary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvApproveFlagName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ReasonName" Key="ReasonName" IsBound="false"
                                                    Width="80px">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadReasonName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" Key="ReasonRemark" IsBound="false"
                                                    Width="150px">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DeTimes" Key="DeTimes" IsBound="false" Width="60">
                                                    <Header Caption="<%$Resources:ControlText,workFlowDeTimes%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApproverName" Key="ApproverName" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvheadsignname%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                    Width="110">
                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproveDate%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="120">
                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="70">
                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifier%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                    Width="110">
                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifyDate%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" Width="100px">
                                                    <CellTemplate>
                                                        <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server" Text="<%$Resources:ControlText,BtnViewSchedule %>"></asp:LinkButton>
                                                    </CellTemplate>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                    </HeaderTemplate> 
                                                    <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="true">
                                                    <Header Caption="Status">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DCode" Key="DCode" IsBound="false" Hidden="true">
                                                    <Header Caption="DCode">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                    <Header Caption="ID">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                            </Columns>
                                        </igtbl:UltraGridBand>
                                    </Bands>
                                </igtbl:UltraWebGrid>

                                <script language="javascript">                                    document.write("</DIV>");</script>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr> 
    </table>

    <script language="javascript">        document.write("<div id='divEdit'style='display:none;height:" + document.body.clientHeight * 84 / 100 + "'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">        document.write("</div>");</script>

    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    </form>
</body>
</html>
