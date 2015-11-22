<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelSelector.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HRM.LevelSelector" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript">

$(function(){
		            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
});
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			window.returnValue={ codeList: row.getCell(0).getValue(),nameList: row.getCell(1).getValue()};
			window.close();
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
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
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tr_edit">
        <tr>
            <td>
                <igtbl:UltraWebGrid ID="UltraWebGridOtherData" runat="server" Width="100%" Height="100%">
                    <DisplayLayout CompactRendering="False" AutoGenerateColumns="false" StationaryMargins="HeaderAndFooter"
                        AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOtherData" TableLayout="Fixed"
                        CellClickActionDefault="RowSelect">
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
                        <igtbl:UltraGridBand BaseTableName="signledata" Key="signledata">
                            <Columns>
                                <igtbl:UltraGridColumn BaseColumnName="LevelCode" IsBound="false" Key="LevelCode" Width="50%">
                                    <Header Caption="<%$Resources:ControlText,gvHeadLevelCode %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="LevelName" IsBound="false" Key="LevelName" Width="50%">
                                    <Header Caption="<%$Resources:ControlText,gvHeadLevelName %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                            </Columns>
                        </igtbl:UltraGridBand>
                    </Bands>
                </igtbl:UltraWebGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
