<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.ImportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr style="background-color: #f5f5f5;">
            <td align="right" style="width: 30%">
                <asp:Label ID="lblAnnexFilePath" runat="server" Text="附件："></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:FileUpload ID="fileAnnexFile" runat="server" Width="400px" Style="border: solid 1px Silver;" />
            </td>
            <td style="width: 30%">
                <asp:Button ID="btnImportExcel" runat="server" Text="導入" OnClick="btnImportExcel_Click" />
            </td>
        </tr>
        <tr style="background-color: #fff;">
            <td colspan="3" align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="420px"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
