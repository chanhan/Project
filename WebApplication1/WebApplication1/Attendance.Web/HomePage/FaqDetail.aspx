<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaqDetail.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HomePage.FaqDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../CSS/CommonCssForIndex.css" rel="stylesheet" type="text/css" />
</head>
<body>
      <form id="form1" runat="server">
    <div class="NoticeInfoHead">
        <div>
            <h1>
                <asp:Literal ID="litFaqTitle" runat="server" ></asp:Literal></h1>
        </div>
    </div>
    <div class="FaqQuestionInfoBG">
        <table width="100%">
            <tr>
                <td style="width: 50px; height: 25px">
                </td>
                <td align="left">
                    <asp:Label ID="lblFaqDate" runat="server" ></asp:Label>
                    <asp:Literal ID="litFaqDate" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Label ID="lblFaqTypeName" runat="server" ></asp:Label>
                    <asp:Literal ID="litFaqTypeName" runat="server"></asp:Literal>
                </td>
                <td style="width:50px">
                </td>
            </tr>
            <tr>
                <td style="width: 50px; height: 25px">
                </td>
                <td align="left">
                    <asp:Label ID="lblFaqEmpNo" runat="server" ></asp:Label>
                    <asp:Literal ID="litEmpNo" runat="server"></asp:Literal>
                </td>
                <td align="left">
                   <asp:Label ID="lblFaqEmpName" runat="server" ></asp:Label>
                   <asp:Literal ID="litEmpName" runat="server"></asp:Literal>
                </td>
                <td>
                </td>
            </tr>
            <tr >
                <td style="width: 50px; height: 25px">
                </td>
                <td align="left">
                    <asp:Label ID="lblEmpPhone" runat="server" ></asp:Label>
                    <asp:Literal ID="litEmpPhone" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Label ID="lblEmpEmail" runat="server" ></asp:Label>
                    <asp:Literal ID="litEmpEmail" runat="server"></asp:Literal>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="2" style="padding-top:10px;">
                    <asp:Label ID="lblFaqContent" runat="server" ></asp:Label>
                    <asp:Literal ID="litFaqContent" runat="server"></asp:Literal>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="FaqAnswerInfoBG">
        <table width="100%">
            <tr>
                <td style="width:50px;height:25px;">
                </td>
                <td align="left">
                    <asp:Label ID="lblAnswerDate" runat="server" ></asp:Label>
                    <asp:Literal ID="litAnswerDate" runat="server"></asp:Literal>
                </td>
                <td align="left">
                    <asp:Label ID="lblAnswerName" runat="server" ></asp:Label>
                    <asp:Literal ID="litAnswerName" runat="server"></asp:Literal>
                </td>
                <td style="width:50px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="2" style="padding-top:10px;">
                    <asp:Label ID="lblAnswerContent" runat="server" ></asp:Label>
                    <asp:Literal ID="litAnswerContent" runat="server"></asp:Literal>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
