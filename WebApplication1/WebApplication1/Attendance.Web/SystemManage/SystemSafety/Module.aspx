<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.Module" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<%@ Register TagPrefix="ControlLib" TagName="Title" Src="../ControlLib/Title.ascx" %>--%>
<%@ Register TagPrefix="ControlLib" TagName="PageNavigator" Src="~/ControlLib/PageNavigator.ascx" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系統模組</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="~/CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script>
	
		
	function AfterSelectChange(tableName, itemName) 
	{
		var row = igtbl_getRowById(itemName);
		DisplayRowData(row);
		return 0;
	}
	function UltraWebGridModule_InitializeLayoutHandler(gridName)
	{
		var row = igtbl_getActiveRow(gridName);
		DisplayRowData(row);
	}
	function DisplayRowData(row)
	{
	    var operateType=$.trim($("#<%=hidOperate.ClientID%>").val());
		if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
		{
		if(operateType!="add" && operateType!="modify" && operateType!="condition")
			{
			igtbl_getElementById("txtModuleCode").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
			igtbl_getElementById("txtOrderId").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
			igtbl_getElementById("txtParentModuleCode").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
			igtbl_getElementById("txtParentModuleName").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			igtbl_getElementById("txtFormName").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
			igtbl_getElementById("txtUrl").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
			igtbl_getElementById("txtDescription").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();	
			igtbl_getElementById("txtPrivileged").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();	
			igtbl_getElementById("txtFunctionList").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
			igtbl_getElementById("txtFunctionDesc").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue();
			igtbl_getElementById("hidDeleted").value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue();
			igtbl_getElementById("txtFunctionMenuType").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();
			igtbl_getElementById("txtFunctionComment").value=row.getCell(14).getValue()==null?"":row.getCell(14).getValue();
			igtbl_getElementById("txtFunctionVersion").value=row.getCell(15).getValue()==null?"":row.getCell(15).getValue();
			igtbl_getElementById("txtFunctionImage").value=row.getCell(16).getValue()==null?"":row.getCell(16).getValue();
			igtbl_getElementById("txtFunctionMouseImage").value=row.getCell(17).getValue()==null?"":row.getCell(17).getValue();
			igtbl_getElementById("ddlListFlag").value=row.getCell(18).getValue()==null?"":row.getCell(18).getValue();
			igtbl_getElementById("ddlIsKaoQin").value=row.getCell(19).getValue()==null?"":row.getCell(19).getValue();
			$("#<%=hidSelectFlag.ClientID %>").val("");
			}
			
		
		}
	}
    
    
    function removeValue()
    {
      $(".input_textBox_noborder_1").attr("value","");
      $(".input_textBox_noborder_2").attr("value","");
      $(".input_textBox_1").attr("value","");
      $(".input_textBox_2").attr("value","");
    }

    
    function removeReadonly()
    { 
      $(".img_hidden").show();
      $("#<%=txtDescription.ClientID %>").removeAttr("readonly");
      $("#<%=txtFormName.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionDesc.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionList.ClientID %>").removeAttr("readonly");
      $("#<%=txtUrl.ClientID %>").removeAttr("readonly");
      $("#<%=txtOrderId.ClientID %>").removeAttr("readonly");
      $("#<%=txtParentModuleCode.ClientID %>").removeAttr("readonly");
      
      $("#<%=txtFunctionMenuType.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionComment.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionVersion.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionImage.ClientID %>").removeAttr("readonly");
      $("#<%=txtFunctionMouseImage.ClientID %>").removeAttr("readonly");
      $('#<%=ddlListFlag.ClientID %>').removeAttr("disabled");
      $('#<%=ddlIsKaoQin.ClientID %>').removeAttr("disabled");
      
      $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
      $("#<%=btnSave.ClientID %>").removeAttr("disabled");
      $(".input_textBox_1").css("border-style", "solid");
      $(".input_textBox_2").css("border-style", "solid");
      $(".input_textBox_1").css("background-color", "White");
      $(".input_textBox_2").css("background-color", "White");
      $("#<%=txtModuleCode.ClientID %>").css("background-color", "Cornsilk");
      $("#<%=txtOrderId.ClientID %>").css("background-color", "Cornsilk");
      $('#<%=ddlIsKaoQin.ClientID %>').removeAttr("disabled");
      $('#<%=ddlListFlag.ClientID %>').removeAttr("disabled");
    }
    
    function addReadonly()
  {
      $("#<%=txtModuleCode.ClientID %>").attr("readonly","true");
      $("#<%=txtDescription.ClientID %>").attr("readonly","true");
      $("#<%=txtFormName.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionDesc.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionList.ClientID %>").attr("readonly","true");
      $("#<%=txtUrl.ClientID %>").attr("readonly","true");
      $("#<%=txtOrderId.ClientID %>").attr("readonly","true");
      $("#<%=txtParentModuleCode.ClientID %>").attr("readonly","true");
      
      $("#<%=txtFunctionMenuType.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionComment.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionVersion.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionImage.ClientID %>").attr("readonly","true");
      $("#<%=txtFunctionMouseImage.ClientID %>").attr("readonly","true");
      $('#<%=ddlListFlag.ClientID %>').attr("disabled","true");
      $('#<%=ddlIsKaoQin.ClientID %>').attr("disabled","true");
      
       $("#<%=txtPrivilegedName.ClientID %>").css("border-style", "none");
      $("#<%=txtParentModuleName.ClientID %>").css("border-style", "none");
      $(".input_textBox_1").css("border-style", "none");
      $(".input_textBox_2").css("border-style", "none");
      $(".input_textBox_1").css("background-color", "#efefef");
      $(".input_textBox_2").css("background-color", "");
      $('#<%=ddlIsKaoQin.ClientID %>').attr("disabled","true");
      $('#<%=ddlListFlag.ClientID %>').attr("disabled","true");
      $('select#ddlIsKaoQin')[0].selectedIndex = 0;
      $('select#ddlListFlag')[0].selectedIndex = 0;
      
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
     $("#<%=imgbtnPrivileged.ClientID %>").attr("disabled","true");
     removeValue();
     removeReadonly();
     $("#<%=txtModuleCode.ClientID %>").removeAttr("readonly");
     $("#<%=hidOperate.ClientID %>").val("condition");    
       addDisabled();
     $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
     $("#<%=btnQuery.ClientID %>").removeAttr("disabled");

     $("#<%=txtPrivileged.ClientID %>").css("border-style", "none");
     $("#<%=txtPrivileged.ClientID %>").css("background-color", "");
     $("#imgbtnPrivileged").hide();
     $("#<%=hidSelectFlag.ClientID %>").val("select");
     return false; 
    }
   

    function add_click()
    {
      removeValue();
      removeReadonly();
        addDisabled();
       $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 

      $("#<%=txtModuleCode.ClientID %>").removeAttr("readonly");
      $("#<%=txtParentModuleCode.ClientID %>").attr("readonly","true");
      $("#<%=hidOperate.ClientID %>").val("add"); 
       return false;                 
    }
    
   function modify_click()
    {
   
     var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
    if (moduleCode =="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
      removeReadonly();
      $("#<%=hidOperate.ClientID %>").val("modify");  
       addDisabled();
       $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
       $("#<%=txtParentModuleCode.ClientID %>").attr("readonly","true");
       $("#<%=txtModuleCode.ClientID %>").css("border-style", "none");
       $("#<%=txtPrivileged.ClientID %>").css("border-style", "none");
      return false; 
      }
    }
    
    
    
    function save_click()
    {
        if (confirm(Message.SaveConfim))
        {
            var alertText;
            var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
            var parentModuleCode = $.trim($("#<%=txtParentModuleCode.ClientID%>").val());
            var orderID = $.trim($("#<%=txtOrderId.ClientID%>").val());
            
            
                if (moduleCode=="" && orderID=="" )
                {
                    alertText = $.trim($("#<%=lblModuleCode.ClientID%>").text()) + "," + $.trim($("#<%=lblOrderID.ClientID%>").text()) + Message.TextBoxNotNull;
                    alert(alertText);
                    return false;
                }
                else if (moduleCode=="")
                {
                    alertText = $.trim($("#<%=lblModuleCode.ClientID%>").text()) +Message.TextBoxNotNull;
                    alert(alertText);
                    return false;
                }
                else if (orderID=="")
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
                    else
                    {
                     if(moduleCode==parentModuleCode)
                     {
                        alert(Message.ModuleCodeEquleParent);
                       return false;
                     }
                     else
                     {
                     return true; 
                     } 
                    
                    }
                }
        }
        else
        {
        return false;
        }
    }
      

      
    function disable_click()
    {
     var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
        if (moduleCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
    if (confirm(Message.DisableConfirm))
    
    {
    $("#<%=hidModuleCode.ClientID %>").val(moduleCode);
     $("#<%=hidOperate.ClientID %>").val("disable");
     var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
     return true;
//     $.ajax({
//                type: "post", 
//                url: "Module.aspx", 
//                dataType: "text", 
//                data: {ActionFlag:actionFlag,ModuleCode: moduleCode},
//                                            success: function(msg) { 
//                                                if (msg==1) {alert(Message.DisableSuccess); } 
//                                                else
//                                                {
//                                                  alert(Message.DisableFaild);
//                           
//                                                }
//                                              }
//                                        });  
                                        }
                                       else return false;   
                                      }
                                        
    }
    
    
    
    function enable_click()
    {
     var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
        if (moduleCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
        if (confirm(Message.EnableConfirm))
        {
     
     $("#<%=hidModuleCode.ClientID %>").val(moduleCode);
     $("#<%=hidOperate.ClientID %>").val("enable");
     var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
  return true;
//     $.ajax({
//                type: "post", 
//                url: "Module.aspx", 
//                dataType: "text", 
//                data: {ActionFlag:actionFlag,ModuleCode: moduleCode},
//                                            success: function(msg) { 
//                                                if (msg==1) {alert(Message.EnableSuccess); } 
//                                                else
//                                                {
//                                                  alert(Message.EnableFaild);
//                           
//                                                }
//                                              }
//                                        });  
                                        }
                                       else  return false;   
                                       }
                                        
    }
    
    function isDigitOnly(num) 
    {
      return /\d+/.test(num);
    }

    function delete_click()
    {
        $("#<%=hidOperate.ClientID %>").val("delete");
        var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
        var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
        if (moduleCode=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
          if (confirm(Message.DeleteConfirm))
            {
            return true;
            }
            else
            {
            return false;
            }
        } 
    }
    
   function cancel_click()
   {
      removeValue();
      $("#<%=hidOperate.ClientID %>").val("");
      $(".input_textBox").css("border-style", "none");
       addReadonly();
       removeDisabled();
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");

      $("#<%=imgbtnPrivileged.ClientID %>").removeAttr("disabled");
//      $("#imgbtnPrivileged").hide();
//      $("#imgbtnParentmodulecode").hide();
      $(".img_hidden").hide();
      return false;
   }
   
	   
    $(function(){
    $("#tr_edit").toggle(
        function(){
            $("#div_select").hide();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#div_select").show();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
       
        }
    )
    $(function(){
    $("#div_img2,#td_show_1,#td_show_2").toggle(
        function(){
            $("#div_showdata").hide();
            $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#div_showdata").show();
            $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
        }
    )
    
    $("#txtModuleCode").blur(
    function (){
    var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
    var moduleCode = $.trim($("#<%=txtModuleCode.ClientID%>").val());
    if (actionFlag=="add"&& moduleCode!="")
    {
     $.ajax({
                          type: "post", 
                          url: "Module.aspx", 
                          dataType: "text", 
                          data: {ActionFlag:actionFlag,ModuleCode: moduleCode},
                                            success: function(msg) { 
                                                if (msg !=1) {alert(Message.ModuleCodeIsExist);} 
                                                
                                              }
                                        });
                                        }
    
    
    }
    )
    
   });
   
   
   
    
     
});    

   
   function setSelector(ctrlCode,ctrlName,flag)
   {
       var code=$("#"+ctrlCode).val();
       if (flag=="module")
       {
       var url="ModuleSelector.aspx?r="+ Math.random();
       }
       else if (flag=="privileged")
       {
       var url="PrivilegedSelector.aspx?r="+ Math.random();
       }
   
       var fe="dialogHeight:400px; dialogWidth:350px; dialogTop:100px; dialogLeft:300px;status:no;scroll:no;";
       var info=window.showModalDialog(url,null,fe);
       if(info)
       {
           $("#"+ctrlCode).val(info.codeList);
           $("#" + ctrlName).val(info.nameList);
       }
       return false;
   }
   
   function Load()
   {
      $(".input_textBox").css("border-style", "none");
       addReadonly();
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      $(".img_hidden").hide();
     return true;
   }
   
    
    </script>

