<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTReporter.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WFReporter.OTReporter" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>加班统计表</title>
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
      $(function(){
        $("#tr_edit").toggle(
            function(){$("#div_1").hide(); $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_1").show();$(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
       });
    $(function(){
       $("#tr_show").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); } ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
   });
   --></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server">
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
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area" style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr class="tr_data_1">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblYearMonth" runat="server">YearMonth:</asp:Label>
                                                </td>
                                                <td>
                                                    <igtxt:WebDateTimeEdit ID="txtYearMonth" class="input_textBox_1" runat="server" EditModeFormat="yyyy/MM">
                                                    </igtxt:WebDateTimeEdit>
                                                </td>
                                                <td style="width: 300px">
                                                    &nbsp;
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnQuery" class="button_1" runat="server" OnClick="btnQuery_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnExport" class="button_1" runat="server" OnClick="btnExport_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
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
                        <asp:Label ID="OTMmsg" runat="server"></asp:Label>
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridOTMMonthTotal" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridOTMMonthTotal_DataBound" OnInitializeLayout="UltraWebGridOTMMonthTotal_InitializeLayout">
                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMMonthTotal" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                    <HeaderStyleDefault Height="25px" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridOTMMonthTotal_InitializeLayoutHandler"
                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="OTM_MonthTotal" Key="OTM_MonthTotal">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="BuName" HeaderText="BuName" IsBound="false"
                                                Key="BuName" Width="100px" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvBuOTMQryName%>" Fixed="True" >
                                                    <RowLayoutColumnInfo OriginX="1"    />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1"   />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                           
                                            <igtbl:UltraGridColumn BaseColumnName="depcode" HeaderText="depcode" IsBound="false"
                                                Key="depcode" Width="120px" Hidden="True">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1"  />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="dname" HeaderText="DName" IsBound="false"
                                                Key="dname" Width="120px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvDName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1"  />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1"  />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1"  />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="100px"
                                                HeaderText="BillNo" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvBillNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                Key="OverTimeType" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvOverTimeType %>" Fixed="True" >
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            
                                            <igtbl:UltraGridColumn BaseColumnName="Day1" HeaderText="1" IsBound="false" Key="Day1"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="1">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day2" HeaderText="2" IsBound="false" Key="Day2"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="2">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day3" HeaderText="3" IsBound="false" Key="Day3"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="3">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day4" HeaderText="4" IsBound="false" Key="Day4"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="4">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day5" HeaderText="5" IsBound="false" Key="Day5"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="5">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day6" HeaderText="6" IsBound="false" Key="Day6"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="6">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day7" HeaderText="7" IsBound="false" Key="Day7"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="7">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day8" HeaderText="8" IsBound="false" Key="Day8"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="8">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day9" HeaderText="9" IsBound="false" Key="Day9"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="9">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day10" HeaderText="10" IsBound="false" Key="Day10"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="10">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day11" HeaderText="11" IsBound="false" Key="Day11"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="11">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day12" HeaderText="12" IsBound="false" Key="Day12"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="12">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day13" HeaderText="13" IsBound="false" Key="Day13"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="13">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day14" HeaderText="14" IsBound="false" Key="Day14"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="14">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day15" HeaderText="15" IsBound="false" Key="Day15"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="15">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day16" HeaderText="16" IsBound="false" Key="Day16"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="16">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day17" HeaderText="17" IsBound="false" Key="Day17"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="17">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day18" HeaderText="18" IsBound="false" Key="Day18"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="18">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day19" HeaderText="19" IsBound="false" Key="Day19"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="19">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day20" HeaderText="20" IsBound="false" Key="Day20"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="20">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day21" HeaderText="21" IsBound="false" Key="Day21"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="21">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day22" HeaderText="22" IsBound="false" Key="Day22"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="22">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day23" HeaderText="23" IsBound="false" Key="Day23"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="23">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day24" HeaderText="24" IsBound="false" Key="Day24"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="24">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day25" HeaderText="25" IsBound="false" Key="Day25"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="25">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day26" HeaderText="26" IsBound="false" Key="Day26"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="26">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day27" HeaderText="27" IsBound="false" Key="Day27"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="27">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day28" HeaderText="28" IsBound="false" Key="Day28"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="28">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day29" HeaderText="29" IsBound="false" Key="Day29"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="29">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day30" HeaderText="30" IsBound="false" Key="Day30"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="30">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day31" HeaderText="31" IsBound="false" Key="Day31"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="31">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay1" HeaderText="1" IsBound="false" Key="SpecDay1"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay2" HeaderText="2" IsBound="false" Key="SpecDay2"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay3" HeaderText="3" IsBound="false" Key="SpecDay3"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay4" HeaderText="4" IsBound="false" Key="SpecDay4"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay5" HeaderText="5" IsBound="false" Key="SpecDay5"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay6" HeaderText="6" IsBound="false" Key="SpecDay6"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay7" HeaderText="7" IsBound="false" Key="SpecDay7"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay8" HeaderText="8" IsBound="false" Key="SpecDay8"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay9" HeaderText="9" IsBound="false" Key="SpecDay9"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay10" HeaderText="10" IsBound="false"
                                                Key="SpecDay10" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay11" HeaderText="11" IsBound="false"
                                                Key="SpecDay11" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay12" HeaderText="12" IsBound="false"
                                                Key="SpecDay12" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay13" HeaderText="13" IsBound="false"
                                                Key="SpecDay13" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay14" HeaderText="14" IsBound="false"
                                                Key="SpecDay14" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay15" HeaderText="15" IsBound="false"
                                                Key="SpecDay15" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay16" HeaderText="16" IsBound="false"
                                                Key="SpecDay16" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay17" HeaderText="17" IsBound="false"
                                                Key="SpecDay17" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay18" HeaderText="18" IsBound="false"
                                                Key="SpecDay18" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay19" HeaderText="19" IsBound="false"
                                                Key="SpecDay19" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay20" HeaderText="20" IsBound="false"
                                                Key="SpecDay20" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay21" HeaderText="21" IsBound="false"
                                                Key="SpecDay21" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay22" HeaderText="22" IsBound="false"
                                                Key="SpecDay22" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay23" HeaderText="23" IsBound="false"
                                                Key="SpecDay23" Hidden="true" Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay24" HeaderText="24" IsBound="false"
                                                Key="SpecDay24" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay25" HeaderText="25" IsBound="false"
                                                Key="SpecDay25" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay26" HeaderText="26" IsBound="false"
                                                Key="SpecDay26" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay27" HeaderText="27" IsBound="false"
                                                Key="SpecDay27" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay28" HeaderText="28" IsBound="false"
                                                Key="SpecDay28" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay29" HeaderText="29" IsBound="false"
                                                Key="SpecDay29" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay30" HeaderText="30" IsBound="false"
                                                Key="SpecDay30" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay31" HeaderText="31" IsBound="false"
                                                Key="SpecDay31" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1Apply" HeaderText="G1Apply" IsBound="false"
                                                Key="G1Apply" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Apply" HeaderText="G2Apply" IsBound="false"
                                                Key="G2Apply" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3Apply" HeaderText="G3Apply" IsBound="false"
                                                Key="G3Apply" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1RelSalary" HeaderText="G1RelSalary" IsBound="false"
                                                Key="G1RelSalary" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                Key="G2RelSalary" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3RelSalary" HeaderText="G3RelSalary" IsBound="false"
                                                Key="G3RelSalary" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                           
                                           <%-- <igtbl:UltraGridColumn BaseColumnName="G2Remain" HeaderText="G2Remain" IsBound="false"
                                                Key="G2Remain" Width="80">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Remain %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>--%>
                                            <%--<igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="false"
                                                Key="MAdjust1" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadMAdjust1 %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MRelAdjust" HeaderText="MRelAdjust" IsBound="false"
                                                Key="MRelAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadMRelAdjust %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AdvanceAdjust" HeaderText="AdvanceAdjust"
                                                IsBound="false" Key="AdvanceAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvAdvanceAdjust %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="RestAdjust" HeaderText="RestAdjust" IsBound="false"
                                                Key="RestAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvRestAdjust %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlagName" HeaderText="ApproveFlagName"
                                                IsBound="false" Key="ApproveFlagName" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadApproveFlagName %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" HeaderText="ApproveFlag" IsBound="false"
                                                Key="ApproveFlag" Width="50px" Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="APPROVER" Key="auditer" IsBound="false" Width="60px"
                                                HeaderText="auditer">
                                                <Header Caption="<%$Resources:ControlText,gvAuditer %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="APPROVEDATE" Key="auditdate" IsBound="false"
                                                Width="150px" HeaderText="auditdate">
                                                <Header Caption="<%$Resources:ControlText,gvAuditDate %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="APREMARK" Key="auditidea" IsBound="false"
                                                Width="100px" HeaderText="auditidea">
                                                <Header Caption="<%$Resources:ControlText,gvAuditIdea %>">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" IsBound="false" Key="ApproveFlag"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="ApproveFlag">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="YearMonth" IsBound="false" Key="YearMonth"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="YearMonth">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>--%>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
