<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFMBillCenterForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WFMBillCenterForm" %>


<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css"/>
    
    <script type="text/javascript"><!--
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridBill_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridBill');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridBill_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridBill_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
        function ShowBillDetail(BillNo,BillTypeCode)
		{
	        var ModuleCode = document.getElementById("ModuleCode").value;
	        var width=screen.width*0.95;
            var height=screen.height*0.8;            
            switch (BillTypeCode)
            {
                case "LeaveTypeAE":
                case "LeaveTypeAF":
                case "LeaveTypeAG":
                case "LeaveTypeAH":
                case "LeaveTypeAI":
                case "LeaveTypeAJ":
                case "LeaveTypeAK":
                case "LeaveTypeM":
                case "LeaveTypeN":
                case "LeaveTypeL":
                    openEditWin("WFMBillCenterLeaveApproveForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
                    break;
                default:
                   // openEditWin("WFMBillCenterApproveForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode,"WFMBillDetail",width,height);
                     window.open("WFMBillCenterApproveForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode, 'WFMBillDetail', 'height=' + height + ', width=' + width + ', top=0,left=0, toolbar=no, menubar=no, scrollbars=yes,resizable=no,location=no, status=no');
                    break;
            }
		}
		function OpenApprove(isApproved)//彈出新增或修改頁面
		{
		    var ModuleCode = document.getElementById("ModuleCode").value;
            if(isApproved)
            {
                document.getElementById("iframeApproved").src="WFMBillApprovedForm.aspx?ModuleCode="+ModuleCode;
                document.getElementById("topTable").style.display="none";
                document.getElementById("divApproved").style.display="";
                document.getElementById("ButtonApproved").className="selected_button";
                document.getElementById("ButtonApprove").className="audit_button";
            }
            else
            {
                document.getElementById("iframeApproved").src="";
                document.getElementById("topTable").style.display="";
                document.getElementById("divApproved").style.display="none";
                document.getElementById("ButtonApproved").className="audit_button";
                document.getElementById("ButtonApprove").className="selected_button";
            }
            return false;
		}
		
		function CheckBatchAudit()
        {
	        var grid = igtbl_getGridById('UltraWebGridBill');
	        var gRows = grid.Rows;
	        var Count=0;
	        var State="";
	        for(i=0;i<gRows.length;i++)
	        {
		        if(igtbl_getElementById("UltraWebGridBill_ci_0_0_"+i+"_CheckBoxCell").checked)
		        {
		             Count+=1;
		        }
	        }			
	        if(Count==0)
	        {
	            alert("<%=this.GetResouseValue("common.message.data.select")%>");
	            return false;
	        }
	        if(confirm("<%=this.GetResouseValue("common.message.data.return")%>"))
	        {
		        FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
		
		function OpenManagerLeave()
        {
		    var ModuleCode = document.getElementById("ModuleCode").value;
            location.href="WFMManagerDeputyForm.aspx?ModuleCode="+ModuleCode;
            return false;
        }
        function OpenLoginLog()
		{
	        var ModuleCode = document.getElementById("ModuleCode").value;
	        var width=screen.width*0.7;
            var height=screen.height*0.6;
            openEditWin("../Sys/SysLoginForm.aspx?ModuleCode="+ModuleCode,"WFMLoginLog",width,height);
            return false;
		}
        function OpenChangePassWord()
		{
	        var width=500;
            var height=250;
            openEditWin("WFMChangePassWordForm.aspx","WFMLoginLog",width,height);
            return false;
		}
	--></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
        <table cellspacing="1" cellpadding="0" width="98%" >
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td  width="100%" runat="server">
                                <asp:Button ID="ButtonApprove" Width="100px" Height="26px" runat="server" CssClass="selected_button"
                                    Text="Approve" ToolTip="Authority Code:Approve" CommandName="Approve" OnClientClick="return OpenApprove(false)">
                                </asp:Button>
                                <asp:Button ID="ButtonApproved" Width="100px" Height="26px" runat="server" CssClass="audit_button"
                                    Text="Approved" ToolTip="Authority Code:Approved" CommandName="Approved" OnClientClick="return OpenApprove(true)">
                                </asp:Button>
                      
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="1" id="topTable" cellpadding="0" width="98%" >
            <tr>
                <td>
                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                        <tr>
                            <td>
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                    <td>
                                    <div style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr2">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                 <asp:Label ID="lblwqh" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <div id="Div2">
                                                        <img id="img1" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    </td>
                                       
                                    </tr>
                                    <tr id="BatchAudit" runat="server" style=" display:none;">
                                        <td width="100%">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td width="100%">
                                                        &nbsp;
                                                        <asp:Button ID="ButtonBatchAudit" ONMOUSEOVER="className='input_button_over'" ONMOUSEOUT="className='input_button'"
                                                            runat="server" CssClass="input_button" Text="BatchAudit" ToolTip="Authority Code:BatchAudit"
                                                            CommandName="BatchAudit" OnClick="ButtonBatchAudit_Click" OnClientClick="return CheckBatchAudit()">
                                                            </asp:Button>
                                                        <asp:Label ID="labelBatchAuditMessage" ForeColor="blueViolet" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">

                                            <script language="JavaScript" type="text/javascript">                                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 72 / 100 + "'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%"
                                                OnDataBound="UltraWebGridBill_DataBound">
                                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridBill" TableLayout="Fixed"
                                                    CellClickActionDefault="RowSelect">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents></ClientSideEvents>
                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp">
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
                                                    <igtbl:UltraGridBand BaseTableName="WFM_Bill" Key="WFM_Bill">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="BillTypeName" HeaderText="BillTypeName" IsBound="True"
                                                                Key="BillTypeName" Width="18%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_doctype %>" Fixed="true">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" HeaderText="BillNo" IsBound="True"
                                                                Key="BillNo" Width="18%">
                                                                <Header Caption="<%$Resources:ControlText,lbllblBillNo %>" Fixed="true">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgName" HeaderText="OrgName" IsBound="True"
                                                                Key="OrgName" Width="33%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_Unit_edit %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" HeaderText="StatusName" IsBound="True"
                                                                Key="StatusName" Width="7%">
                                                                <Header Caption="<%$Resources:ControlText,lblKaoqinStatus %>">
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyManName" HeaderText="ApplyManName" IsBound="True"
                                                                Key="ApplyManName" Width="8%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_cbr %>">
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ApplyDate" HeaderText="ApplyDate" IsBound="True"
                                                                Key="ApplyDate" Width="16%">
                                                                <Header Caption="<%$Resources:ControlText,lbl_cbrq %>">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Status" HeaderText="Status" IsBound="True"
                                                                Key="Status" Hidden="true">
                                                                <Header Caption="Status">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="OrgCode" HeaderText="OrgCode" IsBound="True"
                                                                Key="OrgCode" Hidden="true">
                                                                <Header Caption="OrgCode">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillTypeCode" HeaderText="BillTypeCode" IsBound="True"
                                                                Key="BillTypeCode" Hidden="true">
                                                                <Header Caption="BillTypeCode">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
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

        <script language="javascript">            document.write("<div id='divApproved' style='display:none;height:" + document.body.clientHeight * 79 / 100 + "'>");</script>

        <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
            <tr>
                <td>
                    <iframe id="iframeApproved" class="top_table" src="" width="100%" height="100%" frameborder="0"
                        scrolling="no" style="border: 0"></iframe>
                </td>
            </tr>
        </table>

        <script language="JavaScript" type="text/javascript">            document.write("</div>");</script>

        <asp:Button ID="ButtonReset" Width="0" 
             runat="server" 
            Text="Reset" ToolTip="Authority Code:Reset" CommandName="Reset" OnClick="ButtonReset_Click">
        </asp:Button>
    </form>
</body>
</html>
