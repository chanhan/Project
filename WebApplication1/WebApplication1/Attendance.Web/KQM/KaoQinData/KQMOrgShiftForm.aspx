<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMOrgShiftForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMOrgShiftForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

<script src="../../JavaScript/jquery.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

<script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function AfterNodeSelChange(treeId, nodeId) {        
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;
        var nodeText = igtree_getElementById("NodeText");        
		var ModuleCode = document.getElementById("ModuleCode").value;
		if(node.getLevel()==0||node.getLevel()==1)
		    return;
        document.frames("OrgShift").location.href="KQMOrgShiftEditForm.aspx?OrgCode="+node.getTag()+"&ModuleCode="+ModuleCode;
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
</script>

<body class="color_body" scroll=yes>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
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
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblOrgShift" runat="server"></asp:Label>
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
                                        <div id='div_edit'>
                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                <tr valign="top">
                                                    <td width="45%">

                                                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*90/100+"'>");</script>

                                                        <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
                                                            WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
                                                            DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
                                                            ImageDirectory="/ig_common/images/" LoadOnDemand="ManualSmartCallbacks" CompactRendering="False"
                                                            SingleBranchExpand="True">
                                                            <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                                                ForeColor="Blue" BackgroundImage="../../CSS/Images/overbg.bmp" BackColor="Navy">
                                                                <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                            </SelectedNodeStyle>
                                                            <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
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
                                                    <td width="55%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                                                        border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">

                                                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*90/100+"'>");</script>

                                                        <iframe name="OrgShift" src="KQMOrgShiftEditForm.aspx?ModuleCode=<%=this.Request.QueryString["ModuleCode"] == null ? "" : this.Request.QueryString["ModuleCode"].ToString()%>"
                                                            width="100%" height="100%" scrolling="no"></iframe>

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
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
