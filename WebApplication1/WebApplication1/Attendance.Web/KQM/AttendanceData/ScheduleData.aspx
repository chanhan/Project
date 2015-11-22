<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleData.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.ScheduleData" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Src="../../ControlLib/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="ControlLib" %>
<%--<%@ Register Src="../../ControlLib/title.ascx" TagName="title" TagPrefix="ControlLib" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonCss.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
      function  Import_Click()
      {
          $("#<%=btnQuery.ClientID %>").attr("disabled","true");
           $("#<%=ButtonReset.ClientID %>").attr("disabled","true");
            $("#<%=ButtonOrgShift.ClientID %>").attr("disabled","true");
             $("#<%=ButtonEmpShift.ClientID %>").attr("disabled","true");
              $("#<%=ButtonImport.ClientID %>").attr("display","none");//???
                return false;
      }
      
      
        $("#tr_edit").toggle(function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_select").hide()},function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_select").show()});
        
        $("#div_img_2").toggle(function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_showdata").hide()},function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_showdata").show()});
        
        $("#tr_editimport").toggle(function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#tr_show_3").hide()},function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#tr_show_3").show()});
        
        $("#div_editdata").toggle(function(){
        $("#div_img_4").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#tr_show_4").hide()},function(){
        $("#div_img_4").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#tr_show_4").show()});
        })
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <%--  <ControlLib:title id="Title1" runat="server">--%>
    <%--   </ControlLib:title>--%>
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenShiftNo" type="hidden" name="HiddenShiftNo" runat="server" />
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <table cellspacing="1" id="top_table" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <div style="width: 100%;">
                                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                                        <tr style="width: 100%; cursor: hand;" id="tr_edit">
                                            <td style="width: 17px;">
                                                <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                                            </td>
                                            <td style="width: 100%;" class="tr_title_center">
                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                    background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                                    font-size: 13px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 22px;">
                                                <div id="img_edit">
                                                    <img id="Img3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <asp:Panel ID="pnlContent" runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblDepcode" runat="server">部門:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                            Style="display: none"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100%">
                                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="cursor: hand">
                                                                        <asp:Image ID="ImageDepCode" runat="server" ImageUrl="~/CSS/Images/zoom.png"></asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="labelShiftType" runat="server">類別:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <asp:DropDownList ID="ddlShiftType" runat="server" Width="100%" CssClass="input_textBox">
                                                                <asp:ListItem Value="" Text="請選擇"></asp:ListItem>
                                                                <asp:ListItem Value="OrgShift" Text="組織排班"></asp:ListItem>
                                                                <asp:ListItem Value="EmpShoft" Text="例外排班"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="labelShiftDate" runat="server">排班日期*:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="24%">
                                                            <asp:TextBox ID="txtSchduleDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="labelShift" runat="server">班別:</asp:Label>
                                                        </td>
                                                        <td class="td_input">
                                                            <cc1:DropDownCheckList ID="ddlShift" CheckListCssStyle="background-image: url(../../images/inputbg.bmp);height: 250px;overflow: scroll;"
                                                                Width="420" RepeatColumns="2" CssClass="input_textBox" DropImageSrc="~/CSS/Images/expand.gif"
                                                                TextWhenNoneChecked="" DisplayTextWidth="400" ClientCodeLocation="../../PubSC/DropDownCheckList.js"
                                                                runat="server">
                                                            </cc1:DropDownCheckList>
                                                        </td>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="labelWorkNo" runat="server">工號:</asp:Label>
                                                            <asp:Image ID="ImageBatchWorkNo" runat="server" ImageUrl="~/CSS/Images/zoom.png">
                                                            </asp:Image>
                                                        </td>
                                                        <td class="td_input">
                                                            <asp:TextBox ID="textBoxWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                                z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                                border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                                border-bottom: #0000ff 1px solid; position: absolute; left: 41%; float: left;
                                                                display: none;">
                                                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                    <tr>
                                                                        <td>
                                                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tr>
                                                                                                <td class="td_label" width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                                    <font color="red">Ⅹ</font>
                                                                                                    <%=this.GetResouseValue("common.message.querybatchworkno")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td_label" width="100%">
                                                                                                    <asp:TextBox ID="textBoxBatchEmployeeNo" runat="server" TextMode="MultiLine" Height="100"
                                                                                                        Width="100%" Style="display: none"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <iframe src="JavaScript:false" style="position: absolute; visibility: inherit; top: 0px;
                                                                    left: 0px; width: 225px; height: 100px; z-index: -1; filter='progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                                </iframe>
                                                            </div>
                                                        </td>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="labelLocalName" runat="server">姓名:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="20%">
                                                            <asp:TextBox ID="textBoxLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" colspan="6">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnQuery" runat="server" Text="查詢" ToolTip="Authority Code:Query"
                                                                            CommandName="Query" OnClick="btnQuery_Click"></asp:Button>
                                                                        <asp:Button ID="ButtonReset" runat="server" Text="重置" ToolTip="Authority Code:Reset"
                                                                            CommandName="Reset"></asp:Button>
                                                                    </td>
                                                                    <td class="td_seperator">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="ButtonOrgShift" runat="server" Text="組織排班" CommandName="OrgShift"
                                                                            ToolTip="Authority Code:OrgShift" OnClientClick="return OpenWindow('ORG')"></asp:Button>
                                                                        <asp:Button ID="ButtonEmpShift" runat="server" Text="例外排班" CommandName="EmpShift"
                                                                            ToolTip="Authority Code:EmpShift" OnClientClick="return OpenWindow('EMP')"></asp:Button>
                                                                        <asp:Button ID="ButtonImport" runat="server" Text="導入" CommandName="Import" ToolTip="Authority Code:Import"
                                                                            OnClientClick="return Import_Click()" OnClick="ButtonImport_Click"></asp:Button>
                                                                        <asp:Button ID="ButtonExport" runat="server" Text="導出" CommandName="Export" ToolTip="Authority Code:Export"
                                                                            OnClick="ButtonExport_Click"></asp:Button>
                                                                        <asp:Label ID="labelMessage" ForeColor="red" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td_label" width="100%" align="left" colspan="2">
                                                                        <a href="../Excel/DownLoad/NetSignInSetSample.xls">&nbsp;<%=Resources.ControlText.templateddown%>
                                                                        </a>&nbsp;<asp:Label ID="label1" runat="server" Font-Bold="true"></asp:Label>
                                                                        <asp:FileUpload ID="FileUpload1" CssClass="input_textBox" runat="server" Width="30%" />
                                                                        <asp:Button ID="btnImportSave" runat="server" Text="ImportSave" OnClick="btnImportSave_Click" />
                                                                        <img id="img2" src="<%=sAppPath%>/images/clocks.gif" border="0" style="display: none;
                                                                            height: 20px;" />
                                                                        <asp:Label ID="lblUpload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                                        <asp:Label ID="lblUploadMsg" runat="server" ForeColor="red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" style="height: 25;">
                                                                        &nbsp;<%=Resources.ControlText.importremark%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;">
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
                                    <td style="width: 22px; cursor: hand;">
                                        <img id="Img1" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td class="tr_table_title" width="100%" onclick="turnit('div_2','div_img_2','<%=sAppPath%>');">
                                            <%=this.GetResouseValue("common.display.area")%>
                                            &nbsp;
                                        </td>
                                        <td class="tr_table_title" align="right" valign="middle">
                                            <ControlLib:PageNavigator ID="PageNavigator" runat="server"></ControlLib:PageNavigator>
                                        </td>
                                        <td class="tr_table_title" align="right" onclick="turnit('div_2','div_img_2','<%=sAppPath%>');">
                                            <img id="div_img_2" src="<%=sAppPath%>/images/uparrows_white.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">

                                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*62/100+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridSchedule" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout Name="UltraWebGridShiftQuery" CompactRendering="False" RowHeightDefault="20px"
                                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                    AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridShiftQuery_InitializeLayoutHandler"
                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                    <SelectedRowStyleDefault BackgroundImage="../../CSS/Images/overbg.bmp">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                        CssClass="tr_data">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand BaseTableName="GDS_ATT_EMPLOYEESHIFT" Key="GDS_ATT_EMPLOYEESHIFT">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" IsBound="True" Key="WorkNo" Width="10%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" IsBound="True" Key="LocalName"
                                                                Width="10%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="DName" IsBound="True" Key="DName" Width="25%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" IsBound="True" Key="ShiftDesc"
                                                                Width="35%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StartEndDate" IsBound="True" Key="StartEndDate"
                                                                Width="20%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadStartEndDate %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td class="tr_table_title" width="100%" onclick="turnit('div_3','div_img_3','<%=sAppPath%>');">
                                            <%=this.GetResouseValue("common.import.area")%>
                                            &nbsp;
                                        </td>
                                        <td class="tr_table_title" align="right" onclick="turnit('div_3','div_img_3','<%=sAppPath%>');">
                                            <img id="div_img_3" src="<%=sAppPath%>/images/uparrows_white.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" width="100%" align="left" colspan="2">
                                            <a href="">&nbsp;<%=this.GetResouseValue("bfw.hrm.templateddown")%>
                                            </a>&nbsp;<asp:Label ID="labelUploadText" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                            <asp:Button ID="ButtonImportSave" runat="server" Text="ImportSave" OnClientClick="javascript:UpProgress();" />
                                            <img id="imgWaiting" src="<%=sAppPath%>/images/clocks.gif" border="0" style="display: none;
                                                height: 20px;" />
                                            <asp:Label ID="labelupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="labeluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="height: 25;">
                                            &nbsp;<%=this.GetResouseValue("bfw.hrm.importremark")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"">

                                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*55/100+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                    RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                    BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                    Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                        CssClass="tr_data">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand BaseTableName="HRM_Import" Key="HRM_Import">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="true" Width="20%">
                                                                <Header Caption="ErrorMsg">
                                                                </Header>
                                                                <CellStyle ForeColor="red">
                                                                </CellStyle>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="true" Width="20%">
                                                                <Header Caption="WorkNo">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" Key="ShiftNo" IsBound="true" Width="20%">
                                                                <Header Caption="ShiftNo">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="true"
                                                                Width="20%">
                                                                <Header Caption="StartDate">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="true" Width="20%">
                                                                <Header Caption="EndDate">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <%--<td width="45%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">

                                <script language="javascript">document.write("<DIV id='div_10' style='height:"+document.body.clientHeight*83/100+"'>");</script>

                                <iframe name="EditShift" src="KQMEmployeeShiftEditForm.aspx?OrgCode=<%=this.Request.QueryString["OrgCode"] == null ? "" : this.Request.QueryString["OrgCode"].ToString()%>&ModuleCode=<%=this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString()%>"
                                    width="100%" height="100%" scrolling="no"></iframe>

                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                            </td>--%>
        </tr>
    </table>
    <%--<igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter11" runat="server"
        OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
    </igtblexp:UltraWebGridExcelExporter>--%>
    </form>
</body>
</html>
