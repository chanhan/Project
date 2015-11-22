<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.test" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1" Namespace="Infragistics.WebUI.UltraWebGrid"
    TagPrefix="igtbl" %>
<%@ Register Src="~/ControlLib/UserLabel.ascx" TagName="UserLabel" TagPrefix="uc1" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics2.WebUI.UltraWebTab.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ControlLib" TagName="Title" Src="../ControlLib/Title.ascx" %>
<%@ Register TagPrefix="ControlLib" TagName="PageNavigator" Src="../ControlLib/PageNavigator.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script>
        function afterSelectEvent(owner, item, evt)
		{
			var personcode = owner.Tabs[0].findControl("textBoxPersoncode").value;
			var rolecode = owner.Tabs[0].findControl("textBoxRolcode").value;

			if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==1)
			{
                owner.Tabs[1].setTargetUrl("<%=sAppPath%>/Sys/CompanyAssignForm.aspx?PersonCode="+personcode);
			}
			else if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==2)
			{
                owner.Tabs[2].setTargetUrl("<%=sAppPath%>/Sys/DepartmentAssignForm.aspx?PersonCode="+personcode+"&RoleCode="
                    +rolecode+"&ModuleCode="+document.getElementById("HiddenModuleCode").value);
			}
			else if(personcode.length>0 && igtbl_getElementById("ProcessFlag").value.length==0 && item.getIndex()==3)
			{
                owner.Tabs[3].setTargetUrl("<%=sAppPath%>/Sys/DeplevelAssignForm.aspx?PersonCode="+personcode);
			}
			else if(item.getIndex()==4)
			{
                owner.Tabs[4].setTargetUrl("<%=sAppPath%>/Sys/PowerChangeForm.aspx?PersonCode="+personcode);
			}
			else if(item.getIndex()==0)
			{
			    owner.setSelectedIndex(0);
			}
			else
			{
			    owner.setSelectedIndex(0);
			    alert("<%=this.GetResouseValue("bfw.bfw_personcompany.selectperson")%>");
			}
		}
		
    </script>
