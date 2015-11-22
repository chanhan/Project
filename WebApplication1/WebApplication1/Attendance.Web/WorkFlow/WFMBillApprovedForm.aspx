<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFMBillApprovedForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WFMBillApprovedForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
   
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css"/>
     <script type="text/javascript"><!--  
        function ShowBillDetail(BillNo,BillTypeCode)
		{
	        var ModuleCode = igtbl_getElementById("ModuleCode").value;
	        var width=screen.width*0.95;
            var height=screen.height*0.8;          
            switch (BillTypeCode)
            {
                case "LeaveTypeAE":
                case "LeaveTypeAF":
                case "LeaveTypeAG":
                case "LeaveTypeAH":
                case "LeaveTypeAI":
                case "LeaveTypeAJ":
                case "LeaveTypeAK":
                case "LeaveTypeM":
                case "LeaveTypeN":
                case "LeaveTypeL":
                    openEditWin("WFMBillCenterLeaveApproveForm.aspx?SFlag=Y&BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
                    break;
                default:
                    window.open("WFMBillCenterApproveForm.aspx?SFlag=Y&BillNo=" + BillNo + "&ModuleCode=" + ModuleCode, 'WFMBillDetail', 'height=' + height + ', width=' + width + ', top=0,left=0, toolbar=no, menubar=no, scrollbars=yes,resizable=no,location=no, status=no');
                    //openEditWin("WFMBillCenterApproveForm.aspx?SFlag=Y&BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
                    break;
            }
            //openEditWin("WFMBillDetailForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
        }

        //單位樹
        function GetTreeDataValue(ReturnValueBoxName, moduleCode, ReturnDescBoxName) {
            var windowWidth = 500, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("../HRM/EmployeeData/TreeDataPickForm.aspx?modulecode=" + moduleCode, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=yes");
            if (Revalue != undefined) {
                var arrValue = Revalue.split(";");
                document.all(ReturnValueBoxName).value = arrValue[0];
                if (arrValue.length > 1) {
                    document.all(ReturnDescBoxName).innerText = arrValue[1];
                }
            }
        }
	--></script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
        <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center">
            <tr>
                <td>
                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                        <tr>
                            <td>
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                       <td colspan="2">
                                        
                                        <div style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr2">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                 <asp:Label ID="lblCondition" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <div id="Div2">
                                                        <img id="img1" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="div_1">
                                                <table cellspacing="0" cellpadding="0" width="100%" id="TABLE1">
                                                    <tr>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lbl_doctype" runat="server">BillTypeCode:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            
                                                            <cc1:DropDownCheckList ID="ddlBillTypeCode" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                                                                    Width="250" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                                                                    TextWhenNoneChecked="" DisplayTextWidth="250" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                                                                runat="server">
                                                            </cc1:DropDownCheckList>
                                                            
                                                            </td>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lbl_Unit" runat="server" Text="Label"></asp:Label></td>
                                                        <td class="td_input" width="23%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                            Style="display: none"></asp:TextBox></td>
                                                                    <td width="100%">
                                                                        <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox></td>
                                                                    <td style="cursor: hand">
                                                                        <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/images/zoom.png"></asp:Image></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="lbllblBillNo" runat="server" Text="BillNo"></asp:Label>
                                                        </td>
                                                        <td class="td_input">
                                                            <asp:TextBox ID="textBoxBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="lbl_Status" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td class="td_input">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                                                            </asp:DropDownList></td>
                                                        <td class="td_label" width="8%">
                                                            &nbsp;
                                                            <asp:Label ID="lbl_cbrq" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="28%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td width="50%">
                                                                        <asp:TextBox ID="textBoxApplyDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        ~</td>
                                                                    <td width="50%">
                                                                        <asp:TextBox ID="textBoxApplyDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" colspan="6">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <asp:Button ID="ButtonQuery"  
                                                                            runat="server"  Text="Query" ToolTip="Authority Code:Query"
                                                                            CommandName="Query" OnClick="ButtonQuery_Click"></asp:Button>
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                    <td colspan="3">
                                       <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                            <tr style="cursor: hand">
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                                    background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                                    font-size: 13px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="labapproved" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="tr_title_center" style="width: 300px;">
                                                                <div>
                                                                    <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                                        HorizontalAlign="Center" PageSize="10" PagingButtonType="Image" Width="300px"
                                                                        ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                        DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                                        ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                                                        OnPageChanged="pager_PageChanged"  ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                                    </ess:AspNetPager>
                                                                </div>
                                                            </td>
                                                            <td style="width: 22px;" id="td_show_2">
                                                                <img id="div_img2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">

                                            <script language="JavaScript" type="text/javascript">                                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 70 / 100 + "'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%"
                                                OnDataBound="UltraWebGridBill_DataBound">
                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="No" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridBill" TableLayout="Fixed"
                                                    CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                        CssClass="tr_data">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                    <ActivationObject BorderColor="" BorderWidth="">
                                                    </ActivationObject>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand BaseTableName="WFM_Bill" Key="WFM_Bill">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillTypeName" HeaderText="BillTypeName" IsBound="True"
                                                                Key="BillTypeName" Width="18%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_doctype %>" Fixed="true">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" HeaderText="BillNo" IsBound="True"
                                                                Key="BillNo" Width="15%">
                                                                <Header Caption="<%$Resources:ControlText,lbllblBillNo %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgName" HeaderText="OrgName" IsBound="True"
                                                                Key="OrgName" Width="34%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_Unit_edit %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="True"
                                                                Key="StatusName" Width="8%">
                                                                <Header Caption="<%$Resources:ControlText,lblKaoqinStatus %>">
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyManName" HeaderText="ApplyManName" IsBound="True"
                                                                Key="ApplyManName" Width="8%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_cbr %>">
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyDate" HeaderText="ApplyDate" IsBound="True"
                                                                Key="ApplyDate" Width="17%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_cbrq %>">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="True"
                                                                Key="Status" Hidden="true">
                                                                <Header Caption="Status">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgCode" HeaderText="OrgCode" IsBound="True"
                                                                Key="OrgCode" Hidden="true">
                                                                <Header Caption="OrgCode">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillTypeCode" HeaderText="BillTypeCode" IsBound="True"
                                                                Key="BillTypeCode" Hidden="true">
                                                                <Header Caption="BillTypeCode">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">                                                document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
