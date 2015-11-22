<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveApplyForm_ZBLH.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData.KQMLeaveApplyForm_ZBLH" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script>
    $(function(){

               $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid,#img_div2").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
    })
    
            function OpenUploadFile()//上傳附件
		{
		    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
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
		        alert(Message.AtLastOneChoose);
		        return false;
		    }			    
		    var ModuleCode= igtbl_getElementById('hidModuleCode').value;
		    var BillNo=igtbl_getElementById("hidBillNo").value;
	        var width=550;
            var height=150;
               var X = (screen.availWidth - width) / 2;
            var Y = (screen.availHeight - height) / 2;
              window.showModalDialog("KQMLeaveUploadFileForm.aspx?ModuleCode="+ModuleCode+"&ID="+BillNo, window, "dialogWidth=" + width + "px;dialogHeight=" + height + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
           // openEditWin("KQMLeaveUploadFileForm.aspx?ModuleCode="+ModuleCode+"&ID="+BillNo,"UploadFile",width,height);
            return false;
		}
         function down(f) { $("#frameDown").attr("src", "KQMLeaveApplyForm_ZBLH.aspx?fileName=" + encodeURI(f)); }
    function openEditWin(strUrl,strName,winWidth,winHeight)
{
    var newWindow;
    newWindow=window.open(strUrl,strName,'width='+winWidth+', height='+winHeight+',left='+(screen.width-winWidth)/2.2+', top='+(screen.height-winHeight)/2.2+',resizable=yes, help=no, menubar=no,scrollbars=yes,status=yes,toolbar=no'); 
 //   newWindow.oponer=window;
	newWindow.focus(); 
}
 
		function CheckUnConfirm()//取消銷假   已結案變成已核准 4——》2
		{
		    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
		    var gRows = grid.Rows;
		    var Count=0;
		    var Status="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         Status=gRows.getRow(i).getCell(3).getValue();
			         switch (Status)
                        {
                            case "4":
                                break;
                             default:
	                            alert(Message.NoCloseBillNoCancel);
	                            return false;
                                break;
		                }	 
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.SelectCancelBill);
		        return false;
		    }
	        if(confirm(Message.ConfirmCancle))
	        {
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
		}
		        function OpenTestify()//証明
		{
		    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
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
		          alert(Message.AtLastOneChoose);
		        return false;
		    }			    
		    var ModuleCode= igtbl_getElementById('hidModuleCode').value;
		    var BillNo=igtbl_getElementById("hidBillNo").value;
	        var width=550;
            var height=150;
        //    openEditWin("KQMLeaveTestifyForm.aspx?ModuleCode="+ModuleCode+"&ID="+BillNo,"LeaveTestify",width,height);
            
            var X = (screen.availWidth - width) / 2;
            var Y = (screen.availHeight - height) / 2;
              window.showModalDialog("KQMLeaveTestifyForm.aspx?ModuleCode="+ModuleCode+"&ID="+BillNo, window, "dialogWidth=" + width + "px;dialogHeight=" + height + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
		}
		
		function CheckConfirm()//銷假  已核准變成已結案 2——》4
		{
		    var ModuleCode = igtbl_getElementById("hidModuleCode").value;
		    var BillNo=igtbl_getElementById("hidBillNo").value;
		    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(gRows.getRow(i).getSelected())
			    {
			        Count+=1;	
		            var  Status=gRows.getRow(i).getCell(3).getValue();
		            switch (Status)
                    {
                        case "2":
                            break;
                         default:
                            alert(Message.NoApprovalNoCanel);
                            return false;
                            break;
	                }	 			    
			    }
			 
		    }			
	        if(Count==0)
	        {
	            alert(Message.NoSelect);
	            return false;
	        }
            var width=screen.width*0.6;
            var height=screen.height*0.4;
             var privileged=igtbl_getElementById("hidPrivileged").value;
            openEditWin("KQMLeaveApplyConfirmForm.aspx?BillNo="+BillNo+"&ModuleCode="+ModuleCode+"&Privileged="+privileged,"LeaveApplyConfirm",width,height);	
            return false; 
		}
        function ShowAudit() //核准
    {
	    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
	    var gRows = grid.Rows;
	    var Count=0;
	    var Status="";
	    var ProxyStatus="";
	    for(i=0;i<gRows.length;i++)
	    {
		    if(igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").checked)
		    {
		         Count+=1;
			     Status=gRows.getRow(i).getCellFromKey("Status").getValue();
			     ProxyStatus=gRows.getRow(i).getCellFromKey("ProxyStatus").getValue()==null?"":gRows.getRow(i).getCellFromKey("ProxyStatus").getValue();
		         switch (Status)
                    {
                        case "0":
                            if(ProxyStatus=="1")
                            {
                                alert(Message.UnAudit);
                                return false;
                            }
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
	        alert(Message.SelectBill);
	        return false;
	    }
	    
        document.getElementById("PanelAudit").style.display="";
        document.getElementById("ProcessFlag").value="Audit";
        document.getElementById("PanelAudit").style.top=igtbl_getElementById("btnApproved").style.top;
        var date = new Date();
var seperator = "/";
var month = date.getMonth()+1;
var strDate = date.getDate();
if(month>=1 && month<=9)
{
month = "0"+month;
}
if(strDate>=0&& strDate<=9)
{
strDate = "0"+ strDate;
}
var currentdate =date.getYear()+seperator+month+seperator+strDate;
        document.getElementById("txtApproveDate").value=currentdate;    
        document.getElementById("txtApprover").value="";
        return false;
    }
    
    		function CheckCancelAudit()//取消核準
		{
		    var grid = igtbl_getGridById('UltraWebGridLeaveApply');
		    var gRows = grid.Rows;
		    var Count=0;
		    var Status="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         Status=gRows.getRow(i).getCell(3).getValue();
			         switch (Status)
                        {
                            case "2":
                                break;
                             default:
	                            alert(Message.NoApprovalBillNoVerify);
	                            return false;
                                break;
		                }	 
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.NoSelect);
		        return false;
		    }
	        if(confirm(Message.ConfirmCancle))
	        {
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
		}
  function  ImportClick()
    {
           var moduleCode = igtbl_getElementById("hidModuleCode").value;
            document.getElementById("iframeEdit").src="KQMLeaveApplyImportForm.aspx?ModuleCode="+moduleCode;
            document.getElementById("topTable").style.display="none";
            document.getElementById("tr_show").style.display="none";            
            document.getElementById("div_select2").style.display="none";            
            document.getElementById("divEdit").style.display="";
            return false;
    }
    		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridLeaveApply_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		
				function DisplayRowData(row)
		{
			if(row != null)
			{	
				igtbl_getElementById("hidWorkNo").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
			    igtbl_getElementById("hidBillNo").value=row.getCellFromKey("ID").getValue()==null?"":row.getCellFromKey("ID").getValue();
				igtbl_getElementById("hidAuditBillNo").value=row.getCellFromKey("BillNo").getValue();
			    var lvtypecode=row.getCellFromKey("LVTypeCode").getValue()==null?"":row.getCellFromKey("LVTypeCode").getValue();
			    igtbl_getElementById("hidLVTypeCode").value=lvtypecode;
			    var ISTestify=row.getCellFromKey("ISTestify").getValue()==null?"":row.getCellFromKey("ISTestify").getValue();
			    var Status=row.getCellFromKey("Status").getValue()==null?"":row.getCellFromKey("Status").getValue();
			    if(ISTestify=="Y")
			    {
			        document.getElementById("btnPayProve").disabled=false;
			    }
			    else
			    {			        
			      document.getElementById("btnPayProve").disabled=true;
			    }
			    if(Status=="0"||Status=="1"||Status=="2")
			    {
			     document.getElementById("btnAttachmentManage").disabled=false;
			    }
			    else
			    {			        
			      document.getElementById("btnAttachmentManage").disabled=true;
			    }
			}
		}
		
            function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridLeaveApply_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridLeaveApply');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
       	function OpenEdit(ProcessFlag)//彈出新增或修改頁面
		{		    
		    var ModuleCode = igtbl_getElementById("hidModuleCode").value;
		    var BillNo=igtbl_getElementById("hidBillNo").value;
		    var privileged=igtbl_getElementById("hidPrivileged").value;
		    
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {
		        var grid = igtbl_getGridById('UltraWebGridLeaveApply');
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
			       alert(Message.AtLastOneChoose);
			        return false;
			    }
			      for(i=0;i<gRows.length;i++)
			    {
			    	 if(gRows.getRow(i).getSelected())
				      {
				         if(gRows.getRow(i).getCellFromKey("Status").getValue()!='0' && gRows.getRow(i).getCellFromKey("Status").getValue()!='3')
				       {
				         alert(Message.NoApprovalOrRefuseNoUpdate);    
				         return false;		
				      }
				      }
				    
			    }	
			    
            }
            document.getElementById("iframeEdit").src="KQMLeaveApplyEditForm.aspx?BillNo="+BillNo+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode+"&privileged="+privileged;
            document.getElementById("topTable").style.display="none";
            document.getElementById("tr_show").style.display="none";            
            document.getElementById("div_select2").style.display="none";            
            
            
            document.getElementById("divEdit").style.display="";
            return false;
		}		
		
    
    function openProgress(billNo)
    {
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
              window.showModalDialog("../../../WorkFlow/SignLogAndMap.aspx?Doc=" +
	          billNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
    }
    function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
{
    var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("../../../KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r="+ Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	if(Revalue!=undefined)
	{	
			document.all(ReturnValueBoxName).value=Revalue.codeList;
			if(Revalue.codeList.length>1)
			{
			    document.all(ReturnDescBoxName).innerText=Revalue.nameList;
			}
	}
}
function resetClick()
{
    $(':text').each(function(){
       $(this).val(null);
    });
    $("select").each(function(){
    $(this).get(0).selectedIndex=0;
    });
   $('#<%=txtStartDate.ClientID %>').val($('#<%=hidStartDate.ClientID %>').val());
    $('#<%=txtEndDate.ClientID %>').val($('#<%=hidEndDate.ClientID %>').val());
    return false;
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="hidWorkNo" type="hidden" name="hidWorkNo" runat="server">
    <input id="hidModuleCode" type="hidden" name="hidModuleCode" runat="server">
    <input id="hidBillNo" type="hidden" name="hidBillNo" runat="server">
    <input id="hidOrgCode" type="hidden" name="hidOrgCode" runat="server">
    <input id="hidLVTypeCode" type="hidden" name="hidLVTypeCode" runat="server" />
    <input id="hidAuditBillNo" type="hidden" name="hidAuditBillNo" runat="server">
    <input id="hidImportType" type="hidden" name="hidImportType" runat="server">
    <input id="hidPrivileged" type="hidden" name="hidPrivileged" runat="server">
     <input id="hidStartDate" type="hidden" name="hidStartDate" runat="server">
    <input id="hidEndDate" type="hidden" name="hidEndDate" runat="server">
    
    <iframe id="frameDown" style="display: none;"></iframe>
    <div id="topTable">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
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
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit" style="width: 100%">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <asp:Panel ID="inputPanel" runat="server">
                                            <tr>
                                                <td class="td_label" width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDeptcode" runat="server">Department:</asp:Label>
                                                </td>
                                                <td class="td_input" width="15%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label" style="width: 10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblBillNoBillNo" runat="server">BillNo:</asp:Label>
                                                </td>
                                                <td class="td_input" style="width: 25%">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblTestify" runat="server">Testify:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlTestify" runat="server" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Y</asp:ListItem>
                                                        <asp:ListItem>N</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label" width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblWorkNoWorkNo" runat="server">WorkNo:</asp:Label>
                                                </td>
                                                <td class="td_input" width="15%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblName" runat="server">LocalName:</asp:Label>
                                                </td>
                                                <td class="td_input" width="25%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLVLVTypeCode" runat="server">LVTypeCode:</asp:Label>
                                                </td>
                                                <td class="td_input" width="25%">
                                                    <asp:DropDownList ID="ddlLVTypeCode" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblFormStatus" runat="server">Status:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblblStartDate" runat="server">LeaveDate:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblApplyType" runat="server">ApplyType:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlApplyType" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblApplyDate" runat="server">LeaveDate:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtApplyStartDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtApplyEndDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label" colspan="2">
                                                    &nbsp;
                                                    <asp:CheckBox ID="chkFlag" runat="server" />
                                                    <asp:Label ID="lbllblFlag" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblLastYear" runat="server">IsLastYear:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlIsLastYear" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="td_label" colspan="6">
                                                <table>
                                                    <tr>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <td>
                                                                <asp:Button ID="btnQuery" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnQuery %>"
                                                                    ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                                    ToolTip="Authority Code:Reset" CssClass="button_1" OnClientClick="return resetClick();">
                                                                </asp:Button>
                                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                    ToolTip="Authority Code:Add" CssClass="button_1" OnClientClick="return OpenEdit('Add')">
                                                                </asp:Button>
                                                                <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                    ToolTip="Authority Code:Modify" CssClass="button_1" OnClientClick="return OpenEdit('Modify')">
                                                                </asp:Button>
                                                                <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                    ToolTip="Authority Code:Delete" CssClass="button_1" OnClick="btnDelete_Click">
                                                                </asp:Button>
                                                                <%--      <asp:Button ID="btnAdvanceDayOff" runat="server" Text="<%$Resources:ControlText,btnAdvanceDayOff %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:btnAdvanceDayOff"></asp:Button>--%>
                                                                <asp:Button ID="btnImport" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnImport%>"
                                                                    ToolTip="Authority Code:Import" OnClientClick="return ImportClick();"></asp:Button>
                                                                <asp:Button ID="btnExport" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnExport%>"
                                                                    ToolTip="Authority Code:Export" OnClick="btnExport_Click"></asp:Button>
                                                                <asp:Button ID="btnApproved" runat="server" Text="<%$Resources:ControlText,btnApproved %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:Approved" OnClientClick="return ShowAudit();">
                                                                </asp:Button>
                                                                <asp:Button ID="btnCancelApproved" runat="server" Text="<%$Resources:ControlText,btnCancelApproved %>"
                                                                    CssClass="button_morelarge" ToolTip="Authority Code:CancelApproved" OnClientClick="return CheckCancelAudit();"
                                                                    OnClick="btnCancelApproved_Click"></asp:Button>
                                                                <asp:Button ID="btnSendAudit" runat="server" Text="<%$Resources:ControlText, btnSendAudit %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:SendAudit" OnClick="btnOrgAudit_Click">
                                                                </asp:Button>
                                                           <%--     <asp:Button ID="btnOrgAudit" runat="server" Text="<%$Resources:ControlText,btnOrgAudit %>"
                                                                    CssClass="button_morelarge" ToolTip="Authority Code:OrgAudit" OnClick="btnOrgAudit_Click">
                                                                </asp:Button>--%>
                                                                <asp:Button ID="btnCancelLeave" runat="server" Text="<%$Resources:ControlText,btnCancelLeave %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:CancelLeave" OnClientClick="return CheckConfirm();">
                                                                </asp:Button>
                                                                <asp:Button ID="btnCancelCancelLeave" runat="server" Text="<%$Resources:ControlText,btnCancelCancelLeave %>"
                                                                    CssClass="button_morelarge" ToolTip="Authority Code:CancelCancelLeave" OnClientClick="return CheckUnConfirm();"
                                                                    OnClick="btnCancelCancelLeave_Click"></asp:Button>
                                                                <asp:Button ID="btnPayProve" runat="server" Text="<%$Resources:ControlText,btnPayProve %>"
                                                                    CssClass="button_morelarge" ToolTip="Authority Code:PayProve" OnClientClick="return OpenTestify();">
                                                                </asp:Button>
                                                                <asp:Button ID="btnAttachmentManage" runat="server" Text="<%$Resources:ControlText,btnAttachmentManage %>"
                                                                    CssClass="button_morelarge" ToolTip="Authority Code:AttachmentManage" OnClientClick="return OpenUploadFile();">
                                                                </asp:Button>
                                                                <%--  <asp:Button ID="btnViewProgress" runat="server" Text="<%$Resources:ControlText,btnViewProgress %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:ViewProgress"></asp:Button>--%>
                                                                <img id="imgSendAuditWaiting" src="../../../CSS/Images/clocks.gif" border="0" style="display: none;
                                                                    height: 20px;" />
                                                                <asp:Panel ID="PanelAudit" runat="server" Visible="true" Width="300px" Style="padding-right: 3px;
                                                                    padding-left: 3px; z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px;
                                                                    background-color: #ffffee; border-right: #0000ff 1px solid; border-top: #0000ff 1px solid;
                                                                    border-left: #0000ff 1px solid; border-bottom: #0000ff 1px solid; position: absolute;
                                                                    left: 20%; float: left; display: none">
                                                                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                        <tr>
                                                                            <td class="td_label" width="12%">
                                                                                &nbsp;
                                                                                <asp:Label ID="lblApprover" runat="server" />
                                                                            </td>
                                                                            <td class="td_input" width="15%">
                                                                                <asp:TextBox ID="txtApprover" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                            </td>
                                                                            <td class="td_label" width="12%">
                                                                                &nbsp;
                                                                                <asp:Label ID="lblApproveDate" runat="server" />
                                                                            </td>
                                                                            <td class="td_input" width="15%">
                                                                                <asp:TextBox ID="txtApproveDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button_1" ToolTip="Authority Code:Save"
                                                                                    OnClick="btnSave_Click"></asp:Button>
                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_1" ToolTip="Authority Code:Cancel">
                                                                                </asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </asp:Panel>
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
        </div>
        <div style="width: 100%" id="tr_show_1">
            <asp:Panel ID="PanelData" runat="server" Width="100%">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;">
                     
                        <td style="width: 100%;" class="tr_title_center" id="img_grid">
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
                                    ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                    Width="300px" ImagePath="../../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                    ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../../CSS/Images_new/search01.gif"
                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                </ess:AspNetPager>
                            </div>
                        </td>
                        <td style="width: 22px;">
                            <div id="img_div2">
                                <img id="div_img_2" class="img2" width="22px" height="24px" src="../../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="tr_show">
                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                        <tr style="width: 100%">
                            <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridLeaveApply" runat="server" Width="100%" Height="100%"
                                    OnDataBound="UltraWebGridLeaveApply_DataBound">
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
                                        <ClientSideEvents InitializeLayoutHandler="UltraWebGridLeaveApply_InitializeLayoutHandler"
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
                                        <igtbl:UltraGridBand BaseTableName="KQM_LeaveApply" Key="KQM_LeaveApply">
                                            <Columns>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" Width="30px" Key="CheckBoxAll" HeaderText="CheckBox">
                                                    <CellTemplate>
                                                        <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                    </CellTemplate>
                                                    <HeaderTemplate>
                                                        <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                    </HeaderTemplate>
                                                    <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80px">
                                                    <Header Caption="<%$Resources:ControlText,gvWorkNo%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                    Width="80px">
                                                    <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="True">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="True"
                                                    HeaderText="Status">
                                                    <Header Caption="Status">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="dcode" Key="dcode" IsBound="false" Hidden="True"
                                                    HeaderText="dcode">
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" Key="LVTypeCode" IsBound="false"
                                                    Hidden="True" HeaderText="LVTypeCode">
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="SEXName" Key="SEXName" IsBound="false" Width="40px"
                                                    HeaderText="SEXName">
                                                    <Header Caption="<%$Resources:ControlText,gvSexName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Width="100px"
                                                    HeaderText="buName">
                                                    <Header Caption="<%$Resources:ControlText,gvgvbuName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="100px"
                                                    HeaderText="DEPNAME">
                                                    <Header Caption="<%$Resources:ControlText,gvgvDepName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LVTypeName" Key="LVTypeName" IsBound="false"
                                                    Width="80px" HeaderText="LVTypeName">
                                                    <Header Caption="<%$Resources:ControlText,gvLVTypeName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                    Width="80px" HeaderText="StartDate" Format="yyyy-MM-dd">
                                                    <Header Caption="<%$Resources:ControlText,gvStartDate%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                    Width="60px" HeaderText="StartTime">
                                                    <Header Caption="<%$Resources:ControlText,gvStartTime%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="80px"
                                                    HeaderText="EndDate" Format="yyyy-MM-dd">
                                                    <Header Caption="<%$Resources:ControlText,gvgvEndDate%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="60px"
                                                    HeaderText="EndTime">
                                                    <Header Caption="<%$Resources:ControlText,gvEndTime%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LVTotal" Key="LVTotal" IsBound="false" Width="50px"
                                                    HeaderText="LVTotal">
                                                    <Header Caption="<%$Resources:ControlText,gvgvLVTotal%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ThisLVTotal" HeaderText="ThisLVTotal" IsBound="False"
                                                    Key="ThisLVTotal" Width="50">
                                                    <Header Caption="<%$Resources:ControlText,gvgvThisLVTotal%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LVTotalDays" HeaderText="LVTotalDays" IsBound="False"
                                                    Key="LVTotalDays" Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvgvLVTotalDays%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LVWorkDays" HeaderText="LVWorkDays" IsBound="False"
                                                    Key="LVWorkDays" Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvLVWorkDays%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Reason" Key="Reason" IsBound="false" Width="100px"
                                                    HeaderText="Reason">
                                                    <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="proxyworkno" Key="proxyworkno" IsBound="false"
                                                    Width="70px" HeaderText="ProxyWorkNo">
                                                    <Header Caption="<%$Resources:ControlText,gvProxyWorkNo%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Proxy" Key="Proxy" IsBound="false" Width="70px"
                                                    HeaderText="Proxy">
                                                    <Header Caption="<%$Resources:ControlText,gvProxyName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                  <igtbl:UltraGridColumn BaseColumnName="proxystatusname" Key="proxystatusname" IsBound="false" Width="70px"
                                                    HeaderText="Proxy">
                                                    <Header Caption="<%$Resources:ControlText,gvProxyStatusName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="TestifyFile" Key="TestifyFile" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvTestifyFile%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="UploadFile" Key="UploadFile" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvUploadFile%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApplyTypeName" Key="ApplyTypeName" IsBound="false"
                                                    Width="60px" HeaderText="ApplyTypeName">
                                                    <Header Caption="<%$Resources:ControlText,gvgvApplyTypeName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                    Width="60px" HeaderText="StatusName">
                                                    <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="IsLastYear" Key="IsLastYear" IsBound="false"
                                                    Width="50px" HeaderText="IsLastYear">
                                                    <Header Caption="<%$Resources:ControlText,gvIsLastYear%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150"
                                                    HeaderText="BillNo">
                                                    <Header Caption="<%$Resources:ControlText,gvBillNo%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                    Width="70px" HeaderText="Modifier">
                                                    <Header Caption="<%$Resources:ControlText,gvgvModifier%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="update_datestr" Key="update_datestr" IsBound="false"
                                                    Width="110px" HeaderText="ModifyDate">
                                                    <Header Caption="<%$Resources:ControlText,gvgvModifyDate%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150"
                                                    HeaderText="BillNo">
                                                    <Header Caption="<%$Resources:ControlText,gvProgress%>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LevelCode" Key="LevelCode" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="LevelCode">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ManagerCode" Key="ManagerCode" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="ManagerCode">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ProxyStatus" Key="ProxyStatus" IsBound="false"
                                                    Hidden="true">
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="True"
                                                    HeaderText="ID">
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ISTestify" Key="ISTestify" IsBound="false"
                                                    Hidden="True" HeaderText="ISTestify">
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ProxyNotes" Key="ProxyNotes" IsBound="false"
                                                    Hidden="True" HeaderText="ProxyNotes">
                                                </igtbl:UltraGridColumn>
                                            </Columns>
                                        </igtbl:UltraGridBand>
                                    </Bands>
                                </igtbl:UltraWebGrid>

                                <script language="javascript">document.write("</DIV>");</script>

                            </td>
                            <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                &nbsp;
                            </td>
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
    </div>
    <div>

        <script language="javascript">document.write("<div id='divEdit'style='display:none;height:"+document.body.clientHeight*87/100+"'>");</script>

        <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
            <tr>
                <td>
                    <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                        scrolling="no" style="border: 0"></iframe>
                </td>
            </tr>
        </table>
    </div>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    </form>
</body>
</html>
