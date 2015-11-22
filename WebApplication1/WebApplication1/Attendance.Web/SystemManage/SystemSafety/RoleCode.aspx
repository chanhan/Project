<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleCode.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.RoleCode" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RoleCode</title>

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

</head>
<style type="text/css">
    .input_textBox
    {
        border: 0;
    }
    .ddl_hiden
    {
        display: none;
    }
</style>

<script>
           function AfterSelectChange(gridName, id)
       {
  			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridRole_InitializeLayoutHandler(gridName)
		{
		    var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
		if($("#<%=hidOperate.ClientID %>").val()!='Add' && $("#<%=hidOperate.ClientID %>").val()!='Condition')
		{
			igtbl_getElementById("<%=txtRoleCode.ClientID %>").value=row.getCell(0).getValue();
			igtbl_getElementById("<%=txtRoleName.ClientID %>").value=row.getCell(1).getValue();
     		var deleteInfo=row.getCell(2).getValue();
     		if(deleteInfo=='Y')
     		{
     		      $("#<%=btnDisable.ClientID %>").attr("disabled",true);
     		      $("#<%=btnEnable.ClientID %>").attr("disabled",false);
     		}
     		else if(deleteInfo=='N')
     		{
     		    $("#<%=btnEnable.ClientID %>").attr("disabled",true);
     		    $("#<%=btnDisable.ClientID %>").attr("disabled",false);
     		}
			var acceptmsgInfo=row.getCell(3).getValue();
	        $("#<%=ddlDeleted.ClientID %>").val(deleteInfo);
		   $("#<%=ddlAcceptmsg.ClientID %>").val(acceptmsgInfo);
           $("#<%=ddlAcceptmsg.ClientID %>").attr("disabled",true);	
           $("#<%=ddlDeleted.ClientID %>").attr("disabled",true); 
           $("#<%=txtRoleCode.ClientID %>").attr("readonly",true); 
           var moduleCode =$('#<%=ModuleCode.ClientID %>').val();
            $("#<%=hidModify.ClientID %>").val('Y');
            $("iframe").each(function () {
               var iframeSrc = 'AuthorityForm.aspx?rolecode='+row.getCell(0).getValue()+'&modulecode='+moduleCode;
                $(this).attr("src", iframeSrc);
            });
		}
          var RoleCode = $.trim($("#<%=txtRoleCode.ClientID %>").val());
                $.ajax({
                                 type: "post", url: "RoleCode.aspx", dateType: "text", data: {rolecode: RoleCode,flag: 'Disable'},
                                 success: function(msg) {
                                   if(msg=="Y")
                                              {
                                                  $("#<%=RoleStatue.ClientID %>").val("Y");
                                              }
                                          }
                           });
        //  點擊修改之後,選擇其他數據時,依然為可編輯.  F3228823                   
       if($("#<%=hidOperate.ClientID %>").val()=='Modify' )
       {              
          //移除只读
              $("#<%=txtRoleCode.ClientID %>").removeAttr("readonly");
              $("#<%=txtRoleName.ClientID %>").removeAttr("readonly");
        }
		}
		
         function checkRoleCode()
            {
                 var RoleCode = $.trim($("#<%=txtRoleCode.ClientID %>").val());
                $.ajax({
                                        type: "post", url: "RoleCode.aspx", dateType: "text", data: {rolecode: RoleCode,flag: 'Add'},
                                        success: function(msg) {
                                              if(msg=="Y")
                                              {
                                                  $("#<%=txtRoleCode.ClientID %>").val(null);
                                                   alert(Message.RoleExist);
                                              }
                                          }
                           }); 
            }
            
        $(document).ready(function(){
             $("#<%=btnSave.ClientID %>").attr("disabled","true");
             $('#<%=btnCancel.ClientID %>').attr("disabled","true");                  
             $('#<%=txtRoleCode.ClientID %>').blur(function()
            {
                        if($("#<%=hidOperate.ClientID %>").val()=="Add")
                        {
                        if ($.trim($("#<%=txtRoleCode.ClientID %>").val())) 
                        {  
                            checkRoleCode();
                        } 
                        }
                        else if($("#<%=hidOperate.ClientID %>").val()=="Modify")
                        {
                            
                        }
                   return  false;
            });
             $("#<%=btnSave.ClientID %>").click(function() {
                   var valid=true;
                    $("#<%=txtRoleCode.ClientID %>,#<%=txtRoleName.ClientID %>").each(function() {
                        if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { valid = false; $(this).css("border-color", "#ff6666"); }
                    });
                    return valid;
                });
                

           
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
              $("#img_grid,#td_show_1,#td_show_2").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
            $('#<%=btnCondition.ClientID %>').click(function ()
            {
             $(".input_textBox").removeClass("input_textBox");
             $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
             $("#<%=btnCondition.ClientID %>").attr("disabled","true");
             $("#<%=btnAdd.ClientID %>").attr("disabled","true");
             $("#<%=btnModify.ClientID %>").attr("disabled","true");
             $("#<%=btnDelete.ClientID %>").attr("disabled","true");
             $("#<%=btnDisable.ClientID %>").attr("disabled","true");
             $("#<%=btnEnable.ClientID %>").attr("disabled","true");
             $("#<%=hidOperate.ClientID %>").val("Condition");
             $('#<%=txtRoleCode.ClientID %>').val(null);                             
             $('#<%=txtRoleName.ClientID %>').val(null);
             $('#<%=ddlDeleted.ClientID %>').get(0).selectedIndex=0;
             $('#<%=ddlAcceptmsg.ClientID %>').get(0).selectedIndex=0;
             $("#<%=txtRoleCode.ClientID %>").attr("readonly",false);  
             $("#<%=ddlAcceptmsg.ClientID %>").attr("disabled",false);	
             $("#<%=ddlDeleted.ClientID %>").attr("disabled",false); 
             
              //移除只读  F3228823
              $("#<%=txtRoleCode.ClientID %>").removeAttr("readonly");
              $("#<%=txtRoleName.ClientID %>").removeAttr("readonly");
             return false;
            });
            
            
                 $('#<%=btnDisable.ClientID %>').click(function ()
               {
                  if($('#<%=txtRoleCode.ClientID %>').val()=='')
                  {
                  alert(Message.AtLastOneChoose);
                    return false;
                  }
                 var roleStatue=$("#<%=RoleStatue.ClientID %>").val();
                if (roleStatue=="Y")
                 {
                     return confirm(Message.ConfirmDisable);
                 }
                 return true;
             });
               
             $('#<%=btnEnable.ClientID %>').click(function ()
               {
                  if($('#<%=txtRoleCode.ClientID %>').val()=='')
                  {
                   alert(Message.AtLastOneChoose);
                    return false;
                  }
                   return true;
               });
               
            $('#<%=btnAdd.ClientID %>').click(function()
            { 
              $("#<%=btnCondition.ClientID %>").attr("disabled","true");
              $("#<%=btnQuery.ClientID %>").attr("disabled","true");
              $("#<%=btnAdd.ClientID %>").attr("disabled","true");
              $("#<%=btnModify.ClientID %>").attr("disabled","true");
              $("#<%=btnDelete.ClientID %>").attr("disabled","true");
              $("#<%=btnDisable.ClientID %>").attr("disabled","true");
              $("#<%=btnEnable.ClientID %>").attr("disabled","true");
              $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
              $('#<%=btnSave.ClientID %>').removeAttr("disabled");
              $(".input_textBox").removeClass("input_textBox"); 
              $(".ddl_hiden").removeClass("ddl_hiden");
              $("#<%=hidOperate.ClientID %>").val("Add");
              $('#<%=txtRoleCode.ClientID %>').val(null);               
              $('#<%=txtRoleName.ClientID %>').val(null);
              $('#<%=ddlDeleted.ClientID %>').get(0).selectedIndex=0;
              $('#<%=ddlAcceptmsg.ClientID %>').get(0).selectedIndex=0;
              $("#<%=txtRoleCode.ClientID %>").attr("readonly",false);   
              $("#<%=ddlAcceptmsg.ClientID %>").attr("disabled",false);	
              $("#<%=ddlDeleted.ClientID %>").attr("disabled",false); 
               //移除只读  F3228823
              $("#<%=txtRoleCode.ClientID %>").removeAttr("readonly");
              $("#<%=txtRoleName.ClientID %>").removeAttr("readonly");
               return false;
           });
            
            $('#<%=btnModify.ClientID %>').click(function()
            {
            
            if(  $("#<%=hidModify.ClientID %>").val()=='Y')
            {
              $("#<%=btnCondition.ClientID %>").attr("disabled","true");
              $("#<%=btnQuery.ClientID %>").attr("disabled","true");
              $("#<%=btnAdd.ClientID %>").attr("disabled","true");
              $("#<%=btnModify.ClientID %>").attr("disabled","true");
              $("#<%=btnDelete.ClientID %>").attr("disabled","true");
              $("#<%=btnDisable.ClientID %>").attr("disabled","true");
              $("#<%=btnEnable.ClientID %>").attr("disabled","true");
              $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
              $('#<%=btnSave.ClientID %>').removeAttr("disabled");
              $(".input_textBox").removeClass("input_textBox"); 
              $(".ddl_hiden").removeClass("ddl_hiden");
              $("#<%=hidOperate.ClientID %>").val("Modify");
               //移除只读  F3228823
//              $("#<%=txtRoleCode.ClientID %>").removeAttr("readonly");
              $("#<%=txtRoleName.ClientID %>").removeAttr("readonly");
              }
              else
              {
              alert(Message.AtLastOneChoose);
              }
               return false;
              
            });
            
                        $('#<%=btnDelete.ClientID %>').click(function()
            {
                  if  ($('#<%=txtRoleCode.ClientID %>').val())
                  {
                       return confirm(Message.RoleDeleteConfirm);
                  }
                  else
                  {
                      alert(Message.RoleDeleteCheck);
                      return false;
                  }
            });
       });
       
       
       //頁面初次加載時,頁面文本框應當為只讀   F3228823
       function Load()
       {  
       
          //添加只读 F3228823
          $("#<%=txtRoleCode.ClientID %>").attr("readonly","true");
          $("#<%=txtRoleName.ClientID %>").attr("readonly","true");
          
       }
       

</script>

<body onload="return Load()">
    <div style="width: 100%;">
        <form id="form2" runat="server">
        <asp:HiddenField ID="hidModify" runat="server" Value="N" />
        <asp:HiddenField ID="RoleStatue" runat="server" Value="N" />
        <asp:HiddenField ID="ModuleCode" runat="server" Value="N" />
        <table width="100%">
            <tr>
                <td width="55%" valign="top">
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
                                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                    </div>
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
                                                    <asp:Panel ID="pnlContent" runat="server">
                                                        <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                                        <tr align="left">
                                                            <td class="td_label" width="20%">
                                                                &nbsp;
                                                                <asp:Label ID="lblRoleRoleCode" runat="server" ForeColor="Blue">RoleCode</asp:Label>
                                                            </td>
                                                            <td class="td_input" width="30%">
                                                                <asp:TextBox ID="txtRoleCode" CssClass="input_textBox" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td class="td_label" width="20%">
                                                                &nbsp;
                                                                <asp:Label ID="lblRoleName" runat="server" ForeColor="Blue">RoleName</asp:Label>
                                                            </td>
                                                            <td class="td_input" width="30%">
                                                                <asp:TextBox ID="txtRoleName" CssClass="input_textBox" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_label" width="20%">
                                                                <asp:Label ID="lblDelete" runat="server" CssClass="ddl_hiden">Delete</asp:Label>
                                                            </td>
                                                            <td class="td_input" width="30%">
                                                                <asp:DropDownList runat="server" ID="ddlDeleted" CssClass="ddl_hiden" Width="100%">
                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="N">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="Y"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="td_label" width="20%">
                                                                <asp:Label ID="lblAcceptmsg" runat="server" CssClass="ddl_hiden">Acceptmsg</asp:Label>
                                                            </td>
                                                            <td class="td_input" width="30%">
                                                                <asp:DropDownList runat="server" ID="ddlAcceptmsg" CssClass="ddl_hiden" Width="100%">
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="Y">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="N">
                                                                    </asp:ListItem>
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
                                                                                CssClass="button_1" ToolTip="Authority Code:Cancel" OnClick="btnCancel_Click"></asp:Button>
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
                                            ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                            Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                            ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                        </ess:AspNetPager>
                                    </div>
                                </td>
                                <td style="width: 22px;" id="td_show_2">
                                    <div id="img_grid">
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

                                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                                        <igtbl:UltraWebGrid ID="UltraWebGridRole" runat="server" Width="100%" Height="100%">
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
                                                <igtbl:UltraGridBand BaseTableName="gds_sc_role" Key="gds_sc_role">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn BaseColumnName="rolecode" Key="rolecode" IsBound="false" Width="40%">
                                                            <Header Caption="<%$Resources:ControlText,gvHeaderRoleCode %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ROLENAME" Key="ROLENAME" IsBound="false" Width="60%">
                                                            <Header Caption="<%$Resources:ControlText,gvHeaderRoleName %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="DELETED" Key="DELETED" IsBound="false" Hidden="true">
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ACCEPTMSG" Key="ACCEPTMSG" IsBound="false"
                                                            Hidden="true">
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
                </td>
                <td width="45%" valign="top">
                    <iframe name="Authority" src="AuthorityForm.aspx" width="100%" height="500px" scrolling="auto"
                        id="Iframe1" frameborder="0"></iframe>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
