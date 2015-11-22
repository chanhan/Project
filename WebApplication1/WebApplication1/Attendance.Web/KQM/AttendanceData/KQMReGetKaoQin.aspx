<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMReGetKaoQin.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.KQMReGetKaoQin" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMReGetKaoQinForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
        }
    </style>

    <script type="text/javascript"><!--
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
        function UpProgress()
		{
			document.getElementById("btnCount").style.display="none";			
		}
        function ShowBatchWorkNo() {
            document.all("pnlBatchWorkNo").style.display="";
            document.all("pnlBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtWorkNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        function HiddenBatchWorkNo() {
            document.all("pnlBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";
        }
        $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
           $(function(){
       $("#tr_show").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
   });
   function CheckDate()
   {
      var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
      var FromDate= $("#<%=txtKQDateFrom.ClientID%>").val();
      var ToDate=$("#<%=txtKQDateTo.ClientID %>").val();
      if (FromDate!=null&&FromDate!="")
      {
         if(!check.test(FromDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtKQDateFrom.ClientID%>").val("");
           return false;
         }
      }
      if (ToDate!=null&&ToDate!="")
      {
         if(!check.test(ToDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtKQDateTo.ClientID %>").val("");
           return false;
         }
      }
      if((FromDate!=null&&FromDate!="")&&(ToDate!=null&&ToDate!=""))
      {
        if(ToDate<FromDate)
        {
           alert(Message.ToLaterThanFrom);
           $("#<%=txtKQDateTo.ClientID %>").val("");
           return false;
        }
      }
      return true;
   };
   function query_click()
   {
      var fromDate= $("#<%=txtKQDateFrom.ClientID%>").val();
      var toDate=$("#<%=txtKQDateTo.ClientID %>").val();
      var employeeNo=$("#<%=txtWorkNo.ClientID %>").val();
      if(fromDate.length==0||toDate.length==0)
      {alert(Message.KQDateNotNull);return false;}
      else
      {
          if(employeeNo.length!=0)
             {return true;}
             else
             { var result=0;
                     $.ajax({type: "post", url: "KQMReGetKaoQin.aspx", dataType: "text", data: {FromDate: fromDate, ToDate: toDate,EmployeeNo:employeeNo},async:false,
                             success: function(msg) {
                                    if (msg==1) {alert('您只能查詢兩個月內的數據！');result=0; } 
                                    else{result=1;}}
                           }); 
                      if(result==0){return false;} 
                      else{return true;}  
             }
        }       
   }
	--></script>

</head>
<body onload="return Load()">
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
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
            <table cellspacing="0" cellpadding="0" width="100%" id="TABLE1">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area">
                                            <tr class="tr_data_1">
                                                <td>
                                                    <asp:Label ID="lblReGetDepcode" runat="server" Text="DepCode">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" class="input_textBox_1"
                                                                    Style="display: none">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepName" runat="server" class="input_textBox_1" Width="100%">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="10%">
                                                    <asp:Label ID="lblCountType" runat="server" Text="CountType">
                                                    </asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddlCountType" runat="server" class="input_textBox_1" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="13%">
                                                    &nbsp;
                                                    <asp:Label ID="lblReGetWorkNo" runat="server" Text="lbl">
                                                    </asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtWorkNo" class="input_textBox_2" runat="server" Width="100%">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="pnlBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                        z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                        border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                        border-bottom: #0000ff 1px solid; position: absolute; left: 11%; float: left;
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
                                                                                                Width="100%" Style="display: none">
                                                                                            </asp:TextBox>
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
                                                            left: 0px; width: 225px; height: 100px; z-index: -1; filter='progid:dximgtransform.microsoft.alpha(style=0,opacity=0)';">
                                                                </iframe>
                                                    </div>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblKQDate" runat="server" Text="KQDate">
                                                    </asp:Label>
                                                </td>
                                                <td width="26%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtKQDateFrom" onchange="return CheckDate()" runat="server" Width="100%"
                                                                    class="input_textBox_2">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtKQDateTo" onchange="return CheckDate()" runat="server" Width="100%"
                                                                    class="input_textBox_2">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblName" runat="server" Text="lbl">
                                                    </asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" class="input_textBox_2">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblKaoqinStatus" runat="server" Text="lbl">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" class="input_textBox_1" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblExceptionType" runat="server" Text="lbl">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="ddlExceptionType" Width="350" RepeatColumns="3" class="input_textBox_1"
                                                        DropimgSrc="../../CSS/Images/expand.gif" DisplayTextWidth="350" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblShiftNo" runat="server" Text="lbl">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlShiftNo" runat="server" Width="100%">
                                                    </asp:DropDownList>
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
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="pnlShowControls" runat="server">
                                        <asp:Button ID="btnQuery" runat="server" Class="button_2" OnClientClick="return query_click()"
                                            OnClick="btnQuery_Click"></asp:Button>
                                        <asp:Button ID="btnReset" runat="server" Class="button_2" OnClick="btnReset_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnExport" runat="server" Class="button_2" OnClick="btnExport_Click">
                                        </asp:Button>
                                        <asp:Panel ID="PanelKQ" runat="server" Width="560" Style="padding-right: 3px; padding-left: 3px;
                                            z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                            border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                            border-bottom: #0000ff 1px solid; position: absolute; left: 2%; float: left;
                                            display: none;">
                                            <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <div id="divKQ">
                                                                    </div>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnClose" runat="server" Class="button_2" OnClientClick="return HiddenShowDetail()">
                                                                                </asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnCount" runat="server" Class="button_2" OnClick="btnCount_Click"
                                            OnClientClick="javascript:UpProgress();"></asp:Button>
                                        <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true">
                                        </asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidOperate" runat="server" />
        </div>
        <asp:Panel ID="PanelData" runat="server" Width="100%">
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
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ShowMoreButtons="false"
                                ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <img class="img2" id="tr_showtd" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                            <igtbl:UltraWebGrid ID="UltraWebGridReGetKaoQin" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridReGetKaoQin_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridReGetKaoQin" TableLayout="Fixed"
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_ReGetKaoQinData" Key="KQM_ReGetKaoQinData">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="False"
                                                Key="DepName" Width="120">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="False"
                                                Key="WorkNo" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="False"
                                                Key="LocalName" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" HeaderText="KQDate" IsBound="False"
                                                Key="KQDate" Width="80" Format="yyyy/MM/dd">
                                                <Header Caption="<%$Resources:ControlText,gvKQDate %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="False"
                                                Key="StatusName" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName %>">
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="False"
                                                Key="ShiftDesc" Width="170">
                                                <Header Caption="<%$Resources:ControlText,gvShiftDesc %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" HeaderText="OnDutyTime" IsBound="False"
                                                Key="OnDutyTime" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOnDutyTime %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" HeaderText="OffDutyTime" IsBound="False"
                                                Key="OffDutyTime" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OtOnDutyTime" AllowUpdate="No" HeaderText="OtOnDutyTime"
                                                IsBound="False" Key="OtOnDutyTime" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvOTOnDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OtOffDutyTime" AllowUpdate="No" HeaderText="OtOffDutyTime"
                                                IsBound="False" Key="OtOffDutyTime" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvOTOffDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" HeaderText="AbsentQty" IsBound="False"
                                                Key="AbsentQty" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentQty %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="False"
                                                Key="Status" Hidden="true">
                                                <Header Caption="Status">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionName" HeaderText="ExceptionName"
                                                IsBound="False" Key="ExceptionName" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvExceptionName%>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonName" HeaderText="ReasonName" IsBound="False"
                                                Key="ReasonName" Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonName%>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" HeaderText="ReasonRemark" IsBound="False"
                                                Key="ReasonRemark" Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvReasonRemark %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionType" HeaderText="ExceptionType"
                                                IsBound="False" Key="ExceptionType" Hidden="true">
                                                <Header Caption="ExceptionName">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" HeaderText="ShiftNo" IsBound="False"
                                                Key="ShiftNo" Hidden="true">
                                                <Header Caption="ShiftNo">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
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

    <script type="text/javascript"><!--
        function Load()
        {
          $(".input_textBox").css("border-style", "none");
          return true;
        }
        document.getElementById('txtDepName').readOnly=true;
        function ShowDetail(workNo,KQdate,shiftNo)
		{
		    document.all("PanelKQ").style.display="";
            document.all("PanelKQ").style.top=document.all("btnExport").style.top;            
            docallback(workNo,KQdate,shiftNo);
		}		
        function docallback(workNo,KQdate,shiftNo)
        {
            
            if (workNo!="")
                {
                    $.ajax(
                           {
                             type:"post",url:"KQMReGetKaoQin.aspx",dataType:"json",data:{WorkNo:workNo,KQDate:KQdate,ShiftNo:shiftNo},  
                             success:function(msg) 
                                {
                                    document.all("PanelKQ").style.display="block";
                                    document.getElementById('divKQ').innerText=""; 
                                    if (msg)
                                    {
                                        var tblHtml= new   Array();
　　                                    tblHtml[tblHtml.length] =   "<table cellspacing=0 cellpadding=0 width=100%>";
　　                                    tblHtml[tblHtml.length] = "<tr>";
　　                                    tblHtml[tblHtml.length]="<td class=td_label align=center height=25 width=60>"+Message.No+"</td>"
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=60>"+Message.WorkNo+"</td>";
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=70>"+Message.LocalName+"</td>";
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=120>"+Message.CardTime+"</td>";
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=90>"+Message.BellNo+"</td>";
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=120>"+Message.ReadTime+"</td>";
　　                                    tblHtml[tblHtml.length]="<td class=td_lbl height=25 width=80>"+Message.CardNo+"</td>";
　　                                    tblHtml[tblHtml.length]= '</tr>';
　　                                    $(msg).each(function(i){
　　                                     var WorkNo=msg[i].WorkNo==null?"&nbsp;":msg[i].WorkNo;
　　                                        var LocalName=msg[i].LocalName==null?"&nbsp":msg[i].LocalName;
　　                                        var CardTime=msg[i].CardTime==null?"&nbsp":$. jsonDTToString(msg[i].CardTime);
　　                                        var BellNo=msg[i].BellNo==null?"&nbsp":msg[i].BellNo;
　　                                        var ReadTime=msg[i].ReadTime==null?"&nbsp":$. jsonDTToString(msg[i].ReadTime);
　　                                        var CardNo=msg[i].CardNo==null?"&nbsp":msg[i].CardNo;
　　                                        tblHtml[tblHtml.length]= "<tr>";
　　                                        tblHtml[tblHtml.length]="<td class=td_label align=center height=25>"+(i+1)+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+WorkNo+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+LocalName+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+CardTime+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+BellNo+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+ReadTime+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+CardNo+"</td>";
　　                                        tblHtml[tblHtml.length]="</tr>";
　　                                    });
　　                                    tblHtml[tblHtml.length] ="</table>";
　　                                    document.getElementById("divKQ").innerHTML = tblHtml.join("");
                                    }
                                    else
                                    {
                                       document.all("PanelKQ").style.display="none";
                                    }
                                }
                           });
                }
                return false;
        }
		function HiddenShowDetail()
		{
		    document.all("PanelKQ").style.display="none";
		    return false;
		}        
	--></script>

</body>
</html>
