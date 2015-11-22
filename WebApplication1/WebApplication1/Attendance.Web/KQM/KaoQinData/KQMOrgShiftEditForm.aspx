<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMOrgShiftEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMOrgShiftEditForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMOrgShiftEdit</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
        }
        .input_textBox_no
        {
            border: 0;
        }
        .img_hidden
        {
            display: none;
        }
        .notput_textBox_noborder
        {
            border: 0;
        }
        .img_show
        {
            visibility: visible;
        }
    </style>

    <script language="javascript" type="text/javascript">
    function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);	
			DisplayRowData(row);
			return 0;
		}
	function UltraWebGrid_InitializeLayoutHandler(gridName)
	{
		    var row = igtbl_getActiveRow(gridName);
		    DisplayRowData(row);
	}
	function DisplayRowData(row)
	{
		if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
		{
			igtbl_getElementById("txtStartDate").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue("yyyy/mm/dd");
			igtbl_getElementById("txtEndDate").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue("yyyy/mm/dd");
			igtbl_getElementById("HiddenShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("ddlShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("HiddenID").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
		}
    }
    
    
      
    function add_click()
    {
         $("#HiddenID").val("");
         $("#<%=hidOperate.ClientID %>").attr("value","add")
         $("#<%=txtOrgCode.ClientID %>").attr("readonly","true");
         $("#<%=ddlShiftNo.ClientID %>").removeAttr("disabled");
         $("#<%=txtStartDate.ClientID %>").removeAttr("readonly");
         $("#<%=txtEndDate.ClientID %>").removeAttr("readonly");
         $(".input_textBox").removeClass("input_textBox");
         $("#<%=txtOrgCode.ClientID %>").addClass("input_textBox");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         
         return false;
    
    }
    
    function modify_click()
    {
      if($("#<%=ddlShiftNo.ClientID %>").val()!=null && $("#<%=ddlShiftNo.ClientID %>").val()!="")
    {
         $("#<%=hidOperate.ClientID %>").attr("value","modify")
         $("#<%=txtOrgCode.ClientID %>").attr("readonly","true");
         $("#<%=ddlShiftNo.ClientID %>").removeAttr("disabled");
         $("#<%=txtStartDate.ClientID %>").removeAttr("readonly");
         $("#<%=txtEndDate.ClientID %>").removeAttr("readonly");
         $(".input_textBox").removeClass("input_textBox");
         $("#<%=txtOrgCode.ClientID %>").addClass("input_textBox");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         return false;
     }
     else
     {
      alert(Message.AtLastOneChoose);
      return false;
     }
    }
    
        function sava_click()
    {
        if ( $("#<%=ddlShiftNo.ClientID %>").val()==""||$("#<%=ddlShiftNo.ClientID %>").val()==null)
        {
            alert(Message.ShiftNoNotNull);
            return false;
        }
        else
        {
            if ( $("#<%=txtStartDate.ClientID %>").val()=="")
            {
                alert(Message.StartDateNotNull);
                return false;
            }
            else
            {
                 if ($("#<%=txtEndDate.ClientID %>").val()=="")
                 {
                     alert(Message.EndDateNotNull);
                     return false;
                 }
                else
                 {
                     if ($("#<%=txtStartDate.ClientID %>").val()>$("#<%=txtEndDate.ClientID %>").val())
                     {
                         alert(Message.EndLaterThanStart);
                         return false;
                     }
                     else
                     {
                            return true ;
                     }
                }
            }
        }
    };
    
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
        <tr valign="top">
            <td width="100%">
                <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
                <input id="HiddenShiftNo" type="hidden" name="HiddenShiftNo" runat="server">
                <input id="HiddenID" type="hidden" name="HiddenID" runat="server">
                <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
                <input id="HiddenStartDate" type="hidden" name="HiddenStartDate" runat="server">
                <input id="HiddenEndDate" type="hidden" name="HiddenEndDate" runat="server">
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr valign="top">
                        <td class="td_label" width="40%">
                            &nbsp;
                            <asp:Label ID="lblOrgCode" runat="server">OrgCode:</asp:Label>
                        </td>
                        <td class="td_input" width="60%">
                            <asp:TextBox ID="txtOrgCode" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="td_label">
                            &nbsp;
                            <asp:Label ID="lblShiftNo" runat="server" ForeColor="Blue">ShiftNo:</asp:Label>
                        </td>
                        <td class="td_input">
                            <asp:DropDownList ID="ddlShiftNo" runat="server" Width="100%" CssClass="input_textBox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="td_label">
                            &nbsp;
                            <asp:Label ID="lblShiftDate" runat="server" ForeColor="Blue">ShiftDate:</asp:Label>
                        </td>
                        <td class="td_input">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td width="50%">
                                        <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        ~
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HiddenField ID="hidOperate" runat="server" />
                            <asp:Panel ID="pnlShowPanel" runat="server">
                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                    CssClass="button_2" OnClientClick="return add_click();"></asp:Button>
                                <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                    CssClass="button_2" OnClientClick="return modify_click();"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                    CssClass="button_2" OnClick="btnCancel_Click"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                    CssClass="button_2" OnClick="btnSave_Click" OnClientClick="return sava_click();"></asp:Button>
                                <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                    CssClass="button_2" OnClick="btnDelete_Click" OnClientClick="return confirm('確定要刪除這條數據嗎？');">
                                </asp:Button>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="2">
                            <div id="div_grid">
                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                    <tr style="width: 100%">
                                        <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                                            <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19">
                                        </td>
                                        <td background="../../CSS/Images_new/EMP_07.gif" height="19px">
                                        </td>
                                        <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                                            <img height="18" src="../../CSS/Images_new/EMP_02.gif" width="19">
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td width="19" background="../../CSS/Images_new/EMP_05.gif">
                                            &nbsp;
                                        </td>
                                        <td>

                                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*70/100+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="140px">
                                                <DisplayLayout Name="UltraWebGrid" CompactRendering="False" RowHeightDefault="20px"
                                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                    AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                    AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter"
                                                    AutoGenerateColumns="false">
                                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                        CssClass="tr_header">
                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                        </BorderDetails>
                                                    </HeaderStyleDefault>
                                                    <FrameStyle Width="100%" Height="100%">
                                                    </FrameStyle>
                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
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
                                                    <igtbl:UltraGridBand BaseTableName="KQM_OrgShift" Key="KQM_OrgShift">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                                Width="150">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StartEndDate" Key="StartEndDate" IsBound="false"
                                                                Width="160">
                                                                <Header Caption="<%$Resources:ControlText,gvStartEndDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDays" Key="ShiftDays" IsBound="false"
                                                                Width="50">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDays%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false" Format="yyyy/MM/dd"
                                                                Hidden="false">
                                                                <Header Caption="StartDate">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Hidden="true" Format="yyyy/MM/dd">
                                                                <Header Caption="EndDate">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" Key="ShiftNo" IsBound="false" Hidden="true">
                                                                <Header Caption="ShiftNo">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                                <Header Caption="ID">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                                Width="60">
                                                                <Header Caption="<%$Resources:ControlText,gvModifier%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="update_date" Key="update_date" IsBound="true"
                                                                Width="120">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftUpdateDate%>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>

                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                        </td>
                                        <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                            &nbsp;
                                        </td>
                                        <tr>
                                        </tr>
                                    </tr>
                                    <tr>
                                        <td width="19" background="../../CSS/Images_new/EMP_03.gif" height="18">
                                            &nbsp;
                                        </td>
                                        <td background="../../CSS/Images_new/EMP_08.gif" height="18">
                                            &nbsp;
                                        </td>
                                        <td width="19" background="../../CSS/Images_new/EMP_04.gif" height="18">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script language="javascript" type="text/javascript">
    //<!-- 
    if(igtbl_getElementById("ProcessFlag").value.length==0)
    {
        igtbl_getElementById("txtStartDate").readOnly=true;
        igtbl_getElementById("txtEndDate").readOnly=true;
	    document.all("ddlShiftNo").disabled=true;
    }
    if(igtbl_getElementById("ProcessFlag").value=="Modify")
    {
	    document.all("ddlShiftNo").disabled=false;
	    document.all("ddlShiftNo").value=igtbl_getElementById("HiddenShiftNo").value;
    }
    // -->
    </script>

</body>
</html>
