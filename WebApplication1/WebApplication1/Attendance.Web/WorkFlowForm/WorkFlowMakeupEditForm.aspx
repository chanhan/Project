<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowMakeupEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlowForm.WorkFlowMakeupEditForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WorkFlowMakeupEditForm</title>

    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../WorkFlow/css/MyStyles.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>


 
    <script type="text/javascript"><!--    
    
        /****
        * 根據工號獲取員工姓名部門等信息
        **/
        function GetEmp() {
            var EmployeeNo = document.getElementById("textBoxEmployeeNo").value;
            if (EmployeeNo.length > 0 && document.getElementById("ProcessFlag").value == "Add") {
                docallback();
            }
        }
        function docallback() {
            var empno = $.trim($("#<%=textBoxEmployeeNo.ClientID %>").val());
            var sqlDep = "<%=GetSqlDep()%>";
            if (empno != "") {
                $.ajax(
                                   {
                                       type: "post", url: "WorkFlowMakeupEditForm.aspx", dataType: "json", data: { Empno: empno, SqlDep: sqlDep },
                                       success: function(msg) {
                                           if (msg.LocalName != null) {
                                               $("#<%=textBoxEmployeeNo.ClientID %>").val(msg.WorkNo);
                                               $("#<%=textBoxLocalName.ClientID %>").val(msg.LocalName);
                                               $("#<%=textBoxDepName.ClientID %>").val(msg.DepName);
                                           }
                                           else {
                                               $("#<%=textBoxEmployeeNo.ClientID %>").val("");
                                               $("#<%=textBoxLocalName.ClientID %>").val("");
                                               $("#<%=textBoxDepName.ClientID %>").val("");
                                               alert("<%=Resources.Message.EmpNotExist%>");
                                           }
                                       }
                                   });
            }
            return false;

        }
  
        function onChangeMakeupType(strValue)
    	{
                var OTType = document.getElementById("HiddenOTType").value;
                var MakeupType=strValue;
                //var ReasonType=document.getElementById("ddlReasonType").value;
                var SalaryFlag=document.getElementById("HiddenSalaryFlag").value;
                if(MakeupType=="2"||MakeupType=="3"||
                OTType=="G2"||OTType=="G3"||
                SalaryFlag=="N")
                {
                    document.getElementById("labelDecSalary").innerText = "<%=Resources.ControlText.workFlowDecSalary %>" + ":N";
                   document.getElementById("HiddenDecSalary").value="N";
                }
                else
                {
                    document.getElementById("labelDecSalary").innerText = "<%=Resources.ControlText.workFlowDecSalary %>" + ":Y";
                   document.getElementById("HiddenDecSalary").value="Y";
                }
		    }

		/**function onChangeReasonType(strValue) {
		    if (strValue.length > 0) {
		            GetSALARYFLAG(strValue, doReasonType);
		     }
		 } **/   
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display = "none";
            window.parent.document.all.div_2.style.display = ""; 
            return false;
        }

        $(function() {
            $("#tr_edit").toggle(
            function() {
                $("#tr_show").hide();
                $(".img1").attr("src", "../../CSS/Images_new/left_back_03.gif");

            },
            function() {
                $("#tr_show").show();
                $(".img1").attr("src", "../../CSS/Images_new/left_back_03_a.gif");
            }
        )
        });

        //保存數據
        function save() 
        {
            var alertText = "";
            var empNo = $.trim($("#<%=textBoxEmployeeNo.ClientID %>").val());
            var workDate = $.trim($("#<%=textBoxKQDate.ClientID %>").val());
            var CardTime = $.trim($("#<%=textBoxCardTime.ClientID %>").val());
            var MakeupType = $.trim($("#<%=ddlMakeupType.ClientID %>").val());
            var ReasonType = $.trim($("#<%=ddlReasonType.ClientID %>").val());
            
            if (empNo.length == 0)
            {
                alertText = $.trim($("#<%=labelEmployeeNo.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            else if (workDate.length == 0) 
            {
                alertText = $.trim($("#<%=labelKQDate.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            else if (CardTime.length == 0) {
                alertText = $.trim($("#<%=labelCardTime.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            else if (MakeupType.length == 0) {
                alertText = $.trim($("#<%=labelMakeupType.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            else if (ReasonType.length == 0) {
                alertText = $.trim($("#<%=labelReasonType.ClientID%>").text()) + Message.TextBoxNotNull;
                alert(alertText);
                return false;
            }
            else {
                    return true; 
            }
        } function GetShiftDesc()
		{
            var EmployeeNo=document.getElementById("textBoxEmployeeNo").value;
            var KQTDate=document.getElementById("textBoxKQDate").value; 
           /** if(EmployeeNo.length>0&&KQTDate.length>0)
            {
		        KQM_KaoQinData_KQMKaoDataMakeupEditForm.GetShiftDeac(EmployeeNo,KQTDate,docallbackshift);
		        KQM_KaoQinData_KQMKaoDataMakeupEditForm.GetOTType(KQTDate,EmployeeNo,docallback);
            }
            **/
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
                     document.getElementById("labelShiftDesc").innerText = "<%=Resources.Message.otm_nowshiftno %>" + temVal[0] + "<%=Resources.Message.PMMoreThanFiveH %>" + temVal[1];
                 }
                 else
                 {
                     document.getElementById("labelShiftDesc").innerText = "<%=Resources.Message.otm_nowshiftno %>" + temVal[0];
                 }
              }
              else
              {
                  document.getElementById("labelShiftDesc").innerText = "<%=Resources.Message.otm_nowshiftno %>";
              }
          }
       }
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display = "none";
            window.parent.document.all.div_2.style.display = "";
            
            return false;
        }

        function WeekDate() {
            document.getElementById("txtWeek").value = "";
            document.getElementById("txtOTType").value = "";
            if (document.getElementById("txtOTDate").value.length == 0) {
                return;
            }
            //		    if(!validateCNDate(document.getElementById("txtOTDate").value))
            //		    {		       
            //		       return;		       
            //		    }		    		    
            // Get OverTime Type
            GetOTType();

            //........Get WeekDay........................
            date = new Date(document.getElementById("txtOTDate").value.replace("-", ","));
            //alert(document.getElementById("txtWorkDay").value.replace("-",","));

            if (date.getDay() == 0) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.sunday%>";
            }
            if (date.getDay() == 1) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.monday%>";
            }
            if (date.getDay() == 2) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.tuesday%>";
            }
            if (date.getDay() == 3) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.wednesday%>";
            }
            if (date.getDay() == 4) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.thursday%>";
            }
            if (date.getDay() == 5) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.friday%>";
            }
            if (date.getDay() == 6) {
                document.getElementById("txtWeek").value = "<%=Resources.Message.saturday%>";
            }
        }

        
