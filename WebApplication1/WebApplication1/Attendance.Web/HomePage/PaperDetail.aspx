<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaperDetail.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HomePage.PaperDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self" />
    <link href="../CSS/CommonCssForIndex.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function SavePaper(status) {
            var content = "";
            var paperid = $("#hidPaperSeq").val();
            $(":checked").each(function() {
                content += this.value + "§";
            });
            $.ajax({
                anysc: false, type: "POST", url: "PaperDetail.aspx", data: { answerContent: content, answerStatus: status, paperId: paperid }, dataType: "text",
                success: function(item) {
                    if (item != -1) {
                        $("#lblMessage").text(Message.SaveSuccess);
                        if (status = "Submit") {
                            $("#btnTempSave").css("display", "none");
                            $("#btnSubmit").css("display", "none");
                        }
                    }
                    else {
                        $("#lblMessage").text(Message.SaveFailed);
                    }
                }
            });
            return false;
        }
    </script>

    <style type="text/css">
        .PaperDetail
        {
            background-color: #fff;
            font-size: 14px;
        }
        .PaperDetail table th
        {
            height: 28px;
            background-color: #f5f5f5;
            padding-left: 30px;
        }
        .PaperDetail table td
        {
            height: 25px;
            padding-left: 20px;
        }
    </style>
</head>
<body style="background: #fff; text-align:center;">
    <form id="form1" runat="server">
    <div class="contentTitle" style="margin-top: 5px;">
        <asp:Literal ID="litPaperTitle" runat="server" Text=""></asp:Literal>
    </div>
    <asp:Panel ID="pnlEmpBasic" runat="server">
        <table cellpadding="0" cellspacing="0" class="TableContainer" width="100%">
            <tr>
                <td class="tbTopLeft">
                </td>
                <td class="tbTopMiddle">
                </td>
                <td class="tbTopRight">
                </td>
            </tr>
            <tr>
                <td class="tbMiddleLeft">
                    &nbsp;
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" class="PaperDetail" width="100%">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hidPaperSeq" runat="server" />
                                <asp:Table ID="tabContent" runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                </asp:Table>
                            </td>
                        </tr>
                        <tr style="background-color: #f5f5f5; height: 30px;">
                            <td align="center">
                                <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="450px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 45px;">
                            <td align="center">
                                <table>
                                    <tr>
                                        <td style="width: 45px">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnTempSave" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnTempSave %>"
                                                            OnClientClick='return SavePaper("Temp");' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnSubmit" CssClass="input_linkbutton" runat="server" Text="<%$Resources:ControlText,btnSubmit %>"
                                                            OnClientClick='return SavePaper("SavePaper");' />
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
                <td class="tbMiddleRight">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tbBottomLeft">
                </td>
                <td class="tbBottomMiddle">
                </td>
                <td class="tbBottomRight">
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
