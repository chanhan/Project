<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentLayoutBForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HRM.DepartmentLayoutBForm" %>
<%@ Register TagPrefix="oc" Namespace="Whidsoft.WebControls" Assembly="Whidsoft.WebControls.OrgChart" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
	
	<script type="text/javascript"><!--
	function QueryCheck()
	{
	 var DepName = $.trim($("#<%=txtDepName.ClientID%>").val());
    if (DepName =="")
        {
            alert(Message.DepNameNotNull);
            return false;
        }
	}
		function NoMessage()
		{}
		
		function setSelector()
        {
        var modulecode=$("#<%=HiddenModuleCode.ClientID %>").val();
        var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+modulecode;
        var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
        var info=window.showModalDialog(url,null,fe);
        if(info)
        {
        $("#txtDepCode").val(info.codeList);
        $("#txtDepName").val(info.nameList);
        }
        return false;
        }
	--></script>

    <style media="print" type="text/css">
			.Noprint{display:none;}
			.PageNext{page-break-after: always;}
		</style>
    <style type="text/css">
			.Noprint
			{	
				font-family: "新細明體";
				font-size: 9pt;
			}
			body
            {
	             font-family :新細明體 ;
	            }
            .orgchartTable
            {
	            font-size: 12px;
	            padding : 5px 5px 5px 5px;
	            border : thin solid  orange;
	            background-color: lightgrey;
            }
            .orgchartCellPadding
            {
	            font-size: 12px;
	            padding: 0px 0px 0px 0px;
            }
            table
            tr
            td
            {
	            font-family :新細明體 ;
	            font-size: 9pt;
            	 
	        }
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenType" type="hidden" name="HiddenType" runat="server" />
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server" />
        <table cellspacing="1" class="Noprint" cellpadding="0" width="96%" align="center">
            <tr>
                <td valign="top">
                    <table class="top_table" cellpadding="0" cellspacing="1" width="100%" align="left">
                        <tr>
                            <td>
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <span style="color: Red">
                                                <%=Resources.ControlText.printnotice%>
                                                ：</span><%=Resources.ControlText.printnoticeconfig%>
                                        </td>
                                        <td align="right">
                                            <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0"
                                                width="0">
                                            </object>
                                            <input type="button" id="btnPrint" style="height: 14pt; width: auto" CssClass="button_1" value="Print" onclick="document.all.WebBrowser.ExecWB(6,1)" />
                                            <input type="button" id="brnPageSettings" style="height: 14pt; width: auto" CssClass="button_1" value="Page Settings" onclick="document.all.WebBrowser.ExecWB(8,1)" />
                                            <input type="button" id="btnPrintPreview" style="height: 14pt; width: auto" CssClass="button_1" value="Print Preview" onclick="document.all.WebBrowser.ExecWB(7,1)" />
                                            <input type="button" id="btnPageClose" style="height: 14pt; width: auto" CssClass="button_1" value="Close" onclick="window.close()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <hr align="center" width="100%" noshade="noshade" size="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="100">
                                                        &nbsp;
                                                        <asp:Label ID="lblOrgName" runat="server" Text="Label"></asp:Label></td>
                                                    <td width="200">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                        Style="display: none"></asp:TextBox></td>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox></td>
                                                                <td style="cursor: hand" onclick="setSelector();">
                                                                    <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif"></asp:Image></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="60">
                                                    <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                </asp:Button>
                                                        <%--<asp:Button ID="ButtonQuery" ONMOUSEOVER="className='input_button_over'" ONMOUSEOUT="className='input_button'"
                                                            runat="server" Text="Query" CssClass="input_button" ToolTip="Authority Code:Query"
                                                            CommandName="Query" OnClick="ButtonQuery_Click"></asp:Button>--%>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="RadioType" runat="server" RepeatColumns="2">
                                                            <asp:ListItem Selected="True" Value="0" Text="<%$Resources:ControlText,FromLeftToRight %>" ></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="<%$Resources:ControlText,FromTopToBottom %>" ></asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="0" width="96%" align="center">
            <tr>
                <td valign="top">
                    <table class="top_table" cellpadding="0" cellspacing="1" width="100%" align="left">
                        <tr>
                            <td>
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <div id="div_1">
                                                <oc:OrgChart ID="OrgChart1" Style="z-index: 101; left: 104px; position: absolute;
                                                    top: 88px" runat="server" LineColor="Silver" Width="664px" Height="184px" ChartStyle="Vertical"
                                                    ToolTip="test" Font-Names="黑体"></oc:OrgChart>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript"><!--
		document.getElementById("txtDepName").readOnly=true;
	--></script>
</body>
</html>
