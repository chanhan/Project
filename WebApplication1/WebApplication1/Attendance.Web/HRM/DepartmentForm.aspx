<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.DepartmentForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DepartmentForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--
    
    function removeValue()
    {
      $(".input_textBox_noborder_1").attr("value","");
      $(".input_textBox_noborder_2").attr("value","");
      $(".input_textBox_1").attr("value","");
      $(".input_textBox_2").attr("value","");
      $("#<%=hidOperate.ClientID %>").val("");   
    }

    
    function removeReadonly()
    { 
      $(".img_hidden").show();
      $("#<%=txtDepShortName.ClientID %>").removeAttr("readonly");
      $("#<%=txtDepAlias.ClientID %>").removeAttr("readonly");
      $("#<%=txtFactoryCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtAccountEntity.ClientID %>").removeAttr("readonly");
      $("#<%=txtAreaCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtOrderId.ClientID %>").removeAttr("readonly");
      $("#<%=txtLevelCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtCostCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtParentDepCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtCorporationId.ClientID %>").removeAttr("readonly");
      $("#<%=txtDepName.ClientID %>").removeAttr("readonly");
      $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
      $("#<%=btnSave.ClientID %>").removeAttr("disabled");
      $(".input_textBox_1").css("border-style", "solid");
      $(".input_textBox_2").css("border-style", "solid");
      $(".input_textBox_1").css("background-color", "White");
      $(".input_textBox_2").css("background-color", "White");
    }
    
    function addReadonly()
  {
      $("#<%=txtDepName.ClientID %>").attr("readonly","true");
      $("#<%=txtCorporationId.ClientID %>").attr("readonly","true");
      $("#<%=txtCorporationIdName.ClientID %>").attr("readonly","true");
      $("#<%=txtParentDepCode.ClientID %>").attr("readonly","true");
      $("#<%=txtParentDepName.ClientID %>").attr("readonly","true");
      $("#<%=txtLevelCode.ClientID %>").attr("readonly","true");
      $("#<%=txtLevelName.ClientID %>").attr("readonly","true");
      $("#<%=txtCostCode.ClientID %>").attr("readonly","true");
      $("#<%=txtOrderId.ClientID %>").attr("readonly","true");
      $("#<%=txtAreaCode.ClientID %>").attr("readonly","true");
      $("#<%=txtAccountEntity.ClientID %>").attr("readonly","true");
       $("#<%=txtOrderId.ClientID %>").css("border-style", "none");
      $("#<%=txtAreaName.ClientID %>").css("border-style", "none");
      $("#<%=txtDepShortName.ClientID %>").attr("readonly","true");
      $("#<%=txtDepAlias.ClientID %>").attr("readonly","true");
      $("#<%=txtFactoryCode.ClientID %>").attr("readonly","true");
      $(".input_textBox_1").css("border-style", "none");
      $(".input_textBox_2").css("border-style", "none");
      $(".input_textBox_1").css("background-color", "#efefef");
      $("#txtCorporationIdName").css("background-color", "#efefef");
      $(".input_textBox_2").css("background-color", "");
      
  }
  
  function removeDisabled()
  {
      $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
      
      $("#<%=btnCondition.ClientID %>").removeAttr("disabled");
      $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
      $("#<%=btnAdd.ClientID %>").removeAttr("disabled");
      $("#<%=btnModify.ClientID %>").removeAttr("disabled");
      $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
      $("#<%=btnDisable.ClientID %>").removeAttr("disabled");
      $("#<%=btnEnable.ClientID %>").removeAttr("disabled");
      $("#<%=btnSave.ClientID %>").removeAttr("disabled");

  }
  
  function addDisabled()
  {
      $("#<%=btnCondition.ClientID %>").attr("disabled","true");
      $("#<%=btnQuery.ClientID %>").attr("disabled","true");
      $("#<%=btnAdd.ClientID %>").attr("disabled","true");
      $("#<%=btnModify.ClientID %>").attr("disabled","true");
      $("#<%=btnDelete.ClientID %>").attr("disabled","true");
      $("#<%=btnDisable.ClientID %>").attr("disabled","true");
      $("#<%=btnEnable.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");

  }

    
    function condition_click()
    {
     removeValue();
     removeReadonly();
     $("#<%=ProcessFlag.ClientID %>").val("condition");    
       addDisabled();
     $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
     $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
     return false; 
    }
   

    function add_click()
    {
      removeValue();
      removeReadonly();
        addDisabled();
       $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
      $("#<%=ProcessFlag.ClientID %>").val("Add"); 
      $("#<%=txtAccountEntity.ClientID %>").val("N"); 
       return false;                 
    }
    
    function AddAndModify()
    {
     removeReadonly();
        addDisabled();
       $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
    }
    
   function modify_click()
    {
     var DepCode = $.trim($("#<%=txtDepCode.ClientID%>").val());
    if (DepCode =="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
        var levelCode = $.trim($("#<%=txtLevelCode.ClientID%>").val());
        $.ajax({
                type: "post", 
                url: "DepartmentForm.aspx", 
                dataType: "text", 
                data: {LevelCode: levelCode},
                                            success: function(msg) { 
                                                if (msg==1) {alert(Message.NoAuthority);return false;} 
                                                else
                                                {
                                                removeReadonly();
                                                  $("#<%=ProcessFlag.ClientID %>").val("Modify");  
                                                   addDisabled();
                                                   $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
                                                   $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
                                                  
                                                }
                                              }
                                        });
      
      }return false; 
    }
    
    
    
    function save_click()
    {
            var alertText;
            var DepName = $.trim($("#<%=txtDepName.ClientID%>").val());
            var orderID = $.trim($("#<%=txtOrderId.ClientID%>").val());
            var LevelCode = $.trim($("#<%=txtLevelCode.ClientID%>").val());
            var AccountEntity = $.trim($("#<%=txtAccountEntity.ClientID%>").val());  
           if (DepName=="")
            {
                alertText = $.trim($("#<%=lblAttDepName.ClientID%>").text()) +Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            if (LevelCode=="")
            {
                alertText = $.trim($("#<%=lblDeptLevelCode.ClientID%>").text()) +Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            if (orderID=="")
            {
                alertText = $.trim($("#<%=lblOrderID.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return  false;
            }
            else 
            {
                if(!isDigitOnly(orderID ))
                {
                alert(Message.OrderIDNotNumber);
                return false;
                }
            }
            if (AccountEntity=="")
            {
                alertText = $.trim($("#<%=lblAccountEntity.ClientID%>").text()) +Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
     return confirm(Message.SaveConfim);
    }
      

      
    function disable_click()
    {
    var DepCode = $.trim($("#<%=txtDepCode.ClientID%>").val());
        if (DepCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
        return confirm(Message.DisableConfirm);
        }                                 
    }
    
    
    
    function enable_click()
    {
     var DepCode = $.trim($("#<%=txtDepCode.ClientID%>").val());
        if (DepCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
        return confirm(Message.EnableConfirm);
        }                  
    }
    
    function isDigitOnly(num) 
    {
      return /\d+/.test(num);
    }

    function delete_click()
    {
      var DepCode = $.trim($("#<%=txtDepCode.ClientID%>").val());
        if (DepCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
        return confirm(Message.DeleteConfirm);
        }  
    }
    
   function cancel_click()
   {
      removeValue();
      $("#<%=ProcessFlag.ClientID %>").val("");
      $(".input_textBox").css("border-style", "none");
       addReadonly();
       removeDisabled();
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      $(".img_hidden").hide();
      return false;
   }
   
   function Load()
   {
   var hidOperate=$("#<%=hidOperate.ClientID %>").val(); 
   if(hidOperate=="Save")
   {
      AddAndModify()
   }
   else
   {
      $(".input_textBox").css("border-style", "none");
       addReadonly();
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      $(".img_hidden").hide();
   }
     return true;
   }
   
   function setSelector(ctrlCode,ctrlName,flag)
   {
       var modulecode=$("#<%=HiddenModuleCode.ClientID %>").val();
       if (flag=="depcode")
       {
       var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+modulecode;
       }
       else if (flag=="AreaCode")
       {
       var url="AreaSelector.aspx";
       }
       else if (flag=="CorporationCode")
       {
       var url="CorporationSelector.aspx";
       }
       else if (flag=="LevelCode")
       {
       var url="LevelSelector.aspx";
       }
       
       var fe="dialogHeight:400px; dialogWidth:350px; dialogTop:100px; dialogLeft:300px;status:no;scroll:yes;";
       var info=window.showModalDialog(url,null,fe);
       if(info)
       {
           $("#"+ctrlCode).val(info.codeList);
           $("#" + ctrlName).val(info.nameList);
       }
       return false;
   }
   
    $(function(){
    $("#tr_edit").toggle(function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
    $("#div_select").hide()},function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
    $("#div_select").show()});
    $("#div_img_2,#td_show_1,#td_show_2").toggle(function(){
    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif")
    $("#div_showdata").hide()},function(){
    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif")
    $("#div_showdata").show()})
    });
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridDepartment_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
			{
			    document.getElementById("txtDepCode").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();
				igtbl_getElementById("txtDepName").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				igtbl_getElementById("txtCorporationId").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
				igtbl_getElementById("txtCorporationIdName").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
				igtbl_getElementById("txtParentDepCode").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
				igtbl_getElementById("txtParentDepName").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
				igtbl_getElementById("txtLevelCode").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
				igtbl_getElementById("txtLevelName").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();
				igtbl_getElementById("txtCostCode").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
				igtbl_getElementById("txtOrderId").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
				igtbl_getElementById("txtAreaCode").value=row.getCellFromKey("AreaCode").getValue()==null?"":row.getCellFromKey("AreaCode").getValue();
				igtbl_getElementById("txtAreaName").value=row.getCellFromKey("AreaName").getValue()==null?"":row.getCellFromKey("AreaName").getValue();
				igtbl_getElementById("txtAccountEntity").value=row.getCellFromKey("AccountEntity").getValue()==null?"":row.getCellFromKey("AccountEntity").getValue();
				igtbl_getElementById("txtDepShortName").value=row.getCellFromKey("DeptShortName").getValue()==null?"":row.getCellFromKey("DeptShortName").getValue();
				igtbl_getElementById("txtDepAlias").value=row.getCellFromKey("DeptAlias").getValue()==null?"":row.getCellFromKey("DeptAlias").getValue();
				igtbl_getElementById("txtFactoryCode").value=row.getCellFromKey("FactoryCode").getValue()==null?"":row.getCellFromKey("FactoryCode").getValue();
				igtbl_getElementById("HiddenAreaCode").value=row.getCellFromKey("AreaCode").getValue()==null?"":row.getCellFromKey("AreaCode").getValue();
				
			}
		}
		function OpenOrgLayoutA()
		{   
		    var ModuleCode=document.getElementById("HiddenModuleCode").value;
	        var width;
            var height;
            width=screen.width*0.85;
            height=screen.height*0.7;
            openEditWin("DepartmentLayoutForm.aspx?ModuleCode="+ModuleCode,"DepartmentLayout",width,height);	 
            return false;
		}
		function OpenOrgLayoutB()
		{   
		    var ModuleCode=document.getElementById("HiddenModuleCode").value;
	        var width;
            var height;
            width=screen.width*0.85;
            height=screen.height*0.7;
            openEditWin("DepartmentLayoutBForm.aspx?ModuleCode="+ModuleCode,"DepartmentLayout",width,height);	 
            return false;
		}
		
		function openEditWin(strUrl,strName,winWidth,winHeight)
{
    var newWindow;
    newWindow=window.open(strUrl,strName,'width='+winWidth+', height='+winHeight+',left='+(screen.width-winWidth)/2.2+', top='+(screen.height-winHeight)/2.2+',resizable=yes, help=no, menubar=no,scrollbars=yes,status=yes,toolbar=no'); 
    newWindow.oponer=window;
	newWindow.focus(); 
}
	--></script>

</head>
<body onload="return Load()">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server" />
    <input id="HiddenAreaCode" type="hidden" name="HiddenAreaCode" runat="server" />
    <input id="hidOperate" type="hidden" name="hidOperate" runat="server" />
    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" class="input_textBox_1"
        Style="display: none"></asp:TextBox>
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%; cursor: hand;" id="tr_edit">
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
                    <div id="img_edit">
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_select" style="width: 100%">
        <table class="table_data_area" style="width: 100%">
            <tr style="width: 100%">
                <td>
                    <table style="width: 100%">
                        <tr class="tr_data">
                            <td>
                                <asp:Panel ID="pnlContent" runat="server">
                                    <table class="table_data_area">
                                        <tr class="tr_data_1">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblAttDepName" runat="server" ForeColor="Blue">Dep. Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblCorporation" runat="server">Corporation</asp:Label>
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtCorporationId" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand" onclick="setSelector('txtCorporationId','txtCorporationIdName','CorporationCode')">
                                                            <%--<asp:Image ID="ImageCorporation" runat="server" ImageUrl="~/images/zoom.png"></asp:Image>--%>
                                                            <asp:Image ID="imgCorporation" runat="server" class="img_hidden" src="../../CSS/Images_new/search_new.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCorporationIdName" runat="server" class="input_textBox_noborder_2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblParentDepCode" runat="server">Parent Department</asp:Label>
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtParentDepCode" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand" onclick="setSelector('txtParentDepCode','txtParentDepName','depcode')">
                                                            <%--<asp:Image ID="ImageParentdepcode" runat="server" ImageUrl="~/images/zoom.png"></asp:Image>--%>
                                                            <asp:Image ID="imgParentDepCode" runat="server" class="img_hidden" src="../../CSS/Images_new/search_new.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtParentDepName" runat="server" class="input_textBox_noborder_2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblDeptLevelCode" runat="server" ForeColor="Blue">Level Code</asp:Label>
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtLevelCode" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand" onclick="setSelector('txtLevelCode','txtLevelName','LevelCode')">
                                                            <%--<asp:Image ID="ImageLevelcode" runat="server" ImageUrl="~/images/zoom.png"></asp:Image>--%>
                                                            <asp:Image ID="imgLevelcode" runat="server" class="img_hidden" src="../../CSS/Images_new/search_new.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLevelName" runat="server" class="input_textBox_noborder_2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblCostCode" runat="server">Cost Code:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCostCode" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblOrderID" runat="server" ForeColor="Blue">Code Head:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrderId" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblDeptArea" runat="server">Area</asp:Label>
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtAreaCode" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand" onclick="setSelector('txtAreaCode','txtAreaName','AreaCode')">
                                                            <%--<asp:Image ID="ImageArea" runat="server" ImageUrl="~/images/zoom.png"></asp:Image>--%>
                                                            <asp:Image ID="imgArea" runat="server" class="img_hidden" src="../../CSS/Images_new/search_new.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAreaName" runat="server" class="input_textBox_noborder_2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblAccountEntity" runat="server" ForeColor="Blue">Account Entity:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountEntity" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblDepShortName" runat="server">DepShortName:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepShortName" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblDepAlias" runat="server">DepAlias:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDepAlias" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblFactoryCode" runat="server">FactoryCode:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFactoryCode" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td width="20%">
                                                &nbsp;
                                                <asp:Label ID="lblChoiceDeptLevel" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDepLevel" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlShowPanel" runat="server">
                                    <asp:Button ID="btnCondition" runat="server" CssClass="button_1" OnClientClick="return condition_click()">
                                    </asp:Button>
                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClientClick="return add_click()">
                                    </asp:Button>
                                    <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return modify_click()">
                                    </asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClientClick="return delete_click()" OnClick="btnDelete_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button_1" OnClientClick="return cancel_click()">
                                    </asp:Button>
                                    <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClientClick="return save_click()"
                                        OnClick="btnSave_Click"></asp:Button>
                                    <asp:Button ID="btnDisable" runat="server" CssClass="button_1" OnClientClick="return disable_click()"
                                        OnClick="btnDisable_Click"></asp:Button>
                                    <asp:Button ID="btnEnable" runat="server" CssClass="button_1" OnClientClick="return enable_click()"
                                        OnClick="btnEnable_Click"></asp:Button>
                                    <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnViewOrg" runat="server" CssClass="button_morelarge" OnClientClick="return OpenOrgLayoutA()">
                                    </asp:Button>
                                    <asp:Button ID="btnViewOrgB" runat="server" CssClass="button_morelarge" OnClientClick="return OpenOrgLayoutB()">
                                    </asp:Button>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
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
                            HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                            ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px; cursor: hand;" id="td_show_2">
                    <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*67/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridDepartment" runat="server" Width="100%" Height="100%">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridDepartment" TableLayout="Fixed"
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
                                <igtbl:UltraGridBand BaseTableName="gds_att_department_v" Key="gds_att_department_v">
                                    <Columns>
                                        <%--<igtbl:UltraGridColumn BaseColumnName="DEPCODE" Key="DEPCODE" IsBound="false" Hidden="True"
                                            HeaderText="Dep Code">
                                            <Header Caption="Dep Code">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Hidden="True"
                                            HeaderText="Dep Name">
                                            <Header Caption="Dep Name">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME1" Key="DEPNAME1" IsBound="false" Width="35%"
                                            HeaderText="Dep Name">
                                            <Header Caption="<%$Resources:ControlText,gvHeadDepName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CORPORATIONID" Key="CORPORATIONID" IsBound="false"
                                            Hidden="True" HeaderText="Corporation ID">
                                            <Header Caption="Corporation ID">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CORPORATIONNAME" Key="CORPORATIONNAME" IsBound="false"
                                            Width="10%" HeaderText="Corporation Name">
                                            <Header Caption="<%$Resources:ControlText,gvCorporationName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PARENTDEPCODE" Key="PARENTDEPCODE" IsBound="false"
                                            HeaderText="Parent Dep Code" Hidden="true">
                                            <Header Caption="Parent Dep Code">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PARENTDEPNAME" Key="PARENTDEPNAME" IsBound="false"
                                            Width="10%" HeaderText="Parent Dep Name">
                                            <Header Caption="<%$Resources:ControlText,gvParentDepName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LEVELCODE" Key="LEVELCODE" IsBound="false"
                                            Hidden="True" HeaderText="Level Code">
                                            <Header Caption="Level Code">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LEVELNAME" Key="LEVELNAME" IsBound="false"
                                            Width="10%" HeaderText="Level Name">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLevelName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="COSTCODE" Key="COSTCODE" IsBound="false" HeaderText="Cost Code">
                                            <Header Caption="<%$Resources:ControlText,gvCostCode %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ORDERID" Key="ORDERID" IsBound="false" HeaderText="Order ID"
                                            Width="5%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AreaCode" Key="AreaCode" IsBound="false" Hidden="True"
                                            HeaderText="AreaCode">
                                            <Header Caption="AreaCode">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AreaName" Key="AreaName" IsBound="false" Width="10%"
                                            HeaderText="AreaName">
                                            <Header Caption="<%$Resources:ControlText,gvAreaName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AccountEntity" Key="AccountEntity" IsBound="false"
                                            Width="8%" HeaderText="AccountEntity">
                                            <Header Caption="<%$Resources:ControlText,gvAccountEntity %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SITEID" Key="SITEID" IsBound="false" Width="8%"
                                            HeaderText="SITEID">
                                            <Header Caption="SITEID">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DELETED" Key="DELETED" IsBound="false" Hidden="True"
                                            HeaderText="Deleted">
                                            <Header Caption="Deleted">
                                            </Header>
                                        </igtbl:UltraGridColumn>--%>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPCODE" Key="DEPCODE" IsBound="false" Hidden="True"
                                            HeaderText="Dep Code">
                                            <Header Caption="Dep Code" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Hidden="True"
                                            HeaderText="Dep Name">
                                            <Header Caption="Dep Name" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME1" Key="DEPNAME1" IsBound="false" Width="300"
                                            HeaderText="Dep Name">
                                            <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CORPORATIONID" Key="CORPORATIONID" IsBound="false"
                                            Hidden="True" HeaderText="Corporation ID">
                                            <Header Caption="Corporation ID">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CORPORATIONNAME" Key="CORPORATIONNAME" IsBound="false"
                                            Width="60" HeaderText="Corporation Name">
                                            <Header Caption="<%$Resources:ControlText,gvCorporationName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PARENTDEPCODE" Key="PARENTDEPCODE" IsBound="false"
                                            HeaderText="Parent Dep Code" Hidden="true">
                                            <Header Caption="Parent Dep Code">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PARENTDEPNAME" Key="PARENTDEPNAME" IsBound="false"
                                            Width="80" HeaderText="Parent Dep Name">
                                            <Header Caption="<%$Resources:ControlText,gvParentDepName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LEVELCODE" Key="LEVELCODE" IsBound="false"
                                            Hidden="True" HeaderText="Level Code">
                                            <Header Caption="Level Code">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LEVELNAME" Key="LEVELNAME" IsBound="false"
                                            Width="80" HeaderText="Level Name">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLevelName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="COSTCODE" Key="COSTCODE" IsBound="false" HeaderText="Cost Code">
                                            <Header Caption="<%$Resources:ControlText,gvCostCode %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ORDERID" Key="ORDERID" IsBound="false" HeaderText="Order ID"
                                            Width="50">
                                            <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AreaCode" Key="AreaCode" IsBound="false" Hidden="True"
                                            HeaderText="AreaCode">
                                            <Header Caption="AreaCode">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AreaName" Key="AreaName" IsBound="false" Width="80"
                                            HeaderText="AreaName">
                                            <Header Caption="<%$Resources:ControlText,gvAreaName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AccountEntity" Key="AccountEntity" IsBound="false"
                                            Width="60" HeaderText="AccountEntity">
                                            <Header Caption="<%$Resources:ControlText,gvAccountEntity %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SITEID" Key="SITEID" IsBound="false" Width="60"
                                            HeaderText="SITEID">
                                            <Header Caption="SITEID">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DELETED" Key="DELETED" IsBound="false" Hidden="True"
                                            HeaderText="Deleted">
                                            <Header Caption="Deleted">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DeptShortName" Key="DeptShortName" IsBound="false" 
                                            HeaderText="DeptShortName" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvDeptShortName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DeptAlias" Key="DeptAlias" IsBound="false" 
                                            HeaderText="DeptAlias" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvDeptAlias %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="FactoryCode" Key="FactoryCode" IsBound="false"
                                            HeaderText="FactoryCode" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvFactoryCode %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
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
    </div>
    </form>

    <script type="text/javascript"><!--
		if(igtbl_getElementById("ProcessFlag").value.length==0)
		{		    
			igtbl_getElementById("txtCorporationId").readOnly=true;
			igtbl_getElementById("txtDepName").readOnly=true;
			igtbl_getElementById("txtParentDepCode").readOnly=true;
			igtbl_getElementById("txtLevelCode").readOnly=true;
			igtbl_getElementById("txtCostCode").readOnly=true;
		    igtbl_getElementById("txtOrderId").readOnly=true;
			igtbl_getElementById("txtAreaCode").readOnly=true;
			igtbl_getElementById("txtAreaName").readOnly=true;
		}
		if(igtbl_getElementById("ProcessFlag").value=="Add"||igtbl_getElementById("ProcessFlag").value=="Modify")
		{
			igtbl_getElementById("txtAreaCode").readOnly=true;
			igtbl_getElementById("txtAreaName").readOnly=true;
		}
	--></script>

</body>
</html>
