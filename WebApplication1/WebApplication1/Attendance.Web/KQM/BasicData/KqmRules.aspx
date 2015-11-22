<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KqmRules.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KqmRules" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>缺勤規則設定</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">

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
		    var operateType=$.trim($("#<%=hidOperate.ClientID%>").val());
		    
			if(igtbl_getElementById("ProcessFlag").value!="Add" && igtbl_getElementById("ProcessFlag").value!="Condition" && row != null)
			{
			if(operateType!="add" && operateType!="modify" && operateType!="condition")
			{
			igtbl_getElementById("IDRules").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();
			igtbl_getElementById("ddlAbsentType").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
			igtbl_getElementById("txtAbsentTypeDesc").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
			igtbl_getElementById("txtThreshold0").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
			igtbl_getElementById("txtThreshold1").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();	
			igtbl_getElementById("ddlPunishType").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
			igtbl_getElementById("txtPNumber").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();
			igtbl_getElementById("txtEmolument").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
			igtbl_getElementById("txtFormula").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
			igtbl_getElementById("txtEffectDate").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue('yyyy/mm/dd');
			igtbl_getElementById("txtExpireDate").value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue('yyyy/mm/dd');
			igtbl_getElementById("txtUpdate_User").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();
			igtbl_getElementById("txtUpdate_Date").value=row.getCell(14).getValue()==null?"":row.getCell(14).getValue('yyyy/mm/dd');
			igtbl_getElementById("txtRemark").value=row.getCell(15).getValue()==null?"":row.getCell(15).getValue();
			}
			}
		}

  $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
       
       $(function(){
       $("#td_show_1,#td_show_2,#div_img2").toggle(
            function(){
                $("#div_data").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_data").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 

       });
       
       
             function btnCondition_OnClientClick(){
             removeValue();
             $('#<%=ddlAbsentType.ClientID %>').removeAttr("disabled");
             $('#<%=ddlPunishType.ClientID %>').removeAttr("disabled");
             $("#<%=hidOperate.ClientID %>").val("condition");    
             addDisabled();
             $('#<%=btnQuery.ClientID %>').removeAttr("disabled");
             $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
             $("#<%=IDRules.ClientID %>").val("");
             
             return false;
            }
        
        
        
        
     
        function btnAdd_OnClientClick()
        {
          removeValue();
          removeReadonly();
          $('select#ddlAbsentType')[0].selectedIndex = 0;
            $('select#ddlPunishType')[0].selectedIndex = 0;
          
          $("#<%=hidOperate.ClientID %>").val("add");
             addDisabled();
             $('#<%=btnSave.ClientID %>').removeAttr("disabled");
             $('#<%=btnCancel.ClientID %>').removeAttr("disabled");

           return false;                 
        }
        
        

        function btnModify_OnClientClick()
        {
        var rulesID = $.trim($("#<%=IDRules.ClientID%>").val());
        if (rulesID=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
          //removeValue();
          removeReadonly();
          $("#<%=hidOperate.ClientID %>").val("modify");
          addDisabled();
             $('#<%=btnSave.ClientID %>').removeAttr("disabled");
             $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
            return false;
         
        }                        
        }
        
        
        
        function btnSave_OnClientClick()
        {
           var flag=0;
           var txtThreshold0=$.trim($("#<%=txtThreshold0.ClientID%>").val());
           var txtThreshold1=$.trim($("#<%=txtThreshold1.ClientID%>").val());
           var txtPNumber=$.trim($("#<%=txtPNumber.ClientID%>").val());
           
            if (confirm(Message.SaveConfim))
            {
                 $("#<%=txtAbsentTypeDesc.ClientID %>,#<%=txtThreshold0.ClientID %>,#<%=txtThreshold1.ClientID %>,#<%=txtPNumber.ClientID %>,#<%=txtEmolument.ClientID %>,#<%=ddlPunishType.ClientID %>,#<%=ddlAbsentType.ClientID %>").each(function() 
                 {
                  if ($.trim($(this).val()))  {   flag= flag+1; } 
                  else  { flag=flag;}
                 })
                 
                 if (flag==7)
                 {
                    if((!isDigitOnly(txtThreshold0) )|| (!isDigitOnly(txtThreshold1))|| (!isDigitOnly(txtPNumber) ))
                    {
                        alert($.trim($("#<%=lblThreshold0.ClientID%>").text())+","+$.trim($("#<%=lblThreshold1.ClientID%>").text())+","+ $.trim($("#<%=lblPNumber.ClientID%>").text())  +Message.RulesNotNumber);
                        return false;
                    }
                    else
                    {
                        return true;

                    }
                 }
                 else 
                 {
                    alert(Message.NecessayNotNull);
                    //alert(Message.StringFormNotRight);
                    return false;
                 }            
                 
            }
            else 
            {
            return false;
            }    
        
         }
       
        
  
        
        function btnDelete_OnClientClick()
        {
            var rulesID = $.trim($("#<%=IDRules.ClientID%>").val());
            if (rulesID=="")
            {
                alert(Message.AtLastOneChoose);
                return false;
            }
            else {   if (confirm(Message.RulesDeleteConfirm)) { return true; }  else { return false;  } }                  
        }
        
        
        

        function btnCancel_OnClientClick()
        {
        
            removeDisabled();
            $("#<%=btnSave.ClientID %>").attr("disabled","true");
            $("#<%=btnCancel.ClientID %>").attr("disabled","true");
            $("#<%=hidOperate.ClientID %>").val("");
            $('select#ddlAbsentType')[0].selectedIndex = 0;
            $('select#ddlPunishType')[0].selectedIndex = 0;
            $("#<%=IDRules.ClientID %>").val("");
            addReadonly();
            removeValue();  
            return false;              
        }
        
  
   
  function Load()
  {  
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      addReadonly();
      removeValue();
      return true;
  }
   
   
   
    function isDigitOnly(num) 
    {
      return /\d+/.test(num);
    }
    
    function CheckNum()
    {
        var txtThreshold0=$.trim($("#<%=txtThreshold0.ClientID%>").val())==null ?"":$.trim($("#<%=txtThreshold0.ClientID%>").val());
        var txtThreshold1=$.trim($("#<%=txtThreshold1.ClientID%>").val())==null ?"":$.trim($("#<%=txtThreshold1.ClientID%>").val());
        var txtPNumber=$.trim($("#<%=txtPNumber.ClientID%>").val())==null ?"":$.trim($("#<%=txtPNumber.ClientID%>").val());
        if(!isDigitOnly(txtThreshold0)&&(txtThreshold0!="") ) 
        {
        alert($.trim($("#<%=lblThreshold0.ClientID%>").text()) +Message.RulesNotNumber);
        }
        if (!isDigitOnly(txtThreshold1)&&(txtThreshold1!="") )
        {alert($.trim($("#<%=lblThreshold1.ClientID%>").text())  +Message.RulesNotNumber);}
        if (!isDigitOnly(txtPNumber) &&(txtPNumber!="") )
        {alert($.trim($("#<%=lblPNumber.ClientID%>").text())  +Message.RulesNotNumber);}
    }
   
    function CheckDate()
   {
      var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
      var EffectDate= $("#<%=txtEffectDate.ClientID%>").val();
      var ExpireDate=$("#<%=txtExpireDate.ClientID %>").val();
      if (EffectDate!=null&&EffectDate!="")
      {
         if(!check.test(EffectDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtEffectDate.ClientID%>").val("");
           return false;
         }
      }
      if (ExpireDate!=null&&ExpireDate!="")
      {
         if(!check.test(ExpireDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtExpireDate.ClientID%>").val();
           return false;
         }
      }
      if((EffectDate!=null&&EffectDate!="")&&(ExpireDate!=null&&ExpireDate!=""))
      {
        if(ExpireDate<EffectDate)
        {
           alert(Message.ToLaterThanFrom);
           $("#<%=txtExpireDate.ClientID %>").val("");
           return false;
        }
      }
      return true;
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
      $("#<%=txtAbsentTypeDesc.ClientID %>").removeAttr("readonly");
      $("#<%=txtThreshold0.ClientID %>").removeAttr("readonly");
      $("#<%=txtThreshold1.ClientID %>").removeAttr("readonly");
      $("#<%=txtPNumber.ClientID %>").removeAttr("readonly");
      $("#<%=txtEmolument.ClientID %>").removeAttr("readonly");
      $("#<%=txtFormula.ClientID %>").removeAttr("readonly");
      $("#<%=txtEffectDate.ClientID %>").removeAttr("readonly");
      $("#<%=txtExpireDate.ClientID %>").removeAttr("readonly");
      $("#<%=txtRemark.ClientID %>").removeAttr("readonly");
      
      $('#<%=ddlAbsentType.ClientID %>').removeAttr("disabled");
      $('#<%=ddlPunishType.ClientID %>').removeAttr("disabled");
      $(".input_textBox_1").css("border-style", "solid");
      $(".input_textBox_2").css("border-style", "solid");
      $(".input_textBox_1").css("background-color", "White");
      $(".input_textBox_2").css("background-color", "White");
      $("#<%=txtAbsentTypeDesc.ClientID %>").css("background-color", "Cornsilk");
      $("#<%=txtThreshold0.ClientID %>").css("background-color", "Cornsilk");
      $("#<%=txtThreshold1.ClientID %>").css("background-color", "Cornsilk");
      $("#<%=txtPNumber.ClientID %>").css("background-color", "Cornsilk");
      $("#<%=txtEmolument.ClientID %>").css("background-color", "Cornsilk");



    }
   
   function addReadonly()
    { 

      $(".img_hidden").show();
      $('#<%=txtAbsentTypeDesc.ClientID %>').attr("readonly","true");
      $('#<%=txtThreshold0.ClientID %>').attr("readonly","true");
      $('#<%=txtThreshold1.ClientID %>').attr("readonly","true");
      $('#<%=txtPNumber.ClientID %>').attr("readonly","true");
      $('#<%=txtEmolument.ClientID %>').attr("readonly","true");
      $('#<%=txtFormula.ClientID %>').attr("readonly","true");
      $('#<%=txtEffectDate.ClientID %>').attr("readonly","true");
      $('#<%=txtExpireDate.ClientID %>').attr("readonly","true");
      $('#<%=txtUpdate_User.ClientID %>').attr("readonly","true");
      $('#<%=txtUpdate_Date.ClientID %>').attr("readonly","true");
      $('#<%=txtRemark.ClientID %>').attr("readonly","true");
      $('#<%=ddlAbsentType.ClientID %>').attr("disabled","true");
      $('#<%=ddlPunishType.ClientID %>').attr("disabled","true");
      $(".input_textBox_1").css("border-style", "none");
      $(".input_textBox_2").css("border-style", "none");
      $(".input_textBox_1").css("background-color", "#efefef");
      $(".input_textBox_2").css("background-color", "");
      
    }
    
    function removeDisabled()
    { 
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         $("#<%=btnCondition.ClientID %>").removeAttr("disabled");
         $("#<%=btnAdd.ClientID %>").removeAttr("disabled");
         $("#<%=btnModify.ClientID %>").removeAttr("disabled");
         $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");
          $("#<%=btnQuery.ClientID %>").removeAttr("disabled");

    }
    
     function addDisabled()
    {
         $("#<%=btnCancel.ClientID %>").attr("disabled","true");
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
    }
   

    </script>

</head>
<body onload="return Load()">
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="ContractTypeValue" type="hidden" name="ContractTypeValue" runat="server">
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
        <div id="div_1">
            <asp:Panel ID="pnlContent" runat="server">
                <table class="table_data_area">
                    <tr>
                        <td colspan="2">
                            <table cellspacing="0" class="table_data_area" cellpadding="0" style="width: 100%">
                                <tr class="tr_data_1">
                                    <td class="td_label" style="width: 10%">
                                        <asp:Label ID="lblAbsentType" runat="server" ForeColor="Blue" ></asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 40%" colspan="3">
                                        <asp:DropDownList ID="ddlAbsentType" runat="server" Width="98%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_label" style="width: 10%; height: 24px">
                                        <asp:Label ID="lblAbsentTypeDesc" runat="server" ForeColor="Blue" ></asp:Label>
                                    </td>
                                    <td class="td_label" colspan="3" style="width: 10%">
                                        <asp:TextBox ID="txtAbsentTypeDesc" runat="server" Width="98%" CssClass="input_textBox_1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tr_data_2">
                                    <td class="td_label" style="width: 10%">
                                        <asp:Label ID="lblThreshold0" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 15%">
                                        <asp:TextBox ID="txtThreshold0" runat="server" CssClass="input_textBox_2" onchange="return CheckNum();"
                                            Width="70%"></asp:TextBox>
                                        <asp:Label ID="lblMinute0" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_label" style="width: 10%">
                                        <asp:Label ID="lblThreshold1" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 14%">
                                        <asp:TextBox ID="txtThreshold1" runat="server" CssClass="input_textBox_2" onchange="return CheckNum();"
                                            Width="70%"></asp:TextBox>
                                        <asp:Label ID="lblMinute1" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_label" style="width: 11%">
                                        <asp:Label ID="lblPunishType" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="td_label" style="width: 15%">
                                        <asp:DropDownList ID="ddlPunishType" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_label" style="width: 10%">
                                        <asp:Label ID="lblPNumber" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 10%">
                                        <asp:TextBox ID="txtPNumber" runat="server" Width="95%" onchange="return CheckNum();"
                                            CssClass="input_textBox_2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tr_data_1">
                                    <td class="td_label" style="width: 10%">
                                        <asp:Label ID="lblEmolument" runat="server" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td class="td_label" colspan="3" style="height: 24px">
                                        <asp:TextBox ID="txtEmolument" runat="server" Width="98%" CssClass="input_textBox_1"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 11%; height: 24px">
                                        <asp:Label ID="lblFormula" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_input" colspan="3" style="height: 24px">
                                        <asp:TextBox ID="txtFormula" runat="server" Width="98%" CssClass="input_textBox_1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tr_data_2">
                                    <td class="td_label" style="width: 10%; height: 24px">
                                        <asp:Label ID="lblEffectDate" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_input" style="width: 15%; height: 24px">
                                        <asp:TextBox ID="txtEffectDate" runat="server" CssClass="input_textBox_2" onchange="return CheckDate();"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 10%; height: 24px">
                                        <asp:Label ID="lblExpireDate" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_label" style="width: 14%; height: 24px">
                                        <asp:TextBox ID="txtExpireDate" runat="server" CssClass="input_textBox_2" onchange="return CheckNum();"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 11%; height: 24px">
                                        <asp:Label ID="lblUpdate_User" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_label" style="width: 15%; height: 24px">
                                        <asp:TextBox ID="txtUpdate_User" runat="server" Width="95%" CssClass="input_textBox_noborder_2"></asp:TextBox>
                                    </td>
                                    <td class="td_label" style="width: 10%; height: 24px">
                                        <asp:Label ID="lblUpdate_Date" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_input" style="height: 24px" width="30%">
                                        <asp:TextBox ID="txtUpdate_Date" runat="server" CssClass="input_textBox_noborder_2"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tr_data_1">
                                    <td class="td_label" style="width: 10%; height: 24px">
                                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                    </td>
                                    <td class="td_input" colspan="7" style="height: 24px">
                                        <asp:TextBox ID="txtRemark" runat="server" Width="98%" CssClass="input_textBox_1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                        <asp:Button ID="btnCondition" runat="server" CssClass="button_1" OnClientClick="return btnCondition_OnClientClick()">
                                        </asp:Button>
                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClientClick="return btnAdd_OnClientClick()">
                                        </asp:Button>
                                        <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return btnModify_OnClientClick()">
                                        </asp:Button>
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click"
                                            OnClientClick="return btnDelete_OnClientClick()"></asp:Button>
                                        <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                            OnClientClick="return btnSave_OnClientClick()"></asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" CssClass="button_1" OnClientClick="return btnCancel_OnClientClick()">
                                        </asp:Button>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <input id="IDRules" name="IDRules" type="hidden" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hidOperate" runat="server" />
            </asp:Panel>
        </div>
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
                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                            ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px;" id="td_show_2">
                    <img id="div_img2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                </td>
            </tr>
        </table>
        <div id="div_data">
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

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGrid_DataBound">
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
                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                            <Header Caption="" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AbsentType" Key="AbsentType" IsBound="false"
                                            Hidden="true">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesAbsentType %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AbsentTypeValue" Key="AbsentTypeValue" IsBound="false"
                                            Width="100px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesAbsentTypeValue %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="AbsentTypeDesc" Key="AbsentTypeDesc" IsBound="false"
                                            Width="150px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesAbsentTypeDesc %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Threshold0" Key="Threshold0" IsBound="false"
                                            Width="80px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesThreshold0 %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Threshold1" Key="Threshold1" IsBound="false"
                                            Width="90px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesThreshold1 %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PunishType" Key="PunishType" IsBound="false"
                                            Hidden="true">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesPunishType %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PunishTypeValue" Key="PunishTypeValue" IsBound="false"
                                            Width="90px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesPunishTypeValue %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PNumber" Key="PNumber" IsBound="false" Width="80px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesPNumber %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Emolument" Key="Emolument" IsBound="false"
                                            Width="280px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesEmolument %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Formula" Key="Formula" IsBound="false">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesFormula %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EffectDate" Key="EffectDate" IsBound="false" Format="yyyy/MM/dd">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesEffectDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ExpireDate" Key="ExpireDate" IsBound="false" Format="yyyy/MM/dd">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesExpireDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Update_User" Key="Modifier" IsBound="false">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesUpdateUser %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Update_Date" Key="ModifyDate" IsBound="false"  Format="yyyy/MM/dd">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesUpdateDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false">
                                            <Header Caption="<%$Resources:ControlText,gvHeadRulesRemark %>">
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
</body>
</html>
