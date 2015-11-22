<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KqmLeaveQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KqmLeaveQryForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KqmKaoQinQryForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
        }
    </style>

    <script type="text/javascript" language="javascript">
       function setSelector(ctrlCode,ctrlName,moduleCode)
       {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;
       }    
     //2011-8-19 工號可以多選查詢
        function ShowBatchWorkNo() {
            document.all("pnlBatchWorkNo").style.display="";
            document.all("pnlBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtWorkNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;}
        
         function HiddenBatchWorkNo() {
            document.all("pnlBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";}
        
       $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){
                $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");}) 
       });
      $(function(){
        $("#tr_show").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");}) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");}) 
   });
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
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
                            <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_1">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area">
                                            <tr class="tr_data_1">
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblReGetDepcode" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" class="input_textBox_1"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblLeaveStatusName" runat="server" Text="StatusName"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="ddlEmpStatus" Width="100" RepeatColumns="1" DropImageSrc="../../CSS/Images/expand.gif"
                                                        TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" class="input_textBox_1">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="8%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLeaveQryDate" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtKQDateFrom" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtKQDateTo" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblEmployeeNo" runat="server" Text="Label"></asp:Label>
                                                    <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                    </asp:Image>
                                                </td>
                                                <td width="25%">
                                                    <asp:TextBox ID="txtWorkNo" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    <div id="pnlBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                        z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                        border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                        border-bottom: #0000ff 1px solid; position: absolute; left: 38%; float: left;
                                                        display: none;">
                                                        <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                                    <tr>
                                                                                        <td width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                            <font color="red">Ⅹ</font>
                                                                                            <asp:Label ID="Labelquerybatchworkno" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="100%">
                                                                                            <asp:TextBox ID="txtBatchEmployeeNo" runat="server" TextMode="MultiLine" Height="100"
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
                                                <td width="10%">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    <asp:TextBox ID="txtLocalName" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblleaveQryStatus" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" class="input_textBox_1" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblLeaveType" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="ddlLeaveType" class="input_textBox_1" Width="300" RepeatColumns="3"
                                                        DropImageSrc="../../CSS/Images/expand.gif" DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblApprover" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApprover" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                            </asp:Button>
                            <asp:Button ID="btnReset" runat="server" class="button_2" OnClick="btnReset_Click">
                            </asp:Button>
                            <asp:Button ID="btnExport" runat="server" class="button_2" OnClick="btnExport_Click">
                            </asp:Button>
                        </asp:Panel>
                    </td>
                    <td>
                        <input id="DefaultEmployeeNo" runat="server" name="DefaultEmployeeNo" type="hidden" />
                        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" id="tr_show" class="tr_title_center">
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
                                ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                ShowMoreButtons="false" DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px"
                                ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <img class="img3" id="tr_showtd" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveQry" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridLeaveQry_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridLeaveQry" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
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
                                    <ActivationObject BorderColor="" BorderWidth="">
                                    </ActivationObject>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="KQM_LeaveQryData" Key="KQM_LeaveQryData">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                Key="DepName" Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvDepName%>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo%>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" HeaderText="LVTypeCode" IsBound="false"
                                                Key="LVTypeCode" Hidden="true">
                                                <Header Caption="LVTypeCode" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LeaveType" HeaderText="LeaveType" IsBound="false"
                                                Key="LeaveType" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveType%>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="STime" HeaderText="STime" IsBound="false"
                                                Key="STime" Width="120">
                                                <Header Caption="<%$Resources:ControlText,gvSTime%>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ETime" HeaderText="ETime" IsBound="false"
                                                Key="ETime" Width="120">
                                                <Header Caption="<%$Resources:ControlText,gvETime%>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotal" HeaderText="LVTotal" IsBound="false"
                                                Key="LVTotal" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvLVTotal%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ThisLVTotal" HeaderText="ThisLVTotal" IsBound="False"
                                                Key="ThisLVTotal" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvThisLVTotal%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotalDays" HeaderText="LVTotalDays" IsBound="False"
                                                Key="LVTotalDays" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvLVTotalDays%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Proxy" HeaderText="Proxy" IsBound="false"
                                                Key="Proxy" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvProxy%>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Reason" HeaderText="Reason" IsBound="false"
                                                Key="Reason" Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Approver" HeaderText="Approver" IsBound="false"
                                                Key="Approver" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvApprover%>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                Key="StatusName" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="id" HeaderText="id" IsBound="false" Key="id"
                                                Hidden="true">
                                                <Header Caption="id">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" HeaderText="StartDate" IsBound="false"
                                                Key="StartDate" Hidden="true">
                                                <Header Caption="StartDate">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" HeaderText="EndDate" IsBound="false"
                                                Key="EndDate" Hidden="true">
                                                <Header Caption="EndDate">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                        </td>
                        <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                            &nbsp;
                        </td>
                        <tr>
                        </tr>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
