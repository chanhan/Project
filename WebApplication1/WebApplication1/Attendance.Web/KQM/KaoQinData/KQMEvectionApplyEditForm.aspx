<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMEvectionApplyEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMEvectionApplyEditForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMLeaveApplyEdit</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
       function changeChoose(list) 
       {
          $("#<%=txtRemarkCar.ClientID %>").css("display", "none");$("#<%=lblOthers_.ClientID %>").css("display", "none"); 
          for (var i=0;i<5;i++)
          {
            if(i==0||i==4)
            {
                var item=document.getElementById(list.id+'_'+i);
                if(i==0)
                {
                    if (item.checked) {$("#<%=lblOthers_.ClientID %>").css("display", "");$("#<%=lblOthers_.ClientID %>").text("司機"); $("#<%=txtRemarkCar.ClientID %>").css("display", "");$("#<%=txtRemarkCar.ClientID %>").val("");   }
                }
                if(i==4)
                {
                    var item=document.getElementById(list.id+'_'+i);
                    if (item.checked) {$("#<%=lblOthers_.ClientID %>").css("display", ""); $("#<%=lblOthers_.ClientID %>").text("其他");$("#<%=txtRemarkCar.ClientID %>").css("display", "");$("#<%=txtRemarkCar.ClientID %>").val("");   }
                }
            }
            else
            {continue;}
          }
       }
       function load()
       {
         if(document.getElementById("ProcessFlag").value=="Modify"||document.getElementById("ProcessFlag").value=="")
         {
            var EvectionBy=$("input:radio[name=rdoEvectionBy]:checked").val();
            if(EvectionBy==1)
            {
                $("#<%=lblOthers_.ClientID %>").css("display", "");$("#<%=lblOthers_.ClientID %>").text("司機"); $("#<%=txtRemarkCar.ClientID %>").css("display", "");
            }
            else if(EvectionBy==5)
            {
               $("#<%=lblOthers_.ClientID %>").css("display", ""); $("#<%=lblOthers_.ClientID %>").text("其他");$("#<%=txtRemarkCar.ClientID %>").css("display", "");
            }
            else
            {
                $("#<%=txtRemarkCar.ClientID %>").css("display", "none");$("#<%=lblOthers_.ClientID %>").css("display", "none"); 
            }
         }
       }
        function GetEmp()
		{
            var employeeNo=document.getElementById("txtEmployeeNo").value;
            if(employeeNo.length>0&&document.getElementById("ProcessFlag").value=="Add")
            {docallback(employeeNo);return true;}
            else
           {return false;}
		}		
        function docallback(employeeNo)
        {
          if (employeeNo!="")
            {
                var bFlag=true;
                var result=0;
                $.ajax({ type:"post",url:"KQMEvectionApplyEditForm.aspx",dataType:"json",data:{EmployeeNo:employeeNo},
                            async:false, 
                             success:function(msg) 
                             {
                                    if (msg!=null)
                                       {       
　　　　                                   $(msg).each(function(i){
　　　　                                    var EmployeeNo=msg[i].WorkNo==null?"&nbsp;":msg[i].WorkNo;
　　                                        var LocalName=msg[i].LocalName==null?"&nbsp":msg[i].LocalName;
　　                                        var Sex=msg[i].Sex==null?"&nbsp":msg[i].Sex;
　　                                        var DPcode=msg[i].Dname==null?"&nbsp":msg[i].Dname;
                                            document.getElementById("txtEmployeeNo").value=EmployeeNo==null?"":EmployeeNo;
　　                                        document.getElementById("txtLocalName").value=LocalName==null?"":LocalName;
　　                                        document.getElementById("txtSex").value=Sex==null?"":Sex;
　　                                        document.getElementById("txtDepName").value=DPcode==null?"":DPcode;
　　                                        result=0;
                                        });
                                      }
                                      else
                                      {bFlag=false; result=1; }             
                                 }
                               });
                              if(result==1)
                              {
                                  if(!bFlag)
                                  {
                                    document.getElementById("txtEmployeeNo").value="";
                                    document.getElementById("txtLocalName").value="";
                                    document.getElementById("txtDPcode").value="";
                                    document.getElementById("txtSex").value="";
                                    alert(Message.PersonInfoNotExist);
                                    return false;
                                  }
                              }
                              else{return true;}
              }
        }
        function Return()
        {
//            window.parent.document.all.topTable.style.display="";
//            window.parent.document.all.PanelData_Div.style.display="";
//            window.parent.document.all.div_2.style.display="";
//            window.parent.document.all.divEdit.style.display="none";
            window.parent.location.href=window.parent.location.href;
            return false;
        }
    $(function(){
        $("#tr_edit").toggle(
            function(){$("#div_1").hide(); $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){$("#div_1").show();$(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");}) 
       });
function  save_click()
{
     var EmployeeNo=$.trim($("#<%=txtEmployeeNo.ClientID %>").val());
     var EvectionReason=$.trim($("#<%=txtEvectionReason.ClientID %>").val());
     var EvectionType=$.trim($("#<%=ddlEvectionType.ClientID %>").val());
     var EvectionBy=$("input:radio[name=rdoEvectionBy]:checked").val();
     //var EvectionBy=$("#<%=rdoEvectionBy.ClientID %>").val();
     var EvectionTime=igedit_getById("txtEvectionTime").getValue();
     if(EmployeeNo.length>0)
     {
        if(EvectionReason.length>0)
        {
            if(EvectionType.length>0)
            {
                if(EvectionTime.length>0)
                {
                    if(EvectionBy.length>0)
                    {
                       if(EvectionType==0)
                       {
                          if(EvectionBy==1||EvectionBy==2||EvectionBy==5)
                          {
                             alert(Message.OnlySelectWalk);
                             return false;
                          }
                          else
                          {
                             return true;
                          }
                       }
                       else
                       {
                           if(EvectionBy==3||EvectionBy==4)
                           {
                             alert(Message.OnlySelectByCar);
                             return false;
                           }
                           else
                           {
                              return true;
                           }
                       }
                    }
                    else
                    {
                        var alertTxt=$("#<%=lblEvectionBy.ClientID %>").text()+Message.TextBoxNotNull;
		                alert(alertTxt);
		                return false;
                    } 
                }
                else
                {
                    var alertTxt=$("#<%=lblEvectionTime.ClientID %>").text()+Message.TextBoxNotNull;
		            alert(alertTxt);
		            return false;
                } 
            }
            else
            {
                var alertTxt=$("#<%=lblEvectionTypeApply.ClientID %>").text()+Message.TextBoxNotNull;
		        alert(alertTxt);
		        return false;
            }        
        }
        else
        {
            var alertTxt=$("#<%=lblEvectionReason.ClientID %>").text()+Message.TextBoxNotNull;
		    alert(alertTxt);
		    return false;
        }        
     }
     else
     {
        alert(Message.WorkNoNotNull);return false;
     }
     
}
function timeQty()
{
    var NEHours1,NEHours2,NEHour3;
    NEHours1= igedit_getById("txtReturnTime").getValue();
    NEHours2= igedit_getById("txtEvectionTime").getValue();	
    if(NEHours1<NEHours2)
    {alert(Message.ErrReturnTimeWrong);igedit_getById("txtReturnTime").setValue("");return false;}
    else{return true;}	    
}
// -->
    </script>

</head>
<body   onload="return load()">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <input id="HiddenEvectionType" type="hidden" name="HiddenEvectionType" runat="server">
    <input id="HiddenApplyType" type="hidden" name="HiddenApplyType" runat="server">
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server"><%--新增完資料保留工號--%>
    <input id="HiddenStatus" type="hidden" name="HiddenStatus" runat="server" />
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
                    <div>
                        <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_1">
        <table class="table_data_area" style="width: 100%">
            <tr style="width: 100%">
                <td>
                    <asp:Panel ID="pnlContent" runat="server">
                        <table class="table_data_area">
                            <tr class="tr_data_1">
                                <td style="width: 11%">
                                    &nbsp;
                                    <asp:Label ID="labelEmployeeNo" runat="server">EmployeeNo:</asp:Label>
                                </td>
                                <td style="width: 22%">
                                    <asp:TextBox ID="txtEmployeeNo" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                    <asp:TextBox ID="txtID" class="input_textBox_1" runat="server" Width="100%" Style="display: none;"></asp:TextBox>
                                </td>
                                <td style="width: 11%">
                                    &nbsp;
                                    <asp:Label ID="lblLocalName" runat="server">Name:</asp:Label>
                                </td>
                                <td style="width: 22%">
                                    <asp:TextBox ID="txtLocalName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 11%">
                                    &nbsp;
                                    <asp:Label ID="lblBillNo" runat="server">BillNo:</asp:Label>
                                </td>
                                <td style="width: 22%">
                                    <asp:TextBox ID="txtBillNo" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblSex" runat="server">Sex:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSex" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblDeptName" runat="server">Department:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepName" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionTypeApply" runat="server">JoinDate:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEvectionType" class="input_textBox_2" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionReason" runat="server">LevelCode:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEvectionReason" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionTime" runat="server">Manager:</asp:Label>
                                </td>
                                <td>
                                    <igtxt:WebDateTimeEdit ID="txtEvectionTime" class="input_textBox_1" runat="server"
                                        DataMode="DateOrDBNull" EditModeFormat="yyyy/MM/dd HH:mm">
                                    </igtxt:WebDateTimeEdit>
                                </td>
                                <%-- <td>
                                                <asp:TextBox ID="txtEvectionTime" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                            </td>--%>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionTel" runat="server">ComeYears:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEvectionTel" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td width="20%">
                                    &nbsp;
                                    <asp:Label ID="lblEvectionBy" runat="server">EvectionType:</asp:Label>
                                </td>
                                <td width="60%" colspan="3">
                                    <asp:RadioButtonList ID="rdoEvectionBy" onclick="changeChoose(this)" runat="server"
                                        RepeatDirection="Horizontal">
                                        <%-- <asp:ListItem Text="<%$Resources:ControlText,radioUserCar%>"  Value="1"></asp:ListItem>
                                        <asp:ListItem Text="<%$Resources:ControlText,radioOwnCar%>" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="<%$Resources:ControlText,radioWalk%>" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="<%$Resources:ControlText,radioBallCar%>" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="<%$Resources:ControlText,radioOthers%>" Value="5"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="10%">
                                    <%--<asp:Label ID="lblCarer" runat="server" Style="display: none"></asp:Label>--%>
                                    <asp:Label ID="lblOthers_" runat="server" Style="display: none"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarkCar" runat="server" Style="display: none"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionApplyAddress" runat="server"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtEvectionAddress" runat="server" class="input_textBox_2" Width="100%">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionObject" runat="server">EvectionDetail:</asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtEvectionObject" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionTask" runat="server">EvectionTask:</asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtEvectionTask" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblEvectionRoad" runat="server">EvectionDetail:</asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtEvectionRoad" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblReturnTime" runat="server">StartDate:</asp:Label>
                                </td>
                                <td>
                                    <igtxt:WebDateTimeEdit ID="txtReturnTime" runat="server" class="input_textBox_2"
                                        DataMode="DateOrDBNull" EditModeFormat="yyyy/MM/dd HH:mm" Width="100%">
                                        <ClientSideEvents ValueChange="timeQty" />
                                    </igtxt:WebDateTimeEdit>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblRealReturnTime" runat="server">StartDate:</asp:Label>
                                </td>
                                <td>
                                    <igtxt:WebDateTimeEdit ID="txtRealReturnTime" runat="server" class="input_textBox_2"
                                        EditModeFormat="yyyy/MM/dd HH:mm" Width="100%">
                                    </igtxt:WebDateTimeEdit>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Button ID="btnSave" runat="server" class="button_2" Text="Save" OnClientClick="return save_click();"
                        OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnReturn" class="button_2" runat="server" OnClientClick="return Return();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidOperate" runat="server" />
    </div>
    </form>

    <script type="text/javascript"><!-- 
            document.getElementById("txtBillNo").readOnly=true;
            document.getElementById("txtEmployeeNo").readOnly=true;
            document.getElementById("txtLocalName").readOnly=true;
            document.getElementById("txtDepName").readOnly=true;
            document.getElementById("txtSex").readOnly=true;
		if( document.getElementById("ProcessFlag").value=="Add")
		{
             document.getElementById("txtEmployeeNo").readOnly=false;
             document.getElementById("txtEmployeeNo").focus();
             document.getElementById("txtEmployeeNo").select();
    	}
    	else
    	{document.all("ddlEvectionType").focus();}
    	if ( document.getElementById("ProcessFlag").value=="Modify")
        {document.all("ddlEvectionType").value= document.getElementById("HiddenEvectionType").value;}
	--></script>

</body>
</html>
