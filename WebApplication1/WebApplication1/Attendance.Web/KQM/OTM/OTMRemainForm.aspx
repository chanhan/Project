<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMRemainForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMRemainForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMRemainForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<style type="text/css">
        .a
        {
            border: 0;
        }
        .b
        {
            display: none;
        }
    </style>
<script type="text/javascript"><!--

        $(function(){
        $("#btnReturn,#innerTable_2").addClass("b");
        var importflag=$("#<%=ImportFlag.ClientID %>").val();
        if(importflag=="Import")
        {
          $("#btnQuery,#btnReset,#btnDelete").attr("disabled","disabled");
          $("#btnImport,#innerTable_1").addClass("b");
          $("#btnReturn,#innerTable_2").removeClass("b");
          document.getElementById("innerTable_2").style.display="";
        }
        
        $("#<%=btnImport.ClientID %>").click(function(){
        $("#btnQuery,#btnReset,#btnDelete").attr("disabled","disabled");
        $("#btnImport,#innerTable_1").addClass("b");
        $("#btnReturn,#innerTable_2").removeClass("b");
        document.getElementById("innerTable_2").style.display="";
        return false;
        })
        
        $("#<%=btnReturn.ClientID %>").click(function(){
        $("#btnQuery,#btnReset,#btnDelete").removeAttr("disabled");
        $("#btnImport,#innerTable_1").removeClass("b");
        $("#btnReturn,#innerTable_2").addClass("b");
        $("#<%=ImportFlag.ClientID %>").val(null);
        return false;
        })
        
        $("#tr_edit").toggle(function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_select").hide()},function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_select").show()});
        
         $("#div_img_2,#td_show_1,#td_show_2").toggle(function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_showdata").hide()},function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_showdata").show()});
        
        $("#tr_editimport").toggle(function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#tr_show_1,#tr_show_2,#tr_show_3").hide()},function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#tr_show_1,#tr_show_2,#tr_show_3").show()});
        })
        
        function DeleteConfirm()
        {
        return confirm(Message.DeleteAttTypeConfirm)
        }
        
        function QueryCheck()
        {
          var depname=$.trim($("#<%=txtDepName.ClientID %>").val());
          var workno=$.trim($("#<%=txtWorkNo.ClientID %>").val());
          if(depname.length==0&&workno.length==0)
          {
          alert(Message.DepNameOrWorkNoNotNull);
          return false;
          }
        }
        
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
//			document.getElementById("imgWaiting").style.display="";
//			document.getElementById("lblupload").innerText = "";		
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
          function CheckDelete()
            {
	        var grid = igtbl_getGridById('UltraWebGrid');
	        var gRows = grid.Rows;
	        var Count=0;
	        for(i=0;i<gRows.length;i++)
	        {
		        if(igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
		        {
		             Count+=1;
		        }
	        }			
	        if(Count==0)
	        {
	            alert("");
	            return false;
	        }
	        if(confirm(""))
	        {
		        FormSubmit("");
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
          }
          
          function setSelector()
        {
        var modulecode=$("#<%=ModuleCode.ClientID %>").val();
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

<body >
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server" />
    <asp:HiddenField ID="ImportFlag" runat="server" />
    <asp:HiddenField ID="ModuleCode" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%; cursor: hand;" id="tr_edit">

                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblCondition" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;">
                    <div id="img_edit">
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_select" style="width: 100%">
        <table class="table_data_area" style="width: 100%">
            <tr style="width: 100%">
                <td>
                    <table style="width: 100%">
                        <tr class="tr_data">
                            <td>
                                <asp:Panel ID="pnlContent" runat="server">
                                    <table class="table_data_area">
                                        <tr class="tr_data_1">
                                            <td width="8%">
                                                &nbsp;
                                                <asp:Label ID="lblOrgCode" runat="server" ></asp:Label>
                                            </td>
                                            <td width="17%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDepCode" runat="server" Width="100%" 
                                                                Style="display: none" ></asp:TextBox>
                                                        </td>
                                                        <td width="90%">
                                                            <asp:TextBox ID="txtDepName" runat="server"  Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="10%" style="cursor: hand" onclick="setSelector();">
                                                            <img id="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lbl_empno" runat="server"></asp:Label>
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" ></asp:TextBox>
                                            </td>
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lblLocalName" runat="server"></asp:Label>
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtLocalName" runat="server" Width="100%" ></asp:TextBox>
                                            </td>
                                            <td width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblYearMonth" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <igtxt:WebDateTimeEdit ID="txtYearMonth" runat="server" EditModeFormat="yyyy/MM"
                                                     Width="100%">
                                                </igtxt:WebDateTimeEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td>
                    <table>
                        <tr>
                            <td >
                            <asp:Panel ID="pnlShowPanel" runat="server">
                                <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClientClick="return QueryCheck();" OnClick="btnQuery_Click">
                                    </asp:Button>
                                <asp:Button ID="btnReset" runat="server" CssClass="button_1" OnClick="btnReset_Click">
                                    </asp:Button>
                                <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click">
                                    </asp:Button>

                                <asp:Button ID="btnImport" runat="server" CssClass="button_1">
                                    </asp:Button>
                                <asp:Button ID="btnReturn" runat="server" CssClass="button_1" >
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
    <div id="innerTable_2" style="display:none">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%; cursor:hand;" id="tr_editimport">

                        <td style="width: 100%;" class="tr_title_center">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblImportArea" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div id="div_editimg">
                                <img id="div_img_3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                        </td>
                    </tr>
                <tr id="tr_show_1">
                    <td width="100%" align="left" colspan="2">
                        <a href="../../ExcelModel/OtmRemainSample.xls">&nbsp;<%=Resources.ControlText.templateddown%>
                        </a>&nbsp;
                        <asp:FileUpload ID="FileUpload"  runat="server" Width="30%" />
                        <asp:Button ID="btnImportSave" runat="server" CssClass="button_1" OnClientClick="javascript:UpProgress();"
                            OnClick="btnImportSave_Click" />
                        <img id="imgWaiting" src="" border="0" style="display: none;
                            height: 20px;" />
                        <asp:Label ID="lblUpload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblUploadMsg" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr id="tr_show_2">
                    <td align="left" colspan="2" style="height: 25;">
                        &nbsp;<%=Resources.ControlText.importremark%>
                    </td>
                </tr>
                <tr id="tr_show_3">
                    <td colspan="2"">

                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
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
                                <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="30%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg %>">
                                            </Header>
                                            <CellStyle ForeColor="red">
                                            </CellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="YearMonth" Key="YearMonth" IsBound="false"
                                            Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvYearMonth %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="G1Remain" Key="G1Remain" IsBound="false" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvG1Remain %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="G23Remain" Key="G23Remain" IsBound="false"
                                            Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvG23Remain %>">
                                            </Header>
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
    </div>
    <div style="width: 100%" id="innerTable_1">
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
                <td style="width: 22px; cursor: hand;" id="td_show_2">
                    <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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
                                <igtbl:UltraGridBand BaseTableName="gds_att_remain_v" Key="gds_att_remain_v">
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
                                        <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                            Key="DepName" Width="20%">
                                            <Header Caption="<%$Resources:ControlText,gvOrgName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                            Width="15%">
                                            <Header Caption="<%$Resources:ControlText,gvOverTimeType %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="YearMonth" Key="YearMonth" IsBound="false"
                                            Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvYearMonth %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="G1Remain" Key="G1Remain" IsBound="false" Width="15%">
                                            <Header Caption="<%$Resources:ControlText,gvG1Remain %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="G23Remain" Key="G23Remain" IsBound="false"
                                            Width="17%">
                                            <Header Caption="<%$Resources:ControlText,gvG23Remain %>">
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
    </div>
    </form>
</body>
</html>
