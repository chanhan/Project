<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpOtherMoveForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.EmployeeData.HrmEmpOtherMoveForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EmployeeOtherMoveForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script>
   function CheckBatchConfirm()
    {
     if(confirm(Message.ConfirmBatchConfirm))
	        {
		        document.getElementById("btnBatchConfirm").style.display="none";	
		        document.getElementById("imgBatchWaiting").style.display="";
		        document.getElementById("lblBatchConfirm").innerText = "";
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
    }
    function CheckUnConfirm()//取消確認
     {
     var grid = igtbl_getGridById('UltraWebGridEmpMove');
		    var gRows = grid.Rows;
		    var Count=0;
		    var State="";
		    var MoveType="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridEmpMove_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         State=gRows.getRow(i).getCell(17).getValue();
			         MoveType=gRows.getRow(i).getCell(19).getValue();
			         switch (State)
                        {
                            case "0":
	                            alert(Message.UnConfirmOnTime);
	                            return false;
                                break;
                             default:
                                switch (igtbl_getElementById("HiddenLevelCode").value)
                                    {
                                        case "0":
                                        break;
                                        default:
                                        break;
                                    }
                             break;
		                }	 
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
		        return false;
		    }
	    return confirm(Message.ConfirmUnConfirmData);
     }
  function  checkDelete()//刪除
    {
    var grid = igtbl_getGridById('UltraWebGridEmpMove');
		    var gRows = grid.Rows;
		    var Count=0;
		    var State="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridEmpMove_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         State=gRows.getRow(i).getCell(17).getValue();
                       if(State=='1')
                       {
	                     alert(Message.DeleteConfirmed);
                        return false;                 
	                   }
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
		        return false;
		    }
	       return confirm(Message.ConfirmConfirmData);

    }
    		function CheckConfirm()//確認
		{
		    var grid = igtbl_getGridById('UltraWebGridEmpMove');
		    var gRows = grid.Rows;
		    var Count=0;
		    var State="";
		    var MoveType="";
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridEmpMove_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			         State=gRows.getRow(i).getCell(17).getValue();
			         MoveType=gRows.getRow(i).getCell(19).getValue();
			         switch (State)
                        {
                            case "1":
	                            alert(Message.ConfirmOnTime);
	                            return false;
                                break;
                             default:
                                switch (igtbl_getElementById("HiddenLevelCode").value)
                                    {
                                        case "0":
                                        break;
                                        default:
//                                            switch (MoveType)
//                                            {
//                                                case "T01":
//                                                case "T02":
//                                                    break;
//                                                default:
//                                                    alert("hrm.empothermove.checkconfirm");
//                                                    return false;
//                                                    break;
//                                            }
                                        break;
                                    }
                             break;
		                }	 
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
		        return false;
		    }
	        if(confirm(Message.ConfirmConfirmData))
	        {
		     
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
		}
            function UpProgress()
		{
		//	document.getElementById("btnImportSave").style.display="none";
			document.getElementById("btnImport").disabled="disabled";
			document.getElementById("btnExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="block";
			//document.getElementById("lbllupload").innerText = "this.GetResouseValue";			
		}
        		        function onclickBeforeValue(strMoveTypeCode)
		{		    
			if(strMoveTypeCode=="1")
			{			    
			    document.all("ImageBeforeValue").setAttribute("onclick",onClickBeforeValue("OverTimeType"));
			}else
            if(strMoveTypeCode=="2")
			{			   
			    document.all("ImageBeforeValue").setAttribute("onclick",onClickBeforeValue("PersonType"));
			}else
			if(strMoveTypeCode=="3")
			{			   
			    document.all("ImageBeforeValue").setAttribute("onclick",onClickBeforeValue("Post"));
			}
		    else
			    {
			    alert(Message.ChooseOtherMoveTypeFirst);
			    }	
		} 
				function onClickBeforeValue(sType)
		{			
			GetDataValueApp('txtBeforeValueName',sType,'HiddenBeforeValue');
		}   
    		        function onclickAfterValue(strMoveTypeCode)
		{		    
			if(strMoveTypeCode=="1")
			{			    
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("OverTimeType"));
			}else
            if(strMoveTypeCode=="2")
			{			   
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("PersonType"));
			}else
			if(strMoveTypeCode=="3")
			{			   
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("Post"));
			}
		     else
			    {
			    alert(Message.ChooseOtherMoveTypeFirst);
			    }	
		} 
				function onClickAfterValue(sType)
		{			
			GetDataValueApp('txtAfterValueName',sType,'HiddenAfterValue');
		}   
		function GetDataValueApp(ReturnValueBoxName,DataType,ReturnDescBoxName)
{
	var windowWidth=500,windowHeight=380;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("SingleDataPickForm.aspx?DataType="+DataType+"&r="+ Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	if(Revalue!=undefined)
	{
		var arrValue=Revalue.split(";");
			document.all(ReturnValueBoxName).value=arrValue[0];
			if(arrValue.length>1)
			{
			    document.all(ReturnDescBoxName).innerText=arrValue[1];
			}
	}
}

    function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
{
    var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("../../KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r="+ Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	if(Revalue!=undefined)
	{	
	 	    document.all(ReturnValueBoxName).value=Revalue.codeList;
			if(Revalue.codeList.length>1)
			{
			    document.all(ReturnDescBoxName).innerText=Revalue.nameList;
			}
	}
}

		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);			
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridEmpAccident_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(row != null)
			{	
				document.getElementById("HiddenWorkNo").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
			    document.getElementById("HiddenState").value=row.getCell(17).getValue()==null?"":row.getCell(17).getValue();
			    document.getElementById("HiddenMoveOrder").value=row.getCell(18).getValue()==null?"":row.getCell(18).getValue(); 
			}			
		}	
    		function OpenEdit(ProcessFlag)//彈出新增或修改頁面
		{		
		    var EmployeeNo = igtbl_getElementById("HiddenWorkNo").value;
		    var MoveOrder= igtbl_getElementById("HiddenMoveOrder").value;
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {
		        var grid = igtbl_getGridById('UltraWebGridEmpMove');
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
			        alert( Message.AtLastOneChoose);
			        return false;
			    }
            }
            document.getElementById("iframeEdit").src="HrmEmpOtherMoveEditForm.aspx?EmployeeNo="+EmployeeNo+"&MoveOrder="+MoveOrder+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
            document.getElementById("divTop").style.display="none";
            document.getElementById("divEdit").style.display="";
            document.getElementById("dataPanel").style.display="none";
              document.getElementById("div_select2").style.display="none";
            
            return false;
		}
            function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridEmpMove_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridEmpMove');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridEmpMove_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridEmpMove_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}    
