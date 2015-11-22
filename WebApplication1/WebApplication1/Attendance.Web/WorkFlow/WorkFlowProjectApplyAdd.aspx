<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowProjectApplyAdd.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowProjectApplyAdd" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WorkFlowProjectApplyAdd</title>
    
     <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

     <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

   <script type="text/javascript">
     function CheckWorkNo()
     {
         var WorkNo=document.getElementById("textBoxEmployeeNo").value;
         if (WorkNo.length<3)
         {
             alert(Message.WorkNoFirst);
            document.getElementById("textBoxOTDate").value="";
            document.getElementById("textBoxEmployeeNo").focus();
            return false;        
         }
         return true;
     }
     
       function GetOTType()
       {            
            var OTDate=document.getElementById("textBoxOTDate").value; 
            var WorkNo=document.getElementById("textBoxEmployeeNo").value;
            if (OTDate.length>0)
            {
		        KQM_OTM_OTMProjectApplyEditForm.FindOTType(OTDate,WorkNo,docallback);
		        KQM_OTM_OTMProjectApplyEditForm.GetShiftDeac(WorkNo,OTDate,docallbackshift);
		        KQM_OTM_OTMProjectApplyEditForm.GetOTMFlag(WorkNo,OTDate,docallbackOTMSGFlag);		        
                GetLVTotal();
		    }		
       }
       
       
       function docallback(res) 
       {
            if(res.value!=null)
            {
                document.getElementById("textBoxOTType").value = res.value;
            }
       }
       
       
       function docallbackshift(res) 
       {
          if(res.value!=null)
          {
              if(res.value!="")
              {
                 var temVal= res.value.split(",");
                 if(temVal[1].length>10)
                 {
                    document.getElementById("labelShiftDesc").innerText = "您現在的班別" + temVal[0] + "加班前休息" + temVal[1];
                 }
                 else
                 {
                    document.getElementById("labelShiftDesc").innerText = "您現在的班別是"+temVal[0];
                 }
                 //var sBeginTime=igedit_getById("textBoxBeginTime").getValue()==null?"":igedit_getById("textBoxBeginTime").getValue();
                 var sBeginTime=document.getElementById("textBoxBeginTime").value==null?"":document.getElementById("textBoxBeginTime").value;
		        if(document.getElementById("ProcessFlag").value=="Add"&&sBeginTime.length==0)
		        {
		            KQM_OTM_OTMProjectApplyEditForm.GetBeginTime(document.getElementById("textBoxOTType").value,res.value,docallbackBeginTime);  
		        }
              }
              else
              {
                document.getElementById("labelShiftDesc").innerText = "找不到您的班別，請先排班！";
              }
          }
       }
       
       
       function docallbackOTMSGFlag(res) 
       {
            if(res.value!=null)
            {
                if(res.value=="1")
                {
                    document.getElementById("HiddenOTMSGFlag").value = "1";
                }
                else
                {
                    document.getElementById("HiddenOTMSGFlag").value = "";
                }
            }
       }
       
       function CheckSave()//保存
       {
//            var OTMSGFlag=document.getElementById("HiddenOTMSGFlag").value;
//            if(OTMSGFlag.length>0)
//            {                
//                if(confirm("您已經連續工作6天，確認是否需要加班？"))
//                {
//                    //FormSubmit("<%=sAppPath%>");
//                    return true;
//                }
//                else
//                {			    
//                    return false;
//                }
//            }
//            else
//            {
//                //FormSubmit("<%=sAppPath%>");
//                return true;
//            }    
//            
//            var WorkNo=document.getElementById("textBoxEmployeeNo").value;
//            var ID=document.getElementById("HiddenID").value;
//            var OTDate=document.getElementById("textBoxOTDate").value;
//            var OTHours=document.getElementById("textBoxHours").value;
//            var OTMFlag=document.getElementById("HiddenOTMSGFlag").value;
//            var OTMSGFlag=KQM_OTM_OTMAdvanceApplyEdit.GetOTMSGFlag(WorkNo,OTDate,OTHours,"N",ID).value;
//            if(OTMSGFlag!=null&&OTMSGFlag!="")
//            {
//                if(OTMFlag.length>0)  //连续上班六天
//                {
//	                if(confirm(OTMSGFlag))
//	                {
//		              
//		                return true;
//		            }
//		            else
//		            {			    
//		                return false;
//		            }
//		        }
//		        else //其它管控类提示		        
//		        {
//	                if(confirm(OTMSGFlag))
//	                {
//		                
//		                return true;
//		            }
//		            else
//		            {			    
//		                return false;
//		            }
//		        }
//            }
//            else
//            {
              return true;
//            } 
       }
       
       function docallbackBeginTime(res) 
       {
          if(res.value!=null)
          {
              if(res.value!="")
              {
                //igedit_getById("textBoxBeginTime").setValue(res.value);
                document.getElementById("textBoxBeginTime").value=res.value;
              }
          }
       }
       
		function WeekDate()
		{
		    document.getElementById("textBoxWeek").value="";
		    document.getElementById("textBoxOTType").value="";
		    if(document.getElementById("textBoxOTDate").value.length==0)
		    {		       
		       return;		       
		    }
		    if(!validateCNDate(document.getElementById("textBoxOTDate").value))
		    {		       
		       return;		       
		    }		    		    
		    // Get OverTime Type
		    GetOTType();
		   
			//........Get WeekDay........................
			date=new Date(document.getElementById("textBoxOTDate").value.replace("-",","));				
			//alert(document.getElementById("textBoxWorkDay").value.replace("-",","));
			
			if(date.getDay()==0)
			{
			    document.getElementById("textBoxWeek").value = Message.common_sunday;
			}
			if(date.getDay()==1)
			{
			    document.getElementById("textBoxWeek").value = Message.common_monday;
			}
			if(date.getDay()==2)
			{
			    document.getElementById("textBoxWeek").value = Message.common_tuesday;
			}
			if(date.getDay()==3)
			{
			    document.getElementById("textBoxWeek").value = Message.common_wednesday;
			}
			if(date.getDay()==4)
			{
			    document.getElementById("textBoxWeek").value = Message.common_thursday;
			}
			if(date.getDay()==5)
			{
			    document.getElementById("textBoxWeek").value = Message.common_friday;
			}
			if(date.getDay()==6)
			{
			    document.getElementById("textBoxWeek").value = Message.common_saturday;
			}
		}
		
        function GetEmp()
		{
            var EmployeeNo=document.getElementById("textBoxEmployeeNo").value;
            document.getElementById("HiddenCheckEmp").value="";
            if(EmployeeNo.length>0&&document.getElementById("ProcessFlag").value=="Add")
            {               
               KQM_OTM_OTMAdvanceApplyEdit.GetEmp(EmployeeNo,"","",docallbackds)
               KQM_OTM_OTMAdvanceApplyEdit.Get_MonthOverTime(EmployeeNo,docallbackmonthot)
                var OTDate=document.getElementById("textBoxOTDate").value; 
                if (OTDate.length>0)
                {
		            KQM_OTM_OTMAdvanceApplyEdit.FindOTType(OTDate,EmployeeNo,docallback);
		            KQM_OTM_OTMAdvanceApplyEdit.GetShiftDeac(EmployeeNo,OTDate,docallbackshift);
		            KQM_OTM_OTMAdvanceApplyEdit.GetOTMSGFlag(EmployeeNo,OTDate,docallbackOTMSGFlag);
                    GetLVTotal();
		        }
            }
		}
		
	   function docallbackmonthot(res) 
       {
          document.getElementById("textBoxMonthAllHours").value = res.value;
       }
           
