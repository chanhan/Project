<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMTotalQry.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.OTM.OTMTotalQry" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMMonthTotal</title>
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script src="../../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

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
     //2011-8-17 工號可以多選查詢
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
                $(".img1").attr("src","../../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
      $(function(){
        $("#tr_show").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
      });
     $(function(){
       $("#tr_showtd").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
     });
     function Query_Click()
     {
        var YearMonth=$("#<%=txtYearMonth.ClientID %>").val();
        var lblYearMonth=$("#<%=lblYearMonth.ClientID %>").text();
		if(YearMonth.length=0||YearMonth=="")
		{
		   var alertText=lblYearMonth+Message.TextBoxNotNull;
		   alert(alertText);
		   return false;
		}
		else{return true;}
     }
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="hidYearMonth" type="hidden" name="hidYearMonth" runat="server">
    <div>
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                            <img class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_1">
            <table cellspacing="0" cellpadding="0" width="100%">
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
                                                        <asp:Label ID="lblReGetDepcode" runat="server">Department:</asp:Label>
                                                    </td>
                                                    <td width="15%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                </td>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="txtDepName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td style="cursor: hand">
                                                                    <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td width="10%">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server" Text="Label"></asp:Label>
                                                        <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif">
                                                        </asp:Image>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:TextBox ID="txtWorkNo" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
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
                                                    <td width="15%">
                                                        &nbsp;
                                                        <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtLocalName" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTTypeQryCode" runat="server">OTTypeCode:</asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <cc1:DropDownCheckList ID="ddlOTTypeCode" class="input_textBox_2" Width="150" RepeatColumns="1"
                                                            DropImageSrc="../../../CSS/Images/expand.gif" TextWhenNoneChecked="" DisplayTextWidth="300"
                                                            ClientCodeLocation="../../../JavaScript/DropDownCheckList.js" runat="server">
                                                        </cc1:DropDownCheckList>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblYearMonth" runat="server">YearMonth:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebDateTimeEdit ID="txtYearMonth" class="input_textBox_1" runat="server" EditModeFormat="yyyy/MM">
                                                        </igtxt:WebDateTimeEdit>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblApproveFlag" runat="server">ApproveFlag:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <cc1:DropDownCheckList ID="ddlApproveFlag" class="input_textBox_1" Width="150" RepeatColumns="1"
                                                            DropImageSrc="../../../CSS/Images/expand.gif" TextWhenNoneChecked="" DisplayTextWidth="300"
                                                            ClientCodeLocation="../../../JavaScript/DropDownCheckList.js" runat="server">
                                                        </cc1:DropDownCheckList>
                                                    </td>
                                                    <td colspan="2">
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
                                            <asp:Button ID="btnQuery" runat="server" Class="button_2" OnClick="btnQuery_Click" OnClientClick="return Query_Click()">
                                            </asp:Button>
                                            <asp:Button ID="btnReset" runat="server" Class="button_2" OnClick="btnReset_Click">
                                            </asp:Button>
                                            <asp:Button ID="btnExport" runat="server" Class="button_2" OnClick="btnExport_Click">
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
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" id="tr_show" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                                ImagePath="../../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                ShowMoreButtons="false" DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px"
                                ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <img class="img3" id="tr_showtd" width="22px" height="24px" src="../../../CSS/Images_new/left_back_03_a.gif" />
                    </td>
                </tr>
            </table>
            <div id="div_showdata">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr style="width: 100%">
                        <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_05.gif" height="18">
                            <img height="18" src="../../../CSS/Images_new/EMP_01.gif" width="19">
                        </td>
                        <td background="../../../CSS/Images_new/EMP_07.gif" height="19px">
                        </td>
                        <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_06.gif" height="18">
                            <img height="18" src="../../../CSS/Images_new/EMP_02.gif" width="19">
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td width="19" background="../../../CSS/Images_new/EMP_05.gif">
                            &nbsp;
                        </td>
                        <td>

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridOTMMonthTotal" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridOTMMonthTotal_DataBound" OnInitializeLayout="UltraWebGridOTMMonthTotal_InitializeLayout">
                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMMonthTotal" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                    <HeaderStyleDefault Height="25px" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridOTMMonthTotal_InitializeLayoutHandler"
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
                                    <igtbl:UltraGridBand BaseTableName="OTM_MonthTotal" Key="OTM_MonthTotal">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="BuName" HeaderText="BuName" IsBound="false"
                                                Key="BuName" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvBuOTMQryName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DName" HeaderText="DName" IsBound="false"
                                                Key="DName" Width="120px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvDName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                Key="OverTimeType" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvOverTimeType%>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1Apply" HeaderText="G1Apply" IsBound="false"
                                                Key="G1Apply" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG1Apply%>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Apply" HeaderText="G2Apply" IsBound="false"
                                                Key="G2Apply" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG2Apply%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3Apply" HeaderText="G3Apply" IsBound="false"
                                                Key="G3Apply" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG3Apply%>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1RelSalary" HeaderText="G1RelSalary" IsBound="false"
                                                Key="G1RelSalary" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG1RelSalary%>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                Key="G2RelSalary" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG2RelSalary%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3RelSalary" HeaderText="G3RelSalary" IsBound="false"
                                                Key="G3RelSalary" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG3RelSalary%>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG1Apply" HeaderText="SpecG1Apply" IsBound="false"
                                                Key="SpecG1Apply" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG1Apply%>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG2Apply" HeaderText="SpecG2Apply" IsBound="false"
                                                Key="SpecG2Apply" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG2Apply%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG3Apply" HeaderText="SpecG3Apply" IsBound="false"
                                                Key="SpecG3Apply" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG3Apply%>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG1Salary" HeaderText="SpecG1Salary" IsBound="false"
                                                Key="SpecG1Salary" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG1Salary%>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG2Salary" HeaderText="SpecG2Salary" IsBound="false"
                                                Key="SpecG2Salary" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG2Salary%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG3Salary" HeaderText="SpecG3Salary" IsBound="false"
                                                Key="SpecG3Salary" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG3Salary%>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Remain" HeaderText="G2Remain" IsBound="false"
                                                Key="G2Remain" Width="80">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvG2Remain%>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="false"
                                                Key="MAdjust1" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMAdjust1%>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MRelAdjust" HeaderText="MRelAdjust" IsBound="false"
                                                Key="MRelAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMRelAdjust%>">
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlagName" HeaderText="ApproveFlagName"
                                                IsBound="false" Key="ApproveFlagName" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvApproveFlagName%>">
                                                    <RowLayoutColumnInfo OriginX="16" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="16" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" IsBound="false" Key="ApproveFlag"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="ApproveFlag">
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="YearMonth" IsBound="false" Key="YearMonth"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="YearMonth">
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day1" HeaderText="1" IsBound="false" Key="Day1"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="1">
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day2" HeaderText="2" IsBound="false" Key="Day2"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="2">
                                                    <RowLayoutColumnInfo OriginX="18" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="18" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day3" HeaderText="3" IsBound="false" Key="Day3"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="3">
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day4" HeaderText="4" IsBound="false" Key="Day4"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="4">
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day5" HeaderText="5" IsBound="false" Key="Day5"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="5">
                                                    <RowLayoutColumnInfo OriginX="21" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="21" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day6" HeaderText="6" IsBound="false" Key="Day6"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="6">
                                                    <RowLayoutColumnInfo OriginX="22" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="22" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day7" HeaderText="7" IsBound="false" Key="Day7"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="7">
                                                    <RowLayoutColumnInfo OriginX="23" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="23" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day8" HeaderText="8" IsBound="false" Key="Day8"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="8">
                                                    <RowLayoutColumnInfo OriginX="24" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="24" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day9" HeaderText="9" IsBound="false" Key="Day9"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="9">
                                                    <RowLayoutColumnInfo OriginX="25" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="25" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day10" HeaderText="10" IsBound="false" Key="Day10"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="10">
                                                    <RowLayoutColumnInfo OriginX="26" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="26" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day11" HeaderText="11" IsBound="false" Key="Day11"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="11">
                                                    <RowLayoutColumnInfo OriginX="27" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="27" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day12" HeaderText="12" IsBound="false" Key="Day12"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="12">
                                                    <RowLayoutColumnInfo OriginX="28" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="28" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day13" HeaderText="13" IsBound="false" Key="Day13"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="13">
                                                    <RowLayoutColumnInfo OriginX="29" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="29" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day14" HeaderText="14" IsBound="false" Key="Day14"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="14">
                                                    <RowLayoutColumnInfo OriginX="30" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="30" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day15" HeaderText="15" IsBound="false" Key="Day15"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="15">
                                                    <RowLayoutColumnInfo OriginX="31" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="31" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day16" HeaderText="16" IsBound="false" Key="Day16"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="16">
                                                    <RowLayoutColumnInfo OriginX="32" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="32" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day17" HeaderText="17" IsBound="false" Key="Day17"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="17">
                                                    <RowLayoutColumnInfo OriginX="33" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="33" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day18" HeaderText="18" IsBound="false" Key="Day18"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="18">
                                                    <RowLayoutColumnInfo OriginX="34" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="34" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day19" HeaderText="19" IsBound="false" Key="Day19"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="19">
                                                    <RowLayoutColumnInfo OriginX="35" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="35" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day20" HeaderText="20" IsBound="false" Key="Day20"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="20">
                                                    <RowLayoutColumnInfo OriginX="36" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="36" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day21" HeaderText="21" IsBound="false" Key="Day21"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="21">
                                                    <RowLayoutColumnInfo OriginX="37" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="37" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day22" HeaderText="22" IsBound="false" Key="Day22"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="22">
                                                    <RowLayoutColumnInfo OriginX="38" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="38" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day23" HeaderText="23" IsBound="false" Key="Day23"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="23">
                                                    <RowLayoutColumnInfo OriginX="39" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="39" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day24" HeaderText="24" IsBound="false" Key="Day24"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="24">
                                                    <RowLayoutColumnInfo OriginX="40" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="40" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day25" HeaderText="25" IsBound="false" Key="Day25"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="25">
                                                    <RowLayoutColumnInfo OriginX="41" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="41" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day26" HeaderText="26" IsBound="false" Key="Day26"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="26">
                                                    <RowLayoutColumnInfo OriginX="42" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="42" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day27" HeaderText="27" IsBound="false" Key="Day27"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="27">
                                                    <RowLayoutColumnInfo OriginX="43" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="43" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day28" HeaderText="28" IsBound="false" Key="Day28"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="28">
                                                    <RowLayoutColumnInfo OriginX="44" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="44" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day29" HeaderText="29" IsBound="false" Key="Day29"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="29">
                                                    <RowLayoutColumnInfo OriginX="45" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="45" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day30" HeaderText="30" IsBound="false" Key="Day30"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="30">
                                                    <RowLayoutColumnInfo OriginX="46" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="46" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day31" HeaderText="31" IsBound="false" Key="Day31"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="31">
                                                    <RowLayoutColumnInfo OriginX="47" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="47" />
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
                        <td width="19" background="../../../CSS/Images_new/EMP_06.gif">
                            &nbsp;
                        </td>
                        <tr>
                        </tr>
                    </tr>
                    <tr>
                        <td width="19" background="../../../CSS/Images_new/EMP_03.gif" height="18">
                            &nbsp;
                        </td>
                        <td background="../../../CSS/Images_new/EMP_08.gif" height="18">
                            &nbsp;
                        </td>
                        <td width="19" background="../../../CSS/Images_new/EMP_04.gif" height="18">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>

    <script type="text/javascript"><!--
        HiddenColumns();           
        function getDaysInMonth(year,month){
              month = parseInt(month,10)+1;
              var temp = new Date(year+"/"+month+"/0");
              return temp.getDate();
        }
		function HiddenColumns() {
		var YearMonth=document.getElementById("txtYearMonth").value;
		if(YearMonth.length!=0)
		{
		    var temVal = new Array();   
            temVal =YearMonth.split("/");
            var days=getDaysInMonth(temVal[0],temVal[1],1)  
            var oGrid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
            var oBands = oGrid.Bands;
            var oBand = oBands[0];
            var oColumns = oBand.Columns;
                if(days==28)
                {
                    oColumns[51].setHidden(true);
                    oColumns[52].setHidden(true);
                    oColumns[53].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_51").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_52").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_53").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_87").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_88").style.display="none";
                    }
                }
                if(days==29)
                {
                    oColumns[51].setHidden(false);
                    oColumns[52].setHidden(true);
                    oColumns[53].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_51").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_52").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_53").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_87").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_88").style.display="none";
                    }
                }
                if(days==30)
                {
                    oColumns[51].setHidden(false);
                    oColumns[52].setHidden(false);
                    oColumns[53].setHidden(true);      
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_51").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_52").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_53").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_87").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_88").style.display="none";
                    }
                }
                if(days==31)
                {
                    oColumns[51].setHidden(false);
                    oColumns[52].setHidden(false);
                    oColumns[53].setHidden(false);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_51").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_52").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_53").style.display="";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_86").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_87").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_88").style.display="";
                    }
                }
            }
        }     
	--></script>

</body>
</html>
