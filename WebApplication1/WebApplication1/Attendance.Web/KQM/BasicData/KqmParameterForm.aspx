<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KqmParameterForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KqmParameterForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考勤參數設定</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function AfterNodeSelChange(treeId, nodeId)
     {        
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;
        //var nodeText = igtree_getElementById("node");
        var orgName=node.getText(node).split("[")[0];
		var ModuleCode = document.getElementById("ModuleCode").value;
		if(node.getLevel()==0||node.getLevel()==1)
		return;
        document.frames("ParamsOrg").location.href="KqmParameterEditForm.aspx?OrgCode="+node.getTag()+"&ModuleCode="+ModuleCode;
        document.frames("ParameterEmp").location.href="KqmParameterEmpForm.aspx?OrgCode="+node.getTag()+"&ModuleCode="+ModuleCode;
    } 
    
    
     $(function(){
        $("#tr_org").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
              $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       
       $("#tr_emp").toggle(
            function(){
                $("#div_5").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_5").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       }); 
        
    </script>

</head>
<body style="position:absolute; overflow:auto">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <table cellspacing="1" id="topTable" cellpadding="0" style="width:100%" align="center">
        <tr>
            <td style="width:100%;">
                <table  cellspacing="0" cellpadding="1" style="width:100%"  align="left">
                    <tr style="height:40%">
                        <td style="width:100%;height:100%" >
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand" id="tr_org">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr_edit">
                                                
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02_morelarge.gif');
                                                        background-repeat:no-repeat; background-position-x: center; width: 120px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblParameterArea" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <div id="img_edit">
                                                        <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <div id='div_1'>
                                            <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" height="250px">
                                                <tr valign="top">
                                                    <td width="40%">

                                                        <script language="javascript">document.write("<DIV id='div_2' style='height:100%'>");</script>

                                                        <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20" Height="100%" 
                                                            WebTreeTarget="ClassicTree" CollapseImage="../../CSS/Images/ig_treeMinus.gif" ExpandImage="../../CSS/Images/ig_treePlus.gif"
                                                            DefaultImage="../../CSS/Images/ig_treeFolder.gif" DefaultSelectedImage="../../CSS/Images/ig_treeFolderOpen.gif" 
                                                            ImageDirectory="../../CSS/Images/" LoadOnDemand="ManualSmartCallbacks" CompactRendering="False" 
                                                            SingleBranchExpand="True">
                                                            <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                                                ForeColor="Blue" BackgroundImage="../../CSS/Images/ui-bg_gloss-wave_35_f6a828_500x100.png" BackColor="Orange">
                                                                <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                            </SelectedNodeStyle>
                                                            <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../../CSS/Images/ui-bg_gloss-wave_35_f6a828_500x100.png">
                                                            </HoverNodeStyle>
                                                            <Levels>
                                                                <ignav:Level Index="0"></ignav:Level>
                                                                <ignav:Level Index="1"></ignav:Level>
                                                            </Levels>
                                                            <Images>
                                                                <DefaultImage Url="../../CSS/Images/ig_treeFolder.gif" />
                                                                <SelectedImage Url="../../CSS/Images/ig_treeFolderOpen.gif" />
                                                                <CollapseImage Url="../../CSS/Images/ig_treeMinus.gif" />
                                                                <ExpandImage Url="../../CSS/Images/ig_treePlus.gif" />
                                                            </Images>
                                                            <ClientSideEvents AfterNodeSelectionChange="AfterNodeSelChange"></ClientSideEvents>
                                                        </ignav:UltraWebTree>

                                                       <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                                    </td>
                                                    <td width="60%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                                                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">

                                                        <script language="javascript">document.write("<DIV id='div_3' style='height:100%'>");</script>

                                                        <iframe name="ParamsOrg" src="KqmParameterEditForm.aspx?ModuleCode=<%=this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString()%>" width="100%" height="100%"
                                                            scrolling="no"></iframe>

                                                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style=" height:60%">
                        <td style="width:100%;height:100%">
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%"  height="100%">
                                <tr style="cursor: hand" id="tr_emp">
                                    <td>
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr1">
                                                
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02_morelarge.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 120px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblParameterEmpArea" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 22px;">
                                                    <div id="Div1">
                                                        <img id="Img1" class="img2" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr >
                                    <td colspan="2">
                                        <div id='div_5' >

                                            <script language="javascript">document.write("<DIV id='div_4' style='height:300px'>");</script>

                                            <iframe name="ParameterEmp" src="KqmParameterEmpForm.aspx?ModuleCode=<%=this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString()%>"
                                                width="100%" height="300px" scrolling="no"></iframe>

                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

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
    </form>
</body>
</html>
