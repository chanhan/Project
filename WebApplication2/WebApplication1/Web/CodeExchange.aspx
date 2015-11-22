<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeExchange.aspx.cs" Inherits="Maticsoft.Web.CodeExchange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
            font-size: 12px;
        }
        #gvCode td
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                </td>
                <td width="80%">
                    <table width="100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>输入</legend>手机号<asp:TextBox ID="phone" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                                    <asp:Label ID="labphone" style=" color:red" runat="server" Text=""></asp:Label>
                                    <br />
                                    兑换码<asp:TextBox ID="code" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button2" runat="server" Text="查询" OnClick="Button2_Click" />
                                    <asp:Label ID="labcode" style=" color:red" runat="server" Text=""></asp:Label>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>兑换码信息 </legend>
                                    <asp:GridView ID="gvCode" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" Width="100%">
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="PhoneNumber" HeaderText="手机号" />
                                            <asp:BoundField DataField="Code" HeaderText="验证码" />
                                            <asp:BoundField DataField="Remark" HeaderText="标识" />
                                            <asp:BoundField DataField="IsValidated" HeaderText="验证" />
                                            <asp:BoundField DataField="ValidatedTime" HeaderText="验证日期" />
                                            <asp:BoundField DataField="IsExchanged" HeaderText="兑换" />
                                            <asp:BoundField DataField="ExchangeTime" HeaderText="兑换日期" />
                                        </Columns>
                                        <RowStyle />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
