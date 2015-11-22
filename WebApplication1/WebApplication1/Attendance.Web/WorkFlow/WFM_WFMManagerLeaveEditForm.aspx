<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFM_WFMManagerLeaveEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WFM_WFMManagerLeaveEditForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WFMManagerLeaveEditForm</title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
         
          //顯示隱藏
          $(function()
          {
             $("#tr_edit").toggle
             (
                function()
                {
                    $("#div_select").hide();
                    $(".img1").attr("src","../CSS/Images_new/left_back_03.gif");
                    
                },
                function()
                {
                  $("#div_select").show();
                  $(".img1").attr("src","../CSS/Images_new/left_back_03_a.gif");
               
                }
             )                  
           });
        //返回事件   
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            return false;
        } 
       //日曆驗證
	   function CheckDate()
       {
          var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
          var FromDate= $("#<%=textBoxStartDate.ClientID%>").val();
          var ToDate=$("#<%=textBoxEndDate.ClientID %>").val();
          if (FromDate!=null&&FromDate!="")
          {
             if(!check.test(FromDate))
             {
               alert(Message.WrongDate);
               $("#<%=textBoxStartDate.ClientID%>").val("");
               return false;
             }
          }
          if (ToDate!=null&&ToDate!="")
          {
             if(!check.test(ToDate))
             {
               alert(Message.WrongDate);
               $("#<%=textBoxEndDate.ClientID %>").val("");
               return false;
             }
          }
          if((FromDate!=null&&FromDate!="")&&(ToDate!=null&&ToDate!=""))
          {
            if(ToDate<FromDate)
            {
               alert(Message.ToLaterThanFrom);
               $("#<%=textBoxEndDate.ClientID %>").val("");
               return false;
            }
          }
          return true;
       }
            //單位樹
            function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
            {
                var windowWidth=500,windowHeight=600;
	            var X=(screen.availWidth-windowWidth)/2;
	            var Y=(screen.availHeight-windowHeight)/2;
	            var Revalue = window.showModalDialog("../HRM/EmployeeData/TreeDataPickForm.aspx?modulecode=" + moduleCode, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
	            if(Revalue!=undefined)
	            {	
	                var arrValue=Revalue.split(";");
			            document.all(ReturnValueBoxName).value=arrValue[0];
			            if(arrValue.length>1)
			            {
			                document.all(ReturnDescBoxName).innerText=arrValue[1];
			            }
	            }
            }
            //驗證數據
            function CheckData()
            {   
                if($("#<%=textBoxEmployeeNo.ClientID%>").val()=="")
                {
                   alert(Message.ErrWorknoNull);
                   return false;
                }
                if($("#<%=textBoxLocalName.ClientID%>").val()=="")
                {
                   alert(Message.LocalNameNotNull);
                   return false;
                }
                if($("#<%=textBoxDeputyWorkNo.ClientID%>").val()=="")
                {
                    alert(Message.ErrDeputyNoNotNll);
                   return false;
                }
                if($("#<%=textBoxDeputyName.ClientID%>").val()=="")
                {
                   alert(Message.ErrDeputyNameNll);
                   return false;
                }
                if($("#<%=textBoxDName.ClientID%>").val()=="")
                {
                   alert(Message.DepCodeNotNull);  
                   return false;
                }
                if($("#<%=textBoxDCode.ClientID%>").val()=="")
                {
                   alert(Message.DepCodeNotNull);
                   return false;
                }
                if($("#<%=textBoxDeputyNotes.ClientID%>").val()=="")
                {
                   alert(Message.ErrNotesNull);
                   return false;
                }
                if(!CheckDate())
                {
                   return false;
                }
                if($("#<%=textBoxStartDate.ClientID%>").val()==""||$("#<%=textBoxEndDate.ClientID%>").val()=="")
                {  alert(Message.DeputyDateNotNll);
                   return false;
                }
                if($("#<%=ddlLeaveType.ClientID%>").val()=="")
                {   
                    alert(Message.ErrDeputyReason);
                    return false;
                }
                return true;
            }
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
        <input id="HiddenDeputyNotes" type="hidden" name="HiddenDeputyFlag" runat="server" />
        <input id="HiddenroleCode" type="hidden" name="HiddenroleCode" runat="server" />
        <input id="HiddenDeputyWorkNo" type="hidden" name="HiddenDeputyWorkNo" runat="server" />
    <div style="width: 100%;">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit" runat="server"></asp:Label>
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
        </div>
        <div id="div_select" style="width: 100%">
            <table class="table_data_area">
                <tr class="tr_data_2" id="tr_log" runat="server">
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lblDeputyLog" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="ddlDeputyLog" runat="server" Width="50%" 
                            onselectedindexchanged="ddlDeputyLog_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList> 
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lbl_empno" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="textBoxEmployeeNo" runat="server" Width="50%" 
                            CssClass="input_textBox_1" ontextchanged="textBoxEmployeeNo_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lbl_name" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="textBoxLocalName" runat="server" Width="50%" CssClass="input_textBox_1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lblDeputy" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="textBoxDeputyWorkNo" runat="server" Width="50%" 
                            CssClass="input_textBox_1" ontextchanged="textBoxDeputyWorkNo_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    </td>
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="gvHeadDeputyName" runat="server"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="textBoxDeputyName" runat="server" Width="50%" CssClass="input_textBox_1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lblDeputUnit" runat="server"></asp:Label>
                    </td>
                    <td colspan="3">
                    <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="textBoxDCode" runat="server" CssClass="input_textBox_1" Style="display: none"></asp:TextBox>
                                </td>
                                <td width="50%">
                                    <asp:TextBox ID="textBoxDName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                </td>
                                <td style="cursor: hand">
                                    <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td>
                        &nbsp;
                        <asp:Label ID="gvHeadProxyNotes" runat="server"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="textBoxDeputyNotes" runat="server" Width="50%" CssClass="input_textBox_1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td>
                        &nbsp;
                        <asp:Label ID="lblDeputyDate" runat="server"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="textBoxStartDate" runat="server" Width="20%" CssClass="input_textBox_1" onchange="return CheckDate()"></asp:TextBox>
                        &nbsp;&nbsp;~&nbsp;
                        <asp:TextBox ID="textBoxEndDate" runat="server" Width="20%" CssClass="input_textBox_1"  onchange="return CheckDate()"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td>
                        &nbsp;
                        <asp:Label ID="gvLeaveTypeName" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLeaveType" runat="server" Width="50%" CssClass="input_textBox_1">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td>
                        &nbsp;
                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="textBoxRemark" runat="server" Width="50%" CssClass="input_textBox_1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table>
                            <tr>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                    CssClass="input_linkbutton" OnClientClick="return CheckData();" onclick="btnSave_Click">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnEditReturn %>"
                                                    CssClass="input_linkbutton" OnClientClick="return Return();">
                                                </asp:LinkButton>
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
</body>
</html>
