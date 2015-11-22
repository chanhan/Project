<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveApplyImportForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData.KQMLeaveApplyImportForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script>
$(function(){
          $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
                  $("#img_grid").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
})
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.tr_show.style.display="";                             
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.div_select2.style.display="";                             
            return false;
        }      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="common_import_area" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit">
            <table>
                <tr width="100%">
                    <td class="td_label" align="left" >
                        <a href="../../../ExcelModel/LeaveApplySample.xls">
                            <%=Resources.ControlText.InfoGetExcelModel%>
                        </a>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server"/>
                        <asp:Button ID="btnImportSave" runat="server" CssClass="button_1" OnClick="btnImportSave_Click" />
                        <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click" />
                        <asp:Button ID="btnReturn" runat="server" CssClass="button_1" OnClientClick="return Return();" />
                        <img id="imgWaiting" src="../../../CSS/Images/clocks.gif" border="0" style="display: none;
                            height: 20px;" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="height: 25;">
                        <asp:Label ID="lbluploadInfo2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td>
                <asp:Label runat="server" ID="lbllblupload" ForeColor="Red"></asp:Label>
                </td>
                </tr>
            </table>
        </div>
        <div>
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" class="tr_title_center" id="img_grid">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_div2">
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr style="width: 100%">
                        <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                            <img height="18" src="../../../CSS/Images_new/EMP_01.gif" width="19">
                        </td>
                        <td background="../../../CSS/Images_new/EMP_07.gif" height="19px">
                        </td>
                        <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_06.gif" height="18">
                            <img height="18" src="../../../CSS/Images_new/EMP_02.gif" width="19">
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td width="19" background="../../../CSS/Images_new/EMP_05.gif">
                            &nbsp;
                        </td>
                        <td>

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridImport" TableLayout="Fixed"
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvErrorMsg%>">
                                                </Header>
                                                <CellStyle ForeColor="red">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTYPECODE" Key="LVTYPECODE" IsBound="false"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvLVTypeName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvStartDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvStartTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvgvEndDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvEndTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotal" Key="LVTotal" IsBound="false" Width="5%">
                                                <Header Caption="<%$Resources:ControlText,gvgvLVTotal%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Reason" Key="Reason" IsBound="false" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Proxy" Key="Proxy" IsBound="false" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvProxy%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvRemark%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="APPLYTYPE" Key="APPLYTYPE" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvApplyTypeName%>">
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
