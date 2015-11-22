<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMRealApplyForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMRealApplyForm" %>

<%--<%@ Register Src="../../ControlLib/UserLabel.ascx" TagName="UserLabel" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics2.WebUI.UltraWebTab.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%--<%@ Register Src="../../ControlLib/title.ascx" TagName="title" TagPrefix="ControlLib" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMRealApplyForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">
        
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
			document.getElementById("ButtonImport").disabled="disabled";
			document.getElementById("ButtonExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="";
			document.getElementById("labelupload").innerText = "22";			
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
				igtbl_getElementById("DefaultEmployeeNo").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				igtbl_getElementById("HidID").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
				igtbl_getElementById("HiddenBillNo").value=row.getCellFromKey("BillNo").getValue()==null?"":row.getCellFromKey("BillNo").getValue();
			}
		}
				
		function OpenAdvanceImprot()//彈出預報轉入
		{		    
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;            
	        window.open("OTMRealImpFrAdv.aspx?ModuleCode="+ModuleCode,'OTMRealImpFrAdv', 'height=700, width=1000, top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
            return false;
		}
		function OpenKQImport()//彈出依考勤實報
		{		    
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;            
	        var width;
            var height;
            width=screen.width*0.75;
            height=screen.height*0.7;
            openEditWin("OTMRealImpFrKaoQin.aspx?ModuleCode="+ModuleCode,"OTMRealImpFrAdv",width,height);	 
            return false;
		}
		function CheckAudit()
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
                                break;
                             default:
                                alert("提示1");
                                return false;
                                break;
	                    }
		        }
	        }			
	        if(Count==0)
	        {
	            alert("提示2");
	            return false;
	        }
	        if(confirm("提示3"))
	        {
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
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
		             State=gRows.getRow(i).getCell(5).getValue();
		             switch (State)
                        {
                            case "0":
                            case "3":
                                break;
                             default:
                                alert("提示4");
                                return false;
                                break;
	                    }
		        }
	        }			
	        if(Count==0)
	        {
	            alert("提示5");
	            return false;
	        }
	        if(confirm("提示6"))
	        {
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
		function CheckCancelAudit()
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
                                alert("提示7");
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
	        if(confirm(Message.CancelAudit))
	        {
	
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          function CheckSendAudit()
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
                                break;
                             default:
                                alert("提示10");
                                return false;
                                break;
	                    }	 
		        }
	        }			
	        if(Count==0)
	        {
	            alert("提示11");
	            return false;
	        }
	        if(confirm("提示12"))
	        { 
		        document.getElementById("ButtonSendAudit").style.display="none";	
		        document.getElementById("imgSendAuditWaiting").style.display="";
		        document.getElementById("labeSendAudit").innerText = "提示13";
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          function CheckCancel()
          {
	        document.getElementById("ProcessFlag").value="";
	        return true;
          }
        function OpenAuditStatus()//查看簽核進度
		{
		    var grid = igtbl_getGridById('UltraWebGrid');
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
		        alert("提示14");
		        return false;
		    }			    
		    var ModuleCode="23";
		    var BillNo=igtbl_getElementById("HiddenBillNo").value;
	        var width=550;
            var height=150;
            openEditWin("../../PCM/PCMAuditStatusForm.aspx?ModuleCode="+ModuleCode+"&BillNo="+BillNo,"AuditStatus",width,height);
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
		        alert("提示15");
		        return false;
		    }		    
            var windowWidth=500,windowHeight=600;
	        var X=(screen.availWidth-windowWidth)/2;
	        var Y=(screen.availHeight-windowHeight)/2;
	        var Revalue=window.showModalDialog("Sys/TreeDataPickForm.aspx?DataType=Department&condition=&modulecode="+
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
	
	  $(function(){
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
       $(function(){
        $("#img_grid,#td_show_1,#td_show_2").toggle(
            function(){
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
                $("#div_grid").hide();
                
            },
            function(){
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); 
                $("#div_grid").show();
            }
        )
         
   }); 
   
         function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="DepCode")
           {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;
       }
       
     function delete_click()
      {
    var grid = igtbl_getGridById('UltraWebGrid');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
		        return false;
		    }
	       return confirm(Message.DeleteConfirm);

    }
      
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="DefaultEmployeeNo" type="hidden" name="DefaultEmployeeNo" runat="server">
    <input id="HidID" type="hidden" name="HidID" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr1">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlContent" runat="server">
                                            <div id="div_edit">
                                                <table cellspacing="0" cellpadding="0" width="100%" style="table-layout: fixed">
                                                    <tr>
                                                        <td class="td_label" width="11%">
                                                            <asp:Label ID="lblDept" runat="server" Text="lblDepCode"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
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
                                                                        <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblBillNo" runat="server" Text="BillNo"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblHours" runat="server" Text="Hours"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
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
                                                                        <asp:TextBox ID="txtHours" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--<td style="width: 90px">
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label">
                                                            <asp:Label ID="lblEmployeeNo" runat="server" Text="BillNo"></asp:Label>
                                                            &nbsp;
                                                            <asp:Image ID="ImageBatchWorkNo" runat="server" OnClick="javascript:ShowBatchWorkNo()"
                                                                ImageUrl="../../CSS/Images_new/search_new.gif"></asp:Image>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
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
                                                                                                    <asp:Label ID="lblQueryBatchWorkNo" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td_label" width="100%">
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
                                                                    left: 0px; width: 225px; height: 100px; z-index: -1;"></iframe>
                                                            </div>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblLocalName" runat="server" Text="lblName"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblOTDateForm" runat="server" Text="lblOTDateForm"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="txtOTDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                    </td>
                                                                    <td width="8%">
                                                                        ~
                                                                    </td>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="txtOTDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" width="11%">
                                                            <asp:Label ID="lblPersonType" runat="server" Text="lblPersonType"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblOTType" runat="server" Text="lblOTType"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:DropDownList ID="ddlOTType" runat="server" Width="100%">
                                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="G1">G1</asp:ListItem>
                                                                <asp:ListItem Value="G2">G2</asp:ListItem>
                                                                <asp:ListItem Value="G3">G3</asp:ListItem>
                                                                <asp:ListItem Value="G4">G4</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblOTStatus" runat="server" Text="lblOTStatus"></asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label">
                                                            <asp:Label ID="lblIsProject" runat="server" Text="lblIsProject"></asp:Label>
                                                        </td>
                                                        <td class="td_input">
                                                            <asp:DropDownList ID="ddlIsProject" runat="server" Width="100%">
                                                                <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Value="Y">Y</asp:ListItem>
                                                                <asp:ListItem Value="N">N</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" colspan="8">
                                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                                                <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                    CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                                    CssClass="button_2" OnClick="btnReset_Click"></asp:Button>
                                                                <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                    CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                                <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                    CssClass="button_2" OnClientClick="return delete_click()" OnClick="btnDelete_Click">
                                                                </asp:Button>
                                                                <asp:Button ID="btnAdvanceImport" runat="server" Text="<%$Resources:ControlText,btnAdvanceImport %>"
                                                                    CssClass="button_morelarge" OnClientClick="return OpenAdvanceImprot()"></asp:Button>
                                                                <asp:Button ID="btnCancelAudit" runat="server" Text="<%$Resources:ControlText,btnCancelAudit %>"
                                                                    CssClass="button_morelarge" OnClientClick="return CheckCancelAudit()" OnClick="btnCancelAudit_Click">
                                                                </asp:Button>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr_edit">
                                                    <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                            background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblGrid" runat="server"></asp:Label>
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
                                                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                                            </ess:AspNetPager>
                                                        </div>
                                                    </td>
                                                    <td style="width: 22px;" id="td_show_2">
                                                        <img id="img_grid" class="img2" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="div_grid">
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

                                                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%">
                                                                <%--OnDataBound="UltraWebGrid_DataBound"
                                                    OnUpdateRowBatch="UltraWebGrid_UpdateRowBatch"--%>
                                                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                        CssClass="tr_header">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                        </BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" Height="100%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                                        DataKeyField="" BaseTableName="OTM_RealApply" Key="OTM_RealApply">
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
                                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="150"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                                Width="60" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName%>" Fixed="True">
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
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOTDate%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="40"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOTType%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="50"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWeek%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                                Width="40" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOverTimeType%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                                                Width="170" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="AdvanceTime" Key="AdvanceTime" IsBound="false"
                                                                                Width="80" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadAdvanceTime%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="AdvanceHours" Key="AdvanceHours" IsBound="false"
                                                                                Width="40" Format="0.0" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadAdvanceHours%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeSpan" Key="OverTimeSpan" IsBound="false"
                                                                                Width="80" Format="0.0" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadOverTimeSpan%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RealHours" Key="RealHours" IsBound="false"
                                                                                Width="40" Format="0.0" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadRealHours%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ConfirmHours" Key="ConfirmHours" IsBound="false"
                                                                                Width="60" Format="0.0" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadConfirmHours%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ConfirmRemark" Key="ConfirmRemark" IsBound="false"
                                                                                Width="150" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadConfirmRemark%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="180"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkDesc%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false"
                                                                                Width="60">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadIsProject%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ISPay" Key="ISPay" IsBound="false" Width="130">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadISPay%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                                Width="60" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadStatusName%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="false" Width="50"
                                                                                Format="0.0">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Total%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="false" Width="50"
                                                                                Format="0.0">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Total%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="false" Width="50"
                                                                                Format="0.0">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Total%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadRemark%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ApproverName" Key="ApproverName" IsBound="false"
                                                                                Width="60">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadApproverName%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                                Width="110">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadApproveDate%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadApRemark%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                                                Width="70">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadupdateuser%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="update_date" Key="update_date" IsBound="false"
                                                                                Width="110">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadupdatedate%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="true"
                                                                                Width="0" Hidden="true">
                                                                                <Header Caption="OTMSGFlag">
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
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                </table>
            </td>
        </tr>
    </table>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    </form>

    <script type="text/javascript">
    //<!--  
        function ShowDetail(WorkNo,KQDate,ShiftNo)
		{
		    document.all("PanelKQ").style.display="";
            document.all("PanelKQ").style.top=document.all("ButtonReset").style.top;
            
            KQM_OTM_OTMRealApplyForm.ShowDetail(WorkNo,KQDate,ShiftNo,docallback)
		}		
        function docallback(response)
        {
            document.getElementById('divKQ').innerText=""; 
            if (response.value != null)
                {       
　　　　            var ds = response.value;
                    if(ds != null && typeof(ds) == "object" && ds.Tables != null)
	                {
	                    var tblHtml= new   Array();
　　　　                tblHtml[tblHtml.length] =   "<table cellspacing=0 cellpadding=0 width=100%>";
　　　　                tblHtml[tblHtml.length] = "<tr>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label align=center height=25 width=20>No</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=60>"+"提示16"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=70>"+"17"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+"18"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=90>"+"19"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+"20"+"</td>";
　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25 width=80>"+"21"+"</td>";
　　　　                tblHtml[tblHtml.length]= '</tr>';
                        for(var i=1; i<=ds.Tables[0].Rows.length; i++)
　　　　                {
　　　　                    var WorkNo=ds.Tables[0].Rows[i-1].WORKNO;
　　　　　　                var LocalName=ds.Tables[0].Rows[i-1].LOCALNAME;
　　　　　　                var CardTime=ds.Tables[0].Rows[i-1].CARDTIME;
　　　　　　                var BellNo=ds.Tables[0].Rows[i-1].BELLNO;
　　　　　　                var ReadTime=ds.Tables[0].Rows[i-1].READTIME==null?"&nbsp;":ds.Tables[0].Rows[i-1].READTIME;
　　　　　　                var CardNo=ds.Tables[0].Rows[i-1].CARDNO;
　　　　　　                tblHtml[tblHtml.length]= "<tr>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label align=center height=25>"+i+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+WorkNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+LocalName+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+CardTime+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+BellNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+ReadTime+"</td>";
　　　　　　                tblHtml[tblHtml.length]="<td class=td_label height=25>"+CardNo+"</td>";
　　　　　　                tblHtml[tblHtml.length]="</tr>";
　　　　                }
　　　　                tblHtml[tblHtml.length] ="</table>";
　　　　                document.getElementById("divKQ").innerHTML = tblHtml.join("");
                    }
                }
                else
                {
                    document.all("PanelKQ").style.display="none";
                }             
                return;
        }
		function HiddenShowDetail()
		{
		    document.all("PanelKQ").style.display="none";
		    return false;
		}
	//-->
    </script>

</body>
</html>
