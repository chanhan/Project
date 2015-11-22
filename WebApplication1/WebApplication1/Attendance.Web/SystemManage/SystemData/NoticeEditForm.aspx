<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemData.NoticeEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script>
    		function Return()
		{
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.getElementById("div_2").style.display="";
            window.parent.document.getElementById("PanelData").style.display="";
            return false;
		}
		        function down(f) { $("#frameDown").attr("src", "NoticeManager.aspx?path=" + encodeURI(f)); }		
        function del(i) { 
            $(i).parent("div").remove(); 
            var v = ""; 
            $("#annexCell a").each(function() { v += $(this).text() + "|"; });
            $("#hidAnnexFilePath").val(v); 
            if (!v) {$("#annexRow").hide();} }
            
            
    $(function(){
        $("#<%=btnAddFile.ClientID %>").click(function() {
                $("<input type='file' name='impFile' style='width:350px; display:block;border:solid 1px silver;' />").appendTo("#MyFile");
                return false;
            });
            
                        $("#<%=btnSave.ClientID %>").click(function() {
                var valid = true; $(".messageLabel").text("");
                $("#<%=txtNoticeDate.ClientID %>,#<%=txtNoticeAuthor.ClientID %>,#<%=txtAuthorTel.ClientID %>,#<%=txtNoticeDept.ClientID %>,#<%=txtNoticeTitle.ClientID %>,#<%=txtNoticeContent.ClientID %>,select").each(function() {
                    if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { valid = false; $(this).css("border-color", "#ff6666"); }
                }); if ($.browser.msie && $.browser.version < 8) { $("select").each(function() { if ($(this).val()) { $(this).css("background-color", "#ffffff"); } else { $(this).css("background-color", "#ffaaaa"); } }) }
                if (valid && $("#<%=hidOperate.ClientID %>").val() == "Add") {
                    var today = new Date(); var ndinfo = $("#<%=txtNoticeDate.ClientID %>").val().split('/');
                    if (today.getFullYear() != parseInt(ndinfo[0]) || (today.getMonth() + 1) != parseInt(ndinfo[1]) || today.getDate() != parseInt(ndinfo[2])) { valid = false; $(".messageLabel").text(Message.NoticeDateMustBeToday); }
                }
                return valid;
            });
            
            
      noticeid=$('#<%=hidNoticeId.ClientID %>').val();
      operate=$('#<%=hidOperate.ClientID %>').val();
        $.ajax({
                type: "POST", url: "NoticeEditForm.aspx", data: { NoticeId: noticeid,Operate:operate }, dataType: "json",async: false,
                success: function(item) {
                       $("#<%=chkActiveFlag.ClientID %>").attr("checked", item.ActiveFlag == "Y");
                    $.each(item, function(k, v) { $(":text[id$='txt" + k + "'],select[id$='ddl" + k + "'],input:hidden[id$='hid" + k + "']").val(v); });
                    $("#<%=txtNoticeDate.ClientID %>").val($.jsonDateToString(item.NoticeDate));
                    $("#<%=txtNoticeContent.ClientID %>").val(item.NoticeContent);
                      if (item.AnnexFilePath && item.AnnexFilePath != 'null') {
                        $("#annexRow").show(); 
                        var files = item.AnnexFilePath.split('|'); 
                        $("#annexCell").empty();
                        $(files).each(function(i) {
                            if (files[i]) {
                                $("<div id='AddFile'><a style='text-decoration:none;' href=\"javascript:down('" + files[i] + "')\">" + files[i] + "</a><img src='../../CSS/Images_new/deleteItem.gif' onclick='del(this)' /></div>").appendTo($("#annexCell"));
                            }
                        });
                    } 
                    else { 
                             $("#hidAnnexFilePath").val(null);
                             $("#annexRow").hide();
                             $("#annexCell").empty(); 
                         }
                }
            });
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
    });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblLastNotice" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;">
                    <div id="img_edit">
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
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
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlNoticeInfo" runat="server">
                                                <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                                <asp:HiddenField ID="hidNoticeId" runat="server" />
                                                <asp:HiddenField ID="hidAnnexFilePath" runat="server" />
                                                <asp:HiddenField ID="hidBrowseTimes" runat="server" />
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr style="background-color: #f5f5f5;">
                                                        <td>
                                                            <asp:Label ID="lblNoticeDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNoticeDate" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblNoticeTypeId" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNoticeTypeId" runat="server" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #fff;">
                                                        <td>
                                                            <asp:Label ID="lblNoticeAuthor" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNoticeAuthor" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAuthorTel" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAuthorTel" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #f5f5f5;">
                                                        <td>
                                                            <asp:Label ID="lblNoticeDept" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNoticeDept" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkActiveFlag" runat="server" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #fff;">
                                                        <td>
                                                            <asp:Label ID="lblNoticeTitle" runat="server"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNoticeTitle" runat="server" Width="590px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="annexRow" style="background-color: #f5f5f5; display: none;">
                                                        <td style="vertical-align: top; padding-top: 5px;">
                                                            <asp:Label ID="lblAnnexFile" runat="server"></asp:Label>
                                                            <iframe id="frameDown" style="display: none;"></iframe>
                                                        </td>
                                                        <td id="annexCell" colspan="3" style="vertical-align: top; padding-top: 5px;">
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #f5f5f5;">
                                                        <td style="vertical-align: top; padding: 5px 0 5px 0">
                                                            <asp:Label ID="lblAnnexFilePath" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; padding: 5px 0 5px 0">
                                                            <p id="MyFile">
                                                                <input id="File1" type='file' class='typeFile' name='impFile' runat='server' style='width: 350px;
                                                                    display: block; border: solid 1px silver;' />
                                                            </p>
                                                        </td>
                                                        <td style="vertical-align: top; padding-top: 5px;">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 45px">
                                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                                            background-repeat: no-repeat; background-position-x: center; width: 85px; text-align: center;
                                                                            font-size: 13px;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnAddFile" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnAddFile %>"
                                                                                        ToolTip="Authority Code:AddFile">
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #f5f5f5;">
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #fff;">
                                                        <td valign="top">
                                                            <asp:Label ID="lblNoticeContent" runat="server"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtNoticeContent" runat="server" Width="590px" TextMode="MultiLine"
                                                                Height="120px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #f5f5f5;">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="480px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #fff; height: 45px;">
                                                        <td colspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 45px">
                                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                                            background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                                            font-size: 13px;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnSave" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                                        ToolTip="Authority Code:Save" OnClick="btnSave_Click">
                                                                                    </asp:LinkButton>
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
                                                                                    <asp:LinkButton ID="btnReturn" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnReturn %>"
                                                                                        ToolTip="Authority Code:Return" OnClientClick="return Return();">
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
                                            </asp:Panel>
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
