<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PCMLeaveApplyEditForm_LHZB.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.PCM.PCMLeaveApplyEditForm_LHZB" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script>
        $(function(){
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
    })
//    		function CheckSendNotes()
//		{
//            var LeaveNoAudit=document.getElementById("hidLeaveNoAudit").value; 
//            if(LeaveNoAudit=="N")
//            {  
//	            if(confirm(Message.SaveingAndAudit))
//	            {
//                    document.getElementById("hidSendNotes").value="Y";
//		        }
//		        else
//		        {
//                    document.getElementById("hidSendNotes").value="N";
//		        }
//		    }
//		    return true;
//		}
    		function GetDeputy()
		{
                      var ProxyWorkNo=document.getElementById("txtProxyWorkNo").value;
            if(ProxyWorkNo.length==0&&(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify"))
            {
                document.getElementById("txtProxy").value="";
                document.getElementById("txtProxyNotes").value="";
            }
            if(ProxyWorkNo.length>0&&(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify"))
            {
          $.ajax({
                                 type: "POST", url: "PCMLeaveApplyEditForm_LHZB.aspx", data: {'workno': ProxyWorkNo,'flag': 'proxy'},dataType: "json",
              success: function(item) {
	                if(item.WorkNo==null)
              {
               document.getElementById("txtProxyNotes").readOnly=true;
              alert(Message.EmpBasicInfoNotExist);
              }
            else
            {
                   document.getElementById("txtProxyWorkNo").value=item.WorkNo==null?"":item.WorkNo;
                   document.getElementById("txtProxy").value=item.LocalName==null?"":item.LocalName;
                   document.getElementById("txtProxyNotes").value=item.Notes==null?"":item.Notes;
                   document.getElementById("hidProxyFlag").value=item.Flag==null?"":item.Flag;

                   if(document.getElementById("txtProxyNotes").value.length>5)
                   {
                      document.getElementById("txtProxyNotes").readOnly=true;
                   }
                   else
                   {
                      document.getElementById("txtProxyNotes").readOnly=false;
                   }
           }
         }
      });
		}
}
              function validateCNDate(strValue) 
           { 
             var theStr=strValue.replace("-","/");
             var pattern = /^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
             return pattern.exec(theStr);
         }
        function GetEmp()               
    {
         setTimeout('__doPostBack(\'txtEmployeeNo\',\'\')', 0);
    }
    function GetEmpDataValue(ReturnValueBoxName,ReturnTextBoxName,ReturnNotesBoxName)
    {
        var windowWidth=600,windowHeight=400;
        var X=(screen.availWidth-windowWidth)/2;
        var Y=(screen.availHeight-windowHeight)/2;
        var Revalue=window.showModalDialog("PCMEmpDataPickForm.aspx?r="+ Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
        if(Revalue!=undefined)
        {
            var arrValue=Revalue.split(";");
		    document.all(ReturnValueBoxName).value=arrValue[0];
		    if(arrValue.length>1)
		    {
		        document.all(ReturnTextBoxName).innerText=arrValue[1];
		    }
		    else
		    {
		        document.all(ReturnTextBoxName).innerText="";
		        document.all(ReturnNotesBoxName).innerText="";
		    }
		    if(arrValue.length>2)
		    {
		        document.all(ReturnNotesBoxName).innerText=arrValue[2];
		    }
		    else
		    {
		        document.all(ReturnNotesBoxName).innerText="";
		    }
        }
    } 	
               function ConsortIsMandatory(ConsortInFoxconn)
       {
            var color=ConsortInFoxconn=="Y"?"blue":"black";
            var marryFlag  =  document.getElementById("hidMarryFlag").value;   
            var mandatoryChar=ConsortInFoxconn=="Y"?marryFlag:"";
            document.getElementById("lblUserLabelConsortInFoxconn").style.color = color;   
            document.getElementById("lblUserLabelConsortInFoxconn").innerText= Message.ConsortIn+" "+mandatoryChar
            document.getElementById("lblUserLabelConsortWorkNo").style.color = color;
            document.getElementById("lblUserLabelConsortWorkNo").innerText=Message.ConsortWorkNo+" "+mandatoryChar
            document.getElementById("lblUserLabelConsortWorkName").style.color = color;
            document.getElementById("lblUserLabelConsortWorkName").innerText=Message.ConsortName+" "+mandatoryChar
            document.getElementById("lblUserLabelConsortLevelCode").style.color = color;
            document.getElementById("lblUserLabelConsortLevelCode").innerText=Message.ConsortCode+" "+mandatoryChar
            document.getElementById("lblUserLabelConsortDepCode").style.color = color;
            document.getElementById("lblUserLabelConsortDepCode").innerText=Message.ConsortBg+" "+mandatoryChar
            
       }
            function Return()
        {
            window.parent.document.all.topTable.style.display="";     
            window.parent.document.all.tr_show.style.display="";                             
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.div_select2.style.display="";                             
            return false;
        }      
        	     function ConsortIsShow(LVTypeCode)
       {
       //是否顯示配偶資料             
            if(LVTypeCode=="J") //婚假
            {
                document.getElementById("divConsort").style.display="";
                ConsortIsMandatory("Y");
            }
            else
            {                
                document.getElementById("divConsort").style.display="none";
            }
       }  
            function GetLVTotal(){    
        var sStartDate=document.getElementById("txtStartDate").value;
        var sEndDate=document.getElementById("txtEndDate").value;
        var sStartTime=document.getElementById("txtStartTime").value;
        var sEndTime=document.getElementById("txtEndTime").value;
        var sLVTypeCode=document.getElementById("hidLVTypeCode").value;
        var sWorkNo=document.getElementById("txtEmployeeNo").value;
        var processFlag=document.getElementById("ProcessFlag").value;      
        var status=document.getElementById("hidStatus").value;  
        if(sStartDate!=""||sEndDate!=""||sStartTime!=""||sEndTime!="")
         {
            if(sWorkNo.length<3&&processFlag=="Add")
             {
               alert( Message.InputWorkNoFirst);
                return;       
             }
         }  
        if((processFlag=="Modify"&&(status=="0"||status=="3"))||processFlag=="Add")
        {
            if(sWorkNo!=""&&sStartDate!=""&&sEndDate!=""&&sStartTime!=""&&sEndTime!="")
            {
                if(!validateCNDate(sStartDate)||!validateCNDate(sEndDate))
		        {		       
		           return;		       
		        }        
               	             $.ajax({
                                 type: "post", url: "PCMLeaveApplyEditForm_LHZB.aspx", dataType: "text", data: {workno: sWorkNo,startDate:sStartDate+" "+sStartTime,endDate:sEndDate+" "+sEndTime,typecode:sLVTypeCode,flag: 'LVTotal'},
                                 success: function(msg) {
                                                                                        if(msg !="")
                                                                                   {
                                                                                      igedit_getById("txtLVTotal").setValue(msg);
                                                                                      document.getElementById("txtLVTotalDays").value=parseFloat(msg)/8;
                                                                                   }
                                                                                else
                                                                                  { 
                                                                                 igedit_getById("txtLVTotal").setValue("0");
                                                                                  document.getElementById("txtLVTotalDays").value="0";
                                                                                }
                                                     }
                           });
            }
            if(sWorkNo!=""&&sStartDate!=""&&validateCNDate(sStartDate))
            {
                   		             $.ajax({
                                 type: "post", url: "PCMLeaveApplyEditForm_LHZB.aspx", dataType: "text", data: {workno: sWorkNo,startDate:sStartDate,flag: 'ShiftDeac'},
                                 success: function(msg) {
                                                                                  if(msg!=null)
                                                          {
                                                              if(msg!="")
                                                              {
                                                                document.getElementById("lblShiftDesc").innerText = Message.otm_nowshiftno+msg; 
                                                              }
                                                              else
                                                              {
                                                                document.getElementById("lblShiftDesc").innerText = Message.otm_exception_errorshiftno_1;
                                                              }
                                                          }
                                          }
                           });
            }      
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="hidBillNo" type="hidden" name="hidBillNo" runat="server" />
    <input id="hidLVTypeCode" type="hidden" name="hidLVTypeCode" runat="server" />
    <input id="hidApplyType" type="hidden" name="hidApplyType" runat="server" />
    <input id="hidSave" type="hidden" name="hidSave" runat="server" /><%--新增完資料保留工號--%>
    <input id="hidStatus" type="hidden" name="hidStatus" runat="server" />
    <input id="hidMarryFlag" type="hidden" name="hidMarryFlag" runat="server" />
    <input id="hidProxyFlag" type="hidden" name="hidProxyFlag" runat="server" />
    <input id="hidSendNotes" type="hidden" name="hidSendNotes" runat="server" />
    <input id="hidManagerCode" type="hidden" name="hidManagerCode" runat="server" />
    <input id="hidLevelCode" type="hidden" name="hidLevelCode" runat="server" />
    <input id="hidDCode" type="hidden" name="hidDCode" runat="server" />
    <input id="hidLeaveNoAudit" type="hidden" name="hidLeaveNoAudit" runat="server" />
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
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
                                            <tr>
                                                <td class="td_label_view" style="width: 13%">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmployeeNo" runat="server">EmployeeNo:</asp:Label>
                                                </td>
                                                <td class="td_input" style="width: 20%">
                                                    <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view" style="width: 13%">
                                                    &nbsp;
                                                    <asp:Label ID="lblName" runat="server">Name:</asp:Label>
                                                </td>
                                                <td class="td_input" style="width: 20%">
                                                    <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view" style="width: 13%">
                                                    &nbsp;
                                                    <asp:Label ID="lblBillNoBillNo" runat="server">BillNo:</asp:Label>
                                                </td>
                                                <td class="td_input" style="width: 20%">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label_view">
                                                    &nbsp;
                                                    <asp:Label ID="lblSex" runat="server">Sex:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtSex" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view" width="13%">
                                                    &nbsp;
                                                    <asp:Label ID="lbllblDepName" runat="server">Department:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtDPcode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view">
                                                    &nbsp;
                                                    <asp:Label ID="lbllblJoinDate" runat="server">JoinDate:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtJoinDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label_view">
                                                    &nbsp;
                                                    <asp:Label ID="lblLevelCode" runat="server">LevelCode:</asp:Label>
                                                </td>
                                                <td class="td_input" style="width: 11%">
                                                    <asp:TextBox ID="txtLevelCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view">
                                                    &nbsp;
                                                    <asp:Label ID="lblManager" runat="server">Manager:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtManager" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label_view" width="13%">
                                                    &nbsp;
                                                    <asp:Label ID="lblComeYears" runat="server">ComeYears:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <igtxt:WebNumericEdit ID="txtComeYears" runat="server" CssClass="input_textBox" Width="100%"
                                                        HorizontalAlign="Left">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblLVLVTypeCode" runat="server">LVTypeCode:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlLVTypeCode" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lbllblStartDate" runat="server">StartDate:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="input_textBox" Width="100%"
                                                                    onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <igtxt:WebDateTimeEdit ID="txtStartTime" class="input_textBox_1" runat="server" DataMode="DateOrDBNull"
                                                                    Width="40px" EditModeFormat="HH:mm">
                                                                    <ClientSideEvents ValueChange="GetLVTotal" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lbllblEndDate" runat="server">EndDate:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="input_textBox" Width="100%"
                                                                    onkeydown="if(event.keyCode==13) event.keyCode=9"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <igtxt:WebDateTimeEdit ID="txtEndTime" class="input_textBox_1" runat="server" DataMode="DateOrDBNull"
                                                                    Width="40px" EditModeFormat="HH:mm">
                                                                    <ClientSideEvents ValueChange="GetLVTotal" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblApplyType" runat="server">ApplyType:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:DropDownList ID="ddlApplyType" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblReason" runat="server">Reason:</asp:Label>
                                                </td>
                                                <td class="td_input" colspan="3">
                                                    <asp:TextBox ID="txtReason" runat="server" Width="100%" CssClass="input_textBox">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblProxyWorkNo" runat="server">ProxyWorkNo:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtProxyWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="ImageWorkNo" runat="server" ImageUrl="~/CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblProxyName" runat="server">Proxy:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtProxy" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblLVTotal" runat="server">LVTotal(H):</asp:Label>
                                                </td>
                                                <td class="td_input" style="height: 22px">
                                                    <igtxt:WebNumericEdit ID="txtLVTotal" runat="server" Width="100%" CssClass="input_textBox"
                                                        HorizontalAlign="Left" MinValue="0">
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblProxyNotes" runat="server">DeputyNotes:</asp:Label>
                                                </td>
                                                <td class="td_input" colspan="3">
                                                    <asp:TextBox ID="txtProxyNotes" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblLVTotalDays" runat="server">LVTotalDays:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <asp:TextBox ID="txtLVTotalDays" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmergencyContactPerson" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmergencyContactPerson" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmergencyTelephone" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmergencyTelephone" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                                </td>
                                                <td class="td_input" colspan="5">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div id="divConsort" runat="server">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td class="td_label" style="width: 13%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblUserLabelConsortInFoxconn" runat="server" />
                                                                </td>
                                                                <td class="td_input" style="width: 20%">
                                                                    <asp:DropDownList ID="ddlConsortInFoxonn" runat="server" Width="100%">
                                                                        <asp:ListItem>Y</asp:ListItem>
                                                                        <asp:ListItem>N</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="td_label" style="width: 13%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblUserLabelConsortWorkNo" runat="server" />
                                                                </td>
                                                                <td class="td_input" style="width: 20%">
                                                                    <asp:TextBox ID="txtConsortWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                                <td class="td_label" style="width: 13%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblUserLabelConsortWorkName" runat="server" />
                                                                </td>
                                                                <td class="td_input" style="width: 20%">
                                                                    <asp:TextBox ID="txtConsortName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td_label" style="width: 13%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblUserLabelConsortLevelCode" runat="server" />
                                                                </td>
                                                                <td class="td_input" style="width: 20%">
                                                                    <asp:DropDownList ID="ddlLevelCode" runat="server" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="td_label" style="width: 13%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblUserLabelConsortDepCode" runat="server" />
                                                                </td>
                                                                <td class="td_input" style="width: 20%">
                                                                    <asp:TextBox ID="txtConsortDepCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="divSystemMSG" runat="server">
                                                <td class="td_label">
                                                    &nbsp;
                                                    <asp:Label ID="lblSelectWindow" runat="server">SelectWindow:</asp:Label>
                                                </td>
                                                <td class="td_input" colspan="2">
                                                    <asp:DropDownList ID="ddlWindow" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px">
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 12px">
                                                    <div id="divEmpLeave" runat="server">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px">
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="td_label" colspan="6">
                                                <table>
                                                    <tr>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnSave %>"
                                                                    ToolTip="Authority Code:Save"  OnClick="btnSave_Click">
                                                                </asp:Button>
                                                                <asp:Button ID="btnReturn" runat="server" Text="<%$Resources:ControlText,btnReturn %>"
                                                                    ToolTip="Authority Code:Return" CssClass="button_1" OnClientClick="return Return();">
                                                                </asp:Button>
                                                                <asp:Label ID="lblShiftDesc" runat="server" ForeColor="red"></asp:Label>
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
    </div>
    </form>

    <script type="text/javascript"><!--  
		if(window.parent.document.all.divEdit.style.display=="")
		{  
            document.getElementById("txtBillNo").readOnly=true;
            document.getElementById("txtEmployeeNo").readOnly=true;
            document.getElementById("txtName").readOnly=true;
            document.getElementById("txtDPcode").readOnly=true;
            document.getElementById("txtSex").readOnly=true;
            document.getElementById("txtJoinDate").readOnly=true;         
            document.getElementById("txtLevelCode").readOnly=true;
            document.getElementById("txtManager").readOnly=true;
            document.getElementById("txtProxy").readOnly=true;
            igedit_getById("txtComeYears").setReadOnly(true);
            igedit_getById("txtLVTotal").setReadOnly(true);         
            document.getElementById("divConsort").style.display="none"; 
            document.getElementById("txtLVTotalDays").readOnly=true; 
            if(document.getElementById("ddlLVTypeCode").value=="J") //盉安
            {
                document.getElementById("divConsort").style.display="";
                ConsortIsMandatory(document.getElementById("ddlConsortInFoxonn").value);
            }
            else
            {                
                document.getElementById("divConsort").style.display="none";
            }
		    if(document.getElementById("ProcessFlag").value=="Add")
		    {
                document.getElementById("txtEmployeeNo").readOnly=true;
    	    }
    	    else
    	    {
                document.getElementById("ddlLVTypeCode").focus();
    	    }
    	    if (document.getElementById("ProcessFlag").value=="Modify")
            {
                var sWorkNo=document.getElementById("txtEmployeeNo").value;
                     $.ajax({
                                 type: "post", url: "PCMLeaveApplyEditForm_LHZB.aspx", dataType: "text", data: {workno: sWorkNo,typecode:document.getElementById("ddlLVTypeCode").value,flag: 'LVTypeCode'},
                                 success: function(msg) {
                                   if(msg=="Y")
                                              { 
                                                      alert( Message.BillRefuseAudit);           
                                              }
                                          }
                           });
            }
          }     
	    function onChangeLVTypeCode(strValue)
	    {
	        document.getElementById("hidLVTypeCode").value=strValue;
	        GetLVTotal();
	        ConsortIsShow(strValue);
            var sWorkNo=document.getElementById("txtEmployeeNo").value;	
                  $.ajax({
                                 type: "post", url: "PCMLeaveApplyEditForm_LHZB.aspx", dataType: "text", data: {workno: sWorkNo,typecode:document.getElementById("ddlLVTypeCode").value,flag: 'LVTypeCode'},
                                 success: function(msg) {
                                   if(msg=="Y")
                                              { 
                                                       alert( Message.BillRefuseAudit);           
                                              }
                                          }
                           });
	    }
	--></script>

</body>
</html>
