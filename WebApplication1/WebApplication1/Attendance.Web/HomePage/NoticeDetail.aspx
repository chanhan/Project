<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeDetail.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HomePage.NoticeDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/CommonCssForIndex.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function font(size) { $("p").css("font-size", size); }
        function down(f) { $("#frameDown").attr("src", "NoticeDetail.aspx?path=" + encodeURI(f)); }
    </script>

</head>
<body style="background-color: #e5eddc;">
    <form id="form1" runat="server">
    <div class="NoticeInfoHead">
        <div>
            <h1>
                <asp:Literal ID="litNoticeTitle" runat="server"></asp:Literal>
            </h1>
        </div>
    </div>
    <div class="NoticeInfoTitleBG">
        <table width="100%" style="background: #c6c9c2;">
            <tr>
                <td style="width: 90px; height: 25px;">
                </td>
                <td align="left" style="width: 45%;">
                    <asp:Literal ID="litNoticeDateTag" runat="server" Text="<%$Resources:ControlText,lblNoticeDate %>"></asp:Literal><asp:Literal
                        ID="litNoticeDate" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Literal ID="litDept" runat="server" Text="<%$Resources:ControlText,lblNoticeDept %>"></asp:Literal><asp:Literal
                        ID="litNoticeDept" runat="server"></asp:Literal>
                </td>
                <td style="width: 80px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" style="height: 25px;">
                    <asp:Literal ID="litAuthor" runat="server" Text="<%$Resources:ControlText,lblNoticeAuthor %>"></asp:Literal><asp:Literal
                        ID="litNoticeAuthor" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Literal ID="litTel" runat="server" Text="<%$Resources:ControlText,lblAuthorTel %>"></asp:Literal><asp:Literal
                        ID="litAuthorTel" runat="server"></asp:Literal>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" style="height: 25px;">
                    <asp:Literal ID="litBrowseTimesTag" runat="server"></asp:Literal><asp:Literal ID="litBrowseTimes"
                        runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Literal ID="litFontSize" runat="server"></asp:Literal>
                    <asp:HyperLink ID="HyperLink1" NavigateUrl='javascript:font("large");' runat="server"
                        Text="<%$Resources:ControlText,lnkFontLarge %>"></asp:HyperLink>
                    <asp:HyperLink ID="HyperLink2" NavigateUrl='javascript:font("medium");' runat="server"
                        Text="<%$Resources:ControlText,lnkFontMedium %>"></asp:HyperLink>
                    <asp:HyperLink ID="HyperLink3" NavigateUrl='javascript:font("small");' runat="server"
                        Text="<%$Resources:ControlText,lnkFontSmall %>"></asp:HyperLink>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="2">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="vertical-align: top;" align="left">
                                <asp:Literal runat="server" ID="litAnnexFile" Text="附件："></asp:Literal>
                            </td>
                            <td class="filelink">
                                <asp:PlaceHolder ID="phFiles" runat="server"></asp:PlaceHolder>
                                <iframe id="frameDown" style="display: none;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="NoticeInfoContent">
        <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
            <tr>
                <td style="width: 20px;">
                    &nbsp;
                </td>
                <td align="left">
                    <p id="content">
                        <asp:Literal ID="litNoticeContent" runat="server"></asp:Literal>
                    </p>
                </td>
                <td style="width: 20px;">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
