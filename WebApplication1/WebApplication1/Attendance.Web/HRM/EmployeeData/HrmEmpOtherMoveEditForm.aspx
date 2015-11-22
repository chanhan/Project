<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpOtherMoveEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.EmployeeData.HrmEmpOtherMoveEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
    });

     function showData(EmployeeNo,Privileged)
     {
       $.ajax({
                type: "POST", url: "HrmEmpOtherMoveEditForm.aspx", data: {EmpNO :EmployeeNo,hasPrivileged:Privileged,flag:'showData'}, dataType: "json",
                success: function(item) {
            //   if(document.getElementById("HiddenExist").value=="N")
            //   {
                    document.getElementById("txtEmployeeNo").value=item.Workno==null?"":item.Workno;
                    document.getElementById("txtLocalname").value=item.Localname==null?"":item.Localname;
                    document.getElementById("txtDepname").value=item.Depname==null?"":item.Depname;
                    document.getElementById("txtSex").value=item.Sex==null?"":item.Sex;
                    document.getElementById("txtLevelname").value=item.Levelname==null?"":item.Levelname;
                    document.getElementById("txtManagername").value=item.Managername==null?"":item.Managername;
                    document.getElementById("HiddenDepCode").value=item.Depcode==null?"":item.Depcode;
                    document.getElementById("HiddenDepName").value=item.Depname==null?"":item.Depname;
                    document.getElementById("HiddenOverTimeType").value=item.Overtimetype==null?"":item.Overtimetype;
                    document.getElementById("HiddenOverTimeTypeName").value=item.Overtimetypename==null?"":item.Overtimetypename
                    document.getElementById("HiddenPostCode").value=item.Postcode==null?"":item.Postcode;
                    document.getElementById("HiddenPostName").value=item.Postname==null?"":item.Postname;
                    document.getElementById("HiddenPersonTypeCode").value=item.Persontypecode==null?"":item.Persontypecode;
                    document.getElementById("HiddenPersonTypeName").value=item.Persontypename==null?"":item.Persontypename;
           //      }
             //  else if(document.getElementById("HiddenExist").value=="Y")
               //  {
            //        alert(Message.WorkNONotExist);
            //    }
                }
            });
     }
    function GetEmp()
		{
            var EmployeeNo=document.getElementById("txtEmployeeNo").value;
            var Privileged=document.getElementById("HiddenPrivileged").value;
            
            if(EmployeeNo.length>0&&document.getElementById("ProcessFlag").value=="Add")
            {
                     $.ajax({
                type: "POST", url: "HrmEmpOtherMoveEditForm.aspx", data: {EmpNO :EmployeeNo,hasPrivileged:Privileged,flag:'checkUser'}, dataType: "text",
                success: function(item) {
                    showData(EmployeeNo,Privileged);
                if(item=="N")
                 {
                    alert(Message.WorkNONotExist);
                }
                }
            });
            return false;
            }
		}
            function Return()
        {
            window.parent.document.all.divTop.style.display="";
            window.parent.document.all.divEdit.style.display="none";
           window.parent.document.all.dataPanel.style.display="";
            window.parent.document.all.div_select2.style.display="";
               
            return false;
        }    
                function onChangeMoveType(strMoveTypeCode)
		{
    	    if(!document.getElementById("HiddenState").value=="1")
            {	  
			    if(strMoveTypeCode=="1")
			    {			    
    		        document.all("HiddenBeforeValue").value =document.all("HiddenOverTimeType").value;
		            document.all("txtBeforeValueName").value =document.all("HiddenOverTimeTypeName").value;				    
			    }else
                if(strMoveTypeCode=="2")
			    {			    
    		        document.all("HiddenBeforeValue").value =document.all("HiddenPersonTypeCode").value;
		            document.all("txtBeforeValueName").value =document.all("HiddenPersonTypeName").value;
			    }else
			    if(strMoveTypeCode=="3")
			    {			    
    		        document.all("HiddenBeforeValue").value =document.all("HiddenPostCode").value;
		            document.all("txtBeforeValueName").value =document.all("HiddenPostName").value;			    
			    }
		
			    document.all("txtAfterValueName").value = "";
			}		
		}  
		        function onclickAfterValue(strMoveTypeCode)
		{		    
			if(strMoveTypeCode=="1")
			{			    
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("OverTimeType"));
			}else
            if(strMoveTypeCode=="2")
			{			   
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("PersonType"));
			}else
			if(strMoveTypeCode=="3")
			{			   
			    document.all("ImageAfterValue").setAttribute("onclick",onClickAfterValue("Post"));
			}
		      else
			    {
			    alert(Message.ChooseOtherMoveTypeFirst);
			    }	
    			
		} 
				function onClickAfterValue(sType)
		{			
			GetDataValueApp('txtAfterValueName',sType,'HiddenAfterValue');
		}   
		function GetDataValueApp(ReturnValueBoxName,DataType,ReturnDescBoxName)
{
	var windowWidth=500,windowHeight=380;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("SingleDataPickForm.aspx?DataType="+DataType+"&r="+ Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
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

function checkData()
{
    if($.trim($('#<%=txtEmployeeNo.ClientID %>').val()))
    {
     if ($('#<%=ddlMoveType.ClientID %>').get(0).selectedIndex!=0)
     {
     if ($.trim($('#<%=txtEffectDate.ClientID %>').val()))
     {
     if($.trim($('#<%=txtAfterValueName.ClientID %>').val()))
     {
     return true;
     }
     else{
     alert( Message.AfterValueNameNotNull);
        return false;
     }
     }
     else
     {
      alert( Message.EffDateNotNull);
        return false;
     }
     }
     
     else
     {
     alert( Message.MoveTypeNotNull);
       return false;
     }
    }
    else
    {
     alert(Message.EmpNoNotNull);
     return false;
    }
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenExist" type="hidden" name="HiddenExist" runat="server" value="N">
    <input id="HiddenPrivileged" type="hidden" name="HiddenPrivileged" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenMoveOrder" type="hidden" name="HiddenMoveOrder" runat="server">
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server"><%--新增完資料保留工號--%>
    <input id="HiddenMoveType" type="hidden" name="HiddenMoveType" runat="server">
    <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server">
    <input id="HiddenDepName" type="hidden" name="HiddenDepName" runat="server">
    <input id="HiddenOverTimeType" type="hidden" name="HiddenOverTimeType" runat="server">
    <input id="HiddenOverTimeTypeName" type="hidden" name="HiddenOverTimeTypeName" runat="server">
    <input id="HiddenPersonTypeCode" type="hidden" name="HiddenPersonTypeCode" runat="server">
    <input id="HiddenPersonTypeName" type="hidden" name="HiddenPersonTypeName" runat="server">
    <input id="HiddenPostCode" type="hidden" name="HiddenPostCode" runat="server">
    <input id="HiddenPostName" type="hidden" name="HiddenPostName" runat="server">
    <input id="HiddenState" type="hidden" name="HiddenState" runat="server">
    <input id="HiddenBeforeValue" type="hidden" name="HiddenBeforeValue" runat="server">
    <input id="HiddenAfterValue" type="hidden" name="HiddenAfterValue" runat="server">
    <div style="width: 100%">
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
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="tr_edit" style="width: 100%">
        <table style="width: 100%">
            <tr style="width: 100%">
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                    <asp:Panel ID="inputPanel" runat="server">
                                        <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                        <tr>
                                            <td class="td_label_view" width="12%">
                                                &nbsp;
                                                <asp:Label ID="lbllEmployeeNo" runat="server" ForeColor="Blue">EmployeeNo:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox" ></asp:TextBox>
                                            </td>
                                            <td class="td_label_view" width="13%">
                                                &nbsp;
                                                <asp:Label ID="lblName" runat="server">Name:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtLocalname" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label_view" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblSex" runat="server">Sex:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtSex" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label_view" width="12%">
                                                &nbsp;
                                                <asp:Label ID="lblDeptName" runat="server">Department:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtDepname" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label_view" width="13%">
                                                &nbsp;
                                                <asp:Label ID="lblLevel" runat="server">Level:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtLevelname" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label_view" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblManager" runat="server">Manager:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtManagername" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label" width="12%">
                                                &nbsp;
                                                <asp:Label ID="lbllblMoveType" runat="server" ForeColor="Blue">MoveType:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:DropDownList ID="ddlMoveType" runat="server" Width="100%" CssClass="input_textBox">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_label" width="13%">
                                                &nbsp;
                                                <asp:Label ID="lblBeforeValue" runat="server">BeforeValue:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtBeforeValueName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lbllblAfterValue" runat="server" ForeColor="Blue">AfterValue:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtAfterValueName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="td_label" style="cursor: hand">
                                                <asp:Image ID="ImageAfterValue" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif"></asp:Image>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label" width="12%">
                                                &nbsp;
                                                <asp:Label ID="lblEffectEffectDate" runat="server" ForeColor="Blue">EffectDate:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtEffectDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="13%">
                                                &nbsp;
                                                <asp:Label ID="lblMoveReason" runat="server">MoveReason:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtMoveReason" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="td_label" colspan="6">
                                            <table>
                                                <tr>
                                                    <td style="width: 45px">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                            background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                            font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnSave" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnSave%>"
                                                                        ToolTip="Authority Code:Save" onclick="btnSave_Click" OnClientClick="return checkData();"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 45px">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                            background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                            font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnReturn" runat="server" CssClass="input_linkbutton" Text="<%$Resources:ControlText,btnReturn%>"
                                                                        ToolTip="Authority Code:Return" OnClientClick="return Return();"></asp:LinkButton>
                                                                </td>
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
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