//        function docallbackds(response)
//        {  
//            var ds = response.value;
//            if(ds != null && typeof(ds) == "object" && ds.Tables != null && ds.Tables[0].Rows.length>0)
//            {
//                for(var i=0; i<ds.Tables[0].Rows.length; i++)
//                {
//                    document.getElementById("textBoxEmployeeNo").value=ds.Tables[0].Rows[i].WORKNO==null?"":ds.Tables[0].Rows[i].WORKNO;
//                    document.getElementById("textBoxLocalName").value=ds.Tables[0].Rows[i].LOCALNAME==null?"":ds.Tables[0].Rows[i].LOCALNAME;
//                    document.getElementById("textBoxDPcode").value=ds.Tables[0].Rows[i].DEPNAME==null?"":ds.Tables[0].Rows[i].DEPNAME;
//                    document.getElementById("textBoxPersonType").value=ds.Tables[0].Rows[i].OVERTIMETYPE==null?"":ds.Tables[0].Rows[i].OVERTIMETYPE;
//                    
//                }
//            }
//            else
//            {
//                document.getElementById("textBoxEmployeeNo").value="";
//                document.getElementById("textBoxLocalName").value="";
//                document.getElementById("textBoxDPcode").value="";
//                document.getElementById("textBoxPersonType").value="";
//                
//                alert("員工基本資料不存在");
//            }      
//            return;
//        }
        
        function Return()//返回
        {
            window.parent.document.all.ButtonQuery.click();
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.div_2.style.display="";
            return false;
        }
        
        function GetLVTotal()
        {
            var WorkNo=document.getElementById("textBoxEmployeeNo").value;
            var OTDate=document.getElementById("textBoxOTDate").value; 
            var BeginTime=document.getElementById("textBoxBeginTime").value; 
            var EndTime=document.getElementById("textBoxEndTime").value; 
            var OTType=document.getElementById("textBoxOTType").value;
            if(WorkNo.length>0&&OTDate.length>0&&BeginTime.length==5&&EndTime.length==5)
            {
                //                KQM_OTM_OTMProjectApplyEditForm.GetOTHours(WorkNo,OTDate,BeginTime,EndTime,OTType,docallback_gethour);
                $.ajax(
                {
                    type: "post",
                    url: "../WorkFlowForm/AjaxProcess/GetOtHours.ashx?WorkNo=" + escape(WorkNo) + "&OTDate=" + escape(OTDate) + "&BeginTime=" + escape(BeginTime) + "&EndTime=" + escape(EndTime) + "&OTType=" + escape(OTType),
                    dataType: "text",
                    async: false,
                    success: function(data) {
                        if (data == "-1") {
                            alert("計算時數出現異常,請檢查是否排班或排班開始結束時間和加班開始結束時間是否有沖突");
                        }
                        else {
                            document.getElementById("textBoxHours").value = data;
                        }
                    }
                }
                );
            }
             else
             {
                document.getElementById("textBoxHours").value = 0;
             }
        }
    
    </script>

    <script id="igClientScript" type="text/javascript">
    function textBoxBeginTime_TextChanged(oEdit, newText, oEvent)
    {
        GetLVTotal();
    }
    
    
     function textBoxEndTime_TextChanged(oEdit, newText, oEvent)
    {
        GetLVTotal();
    }
    
    
    function docallback_gethour(res) 
    {
        document.getElementById("textBoxHours").value = res.value;
    }
       
       
    function getDays()//得到相隔小时差
    {       
        var BeginTime=new Date(igedit_getById("textBoxBeginTime").getValue());
        var EndTime=new Date(igedit_getById("textBoxEndTime").getValue());
        var diff=(EndTime.valueOf()-BeginTime.valueOf())/3600000;
        var Strdiff=diff.toString();
        if (Strdiff.indexOf(".")>0)
        {
            var tmpdem=Strdiff.substr(Strdiff.indexOf(".")+1,1);
            var tmpint=Strdiff.substr(0,Strdiff.indexOf("."))
            if (parseInt(tmpdem)>=5)
            {
                Strdiff=tmpint+".5";
            }
            else
            {
                Strdiff=Strdiff.substr(0,Strdiff.indexOf("."))
             }
            
            //alert(Strdiff);
        
        }
        document.getElementById("textBoxHours").value=Strdiff;
    }
    </script>

