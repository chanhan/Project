<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KaoQinQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KaoQinQryForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考勤結果查詢</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a
        {
            text-decoration: none;
        }
    </style>

    <script type="text/javascript"><!--
    $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_select").hide();
                $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_select").show();
                $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
          $("#div_img_2,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_showdata").hide();
                $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_showdata").show();
                $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
       
       
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
    var datefrom=document.getElementById("txtKQDateFrom").value;
    var dateto=document.getElementById("txtKQDateTo").value;
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
      alert(Message.KQDateNotNull);
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
    var a=document.getElementById("chkFlag");
    var b=document.getElementById("chkFlagB");
    if(a.checked||b.checked)
    {
    return true;
    }
    else
    {
     alert(Message.KQDateNotNull);
      return false;
      }
    }
    var result=0;
          $.ajax({type: "post", url: "KaoQinQryForm.aspx", dataType: "text", data: {FromDate: datefrom, ToDate: dateto},async:false,
                success: function(msg) {
                    if (msg==1) {alert(Message.QueryWithinTwoMonths);result=0; } 
                    else{result=1;}}
                     }); 
                   if(result==0){return false;} 
                    else{return true;}  
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
    
    function setSelector()
        {
        var modulecode=$("#<%=ModuleCode.ClientID %>").val();
        var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+modulecode;
        var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
        var info=window.showModalDialog(url,null,fe);
        if(info)
        {
        $("#txtDepCode").val(info.codeList);
        $("#txtDepName").val(info.nameList);
        }
        return false;
        }
    
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="";
            document.all("PanelBatchWorkNo").style.top=document.all("txtEmployeeNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        
        function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";
        }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="DefaultEmployeeNo" runat="server" name="DefaultEmployeeNo" type="hidden" />
    <input id="HiddenHisFlag" runat="server" name="HiddenHisFlag" type="hidden" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%; cursor: hand;" id="tr_edit">
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblCondition" runat="server"></asp:Label>
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
                                    <table class="table_data_area" style="table-layout: fixed">
                                        <tr class="tr_data_1">
                                            <td width="10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblDeptDept" runat="server" Text="Department"></asp:Label>
                                            </td>
                                            <td width="15%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:TextBox ID="txtDepName" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand">
                                                            <asp:ImageButton ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif"
                                                                OnClientClick="return setSelector();"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblIsMakeup" runat="server" Text="IsMakeup"></asp:Label>
                                            </td>
                                            <td width="15%">
                                                <asp:DropDownList ID="ddlIsMakeup" runat="server" Width="100%">
                                                    <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="N">N</asp:ListItem>
                                                    <asp:ListItem Value="Y">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblIsSupporter" runat="server" Text="Supporter"></asp:Label>
                                            </td>
                                            <td width="24%">
                                                <asp:DropDownList ID="ddlIsSupporter" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <%--<td style="width: 100px">
                                                &nbsp;
                                            </td>--%>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lbl_empno" runat="server" Text="WorkNo"></asp:Label>
                                                <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                </asp:Image>
                                            </td>
                                            <td width="15%">
                                                <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%"></asp:TextBox>
                                                <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
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
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblLocalName" runat="server" Text="LocalName"></asp:Label>
                                            </td>
                                            <td width="15%">
                                                <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td width="8%">
                                                &nbsp;
                                                <asp:Label ID="lblKQDate" runat="server" Text="KQDate"></asp:Label>
                                            </td>
                                            <td width="24%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td id="KQDateFlag" width="50%">
                                                            <asp:TextBox ID="txtKQDateFrom" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td id="hiddenflag">
                                                            ~
                                                        </td>
                                                        <td width="50%">
                                                            <asp:TextBox ID="txtKQDateTo" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <%--<td >
                                                &nbsp;
                                            </td>--%>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblAttendHandleStatus" runat="server" Text="StatusName"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatusName" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblExceptionType" runat="server" Text="ExceptionType"></asp:Label>
                                            </td>
                                            <td>
                                                <cc1:DropDownCheckList ID="ddlExceptionType" Width="200" RepeatColumns="3" DropImageSrc="../../../CSS/Images/expand.gif"
                                                    TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../../../JavaScript/DropDownCheckList.js"
                                                    runat="server" CheckListCssStyle="background-image: url(../../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                </cc1:DropDownCheckList>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlShiftNo" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblStatus" runat="server" Text="StatusName"></asp:Label>
                                            </td>
                                            <td>
                                                <cc1:DropDownCheckList ID="ddlStatus" Width="200" RepeatColumns="1" DropImageSrc="../../../CSS/Images/expand.gif"
                                                    TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../../../JavaScript/DropDownCheckList.js"
                                                    runat="server" CheckListCssStyle="background-image: url(../../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                </cc1:DropDownCheckList>
                                            </td>
                                            <td colspan="3">
                                                &nbsp;
                                                <asp:CheckBox ID="chkFlag" runat="server" />
                                                <asp:Label ID="lblFlagA" runat="server" ForeColor="red"></asp:Label>
                                                <asp:CheckBox ID="chkFlagB" runat="server" />
                                                <asp:Label ID="lblFlagB" runat="server" ForeColor="red"></asp:Label>
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
                            <td>
                                <asp:Panel ID="PanelKQ" runat="server" Visible="true" Width="560" Style="padding-right: 3px;
                                    padding-left: 3px; z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px;
                                    background-color: #ffffee; border-right: #0000ff 1px solid; border-top: #0000ff 1px solid;
                                    border-left: #0000ff 1px solid; border-bottom: #0000ff 1px solid; position: absolute;
                                    left: 2%; float: left; display: none">
                                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                        <tr>
                                            <td>
                                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <div id="divKQ">
                                                            </div>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td style="width: 45px" align="center">
                                                                        <asp:Button ID="btnClose" runat="server" CssClass="button_1" OnClientClick="return HiddenShowDetail()">
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
        <div style="width: 100%" id="innerTable_1">
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
                                ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
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

                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*57/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridKaoQinQuery" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridKaoQinQuery_DataBound">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_kaoqindataquery_v" Key="gds_att_kaoqindataquery_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                Key="DepName" Width="120">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" HeaderText="KQDate" IsBound="false"
                                                Key="KQDate" Width="80" Format="yyyy/MM/dd" ValueList-Style-CssClass="a">
                                                <Header Caption="<%$Resources:ControlText,gvKQDate %>">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="false"
                                                Key="StatusName" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName %>">
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="10" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="false"
                                                Key="ShiftDesc" Width="170">
                                                <Header Caption="<%$Resources:ControlText,gvKQMShiftDesc %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsMakeUp" AllowUpdate="No" HeaderText="IsMakeUp"
                                                IsBound="false" Key="IsMakeUp" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvIsMakeUp%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" HeaderText="OnDutyTime" IsBound="True"
                                                Key="OnDutyTime" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOnDutyTime %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" HeaderText="OffDutyTime" IsBound="false"
                                                Key="OffDutyTime" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTOnDutyTime" AllowUpdate="No" HeaderText="OTOnDutyTime"
                                                IsBound="false" Key="OTOnDutyTime" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvOTOnDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTOffDutyTime" AllowUpdate="No" HeaderText="OTOffDutyTime"
                                                IsBound="false" Key="OTOffDutyTime" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvOTOffDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" HeaderText="AbsentQty" IsBound="false"
                                                Key="AbsentQty" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentQty %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionTypeName" HeaderText="ExceptionTypeName"
                                                IsBound="false" Key="ExceptionTypeName" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvExceptionTypeName %>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonName" HeaderText="ReasonName" IsBound="false"
                                                Key="ReasonName" Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvKQMReasonName %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" HeaderText="ReasonRemark" IsBound="false"
                                                Key="ReasonRemark" Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvReasonRemark %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentTotal" HeaderText="AbsentTotal" IsBound="false"
                                                Key="AbsentTotal" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentTotal %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="false"
                                                Key="Status" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvStatus%>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" HeaderText="ShiftNo" IsBound="false"
                                                Key="ShiftNo" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvShiftNo %>">
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
        </div>
    </asp:Panel>
    </form>

    <script type="text/javascript"><!--  
        onChangeFlag(document.all("chkFlag"));
        function ShowDetail(WorkNo,KQDate,ShiftNo)
		{
		    document.all("PanelKQ").style.display="";
            document.all("PanelKQ").style.top=document.all("btnExport").style.top;
	        var HisFlag=document.getElementById("HiddenHisFlag").value;           
//            HRM_Query_KqmKaoQinQryForm.ShowDetail(WorkNo,KQDate,ShiftNo,HisFlag,docallback)
              docallback(WorkNo,KQDate,ShiftNo,HisFlag);
		}		
        function docallback(WorkNo,KQDate,ShiftNo,HisFlag)
        {
            document.getElementById('divKQ').innerText=""; 
            if (WorkNo!="")
                {
                    $.ajax(
                           {
                             type:"post",url:"KaoQinQryForm.aspx",dataType:"json",data:{WorkNo:WorkNo,KQDate:KQDate,ShiftNo:ShiftNo},  
                             success:function(msg) 
                                {
                                    document.all("PanelKQ").style.display="block";
                                    document.getElementById('divKQ').innerText=""; 
                                    if (msg)
                                    {
                                        var tblHtml= new   Array();
　　                                    tblHtml[tblHtml.length] =   "<table cellspacing=0 cellpadding=0 width=100%>";
　　                                    tblHtml[tblHtml.length] = "<tr>";
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
　　                                        var CardTime=msg[i].CardTime==null?"&nbsp":$.jsonDateToString(msg[i].CardTime);
　　                                        var BellNo=msg[i].BellNo==null?"&nbsp":msg[i].BellNo;
　　                                        var ReadTime=msg[i].ReadTime==null?"&nbsp":$.jsonDateToString(msg[i].ReadTime);
　　                                        var CardNo=msg[i].CardNo==null?"&nbsp":msg[i].CardNo;
　　                                        tblHtml[tblHtml.length]= "<tr>";
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
		function onChangeFlag(obj)
        {   
        if(obj.id=='chkFlag')
        {
        document.getElementById("chkFlagB").checked=false;
        }
        else
        {
        document.getElementById("chkFlag").checked=false;
        }
          if(obj.checked)
          {
	        document.getElementById("KQDateFlag").style.width="100%";
	        document.getElementById("txtKQDateTo").value="";
	        document.getElementById("ddlShiftNo").value="";
	        document.getElementById("txtKQDateTo").style.display="none";
	        document.getElementById("hiddenflag").style.display="none";
	        document.getElementById("ddlShiftNo").disabled="disabled"; 
          }
          else
          {
	        document.getElementById("KQDateFlag").style.width="50%";
	        document.getElementById("txtKQDateTo").style.display="";
	        document.getElementById("hiddenflag").style.display="";  
	        document.getElementById("ddlShiftNo").disabled="";        
          } 
        }      
	--></script>

</body>
</html>
