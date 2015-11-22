<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallCenter.aspx.cs" Inherits="Maticsoft.Web.CallCenter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 73px;
        }
        .style2
        {
            width: 230px;
        }
        .style5
        {
            width: 169px;
        }
        .style6
        {
            width: 202px;
        }
        .style7
        {
            width: 170px;
        }
        #tabexchang td
        {
            text-align: center;
        }
        #gvDelivery td
        {
            text-align: center;
        }
        body
        {
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="">
        <table width="100%">
            <tr>
                <td>
                </td>
                <td width="80%">
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <fieldset>
                                    <legend>输入</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                手机号
                                            </td>
                                            <td>
                                                <asp:TextBox ID="phonenumber" runat="server"></asp:TextBox>
                                                <asp:Button ID="search" runat="server" Text="查询" OnClick="search_Click" />
                                                <asp:Label ID="labphone" Style="color: red" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <nobr>添加兑换码</nobr>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="code" runat="server" Width="439px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Label ID="labcode" Style="color: red" runat="server" Text=""></asp:Label>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <fieldset>
                                    <legend>姓名地址 </legend>
                                    <div>
                                        姓名<asp:TextBox ID="name" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        地址<asp:TextBox ID="address" runat="server" Width="439px"></asp:TextBox>
                                        <asp:Button ID="Button2" runat="server" Text="提交" OnClick="Button2_Click" />
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <fieldset>
                                    <legend>兑换详情</legend>
                                    <table id="tabexchang" runat="server" border="1" style="color: #333333; border-collapse: collapse;
                                        width: 100%;">
                                        <tr style="color: White; background-color: #006699; font-weight: bold;">
                                            <th class="style1">
                                                &nbsp;
                                            </th>
                                            <th class="style5">
                                                密
                                            </th>
                                            <th class="style6">
                                                净
                                            </th>
                                            <th class="style7">
                                                准
                                            </th>
                                            <th class="style2">
                                                宝护盖
                                            </th>
                                            <th class="style1">
                                                &nbsp;
                                            </th>
                                           
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                总数
                                            </td>
                                            <td class="style5">
                                                <asp:Label ID="SumMi" runat="server"></asp:Label>
                                            </td>
                                            <td class="style6">
                                                <asp:Label ID="SumJing" runat="server"></asp:Label>
                                            </td>
                                            <td class="style7">
                                                <asp:Label ID="SumZhun" runat="server"></asp:Label>
                                            </td>
                                            <td class="style2">
                                                <asp:Label ID="SumBao" runat="server"></asp:Label>
                                            </td>
                                            <td class="style1">
                                                &nbsp;
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                可用数
                                            </td>
                                            <td class="style5">
                                                <asp:Label ID="AvailableMi" runat="server"></asp:Label>
                                            </td>
                                            <td class="style6">
                                                <asp:Label ID="AvailableJing" runat="server"></asp:Label>
                                            </td>
                                            <td class="style7">
                                                <asp:Label ID="AvailableZhun" runat="server"></asp:Label>
                                            </td>
                                            <td class="style2">
                                                <asp:Label ID="AvailableBao" runat="server"></asp:Label>
                                            </td>
                                            <td class="style1">
                                                <asp:Button ID="Button3" runat="server" Enabled="false" Text="兑换" OnClick="Button3_Click" />
                                            </td>
                                           
                                        </tr>
                                    </table>
                                    &nbsp&nbsp
                                    <div>
                                        <asp:Label ID="Gift" Style="color: Red" runat="server"></asp:Label></div>
                                    &nbsp&nbsp<div>
                                        <asp:Label ID="Product" Style="color: Red" runat="server"></asp:Label></div>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <fieldset>
                                    <legend>快递详细信息 </legend>
                                    <asp:GridView ID="gvDelivery" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" Width="100%">
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="PhoneNumber" HeaderText="手机号" />
                                            <asp:BoundField DataField="DeliveryDate" HeaderText="快递日期" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="客户姓名" />
                                            <asp:BoundField DataField="Province" HeaderText="省" ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="City" HeaderText="市" ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Address" HeaderText="地址" ItemStyle-Width="250px" />
                                            <asp:BoundField DataField="CourierCompanyName" HeaderText="快递公司名" />
                                            <asp:BoundField DataField="CourierID" HeaderText="快递单号" ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="GiftPacks" HeaderText="便携礼包" />
                                            <asp:BoundField DataField="ProductPacks" HeaderText="正装奶粉" />
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
    <div>
    </div>
    </form>
</body>
</html>
