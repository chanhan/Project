<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMExceptionQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.OTM.OTMExceptionQryForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OTMExceptionApplyForm</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">
     $(function(){
    $("#tr_edit").toggle(
        function(){
            $("#table_condition").hide();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#table_condition").show();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
       
        }
    )
    }
    )
    $(function(){
    $("#div_img2,#td_show_1,#td_show_2").toggle(
        function(){
            $("#table_display").hide();
            $(".img2").attr("src","../../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#table_display").show();
            $(".img2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
        }
     )
     })
     
     
      function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="dept")
           {
           var url="../../BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r="+Math.random();
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;           
           
       }    
           function AfterSelectChange(tableName, itemName) 
	{
		var row = igtbl_getRowById(itemName);
		return 0;
	}
	function UltraWebGridModule_InitializeLayoutHandler(gridName)
	{
		var row = igtbl_getActiveRow(gridName);
	}
	
	   function ButtonReset()
    {
    $("#<%=txtDepCode.ClientID %>").attr("value","");
    $("#<%=txtDepName.ClientID %>").attr("value","");
    $("#<%=txtWorkNo.ClientID %>").attr("value","");
    $("#<%=txtLocalName.ClientID %>").attr("value","");
    $("#<%=txtStartDate.ClientID %>").attr("value","");
    $("#<%=txtEndDate.ClientID %>").attr("value","");
    $("#<%=txtHours.ClientID %>").attr("value","");
    $("#ddlHoursCondition").get(0).selectedIndex=0;
    $("#ddlOTType").get(0).selectedIndex=0;
    $("#ddlStatus").get(0).selectedIndex=0;
    $("#ddlDiffReason").get(0).selectedIndex=0;
    $("#ddlPersonType").get(0).selectedIndex=0;
    var myDate = new Date();
    var year=myDate.getFullYear();
    var month=myDate.getMonth()+1>9?(myDate.getMonth()+1):"0" + (myDate.getMonth()+1);
    var date=myDate.getDate()>9?myDate.getDate():"0"+myDate.getDate();
    $("#<%=txtStartDate.ClientID %>").val(year+"/"+month+"/01");
    $("#<%=txtEndDate.ClientID %>").val(year+"/"+month+"/"+date);
     return false;
    }   
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%"  align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                    <td style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                                        <div id="Div1">
                                            <img id="Img1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                            </table>
                            <table id="table_condition" class="table_data_area">
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%" style="table-layout:fixed">
                                                <tr class="tr_data_1">
                                                    <td style="width:10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDepcode" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width:20%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr class="tr_data_1">
                                                                <td>
                                                                    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox_1"
                                                                        Style="display: none"></asp:TextBox>
                                                                </td>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td style="cursor: hand">
                                                                    <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width:10%">
                                                        <asp:Label ID="lblHours" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width:20%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="30%">
                                                                    <asp:DropDownList ID="ddlHoursCondition" runat="server" Width="100%">
                                                                        <asp:ListItem Value="=" Selected="True">=</asp:ListItem>
                                                                        <asp:ListItem Value=">">></asp:ListItem>
                                                                        <asp:ListItem Value="<"><</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td width="70%">
                                                                    <asp:TextBox ID="txtHours" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width:10%">
                                                        &nbsp;
                                                        <asp:Label ID="lblPersonType" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width:30%">
                                                        <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                   <%-- <td style="width:100px">&nbsp;</td>--%>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWorkNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblLocalName" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td >&nbsp;</td>--%>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblOTType" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlOTType" runat="server" Width="100%" CssClass="input_textBox_1">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="lblDiffReason" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlDiffReason" runat="server" Width="100%" CssClass="input_textBox_1">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="lblState" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="input_textBox_1">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--<td >&nbsp;</td>--%>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                <asp:Panel ID="pnlShowPanel" runat="server">
                                                                    <asp:Button ID="btnQuery" class="button_2" runat="server" ToolTip="Authority Code:Query"
                                                                        CommandName="Query" OnClick="btnQuery_Click"></asp:Button>
                                                                    <asp:Button ID="btnReset" class="button_2" runat="server" ToolTip="Authority Code:Reset"
                                                                        CommandName="Reset" OnClientClick="return ButtonReset()"></asp:Button>
                                                                    <asp:Button ID="btnExport" class="button_2" runat="server" CommandName="Export" ToolTip="Authority Code:Export"
                                                                        OnClick="btnExport_Click"></asp:Button>
                                                                        </asp:Panel>
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
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td class="tr_table_title">
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;">
                                                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
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
                                                            DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                            ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../../CSS/Images_new/search01.gif"
                                                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                        </ess:AspNetPager>
                                                    </div>
                                                </td>
                                                <td style="width: 22px;" id="td_show_2">
                                                    <img id="div_img2" class="img2" width="22px" height="24px" src="../../../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="table_display">
                                <tr>
                                    <td colspan="3">
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

                                                    <igtbl:UltraWebGrid ID="UltraWebGridRealApply" runat="server" Width="100%" Height="100%"
                                                        OnDataBound="UltraWebGridRealApply_DataBound">
                                                        <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                            AutoGenerateColumns="false" AllowSortingDefault="Yes" RowHeightDefault="20px"
                                                            Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                            BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                            Name="UltraWebGridRealApply" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
                                                            <igtbl:UltraGridBand BaseTableName="OTM_RealApply" Key="OTM_RealApply">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionWorkNo%>" Fixed="true">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionLocalName%>" Fixed="true">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BuName" Key="BuName" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionBuName%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DName" Key="DName" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionDName%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                        Width="50">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionOverTimeType%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionOTDate%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="50">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionWeek%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionOTType%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="KQShift" Key="KQShift" IsBound="false" Width="130">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionKQShift%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="KQTime" Key="KQTime" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionKQTime%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionBeginTime%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionEndTime%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RealHours" Key="RealHours" IsBound="false"
                                                                        Width="50">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionRealHours%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="150">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionWorkDesc%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DiffReasonName" Key="DiffReasonName" IsBound="false"
                                                                        Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionDiffReasonName%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ReMarks" Key="ReMarks" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionReMarks%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionStatusName%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ReMark" Key="ReMark" IsBound="false" Width="150">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionReMark%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionModifier%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                                        Width="120">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionModifyDate%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMExceptionBillNo%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="true" Width="0" Hidden="true">
                                                                        <Header Caption="ID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="true" Width="0"
                                                                        Hidden="true">
                                                                        <Header Caption="Status">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="dCode" Key="dCode" IsBound="true" Width="0"
                                                                        Hidden="true">
                                                                        <Header Caption="dCode">
                                                                        </Header>
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

    <script type="text/javascript"><!--
    document.getElementById("txtWorkNo").focus();
    document.getElementById("txtWorkNo").select(); 
	--></script>

</body>
</html>
