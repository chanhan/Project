<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMBGCalendarForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KQMBGCalendarForm" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<%@ Register TagPrefix="ControlLib" TagName="Title" Src="../../ControlLib/Title.ascx" %>
<%@ Register TagPrefix="ControlLib" TagName="PageNavigator" Src="../../ControlLib/PageNavigator.ascx" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BGCalendar</title>
</head>
<body class="color_body">
    <form id="form1" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>

                            <script language="javascript">document.write("<DIV id='div_1' style='height:"+document.body.clientHeight*84/100+"'>");</script>

                            <table width="100%" align="center" border="1" cellspacing="1" cellpadding="4" bgcolor="whitesmoke"
                                bordercolor="gray">
                                <tr>
                                    <td width="10%" align="center" valign="middle" bgcolor="whitesmoke" background="../../CSS/images/colorstripe.gif"
                                        style="height: 31px">
                                        <asp:ImageButton ID="ImageButtonPrev" runat="server" ImageUrl="../../CSS/images/prev.png"
                                            Width="10" Height="18" OnClick="ImageButtonPrev_Click" />
                                    </td>
                                    <td width="80%" colspan="5" align="center" valign="middle" bgcolor="#666666" background="../../CSS/images/colorstripe.gif"
                                        style="height: 31px">
                                        <asp:DropDownList ID="ddlBG" runat="server" CssClass="input_ddl" Width="120px" AutoPostBack="true"
                                            OnSelectedIndexChanged="DropDownListBG_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp; <b><font face="Verdana, Arial, Helvetica" color="mintcream">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="input_ddl" Width="80px" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input_ddl" Width="45px"
                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;</font></b>
                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                        <asp:Button ID="ButtonModify" runat="server" Text="Modify" ToolTip="Authority Code:Modify"
                                            CommandName="Modify" OnClick="ButtonModify_Click" CssClass="button_2"></asp:Button><asp:Button
                                                ID="ButtonSave" runat="server" Text="Save" ToolTip="Authority Code:Save" CommandName="Save"
                                                OnClick="ButtonSave_Click" CssClass="button_2"></asp:Button>
                                                </asp:Panel>
                                    </td>
                                    <td width="10%" align="center" valign="middle" bgcolor="whitesmoke" background="../../CSS/images/colorstripe.gif"
                                        style="height: 31px">
                                        <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="../../CSS/Images/next.png"
                                            Width="10" Height="18" OnClick="ImageButtonNext_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica" color="red"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica"></font>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <font face="Verdana, Arial, Helvetica" color="red"></font>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay1" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag1" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag1" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag1" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag1" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark1" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay2" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag2" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag2" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag2" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag2" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark2" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay3" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag3" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag3" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag3" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag3" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark3" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay4" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag4" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag4" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag4" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag4" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark4" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay5" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag5" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag5" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag5" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag5" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark5" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay6" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag6" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag6" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag6" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag6" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark6" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay7" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag7" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag7" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag7" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag7" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark7" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay8" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag8" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag8" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag8" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag8" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark8" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay9" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag9" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag9" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag9" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag9" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark9" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay10" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag10" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag10" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag10" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag10" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark10" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay11" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag11" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag11" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag11" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag11" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark11" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay12" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag12" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag12" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag12" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag12" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark12" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay13" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag13" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag13" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag13" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag13" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark13" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay14" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag14" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag14" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag14" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag14" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark14" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay15" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag15" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag15" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag15" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag15" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark15" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay16" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag16" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag16" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag16" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag16" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark16" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay17" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag17" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag17" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag17" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag17" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark17" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay18" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag18" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag18" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag18" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag18" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark18" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay19" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag19" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag19" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag19" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag19" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark19" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay20" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag20" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag20" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag20" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag20" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark20" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay21" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag21" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag21" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag21" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag21" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark21" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay22" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag22" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag22" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag22" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag22" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark22" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay23" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag23" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag23" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag23" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag23" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark23" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay24" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag24" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag24" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag24" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag24" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark24" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay25" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag25" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag25" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag25" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag25" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark25" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay26" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag26" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag26" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag26" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag26" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark26" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay27" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag27" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag27" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag27" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag27" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark27" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay28" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag28" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag28" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag28" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag28" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark28" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay29" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag29" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag29" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag29" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag29" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark29" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay30" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag30" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag30" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag30" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag30" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark30" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay31" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag31" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag31" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag31" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag31" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark31" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay32" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag32" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag32" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag32" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag32" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark32" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay33" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag33" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag33" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag33" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag33" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark33" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay34" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag34" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag34" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag34" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag34" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark34" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay35" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag35" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag35" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag35" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag35" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark35" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trLast" runat="server">
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay36" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag36" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag36" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag36" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag36" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark36" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay37" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag37" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag37" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag37" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag37" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark37" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay38" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag38" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag38" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag38" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag38" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark38" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay39" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag39" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag39" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag39" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag39" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark39" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay40" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag40" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag40" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag40" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag40" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark40" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay41" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag41" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag41" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag41" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag41" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark41" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="14%" align="center" valign="middle" style="height: 20px">
                                        <asp:Label ID="lblDay42" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label><br />
                                        <asp:Label ID="lblWorkFlag42" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Overline="False"
                                            Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlWorkFlag42" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblHolidayFlag42" runat="server" Font-Bold="True" Font-Names="標楷體"
                                            Font-Overline="False" Font-Size="11pt"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlHolidayFlag42" runat="server" CssClass="input_ddl" Width="35px">
                                            <asp:ListItem Value="Y"></asp:ListItem>
                                            <asp:ListItem Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemark42" CssClass="input_textbox" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
