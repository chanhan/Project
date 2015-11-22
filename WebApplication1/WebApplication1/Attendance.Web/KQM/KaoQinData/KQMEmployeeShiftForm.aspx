<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMEmployeeShiftForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMEmployeeShiftForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMEmployeeShiftForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

<script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

<style type="text/css">
    .img_hidden
    {
        display: none;
    }
    .img_show
    {
        visibility: visible;
    }
</style>

<script type="text/javascript"><!--
        function UpProgress()
		{
			document.getElementById("ButtonImportSave").style.display="none";
			document.getElementById("ButtonImport").disabled="disabled";
			document.getElementById("ButtonExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="";
			document.getElementById("labelupload").innerText = "<%=this.GetResouseValue("common.message.uploading")%>";			
		}
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridShiftQuery_InitializeLayoutHandler(gridName)
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
                //document.frames("EditShift").location.href="KQMEmployeeShiftEditForm.aspx?OrgCode="+OrgCode+"&EmployeeNo="+EmployeeNo+"&ModuleCode="+ModuleCode;
			}
		}
		
		function OpenWindow(ShiftType)//彈出頁面
		{		    
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
	        var width;
            var height;
            if(ShiftType=="ORG")
            {	 
                window.open("KQMOrgShiftForm.aspx?ModuleCode="+ModuleCode,'KQMOrgShiftForm', 'height=700, width=1100, top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
            }
            else
            {
                 window.open("KQMEmployeeShiftEditForm.aspx?ModuleCode="+ModuleCode,'KQMEmployeeShiftEditForm', 'height=700, width=1100, top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');	
            }
            return false;
		}		
		function OpenEmpWindow(WorkNo)//彈出頁面
		{		    
		     var ModuleCode = igtbl_getElementById("ModuleCode").value;
             window.open("KQMEmployeeShiftEditForm.aspx?EmployeeNo="+WorkNo+"&ModuleCode="+ModuleCode,'KQMEmployeeShiftEditForm', 'height=700, width=800, top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');	
		}
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="";
            document.all("PanelBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtWorkNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";
        }
        
        
        	  $(function(){
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
       $(function(){
        $("#img_grid,#td_show_1,#td_show_2").toggle(
            function(){
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
                $("#div_grid").hide();
                
            },
            function(){
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); 
                $("#div_grid").show();
            }
        )
         
   }); 
   
          $(function(){
        $("#div_img_3").toggle(
            function(){
                $("#div_import").hide();
                $("#div_img_3").attr("src","../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_import").show();
                $("#div_img_3").attr("src","../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
         function setSelector(ctrlCode,ctrlName,flag)
       {
           var code=$("#"+ctrlCode).val();
           var moduleCode=$('#<%=ModuleCode.ClientID %>').val();
           if (flag=="DepCode")
           {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;
       }
       
	--></script>

<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenShiftNo" type="hidden" name="HiddenShiftNo" runat="server" />
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <table cellspacing="1" id="top_table" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr1">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlContent" runat="server">
                                            <asp:HiddenField ID="ImportFlag" runat="server" />
                                            <asp:HiddenField ID="ExportFlag" runat="server" />
                                            <div id="div_edit">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblDeptDept" runat="server">Department:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                            Style="display: none"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100%">
                                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="cursor: hand">
                                                                        <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblPersonType" runat="server">ShiftType:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="23%">
                                                            <asp:DropDownList ID="ddlShiftType" runat="server" Width="100%" CssClass="input_textBox">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_label" width="10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblShiftDate" runat="server" ForeColor="Blue">ShiftDate:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="24%">
                                                            <asp:TextBox ID="txtShiftDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="lblShift" runat="server">Shift:</asp:Label>
                                                        </td>
                                                        <td class="td_input">
                                                            <cc1:DropDownCheckList ID="ddlShift" CheckListCssStyle="background-image: url(../../CSS/Images/inputbg.bmp);height: 250px;overflow: scroll;"
                                                                Width="420" RepeatColumns="2" CssClass="input_textBox" DropImageSrc="../../CSS/Images/expand.gif"
                                                                TextWhenNoneChecked="" DisplayTextWidth="400" ClientCodeLocation="../../PubSC/DropDownCheckList.js"
                                                                runat="server">
                                                            </cc1:DropDownCheckList>
                                                        </td>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="lblReGetWorkNo" runat="server">WorkNo:</asp:Label>
                                                            <asp:Image ID="ImageBatchWorkNo" runat="server" OnClick="javascript:ShowBatchWorkNo()"
                                                                ImageUrl="../../CSS/Images_new/search_new.gif"></asp:Image>
                                                        </td>
                                                        <td class="td_input">
                                                            <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                            <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                                z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                                border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                                border-bottom: #0000ff 1px solid; position: absolute; left: 41%; float: left;
                                                                display: none;">
                                                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                    <tr>
                                                                        <td>
                                                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tr>
                                                                                                <td class="td_label" width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                                    <font color="red">Ⅹ</font>
                                                                                                    <asp:Label ID="lblQueryBatchWorkNo" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td_label" width="100%">
                                                                                                    <asp:TextBox ID="txtBatchEmployeeNo" runat="server" TextMode="MultiLine" Height="100"
                                                                                                        Width="100%" Style="display: none"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <iframe src="JavaScript:false" style="position: absolute; visibility: inherit; top: 0px;
                                                                    left: 0px; width: 225px; height: 100px; z-index: -1; filter='progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                                </iframe>
                                                            </div>
                                                        </td>
                                                        <td class="td_label">
                                                            &nbsp;
                                                            <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                        </td>
                                                        <td class="td_input" width="20%">
                                                            <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label" colspan="6">
                                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                                                <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                    CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                                    CssClass="button_2" OnClick="btnReset_Click"></asp:Button>
                                                                <asp:Button ID="btnOrgShift" runat="server" Text="<%$Resources:ControlText,btnOrgShift %>"
                                                                    CssClass="button_large" OnClientClick="return OpenWindow('ORG')"></asp:Button>
                                                                <asp:Button ID="btnEmpShift" runat="server" Text="<%$Resources:ControlText,btnEmpShift %>"
                                                                    CssClass="button_large" OnClientClick="return OpenWindow('EMP')"></asp:Button>
                                                                <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                    CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                                <asp:Button ID="btnImport" runat="server" Text="<%$Resources:ControlText,btnImport %>"
                                                                    CssClass="button_2" OnClick="btnImport_Click"></asp:Button>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr_edit">
                                                    <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                            background-repeat: no-repeat;  width: 80px; text-align: center;
                                                            font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblGrid" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="tr_title_center" style="width: 300px;">
                                                        <div>
                                                            <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                                                HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                                                ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                                                ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                                            </ess:AspNetPager>
                                                        </div>
                                                    </td>
                                                    <td style="width: 22px;" id="td_show_2">
                                                        <img id="img_grid" class="img2" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
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

                                                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*62/100+"'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGridShiftQuery" runat="server" Width="100%" Height="100%"
                                                                OnDataBound="UltraWebGridShiftQuery_DataBound">
                                                                <DisplayLayout Name="UltraWebGridShiftQuery" CompactRendering="False" RowHeightDefault="20px"
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
                                                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridShiftQuery_InitializeLayoutHandler"
                                                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                                    <SelectedRowStyleDefault BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                                    <igtbl:UltraGridBand BaseTableName="KQM_ShiftQuery" Key="KQM_ShiftQuery">
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" IsBound="false" Key="WorkNo" Width="10%">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" IsBound="false" Key="LocalName"
                                                                                Width="10%">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DName" IsBound="false" Key="DName" Width="25%">
                                                                                <Header Caption="<%$Resources:ControlText,gvDName%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" IsBound="false" Key="ShiftDesc"
                                                                                Width="35%">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="StartEndDate" IsBound="false" Key="StartEndDate"
                                                                                Width="20%">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadStartEndDate%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" IsBound="false" Key="StartDate"
                                                                                Hidden="true">
                                                                                <Header Caption="StartDate">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" IsBound="false" Key="EndDate" Hidden="true">
                                                                                <Header Caption="EndDate">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ShiftFlag" IsBound="false" Key="ShiftFlag"
                                                                                Hidden="true">
                                                                                <Header Caption="ShiftFlag">
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
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel class="img_hidden" ID="PanelImport" runat="server" Width="100%">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr2">
                                                    <td style="width: 100%;" class="tr_title_center">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                            background-repeat: no-repeat;  width: 80px; text-align: center;
                                                            font-size: 13px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblImport" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 22px;">
                                                        <img id="div_img_3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" width="100%" align="left" colspan="2">
                                            <div id="div_import">
                                                <table border="1">
                                                    <tr>
                                                        <td class="td_label" width="100%" align="left" colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td width="20%">
                                                                        <a href="/ExcelModel/EmpShiftSample.xls">&nbsp;<asp:Label ID="lblUploadText" runat="server"
                                                                            Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </td>
                                                                    <td width="55%">
                                                                        <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                                                    </td>
                                                                    <td width="5%">
                                                                        <asp:Button ID="btnImportSave" runat="server" Text="<%$Resources:ControlText,btnImportSave %>"
                                                                            CssClass="button_2" OnClick="btnImportSave_Click"></asp:Button>
                                                                    </td>
                                                                    <td width="15%" align="left">
                                                                        <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="100%" colspan="2">
                                                                        <asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2" style="height: 25;">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
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

                                                                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*55/100+"'>");</script>

                                                                        <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                                            <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                                                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                                                BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                                                Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                                                                AutoGenerateColumns="false">
                                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                                    CssClass="tr_header">
                                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                                    </BorderDetails>
                                                                                </HeaderStyleDefault>
                                                                                <FrameStyle Width="100%" Height="100%">
                                                                                </FrameStyle>
                                                                                <ClientSideEvents></ClientSideEvents>
                                                                                <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                                                <igtbl:UltraGridBand BaseTableName="HRM_Import" Key="HRM_Import">
                                                                                    <Columns>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="20%">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg%>">
                                                                                            </Header>
                                                                                            <CellStyle ForeColor="red">
                                                                                            </CellStyle>
                                                                                        </igtbl:UltraGridColumn>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="20%">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="ShiftNo" Key="ShiftNo" IsBound="false" Width="20%">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                                                            Width="20%">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadStartDate%>">
                                                                                            </Header>
                                                                                        </igtbl:UltraGridColumn>
                                                                                        <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="20%">
                                                                                            <Header Caption="<%$Resources:ControlText,gvHeadEndDate%>">
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
