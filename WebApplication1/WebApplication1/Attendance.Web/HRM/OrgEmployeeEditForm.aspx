<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgEmployeeEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.OrgEmployeeEditForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script>
            function UpProgress()
		{
			document.getElementById("btnReturn").disabled="disabled";
			document.getElementById("btnImportSave").style.display="none";	
			document.getElementById("imgWaiting").style.display="";		
		}
    		$(document).ready(function(){
		$('#<%=btnSave.ClientID %>').click(function(){
		return confirm(Message.SaveConfirm);
		});
		$('#<%=btnEmpty.ClientID %>').click(function(){
		return confirm(Message.SaveConfirm);
		});
		});
    function GetDepByParentDep(ReturnValueBoxName,Condition,moduleCode,ReturnDescBoxName,ParentDep)
{
    var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("SingleDataPickForm.aspx?condition="+Condition+"&moduleCode="+moduleCode+"&ParentDep="+ParentDep,window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	if(Revalue!=undefined)
	{	
	    var arrValue=Revalue.split(";");
			document.all(ReturnValueBoxName).value=arrValue[0];
			if(arrValue.length>1)
			{
			    document.all(ReturnDescBoxName).innerText=arrValue[1];
			}
	}
	else 
	{
	  document.getElementById('txtOrgName').value='';
	  document.getElementById('txtOrgCode').value='';
	}
}
    		function BeSelAll()
		{
			var sValue= false;
			var chk=document.getElementById("gridEmployee_ctl00_chkBeSelAll");
			if(chk.checked)
			{
				sValue= true;
			}
			var grid = igtbl_getGridById("gridEmployee");
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("gridEmployee_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("gridEmployee_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}	
		
		function SelAll()
		{
			var sValue=false;
			var chk=document.getElementById("GridSelEmployee_ctl00_chkSelAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById("GridSelEmployee");
			var gRows = grid.Rows;
			
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("GridSelEmployee_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("GridSelEmployee_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
        <tr valign="top">
            <td>
                <asp:HiddenField ID="hiddenDeleteStatu" runat="server" />
                <input id="hiddenworknos" type="hidden" name="hiddenworknos" runat="server">
                <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
                <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
                <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server">
                <asp:Panel ID="panelData" runat="server" Visible="true">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="td_label" width="48%">
                                <asp:Label ID="lblBeSel" runat="server"></asp:Label>
                            </td>
                            <td class="td_label" width="4%">
                            </td>
                            <td class="td_label" width="48%">
                                <asp:Label ID="lblSel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr width="100%">
                            <td align="right" width="48%">

                                <script language="javascript">document.write("<DIV id='div_a' style='height:"+document.body.clientHeight*88/100+"';overflow-y: scroll>");</script>

                                <div style="height: 600px; overflow-x: scroll">
                                    <igtbl:UltraWebGrid ID="gridEmployee" runat="server">
                                        <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                            AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                            AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                            AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                            <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                CssClass="tr_header">
                                                <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                </BorderDetails>
                                            </HeaderStyleDefault>
                                            <FrameStyle Width="100%" Height="100%">
                                            </FrameStyle>
                                            <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                            <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                            </SelectedRowStyleDefault>
                                            <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                            </RowAlternateStyleDefault>
                                            <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#6699ff" BorderStyle="Solid"
                                                CssClass="tr_data1">
                                                <Padding Left="3px"></Padding>
                                                <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                            </RowStyleDefault>
                                        </DisplayLayout>
                                        <Bands>
                                            <igtbl:UltraGridBand BaseTableName="gridEmployee" Key="gridEmployee">
                                                <Columns>
                                                    <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                        HeaderClickAction="Select" Key="chkBeSelAll" Width="25%" HeaderText="CheckBox">
                                                        <CellTemplate>
                                                            <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                        </CellTemplate>
                                                        <HeaderTemplate>
                                                            <input id="chkBeSelAll" name="chkBeSelAll" onclick="javascript:BeSelAll()" runat="server"
                                                                type="checkbox" />
                                                        </HeaderTemplate>
                                                        <Header Caption="CheckBox" ClickAction="Select">
                                                        </Header>
                                                    </igtbl:TemplatedColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" IsBound="false" Key="WorkNo" Width="45%">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgEmpWorkNo %>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" IsBound="false" Key="LocalName"
                                                        Width="30%">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadParamsLocalName %>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                    </igtbl:UltraWebGrid>
                                </div>

                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                            </td>
                            <td>
                                <table class="inner_table">
                                    <tr>
                                        <td style="width: 45px">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnRight" runat="server" Text="<%$Resources:ControlText,btnRight%>"
                                                            CssClass="input_linkbutton" OnClick="btnRight_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 45px">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnLeft" runat="server" Text="<%$Resources:ControlText,btnLeft%>"
                                                            CssClass="input_linkbutton" OnClick="btnLeft_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="48%">

                                <script language="javascript">document.write("<DIV id='div_a' style='height:"+document.body.clientHeight*88/100+"'>");</script>

                                <div style="height: 600px; overflow-x: scroll">
                                    <igtbl:UltraWebGrid ID="GridSelEmployee" runat="server">
                                        <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                            AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                            AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                            AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                            <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                CssClass="tr_header">
                                                <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                </BorderDetails>
                                            </HeaderStyleDefault>
                                            <FrameStyle Width="100%" Height="100%">
                                            </FrameStyle>
                                            <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                            <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                            </SelectedRowStyleDefault>
                                            <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                            </RowAlternateStyleDefault>
                                            <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#6699ff" BorderStyle="Solid"
                                                CssClass="tr_data1">
                                                <Padding Left="3px"></Padding>
                                                <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                            </RowStyleDefault>
                                        </DisplayLayout>
                                        <Bands>
                                            <igtbl:UltraGridBand BaseTableName="KQM_OrgEmployeeData" Key="KQM_OrgEmployeeData">
                                                <Columns>
                                                    <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                        HeaderClickAction="Select" Width="20%" Key="chkSelAll">
                                                        <CellTemplate>
                                                            <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                        </CellTemplate>
                                                        <HeaderTemplate>
                                                            <input id="chkSelAll" onclick="javascript:SelAll()" runat="server" type="checkbox" />
                                                        </HeaderTemplate>
                                                        <Header Caption="CheckBox" ClickAction="Select">
                                                        </Header>
                                                    </igtbl:TemplatedColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="True"
                                                        Key="WorkNo" Width="50%">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgEmpWorkNo %>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="True"
                                                        Key="LocalName" Width="50%">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadParamsLocalName %>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                    </igtbl:UltraWebGrid>
                                </div>

                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                            </td>
                        </tr>
                    </table>
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="td_label" width="20%">
                                <asp:Label ID="lblTeamCode" runat="server"></asp:Label>
                            </td>
                            <td style="cursor: hand" class="td_input" width="40%">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtOrgCode" runat="server" Width="100%" CssClass="input_textBox"
                                                Style="display: none"></asp:TextBox>
                                        </td>
                                        <td width="100%">
                                            <asp:TextBox ID="txtOrgName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                        </td>
                                        <td style="cursor: hand">
                                            <asp:Image ID="ibtnPickDept" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                            </asp:Image>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 45px">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                                            Text="<%$Resources:ControlText,btnSave%>"></asp:Button>
                                                    </td>
                                                    <td style="width: 45px">
                                                        <asp:Button ID="btnImport" runat="server" CssClass="button_1" OnClick="btnImport_Click"
                                                            Text="<%$Resources:ControlText,btnImport%>" />
                                                    </td>
                                                    <td style="width: 45px">
                                                        <asp:Button ID="btnEmpty" runat="server" CssClass="button_1" OnClick="btnEmpty_Click"
                                                            Text="<%$Resources:ControlText,btnEmpty%>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="height: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" width="100%" align="left" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <a href="../ExcelModel/KQMOrgEmployee.xls">&nbsp;
                                                <%=Resources.ControlText.InfoGetExcelModel%>
                                            </a>
                                            <asp:Label ID="lbllUploadText" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnImportSave" runat="server" CssClass="input_linkbutton" Text="<%$Resources:ControlText,btnImportSave%>"
                                                            OnClientClick="javascript:UpProgress();" OnClick="btnImportSave_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnReturn" runat="server" CssClass="input_linkbutton" Text="<%$Resources:ControlText,btnReturn%>"
                                                            OnClick="btnImport_Click" />
                                                        <img id="imgWaiting" src="../../CSS/Images/clocks.gif" border="0" style="display: none;
                                                            height: 20px;" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lbluploadInfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">

                                <script language="javascript">document.write("<DIV id='div_c' style='height:"+document.body.clientHeight*88/100+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server">
                                    <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                        AutoGenerateColumns="false" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridImport" TableLayout="Fixed"
                                        CellClickActionDefault="RowSelect">
                                        <FrameStyle Width="100%" Cursor="Default" BorderWidth="0px" Font-Size="8pt" Font-Names="Verdana"
                                            BorderColor="WhiteSmoke" BorderStyle="Solid" Height="100%">
                                            <BorderDetails ColorLeft="Gray"></BorderDetails>
                                        </FrameStyle>
                                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                        </SelectedRowStyleDefault>
                                        <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                        </RowAlternateStyleDefault>
                                        <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                            CssClass="tr_data">
                                            <Padding Left="3px"></Padding>
                                            <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                        </RowStyleDefault>
                                        <ActivationObject BorderColor="" BorderWidth="">
                                        </ActivationObject>
                                    </DisplayLayout>
                                    <Bands>
                                        <igtbl:UltraGridBand BaseTableName="GDS_ATT_TEMP_ORGEmplyee" Key="GDS_ATT_TEMP_ORGEmplyee">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="errormsg" IsBound="false" Key="ErrorMsg" Width="40%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderErrorMsg %>">
                                                    </Header>
                                                    <CellStyle ForeColor="Red">
                                                    </CellStyle>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" IsBound="false" Key="WorkNo" Width="20%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderWorkNo %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LOCALNAME" IsBound="false" Key="LOCALNAME"
                                                    Width="20%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="ORGCODE" IsBound="false" Key="LOCALNAME" Width="20%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeaderOrgOrgCode %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                            </Columns>
                                            <AddNewRow View="NotSet" Visible="NotSet">
                                            </AddNewRow>
                                        </igtbl:UltraGridBand>
                                    </Bands>
                                </igtbl:UltraWebGrid>

                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
