<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMLeaveYearImport.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.KQMLeaveYearImport" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMLeaveYearImport</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

</head>
<style type="text/css">
    .input_textBox
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

<script type="text/javascript"><!--
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGrid_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGrid');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
        function UpProgress()
		{
			document.getElementById("btnImportSave").style.display="none";
			document.getElementById("btnImport").disabled="disabled";
			document.getElementById("btnExport").disabled="disabled";			
			//document.getElementById("imgWaiting").style.display="";
			//document.getElementById("labelupload").innerText = "<%=this.GetResouseValue("common.message.uploading")%>";			
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
                //document.frames("EditShift").location.href="KQMEmployeeShiftEditForm.aspx?OrgCode="+OrgCode+"&EmployeeNo="+EmployeeNo+"&ModuleCode="+ModuleCode;
			}
		}
	$(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
    $(function(){
       $("#tr_show").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
   });
  $(function(){
    $("#tr_showim").toggle(
            function(){
                $("#div_import").hide();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_import").show();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
   });
    function delete_click()
    {
        var grid = igtbl_getGridById('UltraWebGrid');
        var gRows = grid.Rows;
        var Count=0;
        var State="";
        for(i=0;i<gRows.length;i++)
        {
	        if(igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
	        {
	             Count+=1;
	        }
        }			
        if(Count==0)
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        if(confirm(Message.DeleteConfirm))
        {
	        return true;
	    }
	    else
	    {			    
	        return false;
	    }
    }
    function reset_click()
    {
        $("#<%=txtDepCode.ClientID %>").val("");
        $("#<%=txtDName.ClientID %>").val("");
        $("#<%=txtWorkNo.ClientID %>").val("");
        $("#<%=txtLocalName.ClientID %>").val("");
        $("#<%=ddlLeaveYear.ClientID %>").val("");
        return false;
    }
   function setSelector(ctrlCode,ctrlName,moduleCode)
   {
       var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
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
    <div>
        <asp:HiddenField ID="ImportFlag" Value="1" runat="server" />
        <input id="HiddenID" type="hidden" name="HiddenID" runat="server">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_1">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area">
                                            <tr class="tr_data_1">
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="gvDepName" runat="server">Department:</asp:Label>
                                                </td>
                                                <td width="15%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" class="input_textBox_1"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDName" runat="server" class="input_textBox_1" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblEmployeeNo" runat="server">WorkNo:</asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                </td>
                                                <td width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblYear" runat="server">Year:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLeaveYear" Width="100%" runat="server" class="input_textBox_2">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                        <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnReset" runat="server" class="button_2" OnClientClick="return reset_click()">
                                        </asp:Button>
                                        <asp:Button ID="btnDelete" runat="server" class="button_2" OnClick="btnDelete_Click"
                                            OnClientClick="return delete_click()"></asp:Button>
                                        <asp:Button ID="btnExport" runat="server" class="button_2" OnClick="btnExport_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnImport" runat="server" class="button_2" OnClick="btnImport_Click">
                                        </asp:Button>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidOperate" runat="server" />
        </div>
        <div>
            <asp:Panel ID="PanelData" runat="server" Width="100%">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;">
                        <td style="width: 100%;" id="tr_show" class="tr_title_center">
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
                        <td class="tr_title_center" style="width: 300px;">
                            <div>
                                <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                    HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                    ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                    ShowMoreButtons="false" DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px"
                                    ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                </ess:AspNetPager>
                            </div>
                        </td>
                        <td style="width: 22px;">
                            <img class="img2" id="tr_showtd" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </td>
                    </tr>
                </table>
                <div id="div_showdata">
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

                                <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*64/100+"'>");</script>

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
                                        <igtbl:UltraGridBand BaseTableName="gds_att_LeaveYearImport" Key="gds_att_LeaveYearImport">
                                            <Columns>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" HeaderText="CheckBox" Key="CheckBoxAll" Width="30px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellTemplate>
                                                        <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                    </CellTemplate>
                                                    <HeaderTemplate>
                                                        <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                    </HeaderTemplate>
                                                    <Header Caption="CheckBox" ClickAction="Select">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvLocalName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="BuName" HeaderText="BuName" IsBound="false"
                                                    Key="BuName" Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvBuOTMQryName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="DName" HeaderText="DName" IsBound="false"
                                                    Key="DName" Width="20%">
                                                    <Header Caption="<%$Resources:ControlText,gvDName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LeaveYear" Key="LeaveYear" IsBound="false"
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvLeaveYear %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="LeaveDays" Key="LeaveDays" IsBound="false"
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvLeaveDays %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Create_User" Key="Create_User" IsBound="false"
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvCreateUser %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Create_Date" Key="Create_Date" IsBound="false"
                                                    Width="20%" Format="yyyy/MM/dd">
                                                    <Header Caption="<%$Resources:ControlText,gvCreateDate %>">
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
            </asp:Panel>
        </div>
        <asp:Panel ID="PanelImport" Visible="false" runat="server" Width="100%">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr style="cursor: hand">
                    <td>
                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                            <tr style="width: 100%;" id="tr_showim">
                                <td style="width: 100%;" class="tr_title_center">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblImport" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tr_table_title" align="right">
                                    <img class="img3" src="../../CSS/Images_new/left_back_03_a.gif">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="div_import">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="td_label" width="100%" align="left" colspan="2">
                            <table>
                                <tr>
                                    <td width="20%">
                                        <a href="/ExcelModel/LeaveYearImportSample.xls">&nbsp;<asp:Label ID="lblUploadText"
                                            runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </td>
                                    <td width="55%">
                                        <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                    </td>
                                    <td width="5%">
                                        <asp:Button ID="btnImportSave" runat="server" class="button_2" OnClick="btnImportSave_Click"
                                            OnClientClick="javascript:UpProgress();"></asp:Button>
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
                        <td colspan="2"">
                            <div id="div_showdata">
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

                                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                    RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                    BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                    Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                                    AutoGenerateColumns="false">
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
                                                    <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="30%">
                                                                <Header Caption="<%$Resources:ControlText,gvErrorMsg %>">
                                                                </Header>
                                                                <CellStyle ForeColor="red">
                                                                </CellStyle>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="LeaveYear" Key="LeaveYear" IsBound="false"
                                                                Width="30%">
                                                                <Header Caption="<%$Resources:ControlText,gvLeaveYear %>">
                                                                </Header>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="LeaveDays" Key="LeaveDays" IsBound="false"
                                                                Width="30%">
                                                                <Header Caption="<%$Resources:ControlText,gvLeaveDays %>">
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
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
