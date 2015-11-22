<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepLevelSelector.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.DepLevelSelector" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
	    $(function(){
        $("#img_edit").toggle(
            function(){
                $("#div_select").hide();
                $(".img1").attr("src","../../CSS/Images/downarrows_white.gif");
                
            },
            function(){
              $("#div_select").show();
                $(".img1").attr("src","../../CSS/Images/uparrows_white.gif");
            }
        )
       });
       
    
        
       function AfterSelectChange(gridName, id)
       {
                var row = igtbl_getRowById(id);
                var dnameList = "", didList = "";
               didList = row.getCell(0).getValue()==null?"":row.getCell(0).getValue();
                dnameList = row.getCell(1).getValue()==null?"":row.getCell(1).getValue();       
                window.returnValue = { codeList: didList, nameList: dnameList };
                window.close();
	   }
    </script>

</head>
<base target="_self" />
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="cursor: hand" id="tr_edit" bgcolor="lightblue">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="tr1">
                        <td style="width: 100%;" class="tr_title_center">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </td>
                    </tr>
                </table>
            </tr>
        </table>
    </div>
    <div id="div_select">
        <igtbl:UltraWebGrid ID="UltraWebGridDepLevel" runat="server" Width="100%" Height="100%">
            <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridPerson" CompactRendering="False"
                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter"
                AutoGenerateColumns="false">
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
                <igtbl:UltraGridBand>
                    <Columns>
                        <igtbl:UltraGridColumn BaseColumnName="LEVELCODE" IsBound="false" Key="ORDERID" Width="40%">
                            <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>" Fixed="true">
                            </Header>
                        </igtbl:UltraGridColumn>
                    </Columns>
                    <Columns>
                        <igtbl:UltraGridColumn BaseColumnName="LEVELNAME" IsBound="false" Key="LEVELNAME"
                            Width="50%">
                            <Header Caption="<%$Resources:ControlText,gvHeadLevelName %>" Fixed="true">
                            </Header>
                        </igtbl:UltraGridColumn>
                    </Columns>
                </igtbl:UltraGridBand>
            </Bands>
        </igtbl:UltraWebGrid>
    </div>
    </form>
</body>
</html>
