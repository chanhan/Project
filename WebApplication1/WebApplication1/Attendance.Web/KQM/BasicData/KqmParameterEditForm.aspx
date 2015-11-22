<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KqmParameterEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KqmParameterEditForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考勤參數設定</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

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
			igtbl_getElementById("txtStartDate").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue('yyyy/mm/dd');
			igtbl_getElementById("txtEndDate").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue('yyyy/mm/dd');
			igtbl_getElementById("HiddenShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("ddlShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("HiddenID").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();

		}
    }
    
     function removeDisabled()
    { 
         $("#<%=btnAdd.ClientID %>").removeAttr("disabled");
         $("#<%=btnModify.ClientID %>").removeAttr("disabled");
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");         
         $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
    }
    
     function addDisabled()
    {
         $("#<%=btnCancel.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").attr("disabled","true");
    }
    
    function addDdlDisabled()
    {
              
          $("#<%=ddlBellNo.ClientID %>").attr("disabled","true"); 
          $("#<%=ddlIsNotKaoQin.ClientID %>").attr("disabled","true"); 
    }
   
     function removeDdlDisabled()
    {
              
          $("#<%=ddlBellNo.ClientID %>").removeAttr("disabled"); 
          $("#<%=ddlIsNotKaoQin.ClientID %>").removeAttr("disabled"); 
    }
    
    function Load()
    {
          addDisabled();
          addDdlDisabled();
          $("#<%=txtOrgCode.ClientID %>").css("border-style", "none");
          var numFlag="<%=GetTableNum()%>";
          
          if (numFlag==0  )
          {
          if ($("#<%=txtOrgCode.ClientID %>").val()!="")
          {
          $("#<%=btnAdd.ClientID %>").removeAttr("disabled"); 
          }
          }
          else
          {
          $("#<%=btnModify.ClientID %>").removeAttr("disabled"); 
          $("#<%=btnDelete.ClientID %>").removeAttr("disabled"); 
          }

          
    }
    

     
    
        function btnAdd_OnClientClick()
        {
          addDisabled();
          removeDdlDisabled();
          $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
          $("#<%=btnSave.ClientID %>").removeAttr("disabled");
          $("#<%=hidOperate.ClientID %>").val("add");
           return false;                 
        }
        
        function btnModify_OnClientClick()
        {
          addDisabled();
          removeDdlDisabled();
          $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
          $("#<%=btnSave.ClientID %>").removeAttr("disabled");
 
          $("#<%=hidOperate.ClientID %>").val("modify");
           return false;                 
        }
        
        
        function btnDelete_OnClientClick()
        {
            var rulesID = $.trim($("#<%=txtOrgCode.ClientID%>").val());
            if (rulesID=="")
            {
                alert(Message.AtLastOneChoose);
                return false;
            }
            else {   if (confirm(Message.RulesDeleteConfirm)) { return true; }  else { return false;  } }                  
        }
        
        function btnSave_OnClientClick()
        {
            var isNotKaoQin = $.trim($("#<%=ddlIsNotKaoQin.ClientID%>").val());
            if (isNotKaoQin==null||isNotKaoQin=="")
            {
                alert(Message.KQMIsNotKaoQin);
                return false;
            }
            else{return true;}
        }
        

         function btnCancel_OnClientClick()
         {
          //Load(); 
          return true;       
        }

    </script>

</head>
<body onload="return Load();">
    <form id="form1" runat="server">
    <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
        <tr valign="top">
            <td width="100%">
                <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
                <input id="HiddenIsNotKaoQin" type="hidden" name="HiddenIsNotKaoQin" runat="server">
                <input id="HiddenBellNo" type="hidden" name="HiddenBellNo" runat="server">
                <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
                <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
                    <tr valign="top" class="tr_data_1">
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblOrgCode" runat="server"></asp:Label>
                        </td>
                        <td width="80%">
                            <asp:TextBox ID="txtOrgCode" runat="server" Width="98%" CssClass="input_textBox_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top" class="tr_data_2">
                        <td>
                            &nbsp;
                            <asp:Label ID="lblBellNo" runat="server"></asp:Label>
                        </td>
                        <td>
                            <cc1:DropDownCheckList ID="ddlBellNo" Width="360" RepeatColumns="3" DropImageSrc="../../CSS/Images/expand.gif"
                                TextWhenNoneChecked="===" DisplayTextWidth="420" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                            </cc1:DropDownCheckList>
                        </td>
                    </tr>
                    <tr valign="top" class="tr_data_1">
                        <td>
                            &nbsp;
                            <asp:Label ID="lblIsNotKaoQin" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsNotKaoQin" runat="server" Width="100%" CssClass="input_textBox_1">
                                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Y">Y</asp:ListItem>
                                <asp:ListItem Value="N">N</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlShowPanel" runat="server">
                                <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClientClick="return btnAdd_OnClientClick()">
                                </asp:Button>
                                <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return btnModify_OnClientClick()">
                                </asp:Button>
                                <asp:Button ID="btnCancel" runat="server" CssClass="button_1" OnClientClick="return btnCancel_OnClientClick()" OnClick="btnCancel_Click">
                                </asp:Button>
                                <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click"
                                    OnClientClick="return btnSave_OnClientClick()"></asp:Button>
                                <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click"
                                    OnClientClick="return btnDelete_OnClientClick()"></asp:Button>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="100%" height="140px" colspan="2">
                            <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="140px">
                                <DisplayLayout Name="UltraWebGrid" CompactRendering="False" RowHeightDefault="20px"
                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                    AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                    AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect"
                                    StationaryMargins="HeaderAndFooter">
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
                                    <igtbl:UltraGridBand>
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="30%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgDepName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BellNo" Key="BellNo" IsBound="false" Width="55%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgBellNo %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsNotKaoQin" Key="IsNotKaoQin" IsBound="false"
                                                Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgIsNotKaoQin %>">
                                                </Header>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidOperate" runat="server" />
    </form>
</body>
</html>
