<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolesCode.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.RolesCode" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>群組設定</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .a
        {
            border: 0;
        }
        .b
        {
            display: none;
        }
        #divedit
        {
            float: left;
        }
        #diviframe
        {
            float: left;
        }
    </style>

    <script type="text/javascript">
    $(function(){
    $("#btnCancel,#btnSave").attr("disabled","disabled");
    $("#txtRolesCode,#txtRolesName,#txtAllowFlag,#txtAcceptMsg").addClass("a").attr("readonly","true");
    $("#ddlAllowFlag,#ddlAcceptMsg").addClass("b");
    $("#<%=hidOperate.ClientID %>").val(null);
    
    $("#<%=btnCondition.ClientID %>").click(function(){
    $("#btnCondition,#btnAdd,#btnModify,#btnDelete,#btnDisable,#btnEnable").attr("disabled","disabled");
    $("#btnCancel").removeAttr("disabled");
    $("#txtRolesCode,#txtRolesName,#txtAllowFlag,#txtAcceptMsg").removeAttr("readonly").removeClass("a").val(null);
    $("#<%=hidOperate.ClientID %>").val("Query");
    $("#ddlAllowFlag,#ddlAcceptMsg").val(null);
    return false;
    });
    
    $("#<%=btnQuery.ClientID %>").click(function(){
    $("#<%=hidOperate.ClientID %>").val(null);
    })
    
    $("#<%=btnCancel.ClientID %>").click(function(){
    $("#btnCondition,#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnDisable,#btnEnable").removeAttr("disabled")
    $("#btnCancel,#btnSave").attr("disabled","disabled");
    if($("#<%=hidOperate.ClientID %>").val()!="Modify")
    {
    $("#txtRolesCode,#txtRolesName,#txtAllowFlag,#txtAcceptMsg,#ProcessFlag").val(null);
    }
    $("#txtRolesCode,#txtRolesName,#txtAllowFlag,#txtAcceptMsg,#ProcessFlag").addClass("a").attr("readonly","true");
    $("#<%=hidOperate.ClientID %>").val(null);
    $("#txtAllowFlag,#txtAcceptMsg").removeClass("b");
    $("#ddlAllowFlag,#ddlAcceptMsg").addClass("b");
    return false;
    });
    
    $("#<%=btnAdd.ClientID %>").click(function(){
    $("#btnCondition,#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnDisable,#btnEnable").attr("disabled","disabled");
    $("#btnCancel,#btnSave").removeAttr("disabled");
    $("#txtRolesCode,#txtRolesName,#txtAllowFlag,#txtAcceptMsg").removeClass("a").removeAttr("readonly").val(null);
    $("#<%=hidOperate.ClientID %>").val("Add");
    $("#txtAllowFlag,#txtAcceptMsg").addClass("b");
    $("#ddlAllowFlag,#ddlAcceptMsg").removeClass("b");
    return false;
    });
    
    $("#<%=btnModify.ClientID %>").click(function(){
    var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
    if(rolesCode.length==0)
    {
    alert(Message.NoItemSelected);
    return false;
    }
    $("#btnCondition,#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnDisable,#btnEnable").attr("disabled","disabled");
    $("#btnCancel,#btnSave").removeAttr("disabled").removeClass("a");
    $("#txtRolesName,#txtAllowFlag,#txtAcceptMsg").removeClass("a").removeAttr("readonly");
    $("#<%=hidOperate.ClientID %>").val("Modify");
    $("#txtAllowFlag,#txtAcceptMsg").addClass("b");
    $("#ddlAllowFlag,#ddlAcceptMsg").removeClass("b");
    return false;
    });
    
    $("#<%=btnSave.ClientID %>").click(function() {
       var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
       var rolesName = $.trim($("#<%=txtRolesName.ClientID %>").val());
       var allowFlag = $.trim($("#<%=ddlAllowFlag.ClientID %>").val());
       var acceptMSG = $.trim($("#<%=ddlAcceptMsg.ClientID %>").val());
       if(rolesCode.length==0){alert(Message.RolesCodeNotNull); return false;};
       if(allowFlag.length==0){alert(Message.AllowFlagNotNull); return false;}
       if(acceptMSG.length==0){alert(Message.AcceptMSGNotNull); return false;}
                })
                
                
    $("#<%=txtRolesCode.ClientID %>").blur(function()
    {
        var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
        if($("#<%=hidOperate.ClientID %>").val()=="Add")
        {
        if ($.trim($("#<%=txtRolesCode.ClientID %>").val())) 
        {  
         var processFlag = "check";
         $.ajax({
                type: "post", url: "RolesCode.aspx",dataType: "text",
                data: { RolesCode: rolesCode,ProcessFlag: processFlag },
                success: function(msg) {
                if(msg=="Y"){alert(Message.RolesCodeIsExist);return false;}
                }
                });
        } 
        }
        })
    
   
        
    $("#<%=btnEnable.ClientID %>").click(function() {
    var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
    $("#<%=hidOperate.ClientID %>").val("Enable");
    var processFlag = $("#<%=hidOperate.ClientID %>").val();
    if(rolesCode.length==0)
    {
    $("#<%=hidOperate.ClientID %>").val(null);
    alert(Message.NoItemSelected);
    return false;
    }
    })
    
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
    
    function confirmDisable(){
    var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
    $("#<%=hidOperate.ClientID %>").val("Disable");
    var processFlag = $("#<%=hidOperate.ClientID %>").val();
    if(rolesCode.length==0)
    {
    $("#<%=hidOperate.ClientID %>").val(null);
    alert(Message.NoItemSelected);
    return false;
    }
    else
    {
//    alert($("#<%=Flag.ClientID %>").val());
    if($("#<%=Flag.ClientID %>").val()=="Y")
    {
     var flag=confirm(Message.RolesDisableConfirm);
     if(flag==false)
     {
     $("#<%=hidOperate.ClientID %>").val(null);
     }
     return flag;
    }            
    }
    }
    
    function RolesRoleIsExist()
    {
        $("#<%=Flag.ClientID %>").val(null);
        var rolesCode = $.trim($("#<%=txtRolesCode.ClientID %>").val());
        var processFlag ="Disable";
        $.ajax({
            type: "post", url: "RolesCode.aspx",dataType: "text",
            data: { RolesCode: rolesCode,ProcessFlag: processFlag },
            success: function(msg) {
            if(msg=="Y"){
              $("#<%=Flag.ClientID %>").val("Y");
            }
            else
            {
            $("#<%=Flag.ClientID %>").val("N");
            }
            }
            })
    }
    
    function AllowChange()
    {
    var obj1=document.all.ddlAllowFlag;
    var AllowFlag=obj1.options[obj1.selectedIndex].value;
    $("#<%=txtAllowFlag.ClientID %>").val(AllowFlag);
    }
    
    function AcceptChange()
    {
    var obj1=document.all.ddlAcceptMsg;
    var AcceptMsg=obj1.options[obj1.selectedIndex].value;
    $("#<%=txtAcceptMsg.ClientID %>").val(AcceptMsg);
    }
    
    function BeforeDelect()
    {
       var rolesCode = document.getElementById("txtRolesCode").value;
       if(rolesCode=="")
       {
         alert(Message.NoItemSelected);
         return false;
       }
       else
       {
         return confirm(Message.DeleteConfirm);
       }
     }
    
    function AfterSelectChange(tableName, itemName) 
			 { 
				var row = igtbl_getRowById(itemName);
				DisplayRowData(row);
				RolesRoleIsExist();
				return 0;
			}
			function UltraWebGridRoles_InitializeLayoutHandler(gridName)
			{
				var row = igtbl_getActiveRow(gridName);
				DisplayRowData(row);
			}
			function DisplayRowData(row)
			{
				if(igtbl_getElementById("hidOperate").value.length==0 && row != null)
				{
				    
				    var AllowFlag=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				    var AcceptMsg=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();;
					igtbl_getElementById("txtRolesCode").value=row.getCell(0).getValue();
					igtbl_getElementById("txtRolesName").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
					igtbl_getElementById("txtAllowFlag").value=AllowFlag;
					igtbl_getElementById("txtAcceptMsg").value=AcceptMsg;
					$("#<%=ddlAllowFlag.ClientID %>").val(AllowFlag);
		            $("#<%=ddlAcceptMsg.ClientID %>").val(AcceptMsg);
					document.frames("Authority").location.href="RolesRoleSet.aspx?RolesCode="+row.getCell(0).getValue();
				}
			}
    </script>

</head>
<body >
    <form id="MyForm" method="post" runat="server">
    <div>
    <table width="100%">
            <tr>
                <td width="55%" valign="top">
    <%--<div style="width: 55%; height:100%"  id="divedit">--%>
        <input id="Flag" type="hidden" name="Flag" runat="server" />
        <asp:HiddenField ID="hidOperate" runat="server" />
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
                                                <td width="24%">
                                                    <asp:Label ID="lblRolesCode" runat="server" ForeColor="Blue">RolesCode*</asp:Label>
                                                </td>
                                                <td width="29%">
                                                    <asp:TextBox ID="txtRolesCode"  runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="18%">
                                                    &nbsp;
                                                    <asp:Label ID="lblRolesName" runat="server">RolesName</asp:Label>
                                                </td>
                                                <td width="29%">
                                                    <asp:TextBox ID="txtRolesName"  runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td>
                                                    <asp:Label ID="lblAllowFlag" runat="server" ForeColor="Blue">AllowFlag*</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAllowFlag"  runat="server" Width="100%"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlAllowFlag" runat="server" AutoPostBack="false" onchange="return AllowChange();">
                                                        <asp:ListItem Value="" Text="<%$Resources:ControlText,ddlItemDefault %>"></asp:ListItem>
                                                        <asp:ListItem Value="N">N</asp:ListItem>
                                                        <asp:ListItem Value="Y">Y</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblRolesAcceptMSG" runat="server" ForeColor="Blue">AcceptMSG*</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAcceptMsg"  runat="server" Width="100%"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlAcceptMsg" runat="server" AutoPostBack="false" onchange="return AcceptChange();">
                                                        <asp:ListItem Value="" Text="<%$Resources:ControlText,ddlItemDefault %>"></asp:ListItem>
                                                        <asp:ListItem Value="N">N</asp:ListItem>
                                                        <asp:ListItem Value="Y">Y</asp:ListItem>
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
                                <td >
                                <asp:Panel ID="pnlShowPanel" runat="server">
                                    <asp:Button ID="btnCondition" runat="server" CssClass="button_1" >
                                    </asp:Button>
                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button_1">
                                    </asp:Button>
                                    <asp:Button ID="btnModify" runat="server" CssClass="button_1">
                                    </asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClientClick="return BeforeDelect();" OnClick="btnDelete_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button_1">
                                    </asp:Button>
                                    <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnDisable" runat="server" CssClass="button_1" OnClientClick="return confirmDisable();" OnClick="btnDisable_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnEnable" runat="server" CssClass="button_1" OnClick="btnEnable_Click">
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
                                ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" ShowMoreButtons="false" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px; cursor: hand;" id="td_show_2">
                        <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </td>
                </tr>
            </table>
            <div id="div_showdata" >
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*69/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridRoles" runat="server" Width="100%" Height="100%">
                                <displaylayout usefixedheaders="true" compactrendering="False" stationarymargins="Header"
                                    allowsortingdefault="Yes" rowheightdefault="20px" version="4.00" selecttyperowdefault="Single"
                                    headerclickactiondefault="SortSingle" bordercollapsedefault="Separate" allowcolsizingdefault="Free"
                                    allowrownumberingdefault="ByDataIsland" name="UltraWebGrid" tablelayout="Fixed"
                                    autogeneratecolumns="false" cellclickactiondefault="RowSelect">
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
                                </displaylayout>
                                <bands>
                                    <igtbl:UltraGridBand BaseTableName="gds_sc_roles" Key="gds_sc_roles">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="RolesCode" Key="RolesCode" IsBound="false"
                                                Width="25%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadRolesCode %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="RolesName" Key="RolesName" IsBound="false"
                                                Width="35%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadRolesName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AllowFlag" Key="AllowFlag" IsBound="false"
                                                Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadAllowFlag %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AcceptMSG" Key="AcceptMSG" IsBound="false"
                                                Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadAcceptMsg %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Deleted" Key="Deleted" IsBound="false" Hidden="true">
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </bands>
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
    <%--</div>--%>
    </td>
    <td width="45%" valign="top">
    <%--<div style="width: 45%" id="diviframe">--%>
        <iframe name="Authority" src="RolesRoleSet.aspx" width="100%" height="600px" scrolling="auto"
            frameborder="0"></iframe>
    <%--</div>--%>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
