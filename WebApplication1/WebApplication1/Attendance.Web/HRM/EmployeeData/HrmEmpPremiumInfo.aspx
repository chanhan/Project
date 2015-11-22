<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpPremiumInfo.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.EmployeeData.HrmEmpPremiumInfo" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HrmEmpPremiumInfo</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenWorkNo" runat="server" />
    <asp:HiddenField ID="ImportFlag" runat="server" />
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <div id="topTable">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%; cursor: hand;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCondition" runat="server">查詢條件</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_select" style="width: 100%">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area">
                                            <tr class="tr_data_1">
                                                <td style="width: 16%">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmpNo" runat="server">工號:</asp:Label>
                                                </td>
                                                <td style="width: 17%">
                                                    <asp:TextBox ID="txtEmpNo" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 16%">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmpName" runat="server">姓名:</asp:Label>
                                                </td>
                                                <td style="width: 17%">
                                                    <asp:TextBox ID="txtEmpName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 16%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDeptName" runat="server">部門:</asp:Label>
                                                </td>
                                                <td class="td_input" width="17%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:HiddenField ID="hidDeptList" runat="server" />
                                                                <asp:TextBox ID="txtDeptList" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgbtnDeptSearch" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="16%">
                                                    &nbsp;
                                                    <asp:Label ID="lblPremiumName" runat="server">獎懲類別:</asp:Label>
                                                </td>
                                                <td width="17%">
                                                    <asp:DropDownList ID="ddlPremiumName" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="16%">
                                                    &nbsp;
                                                    <asp:Label ID="lblPremiumDate" runat="server">獎懲日期:</asp:Label>
                                                </td>
                                                <td width="18%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtStartDate" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtEndDate" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 16%">
                                                    &nbsp;
                                                    <asp:Label ID="JobStatus" runat="server">在職狀態:</asp:Label>
                                                </td>
                                                <td class="td_input" width="17%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:DropDownList ID="ddlJobStatus" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <%-- <asp:Panel ID="pnlShowPanel" runat="server">--%>
                                    <asp:Button ID="btnQuery" runat="server" Text="查詢" CssClass="button_1" OnClick="btnQuery_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="button_1"></asp:Button>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button_1" Text="新增"></asp:Button>
                                    <asp:Button ID="btnModify" runat="server" CssClass="button_1" Text="修改"></asp:Button>
                                    <asp:Button ID="btnSendToSign" runat="server" CssClass="button_1" Text="送簽"></asp:Button>
                                    <asp:Button ID="btnSearchThePrograme" runat="server" CssClass="button_1" Text="查看進度">
                                    </asp:Button>
                                    <asp:Button ID="btnImport" runat="server" CssClass="button_1" Text="導入" 
                                        onclick="btnImport_Click"></asp:Button>
                                    <asp:Button ID="btnReturn" runat="server" CssClass="button_1" Text="返回"></asp:Button>
                                    <asp:Button ID="btnExport" runat="server" CssClass="button_1" Text="導出" OnClick="btnExport_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnPrint" runat="server" CssClass="button_1" Text="打印"></asp:Button>
                                    <asp:Button ID="btnCheck" runat="server" CssClass="button_1" Text="核准"></asp:Button>
                                    <asp:Button ID="btnCancelCheck" runat="server" CssClass="button_1" Text="取消核准"></asp:Button>
                                    <asp:Button ID="btnEmpCount" runat="server" CssClass="button_1" Text="人員計算"></asp:Button>
                                    <asp:Button ID="btnRelationCount" runat="server" CssClass="button_1" Text="組織計算"
                                        Height="20px"></asp:Button>
                                    <%-- </asp:Panel>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 100%;" id="PanelData">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDisplayArea" runat="server">顯示區</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tr_title_center" style="width: 300px;">
                    <div>
                        <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                            ShowMoreButtons="false" HorizontalAlign="Center" PageSize="30" PagingButtonType="Image"
                            Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px; cursor: hand;" id="td_show_2">
                    <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                </td>
            </tr>
        </table>
        <div id="div_showdata">
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

                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*58/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridSupportIn" runat="server" Width="100%" Height="100%">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridSupportIn" TableLayout="Fixed"
                                AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
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
                                    <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                </RowStyleDefault>
                            </DisplayLayout>
                            <Bands>
                                <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                    DataKeyField="" BaseTableName="employee" Key="employee">
                                    <Columns>
                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                            HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                            <CellTemplate>
                                                <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                            </CellTemplate>
                                            <HeaderTemplate>
                                                <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                            </HeaderTemplate>
                                            <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                            </Header>
                                        </igtbl:TemplatedColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ColBuName" HeaderText="ColBuName" IsBound="false"
                                            Key="ColBuName" Width="4%">
                                            <Header Caption="事業處" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DeptName" HeaderText="DeptName" IsBound="false"
                                            Key="DeptName" Width="4%">
                                            <Header Caption="部門" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EmpNo" HeaderText="EmpNo" IsBound="false"
                                            Key="EmpNo" Width="14%" ValueList-Style-CssClass="a">
                                            <Header Caption="工號" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EmpName" HeaderText="EmpName" IsBound="false"
                                            Key="EmpName" Width="14%">
                                            <Header Caption="姓名" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PremiumName" HeaderText="PremiumName" IsBound="false"
                                            Key="PremiumName" Width="14%">
                                            <Header Caption="獎懲類型">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PremiumDate" Format="yyyy/MM/dd" HeaderText="PremiumDate"
                                            IsBound="false" Key="PremiumDate" Width="14%">
                                            <Header Caption="獎懲日期">
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PremiumNum" HeaderText="PremiumNum" IsBound="false"
                                            Key="PremiumNum" Width="14%">
                                            <Header Caption="次數">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PremiumTitle" HeaderText="PremiumTitle" IsBound="false"
                                            Key="PremiumTitle">
                                            <Header Caption="獎懲種類">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PremiumComment" HeaderText="PremiumComment"
                                            IsBound="false" Key="PremiumComment" Width="14%">
                                            <Header Caption="獎懲事由">
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

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
    <%--<div style="display: none" id="PanelImport">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%; cursor: hand;" id="tr_editimport">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblImportArea" runat="server">導入區</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="div_editimg">
                            <img id="div_img_3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
                <tr id="tr_show_1">
                    <td width="100%" align="left" colspan="2">
                        <a href="../../ExcelModel/EmployeeSample.xls">&nbsp;下載模板 </a>&nbsp;
                        <asp:FileUpload ID="FileUpload" runat="server" Width="30%" />
                        <asp:Button ID="btnImportSave" CssClass="button_1" runat="server" Text="上傳" OnClick="btnImportSave_Click" />
                        <img id="imgWaiting" src="/images/clocks.gif" border="0" style="display: none; height: 20px;" />
                        <asp:Label ID="lblUpload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblUploadMsg" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr id="tr_show_2">
                    <td align="left" colspan="2" style="height: 25;">
                        &nbsp;注意：可以將錯誤的信息導出修改后重新導入！
                    </td>
                </tr>
                <tr id="tr_show_3">
                    <td colspan="2"">

                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*54/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
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
                                <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler">
                                </ClientSideEvents>
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
                                    DataKeyField="" BaseTableName="HRM_Import" Key="HRM_Import">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" HeaderText="ErrorMsg" IsBound="false"
                                            Key="ErrorMsg" Width="280px">
                                            <Header Caption="錯誤信息">
                                            </Header>
                                            <CellStyle ForeColor="Red">
                                            </CellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                            Key="WorkNo" Width="10%">
                                            <Header Caption="工號">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                            Key="LocalName" Width="10%">
                                            <Header Caption="姓名">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Identify" HeaderText="Identify" IsBound="false"
                                            Key="Identify" Hidden="true">
                                            <Header Caption="身份證">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Age" HeaderText="Age" IsBound="false" Key="Age"
                                            Width="10%">
                                            <Header Caption="年齡">
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="BGCode" HeaderText="BGCode" IsBound="false"
                                            Key="BGCode" Width="10%">
                                            <Header Caption="事業群">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Factory" HeaderText="Factory" IsBound="false"
                                            Key="Factory" Width="10%">
                                            <Header Caption="廠區">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="HireDate" HeaderText="HireDate" IsBound="false"
                                            Key="HireDate" Width="10%">
                                            <Header Caption="入職日期">
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="false"
                                            Key="Status" Width="10%">
                                            <Header Caption="在職狀態">
                                                <RowLayoutColumnInfo OriginX="8" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="8" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                    </td>
                </tr>
            </table>
        </div>
    </div>--%>

    <script language="javascript">document.write("<div id='divEdit'style='display:none;height:"+document.body.clientHeight*84/100+"'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    </form>
</body>
</html>
