<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowProjectApply.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowProjectApply" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WorkFlowProjectApply</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

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
		
        function UpProgress()
		{
			document.getElementById("ButtonImportSave").style.display="none";
			document.getElementById("btnImport").disabled="disabled";
			document.getElementById("ButtonExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="";
			document.getElementById("labelupload").innerText = "System is Loading......";			
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
		
		function DblClick(gridName, id)//修改
		{
		    var ProcessFlag="Modify";
		    OpenEdit(ProcessFlag)
		    return 0;
		}
		
		function OpenEdit(ProcessFlag)//新增
		{	
		    var EmployeeNo = document.getElementById("HiddenWorkNo").value;
		    var ID= document.getElementById("HidID").value;
		    // var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    var ModuleCode= document.getElementById("ModuleCode").value;
		    document.getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {
		        var grid = igtbl_getGridById('UltraWebGrid');
			    var gRows = grid.Rows;
			    var Count=0;
			    for(i=0;i<gRows.length;i++)
			    {
				    if(gRows.getRow(i).getSelected()) {
				        if (gRows.getRow(i).getCellFromKey("Status").getValue() == "0" || gRows.getRow(i).getCellFromKey("Status").getValue() == "3") {
				            OTType = gRows.getRow(i).getCellFromKey("OTType").getValue();
				            Count += 1;
				        }
				        else {
				            alert(Message.checkapproveflag);
				            return false;
				        }
				    }
			    }			
			    if(Count==0)
			    {
			       //alert("<%=this.GetResouseValue("common.message.data.select")%>");
			        alert(Message.AtLastOneChoose);
			        return false;
			    }
            }
            document.getElementById("iframeEdit").src="WorkFlowProjectApplyAdd.aspx?EmployeeNo="+EmployeeNo+"&ID="+ID+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
            document.getElementById("topTable").style.display="none";
            document.getElementById("div_2").style.display="none";
            document.getElementById("divEdit").style.display="";
            return false;
		}
		
		function CheckAudit()//核准
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
		             State=gRows.getRow(i).getCell(5).getValue();
		             switch (State)
                        {
                            case "0":
                            case "3":
                                break;
                             default:
                                 alert(Message.AuditUnaudit);
                                return false;
                                break;
	                    }
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if (confirm(Message.DataReturn))
	        {
		       //FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          
          function CheckDelete()//刪除
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
		             State=gRows.getRow(i).getCell(5).getValue();
		             switch (State)
                        {
                            case "0":
                            case "3":
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
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if (confirm(Message.DataReturn))
	        {
		       // FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          
		function CheckCancelAudit()//取消核准
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
		             State=gRows.getRow(i).getCell(5).getValue();
		             switch (State)
                        {
                            case "2":
                                break;
                             default:
                                 alert(Message.AuditUncancelaudit);
                                return false;
                                break;
	                    }	 
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if (confirm(Message.DataReturn))
	        {
		       // FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          
          function CheckSendAudit()//送簽
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
//		             State=gRows.getRow(i).getCell(5).getValue();
//		             switch (State)
//                        {
//                            case "0":
//                                break;
//                             default:
//                                alert("非未核准的申請單不能送簽");
//                                return false;
//                                break;
//	                    }	 
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if (confirm(Message.DataReturn))
	        {
//		        document.getElementById("ButtonSendAudit").style.display="none";	
//		        document.getElementById("imgSendAuditWaiting").style.display="";
//		        document.getElementById("labeSendAudit").innerText = "System is Loading......";
		        //FormSubmit("<%=sAppPath%>");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
		
	    function GetSignMap()//查看簽核進度
	    {
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
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
		
		function OrgAudit()//選擇組織送簽
		{
		    document.getElementById("HiddenOrgCode").value="";
		    var grid = igtbl_getGridById('UltraWebGrid');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(document.getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			        Count+=1;			        		
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
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


        function setSelector(ctrlCode, ctrlName, moduleCode) {
            var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;
        }
        
 --> </script>

</head>
<body>
    <form id="form1" runat="server">
    <%--<ControlLib:Title ID="Title1" runat="server"></ControlLib:Title>--%>
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <input id="Hidden1" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HidID" type="hidden" name="HidID" runat="server">
    <input id="Hidden2" type="hidden" name="ModuleCode" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <input id="Hidden3" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
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
                                                <img id="div_img_1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
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
                              <td colspan="2">
                                  <div id="div_1">
                                    <table cellspacing="0" cellpadding="0" width="100%" id="TABLE1">
                                        <tr>
                                            <td class="td_label" width="10%">
                                                &nbsp;
                                                <asp:Label ID="labelDepcode" runat="server" Text="單位:"></asp:Label>
                                            </td>
                                            <td class="td_input" width="22%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                Style="display: none"></asp:TextBox>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand">
                                                            <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif"></asp:Image>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="LabelBillNo" runat="server" Text="簽核單號:"></asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="textBoxBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="td_label" width="11%">
                                                &nbsp;
                                                <asp:Label ID="labelEmployeeNo" runat="server" Text="工號:"></asp:Label>
                                                
                                            </td>
                                            <td class="td_input" width="22%">
                                                <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                    z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                    border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                    border-bottom: #0000ff 1px solid; position: absolute; left: 11%; float: left;
                                                    display: none;">
                                                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                        <tr>
                                                            <td>
                                                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td class="td_label" width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                        <font color="red">Ⅹ</font>
                                                                                        批量查詢(輸入工號以回車鍵區分)
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td_label" width="100%">
                                                                                        <asp:TextBox ID="textBoxBatchEmployeeNo" runat="server" TextMode="MultiLine" Height="100"
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
                                                    <%-- <iframe src="JavaScript:false" style="position: absolute; visibility: inherit; top: 0px;
                                                            left: 0px; width: 225px; height: 100px; z-index: -1; filter='progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                        </iframe>--%>
                                                </div>
                                            </td>
                                            <td class="td_label" width="11%">
                                                &nbsp;<!--姓名-->
                                                <asp:Label ID="labelName" runat="server" Text="姓名:"></asp:Label>
                                            </td>
                                            <td class="td_input" width="22%">
                                                <asp:TextBox ID="textBoxName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="11%">
                                                &nbsp;<!--加班日期-->
                                                <asp:Label ID="labelOTDateFrom" runat="server" Text="加班日期:"></asp:Label>
                                            </td>
                                            <td class="td_input" width="23%">
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td width="46%">
                                                            <asp:TextBox ID="textBoxOTDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                        <td width="8%">
                                                            ~
                                                        </td>
                                                        <td width="46%">
                                                            <asp:TextBox ID="textBoxOTDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="td_label" width="11%">
                                                &nbsp;
                                                <asp:Label ID="labelPersonType" runat="server" Text="類別:"></asp:Label>
                                            </td>
                                            <td class="td_input" width="22%">
                                                <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_label" width="11%">
                                                &nbsp;<!--加班類型-->
                                                <asp:Label ID="labelOTType" runat="server" Text="加班類型:"></asp:Label>
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
                                                &nbsp;<!--核准-->
                                                <asp:Label ID="labelOTStatus" runat="server" Text="核准:"></asp:Label>
                                            </td>
                                            <td class="td_input" width="23%">
                                                <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="td_label" colspan="6">
                                                <table width="100%">
                                                    <tr>
                                                        <td width="100%">
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <asp:Button ID="ButtonQuery" runat="server" ToolTip="Authority Code:Query" CssClass="button_1"
                                                                CommandName="Query" OnClick="ButtonQuery_Click"></asp:Button>
                                                            <asp:Button ID="ButtonReset" runat="server" ToolTip="Authority Code:Reset" CssClass="button_1"
                                                                CommandName="Reset" OnClick="ButtonReset_Click"></asp:Button>
                                                            <asp:Button ID="btnAdd" runat="server"  ToolTip="Authority Code:Add" CommandName="Add" CssClass="button_1"
                                                                OnClientClick="return OpenEdit('Add')"></asp:Button>
                                                            <asp:Button ID="btnModify" runat="server"  ToolTip="Authority Code:Modify" CssClass="button_1"
                                                                CommandName="Modify" OnClientClick="return OpenEdit('Modify')"></asp:Button>
                                                            <asp:Button ID="btnDelete" runat="server"  ToolTip="Authority Code:Delete" CssClass="button_1"
                                                                CommandName="Delete" OnClick="ButtonDelete_Click" OnClientClick="return CheckDelete()">
                                                            </asp:Button>
                                                            <asp:Button ID="btnImport" runat="server"  ToolTip="Authority Code:Import" CssClass="button_1"
                                                                CommandName="Import" OnClick="ButtonImport_Click"></asp:Button>
                                                            <asp:Button ID="ButtonExport" runat="server" CommandName="Export" ToolTip="Authority Code:Export" CssClass="button_1"
                                                                OnClick="ButtonExport_Click"></asp:Button>
                                                            <asp:Button ID="btnAudit" runat="server"  CommandName="Audit" ToolTip="Authority Code:Audit" CssClass="button_1"
                                                                OnClick="ButtonAudit_Click" OnClientClick="return CheckAudit()"></asp:Button>
                                                            <asp:Button ID="btnCancelAudit" runat="server"  ToolTip="Authority Code:CancelAudit" CssClass="button_1"
                                                                CommandName="CancelAudit" OnClick="ButtonCancelAudit_Click" OnClientClick="return CheckCancelAudit()">
                                                            </asp:Button>
                                                            <asp:Button ID="btnSendAudit" runat="server"  ToolTip="Authority Code:SendAudit" CssClass="button_1"
                                                                CommandName="SendAudit" OnClick="btnSendAduit_Click" OnClientClick="return CheckSendAudit()">
                                                            </asp:Button>
                                                           <%-- <asp:Button ID="ButtonOrgAudit" runat="server" CommandName="OrgAudit"  CssClass="button_1"
                                                                ToolTip="Authority Code:OrgAudit" onclick="ButtonOrgAudit_Click"  Visible="false" />--%>
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
                   
                     <asp:Panel class="inner_table" ID="PanelData" runat="server" Width="100%" Visible="true">
                        <tr>
                         <td>
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
                       <td>
                           <table id="table_display">
                        <tr>
                            <td colspan="3">                            
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

                                            <script language="JavaScript" type="text/javascript"> document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 57 / 100 + "'>");</script>

                                             <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%"  OnDataBound="UltraWebGrid_DataBound">
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
                                                           <%-- <igtbl:UltraGridColumn BaseColumnName="BuName" Key="BuName" IsBound="false" Width="100">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBuName%>" Fixed="True">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>--%>
                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="120">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80"  Format="yyyy/MM/dd">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="95">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOTType%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="false"
                                                                Width="50" Format="HH:mm">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBeginTime%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="50" Format="HH:mm">
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
                                                            <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="false" Width="50"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG1Total%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="false" Width="120"
                                                                Format="0.0">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG2Total%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="false" Width="130"
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
                                                            <igtbl:UltraGridColumn BaseColumnName="G2ISFORREST" Key="G2ISFORREST" IsBound="false" Width="250">
                                                                <Header Caption="<%$Resources:ControlText,istiaoxiu%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceRemark%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBillNo%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="UPDATE_USER" Key="UPDATE_USER" IsBound="false" Width="70" >
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifier%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="UPDATE_DATE" Key="UPDATE_DATE" IsBound="false" Format="yyyy/MM/dd"
                                                                Width="110">
                                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifyDate%>">
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
                                                            
                                                             <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                    HeaderClickAction="Select" Width="100px" Key="CheckBoxAll">
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
                                                                <Header Caption="OTMSGFlag">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false"
                                                                Width="0" Hidden="true">
                                                                <Header Caption="IsProject">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>


                                            <script language="JavaScript" type="text/javascript"> document.write("</DIV>");</script>

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
                       </td>
                     </tr>
                     </asp:Panel>
                     <tr>
                        <td>
                             <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                         <tr style="cursor: hand">
                                        <td style="width: 100%;" class="tr_title_center" id="td1">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblImportArea" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tr_title_center" style="width: 300px;">
                                            
                                        </td>
                                        <td style="width: 22px;" id="td2">
                                            <img id="Img3" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                                        </td>                                        
                                        </tr>
                                        <tr>
                                            <td class="td_label" width="100%" align="left" colspan="2">
                                                <a href="../ExcelModel/OtmAdvanceApplySampleLHZB.xls">&nbsp;點擊下載EXCEL模版
                                                </a>&nbsp;<asp:Label ID="labelUploadText" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                                <asp:Button ID="ButtonImportSave" runat="server" Text="ImportSave" OnClick="ButtonImportSave_Click" CssClass="button_1"
                                                  OnClientClick="javascript:UpProgress();" />
                                                <img id="imgWaiting" src="../CSS/Images/clocks.gif" border="0"
                                                    style="display: none; height: 20px;" />
                                                <asp:Label ID="labelupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="labeluploadMsg" runat="server" ForeColor="red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="height: 25;">
                                                &nbsp;說明：上傳文件格式為Excel，上傳完成後，錯誤的記錄會顯示出來，可點擊導出下載修改後再導入!
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">

                                                <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*52/100+"'>");</script>

                                                <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                        RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                        BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                        Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect">
                                                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
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
                                                        <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="true" Width="280">
                                                                    <Header Caption="<%$Resources:ControlText,ErrorMsg%>">
                                                                    </Header>
                                                                    <CellStyle ForeColor="red">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="true" Width="80">
                                                                    <Header Caption="<%$Resources:ControlText,WorkNo%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="true" Width="60">
                                                                    <Header Caption="<%$Resources:ControlText,gvgvLOCALNAME%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="true" Width="80">
                                                                    <Header Caption="<%$Resources:ControlText,gvOTDate%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="true"
                                                                    Width="80">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadBeginTime%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="true" Width="60">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadEndTime%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="true" Width="200">
                                                                    <Header Caption="<%$Resources:ControlText,labelWorkDesc%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

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

    <script language="javascript">document.write("<div id='divEdit'style='display:none;height:"+document.body.clientHeight*84/100+"'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server"
        OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
    </igtblexp:UltraWebGridExcelExporter>

   
    </form>
</body>
</html>
