<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleDataPickForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.EmployeeData.SingleDataPickForm" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript">

		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);			
            if(document.getElementById('HiddenDataType').value=="DepCode")
            { 
                var temVal= row.getCell(0).getValue().split("[");
			    window.returnValue=temVal[0]+";"+row.getCell(1).getValue();
            }
			else if(document.getElementById('HiddenDataType').value=="HRAdmin")
            { 
                var temVal= row.getCell(1).getValue().split("[");
			    window.returnValue=row.getCell(0).getValue()+";"+temVal[0];
            }
			else
			{
			    window.returnValue=row.getCell(0).getValue()+";"+row.getCell(1).getValue();
			}
			window.close();			
		}
		$(function()
{
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
              $("#img_grid").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                }
            );
});
		</script>

</head>
<base target="_self" />
<body>
    <form id="Form1" method="post" runat="server">
    <input id="HiddenDataType" type="hidden" name="HiddenDataType" runat="server">
    <input id="HiddenCondition" type="hidden" name="HiddenCondition" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
                <td style="width: 17px;">
                    <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                </td>
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
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
                                    &nbsp;<asp:Label ID="lblTypeName" runat="server">labelName:</asp:Label>
                                </td>
                                <td class="td_input" width="30%">
                                    <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                                <td class="td_label" width="15%">
                                    &nbsp;<asp:Label ID="lblTypeCode" runat="server">labelCode:</asp:Label>
                                </td>
                                <td class="td_input" width="25%" id="td_textBoxCode">
                                    <asp:TextBox ID="txtCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                                <td class="td_label">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnQuery" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnQuery%>"
                                                    ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:LinkButton>
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
    </div>
       <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="Tr1">
                <td style="width: 17px;">
                    <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                </td>
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;">
                    <div id="img_grid">
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
                    <igtbl:UltraWebGrid ID="UltraWebGridOtherData" runat="server" Width="100%" Height="100%">
                        <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                            AutoGenerateColumns="false" AllowSortingDefault="Yes" RowHeightDefault="20px"
                            Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                            BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                            Name="UltraWebGridOtherData" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
                            <igtbl:UltraGridBand BaseTableName="gds_att_typedata" Key="gds_att_typedata">
                                <Columns>
                                    <igtbl:UltraGridColumn BaseColumnName="datavalue" Key="datavalue" IsBound="false"
                                        Width="50%">
                                        <Header Caption="<%$Resources:ControlText,gvHeaderDataValue %>" Fixed="true">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="datacode" Key="datacode" IsBound="false" Width="50%">
                                        <Header Caption="<%$Resources:ControlText,gvHeaderDataCode %>" Fixed="true">
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

<script type='text/javascript'><!--
if(document.getElementById('HiddenDataType').value=="BuCode")
{
    document.getElementById('td_textBoxCode').className='td_label'; 
}
-->
</script>

