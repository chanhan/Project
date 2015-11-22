<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignLogAndMap.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.SignLogAndMap" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Log</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/FlowMap.css" rel="stylesheet" type="text/css" />
<style type="text/css">

</style>
    <script type="text/javascript">
         //顯示隱藏
          $(function()
          {
             $("#tr_edit").toggle
             (
                function()
                {
                    $("#div_log").hide();
                    $(".img1").attr("src","../CSS/Images_new/left_back_03.gif");                   
                },
                function()
                {
                  $("#div_log").show();
                  $(".img1").attr("src","../CSS/Images_new/left_back_03_a.gif");              
                }
             ) 
             $("#tr_head").toggle
             (
                function()
                {
                    $("#div_map").hide();
                    $(".img2").attr("src","../CSS/Images_new/left_back_03.gif");                  
                },
                function()
                {
                  $("#div_map").show();
                  $(".img2").attr("src","../CSS/Images_new/left_back_03_a.gif");              
                }
             )                 
           });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_sign_log" runat="server"></asp:Label>
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
        <div id="div_log" style="text-align:center; margin:0 auto;">
            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                <tr style="width: 100%">
                    <td valign="top" width="19px" background="../CSS/Images_new/EMP_05.gif" height="18">
                        <img height="18px" src="../CSS/Images_new/EMP_01.gif" width="19">
                    </td>
                    <td background="../CSS/Images_new/EMP_07.gif" height="19px">
                    </td>
                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                        <img height="18" src="../CSS/Images_new/EMP_02.gif" width="19">
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td width="19px" background="../CSS/Images_new/EMP_05.gif">
                        &nbsp;
                    </td>
                    <td style="margin:0 auto; text-align:center">
                        <script language="javascript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*30/100+"'>");</script>
                        <igtbl:UltraWebGrid ID="UltraWebGridFlowLog" runat="server"  Height="100%" Width="100%"  >
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="NotSet" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                AutoGenerateColumns="false" Name="UltraWebGrid" >
                                <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                    CssClass="tr_header">
                                    <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                    </BorderDetails>
                                </HeaderStyleDefault>
                                <FrameStyle Width="100%" Height="100%">
                                </FrameStyle>
                                <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                    AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="DblClick"></ClientSideEvents>
                                <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                </SelectedRowStyleDefault>
                                <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                </RowAlternateStyleDefault>
                                <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#6699ff" BorderStyle="Solid"
                                    CssClass="tr_data1">
                                    <Padding Left="3px"></Padding>
                                    <BorderDetails WidthLeft="0px" WidthTop="0px" ></BorderDetails>
                                </RowStyleDefault>
                            </DisplayLayout>
                            <Bands>
                                <igtbl:UltraGridBand BaseTableName="FlowLog" Key="FlowLog">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="Deal_person" Key="Deal_person" IsBound="false" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gv_deal_person %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="start_time" Key="start_time" IsBound="false"  Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadBeginTime %>" >
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="end_time" Key="end_time" IsBound="false"
                                           Width="20%" >
                                            <Header Caption="<%$Resources:ControlText,gvHeadEndTime %>" >
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ismail" Key="ismail" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gv_ismail %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                         <igtbl:UltraGridColumn BaseColumnName="remark" Key="remark" IsBound="false" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,labbeiz %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="status" Key="status" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvApproveFlagName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="javascript" type="text/javascript">document.write("</DIV>");</script>

                    </td>
                    <td width="19" background="../CSS/Images_new/EMP_06.gif">
                        &nbsp;
                    </td>
                    <tr>
                    </tr>
                </tr>
                <tr>
                    <td width="19" background="../CSS/Images_new/EMP_03.gif" height="18">
                        &nbsp;
                    </td>
                    <td background="../CSS/Images_new/EMP_08.gif" height="18">
                        &nbsp;
                    </td>
                    <td width="19" background="../CSS/Images_new/EMP_04.gif" height="18">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_head">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_sign_map" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div2">
                            <img id="Img2" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_map" style="width: 100%; text-align:center;">
            <asp:Literal ID="lbl_Process" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
