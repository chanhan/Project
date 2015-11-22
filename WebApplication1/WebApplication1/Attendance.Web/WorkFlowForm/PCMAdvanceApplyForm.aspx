<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMAdvanceApplyForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlowForm.PCMAdvanceApplyForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PCMAdvanceApplyForm</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    
     <script type="text/javascript"><!--
        
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGrid_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGrid');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGrid_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{		   
			if(row != null)
			{	
				igtbl_getElementById("HiddenWorkNo").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				igtbl_getElementById("HidID").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();	
				igtbl_getElementById("HiddenBillNo").value=row.getCellFromKey("BillNo").getValue()==null?"":row.getCellFromKey("BillNo").getValue();					
			}
		}
		function DblClick(gridName, id)
		{
		    var ProcessFlag="Modify";
		    OpenEdit(ProcessFlag)
		    return 0;
		}
		function OpenEdit(ProcessFlag)
		{		
		    var EmployeeNo = igtbl_getElementById("HiddenWorkNo").value;
		    var ID= igtbl_getElementById("HidID").value;
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    var OTType="";
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if (ProcessFlag == "Modify") {
		        var grid = igtbl_getGridById('UltraWebGrid');
		        var gRows = grid.Rows;
		        var Count = 0;
		        for (i = 0; i < gRows.length; i++) {

		            if (gRows.getRow(i).getSelected()) {
		                if (gRows.getRow(i).getCellFromKey("Status").getValue() == "0") {
		                    OTType = gRows.getRow(i).getCellFromKey("OTType").getValue();
		                    Count += 1;
		                }
		                else {
		                    alert(Message.checkapproveflag);
		                    return false;
		                }
		            }
		        }
		        if (Count == 0) {
		            alert(Message.choess_no);
		            return false;
		        }
		    }
            document.getElementById("iframeEdit").src="PCMAdvanceApplyEditForm.aspx?EmployeeNo="+EmployeeNo+"&ID="+ID+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
            document.getElementById("topTable").style.display = "none";
            document.getElementById("div_2").style.display = "none";
            document.getElementById("divEdit").style.display="";
            return false;
		}
		function CheckDelete()
        {
	        var grid = igtbl_getGridById('UltraWebGrid');
	        var gRows = grid.Rows;
	        var Count=0;
	        var State="";
	        for(i=0;i<gRows.length;i++)
	        {
		        if(igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
		        {
		             Count+=1;			        
		             State=gRows.getRow(i).getCellFromKey("Status").getValue();
		             switch (State)
                        {
                            case "0":
                                break;
                             default:
                                 alert(Message.DeleteApplyovertimeEnd);
                                return false;
                                break;
	                    }
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.common_message_data_select);
	            return false;
	        }
	        if (confirm(Message.ConfirmReturn))
	        {
		        //FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }


          function GetSignMap() {
              var grid = igtbl_getGridById('UltraWebGrid');
              var gRows = grid.Rows;
              var Count = 0;
              for (i = 0; i < gRows.length; i++) {
                  if (gRows.getRow(i).getSelected()) {
                      Count += 1;
                  }
              }
              if (Count == 0) {
                  alert(Message.choess_no);
                  return false;
              }
              var Doc = igtbl_getElementById("HiddenBillNo").value;
              var windowWidth = 600, windowHeight = 600;
              var X = (screen.availWidth - windowWidth) / 2;
              var Y = (screen.availHeight - windowHeight) / 2;
              var Revalue = window.showModalDialog("../WorkFlow/SignLogAndMap.aspx?Doc=" +
	          Doc, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
              return false;
          }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
        <input id="HidID" type="hidden" name="HidID" runat="server">
        <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
        <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
            <tr>
                <td>
                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                        <tr>                            
                            <td>
                                <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                                    <td style="width: 100%;">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                                                            <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="div_1">
                                                <table cellspacing="0" class="table_data_area" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;                                                            
                                                            <asp:Label ID="labelOTDateFrom" runat="server" Text="加班日期"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="textBoxOTDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                    <td width="8%">
                                                                        ~</td>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="textBoxOTDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;                                                            
                                                            <asp:Label ID="labelPersonType" runat="server" Text="類別"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;                                                          
                                                            <asp:Label ID="labelOTType" runat="server" Text="加班類型"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:DropDownList ID="ddlOTType" runat="server" Width="100%">
                                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="G1">G1</asp:ListItem>
                                                                <asp:ListItem Value="G2">G2</asp:ListItem>
                                                                <asp:ListItem Value="G3">G3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;                                                          
                                                            <asp:Label ID="labelOTStatus" runat="server" Text="狀態"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" colspan="8">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                     <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="ButtonQuery" runat="server" Text="Query" CssClass="button_1" ToolTip="Authority Code:Query"
                                                                            CommandName="Query" OnClick="ButtonQuery_Click"></asp:Button>
                                                                        <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="button_1" ToolTip="Authority Code:Reset"
                                                                            CommandName="Reset" OnClick="ButtonReset_Click"></asp:Button>
                                                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="button_1" ToolTip="Authority Code:Add"
                                                                            CommandName="Add" OnClientClick="return OpenEdit('Add')"></asp:Button>
                                                                        <asp:Button ID="ButtonModify" runat="server" CssClass="button_1" CommandName="Modify" Text="Modify" ToolTip="Authority Code:Modify"
                                                                            OnClientClick="return OpenEdit('Modify')" />
                                                                        <asp:Button ID="ButtonDelete" runat="server" CssClass="button_1" Text="Delete" ToolTip="Authority Code:Delete"
                                                                            CommandName="Delete" OnClick="ButtonDelete_Click" OnClientClick="return CheckDelete()">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonExport" runat="server" CssClass="button_1" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                                            OnClick="ButtonExport_Click"></asp:Button>          
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
                            <td  style=" border:1px solid #C0DDFF;">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                    <td  colspan="3">
                                       <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                            <tr style="cursor: hand">
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                                                                        HorizontalAlign="Center" PageSize="10" PagingButtonType="Image" Width="300px"
                                                                        ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                        DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                                        ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                                                        OnPageChanged="pager_PageChanged"  ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                                    </ess:AspNetPager>
                                                                </div>
                                                            </td>
                                                            <td style="width: 22px;" id="td_show_2">
                                                                <img id="div_img2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">

                                            <script language="JavaScript" type="text/javascript">                                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 64 / 100 + "'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGrid_DataBound">
                                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="No" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed" AutoGenerateColumns="false"
                                                    CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                        AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="DblClick"></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp">
                                                    </SelectedRowStyleDefault>
                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                    </RowAlternateStyleDefault>
                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                        CssClass="tr_data">
                                                        <Padding Left="3px"></Padding>
                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                    </RowStyleDefault>
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                        DataKeyField="" BaseTableName="OTM_AdvanceApply" Key="OTM_AdvanceApply">
                                                        <Columns>
                                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                                <CellTemplate>
                                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                </CellTemplate>
                                                                <HeaderTemplate>
                                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                </HeaderTemplate>
                                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                                </Header>
                                                            </igtbl:TemplatedColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWorkNo%>" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceLocalName%>" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="150">
                                                                <Header Caption="<%$Resources:ControlText,lbl_Unit_edit%>" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="0" Hidden="true">
                                                                <Header Caption="ID" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="0"
                                                                Hidden="true">
                                                                <Header Caption="Status" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="dCode" Key="dCode" IsBound="false" Width="0"
                                                                Hidden="true">
                                                                <Header Caption="dCode" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOTDate%>" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                Width="30">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOverTimeType%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="40">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWeek%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="100">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOTType%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="false"
                                                                Width="50">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBeginTime%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="50">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceEndTime%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Hours" Key="Hours" IsBound="false" Width="40"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceHours%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceStatusName%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ImportFlag" Key="ImportFlag" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceImportFlag%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <%--<igtbl:UltraGridColumn BaseColumnName="ImportRemark" Key="ImportRemark" IsBound="false"
                                                                Width="150">
                                                                <Header Caption="ImportRemark">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>--%>
                                                            <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="false" Width="50"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG1Total%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="false" Width="50"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG2Total%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="false" Width="50"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG3Total%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <%--<igtbl:UltraGridColumn BaseColumnName="MonthAllHours" Key="MonthAllHours" IsBound="true"
                                                                    Width="80" Format="0.0">
                                                                    <Header Caption="MonthAllHours">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>--%>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="250">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWorkDesc%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false" Hidden="true"
                                                                Width="60">
                                                                <Header Caption="IsProject">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="250">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceRemark%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="G2IsForRest" Key="G2IsForRest" IsBound="false"
                                                                Width="60" Hidden="true">
                                                                <Header Caption="<%$Resources:ControlText,istiaoxiu%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBillNo%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="APPROVER" Key="APPROVER" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproverName%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                Width="110">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproveDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="100">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="UPDATE_USER" Key="UPDATE_USER" IsBound="false" Width="70">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifier%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="UPDATE_DATE" Key="UPDATE_DATE" IsBound="false"
                                                                Width="110">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifyDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                             <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                    HeaderClickAction="Select" Width="100px" >
                                                                    <CellTemplate>
                                                                        <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server">查看進度</asp:LinkButton> 
                                                                    </CellTemplate>
                                                                    <HeaderTemplate>  
                                                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                                                                                                  
                                                                    </HeaderTemplate>
                                                                    <Header Caption="<%$Resources:ControlText,jindutu%>" >
                                                                    </Header>
                                                                </igtbl:TemplatedColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="false"
                                                                Width="0" Hidden="true">
                                                                <Header Caption="OTMSGFlag" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">                                                document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script language="javascript">            document.write("<div id='divEdit'style='display:none;height:" + document.body.clientHeight * 84 / 100 + "'>");</script>

        <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
            <tr>
                <td>
                    <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                        scrolling="no" style="border: 0"></iframe>
                </td>
            </tr>
        </table>

        <script language="JavaScript" type="text/javascript">            document.write("</div>");</script>

        <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server"
            OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
        </igtblexp:UltraWebGridExcelExporter>
    </form>
</body>
</html>
