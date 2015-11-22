<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowImportForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowImportForm" %>


<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"  Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" ><%--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>       
    </title>
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonCss.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>
    <script type="text/javascript">
       
     
    </script>
    <style type="text/css">
       body
       {
           text-align:center;
           margin:0px auto;
       }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
      <div style="width:99%;">
           <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_expare" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>            
      </div>
       <div id="div_select" style=" width:99%;">
          <table cellspacing="0" cellpadding="0" class="table_title_area">
             <tr  style="width: 100%;" id="tr_edit">
               <td colspan="2"><a href="../ExcelModel/WFMAuditSample.xls"><asp:Label ID="templateddown" runat="server" Text="Label"></asp:Label></a> <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" Width="300" />
                                            <asp:Button ID="ButtonImportSave" runat="server" Text="ImportSave" OnClick="ButtonImportSave_Click"
                                                 />
                                            <asp:Button ID="ButtonExport" runat="server" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                OnClick="ButtonExport_Click"></asp:Button>
                                            <img id="imgWaiting" src="../CSS/images/clocks.gif" border="0" style="display: none;
                                                height: 20px;" />
                   <asp:Label ID="labeluploadMsg" runat="server" style=" color:Red;"></asp:Label>
                </td>
             </tr>
             <tr>
                <td align="left" colspan="2" style="height: 25;">
                                            &nbsp;<asp:Label ID="lbl_shuomin" runat="server" Text="Label"></asp:Label>
                 </td>

             </tr>
             <tr>
               <td colspan="2" style="vertical-align:top">
               
                 <script language="javascript" type="text/javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*85/100+";'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridImport" TableLayout="Fixed" AutoGenerateColumns="false"
                                                    CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp">
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
                                                    <igtbl:UltraGridBand BaseTableName="WFM_Import" Key="WFM_Import">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="250">
                                                                <Header  Caption="<%$Resources:ControlText,ErrorMsg %>" Fixed="true">
                                                                </Header>
                                                                <CellStyle ForeColor="red">
                                                                </CellStyle>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="orgcode" Key="orgcode" IsBound="false" Width="100">
                                                                <Header  Caption="<%$Resources:ControlText,orgcode %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="orgname" AllowUpdate="No" HeaderText="orgname"
                                                                IsBound="True" Key="orgname" Width="150">
                                                                <Header  Caption="<%$Resources:ControlText,orgname %>"  Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="billtypename" Key="billtypename" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,billtypename %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="Overtimetype" Key="Overtimetype" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,Overtimetype %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="LeaveDays" Key="LeaveDays" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,LeaveDays %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="Shiwei" Key="Shiwei" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,Shiwei %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="Manger" Key="Manger" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,Manger %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="leaveType" Key="leaveType" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,leaveType %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="outtype" Key="outtype" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,outtype %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="outtypeDays" Key="outtypeDays" IsBound="true"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,outtypeDays %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman1" Key="auditman1" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman1 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname1" IsBound="false" Key="localname1"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname1 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename1" IsBound="false" Key="audittypename1"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename1 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype1" IsBound="false" Key="auditmantype1"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype1 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman2" Key="auditman2" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman2 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname2" IsBound="false" Key="localname2"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname2 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename2" IsBound="false" Key="audittypename2"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename2 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype2" IsBound="false" Key="auditmantype2"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype2 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman3" Key="auditman3" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman3 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname3" IsBound="false" Key="localname3"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname3 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename3" IsBound="false" Key="audittypename3"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename3 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype3" IsBound="false" Key="auditmantype3"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype3 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman4" Key="auditman4" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman4 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname4" IsBound="True" Key="localname4"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname4 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename4" IsBound="false" Key="audittypename4"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename4 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype4" IsBound="false" Key="auditmantype4"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype4 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman5" Key="auditman5" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman5 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname5" Key="localname5" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname5 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename5" Key="audittypename5" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename5 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype5" IsBound="false" Key="auditmantype5"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype5 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman6" Key="auditman6" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman6 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname6" Key="localname6" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname6 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename6" Key="audittypename6" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename6 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype6" IsBound="false" Key="auditmantype6"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype6 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman7" Key="auditman7" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman7 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname7" Key="localname7" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname7 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename7" Key="audittypename7" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename7 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype7" IsBound="false" Key="auditmantype7"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype7 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman8" Key="auditman8" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman8 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname8" Key="localname8" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname8 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename8" Key="audittypename8" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename8 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype8" IsBound="false" Key="auditmantype8"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype8 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman9" Key="auditman9" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman9 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname9" Key="localname9" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname9 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename9" Key="audittypename9" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename9 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype9" IsBound="false" Key="auditmantype9"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype9 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman10" Key="auditman10" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman10 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname10" Key="localname10" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname10 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename10" Key="audittypename10" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename10 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype10" IsBound="false" Key="auditmantype10"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype10 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman11" Key="auditman11" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman11 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname11" Key="localname11" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname11 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename11" Key="audittypename11" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename11 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype11" IsBound="false" Key="auditmantype11"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype11 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman12" Key="auditman12" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman12 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname12" Key="localname12" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname12 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename12" Key="audittypename12" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename12 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype12" IsBound="false" Key="auditmantype12"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype12 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman13" Key="auditman13" IsBound="false"
                                                                Width="70">
                                                                  <Header Caption="<%$Resources:ControlText,auditman13 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname13" Key="localname13" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname13 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename13" Key="audittypename13" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename13 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype13" IsBound="false" Key="auditmantype13"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype13 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman14" Key="auditman14" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman14 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname14" Key="localname14" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname14 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename14" Key="audittypename14" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename14 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype14" IsBound="false" Key="auditmantype14"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype14 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditman15" Key="auditman15" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman15 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname15" Key="localname15" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname15 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename15" Key="audittypename15" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename15 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype15" IsBound="false" Key="auditmantype15"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype15 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="auditman16" Key="auditman16" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman16 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname16" Key="localname16" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname16 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename16" Key="audittypename16" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename16 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype16" IsBound="false" Key="auditmantype16"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype16 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="auditman17" Key="auditman17" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman17 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname17" Key="localname17" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname17 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename17" Key="audittypename17" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename17 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype17" IsBound="false" Key="auditmantype17"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype17 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="auditman18" Key="auditman18" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman18 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname18" Key="localname18" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname18 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename18" Key="audittypename18" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename18 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype18" IsBound="false" Key="auditmantype18"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype18 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="auditman19" Key="auditman19" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman19 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname19" Key="localname19" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname19 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename19" Key="audittypename19" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename19 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype19" IsBound="false" Key="auditmantype19"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype19 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:UltraGridColumn BaseColumnName="auditman20" Key="auditman20" IsBound="false"
                                                                Width="70">
                                                                <Header Caption="<%$Resources:ControlText,auditman20 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="localname20" Key="localname20" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,localname20 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="audittypename20" Key="audittypename20" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,audittypename20 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="auditmantype20" IsBound="false" Key="auditmantype20"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,auditmantype20 %>" >
                                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                
               </td>
             </tr>
          </table>
          
          <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server">
        </igtblexp:UltraWebGridExcelExporter>
       </div>
      
    </form>
</body>
</html>
