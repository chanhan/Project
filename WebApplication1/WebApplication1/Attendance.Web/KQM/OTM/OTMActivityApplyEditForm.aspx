<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMActivityApplyEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMActivityApplyEditForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>免卡人員加班導入編輯</title>
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
     function CheckWorkNo()
     {
     var WorkNo=document.getElementById("txtEmployeeNo").value;
     if (WorkNo.length<3)
     {
        alert(Message.WorkNoFirst);
        document.getElementById("txtOTDate").value="";
        document.getElementById("txtEmployeeNo").focus();
        return false;        
     }
     return true;
     }
       function GetOTType()
       {            
           var OTDate=document.getElementById("txtOTDate").value; 
            var WorkNo=document.getElementById("txtEmployeeNo").value;
            
            if (OTDate.length>0)
            {
               docallback(WorkNo,OTDate) ;
               docallbackOTMFlag(WorkNo,OTDate);
           }                    
                                      
     }	 
       
       
       function docallback(WorkNo,OTDate) 
       {
       var ActionFlag="FindOTType";
       $.ajax( {
                 type:"post",url:"OTMActivityApplyEditForm.aspx",dataType:"text",data:{WorkNo:WorkNo,OTDate:OTDate,ActionFlag:ActionFlag},  
                 success:function(msg) 
                    {
                        if (msg)
                        { 
                        document.getElementById("txtOTType").value = msg;
                        }
                     }
                }
            )
       }
       function docallbackshift(res) 
       {

       }
       
       function docallbackOTMFlag(WorkNo,OTDate)
       {
            var ActionFlag="GetOTMFlag";
         $.ajax( {
                 type:"post",url:"OTMActivityApplyEditForm.aspx",dataType:"text",data:{WorkNo:WorkNo,OTDate:OTDate,ActionFlag:ActionFlag},  
                 success:function(msg) 
                    {
                        if (msg)
                        { 
                            if(msg=="1")
                            {
                                document.getElementById("HiddenOTMSGFlag").value = "1";
                            }
                            else
                            {
                                document.getElementById("HiddenOTMSGFlag").value = "";
                            }
                        }
                     }
                }
            )
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
       function CheckSave() 
       {
            var OTMSGFlag=document.getElementById("HiddenOTMSGFlag").value;
            if(OTMSGFlag.length>0)
            {                
	            if(confirm(Message.OtmAdvanceapplyWorksixdaymessage))
	            {
		            //FormSubmit("<%=sAppPath%>");
		            return true;
		        }
		        else
		        {			    
		            return false;
		        }
            }
            else
            {
		        //FormSubmit("<%=sAppPath%>");
                return true;
            }
       }
       function docallbackBeginTime(res) 
       {
//          if(res.value!=null)
//          {
//              if(res.value!="")
//              {
//                igedit_getById("txtBeginTime").setValue(res.value);
//                document.getElementById("txtBeginTime").value=res.value;
//              }
//          }
       }
		function WeekDate()
		{
		    document.getElementById("txtWeek").value="";
		    document.getElementById("txtOTType").value="";
		    if(document.getElementById("txtOTDate").value.length==0)
		    {		       
		       return;		       
		    }
//		    if(!validateCNDate(document.getElementById("txtOTDate").value))
//		    {		       
//		       return;		       
//		    }		    		    
		    // Get OverTime Type
		    GetOTType();
		   
			//........Get WeekDay........................
			date=new Date(document.getElementById("txtOTDate").value.replace("-",","));				
			//alert(document.getElementById("txtWorkDay").value.replace("-",","));
			
			if(date.getDay()==0)
			{
				document.getElementById("txtWeek").value=Message.sunday;
			}
			if(date.getDay()==1)
			{
				document.getElementById("txtWeek").value=Message.monday;
			}
			if(date.getDay()==2)
			{
				document.getElementById("txtWeek").value=Message.tuesday;
			}
			if(date.getDay()==3)
			{				   
				document.getElementById("txtWeek").value=Message.wednesday;
			}
			if(date.getDay()==4)
			{
				document.getElementById("txtWeek").value=Message.thursday;
			}
			if(date.getDay()==5)
			{
				document.getElementById("txtWeek").value=Message.friday;
			}
			if(date.getDay()==6)
			{
				document.getElementById("txtWeek").value=Message.saturday;
			}
		}

		function GetEmp() {
		    //debugger;
           
            var EmployeeNo=$("#<%=txtEmployeeNo.ClientID %>").val();
            
            if(EmployeeNo.length>0 && $("#<%=ProcessFlag.ClientID %>").val() =="Add")
            {
               docallbackds(EmployeeNo);
               docallbackmonthot(EmployeeNo);
               
               
                var OTDate=$("#<%=txtOTDate.ClientID %>").val(); 
                if (OTDate.length>0)
                {
	                //docallback(EmployeeNo,OTDate) ;
	            }
	            
            }
		}
		function docallbackmonthot(WorkNo) 
           {
           var ActionFlag="Get_MonthOverTime";
         $.ajax( {
                 type:"post",url:"OTMActivityApplyEditForm.aspx",dataType:"text",data:{WorkNo:WorkNo,ActionFlag:ActionFlag},  
                 success:function(msg) 
                    {
                        if (msg)
                        { 
                           document.getElementById("txtMonthAllHours").value = msg;
                     
                        }
                     }
                }
            )
          
            
           }
        function docallbackds(WorkNo) {
             
             var ActionFlag="GetEmp";
            var sqlDep="<%=GetSqlDep()%>";
         $.ajax( {
                 type:"post",url:"OTMActivityApplyEditForm.aspx",dataType:"json",data:{WorkNo:WorkNo,ActionFlag:ActionFlag,SqlDep:sqlDep},  
                 success:function(msg) 
                    {
                        if (msg)
                        { 
                         $("#<%=txtEmployeeNo.ClientID %>").val(msg.WorkNo);
                         $("#<%=txtLocalName.ClientID %>").val(msg.LocalName);
                         $("#<%=txtDPcode.ClientID %>").val(msg.DepName);
                         $("#<%=txtPersonType.ClientID %>").val(msg.OverTimeType);
                    
                         }
                         else
                         {
                             $("#<%=txtEmployeeNo.ClientID %>").val("");
                             $("#<%=txtLocalName.ClientID %>").val("");
                             $("#<%=txtDPcode.ClientID %>").val("");
                             $("#<%=txtPersonType.ClientID %>").val("");
                             alert(Message.EmpBasicInfoNotExist);
                        
                         }     
                    } 
                })
          
             
            
        }
        function Return()
        {
            //window.parent.document.all.btnQuery.click();
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.UltraWebGrid.style.display="";
            window.parent.document.all.div_2.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            return false;
        }
        function GetLVTotal()
        {
           var WorkNo=document.getElementById("txtEmployeeNo").value;
            var OTDate=document.getElementById("txtOTDate").value; 
            var BeginTime=document.getElementById("txtBeginTime").value; 
            var EndTime=document.getElementById("txtEndTime").value; 
            var OTType=document.getElementById("txtOTType").value;
            if(WorkNo.length>0&&OTDate.length>0&&BeginTime.length==5&&EndTime.length==5)
            {
            
                docallback_gethour(WorkNo,OTDate,BeginTime,EndTime,OTType);
            }
             else
             {
                document.getElementById("txtHours").value = 0;
             } 
        }
        
        function btnSave_Click()
        {
           var txtEmpNo=$.trim($("#<%=txtEmployeeNo.ClientID%>").val());
           var txtOtdate=$.trim($("#<%=txtOTDate.ClientID%>").val());
           var txtHours=$.trim($("#<%=txtHours.ClientID%>").val());
           var txtWorkDesc=$.trim($("#<%=txtWorkDesc.ClientID%>").val());
           var txtStartTime=$.trim($("#<%=txtStartTime.ClientID%>").val());
           var txtEndTime=$.trim($("#<%=txtEndTime.ClientID%>").val());
           if  (txtEmpNo.length==0)
           { alert($.trim($("#<%=lblEmployeeNo.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else if  (txtOtdate.length==0)
           { alert($.trim($("#<%=lblOTDateFrom.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else if  (txtHours.length==0)
           { alert($.trim($("#<%=lblHours.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else if  (txtWorkDesc.length==0)
           { alert($.trim($("#<%=lblWorkDesc.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else if  (txtStartTime.length==0)
           { alert($.trim($("#<%=lblStartTime.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else if  (txtEndTime.length==0)
           { alert($.trim($("#<%=lblEndTime.ClientID%>").text())  +Message.NotNullOrEmpty); return false;}
           else {
           
           var BeginTime=new Date(igedit_getById("txtStartTime").getValue());
           var EndTime=new Date(igedit_getById("txtEndTime").getValue());
           var diff=(EndTime.valueOf()-BeginTime.valueOf())/3600000;
           if(Number(txtHours)>Number(diff))
           {
              alert(Message.ConfirmHoursTooLarge);
              return false;
           }
           else
           {
           
           if (CheckSave()) return true;
           else return false;
           }
           }
          
        }
    
--></script>

    <script id="igClientScript" type="text/javascript">
<!--
    function txtBeginTime_TextChanged(oEdit, newText, oEvent)
    {
        GetLVTotal();
    }
     function txtEndTime_TextChanged(oEdit, newText, oEvent)
    {
        GetLVTotal();
    }
    function docallback_gethour(WorkNo,OTDate,BeginTime,EndTime,OTType) {
     var ActionFlag="GetOTHours";
         $.ajax( {
                 type:"post",url:"OTMActivityApplyEditForm.aspx",dataType:"text",data:{WorkNo:WorkNo,OTDate:OTDate,BeginTime:BeginTime,EndTime:EndTime,ActionFlag:ActionFlag},  
                 success:function(msg) 
                    {
                        if (msg)
                        { 
                           document.getElementById("txtHours").value = msg;
                     
                        }
                     }
                }
            )
    

       }
    function getDays()//得到相隔小时差
    {       
        var BeginTime=new Date(igedit_getById("txtBeginTime").getValue());
        var EndTime=new Date(igedit_getById("txtEndTime").getValue());
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
        document.getElementById("txtHours").value=Strdiff;
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

        function load() 
        {
            GetEmp(); 
        }
        
        window.onload = load;

// -->
    </script>

</head>
<body class="color_body" >
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
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
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
                            <div id="div_1">
                                <table  width="100%" class="table_data_area">
                                    <tr id="tr_show">
                                        <td>
                                            <table  width="100%" class="table_data_area">
                                                <tr class="tr_data_1">
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server" resourceid="common.employeeno" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox_1"
                                                            onblur="GetEmp();"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblName" runat="server" resourceid="common.name" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDepcode" runat="server" resourceid="common.organise" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtDPcode" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblPersonType" runat="server" resourceid="kqm.otm.person.type" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtPersonType" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblMonthAllHours" runat="server" resourceid="kqm.otm.monthallhours" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtMonthAllHours" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTDateFrom" runat="server" resourceid="kqm.otm.date.from" ismandatory="true" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtOTDate" runat="server" Width="100%" CssClass="input_textBox_1"
                                                            onclick="return CheckWorkNo();"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWeek" runat="server" resourceid="common.week" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtWeek" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTType" runat="server" resourceid="kqm.otm.type" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtOTType" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td class="td_label" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblHours" runat="server" resourceid="common.hours" ismandatory="true" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <igtxt:webnumericedit id="txtHours" runat="server" width="100%" cssclass="input_textBox_2"
                                                            datamode="Decimal" horizontalalign="left">
                                                        </igtxt:webnumericedit>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;&nbsp;<asp:Label ID="lblWorkDesc" runat="server" resourceid="kqm.otm.workdesc" ismandatory="true" />
                                                    </td>
                                                    <td class="td_input" width="89%" colspan="5">
                                                        <asp:TextBox ID="txtWorkDesc" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblStartTime" runat="server" resourceid="kqm.otm.date.from" ismandatory="true" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <igtxt:webdatetimeedit id="txtStartTime" runat="server" editmodeformat="HH:mm" width="100%"
                                                            cssclass="input_textBox_1">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEndTime" runat="server" resourceid="common.week" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <igtxt:webdatetimeedit id="txtEndTime" runat="server" editmodeformat="HH:mm" width="100%"
                                                            cssclass="input_textBox_1">
                                                        </igtxt:webdatetimeedit>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                                                OnClientClick="return btnSave_Click()"></asp:Button>
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
    </td> </tr> </table>
    </form>

    <script type="text/javascript"><!--
 		if(window.parent.document.all.divEdit.style.display=="")
		{
            document.getElementById("txtEmployeeNo").readOnly=true;
            document.getElementById("txtLocalName").readOnly=true;
            document.getElementById("txtDPcode").readOnly=true;       
            document.getElementById("txtPersonType").readOnly=true;
            document.getElementById("txtMonthAllHours").readOnly=true;
            //document.getElementById("txtApplyDate").readOnly=true;
            
            document.getElementById("txtWeek").readOnly=true;       
            document.getElementById("txtOTType").readOnly=true;
            //document.getElementById("txtHours").readOnly=true;
           
		    if(document.getElementById("ProcessFlag").value=="Add")
		    {
                document.getElementById("txtEmployeeNo").readOnly=false;
                document.getElementById("txtEmployeeNo").focus();
                document.getElementById("txtEmployeeNo").select();
    	    }
    	    
        }
	--></script>

</body>
</html>
