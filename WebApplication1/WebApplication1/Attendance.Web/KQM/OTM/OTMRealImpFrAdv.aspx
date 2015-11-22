<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMRealImpFrAdv.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMRealImpFrAdv" %>

<%--<%@ Register Src="../../ControlLib/UserLabel.ascx" TagName="UserLabel" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%--<%@ Register Src="../../ControlLib/title.ascx" TagName="title" TagPrefix="ControlLib" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

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
		function CheckImport()
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
		             break;			         
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if(confirm(Message.InportRealApplyConfirm))
	        {
		        document.getElementById("btnInportRealApply").style.display="none";	
		        document.getElementById("imgBatchWaiting").style.display="";
		        document.getElementById("labeBatchConfirm").innerText = "";		        
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
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
        $("#img_grid,#tr_edit").toggle(
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
         
       function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="DepCode")
           {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;
       } 
	--></script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center">
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
                                                <td style="width: 17px;">
                                                    <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                                                </td>
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                                        font-size: 13px;">
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
                                        <div id="div_edit">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDept" runat="server" ResourceID="common.organise" />
                                                    </td>
                                                    <td class="td_input" width="22%">
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
                                                    <td class="td_label" width="11%">
                                                        &nbsp;<asp:Label ID="lblIsProject" runat="server" ResourceID="otm.realapply.isproject" />
                                                    </td>
                                                    <td class="td_input" width="19%">
                                                        <asp:DropDownList ID="ddlIsProject" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_input" width="25%">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td class="td_label" width="26%">
                                                                    &nbsp;
                                                                    <asp:Label ID="lblOTDateForm" runat="server" ResourceID="kqm.otm.date.from" />
                                                                </td>
                                                                <td width="33%">
                                                                    <asp:TextBox ID="txtOTDateFrom" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                                <td width="8%">
                                                                    ~
                                                                </td>
                                                                <td width="33%">
                                                                    <asp:TextBox ID="txtOTDateTo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 90px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server" ResourceID="common.employeeno" />
                                                    </td>
                                                    <td class="td_input" width="22%">
                                                        <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="11%">
                                                        &nbsp;<asp:Label ID="lblName" runat="server" ResourceID="common.name" />
                                                    </td>
                                                    <td class="td_input" width="19%">
                                                        <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 90px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_label" colspan="7">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                        CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                                        CssClass="button_2" OnClick="btnReset_Click"></asp:Button>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                        CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                                </td>
                                                                <td width="30%">
                                                                    <asp:DropDownList ID="ddlImportType" runat="server" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnInportRealApply" runat="server" Text="<%$Resources:ControlText,btnInportRealApply %>"
                                                                        CssClass="button_mostlarge" OnClick="btnInportRealApply_Click" OnClientClick="return CheckImport()">
                                                                    </asp:Button>
                                                                </td>
                                                                <td>
                                                                    <img id="imgBatchWaiting" src="/images/clocks.gif" border="0" style="display: none;
                                                                        height: 20px;" />
                                                                    <asp:Label ID="labeBatchConfirm" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr_edit">
                                                <td style="width: 17px;">
                                                    <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                                                </td>
                                                <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblGrid" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="tr_title_center" style="width: 300px;">
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

                                                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*77/100+"'>");</script>

                                                        <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%">
                                                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                    CssClass="tr_header">
                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                                    </BorderDetails>
                                                                </HeaderStyleDefault>
                                                                <FrameStyle Width="100%" Height="100%">
                                                                </FrameStyle>
                                                                <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                                    AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                                                <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
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
                                                                <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                                    DataKeyField="" BaseTableName="OTM_AdvanceApply" Key="OTM_AdvanceApply">
                                                                    <Columns>
                                                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                            HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                                            <CellTemplate>
                                                                                <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                            </CellTemplate>
                                                                            <HeaderTemplate>
                                                                                <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                            <Header Caption="CheckBox" ClickAction="Select" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:TemplatedColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="100">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadDepName%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="0" Hidden="true">
                                                                            <Header Caption="ID">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="0"
                                                                            Hidden="true">
                                                                            <Header Caption="Status">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                            Width="50">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadOverTimeType%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadOTDate%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadWeek%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadOTType%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="false"
                                                                            Width="70" Format="HH:mm">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadBeginTime%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="70"
                                                                            Format="HH:mm">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadEndTime%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="AdvanceHours" Key="AdvanceHours" IsBound="false"
                                                                            Width="50" Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadHours%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="200">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadWorkDesc%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ImportRemark" Key="ImportRemark" IsBound="false"
                                                                            Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadImportRemark%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                            Width="80">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadStatusName%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="120">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTShiftNo" Key="OTShiftNo" IsBound="false"
                                                                            Hidden="true">
                                                                            <Header Caption="OTShiftNo">
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
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    <script type="text/javascript"><!--  
		document.all("txtDepName").readOnly=true;
	--></script>

    </form>
</body>
</html>
