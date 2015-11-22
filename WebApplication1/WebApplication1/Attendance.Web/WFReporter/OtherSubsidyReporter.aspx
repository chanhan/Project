<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherSubsidyReporter.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WFReporter.OtherSubsidyReporter" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>
    
    <script type="text/javascript"><!--
      $(function(){
        $("#img_edit").toggle(
            function(){$("#tr_edit").hide(); $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#tr_edit").show();$(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
       });
    $(function(){
       $("#img_grid").toggle(
            function(){$("#tr_show").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#tr_show").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); } ) 
    });
    $(function(){
       $("#div_img_2").toggle(
            function(){$("#tr_show").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){$("#tr_show").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
   });
   --></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit" style="width: 100%">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <asp:Panel ID="inputPanel" runat="server">
                                            <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                            <asp:HiddenField ID="hidEffectFlag" runat="server" Value="Y" />
                                            <asp:HiddenField ID="ProcessFlag" runat="server" Value="N" />
                                            <tr>
                                                <td class="td_label" width="14%">
                                                    &nbsp;<asp:Label ID="lblDep" runat="server"></asp:Label>
                                                </td>
                                                <td class="td_input" width="17%">
                                                    <asp:TextBox ID="txtDeptCode" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblWorkNoWorkNo" runat="server"></asp:Label>
                                                </td>
                                                <td class="td_input" width="18%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblLocalName" runat="server"></asp:Label>
                                                </td>
                                                <td class="td_input" width="17%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblblStartDate" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td class="td_input" width="50%">
                                                                <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                            <td>~</td>
                                                            <td class="td_input" width="50%">
                                                                <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblStatu" runat="server"></asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList runat="server" ID="ddlStatus" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                            </asp:Panel>
                            <tr>
                                <td class="td_label" colspan="6">
                                    <table>
                                        <tr>
                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                                <td>
                                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnQuery %>"
                                                        ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                    <asp:Button ID="btnReset" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnReset %>"
                                                        ToolTip="Authority Code:Reset"></asp:Button>
                                                    <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                        ToolTip="Authority Code:Add" CssClass="button_1"></asp:Button>
                                                    <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                        ToolTip="Authority Code:Modify" CssClass="button_1"></asp:Button>
                                                    <asp:Button ID="btnImport" runat="server" Text="<%$Resources:ControlText,btnImport %>"
                                                        CssClass="button_1" ToolTip="Authority Code:Import"></asp:Button>
                                                    <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                        CssClass="button_1" ToolTip="Authority Code:Export" OnClick="btnExport_Click">
                                                    </asp:Button>
                                                </td>
                                            </asp:Panel>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td> </tr> </table>
        </div>
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" class="tr_title_center" id="img_grid">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                                ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                Width="300px" ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_div2">
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr style="width: 100%">
                        <td valign="top" width="19px" background="../CSS/Images_new/EMP_05.gif" height="18">
                            <img height="18" src="../CSS/Images_new/EMP_01.gif" width="19">
                        </td>
                        <td background="../CSS/Images_new/EMP_07.gif" height="19px">
                        </td>
                        <td valign="top" width="19px" background="../CSS/Images_new/EMP_06.gif" height="18">
                            <img height="18" src="../CSS/Images_new/EMP_02.gif" width="19">
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td width="19" background="../CSS/Images_new/EMP_05.gif">
                            &nbsp;
                        </td>
                        <td>

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveType" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_leavetype_v" Key="gds_att_leavetype_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" IsBound="false" Key="LVTypeCode"
                                                Width="5%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLVTypeCode %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeName" IsBound="false" Key="LVTypeName"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLVTypeName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="UseState" IsBound="false" Key="UseState" Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderUseState %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LimitDays" IsBound="false" Key="LimitDays"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLimitDays %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MinHours" IsBound="false" Key="MinHours" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderMinHours%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StandardHours" IsBound="false" Key="StandardHours"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderStandardHours %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FitSexName" IsBound="false" Key="FitSexName"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderFitSexName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="HasMoney" IsBound="false" Key="HasMoney" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderHasMoney %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Prove" IsBound="false" Key="Prove" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderProve %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" IsBound="false" Key="Remark" Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsCludeHoliday" IsBound="false" Key="IsCludeHoliday"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderIsCludeHoliday %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ISAllowPCM" IsBound="false" Key="ISAllowPCM"
                                                Width="12%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderISAllowPCM %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FitSex" IsBound="false" Key="FitSex" Hidden="true">
                                                <Header Caption="FitSex">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EffectFlag" IsBound="false" Key="EffectFlag"
                                                Hidden="true">
                                                <Header Caption="EffectFlag">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="javascript">document.write("</DIV>");</script>

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
        </div>
    </div>
    </form>
</body>
</html>
