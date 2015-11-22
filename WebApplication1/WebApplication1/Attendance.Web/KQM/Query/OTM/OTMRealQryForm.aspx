<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMRealQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.OTM.OTMRealQryForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OTMRealApplyForm</title>
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
    
    
    
        function ShowDetail(workNo,KQDate,ShiftNo)
		{
		    document.all("PanelKQ").style.display="";
            document.all("PanelKQ").style.top=document.all("btnQuery").style.top;
            
            docallback(workNo,KQDate,ShiftNo);
		}		
        function docallback(workNo,KQdate,shiftNo)
        {
            if (workNo != "")
                {  
                 $.ajax(
                           {
                             type:"post",url:"OTMRealQryForm.aspx",dataType:"json",data:{WorkNo:workNo,KQDate:KQdate,ShiftNo:shiftNo},  
                             success:function(msg) 
                                {
                                    document.all("PanelKQ").style.display="block";
                                    document.getElementById('divKQ').innerText=""; 
                                    if (msg)
                                    {              
	                
	                                    var tblHtml= new   Array();
　　　　                                tblHtml[tblHtml.length] =   "<table cellspacing=0 cellpadding=0 width=100%>";
　　　　                                tblHtml[tblHtml.length] = "<tr>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label align=center height=25 width=20>"+Message.No+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=60>"+Message.WorkNo+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=70>"+Message.LocalName+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+Message.CardTime+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=90>"+Message.BellNo+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=120>"+Message.ReadTime+"</td>";
　　　　                                tblHtml[tblHtml.length]="<td class=td_label height=25 width=80>"+Message.CardNo+"</td>";
　　　　                                tblHtml[tblHtml.length]= '</tr>';
                                        $(msg).each(function(i){
　　                                     var WorkNo=msg[i].WorkNo==null?"&nbsp;":msg[i].WorkNo;
　　                                        var LocalName=msg[i].LocalName==null?"&nbsp":msg[i].LocalName;
　　                                        var CardTime=msg[i].CardTime==null?"&nbsp":$. jsonDTToString(msg[i].CardTime);
　　                                        var BellNo=msg[i].BellNo==null?"&nbsp":msg[i].BellNo;
　　                                        var ReadTime=msg[i].ReadTime==null?"&nbsp":$. jsonDTToString(msg[i].ReadTime);
　　                                        var CardNo=msg[i].CardNo==null?"&nbsp":msg[i].CardNo;
　　                                        tblHtml[tblHtml.length]= "<tr>";
　　                                        tblHtml[tblHtml.length]="<td class=td_label align=center height=25>"+(i+1)+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+WorkNo+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+LocalName+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+CardTime+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+BellNo+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+ReadTime+"</td>";
　　                                        tblHtml[tblHtml.length]="<td class=td_lbl height=25>"+CardNo+"</td>";
　　                                        tblHtml[tblHtml.length]="</tr>";
　　                                    });
　　　　                                tblHtml[tblHtml.length] ="</table>";
　　　　                                document.getElementById("divKQ").innerHTML = tblHtml.join("");
                                    }
                
                                    else
                                    {
                                        document.all("PanelKQ").style.display="none";
                                    }             
                                    
                                     }
                           });
                }
                return false;
        }
		function HiddenShowDetail()
		{
		    document.all("PanelKQ").style.display="none";
		    return false;
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
    $("#<%=txtEmployeeNo.ClientID %>").attr("value","");
    $("#<%=txtName.ClientID %>").attr("value","");
    $("#<%=txtBillNo.ClientID %>").attr("value","");
    $("#<%=txtHours.ClientID %>").attr("value","");
    $("#ddlHoursCondition").get(0).selectedIndex=0;
    $("#ddlOTType").get(0).selectedIndex=0;
    $("#ddlOTStatus").get(0).selectedIndex=0;
    $("#ddlPersonType").get(0).selectedIndex=0;
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
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
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
                            <table class="table_data_area" id="table_condition">
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%" style="table-layout: fixed">
                                                <tr class="tr_data_1">
                                                    <td style="width: 11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDepcode" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
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
                                                    <td style="width: 11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblBillNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                        <asp:TextBox ID="txtBillNo" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblHours" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 36%">
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
                                                                    <asp:TextBox ID="txtHours" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td style="width: 90px">
                                                        &nbsp;
                                                    </td>--%>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;<asp:Label ID="lblName" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTDateFrom" runat="server"></asp:Label>
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
                                                        <asp:Label ID="lblPersonType" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblOTType" runat="server"></asp:Label>
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
                                                        <asp:Label ID="lblOTStatus" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="23%">
                                                        <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                   <%-- <td>
                                                        &nbsp;
                                                    </td>--%>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td>
                                                        &nbsp;<asp:Label ID="lblIsProject" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlIsProject" runat="server" Width="100%">
                                                            <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                                            <asp:ListItem Value="N">N</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="8">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="btnQuery" class="button_2" runat="server" ToolTip="Authority Code:Query"
                                                                            CommandName="Query" OnClick="btnQuery_Click"></asp:Button>
                                                                        <asp:Button ID="btnReset" runat="server" ToolTip="Authority Code:Reset" class="button_2"
                                                                            CommandName="Reset" OnClientClick="return ButtonReset()"></asp:Button>
                                                                        <asp:Button ID="btnExport" class="button_2" runat="server" CommandName="Export" ToolTip="Authority Code:Export"
                                                                            OnClick="btnExport_Click"></asp:Button>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="PanelKQ" runat="server" Visible="true" Width="560" Style="padding-right: 3px;
                                                                        padding-left: 3px; z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px;
                                                                        background-color: #ffffee; border-right: #0000ff 1px solid; border-top: #0000ff 1px solid;
                                                                        border-left: #0000ff 1px solid; border-bottom: #0000ff 1px solid; position: absolute;
                                                                        left: 2%; float: left; display: none">
                                                                        <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="divKQ">
                                                                                                </div>
                                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                                    <tr>
                                                                                                        <td class="td_label" align="center">
                                                                                                            <asp:Button ID="btnClose" runat="server" CommandName="Close" class="button_2" ToolTip="Authority Code:Close"
                                                                                                                OnClientClick="return HiddenShowDetail()"></asp:Button>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
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
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%">
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
                                                <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_06.gif" height="18">
                                                    <img height="18" src="../../../CSS/Images_new/EMP_02.gif" width="19">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="19" background="../../../CSS/Images_new/EMP_05.gif">
                                                    &nbsp;
                                                </td>
                                                <td>

                                                    <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

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
                                                            <igtbl:UltraGridBand BaseTableName="OTM_RealApply" Key="OTM_RealApply">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealWorkNo%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealLocalName%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BuName" Key="BuName" IsBound="false" Width="120">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealBuName%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="170">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealDepName%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80"
                                                                        Format="yyyy/MM/dd">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealOTDate%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealOTType%>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="false" Width="50">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealWeek%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="false"
                                                                        Width="40">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealOverTimeType%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                                        Width="170">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealShiftDesc%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="AdvanceTime" Key="AdvanceTime" IsBound="false"
                                                                        Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealAdvanceTime%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="AdvanceHours" Key="AdvanceHours" IsBound="false"
                                                                        Width="40" Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealAdvanceHours%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OverTimeSpan" Key="OverTimeSpan" IsBound="false"
                                                                        Width="80" Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealOverTimeSpan%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RealHours" Key="RealHours" IsBound="false"
                                                                        Width="40" Format="0.0" AllowUpdate="No">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealRealHours%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConfirmHours" Key="ConfirmHours" IsBound="false"
                                                                        Width="60" Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealConfirmHours%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConfirmRemark" Key="ConfirmRemark" IsBound="false"
                                                                        Width="150" AllowUpdate="No">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealConfirmRemark%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="180">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealWorkDesc%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="IsProject" Key="IsProject" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealIsProject%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealStatusName%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="false" Width="50"
                                                                        Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealG1Total%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="false" Width="50"
                                                                        Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealG2Total%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="false" Width="50"
                                                                        Format="0.0">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealG3Total%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <%--<igtbl:UltraGridColumn BaseColumnName="MonthAllHours" Key="MonthAllHours" IsBound="true"
                                                                    Width="80" Format="0.0">
                                                                    <Header Caption="MonthAllHours">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>--%>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealRemark%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealBillNo%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ApproverName" Key="ApproverName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealApproverName%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                        Width="110">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealApproveDate%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealApRemark%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="70">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealModifier%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                                        Width="110">
                                                                        <Header Caption="<%$Resources:ControlText,gvOTMRealModifyDate%>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="false"
                                                                        Width="0" Hidden="true">
                                                                        <Header Caption="OTMSGFlag">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="true" Width="0" Hidden="false">
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
    </form>
</body>
</html>
