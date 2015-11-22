<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbsentReporter.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WFReporter.AbsentReporter" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
      $(function(){
        $("#tr_edit").toggle(
            function(){$("#div_1").hide(); $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_1").show();$(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
       });
    $(function(){
       $("#tr_show").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); } ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
   });
   
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="ModuleCode" runat="server" />
        <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server">
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
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area" style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr class="tr_data_1">
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>
                                                </td>
                                                <td width="24%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="90%">
                                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%" Style=""></asp:TextBox>
                                                            </td>
                                                            <td width="10%" style="cursor: hand" onclick="setSelector();">
                                                                <img id="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblFromPersoncode" runat="server" Text="Personcode"></asp:Label>
                                                </td>
                                                <td width="24%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblFromPersonName" runat="server" Text="PersonName"></asp:Label>
                                                </td>
                                                <td width="25%">
                                                    <asp:TextBox ID="txtLocalname" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDate" runat="server">Date:</asp:Label>
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
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblYearMonth" runat="server">YearMonth:</asp:Label>
                                                </td>
                                                <td width="24%">
                                                    <igtxt:WebDateTimeEdit ID="txtYearMonth"  runat="server" EditModeFormat="yyyy/MM" width="100%">
                                                    </igtxt:WebDateTimeEdit>
                                                </td>
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblAbsentType" runat="server">AbsentType:</asp:Label>
                                                </td>
                                                <td width="25%">
                                                    <asp:DropDownList ID="ddlAbsentType" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblReGetStatus" runat="server">Status:</asp:Label>
                                                </td>
                                                <td width="24%">
                                                    <asp:TextBox ID="txtStatus" runat="server" width="100%"></asp:TextBox>
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnQuery" class="button_1" runat="server" OnClientClick="return checkdata();" OnClick="btnQuery_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnExport" class="button_1" runat="server" OnClick="btnExport_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
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
                        <asp:Label ID="OTMmsg" runat="server"></asp:Label>
                    </td>
                    <td class="tr_title_center" style="width: 300px;">
                        <div>
                            <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridAbsentTotal" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridAbsentTotal_DataBound">
                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridAbsentTotal" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                    <HeaderStyleDefault Height="25px" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridAbsentTotal_InitializeLayoutHandler"
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
                                    <igtbl:UltraGridBand BaseTableName="AbsentTotal" Key="AbsentTotal">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="bgname" HeaderText="bgname" IsBound="false"
                                                Key="bgname" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvBuOTMQryName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="depcode" HeaderText="depcode" IsBound="false"
                                                Key="depcode" Width="120px" Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="depname" HeaderText="depname" IsBound="false"
                                                Key="depname" Width="120px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ab" HeaderText="ab" IsBound="false"
                                                Key="ab" Width="80px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvLateEarly %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="c" HeaderText="c" IsBound="false"
                                                Key="c" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvAbsent %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="i" HeaderText="i" IsBound="false"
                                                Key="i" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvShortAffairLeave %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="t" HeaderText="t" IsBound="false"
                                                Key="t" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSickLeave %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="j" HeaderText="j" IsBound="false"
                                                Key="j" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMarriageLeave %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="s" HeaderText="s" IsBound="false"
                                                Key="s" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMaternityLeave %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="k" HeaderText="k" IsBound="false"
                                                Key="k" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvCompassionateLeave %>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="v" HeaderText="v" IsBound="false"
                                                Key="v" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvBirthControlLeave %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="y" HeaderText="y" IsBound="false"
                                                Key="y" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvLeaveApplyYear %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="r" HeaderText="r" IsBound="false"
                                                Key="r" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSabbaticalLeave %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="x" HeaderText="x" IsBound="false"
                                                Key="x" Width="90px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvLongAffairLeave %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="z" HeaderText="z" IsBound="false"
                                                Key="z" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvTreatmentLeave %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="numcount" HeaderText="numcount" IsBound="false"
                                                Key="numcount" Width="90px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMarkUpCount %>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="remark" HeaderText="remark" IsBound="false"
                                                Key="remark" Width="180px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvAbsentRemark %>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
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
</body>
</html>
