<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KqmParameterEmpForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KqmParameterEmpForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics2.WebUI.UltraWebTab.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考勤參數設定</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
        function UpProgress()
		{
			var filepath= $("#<%=FileUpload.ClientID %>").val();
		    $("#<%=hidFilePath.ClientID %>").val(filepath);
		    if (filepath!="")
		    {
		        if (filepath.indexOf('\\')>=0)
		        {
		           $("#<%=btnImportSave.ClientID %>").css("display","none");
			       $("#<%=btnImport.ClientID %>").attr("disabled","true");
			       $("#<%=btnExport.ClientID %>").attr("disabled","true");
			       $("#imgWaiting").css("display","");		
			       $("#<%=lblupload.ClientID %>").text(Message.uploading);		
			       return true;
		        }
		        else
		        {
		           $("#<%=lblupload.ClientID %>").text(Message.WrongFilePath);
		           return false;
		        }
		    }
		    else
		    {
		     $("#<%=lblupload.ClientID %>").text(Message.PathIsNull);
		     return false;
		    }	
		}
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);			
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridEmpSkill_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(row != null)
			{
				var EmployeeNo=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();       
		        var ModuleCode = document.getElementById("ModuleCode").value;
                document.frames("EditParameterEmp").location.href="KqmParameterEmpEditForm.aspx?EmployeeNo="+EmployeeNo+"&ModuleCode="+ModuleCode;
			}
		}
		
		function removeDisabled()
    { 
         $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
         $("#<%=btnReset.ClientID %>").removeAttr("disabled");
         $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
         $("#<%=btnImport.ClientID %>").removeAttr("disabled");         
         $("#<%=btnExport.ClientID %>").removeAttr("disabled");
    }
    
     function addDisabled()
    {
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");         
         $("#<%=btnReset.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnImport.ClientID %>").attr("disabled","true");
         $("#<%=btnExport.ClientID %>").attr("disabled","true");
    }
		
	function Load()
    {
          var numFlag="<%=GetTableNum()%>";
          
          if (numFlag>0)
          {
             removeDisabled(); 
          }
          else if (numFlag==-1)
          {
            addDisabled();
            $("#<%=btnImport.ClientID %>").removeAttr("disabled");         
            $("#<%=btnExport.ClientID %>").removeAttr("disabled");
          }
          else
          {
            addDisabled();
          }
           
    }	
    

        function btnDelete_OnClientClick()
        {
            var row = igtbl_getRowById(id);	
            if (row==null)
            {
                alert(Message.AtLastOneChoose);
                return false;
            }
            else {   if (confirm(Message.RulesDeleteConfirm)) { return true; }  else { return false;  } }                  
        }
        
        

        function btnReset_OnClientClick()
        {  
            $("#<%=txtWorkNo.ClientID %>").val("");
            $("#<%=txtLocalName.ClientID %>").val("");
     
        }
        
        
        
      $(function(){   
       $("#div_img_2,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_data").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_data").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
     });
		
    </script>

</head>
<body class="color_body" onload="return Load();">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <asp:HiddenField ID="hidFilePath" runat="server" />
    <table cellspacing="1" id="top_table" cellpadding="0" width="100%" align="center">
    
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" width="100%">
                    <tr valign="top">
                        <td width="40%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                            border-right-width: 0px; border-bottom-width: 1px; border-left-width: 1px;">
                            <table class="table_data_area" cellspacing="0" cellpadding="1" width="100%" align="left">
                                <tr>
                                    <td>
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr class="tr_data_1">
                                                    <td class="td_label" width="20%">
                                                        &nbsp;
                                                        <asp:Label ID="lblWorkNo" runat="server">WorkNo:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="30%">
                                                        <asp:TextBox ID="txtWorkNo" runat="server" Width="95%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="20%">
                                                        &nbsp;
                                                        <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="30%">
                                                        <asp:TextBox ID="txtLocalName" runat="server" Width="95%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td class="td_label" colspan="4">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button_1" OnClientClick="return btnReset_OnClientClick();">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click"
                                                                            OnClientClick="return btnDelete_OnClientClick();"></asp:Button>
                                                                        <asp:Button ID="btnImport" runat="server" CssClass="button_1" OnClick="btnImport_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click">
                                                                        </asp:Button>
                                                                    </asp:Panel>
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
                    <tr>
                        <td>
                            <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="td_label" width="100%" align="left" colspan="2">
                                            <a href="../../ExcelModel/KQParamEmployeeSample.xls">
                                                <asp:Label ID="lblUploadText" runat="server" Font-Bold="true"></asp:Label>
                                            </a>&nbsp;
                                            <asp:FileUpload ID="FileUpload" CssClass="input_textBox" runat="server" />
                                            <asp:Button ID="btnImportSave" runat="server" Text="ImportSave" OnClick="btnImportSave_Click"
                                                OnClientClick="return UpProgress();" CssClass="button_1" />
                                            <img id="imgWaiting" src="../../CSS/images/clocks.gif" border="0" style="display: none;
                                                height: 20px;" /><br />
                                            <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label><br />
                                            <asp:Label ID="lbluploadMsg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td align="left" colspan="2" style="height: 25;">
                                            &nbsp;<%=this.GetResouseValue("bfw.hrm.importremark")%>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="2"">

                                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*50/100+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout Name="UltraWebGridImport" CompactRendering="False" RowHeightDefault="20px"
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
                                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="22%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsErrorMsg %>">
                                                                </Header>
                                                                <CellStyle ForeColor="red">
                                                                </CellStyle>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="22%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsWorkNo %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="BellNo" Key="BellNo" IsBound="false" Width="36%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsBellNo %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="IsNotKaoQin" Key="IsNotKaoQin" IsBound="false"
                                                                Width="20%">
                                                                <Header Caption="<%$Resources:ControlText,gvHeadParamsIsNotKaoQin %>">
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
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
                    <tr>
                        <td>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;">
                                        <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tr_title_center" style="width: 200px;">
                                            <div>
                                                <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                    HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                                    ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                                    ShowMoreButtons="false" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                </ess:AspNetPager>
                                            </div>
                                        </td>
                                        <td style="width: 22px;" id="td_show_2">
                                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="div_data">
                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td colspan="3">

                                                <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                                                <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%">
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
                                                                <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="22%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadParamsWorkNo %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LOCALNAME" Key="LOCALNAME" IsBound="false"
                                                                    Width="22%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadParamsLocalName %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="BELLNO" Key="BELLNO" IsBound="false" Width="36%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadParamsBellNo %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ISNOTKAOQIN" Key="ISNOTKAOQIN" IsBound="false"
                                                                    Width="20%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadParamsIsNotKaoQin %>">
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="60%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">

                <script language="javascript">document.write("<DIV id='div_10' style='height:"+document.body.clientHeight*95/100+"'>");</script>

                <iframe name="EditParameterEmp" src="KqmParameterEmpEditForm.aspx?ModuleCode=<%=this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString()%>"
                    width="100%" height="100%" scrolling="no"></iframe>

                <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

            </td>
        </tr>
    </table>
    </form>
</body>
</html>