// -->
    </script>

</head>
<body class="color_body" onload="GetEmp()">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
    <input type="hidden" id="HiddenCheckEmp" runat="server" />
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server" /><%--新增完資料保留工號--%>
    <input id="HiddenState" type="hidden" name="HiddenState" runat="server" />
    <input id="HiddenOTMSGFlag" type="hidden" name="HiddenOTMSGFlag" runat="server" />
    <input id="HiddenOTType" type="hidden" name="HiddenOTType" runat="server" />
    <input id="HiddenSalaryFlag" type="hidden" name="HiddenSalaryFlag" runat="server" />
    <input id="HiddenDecSalary" type="hidden" name="HiddenDecSalary" runat="server" />
    <input id="HiddenModuleCode" type="hidden" name="HiddenDecSalary" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" height="90%" align="center">
        <tr>
            <td valign="top" style>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand" onclick="turnit('div_1','div_img_1','<%=sAppPath%>');">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCondition" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div id="img_edit">
                                            <img id="div_img" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" class="table_data_area" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="labelEmployeeNo" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="100%" 
                                                            CssClass="input_textBox"   onblur="return GetEmp();" ></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="labelName" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="textBoxLocalName" runat="server" Width="100%" 
                                                            CssClass="input_textBox" ontextchanged="textBoxLocalName_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label_view" width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="labelDepcode" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="textBoxDepName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="labelKQDate" runat="server" Text="Label" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="textBoxKQDate" runat="server" Width="100%"  CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="labelCardTime" runat="server" Text="Label" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <igtxt:WebDateTimeEdit ID="textBoxCardTime" runat="server" EditModeFormat="HH:mm"
                                                            Width="100%" CssClass="input_textBox" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                        </igtxt:WebDateTimeEdit>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="labelMakeupType" runat="server" Text="Label" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlMakeupType" runat="server" Width="100%" CssClass="input_textBox">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label">
                                                        &nbsp;<asp:Label ID="labelReasonType" runat="server" Text="Label" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:DropDownList ID="ddlReasonType" runat="server" Width="100%" 
                                                            CssClass="input_textBox" 
                                                            onselectedindexchanged="ddlReasonType_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="labelReasonRemark" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="textBoxReasonRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                        <asp:Label ID="labelDecSalary" runat="server" ForeColor="red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        &nbsp;
                                                        <asp:Label ID="labelRemark" runat="server" ForeColor="red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="6">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ToolTip="Authority Code:Save"
                                                                        CommandName="Save" OnClick="ButtonSave_Click" CssClass="input_linkbutton WF_input_Button2"
                                                                        BorderWidth="0px" OnClientClick="save()"></asp:Button>
                                                                    <asp:Button ID="ButtonReturn" runat="server" CommandName="Return" CssClass="WF_input_Button2"
                                                                        Text="Return" ToolTip="Authority Code:Return" OnClientClick="return Return();"
                                                                        BorderWidth="0px" />
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
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript"><!-- 		
 		if(window.parent.document.all.divEdit.style.display=="")
		{
            document.getElementById("textBoxEmployeeNo").readOnly=true;
            document.getElementById("textBoxLocalName").readOnly=true;
            document.getElementById("textBoxDepName").readOnly=true;

            //alert("SSSSSSS:"+document.getElementById("ProcessFlag").value);
            if (document.getElementById("ProcessFlag").value == "Add" && document.getElementById("HiddenModuleCode").value != "PCMSYS108") {
		        //alert();
                document.getElementById("textBoxEmployeeNo").readOnly=false;
                document.getElementById("textBoxEmployeeNo").focus();
                document.getElementById("textBoxEmployeeNo").select();
    	    }
    	    
    	    
		   
    	    
    	    /*** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** **/
    	   function doReasonType(res)
    	   {
    	        document.getElementById("HiddenSalaryFlag").value=res.value;
    	        var OTType = document.getElementById("HiddenOTType").value;
                var MakeupType=document.getElementById("ddlMakeupType").value;
                //var ReasonType=strValue;
                
                if(MakeupType=="2"||MakeupType=="3"||
                OTType=="G2"||OTType=="G3"||
                res.value=="N")
                {
                   document.getElementById("labelDecSalary").innerText = "<%=Resources.ControlText.workFlowDecSalary%>"+":N";
                   document.getElementById("HiddenDecSalary").value="N";
                }
                else
                {
                    document.getElementById("labelDecSalary").innerText = "<%=Resources.ControlText.workFlowDecSalary %>" + ":Y";
                   document.getElementById("HiddenDecSalary").value="Y";
                }
    	   }
    	}
         
	--></script>

</body>
</html>
