<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbnormalAttendanceHandle.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlowForm.AbnormalAttendanceHandle" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridKaoQinData_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}
			var grid = igtbl_getGridById('UltraWebGridKaoQinData');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridKaoQinData_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridKaoQinData_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			
			DisplayRowData(row);
			return false;
		}
		function UltraWebGridKaoQinData_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if( row != null)
			{
				document.getElementById("HiddenBillNo").value=row.getCellFromKey("BillNo").getValue();
			}
		}
        function GridCancel()
		{
	        document.getElementById("ProcessFlag").value="";
	        
	        return true;
		}		
        function UpProgress()
		{
			document.getElementById("ButtonImportSave").style.display="none";
			document.getElementById("ButtonImport").disabled="disabled";
			document.getElementById("ButtonExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="";
			document.getElementById("labelupload").innerText = "System is Loading......";			
		}
		
		function CheckSendAudit()//送簽
		{
		    var grid = igtbl_getGridById('UltraWebGridKaoQinData');
		    var gRows = grid.Rows;
		    var Count=0;
		    var State="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridKaoQinData_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         State=gRows.getRow(i).getCellFromKey("Status").getValue();
			         switch (State)
                        {
                            case "2":
                                break;
                             default:
	                            alert(Message.common_message_audit_unsendaudit);
	                            return false;
                                break;
		                }	 
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.common_message_sendaudit_noselect);
		        return false;
		    }
            try
		    {
		        if(confirm(Message.common_message_data_return))
		        {
			        document.getElementById("ButtonSendAudit").style.display="none";	
			        document.getElementById("imgSendAuditWaiting").style.display="";		        
	                FormSubmit("<%=sAppPath%>");
			        return true;
			    }
			    else
			    {			    
			        return false;
			    }
			}
			catch(err)
			{
			    return true;
			}
		}
      function CheckCancel()
      {
        document.getElementById("ProcessFlag").value="";
        FormSubmit("<%=sAppPath%>");
        return true;
      }
		function OrgAudit()//選擇組織送簽
		{
		    document.getElementById("HiddenOrgCode").value="";
		    var grid = igtbl_getGridById('UltraWebGridKaoQinData');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(document.getElementById("UltraWebGridKaoQinData_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			        Count+=1;			        		
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.common_message_data_select);
		        return false;
		    }		    
            var windowWidth=500,windowHeight=600;
	        var X=(screen.availWidth-windowWidth)/2;
	        var Y=(screen.availHeight-windowHeight)/2;
	        var Revalue=window.showModalDialog("<%=sAppPath%>/Sys/TreeDataPickForm.aspx?DataType=Department&condition=&modulecode="+
	          document.getElementById("ModuleCode").value,window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	        if(Revalue!=undefined)
	        {	
		        document.getElementById("HiddenOrgCode").value=Revalue.split(';')[0];
	        }
		    //document.getElementById("ButtonSendAudit").click();
		    if(document.getElementById("HiddenOrgCode").value.length>0)
		    {
		        //setTimeout('__doPostBack(\'ButtonSendAudit\',\'\')', 0);
		        document.getElementById("ButtonSendAudit").click();
		    }
		    return false;
		}
        function OpenAuditStatus()//查看簽核進度
		{
		    var grid = igtbl_getGridById('UltraWebGridKaoQinData');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(gRows.getRow(i).getSelected())
			    {
			        Count+=1;			        		
			    }
		    }			
		    if(Count==0)
		    {
		         alert(Message.common_message_data_select);
		        return false;
		    }			    
		    var ModuleCode="<%=base.Request["ModuleCode"].ToString() %>";
		    var BillNo=igtbl_getElementById("HiddenBillNo").value;
	        var width=550;
            var height=150;
            openEditWin("../../PCM/PCMAuditStatusForm.aspx?ModuleCode="+ModuleCode+"&BillNo="+BillNo,"AuditStatus",width,height);
            return false;
		}
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="";
            document.all("PanelBatchWorkNo").style.top=document.all("textBoxEmployeeNo").style.top;
            document.getElementById("textBoxBatchEmployeeNo").style.display="";
            document.getElementById("textBoxEmployeeNo").value="";
            document.getElementById("textBoxBatchEmployeeNo").value="";
            document.getElementById("textBoxBatchEmployeeNo").focus();
            return false;
        }
        function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("textBoxBatchEmployeeNo").style.display="none";
            document.getElementById("textBoxBatchEmployeeNo").value="";
        }
        //調用放大鏡
        function setSelector(ctrlCode, ctrlName, flag,moduleCode) {
            var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;
        }
        function GetSignMap() {
            var grid = igtbl_getGridById('UltraWebGridKaoQinData');
            var gRows = grid.Rows;
            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue();
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }
            //alert("BillNo:"+BillNo)
            if (BillNo == null) {
                alert(Message.wfm_nosign_message);
                return false;
            }
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            var Revalue = window.showModalDialog("../WorkFlow/SignLogAndMap.aspx?Doc=" +
	          BillNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
        <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
        <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
            <tr>
                <td>
                    <table cellspacing="1" cellpadding="0" width="98%" align="center">
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
                            <td>
                                <table id="table_condition" class="table_data_area">
                                    <tr>
                                        <td>
                                            <div id="div_1">
                                                <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                                    <tr class="tr_data_1">
                                                        <td style="width: 11%">
                                                            &nbsp;<asp:Label ID="lblOrgCode" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" CssClass="input_textBox_1"
                                                                            Style="display: none"></asp:TextBox>
                                                                        <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="cursor: hand">
                                                                        <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;<asp:Label ID="lblIsFillCard" runat="server" Text="lblIsFillCard"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:DropDownList ID="ddlIsMakeup" runat="server" Width="100%">
                                                                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="N">N</asp:ListItem>
                                                                <asp:ListItem Value="Y">Y</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;<asp:Label ID="lblIsSupporter" runat="server" Text="lblIsSupporter"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <asp:DropDownList ID="ddlIsSupporter" runat="server" Width="100%">
                                                                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="N">N</asp:ListItem>
                                                                <asp:ListItem Value="Y">Y</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr class="tr_data_2">
                                                        <td class="td_label" width="11%">
                                                            &nbsp;<asp:Label ID="lblEmployeeNo" runat="server" ResourceID="common.employeeno"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblName" runat="server" ResourceID="common.name"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="textBoxName" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblAttendanceDate" runat="server" ResourceID="kqm.otm.date.from"></asp:Label>
                                                        </td>
                                                        <td width="23%">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="textBoxKQDateFrom" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                    </td>
                                                                    <td width="8%">
                                                                        <asp:Label ID="DataFlag" runat="server" Text="~" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="textBoxKQDateTo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="tr_data_1">
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblAttendHandleStatus" runat="server" Text="lblAttendHandleStatus"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="input_textBox">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblExceptionType" runat="server" Text="lblExceptionType"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <cc1:DropDownCheckList ID="ddlExceptionType" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                                                                Width="250" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                                                                TextWhenNoneChecked="" DisplayTextWidth="250" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                                                                runat="server">
                                                            </cc1:DropDownCheckList>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblShiftNoTypes" runat="server" Text="lblShiftNoType"></asp:Label>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlShiftNo" runat="server" Width="100%" CssClass="input_textBox">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100%; height: 25px;">
                                                                      <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="ButtonQuery" runat="server" CssClass="button_1" OnClick="ButtonQuery_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonReset" runat="server" CssClass="button_1" OnClick="ButtonReset_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonImport" runat="server" CssClass="button_1" OnClick="ButtonImport_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonExport" runat="server" CssClass="button_1" OnClick="ButtonExport_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonModify" runat="server" CssClass="button_1" OnClick="ButtonModify_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="button_1" OnClientClick="GridCancel()"
                                                                            OnClick="ButtonCancel_Click"></asp:Button>
                                                                        <asp:Button ID="ButtonSave" runat="server" CssClass="button_1" OnClick="ButtonSave_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonConfirm" runat="server" CssClass="button_1" OnClick="ButtonConfirm_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonUnConfirm" runat="server" CssClass="button_large" OnClick="ButtonUnConfirm_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="ButtonSendAudit" runat="server" ToolTip="Authority Code:Export" CssClass="button_1"
                                                                            OnClientClick="return CheckSendAudit()" OnClick="ButtonSendAudit_Click"></asp:Button>
                                                                   <%--     <asp:Button ID="ButtonOrgAudit" runat="server" CommandName="OrgAudit" Text="OrgAudit"
                                                                            ToolTip="Authority Code:OrgAudit" CssClass="button_large" OnClick="ButtonOrgAudit_Click"  Visible="false"/>
                                                                            --%></asp:Panel>
                                                                        &nbsp;
                                                                        <asp:CheckBox ID="CheckBoxFlag" runat="server" onClick="return onChangeFlag(this)" />
                                                                        <asp:Label ID="lblFlagB" runat="server" Text="lblFlagB" ForeColor="red"></asp:Label>
                                                                        <%--<asp:CheckBox ID="CheckBoxFlagB" runat="server" onClick="return onChangeFlag(this)" />--%>
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
                                                                                                            <td class="td_label" align="center">
                                                                                                                <asp:Button ID="ButtonClose" runat="server" Text="關閉" CommandName="Close" ToolTip="Authority Code:Close"
                                                                                                                    OnClientClick="return HiddenShowDetail()"></asp:Button>
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel class="inner_table" ID="PanelData" runat="server" Width="100%" Visible="true">
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
                                                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
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
                                        <tr>
                                            <td>
                                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                    <tr style="width: 100%">
                                                        <td valign="top" width="19px" background="../CSS/Images_new/EMP_05.gif" height="18">
                                                            <img height="18" src="../CSS/Images_new/EMP_01.gif" width="19">
                                                        </td>
                                                        <td background="../CSS/Images_new/EMP_07.gif" height="19px">
                                                        </td>
                                                        <td valign="top" width="19px" background="../CSS/Images_new/EMP_06.gif" height="18">
                                                            <img height="18" src="../CSS/Images_new/EMP_02.gif" width="19">
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%">
                                                        <td width="19" background="../CSS/Images_new/EMP_05.gif">
                                                            &nbsp;
                                                        </td>
                                                        <td>

                                                            <script language="JavaScript" type="text/javascript">                                                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 57 / 100 + "'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGridKaoQinData" runat="server" Width="100%" Height="100%"
                                                                OnDataBound="UltraWebGridKaoQinData_DataBound" OnUpdateRowBatch="UltraWebGridKaoQinData_UpdateRowBatch">
                                                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
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
                                                                    <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                                        DataKeyField="" BaseTableName="AbnormalAttendanceHandle" Key="AbnormalAttendanceHandle">
                                                                        <Columns>
                                                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                                HeaderClickAction="Select" HeaderText="CheckBox" Width="30px">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <CellTemplate>
                                                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                                </CellTemplate>
                                                                                <HeaderTemplate>
                                                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                                </HeaderTemplate>
                                                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="true">
                                                                                </Header>
                                                                            </igtbl:TemplatedColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WORKNO" AllowUpdate="No" HeaderText="WorkNo"
                                                                                IsBound="false" Key="WorkNo" Width="70px">
                                                                                <Header Caption="<%$Resources:ControlText,gvAttendHandleWorkNo%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" AllowUpdate="No" IsBound="false"
                                                                                Key="LocalName" Width="60">
                                                                                <Header Caption="<%$Resources:ControlText,gvAttendHandleLocalName%>" Fixed="True">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" AllowUpdate="No" IsBound="false"
                                                                                Key="DepName" Width="120px">
                                                                                <Header Caption="<%$Resources:ControlText,gvDepName%>" Fixed="True">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" AllowUpdate="No" IsBound="false" Key="KQDate"  Format="yyyy-MM-dd"
                                                                                Width="70px">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadKQDate%>" Fixed="True">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" AllowUpdate="No" IsBound="false"
                                                                                Key="StatusName" Width="50">
                                                                                <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" AllowUpdate="No" IsBound="false"
                                                                                Key="ShiftDesc" Width="170px">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="isMakeUp" AllowUpdate="No" IsBound="false"
                                                                                Key="isMakeUp" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvIsMakeUp%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" AllowUpdate="No" IsBound="false"
                                                                                Key="OnDutyTime" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOnDutyTime%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" AllowUpdate="No" IsBound="false"
                                                                                Key="OffDutyTime" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTOnDutyTime" AllowUpdate="No" IsBound="false"
                                                                                Key="OTOnDutyTime" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvOTOnDutyTime%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTOffDutyTime" AllowUpdate="No" HeaderText="OTOffDutyTime"
                                                                                IsBound="false" Key="OTOffDutyTime" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOTOffDutyTime%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" AllowUpdate="No" IsBound="false"
                                                                                Key="AbsentQty" Width="60px">
                                                                                <Header Caption="<%$Resources:ControlText,gvAbsentQty%>">
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionTypeName" AllowUpdate="No" IsBound="false"
                                                                                Key="ExceptionTypeName" Width="80px">
                                                                                <Header Caption="<%$Resources:ControlText,gvExceptionTypeName%>">
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" AllowUpdate="No" IsBound="false"
                                                                                Key="ReasonRemark" Width="150px">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark%>">
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="AbsentTotal" IsBound="false" Key="AbsentTotal"
                                                                                Width="80" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvAbsentTotal%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Approver" Key="Approver" IsBound="false" Width="60">
                                                                                <Header Caption="<%$Resources:ControlText,gvApprover%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                                Width="110">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadApproveDate%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="120">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadApRemark%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" AllowUpdate="No" IsBound="false" Key="BillNo"
                                                                                Width="160">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>">
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="12" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                                HeaderClickAction="Select" Width="100px">
                                                                                <CellTemplate>
                                                                                    <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server">查看進度</asp:LinkButton>
                                                                                </CellTemplate>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                                                </Header>
                                                                            </igtbl:TemplatedColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" IsBound="false" Key="ShiftNo" Hidden="true">
                                                                                <Header Caption="ShiftNo">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Status" AllowUpdate="No" IsBound="false" Key="Status"
                                                                                Hidden="true">
                                                                                <Header Caption="Status">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="dCode" AllowUpdate="No" IsBound="false" Key="dCode"
                                                                                Hidden="true">
                                                                                <Header Caption="dCode">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionType" AllowUpdate="No" IsBound="false"
                                                                                Key="ExceptionType" Hidden="true">
                                                                                <Header Caption="ExceptionType>">
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>

                                                            <script language="JavaScript" type="text/javascript">                                                                document.write("</DIV>");</script>

                                                        </td>
                                                        <td width="19" background="../CSS/Images_new/EMP_06.gif">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="19" background="../CSS/Images_new/EMP_03.gif" height="18">
                                                            &nbsp;
                                                        </td>
                                                        <td background="../CSS/Images_new/EMP_08.gif" height="18">
                                                            &nbsp;
                                                        </td>
                                                        <td width="19" background="../CSS/Images_new/EMP_04.gif" height="18">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                        <tr style="cursor: hand">
                                            <td class="tr_table_title" width="100%">
                                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                    <tr style="width: 100%;" id="tr1" class="tr_title_center">
                                                        <td style="width: 100%;">
                                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                                font-size: 13px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblImportArea" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 22px;">
                                                            <div id="Div2">
                                                                <img id="Img2" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label" width="100%" align="left">
                                                <a href="../ExcelModel/KaoQinDataSample.xls">&nbsp;<asp:Label ID="labelUploadText"
                                                    runat="server"></asp:Label>
                                                </a>&nbsp;
                                                <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                                <asp:Button ID="ButtonImportSave" runat="server" Text="ImportSave" OnClick="ButtonImportSave_Click"
                                                    OnClientClick="javascript:UpProgress();" CssClass="button_1" />
                                                <img id="imgWaiting" src="../CSS/images/clocks.gif" border="0" style="display: none;
                                                    height: 20px;" />
                                                <asp:Label ID="labelupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="labeluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="height: 25;">
                                                &nbsp;<asp:Label ID="importremark" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <script language="javascript">                                                    document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 53 / 100 + "'>");</script>

                                                <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                        RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                        BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                        Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                                        AutoGenerateColumns="false">
                                                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                            </BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="100%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="HRM_Import" Key="HRM_Import">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="40%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg%>" Fixed="True">
                                                                    </Header>
                                                                    <CellStyle ForeColor="red">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvAttendHandleWorkNo%>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="KQDate" AllowUpdate="No" IsBound="false" Key="KQDate"
                                                                    Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadKQDate%>" Fixed="True">
                                                                    </Header>
                                                                    <Footer>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" IsBound="false" Key="ReasonRemark"
                                                                    Width="40%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark%>" Fixed="True">
                                                                    </Header>
                                                                    <Footer>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                                <script language="JavaScript" type="text/javascript">                                                    document.write("</DIV>");</script>

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
        <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server"
            OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
        </igtblexp:UltraWebGridExcelExporter>

        <script language="javascript">            document.write("<div id='divApproved' style='display:none;height:" + document.body.clientHeight * 90 / 100 + "'>");</script>

        <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
            <tr>
                <td>
                    <iframe id="iframeApproved" src="" width="100%" height="100%" frameborder="0" scrolling="no"
                        style="border: 0"></iframe>
                </td>
            </tr>
        </table>

        <script language="JavaScript" type="text/javascript">            document.write("</div>");</script>

    </div>

    <script language="javascript">
        document.all("PanelKQ").style.display = "none";
        onChangeFlag(document.all("CheckBoxFlag"));
        function ShowDetail(WorkNo, KQDate, ShiftNo) {
            debugger;
            document.all("PanelKQ").style.display = "";
            document.all("PanelKQ").style.top = document.all("ButtonModify").style.top;
            $.ajax(
                {
                    type: "post",
                    url: "AjaxProcess/AbnormalAttendanceHandle.ashx?WorkNo=" + escape(WorkNo) + "&&KQDate=" + escape(KQDate) + "&&ShiftNo=" + escape(ShiftNo),
                    dataType: "text",
                    async: false,
                    success: function(data) {
                        if (data == "") {

                        }
                        else {
                            document.getElementById("divKQ").innerHTML = data;
                        }
                    }
                }
                );
            return;
        }
        function HiddenShowDetail() {
            document.all("PanelKQ").style.display = "none";
            return false;
        }
        function onChangeFlag(obj) {
            if (obj.checked) {
                //document.getElementById("KQDateFlag").style.width = "100%";
                document.getElementById("textBoxKQDateTo").value = "";
                document.getElementById("ddlShiftNo").value = "";
                document.getElementById("textBoxKQDateTo").style.display = "none";
                document.getElementById("DataFlag").style.display = "none";
                document.getElementById("ddlShiftNo").disabled = "disabled";
            }
            else {
                //document.getElementById("KQDateFlag").style.width = "50%";
                document.getElementById("textBoxKQDateTo").style.display = "";
                document.getElementById("DataFlag").style.display = "";
                document.getElementById("ddlShiftNo").disabled = "";
            }
        }
    </script>

    </form>
</body>
</html>
