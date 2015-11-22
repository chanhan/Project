<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowBillChangeAuditFlowForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowBillChangeAuditFlowForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>WorkFlowBillChangeAuditFlowForm</title>
    <base target="_self" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="css/MyStyles.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        //<!--
        function MakeImgFlow(tp) {
            var myrow = igtbl_getActiveRow("UltraWebGridBill");
            if (myrow == null) {
                alert("<%=Resources.Message.common_message_data_select %>");
                return;
            }
            var grid = igtbl_getGridById('UltraWebGridBill');
            var rows = grid.Rows;
            var index = myrow.getIndex();
            var cindex;
            var cnt = rows.length;
            if (myrow.getCellFromKey("AuditStatus").getValue() != "0") {
                alert("<%=Resources.Message.common_message_checkmovestate %>");
                return;
            }
            if (tp == 1) {
                if (index == 0) {
                    return;
                }
                cindex = index - 1;

            }
            else if (tp == 2) {
                if (index == rows.length - 1) {
                    return;
                }
                cindex = index + 1;
            }
            var tmp;
            var crow = rows.getRow(cindex);
            if (crow.getCellFromKey("AuditStatus").getValue() != "0") {
                alert("<%=Resources.Message.common_message_checkmovestate %>");
                return;
            }
            var ccell;
            var cell;

            for (i = 0; i < 7; i++) {
                ccell = crow.getCell(i);
                cell = myrow.getCell(i);
                tmp = cell.getValue();
                cell.setValue(ccell.getValue());
                ccell.setValue(tmp);
            }
            crow.setSelected(true);
            igtbl_setActiveRow("UltraWebGridBill", igtbl_getElementById("UltraWebGridBill_r_" + cindex));
        }
        //刪除數據
        function Delete() {
            var selRow = igtbl_getActiveRow("UltraWebGridBill");
            if (selRow == null) {
                alert("<%=Resources.Message.common_message_data_select %>");
                return false;
            }
            else {
                //			    igtbl_getGridById("UltraWebGridBill").AllowDelete=1;
                //			    igtbl_deleteRow("UltraWebGridBill","UltraWebGridBill_r_"+selRow.getIndex());
                selRow.setSelected(true);
                igtbl_setActiveRow("UltraWebGridBill", igtbl_getElementById("UltraWebGridBill_r_" + selRow.getIndex()));
            }
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    if (gRows.getRow(i).getCellFromKey("AuditStatus").getValue() != "0") {
                        alert("<%=Resources.Message.common_message_checkdeletestate %>");
                        return false;
                    }
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert("<%=Resources.Message.common_message_data_select %>");
                return false;
            }
            return true;
        }

        //保存數據
        function Save() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                Count += 1;
            }
            if (Count == 0) {
                alert("<%=Resources.Message.noauditman %>");
                return false;
            }
            if (confirm("<%=Resources.Message.DataReturn%>")) {
                FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        //增加數據
        function Add() {
            var grid = igtbl_getGridById('UltraWebGridAudit');
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
            return true;
        }

        //調用放大鏡
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

        //收縮模塊控制
        $(function() {
            $("#tr_edit").toggle
            (
                function() {
                    $("#div_1").hide();

                },
                function() {
                    $("#div_1").show();

                }
            )
            $(function() {
                $("#trselect").toggle
           (
            function() {
                $("#div_5").hide();
            },
            function() {
                $("#div_5").show();
            }
           )
            });
        });

        //-->
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server" />
    <table class="top_table" cellspacing="1" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand" onclick="turnit('div_1','div_img_1','<%=sAppPath%>');">
                        <td class="tr_table_title" colspan="2">
                            <div style="width: 100%;">
                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;" id="tr_edit">
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
                                            <div id="Div1">
                                                <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_1">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="td_label" width="100%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Button ID="Btn_save" runat="server" Text="Save" ToolTip="Authority Code:Save"
                                                            BorderWidth="0px" CssClass="input_linkbutton WF_input_Button2" OnClientClick="return Save();"
                                                            OnClick="ButtonSave_Click"></asp:Button>
                                                        <asp:Button ID="ButtonDelete" runat="server" Text="   -   " ToolTip="Authority Code:Delete"
                                                            OnClientClick="return Delete();" BorderWidth="0px" CssClass="input_linkbutton WF_input_Button2"
                                                            OnClick="ButtonDelete_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label">

                                                        <script language="javascript">                                                            document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>

                                                        <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%">
                                                            <DisplayLayout Name="UltraWebGridBill" CompactRendering="False" RowHeightDefault="20px"
                                                                Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                                AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                                AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="CellSelect"
                                                                StationaryMargins="HeaderAndFooter">
                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderColor="#6699ff" HorizontalAlign="Left"
                                                                    CssClass="tr_header">
                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                    </BorderDetails>
                                                                </HeaderStyleDefault>
                                                                <FrameStyle Width="100%" Height="100%">
                                                                </FrameStyle>
                                                                <ClientSideEvents></ClientSideEvents>
                                                                <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
                                                                </SelectedRowStyleDefault>
                                                                <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                                                </RowAlternateStyleDefault>
                                                                
                                                                <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                    CssClass="tr_data">
                                                                    <Padding Left="3px"></Padding>
                                                                    <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                </RowStyleDefault>
                                                            </DisplayLayout>
                                                            <Bands>
                                                                <igtbl:UltraGridBand>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="AuditMan" Key="AuditMan" IsBound="false" Width="15%"
                                                                            AllowUpdate="No">
                                                                            <Header Caption="<%$Resources:ControlText,gvgvWorkNo %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                            Width="15%" AllowUpdate="No">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadApproverName %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="AuditType" Key="AuditType" IsBound="false"
                                                                            Hidden="true" Type="Custom" EditorControlID="ddlAuditType" AllowUpdate="Yes">
                                                                            <Header Caption="<%$Resources:ControlText,auditmantype1 %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="AuditManType" Key="AuditManType" IsBound="false"
                                                                            Hidden="true" Type="Custom" EditorControlID="ddlAuditManType" AllowUpdate="Yes">
                                                                            <Header Caption="<%$Resources:ControlText,auditmantype1 %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Notes" Key="Notes" IsBound="false" Width="30%"
                                                                            AllowUpdate="No">
                                                                            <Header Caption="Notes">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ManagerName" Key="ManagerName" IsBound="false"
                                                                            Width="15%" AllowUpdate="No">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                            Width="15%" AllowUpdate="No">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadStatus %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SendNotes" Key="SendNotes" IsBound="false"
                                                                            Width="10%" AllowUpdate="No">
                                                                            <Header Caption="<%$Resources:ControlText,ButtonSendNotes %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="AuditStatus" Key="AuditStatus" IsBound="false"
                                                                            Hidden="true">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadStatus %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>

                                                        <script language="JavaScript" type="text/javascript">                                                            document.write("</DIV>");</script>

                                                        <igcmbo:WebCombo ID="ddlAuditType" OnInitializeDataSource="ddlAuditType_OnInitializeDataSource"
                                                            CssClass="input_ddl" runat="server" Version="4.00" Width="100%" ComboTypeAhead="Suggest">
                                                            <DropDownLayout ColHeadersVisible="No" RowSelectors="No" ColFootersVisible="No" DropdownWidth="150px"
                                                                BorderCollapse="Separate" RowHeightDefault="23px" DropdownHeight="200px" TableLayout="Fixed"
                                                                StationaryMargins="Header" BaseTableName="HRM_FixedlType" Version="4.00" AutoGenerateColumns="true">
                                                                <RowAlternateStyle Cursor="Hand" CssClass="tr_data1">
                                                                </RowAlternateStyle>
                                                                <FrameStyle Width="100%" Height="150px">
                                                                </FrameStyle>
                                                                <RowStyle Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                    CssClass="tr_data">
                                                                    <Padding Left="3px" Right="3px"></Padding>
                                                                    <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                </RowStyle>
                                                                <SelectedRowStyle ForeColor="Black" BackgroundImage="../CSS/Images/overbg.bmp"></SelectedRowStyle>
                                                            </DropDownLayout>
                                                            <ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DataValue" HeaderText="DataValue" IsBound="false"
                                                                    Width="100%" Key="DataValue">
                                                                    <Header Caption="DataValue">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DataCode" HeaderText="DataCode" IsBound="false"
                                                                    Key="DataCode" Hidden="true">
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igcmbo:WebCombo>
                                                        <igcmbo:WebCombo ID="ddlAuditManType" OnInitializeDataSource="ddlAuditManType_OnInitializeDataSource"
                                                            CssClass="input_ddl" runat="server" Version="4.00" Width="100%" ComboTypeAhead="Suggest">
                                                            <DropDownLayout ColHeadersVisible="No" RowSelectors="No" ColFootersVisible="No" DropdownWidth="150px"
                                                                BorderCollapse="Separate" RowHeightDefault="23px" DropdownHeight="200px" TableLayout="Fixed"
                                                                StationaryMargins="Header" BaseTableName="HRM_FixedlType" Version="4.00" AutoGenerateColumns="true">
                                                                <RowAlternateStyle Cursor="Hand" CssClass="tr_data1">
                                                                </RowAlternateStyle>
                                                                <FrameStyle Width="100%" Height="150px">
                                                                </FrameStyle>
                                                                <RowStyle Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                    CssClass="tr_data">
                                                                    <Padding Left="3px" Right="3px"></Padding>
                                                                    <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                </RowStyle>
                                                                <SelectedRowStyle ForeColor="Black" BackgroundImage="../CSS/Images/overbg.bmp"></SelectedRowStyle>
                                                            </DropDownLayout>
                                                            <ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DataValue" HeaderText="DataValue" IsBound="false"
                                                                    Width="100%" Key="DataValue">
                                                                    <Header Caption="DataValue">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DataCode" HeaderText="DataCode" IsBound="false"
                                                                    Key="DataCode" Hidden="true">
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igcmbo:WebCombo>
                                                    </td>
                                                    <td class="td_label" width="10">
                                                        <p>
                                                            <img id="imgUp" style="cursor: hand" onclick="MakeImgFlow(1)" alt="" src="../CSS/Images/up.bmp"></p>
                                                        <p>
                                                            &nbsp;</p>
                                                        <p>
                                                            <img id="imgDown" style="cursor: hand" onclick="MakeImgFlow(2)" alt="" src="../CSS/Images/down.bmp"></p>
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
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand" onclick="turnit('div_5','div_img_5','<%=sAppPath%>');">
                        <td class="tr_table_title" colspan="2">
                            <div style="width: 100%;">
                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;" id="trselect">
                                        <td style="width: 100%;" class="tr_title_center">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_select_1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 22px;">
                                            <div id="Div3">
                                                <img id="img3" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_5">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                            <asp:Label ID="lbl_Unit" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="27%">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="textBoxDepName" runat="server" Width="80%"></asp:TextBox>
                                                        <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                        <asp:Image ID="ImageDepCode" runat="server" Style="cursor: pointer;" class="img_hidden"
                                                            ImageUrl="../CSS/Images_new/search_new.gif"></asp:Image>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                            <asp:Label ID="lbl_empno" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="20%">
                                            <asp:TextBox ID="textBoxWorkNo" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                            <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="23%">
                                            <asp:TextBox ID="textBoxLocalName" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" colspan="6">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Button ID="ButtonQuery" runat="server" BorderWidth="0" Text="Query" ToolTip="Authority Code:Query"
                                                            CommandName="Query" CssClass="input_linkbutton WF_input_Button2" OnClick="ButtonQuery_Click">
                                                        </asp:Button>
                                                        <asp:Button ID="Btn_Add" runat="server" Text="Add" BorderWidth="0" ToolTip="Authority Code:Add"
                                                            CommandName="Add" OnClientClick="return Add();" CssClass="input_linkbutton WF_input_Button2"
                                                            OnClick="ButtonAdd_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" colspan="6">

                                            <script language="javascript">                                                document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 43 / 100 + "'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridAudit" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                    AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                    AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect"
                                                    StationaryMargins="HeaderAndFooter">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderColor="#6699ff" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
                                                    <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                        CssClass="tr_data">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand BaseTableName="gds_att_employee" Key="gds_att_employee">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="depname" Key="depname" IsBound="false" Width="30%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadDept%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="workno" Key="workno" IsBound="false" Width="15%">
                                                                <Header Caption="<%$Resources:ControlText,gvheadempno %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname" Key="localname" IsBound="false"
                                                                Width="15%">
                                                                <Header Caption="<%$Resources:ControlText,gvheadsignname %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="notes" Key="notes" IsBound="false" Width="30%">
                                                                <Header Caption="<%$Resources:ControlText,Notes %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="managername" Key="managername" IsBound="false"
                                                                Width="10%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
