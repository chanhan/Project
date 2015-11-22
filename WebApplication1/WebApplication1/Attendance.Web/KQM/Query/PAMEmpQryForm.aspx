<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAMEmpQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.PAMEmpQryForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Resources.ControlText.lblPlusDemeritPoints%></title>
</head>
<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

<script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>
<script>
$(function(){
    $("#img_grid_1").toggle(
                function(){
                    $("#div_1").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_1").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
               $("#img_grid_2").toggle(
                function(){
                    $("#div_2").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_2").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_3").toggle(
                function(){
                    $("#div_3").hide();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_3").show();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_4").toggle(
                function(){
                    $("#div_4").hide();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_4").show();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
});
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_1">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center" id="td5">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblKaoQin" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_1" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_1">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/300+"'>");</script>

                            <igtbl:UltraWebGrid ID="WebGridKaoQin" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="WebGridKaoQin" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="KaoQin" Key="KaoQin">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ITEMNAME" Key="ITEMNAME" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvItemName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ITEMDESC" Key="ITEMDESC" IsBound="false" Width="50%">
                                                <Header Caption="<%$Resources:ControlText,gvItemDesc%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="QTY" Key="QTY" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvQTY%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SCORE" Key="SCORE" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvScore%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="javascript">document.write("</DIV>");</script>

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
        </div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_2">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center" id="td1">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblRewardsAndPunishments" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_2">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/300+"'>");</script>

                            <igtbl:UltraWebGrid ID="WebGridJiangCheng" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="WebGridJiangCheng" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="JiangCheng" Key="JiangCheng">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ITEMNAME" Key="ITEMNAME" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvItemName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ITEMDESC" Key="ITEMDESC" IsBound="false" Width="50%">
                                                <Header Caption="<%$Resources:ControlText,gvItemDesc%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="QTY" Key="QTY" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvQTY%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SCORE" Key="SCORE" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvScore%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="javascript">document.write("</DIV>");</script>

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
        </div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_3">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center" id="td2">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblProposalsImprove" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_3" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_3">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/300+"'>");</script>

                            <igtbl:UltraWebGrid ID="WebGridIE" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="WebGridIE" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand  BaseTableName="IE" Key="IE">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveLevel10" Key="ImproveLevel10" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveLevel10%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveLevel89" Key="ImproveLevel89" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveLevel89%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveLevel67" Key="ImproveLevel67" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveLevel67%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveLevel15" Key="ImproveLevel15" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveLevel15%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveRealValue" Key="ImproveRealValue" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveRealValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ImproveTargetValue" Key="ImproveTargetValue"
                                                IsBound="false" Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveTargetValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="improveValue" Key="improveValue" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvImproveValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TimproveValue" Key="TimproveValue" IsBound="false"
                                                Width="11%">
                                                <Header Caption="<%$Resources:ControlText,gvTimproveValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Score" Key="Score" IsBound="false" Width="12%">
                                                <Header Caption="<%$Resources:ControlText,gvScore%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="javascript">document.write("</DIV>");</script>

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
        </div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_4">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center" id="td3">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCredit" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_4" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_4">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/300+"'>");</script>

                            <igtbl:UltraWebGrid ID="WebGridStudy" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="WebGridStudy" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                        DataKeyField="" BaseTableName="Study" Key="Study">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="TrainRealValue" Key="TrainRealValue" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvTrainRealValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TrainTargetValue" Key="TrainTargetValue" IsBound="false"
                                                Width="50%">
                                                <Header Caption="<%$Resources:ControlText,gvTrainTargetValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="trainValue" Key="trainValue" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvTrainValue%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Score" Key="Score" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvScore%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="javascript">document.write("</DIV>");</script>

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
        </div>
    </div>
    </form>
</body>
</html>
