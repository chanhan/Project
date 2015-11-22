<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMSelfServiceForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KQMSelfServiceForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMSelfServiceForm</title>
</head>
<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

<script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

<script>

function openEditWin(strUrl,strName,winWidth,winHeight)
{
    var newWindow;
    newWindow=window.open(strUrl,strName,'width='+winWidth+', height='+winHeight+',left='+(screen.width-winWidth)/2.2+', top='+(screen.height-winHeight)/2.2+',resizable=yes, help=no, menubar=no,scrollbars=yes,status=yes,toolbar=no'); 
   newWindow.oponer=window;
	newWindow.focus(); 
}

function checkEmpno()
{
if($.trim($('#<%=txtWorkNo.ClientID %>').val())=='')
{
 alert(Message.EmpNoInput);
 return false;
}
return true;
}
$(function(){
              $("#img_grid_1").toggle(
                function(){
                    $("#div_1").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_1").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_2").toggle(
                function(){
                    $("#div_2").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_2").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_3").toggle(
                function(){
                    $("#div_3").hide();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_3").show();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_4").toggle(
                function(){
                    $("#div_4").hide();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_4").show();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
              $("#img_grid_5").toggle(
                function(){
                    $("#div_5").hide();
                    $("#div_img_5").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_5").show();
                    $("#div_img_5").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
        
              $("#img_grid_6").toggle(
                function(){
                    $("#div_6").hide();
                    $("#div_img_6").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_6").show();
                    $("#div_img_6").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
                $("#img_grid_7").toggle(
                function(){
                    $("#div_7").hide();
                    $("#div_img_7").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#div_7").show();
                    $("#div_img_7").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
})
</script>

<body>
    <form id="form1" runat="server">
    <input id="hidWorkNo" type="hidden" name="hidWorkNo" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="td_label" width="10%">
                    &nbsp;
                    <asp:Label ID="lbllWorkNo" runat="server">EmployeeNo:</asp:Label>
                </td>
                <td class="td_label" width="15%">
                    <asp:TextBox ID="txtWorkNo" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                </td>
                <asp:Panel ID="pnlShowPanel" runat="server">
                    <td class="td_label" width="75%" align="left">
                        &nbsp;
                        <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="button_1" OnClientClick="return checkEmpno();"
                            OnClick="btnQuery_Click" ToolTip="Authority Code:Query"></asp:Button>
                    </td>
                </asp:Panel>
            </tr>
        </table>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_1">
                    <td style="width: 100%;" class="tr_title_center" >
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmpInfo" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_1">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="td_label" width="12%">
                        &nbsp;
                        <asp:Label ID="lblEmployeeNo" runat="server">EmployeeNo:</asp:Label>&nbsp;
                    </td>
                    <td class="td_input" width="18%">
                        <asp:TextBox ID="txtEmployeeNo" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" width="12%" style="height: 23px">
                        &nbsp;
                        <asp:Label ID="lblLevel" runat="server">Level:</asp:Label>
                    </td>
                    <td class="td_input" width="18%" style="height: 23px">
                        <asp:TextBox ID="txtLevel" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" style="height: 24px; width: 12%;">
                        &nbsp;
                        <asp:Label ID="lblJoinBGDate" runat="server">JoinBGDate:</asp:Label>
                    </td>
                    <td class="td_label" style="width: 18%; height: 24px">
                        <asp:TextBox ID="txtJoinBGDate" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label" width="12%">
                        &nbsp;
                        <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>&nbsp;
                    </td>
                    <td class="td_input" width="18%">
                        <asp:TextBox ID="txtLocalName" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" style="width: 12%; height: 23px">
                        &nbsp;
                        <asp:Label ID="lblTechnical" runat="server">Technical:</asp:Label>
                    </td>
                    <td class="td_input" width="18%" style="height: 23px">
                        <asp:TextBox ID="txtTechnical" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" style="width: 12%; height: 23px">
                        &nbsp;
                        <asp:Label ID="lblSex" runat="server">Sex:</asp:Label>
                    </td>
                    <td class="td_input" width="18%" style="height: 23px">
                        <asp:TextBox ID="txtSex" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label" width="12%">
                        &nbsp;
                        <asp:Label ID="lblBUDepName" runat="server">BUDepName:</asp:Label>&nbsp;
                    </td>
                    <td class="td_input" width="18%">
                        <asp:TextBox ID="txtBUDepName" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" width="12%" style="height: 23px">
                        &nbsp;
                        <asp:Label ID="lbllblDepName" runat="server">DepName:</asp:Label>
                    </td>
                    <td class="td_input" width="18%" style="height: 23px">
                        <asp:TextBox ID="txtDepName" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        &nbsp;
                        <asp:Label ID="lblPlaceName" runat="server">PlaceName:</asp:Label>&nbsp;
                    </td>
                    <td class="td_input" colspan="5">
                        <asp:TextBox ID="txtPlaceName" Width="100%" runat="server" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        &nbsp;
                        <asp:Label ID="lblEffectBellNo" runat="server">EffectBellCard:</asp:Label>&nbsp;
                    </td>
                    <td class="td_input" colspan="5">
                        <asp:TextBox ID="txtEffectBellNo" TextMode="MultiLine" Rows="3" Width="100%" runat="server"
                            CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_2">
                    <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblKaoQinUnusualDetails" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_2">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridKaoQinQuery" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridKaoQinQuery_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridKaoQinQuery" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_KaoQinData" Key="KQM_KaoQinData">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" HeaderText="KQDate" IsBound="false"
                                                Key="KQDate" Width="9%" Format="yyyy/MM/dd">
                                                <Header Caption="<%$Resources:ControlText,gvKQDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                Key="StatusName" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="false"
                                                Key="ShiftDesc" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvgvShiftDesc%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" HeaderText="OnDutyTime" IsBound="false"
                                                Key="OnDutyTime" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvOnDutyTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" HeaderText="OffDutyTime" IsBound="false"
                                                Key="OffDutyTime" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTOnDutyTime" AllowUpdate="No" HeaderText="OTOnDutyTime"
                                                IsBound="false" Key="OTOnDutyTime" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvOTOnDutyTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTOffDutyTime" AllowUpdate="No" HeaderText="OTOffDutyTime"
                                                IsBound="false" Key="OTOffDutyTime" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvOTOffDutyTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" HeaderText="AbsentQty" IsBound="false"
                                                Key="AbsentQty" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentQty%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionTypeName" HeaderText="ExceptionTypeName"
                                                IsBound="false" Key="ExceptionTypeName" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvExceptionTypeName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" HeaderText="ReasonRemark" IsBound="false"
                                                Key="ReasonRemark" Width="9%">
                                                <Header Caption="<%$Resources:ControlText,gvReasonRemark%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentTotal" HeaderText="AbsentTotal" IsBound="false"
                                                Key="AbsentTotal" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentTotal%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="false"
                                                Key="Status" Hidden="true">
                                                <Header Caption="Status">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" HeaderText="ShiftNo" IsBound="false"
                                                Key="ShiftNo" Hidden="true">
                                                <Header Caption="ShiftNo">
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
            <div style="width: 100%">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="img_grid_3">
                        <td style="width: 100%;" class="tr_title_center" id="td1">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPlusDemeritPoints" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div>
                                <img id="div_img_3" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="div_3">
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                                <igtbl:UltraWebGrid ID="WebGridScoreItem" runat="server" Width="100%" Height="100%"
                                    OnDataBound="WebGridScoreItem_DataBind">
                                    <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                        AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="WebGridScoreItem" TableLayout="Fixed"
                                        AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                        <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <FrameStyle Width="100%" Height="100%">
                                        </FrameStyle>
                                        <ClientSideEvents></ClientSideEvents>
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
                                            DataKeyField="" BaseTableName="PAM_EmpAssess" Key="PAM_EmpAssess">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="ANNUALCode" Key="ANNUALCode" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvANNUALCode%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvgvWorkNo%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LOCALNAME" Key="LOCALNAME" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvgvLOCALNAME%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQSICKLEAVE" Key="KQSICKLEAVE" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvKQSICKLEAVE%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQAFFAIRLEAVE" Key="KQAFFAIRLEAVE" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvKQAFFAIRLEAVE%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQLATE1" Key="KQLATE1" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQLATE1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQLATE2" Key="KQLATE2" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQLATE2%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQEARLY1" Key="KQEARLY1" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQEARLY1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQEARLY2" Key="KQEARLY2" IsBound="false" Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQEARLY2%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQABSENT1" Key="KQABSENT1" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQABSENT1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="KQABSENT2" Key="KQABSENT2" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvKQABSENT2%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APAWARD1" Key="APAWARD1" IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPAWARD1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APAWARD2" Key="APAWARD2" IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPAWARD2%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APAWARD3" Key="APAWARD3" IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPAWARD3%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APPUNISH1" Key="APPUNISH1" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPPUNISH1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APPUNISH2" Key="APPUNISH2" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPPUNISH2%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="APPUNISH3" Key="APPUNISH3" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvAPPUNISH3%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVELEVEL10" Key="IMPROVELEVEL10" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVELEVEL10%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVELEVEL89" Key="IMPROVELEVEL89" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVELEVEL89%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVELEVEL67" Key="IMPROVELEVEL67" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVELEVEL67%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVELEVEL15" Key="IMPROVELEVEL15" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVELEVEL15%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVEREALVALUE" Key="IMPROVEREALVALUE" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVEREALVALUE%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IMPROVETARGETVALUE" Key="IMPROVETARGETVALUE"
                                                    IsBound="false" Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvIMPROVETARGETVALUE%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="TRAINTARGETVALUE" Key="TRAINTARGETVALUE" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvTRAINTARGETVALUE%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="TRAINREALVALUE" Key="TRAINREALVALUE" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvTRAINREALVALUE%>">
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
            </div>
            <div style="width: 100%">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;">
                        <td style="width: 100%;" class="tr_title_center" id="td2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLeaveCount" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCountA" runat="server" CssClass="button_1" ToolTip="Authority Code:Count"
                                            OnClick="btnCountA_Click"></asp:Button>
                                    </td>
                                    <td>
                                        /
                                    </td>
                                    <td>
                                        <asp:Label ID="lblScheduling" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCountB" runat="server" CssClass="button_1" ToolTip="Authority Code:Count"
                                            OnClick="btnCountB_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;" id="img_grid_4_2">
                            <div>
                                <img id="div_img_4" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="div_4">
                    <table>
                        <tr>
                            <td width="60%">
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

                                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveReport" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridLeaveReport" TableLayout="Fixed"
                                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
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
                                                    <igtbl:UltraGridBand BaseTableName="TempTable" Key="TempTable">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeName" HeaderText="LVTypeName" IsBound="false"
                                                                Key="LVTypeName" Width="25%">
                                                                <Header Caption="<%$Resources:ControlText,gvLVTypeName%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StandardDays" HeaderText="StandardDays" IsBound="false"
                                                                Key="StandardDays" Width="25%">
                                                                <Header Caption="<%$Resources:ControlText,gvgvStandardDays%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="AlreadDays" HeaderText="AlreadDays" IsBound="false"
                                                                Key="AlreadDays" Width="25%">
                                                                <Header Caption="<%$Resources:ControlText,gvgvAlreadDays%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="LeaveDays" HeaderText="LeaveDays" IsBound="false"
                                                                Key="LeaveDays" Width="25%">
                                                                <Header Caption="<%$Resources:ControlText,gvgvLeaveDays%>">
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
                            </td>
                            <td width="40%">
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

                                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridWorkShift" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridWorkShift" TableLayout="Fixed"
                                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
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
                                                    <igtbl:UltraGridBand BaseTableName="TempTable" Key="TempTable">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" HeaderText="KQDate" IsBound="false"
                                                                Key="KQDate" Width="40%" Format="yyyy/MM/dd">
                                                                <Header Caption="<%$Resources:ControlText,gvKQDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="false"
                                                                Key="ShiftDesc" Width="60%">
                                                                <Header Caption="<%$Resources:ControlText,gvShift%>">
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
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_grid_5">
                    <td style="width: 100%;" class="tr_title_center" id="td3">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblThisYearLeaveDetial" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_5" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_5">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveDetail" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridLeaveDetail_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridLeaveDetail" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_LeaveQryData" Key="KQM_LeaveQryData">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="LeaveType" HeaderText="LeaveType" IsBound="false"
                                                Key="LeaveType" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveType%>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="STime" HeaderText="STime" IsBound="false"
                                                Key="STime" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvStartDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ETime" HeaderText="ETime" IsBound="false"
                                                Key="ETime" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvEndDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotal" HeaderText="LVTotal" IsBound="false"
                                                Key="LVTotal" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvgvLVTotal%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotalDays" HeaderText="LVTotalDays" IsBound="False"
                                                Key="LVTotalDays" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvLVTotalDays%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Proxy" HeaderText="Proxy" IsBound="false"
                                                Key="Proxy" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvProxy%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Reason" HeaderText="Reason" IsBound="false"
                                                Key="Reason" Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Approver" HeaderText="Approver" IsBound="false"
                                                Key="Approver" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvApprover%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                Key="StatusName" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName%>">
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

                            <script language="javascript">document.write("</DIV>");</script>

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
            <div style="width: 100%;">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="img_grid_6">
                        <td style="width: 100%;" class="tr_title_center" id="td4">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOTMMonthTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div>
                                <img id="div_img_6" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="div_6">
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridOTMMonthTotal" runat="server" Width="100%" Height="100%">
                                    <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                        AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMMonthTotal" TableLayout="Fixed"
                                        AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                        <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <FrameStyle Width="100%" Height="100%">
                                        </FrameStyle>
                                        <ClientSideEvents></ClientSideEvents>
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
                                        <igtbl:UltraGridBand BaseTableName="OTM_MonthTotal" Key="OTM_MonthTotal">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="YearMonth" IsBound="false" Key="YearMonth"
                                                    Width="6%">
                                                    <Header Caption="<%$Resources:ControlText,gvYearMonth%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                    Key="OverTimeType" Width="6%">
                                                    <Header Caption="<%$Resources:ControlText,gvOverTimeType%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G1Apply" HeaderText="G1Apply" IsBound="false"
                                                    Key="G1Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvG1Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G2Apply" HeaderText="G2Apply" IsBound="false"
                                                    Key="G2Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvG2Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G3Apply" HeaderText="G3Apply" IsBound="false"
                                                    Key="G3Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvG3Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G1RelSalary" HeaderText="G1RelSalary" IsBound="false"
                                                    Key="G1RelSalary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvG1RelSalary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                    Key="G2RelSalary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvG2RelSalary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G3RelSalary" HeaderText="G3RelSalary" IsBound="false"
                                                    Key="G3RelSalary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvG3RelSalary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG1Apply" HeaderText="SpecG1Apply" IsBound="false"
                                                    Key="SpecG1Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvSpecG1Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG2Apply" HeaderText="SpecG2Apply" IsBound="false"
                                                    Key="SpecG2Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvSpecG2Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG3Apply" HeaderText="SpecG3Apply" IsBound="false"
                                                    Key="SpecG3Apply" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvSpecG3Apply%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG1Salary" HeaderText="SpecG1Salary" IsBound="false"
                                                    Key="SpecG1Salary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvSpecG1Salary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG2Salary" HeaderText="SpecG2Salary" IsBound="false"
                                                    Key="SpecG2Salary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvSpecG2Salary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SpecG3Salary" HeaderText="SpecG3Salary" IsBound="false"
                                                    Key="SpecG3Salary" Width="5%">
                                                    <Header Caption="<%$Resources:ControlText,gvgvSpecG3Salary%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="G2Remain" HeaderText="G2Remain" IsBound="false"
                                                    Key="G2Remain" Width="6%">
                                                    <Header Caption="<%$Resources:ControlText,gvG2Remain%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="false"
                                                    Key="MAdjust1" Width="7%">
                                                    <Header Caption="<%$Resources:ControlText,gvMAdjust1%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MRelAdjust" HeaderText="MRelAdjust" IsBound="false"
                                                    Key="MRelAdjust" Width="7%">
                                                    <Header Caption="<%$Resources:ControlText,gvMRelAdjust%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApproveFlagName" HeaderText="ApproveFlagName"
                                                    IsBound="false" Key="ApproveFlagName" Width="6%">
                                                    <Header Caption="<%$Resources:ControlText,gvApproveFlagName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" IsBound="false" Key="ApproveFlag"
                                                    Hidden="true">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <Header Caption="ApproveFlag">
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
            </div>
            <div style="width: 100%;">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="img_grid_7">
                        <td style="width: 100%;" class="tr_title_center" id="td5">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOTMonthDetail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div>
                                <img id="div_img_7" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="div_7">
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/500+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridOTMonthDetail" runat="server" Width="100%" Height="100%"
                                    OnDataBound="UltraWebGridOTMonthDetail_DataBound">
                                    <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                        AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMonthDetail" TableLayout="Fixed"
                                        AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                        <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <FrameStyle Width="100%" Height="100%">
                                        </FrameStyle>
                                        <ClientSideEvents></ClientSideEvents>
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
                                            DataKeyField="" BaseTableName="OTM_RealApply" Key="OTM_RealApply">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80"
                                                    AllowUpdate="No" Format="yyyy/MM/dd">
                                                    <Header Caption="<%$Resources:ControlText,gvOTDate%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="40"
                                                    AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvOTType%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="50"
                                                    AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvWeek%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                    Width="50" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvgvOverTimeType%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                    Width="130" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvShiftDesc%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="AdvanceTime" Key="AdvanceTime" IsBound="false"
                                                    Width="80" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvAdvanceTime%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="AdvanceHours" Key="AdvanceHours" IsBound="false"
                                                    Width="40" Format="0.0" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvAdvanceHours%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="OverTimeSpan" Key="OverTimeSpan" IsBound="false"
                                                    Width="80" Format="0.0" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gOverTimeSpanv%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="RealHours" Key="RealHours" IsBound="false"
                                                    Width="40" Format="0.0" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvRealHours%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ConfirmHours" Key="ConfirmHours" IsBound="false"
                                                    Width="60" Format="0.0" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvConfirmHours%>">
                                                    </Header>
                                                    <CellStyle BackColor="AliceBlue">
                                                    </CellStyle>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ConfirmRemark" Key="ConfirmRemark" IsBound="false"
                                                    Width="150" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvConfirmRemark%>">
                                                    </Header>
                                                    <CellStyle BackColor="AliceBlue">
                                                    </CellStyle>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="250"
                                                    AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvWorkDesc%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvIsProject%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                    Width="60" AllowUpdate="No">
                                                    <Header Caption="<%$Resources:ControlText,gvgvStatusName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="250">
                                                    <Header Caption="<%$Resources:ControlText,gvRemark%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="false"
                                                    Width="0" Hidden="true">
                                                    <Header Caption="OTMSGFlag">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="0"
                                                    Hidden="true">
                                                    <Header Caption="Status">
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