$(function()
{
		            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid,#img_div").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
                          $("#img_grid2,#img_div2").toggle(
                function(){
                    $("#tr_show2").hide();
                    $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show2").show();
                    $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
});


    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <input id="HiddenState" type="hidden" name="HiddenState" runat="server">
    <input id="HiddenMoveOrder" type="hidden" name="HiddenMoveOrder" runat="server">
    <input id="HiddenBeforeValue" type="hidden" name="HiddenBeforeValue" runat="server">
    <input id="HiddenAfterValue" type="hidden" name="HiddenAfterValue" runat="server">
    <input id="HiddenLevelCode" type="hidden" name="HiddenLevelCode" runat="server">
    <div id="divTop">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
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
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit" style="width: 100%">
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <asp:Panel ID="inputPanel" runat="server">
                                            <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                            <tr>
                                                <td class="td_label" width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDeptDept" runat="server">Department:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
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
                                                <td>
                                                    <asp:Label ID="lblHistoryMove" runat="server">HistoryMove:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlHistoryMove" Width="100%">
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemCurrentMove %>" Value="Y">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemHistoryMove %>" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label" width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblWorkNo" runat="server">WorkNo:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                </td>
                                                <td class="td_label" width="14%">
                                                    &nbsp;
                                                    <asp:Label ID="lblApplyMan" runat="server">ApplyMan:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtApplyMan" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label" width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblMoveType" runat="server">MoveType:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:DropDownList ID="ddlMoveType" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label" width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblBeforeValue" runat="server">BeforeValue:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtBeforeValueName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td class="td_label" style="cursor: hand">
                                                    <asp:Image ID="ImageBeforeValue" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                    </asp:Image>
                                                </td>
                                                <td class="td_label" width="14%">
                                                    &nbsp;
                                                    <asp:Label ID="lblAfterValue" runat="server">AfterValue:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtAfterValueName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                </td>
                                                <td class="td_label" style="cursor: hand">
                                                    <asp:Image ID="ImageAfterValue" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                    </asp:Image>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label" width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblMoveState" runat="server">State:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lbllblEffectDate" runat="server">EffectDate:</asp:Label>
                                                </td>
                                                <td style="cursor: hand" width="25%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td class="td_label" width="50%">
                                                                <asp:TextBox ID="txtEffectDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                &nbsp;~&nbsp;
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtEffectDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                </td>
                                                <td class="td_label" width="14%">
                                                    &nbsp;
                                                    <asp:Label ID="lblMoveReason" runat="server">MoveReason:</asp:Label>
                                                </td>
                                                <td class="td_input" width="20%">
                                                    <asp:TextBox ID="txtMoveReason" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="td_label" colspan="6">
                                                <table>
                                                    <tr>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                        <td>
                                                            <asp:Button ID="btnQuery" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnQuery%>"
                                                                ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                            <asp:Button ID="btnReset" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnReset%>"
                                                                ToolTip="Authority Code:Query" OnClick="btnReset_Click"></asp:Button>
                                                            <asp:Button ID="btnAdd" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnAdd%>"
                                                                ToolTip="Authority Code:Add" CommandName="Add" OnClientClick="return OpenEdit('Add')">
                                                            </asp:Button>
                                                            <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify%>"
                                                                ToolTip="Authority Code:Modify" CssClass="button_1" OnClientClick="return OpenEdit('Modify')" />
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnDelete%>"
                                                                ToolTip="Authority Code:Delete" OnClick="btnDelete_Click" OnClientClick="return checkDelete();">
                                                            </asp:Button>
                                                            <asp:Button ID="btnImport" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnImport%>"
                                                                ToolTip="Authority Code:Import" OnClick="btnImport_Click"></asp:Button>
                                                            <asp:Button ID="btnExport" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnExport%>"
                                                                ToolTip="Authority Code:Export" OnClick="btnExport_Click"></asp:Button>
                                                            <asp:Button ID="btnConfirm" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnConfirm%>"
                                                                ToolTip="Authority Code:Confirm" OnClientClick="return CheckConfirm()" OnClick="btnConfirm_Click">
                                                            </asp:Button>
                                                            <asp:Button ID="btnUnConfirm" runat="server" CssClass="button_morelarge" Text="<%$Resources:ControlText,btnUnConfirm%>"
                                                                ToolTip="Authority Code:UnConfirm" OnClientClick="return CheckUnConfirm();" OnClick="btnUnConfirm_Click">
                                                            </asp:Button>
                                                            <asp:Button ID="btnBatchConfirm" runat="server" CssClass="button_morelarge" Text="<%$Resources:ControlText,btnBatchConfirm%>"
                                                                ToolTip="Authority Code:Confirm" OnClientClick="return CheckBatchConfirm();"
                                                                OnClick="btnBatchConfirm_Click"></asp:Button>
                                                        </td>
                                                        <td>
                                                            <img id="imgBatchWaiting" src="../../CSS/Images/clocks.gif" border="0" style="display: none;
                                                                height: 20px;" />
                                                            <asp:Label ID="lblBatchConfirm" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </asp:Panel>
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
        <div style="width: 100%" id="dataPanel">
            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;">
                        <td style="width: 100%;" class="tr_title_center" id="img_grid">
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
                                    ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                    Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                    ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                </ess:AspNetPager>
                            </div>
                        </td>
                        <td style="width: 22px;">
                            <div id="img_div">
                                <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="tr_show">
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridEmpMove" runat="server" Width="100%" Height="100%"
                                    OnDataBound="UltraWebGridEmpMove_DataBound">
                                    <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                        AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridEmpMove" TableLayout="Fixed"
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
                                        <igtbl:UltraGridBand BaseTableName="HRM_EmpMove" Key="HRM_EmpMove">
                                            <Columns>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                    <CellTemplate>
                                                        <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                    </CellTemplate>
                                                    <HeaderTemplate>
                                                        <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                    </HeaderTemplate>
                                                    <Header Caption="CheckBox" ClickAction="Select" Fixed="true">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BUName" Key="BUName" IsBound="false" Width="100">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderBUName %>" Fixed="true">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="100">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderDepName %>" Fixed="true">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderWorkNo %>" Fixed="true">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>" Fixed="true">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MoveTypeName" Key="MoveTypeName" IsBound="false"
                                                    Width="120">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderMoveTypeName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BeforeValueName" Key="BeforeValueName" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderBeforeValueName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="AfterValueName" Key="AfterValueName" IsBound="false"
                                                    Width="150">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderAfterValueName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="EFFECTDATEStr" Key="EFFECTDATEStr" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderEffectDate %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="JoinDate" Key="JoinDate" IsBound="false" Hidden="true">
                                                    <Header Caption="">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MoveReason" Key="MoveReason" IsBound="false"
                                                    Width="120">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderMoveReason %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="100">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApplyMan" Key="ApplyMan" IsBound="false" Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderApplyMan %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ApplyDateStr" Key="ApplyDateStr" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderApplyDate %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ConfirmMan" Key="ConfirmMan" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderConfirmMan %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ConfirmDateStr" Key="ConfirmDateStr" IsBound="false"
                                                    Width="80">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderConfirmDate %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="StateName" Key="StateName" IsBound="false"
                                                    Width="60">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderStateName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="State" Key="State" IsBound="false" Hidden="true">
                                                    <Header Caption="State">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MoveOrder" Key="MoveOrder" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="MoveOrder">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="MoveTypeCode" Key="MoveTypeCode" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="MoveTypeCode">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="beforevalue" Key="beforevalue" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="aftervalue" Key="aftervalue" IsBound="false"
                                                    Hidden="true">
                                                    <Header Caption="">
                                                    </Header>
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
            <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="Tr1">
                        <td style="width: 17px;">
                            <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                        </td>
                        <td style="width: 100%;" class="tr_title_center" id="img_grid2">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmport" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div id="img_div2">
                                <img id="div_img_3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                        </td>
                    </tr>
                </table>
                <div id="tr_show2" style="width: 100%">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="td_label" align="left" colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                            <a href="../../ExcelModel/EmpOtherMoveSample.xls">
                                                <%=Resources.ControlText.InfoGetExcelModel%>
                                            </a>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnImportSave" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnImportSave%>"
                                                OnClick="btnImportSave_Click" OnClientClick="javascript:UpProgress();" />
                                        </td>
                                        <td>
                                            <img id="imgWaiting" src="../../CSS/Images/clocks.gif" border="0" style="display: none;
                                                height: 20px;" />
                                            <asp:Label ID="lbllblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                            <%--<asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>--%>
                                        </td>
                                    </tr>
                                </table>
                                <%--     <asp:Label ID="lblUploadTip" runat="server" Font-Bold="true"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="height: 25;">
                                <asp:Label ID="lbluploadInfo2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div>

                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*49/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridEmpMove" TableLayout="Fixed"
                                AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                    CssClass="tr_header">
                                    <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                    </BorderDetails>
                                </HeaderStyleDefault>
                                <FrameStyle Width="100%" Height="100%">
                                </FrameStyle>
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
                                <igtbl:UltraGridBand BaseTableName="HRM_Import" Key="HRM_Import">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="40%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderErrorMsg %>">
                                            </Header>
                                            <CellStyle ForeColor="red">
                                            </CellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderWorkNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="MoveTypeName" Key="MoveTypeName" IsBound="false"
                                            Hidden="true">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderMoveTypeName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AfterValueName" Key="AfterValueName" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderAfterValueName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EffectDate" Key="EffectDate" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEffectDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="JoinDate" Key="JoinDate" IsBound="false" Hidden="true">
                                            <Header Caption="JoinDate">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="MoveReason" Key="MoveReason" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderMoveReason %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="MoveOrder" Key="MoveOrder" IsBound="false"
                                            Hidden="true">
                                            <Header Caption="MoveOrder">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div id="divEdit">
        <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
            <tr>
                <td>
                    <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                        scrolling="no" style="border: 0"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
