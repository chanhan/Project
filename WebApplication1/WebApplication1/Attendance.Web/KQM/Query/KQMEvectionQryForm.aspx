<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMEvectionQryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.Query.KQMEvectionQryForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script>
    function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
{
    var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("../../KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode+"&r="+Math.random(),window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	if(Revalue!=undefined)
	{	
			document.all(ReturnValueBoxName).value=Revalue.codeList;
			if(Revalue.codeList.length>1)
			{
			    document.all(ReturnDescBoxName).innerText=Revalue.nameList;
			}
	}
}
    $(function(){
       $('#<%=btnReset.ClientID %>').click(
       function(){
    $(':text').each(function(){
                $(this).val(null);
    });
        $("select").each(function(){
    $(this).get(0).selectedIndex=0;
    });
     return false;
       });
            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid,#td_show_1,#td_show_2").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
    });
</script>

<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="DefaultEmployeeNo" type="hidden" name="DefaultEmployeeNo" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
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
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="tr_edit" style="width: 100%">
        <asp:Panel ID="inputPanel" runat="server" Visible="true">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="td_label" width="10%">
                        &nbsp;
                        <asp:Label ID="lblDepcode" runat="server">Department:</asp:Label>
                    </td>
                    <td class="td_input" width="15%">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDcode" runat="server" Width="100%" CssClass="input_textBox" Style="display: none"></asp:TextBox>
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
                    <td class="td_label" style="width: 10%">
                        &nbsp;
                        <asp:Label ID="lbllblBillNo" runat="server">BillNo:</asp:Label>
                    </td>
                    <td class="td_input" style="width: 25%">
                        <asp:TextBox ID="txtBillno" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label" width="10%">
                        &nbsp;
                        <asp:Label ID="lblWorkNo" runat="server">WorkNo:</asp:Label>
                    </td>
                    <td class="td_input" width="15%">
                        <asp:TextBox ID="txtWorkno" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" width="10%">
                        &nbsp;
                        <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                    </td>
                    <td class="td_input" width="25%">
                        <asp:TextBox ID="txtLocalname" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                    </td>
                    <td class="td_label" width="15%">
                        &nbsp;
                        <asp:Label ID="lblEvectionType" runat="server">EvectionType:</asp:Label>
                    </td>
                    <td class="td_input" width="25%">
                        <asp:DropDownList ID="ddlEvectiontype" runat="server" Width="100%" CssClass="input_textBox">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td_label">
                        &nbsp;
                        <asp:Label ID="lblState" runat="server">State:</asp:Label>
                    </td>
                    <td class="td_input">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="input_textBox">
                        </asp:DropDownList>
                    </td>
                    <td class="td_label">
                        &nbsp;
                        <asp:Label ID="lbllblLeaveDate" runat="server">LeaveDate:</asp:Label>
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
                    <td class="td_label">
                        &nbsp;
                        <asp:Label ID="lblEvectionAddress" runat="server">EvectionAddress:</asp:Label>
                    </td>
                    <td class="td_input">
                        <asp:TextBox ID="txtEvectionaddress" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_label" colspan="6">
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnQuery %>"
                                            ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                        <asp:Button ID="btnReset" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                            ToolTip="Authority Code:Reset"></asp:Button>
                                        <asp:Button ID="btnExport" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnExport %>"
                                            ToolTip="Authority Code:Export" OnClick="btnExport_Click"></asp:Button>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="width: 100%">
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
                            ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                            Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px;">
                    <div id="img_grid">
                        <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
        <div id="tr_show">
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

                        <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridEvectionApply" runat="server" Width="100%" Height="100%"
                            OnDataBound="UltraWebGridEvectionApply_DataBound">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridEvectionApply" TableLayout="Fixed"
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
                                <igtbl:UltraGridBand BaseTableName="gds_att_evectionapply_v" Key="gds_att_evectionapply_v">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderWorkNo %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SEX" Key="SEX" IsBound="false" Width="40">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderSex %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderBUName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderDepName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTypeName" Key="EvectionTypeName" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEvectionTypeName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StartDateStr" Key="StartDateStr" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderStartDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EndDateStr" Key="EndDateStr" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEndDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EvectionTask" Key="EvectionTask" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEvectionTask %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EvectionDetail" Key="EvectionDetail" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEvectionDetail %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EvectionAddress" Key="EvectionAddress" IsBound="false"
                                            Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderEvectionAddress %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Proxy" Key="Proxy" IsBound="false" Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderProxy %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                            Width="50">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderStatusName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="50">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderModifier %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ModifyDateStr" Key="ModifyDateStr" IsBound="false"
                                            Width="110">
                                            <Header Caption="<%$Resources:ControlText,gvHeaderModifyDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EvectionType" Key="EvectionType" IsBound="false"
                                            Hidden="true">
                                            <Header Caption="">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="id" Key="id" IsBound="false" Hidden="true">
                                            <Header Caption="id">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="true">
                                            <Header Caption="Status">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="javascript">document.write("</DIV>");</script>

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
