<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveTypeForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.LeaveTypeForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script>
    
        	function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);	
			DisplayRowData(row);
			return 0;
		}
				function UltraWebGridOTMType_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
			function DisplayRowData(row)
		{
		if( $("#<%=hidOperate.ClientID %>").val()!='Add'&&$("#<%=hidOperate.ClientID %>").val()!='Condition')
		{
			igtbl_getElementById("<%=txtLvTypeCode.ClientID %>").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();
		    igtbl_getElementById("<%=txtLvTypeName.ClientID %>").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
		    igtbl_getElementById("<%=txtUseState.ClientID %>").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
		    igtbl_getElementById("<%=txtLimitDays.ClientID %>").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
		    igtbl_getElementById("<%=txtMinHours.ClientID %>").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
		    igtbl_getElementById("<%=txtStandardHours.ClientID %>").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
		    if( row.getCell(6).getValue()=='全部')
		    {
		    $("#<%=ddlFitSex.ClientID %>").get(0).selectedIndex=1;
		    }
		    else if(row.getCell(6).getValue()=='男')
		    {
		      $("#<%=ddlFitSex.ClientID %>").get(0).selectedIndex=2;
		    }
		      else if(row.getCell(6).getValue()=='女')
		    {
		      $("#<%=ddlFitSex.ClientID %>").get(0).selectedIndex=3;
		    }
		    igtbl_getElementById("<%=ddlHasMoney.ClientID %>").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
		    igtbl_getElementById("<%=txtProve.ClientID %>").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();
		    igtbl_getElementById("<%=txtRemark.ClientID %>").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
		    igtbl_getElementById("<%=ddlIscludeHoliday.ClientID %>").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
		    igtbl_getElementById("<%=ddlIsAllowPCM.ClientID %>").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue();	
		   $('#<%=ProcessFlag.ClientID %>').val('Y');
		   }
		}
		
    function checkLVTypeCode()
    {
     var LVTypeCode = $.trim($("#<%=txtLvTypeCode.ClientID %>").val());
                $.ajax({
                                        type: "post", url: "LeaveTypeForm.aspx", dateType: "text", data: {lvtypecode: LVTypeCode},
                                        success: function(msg) {
                                              if(msg=="Y")
                                              {
                                                  $("#<%=txtLvTypeCode.ClientID %>").val(null);
                                                   alert(Message.LvTypeExist);
                                              }
                                          }
                           }); 
    }
    
    
    $(document).ready(function()
    {
    
    $('#<%=btnDelete.ClientID %>,#<%=btnDisable.ClientID %>,#<%=btnEnable.ClientID %>').each(function(){
        $(this).click(function(){
         $("#<%=hidOperate.ClientID %>").val($(this).attr("id").substring(3));
           if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
		      return false;
	     	 }
	     	 else
	     	 {
	     	 if($(this).attr("id")=='<%=btnDelete.ClientID %>')
	     	 {
              return confirm(Message.DeleteAttTypeConfirm);
             }
             return true;
            }
    });
    });
   $('#<%=btnModify.ClientID %>').click(function()
   {
    	   if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
		      return false;
	     	 }
	     	 else
	     	 {
	     	   $(":text").removeClass("input_textBox");
	     	   $("#<%=hidOperate.ClientID %>").val("Modify");
	     	   $('#<%=txtLvTypeName.ClientID %>,#<%=txtUseState.ClientID %>,#<%=txtLimitDays.ClientID %>,#<%=txtProve.ClientID %>,#<%=txtRemark.ClientID %>,#<%=txtMinHours.ClientID %>,#<%=txtStandardHours.ClientID %>').each(function(){
	     	     $(this).attr('readonly',false);
	     	   });
	     	   $('select').each(function()
	     	   {
	     	   $(this).attr('disabled',false);
	     	   });
	       $('.button_1').each(function(){
                $(this).attr('disabled',true);
                 });
             $("#<%=btnCancel.ClientID %>").attr("disabled",false);
             $("#<%=btnSave.ClientID %>").attr("disabled",false);
             return false;
	     	 }
   });
    $('#<%=txtMinHours.ClientID %>,#<%=txtLimitDays.ClientID %>,#<%=txtStandardHours.ClientID %>').each(function(){
    $(this).blur(function(){
    if($.trim($(this).val()))
    {
    var regstr=/^\d+(\.\d{1,1})?$/;
     if(!regstr.exec($.trim($(this).val())))
     {
     $(this).val(null);
     }
    }
    });
    });
                 $('#<%=txtLvTypeCode.ClientID %>').blur(function()
            {
                        if($("#<%=hidOperate.ClientID %>").val()=="Add")
                        {
                        if ($.trim($("#<%=txtLvTypeCode.ClientID %>").val())) 
                        {  
                         var regstr=/^[A-Za-z]{1}$/;
                        if (regstr.exec($.trim($("#<%=txtLvTypeCode.ClientID %>").val())))
                        {
                           checkLVTypeCode();
                        }
                         else
                         {
                         $("#<%=txtLvTypeCode.ClientID %>").val(null);
                         }
                        } 
                        }
                   return  false;
            });
    $('#<%=btnAdd.ClientID %>').click(function(){
        $(':text').removeClass('input_textBox');
    $("#<%=hidOperate.ClientID %>").val("Add");
          $(':text').each(function(){
                $(this).attr('readonly',false);
                
                   $(this).val(null);
    });

    $("select").attr("disabled",false);
    $("select").val(null);
        $('.button_1').each(function(){
    $(this).attr('disabled',true);
    });
         $("#<%=btnCancel.ClientID %>").attr("disabled",false);
           $("#<%=btnSave.ClientID %>").attr("disabled",false);
     return false;
    });
    $('#<%=btnCancel.ClientID %>').click(function(){
      $(":text").addClass("input_textBox");
     $("#<%=hidOperate.ClientID %>").val('Cancel');
       $("#<%=ProcessFlag.ClientID %>").val('N');
                  $(':text').each(function(){
                $(this).attr('readonly',true);
                $(this).val(null);
    });
    $("select").attr("disabled",true);      
          $('.button_1').each(function(){
    $(this).attr('disabled',false);
    });  
       $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           $("#<%=btnSave.ClientID %>").attr("disabled",true);
    return false;
    });
    $('#<%=btnCondition.ClientID %>').click(function(){
    $(':text').removeClass('input_textBox');
   $("#<%=hidOperate.ClientID %>").val('Condition');
      $(':text').each(function(){
                $(this).attr('readonly',false);
                  $(this).val(null);
    });
    $("select").each(function(){
    $(this).attr("disabled",false);
    $(this).get(0).selectedIndex=0;
    });
        $('.button_1').each(function(){
    $(this).attr('disabled',true);
    });
        $("#<%=btnCancel.ClientID %>,#<%=btnQuery.ClientID %>").each(function()
        {
        $(this).attr("disabled",false);
        }
        );
        return false;
    });
              $('input').each(function(){
                $(this).attr('readonly',true);
    });
    $("select").attr("disabled",true);        
       $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           $("#<%=btnSave.ClientID %>").attr("disabled",true);
           
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
              $("#img_grid,#img_div2").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
                
    $('#<%=btnSave.ClientID %>').click(function()
    {
      
       		  var valid=true;
		    $("#<%=txtLvTypeCode.ClientID %>,#<%=txtLvTypeName.ClientID %>,#<%=txtMinHours.ClientID %>,#<%=txtStandardHours.ClientID %>").each(function() {
		 
                        if ($.trim($(this).val())) 
                        { $(this).css("border-color", "silver"); }
                        else
                         { valid = false; $(this).css("border-color", "#ff6666"); }
                    });
                   if($("#<%=ddlIsAllowPCM.ClientID %>").get(0).selectedIndex==0)
                    {
                      valid = false;
                      $(this).css("border-color", "#ff6666");
                    }
                   if($("#<%=ddlIscludeHoliday.ClientID %>").get(0).selectedIndex==0)
                    {
                      valid = false;
                      $(this).css("border-color", "#ff6666");
                    }
                    if($("#<%=ddlFitSex.ClientID %>").get(0).selectedIndex==0)
                    {
                      valid = false;
                      $(this).css("border-color", "#ff6666");
                    }
                    return valid;
             });
    });
    </script>

