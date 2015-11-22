<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonManage.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.PersonManage" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics2.WebUI.UltraWebTab.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PersonForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox_noborder
        {
            border: 0;
        }
        .img_hidden
        {
            display: none;
        }
        .notput_textBox_noborder
        {
            border: 0;
        }
        .img_show
        {
            visibility: visible;
        }
    </style>

    <script type="text/javascript">

    function condition_click()
     {
        $("#<%=hidOperate.ClientID %>").attr("value","condition")
        removeValue();
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         removeReadonly();
          addcolor();
         $("#<%=txtPersoncode.ClientID %>").removeAttr("readonly");
         $("#<%=txtIfAdmin.ClientID %>").removeAttr("readonly");
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
          $("#<%=btnSave.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnResetPWD.ClientID %>").attr("disabled","true");
         $("#<%=btnDisable.ClientID %>").attr("disabled","true");
         $("#<%=btnEnable.ClientID %>").attr("disabled","true");
         
         return false;
     } 
    
    function removeValue()
    {
      $(".input_textBox_noborder").attr("value","");
      $(".notput_textBox_noborder").attr("value","");
    }
    
    function removeReadonly()
    { 
      $(".input_textBox_noborder").removeClass("input_textBox_noborder");
      $(".notput_textBox_noborder").removeClass("notput_textBox_noborder");
      $(".img_hidden").show();
      $("#<%=txtCname.ClientID %>").removeAttr("readonly");
      $("#<%=txtTel.ClientID %>").removeAttr("readonly");
      $("#<%=txtMail.ClientID %>").removeAttr("readonly");
      $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
      $("#<%=btnSave.ClientID %>").removeAttr("disabled");
    }
    
    function  addcolor()
    {         
        $("#<%=txtPersoncode.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtDepName.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtCname.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtRoleCode.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtRolesName.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtDepLevel.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtLevelName.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtIfAdmin.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtAdminName.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtLanguage.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtLanguageName.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtDepCode.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtCompanyId.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtCompanyName.ClientID %>").css("background-color", "Cornsilk");
    }
    
    function add_click()
    {
         $("#<%=hidOperate.ClientID %>").attr("value","add")
         removeValue();
         removeReadonly();
          addcolor();
         $("#<%=txtPersoncode.ClientID %>").removeAttr("readonly");
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnResetPWD.ClientID %>").attr("disabled","true");
         $("#<%=btnDisable.ClientID %>").attr("disabled","true");
         $("#<%=btnEnable.ClientID %>").attr("disabled","true");
         
         return false;
    
    }
    
    function modify_click()
    {
    if($("#<%=txtPersoncode.ClientID %>").val()!="")
    {
         $("#<%=hidOperate.ClientID %>").attr("value","modify")
         removeReadonly();
          addcolor();
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnResetPWD.ClientID %>").attr("disabled","true");
         $("#<%=btnDisable.ClientID %>").attr("disabled","true");
         $("#<%=btnEnable.ClientID %>").attr("disabled","true");
         return false;
     }
     else
     {
      alert(Message.AtLastOneChoose);
      return false;
     }
    }
    
    
    function delete_click()
    {
        var PersonCode = $.trim($("#<%=txtPersoncode.ClientID%>").val());
        if (PersonCode=="")
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
    
    function save_click()
    {
        if ( $("#<%=txtPersoncode.ClientID %>").val()=="")
        {
            alert(Message.PersoncodeNotNull);
            return false;
        }
        else
        {
            if ( $("#<%=txtCname.ClientID %>").val()=="")
            {
                alert(Message.CnameNotNull);
                return false;
            }
            else
            {
                if ($("#<%=txtCompanyId.ClientID %>").val()=="")
                {
                    alert(Message.CompanyIdNotNull);
                    return false;
                }
                else
                {
                    if($("#<%=txtRoleCode.ClientID %>").val()=="")
                    {
                        alert(Message.RolesCodeNotNull);
                        return false;
                    }
                    else
                    {
                        if ($("#<%=txtDepCode.ClientID %>").val()=="")
                        {
                             alert(Message.DepCodeNotNull);
                             return false;
                        }
                        else
                        {
                            if($("#<%=txtLanguage.ClientID %>").val()=="")
                            {
                                alert(Message.LanguageNotNull);
                                return false;
                            }
                            else
                            {
                                if ($("#<%=txtIfAdmin.ClientID %>").val()=="")
                                {
                                     alert(Message.IfAdminNotNull);
                                     return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
    };
    
     function cancel_click()
     {
         window.location.href="PersonManage.aspx?ModuleCode=SYS03&SqlDep=true";

     } 
    

       function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           var personcode=$("#HiddenPersonCode").val();
           if (flag=="IfAdmin")
           {
           var url="PrivilegedSelector.aspx";
           }
           else if (flag=="CompanyId")
           {
           var url="CompanySelector.aspx?r=" + Math.random()+"&personcode=" + personcode;
           }
           else if (flag=="Language")
           {
           var url="LanguageSelector.aspx?r=" + Math.random();
           }
           else if (flag=="DepLevel")
           {
           var url="DepLevelSelector.aspx?r=" + Math.random();
           }
           else if (flag=="RoleCode")
           {
           var url="RolesSelector.aspx?r=" + Math.random();
           }
           else if (flag=="DepCode")
           {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r=" + Math.random();
           }
           else if (flag=="UserChangePwd")
           {
           var url="PrivilegedSelector.aspx";
           }
           else if (flag=="UserStartPwd")
           {
           var url="PrivilegedSelector.aspx";	    
           }
           else if (flag=="SalaryLogonEnable")
           {
           var url="PrivilegedSelector.aspx";
           }
           else if (flag=="IpControlFlag")
           {
           var url="PrivilegedSelector.aspx";
           }
           else if (flag=="EmpType")
           {
           var url="EmpTypeSelector.aspx";
           }
       
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;
       }
    
    function GrantFunction(rid) {
            window.showModalDialog("GrantRoleFunction.aspx?rid=" + rid + "&r=" + Math.random(), rid, "dialogHeight:520px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;");
            return false;
        }
    
    $(function(){
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03_a.gif");
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

            function vailPersoncode()
            {
            if ($("#<%=hidOperate.ClientID %>").val()=='add')
            {
                var empno;
                empno=$.trim($("#<%=txtPersoncode.ClientID %>").val());
                    if (empno!="")
                        {
                            $.ajax(
                                   {
                                     type:"post",url:"PersonManage.aspx",dataType:"json",data:{Empno:empno},  
                                     success:function(msg) 
                                        {
                                            if (msg.Num!=0)
                                            {
                                            $("#<%=txtPersoncode.ClientID %>").val("");
                                            alert(Message.DataExist);  
                                            }
                                            else
                                            {
                                            $("#<%=txtCname.ClientID %>").attr("value",msg.Cname);
                                            }
                                        }
                                   });
                        }
                        return false;
             }
             }


        function GetSession()
            {

                var empno;
                empno=$.trim($("#<%=txtPersoncode.ClientID %>").val());
                            $.ajax(
                                   {
                                     type:"post",url:"PersonManage.aspx",dataType:"text",data:{Personcode:empno},  
                                     success:function(msg) 
                                        {
                                        return false;
                                        }
                                   });

                        return false;
             }  
       function AfterSelectChange(gridName, id)
       {
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridPerson_InitializeLayoutHandler(gridName)
		{
		    var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
		    var tab = igtab_getTabById("UltraWebTab");
			var  textBoxPersoncode=tab.Tabs[0].findControl("txtPersoncode");
			var  textBoxCname=tab.Tabs[0].findControl("txtCname");
			var  textBoxCompanyID=tab.Tabs[0].findControl("txtCompanyId");
			var  textBoxDPcode=tab.Tabs[0].findControl("txtDepCode");
			var  textBoxRolcode=tab.Tabs[0].findControl("txtRoleCode");
			var  textBoxAdmini=tab.Tabs[0].findControl("txtIfAdmin");
			var  textBoxLanguage=tab.Tabs[0].findControl("txtLanguage");
			var  textBoxCompanyName=tab.Tabs[0].findControl("txtCompanyName");
			var  textBoxRolesName=tab.Tabs[0].findControl("txtRolesName");
			var  textBoxDepName=tab.Tabs[0].findControl("txtDepName");
			var  textBoxLanguageName=tab.Tabs[0].findControl("txtLanguageName");
			var  textBoxLevelCode=tab.Tabs[0].findControl("txtDepLevel");
			var  textBoxLevelName=tab.Tabs[0].findControl("txtLevelName");
			var  textBoxTel=tab.Tabs[0].findControl("txtTel");
			var  textBoxMail=tab.Tabs[0].findControl("txtMail");
		
			var  txtUserChangePwd=tab.Tabs[0].findControl("txtUserChangePwd");
			var  txtUserChangePwdName=tab.Tabs[0].findControl("txtUserChangePwdName");
			var  txtUserStartPwd=tab.Tabs[0].findControl("txtUserStartPwd");
			var  txtUserStartPwdName=tab.Tabs[0].findControl("txtUserStartPwdName");
			var  txtSalaryLogonEnable=tab.Tabs[0].findControl("txtSalaryLogonEnable");
			var  txtSalaryLogonEnableName=tab.Tabs[0].findControl("txtSalaryLogonEnableName");
			var  txtIpControlFlag=tab.Tabs[0].findControl("txtIpControlFlag");
			var  txtIpControlFlagName=tab.Tabs[0].findControl("txtIpControlFlagName");			
		    var  txtEmpType=tab.Tabs[0].findControl("txtEmpType");
			var  txtEmpTypeName=tab.Tabs[0].findControl("txtEmpTypeName");
			var  txtEmpNo=tab.Tabs[0].findControl("txtEmpNo");
			var  txtUserComment=tab.Tabs[0].findControl("txtUserComment");
			var  txtIpAddress=tab.Tabs[0].findControl("txtIpAddress");
			if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
			{
				textBoxPersoncode.value=row.getCell(0).getValue();
				textBoxCname.value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				textBoxCompanyID.value=row.getCell(2).getValue();
				textBoxCompanyName.value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
				textBoxDPcode.value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
				textBoxDepName.value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
				textBoxRolcode.value=row.getCell(6).getValue();
				textBoxRolesName.value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
				textBoxAdmini.value=row.getCell(8).getValue();
				textBoxLanguage.value=row.getCell(9).getValue();
				textBoxLanguageName.value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
				textBoxLevelCode.value=row.getCell(20).getValue()==null?"":row.getCell(20).getValue();
				textBoxLevelName.value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue();
				textBoxTel.value=row.getCell(15).getValue()==null?"":row.getCell(15).getValue();
				textBoxMail.value=row.getCell(16).getValue()==null?"":row.getCell(16).getValue();
				
				txtUserChangePwd.value=row.getCell(21).getValue()==null?"":row.getCell(21).getValue();
				txtUserChangePwdName.value=row.getCell(21).getValue()==null?"":(row.getCell(21).getValue()=="Y"?"Yes":"No");
				txtUserStartPwd.value=row.getCell(22).getValue()==null?"":row.getCell(22).getValue();
				txtUserStartPwdName.value=row.getCell(22).getValue()==null?"":(row.getCell(22).getValue()=="Y"?"Yes":"No");
				txtSalaryLogonEnable.value=row.getCell(23).getValue()==null?"":row.getCell(23).getValue();
				txtSalaryLogonEnableName.value=row.getCell(23).getValue()==null?"":(row.getCell(23).getValue()=="Y"?"Yes":"No");
				txtIpControlFlag.value=row.getCell(24).getValue()==null?"":row.getCell(24).getValue();
				txtIpControlFlagName.value=row.getCell(24).getValue()==null?"":(row.getCell(24).getValue()=="Y"?"Yes":"No");
				txtEmpTypeName.value=row.getCell(25).getValue()==null?"":row.getCell(25).getValue();
				txtEmpType.value=row.getCell(26).getValue()==null?"":row.getCell(26).getValue();
				txtEmpNo.value=row.getCell(29).getValue()==null?"":row.getCell(29).getValue();
				txtUserComment.value=row.getCell(27).getValue()==null?"":row.getCell(27).getValue();
				txtIpAddress.value=row.getCell(28).getValue()==null?"":row.getCell(28).getValue();
				
				
				
			}
		}
		function afterSelectEvent(owner, item, evt)
		{
			var personcode = owner.Tabs[0].findControl("txtPersoncode").value;
			var rolecode = owner.Tabs[0].findControl("txtRoleCode").value;

			if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==1)
			{
             owner.Tabs[1].setTargetUrl("<%=sAppPath%>CompanyAssignForm.aspx?PersonCode="+personcode);
			}
			else if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==2)
			{
                owner.Tabs[2].setTargetUrl("<%=sAppPath%>DepartmentAssignForm.aspx?PersonCode="+personcode+"&RoleCode="
                    +rolecode+"&ModuleCode="+document.getElementById("HiddenModuleCode").value);
			}
			else if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==3)
			{
                owner.Tabs[3].setTargetUrl("<%=sAppPath%>PersonLevel.aspx?PersonCode="+personcode+"&ModuleCode="+document.getElementById("HiddenModuleCode").value);
			}
			else if(item.getIndex()==4)
			{
                owner.Tabs[4].setTargetUrl("<%=sAppPath%>PowerChange.aspx?PersonCode="+personcode+"&ModuleCode="+document.getElementById("HiddenModuleCode").value);
			}
			else if(item.getIndex()==0)
			{
			    owner.setSelectedIndex(0);
			}
			else
			{
			    owner.setSelectedIndex(0);
			    alert(Message.AtLastOneChoose);
			}
		}
		
		--></script>

</head>
<body class="color_body">
    <form id="Form1" method="post" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="TextID" type="hidden" name="TextID" runat="server" />
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server" />
     <input id="HiddenPersonCode" type="hidden" name="HiddenPersonCode" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" height="88%" align="center">
        <tr>
            <td>
                <igtab:UltraWebTab ID="UltraWebTab" runat="server" ImageDirectory="/ig_common/images/"
                    Width="100%" Height="105%" BorderColor="#9CAFEF" BorderStyle="Solid" BorderWidth="0px">
                    <ClientSideEvents AfterSelectedTabChange="afterSelectEvent"></ClientSideEvents>
                    <DefaultTabStyle Width="130px" Height="26px" CssClass="tab_normal" BackgroundImage="../../CSS/Images_new/org_top01.gif">
                    </DefaultTabStyle>
                    <SelectedTabStyle CssClass="tab_current" Font-Bold="true">
                    </SelectedTabStyle>
                    <HoverTabStyle CssClass="tab_current">
                    </HoverTabStyle>
                    <RoundedImage LeftSideWidth="1" RightSideWidth="1" FillStyle="None"></RoundedImage>
                    <DefaultTabSeparatorStyle Width="4px">
                    </DefaultTabSeparatorStyle>
                    <Tabs>
                        <igtab:TabSeparator Text="" Tag="">
                            <Style Width="3px">
                                </Style>
                        </igtab:TabSeparator>
                        <igtab:Tab Key="Person" Text="<%$Resources:ControlText,TabHeadPerson%>" Tooltip="Authority Code:Person"
                            Tag="Person">
                            <ContentTemplate>
                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                    <tr>
                                        <td>
                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                            <tr style="width: 100%;" id="tr1">
                                                                <td style="width: 100%;" class="tr_title_center">
                                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                                        background-repeat: no-repeat;  width: 75px; text-align: center;
                                                                        font-size: 13px;">
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
                                                            <asp:HiddenField ID="hidOperate" runat="server" />
                                                            <asp:HiddenField ID="hidDeleted" runat="server" />
                                                            <div id="div_edit">
                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr class="tr_data_1">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblPersoncode" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <asp:TextBox ID="txtPersoncode" runat="server" Width="100%" CssClass="input_textBox_noborder"
                                                                                onblur="return vailPersoncode()"></asp:TextBox>
                                                                        </td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblCname" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <asp:TextBox ID="txtCname" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label">
                                                                            &nbsp;<asp:Label ID="lblCompanyID" runat="server" ForeColor="Blue">Company:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtCompanyId" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgCompanyId" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCompanyName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label">
                                                                             &nbsp;<asp:Label ID="lblRolecode" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtRoleCode" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgRolCode" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtRolesName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_1">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblDepcode" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgDePCode" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtDepName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label">
                                                                             &nbsp;<asp:Label ID="lblDepLevel" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtDepLevel" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgDepLevel" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtLevelName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_2">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblLanguage" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input" valign="top" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtLanguage" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgLanguage" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtLanguageName" runat="server" class="notput_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblIfadmin" runat="server" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtIfAdmin" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgAdmini" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtAdminName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_2">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblUserChangePwd" runat="server" >UserChangePwd</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" valign="top" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtUserChangePwd" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgUserChangePwd" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtUserChangePwdName" runat="server" class="notput_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblUserStartPwd" runat="server" >UserStartPwd</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtUserStartPwd" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgUserStartPwd" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtUserStartPwdName" runat="server" class="notput_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_2">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblSalaryLogonEnable" runat="server" >SalaryLogonEnable</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" valign="top" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtSalaryLogonEnable" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imegSalaryLogonEnable" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtSalaryLogonEnableName" runat="server" class="notput_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblIpControlFlag" runat="server" >IpControlFlag</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtIpControlFlag" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgIpControlFlag" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtIpControlFlagName" runat="server" class="notput_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_2">
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="lblEmpType" runat="server" >EmpType</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" valign="top" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="txtEmpType" runat="server" Width="100%" class="input_textBox_noborder"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="imgEmpType" runat="server" CssClass="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                                        </asp:Image>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtEmpTypeName" runat="server" class="notput_textBox_noborder" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label">
                                                                             &nbsp;<asp:Label ID="lblEmpNo" runat="server">EmpNo</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="txtEmpNo" runat="server" class="input_textBox_noborder" Width="100%"
                                                                                MaxLength="36"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_1">
                                                                        <td class="td_label" width="20%">
                                                                             &nbsp;<asp:Label ID="lalUserComment" runat="server">UserComment</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="txtUserComment" runat="server" Width="100%" class="input_textBox_noborder"
                                                                                MaxLength="36"></asp:TextBox>
                                                                        </td>
                                                                       <td class="td_label" width="20%">
                                                                             &nbsp;<asp:Label ID="lblIpAddress" runat="server">AdminPwd</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="txtIpAddress" runat="server" Width="100%" class="input_textBox_noborder"
                                                                                MaxLength="36"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tr_data_1">
                                                                        <td class="td_label" width="20%">
                                                                             &nbsp;<asp:Label ID="lblTel" runat="server">Tel</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="txtTel" runat="server" Width="100%" class="input_textBox_noborder"
                                                                                MaxLength="36"></asp:TextBox>
                                                                        </td>
                                                                        <td class="td_label">
                                                                            &nbsp;<asp:Label ID="lblMail" runat="server">Mail</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="txtMail" runat="server" class="input_textBox_noborder" Width="100%"
                                                                                MaxLength="36"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                   
                                                                    <tr>
                                                                        <td class="td_label" colspan="4">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                                                            <asp:Button ID="btnCondition" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                                                                CssClass="button_2" OnClientClick="return condition_click()"></asp:Button>
                                                                                            <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                                                CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                                                            <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                                                CssClass="button_2" OnClientClick="return add_click()"></asp:Button>
                                                                                            <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                                                CssClass="button_2" OnClientClick="return  modify_click()"></asp:Button>
                                                                                            <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                                                CssClass="button_2" OnClientClick="return  delete_click()" OnClick="btnDelete_Click">
                                                                                            </asp:Button>
                                                                                            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                                                                CssClass="button_2" OnClientClick="return  cancel_click()"></asp:Button>
                                                                                            <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                                                CssClass="button_2" OnClientClick="return  save_click()" OnClick="btnSave_Click">
                                                                                            </asp:Button>
                                                                                            <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                                                CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                                                            <asp:Button ID="btnResetPWD" runat="server" Text="<%$Resources:ControlText,btnResetPWD %>"
                                                                                                CssClass="button_2" OnClick="btnResetPWD_Click"></asp:Button>
                                                                                            <asp:Button ID="btnDisable" runat="server" Text="<%$Resources:ControlText,btnDisable %>"
                                                                                                CssClass="button_2" OnClick="btnDisable_Click"></asp:Button>
                                                                                            <asp:Button ID="btnEnable" runat="server" Text="<%$Resources:ControlText,btnEnable %>"
                                                                                                CssClass="button_2" OnClick="btnEnable_Click"></asp:Button>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
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
                                    <!--test-->
                                    <tr>
                                        <td>
                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%;" class="tr_title_center">
                                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                            <tr style="width: 100%;">
                                                                <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                                        font-size: 13px;">
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
                                                                            OnPageChanged="pager_PageChanged" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                                                            Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                                                            ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                                            ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
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

                                                                        <script type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*56/100+"'>");</script>

                                                                        <igtbl:UltraWebGrid ID="UltraWebGridPerson" runat="server" Width="100%" Height="100%"
                                                                            OnDataBound="UltraWebGrid_DataBound">
                                                                            <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridPerson" CompactRendering="False"
                                                                                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                                                AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                                                AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter"
                                                                                AutoGenerateColumns="false">
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
                                                                                <igtbl:UltraGridBand>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="PERSONCODE" IsBound="false" Key="PERSONCODE"
                                                                                            Width="70">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadPersonCode %>" Fixed="true">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="CNAME" IsBound="false" Key="CNAME" Width="60">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadCname%>" Fixed="true">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="COMPANYID" IsBound="false" Key="COMPANYID"
                                                                                            Hidden="true">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadCompanyID %>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="COMPANYNAME" IsBound="false" Key="COMPANYNAME"
                                                                                            Width="130">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadCompanyName%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="DEPCODE" IsBound="false" Key="DEPCODE" Hidden="true">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadDepCode%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" IsBound="false" Key="DEPNAME" Width="80">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadDepName%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="ROLECODE" IsBound="false" Key="ROLECODE" Hidden="true">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadRoleCode%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="RolesName" IsBound="false" Key="RolesName"
                                                                                            Width="80">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadRoleName%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="IFADMIN" IsBound="false" Key="IFADMIN" Width="60">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadAdmin%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LANGUAGE" IsBound="false" Key="LANGUAGE" Hidden="true">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLanguage%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LANGUAGENAME" IsBound="false" Key="LANGUAGENAME"
                                                                                            Width="60">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLanguageName%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LevelCode" IsBound="false" Key="LevelCode"
                                                                                            Hidden="true">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLevelCode%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LevelName" IsBound="false" Key="LevelName"
                                                                                            Width="60">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLevelName%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LOGINTIMES" IsBound="false" Key="LOGINTIMES"
                                                                                            Width="60">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLoginTimes%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="LOGINTIME" IsBound="false" Key="LOGINTIME"
                                                                                            Width="120">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLoginTime%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="Tel" IsBound="false" Key="Tel" Width="80">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadTel%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="Mail" IsBound="false" Key="Mail" Width="120">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMail%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="DELETED" IsBound="false" Key="DELETED" Width="80">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadDeleted%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                                                            Width="70">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadModifier%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="update_Date" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadModifyDate%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="deplevel" Key="update_Date" IsBound="false"
                                                                                            Width="110" Hidden="true">
                                                                                            <Header Caption="">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="user_change_pwd" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadUserChangePwd%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="user_start_pwd" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadUserStartPwd%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="salary_logon_enabled" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadSalaryLogonEnabled%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="ip_control_flag" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadIpControlFlag%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="emp_type_name" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadEmpType%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="emp_type" Key="update_Date" IsBound="false"
                                                                                            Width="110" Hidden="true">
                                                                                            <Header Caption="">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="user_comment" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadUserComment%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="IpAddress" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadIpAddress%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                    </Columns>
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="emp_no" Key="update_Date" IsBound="false"
                                                                                            Width="110">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadEmpNo%>">
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
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </igtab:Tab>
                        <igtab:Tab Key="Company" Text="<%$Resources:ControlText,TabHeadCompany%>" Tooltip="Authority Code:Company"
                            Tag="Company">
                            <ContentPane TargetUrl="about:blank">
                            </ContentPane>
                        </igtab:Tab>
                        <igtab:Tab Key="Dept" Text="<%$Resources:ControlText,TabHeadDept%>" Tooltip="Authority Code:Department"
                            Tag="Department">
                            <ContentPane TargetUrl="about:blank">
                            </ContentPane>
                        </igtab:Tab>
                        <igtab:Tab Key="DepLevel" Text="<%$Resources:ControlText,TabHeadDepLevel%>" Tooltip="Authority Code:Level"
                            Tag="Level">
                            <ContentPane TargetUrl="about:blank">
                            </ContentPane>
                        </igtab:Tab>
                        <igtab:Tab Key="PowerChange" Text="<%$Resources:ControlText,TabHeadPowerChange%>"
                            Tooltip="Authority Code:PowerChange" Tag="PowerChange">
                            <ContentPane TargetUrl="about:blank">
                            </ContentPane>
                        </igtab:Tab>
                    </Tabs>
                </igtab:UltraWebTab>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
