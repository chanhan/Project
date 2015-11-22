<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgEmployeeQueryForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.OrgEmployeeQueryForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%--<%@ Register Src="../../ControlLib/UserLabel.ascx" TagName="UserLabel" TagPrefix="uc1" %>--%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%--<%@ Register TagPrefix="ControlLib" TagName="Title" Src="~/ControlLib/title.ascx" %>--%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%--<%@ Register Src="../../ControlLib/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="ControlLib" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OrgEmployeeForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>
    
    <link href="../../CSS/CommonCss.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function AfterNodeSelChange(treeId, nodeId)
     {        
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;
        var nodeText = igtree_getElementById("NodeText");        
		var ModuleCode = document.getElementById("ModuleCode").value;
		if(node.getLevel()==0||node.getLevel()==1)
		return;
        //document.frames("ParamsOrg").location.href="KqmParameterEditForm.aspx?OrgCode="+node.getTag();
        //document.frames("ParameterEmp").location.href="KqmParameterEmpForm.aspx?OrgCode="+node.getTag()+"&ModuleCode="+ModuleCode;
    } 
    
     $(function(){
        $("#div_img_1,#tr1").toggle(
            function(){
                $("#div_1").hide();
                $("#div_img_1").attr("src","../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
                $("#div_img_1").attr("src","../CSS/Images_new/left_back_03_a.gif");
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
  
   
   function Reset()
   {
     $("#txtDepCode").val("");
        $("#txtDepName").val("");
        $("#txtWorkNo").val("");
   return false;
   }
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <input id="txtDeptNo" type="hidden" name="txtDeptNo" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellpadding="0" width="100%" align="center">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr1">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat;  width: 80px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCondition" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <img id="div_img_1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div_1">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label" width="10%">
                                                        &nbsp;<asp:Label ID="lblDepcode" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="15%">
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
                                                                    <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="td_label" style="width: 10%">
                                                        &nbsp;<asp:Label ID="lblWorkNo" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td class="td_input" style="width: 15%">
                                                        <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" align="left">
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                            <asp:Button ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                                CssClass="button_2" OnClientClick="return Reset();"></asp:Button>
                                                            <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                    </td>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
    </table>
    </td> </tr> </table> </td> </tr> </table>
    <table cellspacing="1" cellpadding="0" width="100%" align="center">
        <tr valign="top">
            <td width="40%" align="left">
                <asp:Label ID="LabelOrg" runat="server"></asp:Label>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr valign="top">
                        <td>

                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*75/100+"'>");</script>

                            <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
                                WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
                                DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
                                ImageDirectory="../CSS/images/" LoadOnDemand="ManualSmartCallbacks" CompactRendering="False"
                                SingleBranchExpand="True" OnNodeClicked="UltraWebTreeDept_NodeClicked">
                                <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                    ForeColor="Blue" BackgroundImage="../../CSS/images/overbg.bmp" BackColor="Navy">
                                    <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                </SelectedNodeStyle>
                                <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../CSS/Images/overbg.bmp">
                                </HoverNodeStyle>
                                <Levels>
                                    <ignav:Level Index="0"></ignav:Level>
                                    <ignav:Level Index="1"></ignav:Level>
                                </Levels>
                                <Images>
                                    <DefaultImage Url="ig_treeFolder.gif" />
                                    <SelectedImage Url="ig_treeFolderOpen.gif" />
                                    <CollapseImage Url="ig_treeMinus.gif" />
                                    <ExpandImage Url="ig_treePlus.gif" />
                                </Images>
                                <ClientSideEvents AfterNodeSelectionChange="AfterNodeSelChange"></ClientSideEvents>
                            </ignav:UltraWebTree>

                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                        </td>
                    </tr>
                </table>
            </td>
            <td width="60%" align="left">
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td width="100%" height="25">
                            <asp:Label ID="lblBeSel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
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

                                        <script language="javascript">document.write("<DIV id='div_4' style='height:"+document.body.clientHeight*72/100+"'>");</script>

                                        <igtbl:UltraWebGrid ID="gridEmployee" runat="server">
                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                AllowRowNumberingDefault="ByDataIsland" Name="gridEmployee" TableLayout="Fixed"
                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
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
                                                <igtbl:UltraGridBand BaseTableName="TempTable" Key="TempTable">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                            Key="WorkNo" Width="60">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>" Fixed="true">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                            Key="LocalName" Width="60">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName%>" Fixed="true">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                            Key="DName" Width="120">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadParamsOrgDepName%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="LevelName" HeaderText="LevelName" IsBound="false"
                                                            Key="LevelName" Width="40">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadLevelName%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ManagerName" HeaderText="ManagerName" IsBound="false"
                                                            Key="ManagerName" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadManagerName%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="IsDirectlyUnder" HeaderText="IsDirectlyUnder"
                                                            IsBound="false" Key="IsManager" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadIsManager%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="IsTW" HeaderText="IsTW" IsBound="false" Key="IsTW"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadIsTW%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Tel" HeaderText="Tel" IsBound="false" Key="Tel"
                                                            Width="60">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadTel%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Notes" HeaderText="Notes" IsBound="false"
                                                            Key="Notes" Width="240">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadNotes%>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
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
            </td>
        </tr>
    </table>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    </form>

    <script type="text/javascript">
            document.getElementById("txtDepName").readOnly=true;
    --></script>

</body>
</html>
