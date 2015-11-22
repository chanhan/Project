<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMEmpDataPickForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMEmpDataPickForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script type="text/javascript">
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
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);	
			window.returnValue=row.getCellFromKey("AuditMan").getValue()+";"+row.getCellFromKey("LocalName").getValue()+";"+row.getCellFromKey("Notes").getValue();
			window.close();			
		}
    </script>

</head>
<base target="_self" />
<body>
    <form id="Form1" method="post" runat="server">
    <input id="HiddenDataType" type="hidden" name="HiddenDataType" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblSearchCondition" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;">
                    <div>
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" id="tr_edit">
            <tr>
                <td colspan="2">
                    <div id="div_1">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="td_label" width="15%">
                                    &nbsp;<asp:Label ID="lblWorkNo" runat="server">labelName:</asp:Label>
                                </td>
                                <td class="td_input" width="30%">
                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                                <td class="td_label" width="15%">
                                    &nbsp;<asp:Label ID="lblLocalName" runat="server">labelCode:</asp:Label>
                                </td>
                                <td class="td_input" width="25%">
                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                                <td class="td_label">
                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" ToolTip="Authority Code:Query"
                                        OnClick="btnQuery_Click"></asp:Button>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_grid">
                <td style="width: 100%;" class="tr_title_center">
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
                <td style="width: 22px;">
                    <div>
                        <img id="div_img_2" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" id="tr_show">
        <tr>
            <td colspan="2">
                <div id="div_2" style="height: 270;">
                    <igtbl:UltraWebGrid ID="UltraWebGridAudit" runat="server" Width="100%" Height="100%">
                        <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                            AutoGenerateColumns="false" AllowSortingDefault="Yes" RowHeightDefault="20px"
                            Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                            BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                            Name="UltraWebGridAudit" TableLayout="Fixed" CellClickActionDefault="RowSelect">
                            <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                CssClass="tr_header">
                                <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                </BorderDetails>
                            </HeaderStyleDefault>
                            <FrameStyle Width="100%" Height="100%">
                            </FrameStyle>
                            <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
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
                            <igtbl:UltraGridBand BaseTableName="TempTable" Key="TempTable">
                                <Columns>
                                    <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="25%">
                                        <Header Caption="<%$Resources:ControlText,gvOTMRealDepName%>">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="AuditMan" Key="AuditMan" IsBound="false" Width="15%">
                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionWorkNo%>">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                        Width="15%">
                                        <Header Caption="<%$Resources:ControlText,gvOTMRealApproverName%>">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="Notes" Key="Notes" IsBound="false" Width="30%">
                                        <Header Caption="Notes">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="ManagerName" Key="ManagerName" IsBound="false"
                                        Width="15%">
                                        <Header Caption="<%$Resources:ControlText,gvManager%>">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                </Columns>
                            </igtbl:UltraGridBand>
                        </Bands>
                    </igtbl:UltraWebGrid>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
