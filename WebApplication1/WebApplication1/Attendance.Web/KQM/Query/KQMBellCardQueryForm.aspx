<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMBellCardQueryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KQMBellCaedQueryForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>KQMBellCardQueryForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript"><!--

$(function(){
 $("#tr_edit").toggle(function(){
 $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
 $("#div_select").hide()},function(){
 $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
 $("#div_select").show()});
 
 $("#div_img_2,#td_show_1,#td_show_2").toggle(function(){
 $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif")
 $("#div_showdata").hide()},function(){
 $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif")
 $("#div_showdata").show()});
 
})

function OpenImport()
{
    document.all("PanelImport").style.display="";
    return false;
}

function HiddenPanel() 
{
    document.all("PanelImport").style.display="none";
    return false;
}
 
function valDate(M,D,Y){   
      Months=new Array(31,28,31,30,31,30,31,31,30,31,30,31);   
      Leap=false;   
      if((Y%4==0)&&((Y%100!=0)||(Y%400==0)))   
      Leap=true;   
      if((D<1)||(D>31)||(M < 1)||(M>12)||(Y < 0))   
      return(false);   
      if((D> Months[M-1])&&!((M==2)&&(D>28)))   
      return(false);   
      if(!(Leap)&&(M==2)&&(D>28))   
      return(false);   
      if((Leap)&&(M==2)&&(D>29))   
      return(false);   
      };      

function checkdata()
{
//var obj1=document.all.ddlBellNo;
//var BellNo=obj1.options[obj1.selectedIndex].value; 
var workno=document.getElementById("txtWorkNo").value;
var datefrom=document.getElementById("txtFromDate").value;
var dateto=document.getElementById("txtToDate").value;
var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
if(datefrom!=""&&datefrom!=null)
{
if(!check.test(datefrom))
  {
   alert(Message.WrongDate);
   return false;
  }
if(formatDate(datefrom)==false)
{
  alert(Message.WrongDate);
  return false;
}
}
else
{
  alert(Message.CardDateNotNull);
  return false;
}
if(dateto!=""&&dateto!=null)
{
if(!check.test(dateto))
  {
   alert(Message.WrongDate);
   return false;
  }
if(formatDate(dateto)==false)
{
  alert(Message.WrongDate);
  return false;
}
}
else
{
 alert(Message.CardDateNotNull);
  return false;
}
if(workno==""||workno==null)
{
 alert(Message.WorkNoNotNull);
  return false;
}
}

function   formatDate(date){   
cDate   =   date;   
dSize   =   cDate.length;   
sCount=   0;   


idxBarI   =   cDate.indexOf( "/");   
idxBarII=   cDate.lastIndexOf( "/");   
strY   =   cDate.substring(0,idxBarI);   
strM   =   cDate.substring(idxBarI+1,idxBarII);   
strD   =   cDate.substring(idxBarII+1,dSize);   
ok   =   valDate(strM,   strD,   strY);   
if(ok==false){          
return(false);   
};   
};   


		--></script>

<body>
    <form id="Form1" method="post" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%; cursor: hand;" id="tr_edit">
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
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblKQMBellNo" runat="server">BellNo:</asp:Label>
                                            </td>
                                            <td width="23%">
                                                <asp:DropDownList ID="ddlBellNo" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="4">
                                                &nbsp;
                                                <asp:CheckBox ID="chkHisFlag" runat="server" Visible="true" />
                                                <asp:Label ID="lblHisFlag" runat="server" Text="HisFlag" ForeColor="RED" Visible="true"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lbl_empno" runat="server">WorkNo:</asp:Label>
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                            </td>
                                            <td width="23%">
                                                <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblCardDate" runat="server">CardDate:</asp:Label>
                                            </td>
                                            <td width="27%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="49%">
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="2%">
                                                            ~
                                                        </td>
                                                        <td width="49%">
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 90px">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <asp:Panel ID="pnlShowPanel" runat="server">
                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClientClick="return checkdata();"
                                        OnClick="btnQuery_Click"></asp:Button>
                                    <asp:Button ID="btnReset" runat="server" CssClass="button_1" OnClick="btnReset_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click">
                                    </asp:Button>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%">
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
                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" ShowMoreButtons="false"
                            CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
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

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*64/100+";'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridBellCardQuery" runat="server" Width="100%" Height="100%">
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
                                <igtbl:UltraGridBand BaseTableName="gds_att_bellcarddata_v" Key="gds_att_bellcarddata_v">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" IsBound="false" Key="WorkNo" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" IsBound="false" Key="LocalName"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvLocalName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DepName" IsBound="false" Key="DepName" Width="15%">
                                            <Header Caption="<%$Resources:ControlText,gvDName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CardTime" IsBound="false" Key="CardTime" Width="10%"
                                            Format="yyyy/MM/dd">
                                            <Header Caption="<%$Resources:ControlText,gvCardTime %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CardTimeMM" IsBound="false" Key="CardTimeMM"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvCardTimeMM %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CardNo" IsBound="false" Key="CardNo" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvCardNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="BellNo" IsBound="false" Key="BellNo" Width="15%">
                                            <Header Caption="<%$Resources:ControlText,gvBellNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AddRess" IsBound="false" Key="AddRess" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvAddRess %>">
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
    </form>
</body>
</html>