</head>
<style type="text/css">
    .input_textBox
    {
        border: 0;
        background-color:White;
    }
    .ddl_hiden
    {
        display: none;
    }
</style>
<body>
    <div style="width: 100%;">
        <form id="form1" runat="server">
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
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <asp:Panel ID="inputPanel" runat="server">
                                            <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                            <asp:HiddenField ID="hidEffectFlag" runat="server" Value="Y" />
                                            <asp:HiddenField ID="ProcessFlag" runat="server" Value="N" />
                                            <tr>
                                                <td class="td_label" width="14%">
                                                    &nbsp;<asp:Label ID="lblLvTypeCode" runat="server" ForeColor="Blue">LvTypeCode:</asp:Label>
                                                </td>
                                                <td class="td_input" width="13%">
                                                    <asp:TextBox ID="txtLvTypeCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblLvTypeName" runat="server" ForeColor="Blue">LvTypeName:</asp:Label>
                                                </td>
                                                <td class="td_input" width="18%">
                                                    <asp:TextBox ID="txtLvTypeName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblUseState" runat="server">UseState:</asp:Label>
                                                </td>
                                                <td class="td_input" width="21%">
                                                    <asp:TextBox ID="txtUseState" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label" width="14%">
                                                    &nbsp;<asp:Label ID="lblLimitDays" runat="server">LimitDays:</asp:Label>
                                                </td>
                                                <td class="td_input" width="13%">
                                                    <asp:TextBox ID="txtLimitDays" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblHasMoney" runat="server">HasMoney:</asp:Label>
                                                </td>
                                                <td class="td_input" width="18%">
                                                    <asp:DropDownList runat="server" ID="ddlHasMoney" Width="100%">
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="Y">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="N">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label" width="12%">
                                                    &nbsp;<asp:Label ID="lblProve" runat="server">Prove:</asp:Label>
                                                </td>
                                                <td class="td_input" width="21%">
                                                    <asp:TextBox ID="txtProve" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblIscludeHoliday" runat="server" ForeColor="Blue">IsCludeHoliday:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList runat="server" ID="ddlIscludeHoliday" Width="100%">
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="Y">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="N">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblIsAllowPCM" runat="server" ForeColor="Blue">ISAllowPCM:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList runat="server" ID="ddlIsAllowPCM" Width="100%">
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="Y">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="N">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblMinHours" runat="server" ForeColor="Blue">MinHours:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtMinHours" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblStandardHours" runat="server" ForeColor="Blue">StandardHours:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtStandardHours" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblFitSexName" runat="server" ForeColor="Blue">FitSex:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td style="cursor: hand" width="98%">
                                                                <asp:DropDownList runat="server" ID="ddlFitSex" Width="100%">
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,FitSexNameAll %>" Value="2">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,FitSexNameMan %>" Value="1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,FitSexNameWoman %>" Value="0">
                                                                    </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="td_label" colspan="6">
                                                <table>
                                                    <tr>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <td>
                                                                <asp:Button ID="btnCondition" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                                    ToolTip="Authority Code:Condition"></asp:Button>
                                                                <asp:Button ID="btnQuery" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnQuery %>"
                                                                    ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                    ToolTip="Authority Code:Add" CssClass="button_1"></asp:Button>
                                                                <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                    ToolTip="Authority Code:Modify" CssClass="button_1"></asp:Button>
                                                                <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                    ToolTip="Authority Code:Delete" CssClass="button_1" OnClick="btnDelete_Click">
                                                                </asp:Button>
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:Cancel"></asp:Button>
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:Save" OnClick="btnSave_Click"></asp:Button>
                                                                <asp:Button ID="btnDisable" runat="server" Text="<%$Resources:ControlText,btnDisable %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:Disable" OnClick="btnDisable_Click">
                                                                </asp:Button>
                                                                <asp:Button ID="btnEnable" runat="server" Text="<%$Resources:ControlText,btnEnable %>"
                                                                    CssClass="button_1" ToolTip="Authority Code:Enable" OnClick="btnEnable_Click">
                                                                </asp:Button>
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
        <div style="width: 100%">
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
                        <div id="img_div2">
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

                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveType" runat="server" Width="100%" Height="100%">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_leavetype_v" Key="gds_att_leavetype_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" IsBound="false" Key="LVTypeCode"
                                                Width="5%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLVTypeCode %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeName" IsBound="false" Key="LVTypeName"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLVTypeName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="UseState" IsBound="false" Key="UseState" Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderUseState %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LimitDays" IsBound="false" Key="LimitDays"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLimitDays %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MinHours" IsBound="false" Key="MinHours" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderMinHours%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StandardHours" IsBound="false" Key="StandardHours"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderStandardHours %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FitSexName" IsBound="false" Key="FitSexName"
                                                Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderFitSexName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="HasMoney" IsBound="false" Key="HasMoney" Width="7%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderHasMoney %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Prove" IsBound="false" Key="Prove" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderProve %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" IsBound="false" Key="Remark" Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsCludeHoliday" IsBound="false" Key="IsCludeHoliday"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderIsCludeHoliday %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ISAllowPCM" IsBound="false" Key="ISAllowPCM"
                                                Width="12%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderISAllowPCM %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FitSex" IsBound="false" Key="FitSex" Hidden="true">
                                                <Header Caption="FitSex">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EffectFlag" IsBound="false" Key="EffectFlag"
                                                Hidden="true">
                                                <Header Caption="EffectFlag">
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
