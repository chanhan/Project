<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveApplyYearQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KQMLeaveApplyYearQryForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KQMLeaveApplyYearQryForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
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
    var joindatefrom=document.getElementById("txtJoinDateFrom").value;
    var joindateto=document.getElementById("txtJoinDateTo").value;
    var countdatefrom=document.getElementById("txtCountDateFrom").value;
    var countdateto=document.getElementById("txtCountDateTo").value;
    var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
    if(joindatefrom!=""&&joindatefrom!=null)
    {
    if(!check.test(joindatefrom))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(joindatefrom)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(joindateto!=""&&joindateto!=null)
    {
    if(!check.test(joindateto))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(joindateto)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(countdatefrom!=""&&countdatefrom!=null)
    {
    if(!check.test(countdatefrom))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(countdatefrom)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(countdateto!=""&&countdateto!=null)
    {
    if(!check.test(countdateto))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(countdateto)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
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
            document.all("PanelBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtWorkNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        
         function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
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
                                    <table class="table_data_area" style="table-layout:fixed">
                                        <tr class="tr_data_1">
                                            <td style="width:10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblDeptDept" runat="server" Text="DepName"></asp:Label>
                                            </td>
                                            <td style="width:20%">
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
                                            <td style="width:10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblLeaveYear" runat="server" Text="LeaveYear"></asp:Label>
                                            </td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="ddlLeaveyear" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:10%">
                                                &nbsp;
                                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                            </td>
                                            <td style="width:29%">
                                                <cc1:DropDownCheckList ID="ddlStatus" Width="200" RepeatColumns="1" CssClass="input_textBox"
                                                    DropImageSrc="../../../CSS/Images/expand.gif" TextWhenNoneChecked="" DisplayTextWidth="300"
                                                    ClientCodeLocation="../../../JavaScript/DropDownCheckList.js" runat="server"
                                                    CheckListCssStyle="background-image: url(../../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                </cc1:DropDownCheckList>
                                            </td>
                                            <%--<td style="width:100px">&nbsp;</td>--%>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblEmployeeNo" runat="server" Text="EmployeeNo"></asp:Label>
                                                <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                </asp:Image>
                                            </td>
                                            <td width="18%">
                                                <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
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
                                            <td width="18%">
                                                <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblJoinDate" runat="server" Text="JoinDate"></asp:Label>
                                            </td>
                                            <td width="30%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="50%">
                                                            <asp:TextBox ID="txtJoinDateFrom" runat="server" Width="98%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            ~
                                                        </td>
                                                        <td width="50%">
                                                            <asp:TextBox ID="txtJoinDateTo" runat="server" Width="98%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <%--<td>&nbsp;</td>--%>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblCountDate" runat="server" Text="CountDate"></asp:Label>
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="50%">
                                                            <asp:TextBox ID="txtCountDateFrom" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            ~
                                                        </td>
                                                        <td width="50%">
                                                            <asp:TextBox ID="txtCountDateTo" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <%--<td> &nbsp;</td><td> &nbsp;</td><td> &nbsp;</td><td> &nbsp;</td><td> &nbsp;</td>--%>
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
                            <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19" />
                        </td>
                        <td background="../../CSS/Images_new/EMP_07.gif" height="19px" />
                        </td>
                        <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                            <img height="18" src="../../CSS/Images_new/EMP_02.gif" width="19">
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td width="19" background="../../CSS/Images_new/EMP_05.gif" />
                        &nbsp; </td>
                        <td>

                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*63/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveQry" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridLeaveQry_DataBound">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_leaveyears" Key="gds_att_leaveyears">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                Key="DepName" Width="160">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SexName" HeaderText="SexName" IsBound="false"
                                                Key="SexName" Width="40">
                                                <Header Caption="<%$Resources:ControlText,gvSex %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="JoinDate" Format="yyyy/MM/dd" HeaderText="JoinDate" IsBound="false"
                                                Key="JoinDate" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvKQMJoinDate %>" Fixed="true">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EnableStartDate" Format="yyyy/MM/dd" HeaderText="EnableStartDate"
                                                IsBound="false" Key="EnableStartDate" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvEnableStartDate %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LeaveYear" HeaderText="LeaveYear" IsBound="false"
                                                Key="LeaveYear" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveYear %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartYears" HeaderText="StartYears" IsBound="false"
                                                Key="StartYears" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvStartYears %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndYears" HeaderText="EndYears" IsBound="false"
                                                Key="EndYears" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvEndYears %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OutWorkYears" Key="OutWorkYears" IsBound="false"
                                                Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvOutWorkYears %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OutFoxconnYears" Key="OutFoxconnYears" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvOutFoxconnYears %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StandardDays" HeaderText="StandardDays" IsBound="false"
                                                Key="StandardDays" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvStandardDays %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AlreadDays" HeaderText="AlreadDays" IsBound="false"
                                                Key="AlreadDays" Width="40">
                                                <Header Caption="<%$Resources:ControlText,gvAlreadDays %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LeaveDays" HeaderText="LeaveDays" IsBound="false"
                                                Key="LeaveDays" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvKQMLeaveDays %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="NextYearDays" HeaderText="NextYearDays" IsBound="false"
                                                Key="NextYearDays" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvNextYearDays %>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LeaveRecDays" HeaderText="LeaveRecDays" IsBound="false"
                                                Key="LeaveRecDays" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveRecDays %>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="CountDays" Key="CountDays" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvCountDays %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvStatus %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="UpdateDate" Format="yyyy/MM/dd" Key="UpdateDate" IsBound="false"
                                                Width="120">
                                                <Header Caption="<%$Resources:ControlText,gvUpdateDate %>">
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
        </div>
    </asp:Panel>
    </form>
</body>
</html>