</head>
<body>
 <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server">
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server"><%--新增完資料保留工號--%>
    <input id="HiddenState" type="hidden" name="HiddenState" runat="server" />
    <input id="HiddenOTMSGFlag" type="hidden" name="HiddenOTMSGFlag" runat="server" />
    
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" height="96%" align="center">
        <tr>
            <td valign="top">
                 <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                
                    <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                        <td style="width: 100%;">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                                            <div id="Div1">
                                                <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                                
                    <tr>
                       <td colspan="2">
                            <div id="div_1">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    
                                    <tr>
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelEmployeeNo" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="100%" CssClass="input_textBox" ontextchanged="textBoxEmployeeNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelName" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                         <asp:TextBox ID="textBoxLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>                                            
                                        </td>
                                        
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="12%">
                                            &nbsp;
                                            <asp:Label ID="labelDepcode" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxDPcode" ReadOnly="true" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelPersonType" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxPersonType" ReadOnly="true" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelApplyDate" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxApplyDate" ReadOnly="true" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label_view" style=" background:#DFE8FA;" width="12%">
                                            &nbsp;
                                            <asp:Label ID="labelMonthAllHours" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxMonthAllHours" ReadOnly="true" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" style=" color:Blue;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelOTDateFrom" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                             <asp:TextBox ID="textBoxOTDate" runat="server" Width="100%" CssClass="input_textBox"
                                             ontextchanged="textBoxOTDate_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="11%"><!--星期-->
                                            &nbsp;
                                            <asp:Label ID="labelWeek" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxWeek" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="11%"> &nbsp;<!--加班類型-->
                                            <asp:Label ID="labelOTType" runat="server" Text="Label"></asp:Label>
                                        </td>
                                       <td class="td_input" width="22%">
                                           <asp:TextBox ID="textBoxOTType" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                       </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" style=" color:Blue;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelBeginTime" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <igtxt:WebDateTimeEdit ID="textBoxBeginTime" runat="server" EditModeFormat="HH:mm"
                                                   Width="100%" CssClass="input_textBox" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                   <ClientSideEvents ValueChange="textBoxBeginTime_TextChanged" />
                                            </igtxt:WebDateTimeEdit>
                                        </td>
                                        <td class="td_label" style=" color:Blue;" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelEndTime" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <igtxt:WebDateTimeEdit ID="textBoxEndTime" runat="server" EditModeFormat="HH:mm"
                                                   Width="100%" CssClass="input_textBox" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                   <ClientSideEvents ValueChange="textBoxEndTime_TextChanged" />
                                            </igtxt:WebDateTimeEdit>
                                        </td>
                                        <td class="td_label" style=" color:Blue;" width="12%">   &nbsp;
                                            <asp:Label ID="labelHours" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxHours" runat="server" onfocus="this.blur();" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" width="11%" style=" color:Blue">
                                            &nbsp;
                                            <asp:Label ID="labelWorkDesc" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input" colspan="3">
                                            <asp:TextBox ID="textBoxWorkDesc" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label" style=" color:Blue;" style=" color:Blue">
                                            &nbsp;
                                            <asp:Label ID="labbeiz" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td class="td_input">
                                            <asp:TextBox ID="textBoxRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>                                   
                                    
                                    <tr>
                                        <td class="td_label" colspan="6">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonSave" runat="server"  ToolTip="Authority Code:Save" CssClass="button_1"
                                                            CommandName="Save" OnClick="ButtonSave_Click" >
                                                        </asp:Button>
                                                        <asp:Button ID="ButtonReturn" runat="server" CommandName="Return"  ToolTip="Authority Code:Return" CssClass="button_1"
                                                            OnClientClick="return Return();" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="labelShiftDesc" runat="server" ForeColor="red"></asp:Label>
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
   
</body>
</html>
