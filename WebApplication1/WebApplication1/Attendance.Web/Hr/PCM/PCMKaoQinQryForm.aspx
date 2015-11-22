<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMKaoQinQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMKaoQinQryForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PCMKaoQinQryForm</title>

<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

<script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    $(function(){
         $("#img_edit,#tr1").toggle(
              function(){
                $("#div_edit").hide();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
       $(function(){
        $("#img_grid,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_grid").hide();
                $("#img_grid").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_grid").show();
                $("#img_grid").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
       });
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="DefaultEmployeeNo" runat="server" name="DefaultEmployeeNo" type="hidden" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr1">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
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
                                                    <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_edit">
                                            <table cellspacing="0" cellpadding="0" width="100%" id="TABLE1">
                                                <tr>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblKQDate" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="28%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtKQDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtKQDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblExceptionType" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="28%">
                                                        <cc1:DropDownCheckList ID="ddlExceptionType" Width="300" RepeatColumns="3" CssClass="input_textBox"
                                                            DropImageSrc="../../CSS/Images/expand.gif" DisplayTextWidth="300" ClientCodeLocation="../PubSC/DropDownCheckList.js"
                                                            runat="server">
                                                        </cc1:DropDownCheckList>
                                                    </td>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblCheckStatus" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="23%">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblShiftNo" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:DropDownList ID="ddlShiftNo" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="100%">
                                                                    <asp:Button ID="btnQuery" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                                                        CommandName="Query" OnClick="btnQuery_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" ToolTip="Authority Code:Reset"
                                                                        CommandName="Reset" OnClick="btnReset_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                                        OnClick="btnExport_Click" CssClass="button_1"></asp:Button>
                                                                    <asp:Panel ID="PanelKQ" runat="server" Visible="true" Width="560" Style="padding-right: 3px;
                                                                        padding-left: 3px; z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px;
                                                                        background-color: #ffffee; border-right: #0000ff 1px solid; border-top: #0000ff 1px solid;
                                                                        border-left: #0000ff 1px solid; border-bottom: #0000ff 1px solid; position: absolute;
                                                                        left: 2%; float: left; display: none">
                                                                        <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="divKQ">
                                                                                                </div>
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tr>
                                                                                                        <td class="td_label" align="center">
                                                                                                            <asp:Button ID="btnClose" runat="server" Text="Close" CommandName="Close" ToolTip="Authority Code:Close"
                                                                                                                OnClientClick="return HiddenShowDetail()" CssClass="button_1"></asp:Button>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
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
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr_edit">
                                                    <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                            background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblGrid" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="tr_title_center" style="width: 300px;">
                                                        <div>
                                                            <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                                HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                                                ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                                            </ess:AspNetPager>
                                                        </div>
                                                    </td>
                                                    <td style="width: 22px;" id="td_show_2">
                                                        <img id="img_grid" class="img2" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="div_grid">
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

                                                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*64/100+"'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGridKaoQinQuery" runat="server" Width="100%" Height="100%"
                                                                >
                                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridKaoQinQuery" TableLayout="Fixed"
                                                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                        CssClass="tr_header">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                        </BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" Height="100%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                                    <igtbl:UltraGridBand BaseTableName="KQM_KaoQinData" Key="KQM_KaoQinData">
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                                                Key="DepName" Width="120">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="true">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                                                Key="WorkNo" Width="70">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="true">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                                                Key="LocalName" Width="60">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="true">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" HeaderText="KQDate" IsBound="false"
                                                                                Key="KQDate" Width="40">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadKQDate %>">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                                                Key="StatusName" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadStatusName %>">
                                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="false"
                                                                                Key="ShiftDesc" Width="150">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc %>">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" HeaderText="OnDutyTime" IsBound="false"
                                                                                Key="OnDutyTime" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOnDutyTime %>">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" HeaderText="OffDutyTime" IsBound="false"
                                                                                Key="OffDutyTime" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOffDutyTime %>">
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTOnDutyTime" AllowUpdate="No" HeaderText="OTOnDutyTime"
                                                                                IsBound="false" Key="OTOnDutyTime" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOTOnDutyTime %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTOffDutyTime" AllowUpdate="No" HeaderText="OTOffDutyTime"
                                                                                IsBound="false" Key="OTOffDutyTime" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOTOffDutyTime %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" HeaderText="AbsentQty" IsBound="false"
                                                                                Key="AbsentQty" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadAbsentQty %>">
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionName" HeaderText="ExceptionName"
                                                                                IsBound="false" Key="ExceptionName" Width="80">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadExceptionName %>">
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="false"
                                                                                Key="Status" Hidden="true">
                                                                                <Header Caption="Status">
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ReasonName" HeaderText="ReasonName" IsBound="false"
                                                                                Key="ReasonName" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonName %>">
                                                                                    <RowLayoutColumnInfo OriginX="11" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="11" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" HeaderText="ReasonRemark" IsBound="false"
                                                                                Key="ReasonRemark" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark %>">
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionType" HeaderText="ExceptionType"
                                                                                IsBound="false" Key="ExceptionType" Hidden="true">
                                                                                <Header Caption="ExceptionName">
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" HeaderText="ShiftNo" IsBound="false"
                                                                                Key="ShiftNo" Hidden="true">
                                                                                <Header Caption="ShiftNo">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>

                                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                                        </td>
                                                        <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                                            &nbsp;
                                                        </td>
                                                        <tr>
                                                        </tr>
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
    </form>

    <script type="text/javascript"><!--  
        function ShowDetail(WorkNo,KQDate,ShiftNo)
		{
		    document.all("PanelKQ").style.display="";
            document.all("PanelKQ").style.top=document.all("ButtonExport").style.top;            
            PCM_PCMKaoQinQryForm.ShowDetail(WorkNo,KQDate,ShiftNo,docallback)
		}		
        function docallback(response)
        {
            document.getElementById('divKQ').innerText=""; 
            if (response.value != null)
                {
　　　　            var ds = response.value;
                    if(ds != null && typeof(ds) == "object" && ds.Tables != null)
	                {
	                    var tblHtml= new   Array();
　　　　                tblHtml[tblHtml.length] =   "<table cellspacing=0 cellpadding=0 width=100%>";
　　　　                tblHtml[tblHtml.length] = "<tr>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label align=center height=25 width=20>No</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=60>"+"1"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=70>"+"2"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+"3"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=90>"+"4"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+"5"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=80>"+"6"+"</td>";
　　　　                tblHtml[tblHtml.length]= '</tr>';
                        for(var i=1; i<=ds.Tables[0].Rows.length; i++)
　　　　                {
　　　　                    var WorkNo=ds.Tables[0].Rows[i-1].WORKNO;
　　　　　　                var LocalName=ds.Tables[0].Rows[i-1].LOCALNAME;
　　　　　　                var CardTime=ds.Tables[0].Rows[i-1].CARDTIME;
　　　　　　                var BellNo=ds.Tables[0].Rows[i-1].BELLNO;
　　　　　　                var ReadTime=ds.Tables[0].Rows[i-1].READTIME==null?"&nbsp;":ds.Tables[0].Rows[i-1].READTIME;
　　　　　　                var CardNo=ds.Tables[0].Rows[i-1].CARDNO;
　　　　　　                tblHtml[tblHtml.length]= "<tr>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label align=center height=25>"+i+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+WorkNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+LocalName+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+CardTime+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+BellNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+ReadTime+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+CardNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="</tr>";
　　　　                }
　　　　                tblHtml[tblHtml.length] ="</table>";
　　　　                document.getElementById("divKQ").innerHTML = tblHtml.join("");
                    }
                }
                else
                {
                    document.all("PanelKQ").style.display="none";
                }             
                return;
        }
		function HiddenShowDetail()
		{
		    document.all("PanelKQ").style.display="none";
		    return false;
		}        
	--></script>

</body>
</html>
