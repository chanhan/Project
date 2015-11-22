<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HomePage.Form" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script>
         function down(f) { $("#frameDown").attr("src", "Form.aspx?path=" + encodeURI(f)); }
    $(function(){
                  $("#img_grid").toggle(
                function(){
                        $("#tr_show").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid2").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
    });
    </script>

</head>
<body>
    <form id="form1" runat="server">
           <iframe id="frameDown" style="display: none;"></iframe>
    <div class="contentTitle">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td3">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblFormDownLoad" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;" id="td4">
                    <div id="img_grid2">
                        <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" id="tr_edit" width="100%">
        <tr>
            <td class="tbTopLeft">
            </td>
            <td class="tbTopMiddle">
            </td>
            <td class="tbTopRight">
            </td>
        </tr>
        <tr>
            <td class="tbMiddleLeft">
                &nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlFormInfo" runat="server">
                    <table cellpadding="0" cellspacing="0" class="QueryTable" width="100%">
                        <tr>
                            <td align="right" style="width: 15%;">
                                <asp:Label ID="lblTypeId" runat="server"></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlTypeId" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 15%;">
                                <asp:Label ID="lbllblFormName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtFormName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 25%;">
                                <table>
                                    <tr>
                                        <td>
                                            <td style="width: 45px">
                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                    background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                    font-size: 13px;">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnQuery" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                ToolTip="Authority Code:Query" OnClick="btnQuery_Click">
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td class="tbMiddleRight">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbBottomLeft">
            </td>
            <td class="tbBottomMiddle">
            </td>
            <td class="tbBottomRight">
            </td>
        </tr>
    </table>
    <div style="width: 100%" id="PanelData">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
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
                <td class="tr_title_center" style="width: 300px;">
                    <div>
                        <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                            HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                            ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px;" id="td_show_2">
                    <div id="img_grid">
                        <img id="div_img_1" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
        <div id="tr_show">
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

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridInfoForm" runat="server" Width="100%" Height="100%"
                            OnDataBound="UltraWebGridInfoForm_DataBound">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoForm" TableLayout="Fixed"
                                AutoGenerateColumns="false" CellClickActionDefault="NotSet">
                                <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                    CssClass="tr_header">
                                    <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                    </BorderDetails>
                                </HeaderStyleDefault>
                                <FrameStyle Width="100%" Height="100%">
                                </FrameStyle>
                                <ClientSideEvents DblClickHandler="UltraWebGridInfoForm_DblClickHandler"></ClientSideEvents>
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
                                <igtbl:UltraGridBand BaseTableName="gds_att_info_forms_v" Key="gds_att_info_forms_v">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="Type_Name" Key="Type_Name" IsBound="false"
                                            Format="yyyy/MM/dd" Width="40%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadTypeName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Form_Name" Key="Form_Name" IsBound="false"
                                            Width="40%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadFormFormName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Form_Path" Key="Form_Path" IsBound="false" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadFormPath %>">
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
    </form>
</body>
</html>
