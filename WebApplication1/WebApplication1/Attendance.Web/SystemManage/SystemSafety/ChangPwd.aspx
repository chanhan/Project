<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangPwd.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.ChangPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../JavaScript/jquery-ui-custom.js"></script>

    <script>
     function  checkpwd()
     {    
           var oldPwd = $.trim($("#<%=txtOldPwd.ClientID %>").val());
           var newPwd1 = $.trim($("#<%=txtNewPwd.ClientID %>").val());
           var newPwd2 = $.trim($("#<%=txtNewPwdAgain.ClientID %>").val());
           if(oldPwd.length>0)
           {
              if(newPwd1.length>0)
              {
                 if(newPwd2.length>0)
                 {
                   if(newPwd1==newPwd2)
                   {
                     if(newPwd1.length >= 8 && /[a-zA-Z]+/.test(newPwd1) && /[0-9]/.test(newPwd1))
                     {
                       if(oldPwd!=newPwd1)
                       {
                             $.ajax({
                                        type: "post", url: "ChangPwd.aspx", dataType: "text", data: {Password: oldPwd, newPassword: newPwd1 },
                                        success: function(msg) {
                                            if (msg==1) {alert(Message.UpdateSuccess); } 
                                            else if(msg==2)
                                            {
                                            alert(Message.OldPwdNotTrue);
                                            }
                                            else
                                            {
                                              alert(Message.UpdateFailed);
                                            }
                                          }
                           });          
                       }
                       else
                       {
                          alert(Message.NewPasswordEqualsOld);
                       }
                     }
                     else
                     {
                          alert(Message.NewPasswordNotValid);
                     }
                   }
                   else
                   {
                      alert(Message.TwoNewPasswordNotEqual);
                   }
                 }
                 else
                 {
                    alert(Message.NewPwdAgainNotNull);
                 }
              }
              else
              {
                 alert(Message.NewPwdNotNull);
              }
           }
           else
           {
             alert(Message.OldPwdNotNull);
           }
           return false;
           
      }
      function checkmail()
      {
        var str1=/^\d{5}$/;
        var str2=/^\d{11}$/
        var mail = $.trim($("#<%=txtMail.ClientID %>").val());
        var tel = $.trim($("#<%=txtTel.ClientID %>").val());
        var phone = $.trim($("#<%=txtPhone.ClientID %>").val());
        if(mail.length>0)
        {
           $.ajax({
            type: "post", url: "ChangPwd.aspx", dataType: "text", data: {Mail: mail, Tel: tel,Phone:phone },
                                        success: function(msg) { 
                                            if (msg==1) {alert(Message.UpdateSuccess); } 
                                            else
                                            {
                                              alert(Message.UpdateFailed);
                                            }
                                          }
                                    });                     
        }
        else
        {
          alert(Message.MailNotNull);
          return false;
        }    
      }
         $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_select").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_select").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
       });
        $(function(){
        $("#tr_show").toggle(
            function(){
                $("#div_select2").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_select2").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlShowPanel" runat="server">
            <div>
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
            <div id="div_select">
                <table width="100%">
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblOldPwd" runat="server" Text="舊密碼*" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOldPwd" runat="server" Width="60%" TextMode="Password"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblNewPwd" runat="server" Text="新密碼*" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtNewPwd" runat="server" Width="60%" TextMode="Password"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblNewPwdAgain" runat="server" Text="確認新密碼*" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtNewPwdAgain" runat="server" Width="60%" TextMode="Password"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnComment" runat="server" class="button_2" OnClientClick=" return checkpwd()">
                            </asp:Button>
                            <asp:Button ID="btnReset" runat="server" class="button_2" onclintClick="javascript:location.href='ChangPwd.aspx'">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <asp:Label ID="lblResion" Text="密碼長度不能小於8位，且必須由數字和字母組成" runat="server" Style="font-size: 11pt;
                                color: Red; font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%">
                <tr style="width: 100%;">
                    <td>
                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                            <tr style="width: 100%;" id="tr_show">
                                <td style="width: 100%;" class="tr_title_center">
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
                                <td style="width: 22px;">
                                    <img class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="div_select2">
                <table width="100%">
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblMail" runat="server" Text="電子郵箱*" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtMail" runat="server" Width="60%"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblTel" runat="server" Text="本人分機號碼" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtTel" runat="server" Width="60%"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%">
                            <asp:Label ID="lblPhone" runat="server" Text="手機號碼" Style="color: Blue;"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPhone" runat="server" Width="60%"></asp:TextBox>
                        </td>
                        <td width="50%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" class="button_2" runat="server" OnClientClick="return checkmail()"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
