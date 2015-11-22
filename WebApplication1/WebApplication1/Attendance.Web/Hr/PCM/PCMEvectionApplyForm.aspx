<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMEvectionApplyForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMEvectionApplyForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EvectionApplyForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
        function UpProgress() {
            document.getElementById("btnImportSave").style.display = "none";
            document.getElementById("btnImport").disabled = "disabled";
            document.getElementById("btnExport").disabled = "disabled";
            document.getElementById("imgWaiting").style.display = "";
            document.getElementById("lblupload").innerText = Message.DeleteConfirm;
        }
        function CheckAll() {
            var sValue = false;
            var chk = document.getElementById("UltraWebGridEvectionApply_ctl00_CheckBoxAll");
            if (chk.checked) {
                sValue = true;
            }
            var grid = igtbl_getGridById('UltraWebGridEvectionApply');
            var gRows = grid.Rows;
            for (i = 0; i < gRows.length; i++) {
                if (!igtbl_getElementById("UltraWebGridEvectionApply_ci_0_0_" + i + "_CheckBoxCell").disabled) {
                    igtbl_getElementById("UltraWebGridEvectionApply_ci_0_0_" + i + "_CheckBoxCell").checked = sValue;
                }
            }
        }
        function AfterSelectChange(gridName, id) {
            var row = igtbl_getRowById(id);

            DisplayRowData(row);
            return 0;
        }
        function UltraWebGridEvectionApply_InitializeLayoutHandler(gridName) {
            var row = igtbl_getActiveRow(gridName);
            DisplayRowData(row);
        }
        function DisplayRowData(row) {
            if (row != null) {
                igtbl_getElementById("HiddenBillNo").value = row.getCell(1).getValue() == null ? "" : row.getCell(1).getValue();
                igtbl_getElementById("HiddenID").value = row.getCell(2).getValue() == null ? "" : row.getCell(2).getValue();
                igtbl_getElementById("HiddenWorkNo").value = row.getCell(3).getValue() == null ? "" : row.getCell(3).getValue();
            }
        }
        function OpenEdit(ProcessFlag)//彈出新增或修改頁面
        {
            var ModuleCode = igtbl_getElementById("HiddenModuleCode").value;
            var BillNo = igtbl_getElementById("HiddenBillNo").value;
            igtbl_getElementById("ProcessFlag").value = ProcessFlag;
            if (ProcessFlag == "Modify") {
                var grid = igtbl_getGridById('UltraWebGridEvectionApply');
                var gRows = grid.Rows;
                var Count = 0;
                var Status;
                for (i = 0; i < gRows.length; i++) {
                    if (gRows.getRow(i).getSelected()) {
                        Count += 1;
                        Status = gRows.getRow(i).getCell(20).getValue();
                        switch (Status)
                        { case "0": break; case "3": break; default: alert(Message.OnlyNoCanModify); return false; break; }
                    }
                }
                if (Count == 0)
                { alert(Message.AtLastOneChoose); return false; }
            }
            document.getElementById("iframeEdit").src = "PCMEvectionApplyEditForm.aspx?BillNo=" + BillNo + "&ProcessFlag=" + ProcessFlag + "&ModuleCode=" + ModuleCode;
            document.getElementById("topTable").style.display = "none";
            document.getElementById("PanelData_Div").style.display = "none";
            document.getElementById("div_2").style.display = "none";
            document.getElementById("divEdit").style.display = "";
            return false;
        }
        function UltraWebGridEvectionApply_DblClickHandler(gridName, cellId) { OpenEdit("Modify"); return 0; }

        $(function() {
            $("#tr_edit").toggle(
            function() { $("#div_1").hide(); $(".img1").attr("src", "../../CSS/Images_new/left_back_03.gif"); },
            function() { $("#div_1").show(); $(".img1").attr("src", "../../CSS/Images_new/left_back_03_a.gif"); })
        });
        $(function() {
            $("#tr_show").toggle(
            function() { $("#div_showdata").hide(); $(".img2").attr("src", "../../CSS/Images_new/left_back_03.gif"); },
            function() { $("#div_showdata").show(); $(".img2").attr("src", "../../CSS/Images_new/left_back_03_a.gif"); })
        });
        $(function() {
            $("#tr_showtd").toggle(
            function() { $("#div_showdata").hide(); $(".img2").attr("src", "../../CSS/Images_new/left_back_03.gif"); },
            function() { $("#div_showdata").show(); $(".img2").attr("src", "../../CSS/Images_new/left_back_03_a.gif"); })
        });
        function reset_click() {
            $("#<%=txtDepName.ClientID %>").val("");
            $("#<%=txtWorkNo.ClientID %>").val("");
            $("#<%=txtLocalName.ClientID %>").val("");
            $("#<%=ddlEvectionType.ClientID %>").val("");
            $("#<%=ddlStatus.ClientID %>").val("");
            $("#<%=txtEvectionTime.ClientID %>").val("");

            $("#<%=txtBillNo.ClientID %>").val("");
            return false;
        }
        function CheckDate() {
            var check = /^\d{4}[\/]\d{2}[\/]\d{2}$/;
            var EffectDate = $("#<%=txtEvectionTime.ClientID%>").val();
            if (EffectDate != null && EffectDate != "") {
                if (!check.test(EffectDate))
                { alert(Message.WrongDate); $("#<%=txtEvectionTime.ClientID%>").val(""); return false; }
            }
            return true;
        }   
	--></script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <asp:HiddenField ID="ImportFlag" Value="1" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
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
                                    <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div_1">
                    <table class="table_data_area" style="width: 100%">
                        <tr style="width: 100%">
                            <td>
                                <table style="width: 100%">
                                    <tr class="tr_data">
                                        <td>
                                            <asp:Panel ID="pnlContent" runat="server">
                                                <table class="table_data_area">
                                                    <tr class="tr_data_1">
                                                        <td width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblBillNo" runat="server">BillNo:</asp:Label>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:TextBox ID="txtBillNo" runat="server" class="input_textBox_1" Width="100%"></asp:TextBox>
                                                            <asp:TextBox ID="txtDepName" runat="server" class="input_textBox_1" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txtDepCode" runat="server" class="input_textBox_1" Style="display: none"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblEvectionTypeApply" runat="server">EvectionType:</asp:Label>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <asp:DropDownList ID="ddlEvectionType" class="input_textBox_2" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtWorkNo" runat="server" class="input_textBox_2" Style="display: none"></asp:TextBox>
                                                            <asp:TextBox ID="txtLocalName" runat="server" class="input_textBox_2" Style="display: none"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="tr_data_2">
                                                        <td width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblEvectionStatus" runat="server">Status:</asp:Label>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:DropDownList ID="ddlStatus" class="input_textBox_1" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblEvectionTime" runat="server">LeaveDate:</asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td width="50%">
                                                                        <asp:TextBox ID="txtEvectionTime" onchange="return CheckDate()" class="input_textBox_1"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            <asp:Panel ID="pnlShowPanel" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnQuery" runat="server" class="button_2" Text="Query" ToolTip="Authority Code:Query"
                                                CommandName="Query" OnClick="btnQuery_Click"></asp:Button>
                                            <asp:Button ID="btnReset" runat="server" class="button_2" Text="Reset" ToolTip="Authority Code:Reset"
                                                CommandName="Reset" OnClientClick="return reset_click();"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" class="button_2" Text="Add" ToolTip="Authority Code:Add"
                                                CommandName="Add" OnClientClick="return OpenEdit('Add')"></asp:Button>
                                            <asp:Button ID="btnModify" runat="server" class="button_2" Text="Modify" ToolTip="Authority Code:Modify"
                                                CommandName="Modify" OnClientClick="return OpenEdit('Modify')"></asp:Button>
                                            <asp:Button ID="btnDelete" runat="server" class="button_2" Text="Delete" ToolTip="Authority Code:Delete"
                                                CommandName="Delete" OnClientClick="return delete_click()" OnClick="btnDelete_Click">
                                            </asp:Button>
                                            <asp:Button ID="btnExport" runat="server" class="button_2" Text="Export" CommandName="Export"
                                                ToolTip="Authority Code:Export" OnClick="btnExport_Click"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Panel ID="PanelAudit" runat="server" Visible="true" Width="300px" Style="padding-right: 3px;
                                                padding-left: 3px; z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px;
                                                background-color: #ffffee; border-right: #0000ff 1px solid; border-top: #0000ff 1px solid;
                                                border-left: #0000ff 1px solid; border-bottom: #0000ff 1px solid; position: absolute;
                                                left: 20%; float: left; display: none">
                                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                    <tr>
                                                        <td width="15%">
                                                            &nbsp;
                                                            <asp:Label ID="lblApprover" runat="server" ismandatory="true"></asp:Label>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:TextBox ID="txtApprover" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="15%">
                                                            &nbsp;
                                                            <asp:Label ID="lblApproveDate" runat="server" ismandatory="true"></asp:Label>
                                                        </td>
                                                        <td width="15%">
                                                            <asp:TextBox ID="txtApproveDate" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnSave" class="button_2" runat="server" Text="Save" ToolTip="Authority Code:Save"
                                                                CommandName="Ok" OnClick="btnSave_Click"></asp:Button>
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Authority Code:Cancel"
                                                                CommandName="Cancel" class="button_2" OnClientClick="return HiddenAudit()"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hidOperate" runat="server" />
                </div>
                <div id="PanelData_Div">
                    <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                            <tr style="width: 100%;">
                                <td style="width: 100%;" id="tr_show" class="tr_title_center">
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
                                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                            ShowMoreButtons="false" DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px"
                                            ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                        </ess:AspNetPager>
                                    </div>
                                </td>
                                <td style="width: 22px;">
                                    <img class="img2" id="tr_showtd" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                </td>
                            </tr>
                        </table>
                        <div id="div_showdata">
                            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                <tr style="width: 100%">
                                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                                        <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19">
                                    </td>
                                    <td background="../../CSS/Images_new/EMP_07.gif" height="19px">
                                    </td>
                                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                                        <img height="18" src="../../CSS/Images_new/EMP_02.gif" width="19">
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td width="19" background="../../CSS/Images_new/EMP_05.gif">
                                        &nbsp;
                                    </td>
                                    <td>

                                        <script language="javascript">                                            document.write("<div id='div_2' style='height:" + document.body.clientHeight * 59 / 100 + "'>");</script>

                                        <igtbl:UltraWebGrid ID="UltraWebGridEvectionApply" runat="server" Width="100%" Height="100%"
                                            OnDataBound="UltraWebGridEvectionApply_DataBound">
                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridEvectionApply" TableLayout="Fixed"
                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                    CssClass="tr_header">
                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                    </BorderDetails>
                                                </HeaderStyleDefault>
                                                <FrameStyle Width="100%" Height="100%">
                                                </FrameStyle>
                                                <ClientSideEvents InitializeLayoutHandler="UltraWebGridEvectionApply_InitializeLayoutHandler"
                                                    AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="UltraWebGridEvectionApply_DblClickHandler">
                                                </ClientSideEvents>
                                                <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                                </SelectedRowStyleDefault>
                                                <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
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
                                                <igtbl:UltraGridBand BaseTableName="gds_att_applyout" Key="gds_att_applyout">
                                                    <Columns>
                                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                            HeaderClickAction="Select" Width="30px" Key="CheckBoxAll" HeaderText="CheckBox">
                                                            <CellTemplate>
                                                                <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                            </CellTemplate>
                                                            <HeaderTemplate>
                                                                <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                            </HeaderTemplate>
                                                            <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                            </Header>
                                                        </igtbl:TemplatedColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="130px"
                                                            HeaderText="ID" Hidden="true">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>" Fixed="True">
                                                                <RowLayoutColumnInfo OriginX="19" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="19" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="100px"
                                                            HeaderText="BillNo">
                                                            <Header Caption="<%$Resources:ControlText,gvBillNo %>" Fixed="True">
                                                                <RowLayoutColumnInfo OriginX="19" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="19" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80px"
                                                            HeaderText="WORKNO">
                                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="True">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                            Width="80px" HeaderText="LocalName">
                                                            <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="True">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="dcode" Key="dcode" IsBound="false" Hidden="True"
                                                            HeaderText="dcode">
                                                            <Header Caption="DCode">
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Hidden="True"
                                                            Width="100px" HeaderText="buName">
                                                            <Header Caption="<%$Resources:ControlText,gvBUName %>">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="60px"
                                                            HeaderText="DEPNAME">
                                                            <Header Caption="<%$Resources:ControlText,gvDepName %>">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTypeName" Key="EvectionTypeName" IsBound="false"
                                                            Width="60px" HeaderText="EvectionTypeName">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionTypeName%>">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionType" Key="EvectionType" Hidden="true"
                                                            IsBound="false" HeaderText="EvectionType">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionTypeName%>">
                                                                <RowLayoutColumnInfo OriginX="17" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="17" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionReason" Key="EvectionReason" IsBound="false"
                                                            Width="100px" HeaderText="EvectionReason">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionReason%>">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTime" Key="EvectionTime" IsBound="false"
                                                            Width="130px" HeaderText="EvectionTime" Format="yyyy/MM/dd HH:mm">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionTime %>">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTel" Key="EvectionTel" IsBound="false"
                                                            Width="80px" HeaderText="EvectionTel">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionTel %>">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionAddress" Key="EvectionAddress" IsBound="false"
                                                            Width="100px" HeaderText="EvectionAddress">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionAddress %>">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTask" Key="EvectionTask" IsBound="false"
                                                            Width="80px" HeaderText="EvectionTask">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionTask %>">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionRoad" Key="EvectionRoad" IsBound="false"
                                                            Width="60px" HeaderText="EvectionRoad">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionRoad%>">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ReturnTime" Key="ReturnTime" IsBound="false"
                                                            Width="130px" HeaderText="ReturnTime" Format="yyyy/MM/dd HH:mm">
                                                            <Header Caption="<%$Resources:ControlText,gvReturnTime%>">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EvectionBy" Key="EvectionBy" IsBound="false"
                                                            Width="50px" HeaderText="EvectionBy">
                                                            <Header Caption="<%$Resources:ControlText,gvEvectionBy%>">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="motorman" Key="motorman" IsBound="false" Width="60px"
                                                            HeaderText="motorman">
                                                            <Header Caption="<%$Resources:ControlText,gvMotorMan%>">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="100px"
                                                            HeaderText="Remark">
                                                            <Header Caption="<%$Resources:ControlText,gvRemark %>">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="True"
                                                            HeaderText="Status">
                                                            <Header Caption="<%$Resources:ControlText,gvStatus %>">
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                            Width="50px" HeaderText="StatusName">
                                                            <Header Caption="<%$Resources:ControlText,gvStatusName %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="approver" Key="approver" IsBound="false" Width="50px"
                                                            HeaderText="approver">
                                                            <Header Caption="<%$Resources:ControlText,gvApprover %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="approvedate" Key="approvedate" IsBound="false"
                                                            Width="50px" HeaderText="approvedate">
                                                            <Header Caption="<%$Resources:ControlText,gvApproverDate %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="auditer" Key="auditer" IsBound="false" Width="50px"
                                                            HeaderText="auditer">
                                                            <Header Caption="<%$Resources:ControlText,gvAuditer %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="auditdate" Key="auditdate" IsBound="false"
                                                            Width="50px" HeaderText="auditdate">
                                                            <Header Caption="<%$Resources:ControlText,gvAuditDate %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="auditidea" Key="auditidea" IsBound="false"
                                                            Width="50px" HeaderText="auditidea">
                                                            <Header Caption="<%$Resources:ControlText,gvAuditIdea %>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="create_user" Key="create_user" IsBound="false"
                                                            Width="70px" HeaderText="create_user">
                                                            <Header Caption="<%$Resources:ControlText,gvCreateUser %>">
                                                                <RowLayoutColumnInfo OriginX="15" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="15" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="create_Date" Key="create_Date" IsBound="false"
                                                            Width="110px" HeaderText="create_Date">
                                                            <Header Caption="<%$Resources:ControlText,gvCreateDate%>">
                                                                <RowLayoutColumnInfo OriginX="16" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="16" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                            Width="65" AllowUpdate="Yes">
                                                            <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                            HeaderClickAction="Select" Width="100px" Key="CheckBoxAll">
                                                            <CellTemplate>
                                                                <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" Text="<%$Resources:ControlText,lb_jindu%>"
                                                                    runat="server"></asp:LinkButton>
                                                            </CellTemplate>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                            </HeaderTemplate>
                                                            <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                            </Header>
                                                        </igtbl:TemplatedColumn>
                                                    </Columns>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                        </igtbl:UltraWebGrid>

                                        <script language="JavaScript" type="text/javascript">                                            document.write("</div>");</script>

                                    </td>
                                    <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="19" background="../../CSS/Images_new/EMP_03.gif" height="18">
                                        &nbsp;
                                    </td>
                                    <td background="../../CSS/Images_new/EMP_08.gif" height="18">
                                        &nbsp;
                                    </td>
                                    <td width="19" background="../../CSS/Images_new/EMP_04.gif" height="18">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <div id="PanelImport_Div">
                    <asp:Panel ID="PanelImport" runat="server" Width="100%" Visible="false">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr style="cursor: hand">
                                <td>
                                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                                        <tr style="width: 100%;" id="tr_showim">
                                            <td style="width: 100%;" class="tr_title_center">
                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                    background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                                    font-size: 13px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblImport" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="tr_table_title" align="right">
                                                <img class="img3" src="../../CSS/Images_new/left_back_03_a.gif">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">        document.write("<div id='divEdit'style='display:none;height:" + document.body.clientHeight * 200 / 100 + "'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">        document.write("</div>");</script>

    </form>

    <script type="text/javascript"><!--
       // document.getElementById("txtWorkNo").focus();
        //document.getElementById("txtWorkNo").select();

        function HiddenAudit() {
            document.all("PanelAudit").style.display = "none";
            document.getElementById("ProcessFlag").value = "";
            return false;
        }
        function delete_click() {
            var grid = igtbl_getGridById('UltraWebGridEvectionApply');
            var gRows = grid.Rows;
            var Count = 0;
            var Status = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGridEvectionApply_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                    Status = gRows.getRow(i).getCell(20).getValue();
                    switch (Status) {
                        case "0":
                            break;
                        case "3":
                            break;
                        default:
                            alert(Message.OnlyNoAudit);
                            return false;
                            break;
                    }
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            if (confirm(Message.DeleteConfirm)) {
                return true;
            }
            else {
                return false;
            }
        }
        function GetSignMap() {
            var grid = igtbl_getGridById('UltraWebGridEvectionApply');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            var Doc = igtbl_getElementById("HiddenID").value;
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("/WorkFlow/SignLogAndMap.aspx?Doc=" +
      Doc, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
        }       
	--></script>

</body>
</html>