</head>
<body>
   <form id="Form1" method="post" runat="server">
        <ControlLib:Title ID="Title" runat="server"></ControlLib:Title>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="TextID" type="hidden" name="TextID" runat="server" />
        <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server" />
        <table cellspacing="1" id="topTable" cellpadding="0" width="98%" height="88%" align="center">
            <tr>
                <td>
                    <igtab:UltraWebTab ID="UltraWebTab" runat="server" ImageDirectory="/ig_common/images/"
                        Width="100%" Height="105%" BorderColor="#9CAFEF" BorderStyle="Solid" BorderWidth="0px">
                        <ClientSideEvents AfterSelectedTabChange="afterSelectEvent"></ClientSideEvents>
                        <DefaultTabStyle Width="100px" Height="26px" CssClass="tab_normal">
                        </DefaultTabStyle>
                        <SelectedTabStyle CssClass="tab_current" Font-Bold="true">
                        </SelectedTabStyle>
                        <HoverTabStyle CssClass="tab_current">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="1" RightSideWidth="1" FillStyle="None"></RoundedImage>
                        <DefaultTabSeparatorStyle Width="4px">
                        </DefaultTabSeparatorStyle>
                        <Tabs>
                            <igtab:TabSeparator Text="" Tag="">
                                <Style Width="3px">
                            </Style>
                            </igtab:TabSeparator>
                            <igtab:Tab Key="Person" Text="Person" Tooltip="Authority Code:Person" Tag="Person">
                                <ContentTemplate>
                                    <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                        <tr>
                                            <td>
                                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                    <tr style="cursor: hand" onclick="turnit('div_1','div_img_1','<%=sAppPath%>');">
                                                        <td class="tr_table_title">
                                                            <%=this.GetResouseValue("common.edit.area")%>
                                                        </td>
                                                        <td class="tr_table_title" align="right">
                                                            <img id="div_img_1" alt="" src="<%=sAppPath%>/images/uparrows_white.gif">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div id="div_1">
                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="labelPersoncode" runat="server">Person Code:</asp:Label></td>
                                                                        <td class="td_input" width="30%">
                                                                            <asp:TextBox ID="textBoxPersoncode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="labelCname" runat="server">Chinese Name:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <asp:TextBox ID="textBoxCname" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label">
                                                                            &nbsp;<asp:Label ID="labelCompanyID" runat="server">Company:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxCompanyID" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageCompanyID" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxCompanyName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label">
                                                                            &nbsp;<asp:Label ID="labelRolecode" runat="server">Role:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxRolcode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageRolcode" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxRolesName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="labelDepcode" runat="server">Department:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxDPcode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageDPcode" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true" Width="100%"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label">
                                                                            <asp:Label ID="labelDepLevel" runat="server">LevelCode:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxLevelCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageDepLevel" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxLevelName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true" Width="100%"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="labelLanguage" runat="server">Language:</asp:Label></td>
                                                                        <td class="td_input" valign="top" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxLanguage" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageLanguage" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxLanguageName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<asp:Label ID="labelIfadmin" runat="server">Admin:</asp:Label>
                                                                        </td>
                                                                        <td class="td_input" width="30%">
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td width="40%">
                                                                                        <asp:TextBox ID="textBoxAdmini" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                                                    <td style="cursor: hand">
                                                                                        <asp:Image ID="ImageAdmini" runat="server" ImageUrl="~/images/zoom.png"></asp:Image></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="textBoxAdminName" runat="server" CssClass="input_textBox_noborder"
                                                                                            ReadOnly="true"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label" width="20%">
                                                                            &nbsp;<uc1:UserLabel ID="UserLabelTel" ResourceID="common.tel" runat="server" />
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="textBoxTel" runat="server" Width="100%" CssClass="input_textBox"
                                                                                MaxLength="36"></asp:TextBox></td>
                                                                        <td class="td_label">
                                                                            <asp:Label ID="label2" runat="server">Mail</asp:Label>
                                                                        </td>
                                                                        <td class="td_input">
                                                                            <asp:TextBox ID="textBoxMail" runat="server" CssClass="input_textBox" Width="100%"
                                                                                MaxLength="36"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td_label" colspan="4">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="ButtonCondition" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Condition" ToolTip="Authority Code:Condition" CssClass="input_button"
                                                                                            Text="Condition" runat="server" OnClick="ButtonCondition_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonQuery" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Query" ToolTip="Authority Code:Query" CssClass="input_button" Text="Query"
                                                                                            runat="server" OnClick="ButtonQuery_Click"></asp:Button></td>
                                                                                    <td class="td_seperator">
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="ButtonAdd" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Add" ToolTip="Authority Code:Add" CssClass="input_button" Text="Add"
                                                                                            runat="server" OnClick="ButtonAdd_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonModify" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Modify" ToolTip="Authority Code:Modify" CssClass="input_button"
                                                                                            Text="Modify" runat="server" OnClick="ButtonModify_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonDelete" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Delete" ToolTip="Authority Code:Delete" CssClass="input_button"
                                                                                            Text="Delete" runat="server" OnClick="ButtonDelete_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonCancel" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Cancel" ToolTip="Authority Code:Cancel" CssClass="input_button"
                                                                                            Text="Cancel" runat="server" OnClick="ButtonCancel_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonSave" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Save" ToolTip="Authority Code:Save" CssClass="input_button" Text="Save"
                                                                                            runat="server" OnClick="ButtonSave_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonExport" CssClass="input_button" ONMOUSEOVER="className='input_button_over'"
                                                                                            ONMOUSEOUT="className='input_button'" runat="server" Text="Export" CommandName="Export"
                                                                                            ToolTip="Authority Code:Export" OnClick="ButtonExport_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonResetPWD" onmouseover="className='input_button_over'" onmouseout="className='input_button'"
                                                                                            CommandName="Save" ToolTip="Authority Code:ResetPWD" CssClass="input_button"
                                                                                            Text="ResetPWD" runat="server" OnClick="ButtonResetPWD_Click"></asp:Button>
                                                                                        <asp:Button ID="ButtonDisable" CssClass="input_button" ToolTip="Authority Code:Disable"
                                                                                            ONMOUSEOVER="className='input_button_over'" ONMOUSEOUT="className='input_button'"
                                                                                            runat="server" Text="Disable" CommandName="Disable" OnClick="ButtonDisable_Click">
                                                                                        </asp:Button>
                                                                                        <asp:Button ID="ButtonEnable" CssClass="input_button" ToolTip="Authority Code:Enable"
                                                                                            ONMOUSEOVER="className='input_button_over'" ONMOUSEOUT="className='input_button'"
                                                                                            runat="server" Text="Enable" CommandName="Enable" OnClick="ButtonEnable_Click"></asp:Button>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!--test-->
                                        <tr>
                                            <td>
                                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                    <tr style="cursor: hand">
                                                        <td class="tr_table_title" width="100%" onclick="turnit('div_2','div_img_2','<%=sAppPath%>');">
                                                            <%=this.GetResouseValue("common.display.area")%>
                                                        </td>
                                                        <td class="tr_table_title" align="right" valign="middle">
                                                            <ControlLib:PageNavigator ID="PageNavigator" runat="server"></ControlLib:PageNavigator>
                                                        </td>
                                                        <td class="tr_table_title" onclick="turnit('div_2','div_img_2','<%=sAppPath%>');"
                                                            align="right">
                                                            <img id="div_img_2" alt="" src="<%=sAppPath%>/images/uparrows_white.gif">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">

                                                            <script type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*56/100+"'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGridPerson" runat="server" Width="100%" Height="100%">
                                                                <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridPerson" CompactRendering="False" RowHeightDefault="20px"
                                                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                                    AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                                    AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                        CssClass="tr_header">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" Height="100%">
                                                                    </FrameStyle>
                                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridPerson_InitializeLayoutHandler"
                                                                        AfterSelectChangeHandler="AfterSelectChange" AfterRowActivateHandler="AfterSelectChange">
                                                                    </ClientSideEvents>
                                                                    <SelectedRowStyleDefault BackgroundImage="~/images/overbg.bmp">
                                                                    </SelectedRowStyleDefault>
                                                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                                    </RowAlternateStyleDefault>
                                                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                        CssClass="tr_data">
                                                                        <Padding Left="3px"></Padding>
                                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                    </RowStyleDefault>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand BaseTableName="bfw_person" Key="bfw_person">
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="PERSONCODE" IsBound="True" Key="PERSONCODE"
                                                                                Width="70">
                                                                                <Header Caption="Person Code" Fixed="true">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="CNAME" IsBound="True" Key="CNAME" Width="60">
                                                                                <Header Caption="Chinese Name" Fixed="true">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="COMPANYID" IsBound="True" Key="COMPANYID"
                                                                                Hidden="true">
                                                                                <Header Caption="Company ID">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="COMPANYNAME" IsBound="True" Key="COMPANYNAME"
                                                                                Width="130">
                                                                                <Header Caption="Company Name">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DEPCODE" IsBound="True" Key="DEPCODE" Hidden="true">
                                                                                <Header Caption="Dep Code">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DEPNAME" IsBound="True" Key="DEPNAME" Width="80">
                                                                                <Header Caption="Dep Name">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ROLECODE" IsBound="True" Key="ROLECODE" Hidden="true">
                                                                                <Header Caption="Role Code">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RolesName" IsBound="True" Key="RolesName"
                                                                                Width="80">
                                                                                <Header Caption="Role Name">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IFADMIN" IsBound="True" Key="IFADMIN" Width="60">
                                                                                <Header Caption="Admin?">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LANGUAGE" IsBound="True" Key="LANGUAGE" Hidden="true">
                                                                                <Header Caption="Language">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LANGUAGENAME" IsBound="True" Key="LANGUAGENAME"
                                                                                Width="60">
                                                                                <Header Caption="Language Name">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LevelCode" IsBound="True" Key="LevelCode"
                                                                                Hidden="true">
                                                                                <Header Caption="LevelCode">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LevelName" IsBound="True" Key="LevelName"
                                                                                Width="60">
                                                                                <Header Caption="LevelName">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LOGINTIMES" IsBound="True" Key="LOGINTIMES"
                                                                                Width="60">
                                                                                <Header Caption="Login Times">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LOGINTIME" IsBound="True" Key="LOGINTIME"
                                                                                Width="120">
                                                                                <Header Caption="Login Time">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Tel" IsBound="True" Key="Tel" Width="80">
                                                                                <Header Caption="Tel">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Mail" IsBound="True" Key="Mail" Width="120">
                                                                                <Header Caption="Mail">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DELETED" IsBound="True" Key="DELETED" Width="80">
                                                                                <Header Caption="Deleted">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="True" Width="70">
                                                                                <Header Caption="Modifier">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="True"
                                                                                Width="110">
                                                                                <Header Caption="ModifyDate">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                            </DIV>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Key="Company" Text="Company" Tooltip="Authority Code:Company" Tag="Company">
                                <ContentPane TargetUrl="about:blank">
                                </ContentPane>
                            </igtab:Tab>
                            <igtab:Tab Key="Dept" Text="Department" Tooltip="Authority Code:Department" Tag="Department">
                                <ContentPane TargetUrl="about:blank">
                                </ContentPane>
                            </igtab:Tab>
                            <igtab:Tab Key="DepLevel" Text="Level" Tooltip="Authority Code:Level" Tag="Level">
                                <ContentPane TargetUrl="about:blank">
                                </ContentPane>
                            </igtab:Tab>
                            <igtab:Tab Key="PowerChange" Text="PowerChange" Tooltip="Authority Code:PowerChange"
                                Tag="PowerChange">
                                <ContentPane TargetUrl="about:blank">
                                </ContentPane>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                   
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript"><!--
            var tab = igtab_getTabById("UltraWebTab");
		    if(igtbl_getElementById("ProcessFlag").value.length==0)
		    {
			     tab.Tabs[0].findControl("textBoxPersoncode").readOnly=true;
				 tab.Tabs[0].findControl("textBoxCname").readOnly=true;
				 tab.Tabs[0].findControl("textBoxCompanyID").readOnly=true;
				 tab.Tabs[0].findControl("textBoxDPcode").readOnly=true;
				 tab.Tabs[0].findControl("textBoxLevelCode").readOnly=true;
				 tab.Tabs[0].findControl("textBoxRolcode").readOnly=true;
				 tab.Tabs[0].findControl("textBoxAdmini").readOnly=true;
				 tab.Tabs[0].findControl("textBoxLanguage").readOnly=true;
				 tab.Tabs[0].findControl("textBoxTel").readOnly=true;
				 tab.Tabs[0].findControl("textBoxMail").readOnly=true;
		    }
		    if (igtbl_getElementById("ProcessFlag").value=="Modify")
            {
                 tab.Tabs[0].findControl("textBoxPersoncode").readOnly=true;
		    }
	--></script>

</body>
</html>