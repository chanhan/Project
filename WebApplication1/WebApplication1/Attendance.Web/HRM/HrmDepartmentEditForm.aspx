<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmDepartmentEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.HrmDepartmentEditForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HrmDepartmentEditForm</title>

    <script src="../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--  
    $(function(){
    $("#tr_edit").toggle(function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
    $("#div_select").hide()},function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
    $("#div_select").show()});
    })  
		function CheckButton()
        {
           var DepName=document.getElementById("txtDepName").value;
           var OrderID=document.getElementById("txtOrderID").value;
           if(DepName.length==0)
           {
           alert(Message.DepNameNotNull);
           return false;
           }
           else if(OrderID.length==0)
           {
             alert(Message.OrderIdNotNull);
             return false;
           }
           else
           {
	        return confirm(Message.ConfirmBatchConfirm); 
	       }
        }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server" />
    <input id="HiddenLevelCode" type="hidden" name="HiddenLevelCode" runat="server" />
    <input id="HiddenNextLevelCode" type="hidden" name="HiddenNextLevelCode" runat="server" />
    <input id="HiddenMaxDepCode" type="hidden" name="HiddenMaxDepCode" runat="server" />
    <input id="HiddenParentDepCode" type="hidden" name="HiddenParentDepCode" runat="server" />
    <input id="HiddenHead" type="hidden" name="HiddenHead" runat="server" />
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
                                        <tr>
                                            <td width="40%">
                                                &nbsp;
                                                <asp:Label ID="lblDepName" runat="server" ForeColor="Blue">DepName*:</asp:Label>
                                            </td>
                                            <td width="60%">
                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblOrderID" runat="server" ForeColor="Blue">CodeHead*:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrderID" runat="server" Width="100%" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                                    onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblLevelName" runat="server">LevelName:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLevelName" runat="server" Width="100%"></asp:TextBox>
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
                            <td>
                                <asp:Panel ID="pnlShowPanel" runat="server">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClick="btnAdd_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClick="btnModify_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click"
                                        OnClientClick="return CheckButton()"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button_1" OnClick="btnCancel_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                        OnClientClick="return CheckButton()"></asp:Button>
                                    <asp:Button ID="btnDisable" runat="server" CssClass="button_1" OnClick="btnDisable_Click"
                                        OnClientClick="return CheckButton()"></asp:Button>
                                    <asp:Button ID="btnEnable" runat="server" CssClass="button_1" OnClick="btnEnable_Click"
                                        OnClientClick="return CheckButton()"></asp:Button>
                                    <asp:Button ID="btnChange" runat="server" CssClass="button_large" OnClick="btnChange_Click">
                                    </asp:Button>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="div_change" runat="server" visible="false">
                <td colspan="2">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="20%">
                                &nbsp;
                                <asp:Label ID="lblNewDepName" runat="server" ForeColor="Blue">NewDepName:</asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtNewDepName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 45px">
                                <asp:Button ID="btnChangedSave" runat="server" CssClass="button_1" OnClick="btnChangedSave_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript"><!--    
        document.getElementById("txtLevelName").readOnly=true;
        if(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify")
        {
            document.getElementById("txtDepname").readOnly=false;
            document.getElementById("txtOrderid").readOnly=false;
            document.getElementById("txtDepname").focus();
            document.getElementById("txtDepname").select();
        }
        else
        {
            document.getElementById("txtDepname").readOnly=true;
            document.getElementById("txtOrderid").readOnly=true;
        }
    --></script>

</body>
</html>