</head>
<body onload="return Load()">
    <form id="MyForm" runat="server">
    <div>
        <input type="hidden" id="ProcessFlag" runat="server">
        <input type="hidden" id="hidSelectFlag" runat="server">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
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
                            <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
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
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblModuleCode" runat="server" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtModuleCode" class="input_textBox_1" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblOrderID" runat="server" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtOrderId" runat="server" class="input_textBox_1" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblParentmodulecode" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="40%">
                                                                <asp:TextBox ID="txtParentModuleCode" runat="server" class="input_textBox_2" Width="95%"></asp:TextBox>
                                                            </td>
                                                            <td width="10%" style="cursor: hand">
                                                                <asp:ImageButton ID="imgbtnParentmodulecode" runat="server" class="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:ImageButton>
                                                            </td>
                                                            <td width="40%">
                                                                <asp:TextBox ID="txtParentModuleName" runat="server" class="input_textBox_noborder_2"
                                                                    Width="98%" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFormName" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFormName" runat="server" class="input_textBox_2" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblUrl" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtUrl" class="input_textBox_1" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtDescription" class="input_textBox_1" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblPrivileged" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 40%">
                                                                <asp:TextBox ID="txtPrivileged" runat="server" class="input_textBox_2" Width="95%"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 10%; cursor: hand">
                                                                <asp:ImageButton ID="imgbtnPrivileged" runat="server" class="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:ImageButton>
                                                            </td>
                                                            <td style="width: 40%">
                                                                <asp:TextBox ID="txtPrivilegedName" runat="server" class="input_textBox_noborder_2"
                                                                    Width="98%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionList" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionList" runat="server" class="input_textBox_2" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionDesc" runat="server"> </asp:Label>
                                                </td>
                                                <td style="width: 50%" colspan="2">
                                                    <asp:TextBox ID="txtFunctionDesc" runat="server" class="input_textBox_1" Width="98%"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionMenuType" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionMenuType" runat="server" class="input_textBox_2" Width="95%"></asp:TextBox>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionComment" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionComment" runat="server" class="input_textBox_2" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionVersion" runat="server"> </asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionVersion" runat="server" class="input_textBox_1" Width="98%"></asp:TextBox>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionImage" runat="server"> </asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionImage" runat="server" class="input_textBox_1" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblFunctionMouseImage" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtFunctionMouseImage" runat="server" class="input_textBox_2" Width="95%"></asp:TextBox>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="lblListFlag" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="ddlListFlag" runat="server">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="Y">Y</asp:ListItem>
                                                        <asp:ListItem Value="N">N</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="lblIsKaoQin1" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="ddlIsKaoQin" runat="server">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="Y">Y</asp:ListItem>
                                                        <asp:ListItem Value="N">N</asp:ListItem>
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
                                        <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return  modify_click()">
                                        </asp:Button>
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClientClick="return  delete_click()"
                                            OnClick="btnDelete_Click"></asp:Button>
                                        <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClientClick="return  save_click()"
                                            OnClick="btnSave_Click"></asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" CssClass="button_1" OnClientClick="return  cancel_click()">
                                        </asp:Button>
                                        <asp:Button ID="btnDisable" runat="server" CssClass="button_1" OnClientClick="return  disable_click()"
                                            OnClick="btnDisable_Click"></asp:Button>
                                        <asp:Button ID="btnEnable" runat="server" CssClass="button_1" OnClientClick="return  enable_click()"
                                            OnClick="btnEnable_Click"></asp:Button>
                                        <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click">
                                        </asp:Button>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidOperate" runat="server" />
            <asp:HiddenField ID="hidDeleted" runat="server" />
            <asp:HiddenField ID="hidModuleCode" runat="server" />
        </div>
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" id="td_show_1" class="tr_title_center">
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
                                ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;" id="td_show_2">
                        <img id="div_img2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*45/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridModule" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridModule_DataBound">
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
                                        DataKeyField="">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="description1" Key="description1" IsBound="false"
                                                Width="230px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadModuleDescription %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MODULECODE" Key="MODULECODE" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvHeadModuleCode %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DESCRIPTION" Key="DESCRIPTION" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvHeadDescription %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ORDERID" Key="ORDERID" IsBound="false" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PARENTMODULECODE" Key="PARENTMODULECODE" IsBound="false"
                                                Width="100px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadParentModuleCode %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PARENTMODULENAME" Key="PARENTMODULENAME" IsBound="false"
                                                Width="120px" Hidden="true">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FORMNAME" Key="FORMNAME" IsBound="false" Width="100px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="URL" Key="URL" IsBound="false" Width="300px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadURL %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DESCRIPTION" Key="DESCRIPTION" IsBound="false"
                                                Hidden="true">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PRIVILEGED" Key="PRIVILEGED" IsBound="false"
                                                Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadPrivileged %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FUNCTIONLIST" Key="FUNCTIONLIST" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFunctionList %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FUNCTIONDESC" Key="FUNCTIONDESC" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFunctionDesc %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DELETED" Key="DELETED" IsBound="false" Hidden="True">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Function_Menu_Type" Key="Function_Menu_Type" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblFunctionMenuType %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Function_Comment" Key="Function_Comment" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblFunctionComment %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Function_Version" Key="Function_Version" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblFunctionVersion %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Function_Image" Key="Function_Image" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblFunctionImage %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Function_Mouse_Image" Key="Function_Mouse_Image" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblFunctionMouseImage %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="List_Flag" Key="List_Flag" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblListFlag %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsKaoQin" Key="IsKaoQin" IsBound="false"
                                                Width="300px">
                                                <Header Caption="<%$Resources:ControlText,lblIsKaoQin1 %>">
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
        </div>
    </div>
    </form>
</body>
</html>
