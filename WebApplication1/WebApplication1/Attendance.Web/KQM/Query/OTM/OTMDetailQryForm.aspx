<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMDetailQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.OTM.OTMDetailQryForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assess</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
    

    function AfterSelectChange(tableName, itemName) 
	{
		var row = igtbl_getRowById(itemName);
		return 0;
	}
	function UltraWebGridModule_InitializeLayoutHandler(gridName)
	{
		var row = igtbl_getActiveRow(gridName);
	}
		function OpenWindows(QueryType)//彈出新增或修改頁面
		{
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    switch(QueryType)
		    {
		        case "Advance":
                    document.getElementById("topTable").style.display="";
                    document.getElementById("div_2").style.display="";
                    document.getElementById("divApproved").style.display="none";
                    document.getElementById("btnAdvanceApply").className="selected_button";
                    document.getElementById("btnRealApply").className="audit_button";
                    document.getElementById("btnExceptionApply").className="audit_button";
                    break;
		        case "Real":
		            if(document.getElementById("iframeApproved").src!="OTMRealQryForm.aspx?ModuleCode="+ModuleCode)
		            {
                      document.getElementById("iframeApproved").src="OTMRealQryForm.aspx?ModuleCode="+ModuleCode;
                    }
                    document.getElementById("topTable").style.display="none";
                    document.getElementById("div_2").style.display="none";
                    document.getElementById("divApproved").style.display=""; 
                    document.getElementById("btnAdvanceApply").className="audit_button";
                    document.getElementById("btnRealApply").className="selected_button";
                    document.getElementById("btnExceptionApply").className="audit_button";
                    break;
		        case "Exception":
		            if(document.getElementById("iframeApproved").src!="OTMExceptionQryForm.aspx?ModuleCode="+ModuleCode)
		            {
                      document.getElementById("iframeApproved").src="OTMExceptionQryForm.aspx?ModuleCode="+ModuleCode;
                    }
                    document.getElementById("topTable").style.display="none";
                    document.getElementById("div_2").style.display="none";
                    document.getElementById("divApproved").style.display="";                    
                    document.getElementById("btnAdvanceApply").className="audit_button";
                    document.getElementById("btnRealApply").className="audit_button";
                    document.getElementById("btnExceptionApply").className="selected_button";
                    break;                    
		    }
            return false;
		}
		
	
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="";
            document.all("PanelBatchWorkNo").style.top=document.all("txtEmployeeNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        
         function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            //document.getElementById("txtBatchEmployeeNo").value="";
            var empno=$.trim($("#<%=txtBatchEmployeeNo.ClientID%>").val()); 
            var i;
            var result = "";
            var c;
            for (i = 0; i < empno.length; i++) {
             c = empno.substr(i, 1);
             if (c == "\n")
               result = result + "§";
             else if (c != "\r")
               result = result + c;
              }
            //document.getElementById("txtEmployeeNo").value=result;
            document.getElementById("txtBatchEmployeeNo").value=result;
        }
        function BatchLoseFocus()
        {
            HiddenBatchWorkNo();
            return true;
        }
        
        
        $(function(){
    $("#tr_edit").toggle(
        function(){
            $("#table_condition").hide();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#table_condition").show();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
       
        }
    )
    }
    )
    $(function(){
    $("#div_img2,#td_show_1,#td_show_2").toggle(
        function(){
            $("#table_display").hide();
            $(".img2").attr("src","../../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#table_display").show();
            $(".img2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
        }
     )
     })
     function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="dept")
           {
           var url="../../BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r="+Math.random();
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
       
       
   function ButtonReset()
    {
    $("#<%=txtDepCode.ClientID %>").attr("value","");
    $("#<%=txtDepName.ClientID %>").attr("value","");
    $("#<%=txtBillNo.ClientID %>").attr("value","");
    $("#<%=txtEmployeeNo.ClientID %>").attr("value","");
    $("#<%=txtName.ClientID %>").attr("value","");
    $("#<%=txtHours.ClientID %>").attr("value","");
    $("#<%=txtBatchEmployeeNo.ClientID %>").attr("value","");
    $("#ddlHoursCondition").get(0).selectedIndex=0;
    $("#ddlPersonType").get(0).selectedIndex=0;
    $("#ddlOTType").get(0).selectedIndex=0;
    $("#ddlOTStatus").get(0).selectedIndex=0;
    $("#ddlIsProject").get(0).selectedIndex=0;
    var myDate = new Date();
    var year=myDate.getFullYear();
    var month=myDate.getMonth()+1>9?(myDate.getMonth()+1):"0" + (myDate.getMonth()+1);
    var date=myDate.getDate()>9?myDate.getDate():"0"+myDate.getDate();
    $("#<%=txtOTDateFrom.ClientID %>").val(year+"/"+month+"/01");
    $("#<%=txtOTDateTo.ClientID %>").val(year+"/"+month+"/"+date);
     return false;

    }   
   
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
        <table cellspacing="1" cellpadding="0" width="100%" align="center">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" class="table_title_area">
                        <tr class="tr_title_center">
                            <td width="100%">
                                <asp:Button ID="btnAdvanceApply" Width="110px" Height="24px" runat="server" CssClass="selected_button"
                                    Text="AdvanceApply" ToolTip="Authority Code:AdvanceApply" CommandName="AdvanceApply"
                                    OnClientClick="return OpenWindows('Advance')"></asp:Button>
                                <asp:Button ID="btnRealApply" Width="110px" Height="24px" runat="server" CssClass="audit_button"
                                    Text="RealApply" ToolTip="Authority Code:RealApply" CommandName="RealApply" OnClientClick="return OpenWindows('Real')">
                                </asp:Button>
                                <asp:Button ID="btnExceptionApply" Width="110px" Height="24px" runat="server" CssClass="audit_button"
                                    Text="ExceptionApply" ToolTip="Authority Code:ExceptionApply" CommandName="ExceptionApply"
                                    OnClientClick="return OpenWindows('Exception')"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="1" id="topTable" cellpadding="0" style="width: 100%" align="center">
            <tr style="width: 100%">
                <td style="width: 100%">
                    <table cellspacing="1" cellpadding="0" style="width: 100%" align="center">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                                <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                                    <td style="width: 100%;">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                                                        <div id="Div1">
                                                            <img id="Img1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td>
                                <table id="table_condition" style="width: 100%">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <div id="div_1" style="width: 100%">
                                                <table cellspacing="0" cellpadding="0"  class="table_data_area" style="table-layout: fixed">
                                                    <tr class="tr_data_1">
                                                        <td style="width: 10%">
                                                            &nbsp;
                                                            <asp:Label ID="lblDepcode" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="20%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox_1"
                                                                            Style="display: none"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100%">
                                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="cursor: hand">
                                                                        <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="td_label" style="width: 10%">
                                                            
                                                            <asp:Label ID="lblBillNo" runat="server" Text="BillNo"></asp:Label>
                                                        </td>
                                                        <td class="td_input" style="width: 20%">
                                                            <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                        </td>
                                                        <td class="td_label" style="width: 10%">
                                                            <asp:Label ID="lblHours" runat="server" Text="Hours"></asp:Label>
                                                        </td>
                                                        <td class="td_input" style="width: 27%">
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        <asp:DropDownList ID="ddlHoursCondition" runat="server" Width="100%">
                                                                            <asp:ListItem Value="=" Selected="True">=</asp:ListItem>
                                                                            <asp:ListItem Value=">">></asp:ListItem>
                                                                            <asp:ListItem Value="<"><</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:TextBox ID="txtHours" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--<td style="width: 100px">
                                                            &nbsp;
                                                        </td>--%>
                                                    </tr>
                                                    <tr class="tr_data_2">
                                                        <td class="td_label" width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblEmployeeNo" runat="server" ResourceID="common.employeeno"></asp:Label>
                                                            <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif"
                                                                Style="cursor: hand"></asp:Image>
                                                        </td>
                                                        <td class="td_input" width="22%">
                                                            <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                            <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                                z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                                border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                                border-bottom: #0000ff 1px solid; position: absolute; left: 11%; float: left;
                                                                display: none;">
                                                                <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tr style="width: 100%">
                                                                                                <td width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                                    <font color="red">Ⅹ</font>
                                                                                                    <asp:Label ID="Labelquerybatchworkno" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="width: 100%">
                                                                                                <td width="100%">
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
                                                                <iframe src="JavaScript:false" style="position: absolute; visibility: hidden; top: 0px;
                                                                    left: 0px; width: 225px; height: 100px; z-index: -1; filter: 'progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                                </iframe>
                                                            </div>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblName" runat="server" ResourceID="common.name"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblOTDateFrom" runat="server" ResourceID="kqm.otm.date.from"></asp:Label>
                                                        </td>
                                                        <td width="23%">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="txtOTDateFrom" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                    </td>
                                                                    <td width="8%">
                                                                        ~
                                                                    </td>
                                                                    <td width="46%">
                                                                        <asp:TextBox ID="txtOTDateTo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--<td>
                                                            &nbsp;
                                                        </td>--%>
                                                    </tr>
                                                    <tr class="tr_data_1">
                                                        <td width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblPersonType" runat="server" ResourceID="kqm.otm.person.type"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;<asp:Label ID="lblOTType" runat="server" ResourceID="kqm.otm.type"></asp:Label>
                                                        </td>
                                                        <td width="22%">
                                                            <asp:DropDownList ID="ddlOTType" runat="server" Width="100%">
                                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="G1">G1</asp:ListItem>
                                                                <asp:ListItem Value="G2">G2</asp:ListItem>
                                                                <asp:ListItem Value="G3">G3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="11%">
                                                            &nbsp;
                                                            <asp:Label ID="lblOTStatus" runat="server" ResourceID="kqm.otm.status"></asp:Label>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <%--<td>
                                                            &nbsp;
                                                        </td>--%>
                                                    </tr>
                                                    <tr class="tr_data_2">
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblIsProject" runat="server" ResourceID="otm.realapply.isproject"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlIsProject" runat="server" Width="100%">
                                                                <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Value="Y">Y</asp:ListItem>
                                                                <asp:ListItem Value="N">N</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <%--<td>
                                                            &nbsp;
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100%; height: 25px;">
                                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="btnQuery" runat="server" ToolTip="Authority Code:Query" CssClass="button_2"
                                                                            CommandName="Query" OnClick="btnQuery_Click" OnClientClick="return BatchLoseFocus()">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnReset" runat="server" ToolTip="Authority Code:Reset" CssClass="button_2"
                                                                            CommandName="Reset" OnClientClick="return ButtonReset()"></asp:Button>
                                                                        <asp:Button ID="btnExport" runat="server" Text="Export" CommandName="Export" ToolTip="Authority Code:Export"
                                                                            CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                                            </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--<td>
                                                            &nbsp;
                                                        </td>--%>
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
                                                <tr style="width: 100%;">
                                                    <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
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
                                                                ImagePath="../../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                                ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../../CSS/Images_new/search01.gif"
                                                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                            </ess:AspNetPager>
                                                        </div>
                                                    </td>
                                                    <td style="width: 22px;" id="td_show_2">
                                                        <img id="div_img2" class="img2" width="22px" height="24px" src="../../../CSS/Images_new/left_back_03_a.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="table_display">
                                    <tr>
                                        <td colspan="3">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                <tr style="width: 100%">
                                                    <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_05.gif" height="18">
                                                        <img height="18" src="../../../CSS/Images_new/EMP_01.gif" width="19">
                                                    </td>
                                                    <td background="../../../CSS/Images_new/EMP_07.gif" height="19px">
                                                    </td>
                                                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                                                        <img height="18" src="../../../CSS/Images_new/EMP_02.gif" width="19">
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td width="19" background="../../../CSS/Images_new/EMP_05.gif">
                                                        &nbsp;
                                                    </td>
                                                    <td>

                                                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*57/100+"'>");</script>

                                                        <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGrid_DataBound">
                                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
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
                                                                <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                                    DataKeyField="" BaseTableName="OTM_AdvanceApply" Key="OTM_AdvanceApply">
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWorkNo%>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                            Width="80">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceLocalName%>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="BuName" Key="BuName" IsBound="false" Width="140">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBuName%>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="140">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceDepName%>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="0" Hidden="true">
                                                                            <Header Caption="ID" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="0"
                                                                            Hidden="true">
                                                                            <Header Caption="Status" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="dCode" Key="dCode" IsBound="false" Width="0"
                                                                            Hidden="true">
                                                                            <Header Caption="dCode" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80" Format="yyyy/MM/dd">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOTDate%>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                            Width="50">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOverTimeType%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="50">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWeek%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="50">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceOTType%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="false"
                                                                            Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBeginTime%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceEndTime%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Hours" Key="Hours" IsBound="false" Width="40"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceHours%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceStatusName%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ImportFlag" Key="ImportFlag" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceImportFlag%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="false" Width="50"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG1Total%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="false" Width="120"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG2Total%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="false" Width="130"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceG3Total%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <%--<igtbl:UltraGridColumn BaseColumnName="MonthAllHours" Key="MonthAllHours" IsBound="true"
                                                                    Width="80" Format="0.0">
                                                                    <Header Caption="MonthAllHours">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>--%>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="250">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceWorkDesc%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceRemark%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBillNo%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ApproverName" Key="ApproverName" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproverName%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                            Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproveDate%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="100">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="70">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifier%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                                            Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifyDate%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="false"
                                                                            Width="0" Hidden="true">
                                                                            <Header Caption="OTMSGFlag">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false"
                                                                            Width="0" Hidden="true">
                                                                            <Header Caption="IsProject">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>

                                                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                                    </td>
                                                    <td width="19" background="../../../CSS/Images_new/EMP_06.gif">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="19" background="../../../CSS/Images_new/EMP_03.gif" height="18">
                                                        &nbsp;
                                                    </td>
                                                    <td background="../../../CSS/Images_new/EMP_08.gif" height="18">
                                                        &nbsp;
                                                    </td>
                                                    <td width="19" background="../../../CSS/Images_new/EMP_04.gif" height="18">
                                                        &nbsp;
                                                    </td>
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

        <script language="javascript">document.write("<div id='divApproved' style='display:none;height:"+document.body.clientHeight*90/100+"'>");</script>

        <table cellspacing="1" cellpadding="0" width="100%" height="600px" align="center">
            <tr>
                <td>
                    <iframe id="iframeApproved" src="" width="100%" height="100%" frameborder="0" scrolling="no"
                        style="border: 0"></iframe>
                </td>
            </tr>
        </table>

        <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    </div>
    </form>

    <script type="text/javascript"><!--     
      document.getElementById("txtEmployeeNo").focus();
      document.getElementById("txtEmployeeNo").select();
	--></script>

</body>
</html>
