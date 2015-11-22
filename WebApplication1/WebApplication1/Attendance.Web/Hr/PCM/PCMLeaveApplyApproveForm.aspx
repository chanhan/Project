<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMLeaveApplyApproveForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMLeaveApplyApproveForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCMLeaveApplyApproveForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--
        function ShowBillDetail(BillNo) {
            var ModuleCode = igtbl_getElementById("HiddenModuleCode").value;
            var width = screen.width * 0.7;
            var height = screen.height * 0.6;
            openEditWin("PCMLeaveApproveCenterForm.aspx?BillNo=" + BillNo + "&ModuleCode=" + ModuleCode + "&OPENTYPE=PCMLeaveApproveCenter", "PCMLeaveApproveCenter", width, height);
        }
        function OpenApprove(isApproved)//彈出新增或修改頁面
        {
            var ModuleCode = igtbl_getElementById("HiddenModuleCode").value;
            if (isApproved) {
                if (document.getElementById("iframeApproved").src.length == 0) {
                    document.getElementById("iframeApproved").src = "PCMLeaveApplyApprovedForm.aspx?ModuleCode=" + ModuleCode;
                }
                document.getElementById("topTable").style.display = "none";
                document.getElementById("divApproved").style.display = "";
                document.getElementById("ButtonApproved").className = "selected_button";
                document.getElementById("ButtonApprove").className = "audit_button";
            }
            else {
                //document.getElementById("iframeApproved").src="";
                document.getElementById("topTable").style.display = "";
                document.getElementById("divApproved").style.display = "none";
                document.getElementById("ButtonApproved").className = "audit_button";
                document.getElementById("ButtonApprove").className = "selected_button";
            }
            return false;
        }
        function openEditWin(strUrl, strName, winWidth, winHeight) {
            var newWindow;
            newWindow = window.open(strUrl, strName, 'width=' + winWidth + ', height=' + winHeight + ',left=' + (screen.width - winWidth) / 2.2 + ', top=' + (screen.height - winHeight) / 2.2 + ',resizable=yes, help=no, menubar=no,scrollbars=yes,status=yes,toolbar=no');
            newWindow.oponer = window;
            newWindow.focus();
        }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <table cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td id="Td1" width="100%" runat="server">
                            <asp:Button ID="ButtonApprove" Width="100px" Height="26px" runat="server" CssClass="selected_button"
                                Text="Approve" ToolTip="Authority Code:Approve" CommandName="Approve" OnClientClick="return OpenApprove(false)">
                            </asp:Button>
                            <asp:Button ID="ButtonApproved" Width="100px" Height="26px" runat="server" CssClass="audit_button"
                                Text="Approved" ToolTip="Authority Code:Approved" CommandName="Approved" OnClientClick="return OpenApprove(true)">
                            </asp:Button>
                          <font color="red"><asp:Label ID="pcm_leaveapply_aproveremark" runat="server" Text="pcm_leaveapply_aproveremark"></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr2">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblwqh" runat="server" Text="lblwqh"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="tr_title_center">
                                                    <div>
                                                        <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                            HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                            DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                            ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                        </ess:AspNetPager>
                                                    </div>
                                                </td>
                                                <td style="width: 22px;">
                                                    <div id="Div2">
                                                        <img id="img1" class="img1" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script language="javascript">                                            document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 75 / 100 + "'>");</script>

                                        <igtbl:UltraWebGrid ID="UltraWebGridLeaveApply" runat="server" Width="100%" Height="100%"
                                            OnDataBound="UltraWebGridLeaveApply_DataBound">
                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridLeaveApply" TableLayout="Fixed"
                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                    CssClass="tr_header">
                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                    </BorderDetails>
                                                </HeaderStyleDefault>
                                                <FrameStyle Width="100%" Height="100%">
                                                </FrameStyle>
                                                <ClientSideEvents InitializeLayoutHandler="UltraWebGridLeaveApply_InitializeLayoutHandler"
                                                    AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="UltraWebGridEvectionApply_DblClickHandler">
                                                </ClientSideEvents>
                                                <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                <igtbl:UltraGridBand BaseTableName="KQM_LeaveApply" Key="KQM_LeaveApply">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80px"
                                                            HeaderText="WorkNo">
                                                            <Header Caption="<%$Resources:ControlText,gvAttendHandleWorkNo%>" Fixed="True">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                            Width="80px" HeaderText="LocalName">
                                                            <Header Caption="<%$Resources:ControlText,gvAttendHandleLocalName%>" Fixed="True">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="SEXName" Key="SEXName" IsBound="false" Width="40px"
                                                            HeaderText="SEXName">
                                                            <Header Caption="<%$Resources:ControlText,gvSex%>">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Width="100px"
                                                            HeaderText="buName">
                                                            <Header Caption="<%$Resources:ControlText,gvBUName%>">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="100px"
                                                            HeaderText="DEPNAME">
                                                            <Header Caption="<%$Resources:ControlText,gvDepName%>">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LVTypeName" Key="LVTypeName" IsBound="false"
                                                            Width="80px" HeaderText="LVTypeName">
                                                            <Header Caption="<%$Resources:ControlText,gvLVTypeName%>">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                            Width="80px" HeaderText="StartDate">
                                                            <Header Caption="<%$Resources:ControlText,gvStartDate%>">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                            Width="60px" HeaderText="StartTime">
                                                            <Header Caption="<%$Resources:ControlText,gvStartTime%>">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="80px"
                                                            HeaderText="EndDate">
                                                            <Header Caption="<%$Resources:ControlText,gvEndDate%>">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="60px"
                                                            HeaderText="EndTime">
                                                            <Header Caption="<%$Resources:ControlText,gvEndTime%>">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LVTotal" Key="LVTotal" IsBound="false" Width="50px"
                                                            HeaderText="LVTotal">
                                                            <Header Caption="<%$Resources:ControlText,gvLVTotal%>">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Reason" Key="Reason" IsBound="false" Width="100px"
                                                            HeaderText="Reason">
                                                            <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ProxyWorkNo" Key="ProxyWorkNo" IsBound="false"
                                                            Width="70px" HeaderText="ProxyWorkNo">
                                                            <Header Caption="<%$Resources:ControlText,gvProxyWorkNo%>">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Proxy" Key="Proxy" IsBound="false" Width="70px"
                                                            HeaderText="Proxy">
                                                            <Header Caption="<%$Resources:ControlText,gvProxyName%>">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ProxyNotes" Key="ProxyNotes" IsBound="false"
                                                            Width="180px" HeaderText="ProxyNotes">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadProxyNotes%>">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ProxyStatusName" Key="ProxyStatusName" IsBound="false"
                                                            Width="100" HeaderText="ProxyStatusName">
                                                            <Header Caption="<%$Resources:ControlText,gvProxyStatusName%>">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="100px"
                                                            HeaderText="Remark">
                                                            <Header Caption="<%$Resources:ControlText,gvRemark%>">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ApplyTypeName" Key="ApplyTypeName" IsBound="false"
                                                            Width="60px" HeaderText="ApplyTypeName">
                                                            <Header Caption="<%$Resources:ControlText,gvApplyTypeName%>">
                                                                <RowLayoutColumnInfo OriginX="15" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="15" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                            Width="50px" HeaderText="StatusName">
                                                            <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                                <RowLayoutColumnInfo OriginX="16" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="16" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="70px"
                                                            HeaderText="Modifier">
                                                            <Header Caption="<%$Resources:ControlText,gvModifier%>">
                                                                <RowLayoutColumnInfo OriginX="17" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="17" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                            Width="110px" HeaderText="ModifyDate">
                                                            <Header Caption="<%$Resources:ControlText,gvModifyDate%>">
                                                                <RowLayoutColumnInfo OriginX="18" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="18" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="True"
                                                            HeaderText="Status">
                                                            <Header Caption="Status">
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="dcode" Key="dcode" IsBound="false" Hidden="True"
                                                            HeaderText="dcode">
                                                            <Header Caption="dcode">
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" Key="LVTypeCode" IsBound="false"
                                                            Hidden="True" HeaderText="LVTypeCode">
                                                            <Header Caption="LVTypeCode">
                                                                <RowLayoutColumnInfo OriginX="21" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="21" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LevelCode" Key="LevelCode" IsBound="false"
                                                            Hidden="true">
                                                            <Header Caption="LevelCode">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ManagerCode" Key="ManagerCode" IsBound="false"
                                                            Hidden="true">
                                                            <Header Caption="ManagerCode">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ProxyStatus" Key="ProxyStatus" IsBound="false"
                                                            Hidden="true">
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                        </igtbl:UltraWebGrid>

                                        <script language="JavaScript" type="text/javascript">                                            document.write("</DIV>");</script>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript">        document.write("<div id='divApproved' style='display:none;height:" + document.body.clientHeight * 79 / 100 + "'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeApproved" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0px;"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">        document.write("</div>");</script>

    <asp:Button ID="ButtonReset" Width="0" runat="server" Text="Reset" ToolTip="Authority Code:Reset"
        CommandName="Reset" OnClick="ButtonReset_Click"></asp:Button>
    </form>
</body>
</html>
