<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageNavigator.ascx.cs"  Inherits="GDSBG.MiABU.Attendance.Web.ControlLib.PageNavigator" %>


<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>


<table>
    <tr height="20">
        <td>
            <nobr><asp:Label ID="lblTotalRecordsTitle" runat="server"></asp:Label>
            <asp:Label ID="lblTotalrecords" runat="server" Text=""></asp:Label></nobr>
        </td>
        <td>
            <nobr><asp:ImageButton ID="imgbtnPrevious"   runat="server" ImageUrl="../CSS/Images/prev.png" ToolTip="Previous Page" />
            <asp:ImageButton ID="imgbtnNext"  runat="server" ImageUrl="../CSS/Images/next.png" ToolTip="Next Page"/></nobr>
        </td>
        <td>

            <nobr><igtxt:WebNumericEdit id="WebNumericEditCurrentpage" runat="server" Height="16px" Width="30px" CssClass="input_textBox" DataMode="Int"></igtxt:WebNumericEdit></nobr>
        </td>
        <td>
            <nobr>&nbsp;/&nbsp;
            <asp:Label ID="lblTotalpage" runat="server" Text=""></asp:Label></nobr>
        </td>
        <td>
            <asp:ImageButton ID="imgbtnGoto" runat="server" ImageUrl="../CSS/Images/accept.png" 
                ToolTip="Go To" />
        </td>
    </tr>
</table>