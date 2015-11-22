<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NightAllowanceToExcel.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.Query.NightAllowanceToExcel" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>NightAllowanceToExcel</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="AutoNumber1" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
        width="100%" border="1">
        <tr>
            <td align="center" valign="middle" colspan="<%=getDays()+5%>" height="36">
                <font style="font-size: 20pt; color: black; font-family: 標楷體">
                    <asp:Label ID="lblReportTitle" runat="server" Text="Label"></asp:Label></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblLeaveQryDate" runat="server" Text="Label"></asp:Label>:<%=dateFrom%>
                ~<%=dateTo%>
            </td>
        </tr>
        <tr style="font-size: 10pt">
            <td align="center" width="1.5%" bgcolor="#000080" rowspan="2">
                <font color="#f0f8ff" style="font-weight: bold">
                    <asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label></font>
            </td>
            <td align="center" width="6%" bgcolor="#000080" rowspan="2">
                <font color="#f0f8ff" style="font-weight: bold">
                    <asp:Label ID="gvDName" runat="server" Text="Label"></asp:Label></font>
            </td>
            <td align="center" width="3%" bgcolor="#000080" rowspan="2">
                <font color="#f0f8ff" style="font-weight: bold">
                    <asp:Label ID="lblKQParamsWorkNo" runat="server" Text="Label"></asp:Label></font>
            </td>
            <td align="center" width="3%" bgcolor="#000080" rowspan="2">
                <font color="#f0f8ff" style="font-weight: bold">
                    <asp:Label ID="lblLocalName" runat="server" Text="Label"></asp:Label></font>
            </td>
            <td align="center" width="2%" bgcolor="#000080" rowspan="2">
                <font color="#f0f8ff" style="font-weight: bold">
                    <asp:Label ID="lblDay" runat="server" Text="Label"></asp:Label></font>
            </td>
            <% SetWeek();%>
        </tr>
        <tr style="font-size: 10pt">
            <%SetDays();%>
        </tr>
        <%SetExcel();%>
    </table>
    </form>
</body>
</html>
