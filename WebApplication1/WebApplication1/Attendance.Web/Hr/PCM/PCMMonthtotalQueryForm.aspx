<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMMonthtotalQueryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMMonthtotalQueryForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMMonthTotal</title>
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
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
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
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblYearMonth" runat="server">YearMonth:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="10%">
                                                        <igtxt:WebDateTimeEdit ID="txtYearMonth" runat="server" EditModeFormat="yyyy/MM"
                                                            CssClass="input_textBox" Width="100%">
                                                        </igtxt:WebDateTimeEdit>
                                                    </td>
                                                    <td class="td_label" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="6">
                                                        <table>
                                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnQuery" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                                                            CommandName="Query" OnClick="btnQuery_Click" CssClass="button_1"></asp:Button>
                                                                        <asp:Button ID="btnReset" runat="server" Text="Reset" ToolTip="Authority Code:Reset"
                                                                            CommandName="Reset" OnClick="btnReset_Click" CssClass="button_1"></asp:Button>
                                                                    </td>
                                                                    <td class="td_seperator">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                                            OnClick="btnExport_Click" CssClass="button_1"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
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

                                                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*70/100+"'>");</script>

                                                        <igtbl:UltraWebGrid ID="UltraWebGridOTMMonthTotal" runat="server" Width="100%" Height="100%"
                                                            OnDataBound="UltraWebGridOTMMonthTotal_DataBound" OnInitializeLayout="UltraWebGridOTMMonthTotal_InitializeLayout">
                                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMMonthTotal" TableLayout="Fixed"
                                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                    CssClass="tr_header" Height="20px">
                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                    </BorderDetails>
                                                                </HeaderStyleDefault>
                                                                <FrameStyle Width="100%" Height="100%">
                                                                </FrameStyle>
                                                                <ClientSideEvents InitializeLayoutHandler="UltraWebGridOTMMonthTotal_InitializeLayoutHandler"
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
                                                                <igtbl:UltraGridBand BaseTableName="OTM_MonthTotal" Key="OTM_MonthTotal">
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Total" HeaderText="Total" IsBound="false"
                                                                            Key="Total" Hidden="true">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="Total" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                                            Key="DepName" Width="120px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="True">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                                            Key="WorkNo" Width="70px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="True">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                                            Key="LocalName" Width="70px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                                            Key="OverTimeType" Width="60px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadOverTimeType %>">
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G1Apply" HeaderText="G1Apply" IsBound="false"
                                                                            Key="G1Apply" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G2Apply" HeaderText="G2Apply" IsBound="false"
                                                                            Key="G2Apply" Width="95px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G3Apply" HeaderText="G3Apply" IsBound="false"
                                                                            Key="G3Apply" Width="105px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G1UPLMT" HeaderText="G1UPLMT" IsBound="false"
                                                                            Key="G1UPLMT" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G2UPLMT" HeaderText="G2UPLMT" IsBound="false"
                                                                            Key="G2UPLMT" Width="95px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G12UPLMT" HeaderText="G12UPLMT" IsBound="false"
                                                                            Key="G12UPLMT" Width="120px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG12UPLMT %>">
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G3UPLMT" HeaderText="G3UPLMT" IsBound="false"
                                                                            Key="G3UPLMT" Width="105px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG3Apply  %>">
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G1RelSalary" HeaderText="G1RelSalary" IsBound="false"
                                                                            Key="G1RelSalary" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                                            Key="G2RelSalary" Width="95px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G3RelSalary" HeaderText="G3RelSalary" IsBound="false"
                                                                            Key="G3RelSalary" Width="105px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="13" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="13" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG1Apply" HeaderText="SpecG1Apply" IsBound="false"
                                                                            Key="SpecG1Apply" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG2Apply" HeaderText="SpecG2Apply" IsBound="false"
                                                                            Key="SpecG2Apply" Width="95px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG3Apply" HeaderText="SpecG3Apply" IsBound="false"
                                                                            Key="SpecG3Apply" Width="105px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG1Salary" HeaderText="SpecG1Salary" IsBound="false"
                                                                            Key="SpecG1Salary" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG2Salary" HeaderText="SpecG2Salary" IsBound="false"
                                                                            Key="SpecG2Salary" Width="95px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="SpecG3Salary" HeaderText="SpecG3Salary" IsBound="false"
                                                                            Key="SpecG3Salary" Width="105px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                                                <RowLayoutColumnInfo OriginX="13" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="13" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G2Remain" HeaderText="G2Remain" IsBound="false"
                                                                            Key="G2Remain" Width="80">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadG2Remain %>">
                                                                                <RowLayoutColumnInfo OriginX="14" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="14" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="false"
                                                                            Key="MAdjust1" Width="60px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMAdjust1 %>">
                                                                                <RowLayoutColumnInfo OriginX="14" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="14" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="MRelAdjust" HeaderText="MRelAdjust" IsBound="false"
                                                                            Key="MRelAdjust" Width="60px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMRelAdjust %>">
                                                                                <RowLayoutColumnInfo OriginX="15" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="15" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ApproveFlagName" HeaderText="ApproveFlagName"
                                                                            IsBound="false" Key="ApproveFlagName" Width="50px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadApproveFlagName %>">
                                                                                <RowLayoutColumnInfo OriginX="16" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="16" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" IsBound="false" Key="ApproveFlag"
                                                                            Hidden="true">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="ApproveFlag">
                                                                                <RowLayoutColumnInfo OriginX="48" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="48" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="YearMonth" IsBound="false" Key="YearMonth"
                                                                            Hidden="true">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="YearMonth">
                                                                                <RowLayoutColumnInfo OriginX="48" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="48" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day1" HeaderText="1" IsBound="false" Key="Day1"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="1">
                                                                                <RowLayoutColumnInfo OriginX="17" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="17" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day2" HeaderText="2" IsBound="false" Key="Day2"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="2">
                                                                                <RowLayoutColumnInfo OriginX="18" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="18" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day3" HeaderText="3" IsBound="false" Key="Day3"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="3">
                                                                                <RowLayoutColumnInfo OriginX="19" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="19" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day4" HeaderText="4" IsBound="false" Key="Day4"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="4">
                                                                                <RowLayoutColumnInfo OriginX="20" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="20" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day5" HeaderText="5" IsBound="false" Key="Day5"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="5">
                                                                                <RowLayoutColumnInfo OriginX="21" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="21" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day6" HeaderText="6" IsBound="false" Key="Day6"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="6">
                                                                                <RowLayoutColumnInfo OriginX="22" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="22" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day7" HeaderText="7" IsBound="false" Key="Day7"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="7">
                                                                                <RowLayoutColumnInfo OriginX="23" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="23" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day8" HeaderText="8" IsBound="false" Key="Day8"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="8">
                                                                                <RowLayoutColumnInfo OriginX="24" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="24" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day9" HeaderText="9" IsBound="false" Key="Day9"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="9">
                                                                                <RowLayoutColumnInfo OriginX="25" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="25" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day10" HeaderText="10" IsBound="false" Key="Day10"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="10">
                                                                                <RowLayoutColumnInfo OriginX="26" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="26" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day11" HeaderText="11" IsBound="false" Key="Day11"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="11">
                                                                                <RowLayoutColumnInfo OriginX="27" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="27" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day12" HeaderText="12" IsBound="false" Key="Day12"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="12">
                                                                                <RowLayoutColumnInfo OriginX="28" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="28" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day13" HeaderText="13" IsBound="false" Key="Day13"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="13">
                                                                                <RowLayoutColumnInfo OriginX="29" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="29" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day14" HeaderText="14" IsBound="false" Key="Day14"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="14">
                                                                                <RowLayoutColumnInfo OriginX="30" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="30" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day15" HeaderText="15" IsBound="false" Key="Day15"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="15">
                                                                                <RowLayoutColumnInfo OriginX="31" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="31" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day16" HeaderText="16" IsBound="false" Key="Day16"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="16">
                                                                                <RowLayoutColumnInfo OriginX="32" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="32" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day17" HeaderText="17" IsBound="false" Key="Day17"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="17">
                                                                                <RowLayoutColumnInfo OriginX="33" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="33" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day18" HeaderText="18" IsBound="false" Key="Day18"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="18">
                                                                                <RowLayoutColumnInfo OriginX="34" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="34" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day19" HeaderText="19" IsBound="false" Key="Day19"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="19">
                                                                                <RowLayoutColumnInfo OriginX="35" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="35" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day20" HeaderText="20" IsBound="false" Key="Day20"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="20">
                                                                                <RowLayoutColumnInfo OriginX="36" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="36" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day21" HeaderText="21" IsBound="false" Key="Day21"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="21">
                                                                                <RowLayoutColumnInfo OriginX="37" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="37" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day22" HeaderText="22" IsBound="false" Key="Day22"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="22">
                                                                                <RowLayoutColumnInfo OriginX="38" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="38" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day23" HeaderText="23" IsBound="false" Key="Day23"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="23">
                                                                                <RowLayoutColumnInfo OriginX="39" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="39" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day24" HeaderText="24" IsBound="false" Key="Day24"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="24">
                                                                                <RowLayoutColumnInfo OriginX="40" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="40" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day25" HeaderText="25" IsBound="false" Key="Day25"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="25">
                                                                                <RowLayoutColumnInfo OriginX="41" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="41" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day26" HeaderText="26" IsBound="false" Key="Day26"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="26">
                                                                                <RowLayoutColumnInfo OriginX="42" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="42" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day27" HeaderText="27" IsBound="false" Key="Day27"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="27">
                                                                                <RowLayoutColumnInfo OriginX="43" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="43" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day28" HeaderText="28" IsBound="false" Key="Day28"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="28">
                                                                                <RowLayoutColumnInfo OriginX="44" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="44" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day29" HeaderText="29" IsBound="false" Key="Day29"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="29">
                                                                                <RowLayoutColumnInfo OriginX="45" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="45" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day30" HeaderText="30" IsBound="false" Key="Day30"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="30">
                                                                                <RowLayoutColumnInfo OriginX="46" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="46" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Day31" HeaderText="31" IsBound="false" Key="Day31"
                                                                            Width="30px">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="31">
                                                                                <RowLayoutColumnInfo OriginX="47" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="47" />
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript"><!--
        HiddenColumns();
		function HiddenColumns() {
		var YearMonth=document.getElementById("txtYearMonth").value;
		if(YearMonth.length!=0)
		{
		    var temVal = new Array();   
            temVal =YearMonth.split("/");
		    var d = new Date(temVal[0],temVal[1],0);
            var days= d.getDate();
            
            var oGrid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
            var oBands = oGrid.Bands;
            var oBand = oBands[0];
            var oColumns = oBand.Columns;
                if(days==28)
                {
                    oColumns[55].setHidden(true);
                    oColumns[56].setHidden(true);
                    oColumns[57].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_57").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_92").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_93").style.display="none";
                    }
                }
                if(days==29)
                {
                    oColumns[55].setHidden(false);
                    oColumns[56].setHidden(true);
                    oColumns[57].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_57").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_92").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_93").style.display="none";
                    }
                }
                if(days==30)
                {
                    oColumns[55].setHidden(false);
                    oColumns[56].setHidden(false);
                    oColumns[57].setHidden(true);      
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_57").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_92").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_93").style.display="none";
                    }
                }
                if(days==31)
                {
                    oColumns[55].setHidden(false);
                    oColumns[56].setHidden(false);
                    oColumns[57].setHidden(false);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_57").style.display="";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_92").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_93").style.display="";
                    }
                }
            }
        }     
	--></script>

</body>
</html>
