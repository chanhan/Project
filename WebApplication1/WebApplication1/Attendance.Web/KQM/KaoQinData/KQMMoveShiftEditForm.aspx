<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMMoveShiftEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMMoveShiftEditForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>彈性調班編輯</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">
    function GetEmp()
		{
            var EmployeeNo=document.getElementById("txtEmployeeNo").value;
            if(EmployeeNo.length>0&&document.getElementById("ProcessFlag").value=="Add")
            {
                docallback();
            }
		}		
        function docallback()
        {       
               var empno=$.trim($("#<%=txtEmployeeNo.ClientID %>").val());
               var sqlDep="<%=GetSqlDep()%>";
                    if (empno!="")
                        {
                            $.ajax(
                                   {
                                     type:"post",url:"KQMMoveShiftEditForm.aspx",dataType:"json",data:{Empno:empno,SqlDep:sqlDep},  
                                     success:function(msg) 
                                        {
                                        if (msg.LocalName!=null)
                                        {
                                            $("#<%=txtEmployeeNo.ClientID %>").val(msg.WorkNo);
                                            $("#<%=txtName.ClientID %>").val(msg.LocalName);
                                             $("#<%=txtDPcode.ClientID %>").val(msg.DepName);
                                        }
                                        else
                                        {
                                        $("#<%=txtEmployeeNo.ClientID %>").val("");
                                        $("#<%=txtName.ClientID %>").val("");
                                        $("#<%=txtDPcode.ClientID %>").val("");
                                        alert(Message.EmpNotExist);
                                        }
                                        }
                                   });
                        }
                        return false;
            
        }
        
        function Save()
        {
          var alertText="";
          var empNo=$.trim($("#<%=txtEmployeeNo.ClientID %>").val()); 
          var workDate=$.trim($("#<%=txtWorkDate.ClientID %>").val()); 
          var timeQty=$.trim($("#<%=txtTimeQty.ClientID %>").val()); 
          var noWorkDate=$.trim($("#<%=txtNoWorkDate.ClientID %>").val()); 
          if (empNo.length==0)
          { alertText = $.trim($("#<%=lblEmployeeNo.ClientID%>").text()) + Message.TextBoxNotNull;  alert(alertText); return false; }
          else if (workDate.length==0) 
          {alertText = $.trim($("#<%=lblWorkDate.ClientID%>").text()) + Message.TextBoxNotNull;  alert(alertText); return false;}
          else if (timeQty.length==0) {alertText = $.trim($("#<%=lblTimeQty.ClientID%>").text()) + Message.TextBoxNotNull;  alert(alertText); return false;}
          else if (noWorkDate.length==0) {alertText = $.trim($("#<%=lblNoWorkDate.ClientID%>").text()) + Message.TextBoxNotNull;  alert(alertText); return false;}
          else {
          
           var BeginTime=new Date(igedit_getById("txtWorkSTime").getValue());
           var EndTime=new Date(igedit_getById("txtWorkETime").getValue());
           if (BeginTime!=""&& EndTime!="")
           { 
               if(EndTime.valueOf()-BeginTime.valueOf()<0)
               {
                  alert(Message.StartDateAndEndDate);
                  return false;
               }
           }
           var noBeginTime=new Date(igedit_getById("txtNoWorkSTime").getValue());
           var noEndTime=new Date(igedit_getById("txtNoWorkETime").getValue());
           if (noBeginTime!=""&& noEndTime!="")
           { 
               if(noEndTime.valueOf()-noBeginTime.valueOf()<0)
               {
                  alert(Message.StartDateAndEndDate);
                  return false;
               }
           }
          return true;}
        }
        
        
        function Return()
        {
            //window.parent.document.all.ButtonQuery.click();
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            return false;
        }
        
          $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#tr_show").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#tr_show").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenWorkDate" type="hidden" name="HiddenWorkDate" runat="server">
    <input id="HiddenNoWorkDate" type="hidden" name="HiddenNoWorkDate" runat="server">
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server"><%--新增完資料保留工號--%>
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center">
        <tr>
            <td valign="top">
                <table cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand" id="tr_edit">
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
                        </td>
                    </tr>
                    <tr id="tr_show">
                        <td>
                            <div id="div_1">
                                <table id="table_show" class="table_data_area">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                                <tr class="tr_data_1">
                                                    <td class="td_label_view" width="17%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server" IsMandatory="true" ForeColor="Blue" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox_1"
                                                            onblur="return GetEmp();"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblName" runat="server" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDeptcode" runat="server" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtDPcode" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td class="td_label" width="17%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWorkDate" runat="server" IsMandatory="true"  ForeColor="Blue"/>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtWorkDate" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWorkSTime" runat="server" />
                                                    </td>
                                                    <td width="20%">
                                                        <igtxt:webdatetimeedit id="txtWorkSTime" runat="server" editmodeformat="HH:mm" width="100%"
                                                            cssclass="input_textBox_2">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                    <td width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWorkETime" runat="server" />
                                                    </td>
                                                    <td width="20%">
                                                        <igtxt:webdatetimeedit id="txtWorkETime" runat="server" editmodeformat="HH:mm" width="100%"
                                                            cssclass="input_textBox_2">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td class="td_label" width="17%">
                                                        &nbsp;
                                                        <asp:Label ID="lblNoWorkDate" runat="server" IsMandatory="true" ForeColor="Blue" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtNoWorkDate" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblNoWorkSTime" runat="server" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <igtxt:webdatetimeedit id="txtNoWorkSTime" runat="server" editmodeformat="HH:mm"
                                                            width="100%" cssclass="input_textBox_1">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                    <td class="td_label" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblNoWorkETime" runat="server" />
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <igtxt:webdatetimeedit id="txtNoWorkETime" runat="server" editmodeformat="HH:mm"
                                                            width="100%" cssclass="input_textBox_1">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td width="17%">
                                                        &nbsp;
                                                        <asp:Label ID="lblTimeQty" runat="server" IsMandatory="true" ForeColor="Blue" />
                                                    </td>
                                                    <td width="20%">
                                                        <igtxt:webnumericedit id="txtTimeQty" runat="server" width="100%" cssclass="input_textBox_2"
                                                            horizontalalign="Left">
                                                        </igtxt:webnumericedit>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblRemark" runat="server" />
                                                    </td>
                                                    <td width="56%" colspan="3">
                                                        <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                                                OnClientClick="return Save();"></asp:Button>
                                                            <asp:Button ID="btnReturn" runat="server" CssClass="button_1" OnClientClick="return Return();">
                                                            </asp:Button>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
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
    </form>

    <script type="text/javascript"><!--

		
		if(window.parent.document.all.divEdit.style.display=="")
		{
            document.getElementById("txtEmployeeNo").readOnly=true;
            document.getElementById("txtName").readOnly=true;
            document.getElementById("txtDPcode").readOnly=true;       
            
		    if(document.getElementById("ProcessFlag").value=="Add")
		    {
                document.getElementById("txtEmployeeNo").readOnly=false;
                document.getElementById("txtEmployeeNo").focus();
                document.getElementById("txtEmployeeNo").select();
    	    }
    	    if(document.getElementById("ProcessFlag").value=="Modify")
		    {
                document.getElementById("txtWorkDate").readOnly=false;
                document.getElementById("txtNoWorkDate").readOnly=false;
    	    }    	   
        }
	--></script>

</body>
</html>
