<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmDepartmentForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HRM.HrmDepartmentForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HrmDepartmentForm</title>
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
     $(function(){
    $("#tr_edit").toggle(function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
    $("#tr_show").hide()},function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
    $("#tr_show").show()});
    })  
    
    //點擊節點事件
    function AfterNodeSelChange(treeId, nodeId) {   
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;
        var nodeText = igtree_getElementById("NodeText");        
		var ModuleCode = document.getElementById("ModuleCode").value;
		//update  事業處下面部門也可以擴建 原因就是node的第三層也可以擴建
		//if(node.getLevel()==0||node.getLevel()==1||node.getLevel()==2)
	    if(node.getLevel()==0||node.getLevel()==1)
	        return;
        document.frames("OrgShift").location.href="HrmDepartmentEditForm.aspx?DepCode="+node.getTag()+"&ModuleCode="+ModuleCode;
    }
    //節點新增
    function ToggleAdd() {    
        var DepName=document.getElementById("HiddenAdd").value;        
        var DepCode=document.getElementById("HiddenAddDepCode").value;
        var tree = igtree_getTreeById("UltraWebTreeData");
        var node = tree.getSelectedNode();
        var newnode;
        if (node)
        {
            newnode = node.addChild(DepName);
            newnode.setTag(DepCode);
        }
    }
    
    //節點刪除
    function ToggleDelete() {
        var tree = igtree_getTreeById("UltraWebTreeData");
        var node = tree.getSelectedNode();
        if(node == null)
            return;
        //tree.setSelectedNode(null);
        node.remove(node);        
    }
    
    //節點設是否有效
    function ToggleEnabled() {
        var EnableFlag=document.getElementById("HiddenEnabled").value;
        var tree = igtree_getTreeById("UltraWebTreeData");
        var node = tree.getSelectedNode();
        if(node != null) {
            if(EnableFlag=="true")
            {                
                node.setText(node.getText().replace("[Disabled]",""));
            }
            else
            {               
                node.setText(node.getText()+"[Disabled]"); 
            }
            //tree.setSelectedNode(null);
            //node.setEnabled(EnableFlag);
        }
        
    }
    //修改節點名稱
    function ChangeNodeText() {
        var newText=document.getElementById("HiddenChange").value;
        var tree = igtree_getTreeById("UltraWebTreeData");
        var node = tree.getSelectedNode();
        if(node != null) {
            node.setText(newText);
        }
    }
    </script>
    
</head>
<body >
    <form id="form1" runat="server">
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
        <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
        <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server" />
        <input id="HiddenChange" type="hidden" name="HiddenChange" runat="server" onclick="ChangeNodeText()" />
        <input id="HiddenEnabled" type="hidden" name="HiddenEnabled" runat="server" onclick="ToggleEnabled()" />
        <input id="HiddenDelete" type="hidden" name="HiddenDelete" runat="server" onclick="ToggleDelete()" />
        <input id="HiddenAdd" type="hidden" name="HiddenAdd" runat="server" onclick="ToggleAdd()" />
        <input id="HiddenAddDepCode" type="hidden" name="HiddenAddDepCode" runat="server" />
        <table cellspacing="1" id="Table1" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand;" >
                                    <div style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                                            <tr style="width: 100%;" id="tr_edit">
                                                <td style="width: 100%;" class="tr_title_center">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                        font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
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
                                </tr>
                                <tr id="tr_show">
                                    <td colspan="2">
                                        <div >
                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                <tr valign="top">
                                                     <td width="50%">

                                                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*80/100+"'>");</script>

                                                            <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
                                                                WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
                                                                DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
                                                                ImageDirectory="/ig_common/images/" LoadOnDemand="ManualSmartCallbacks" CompactRendering="False"
                                                                SingleBranchExpand="True">
                                                                <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Orange" BorderStyle="None"
                                                                    ForeColor="Blue" BackgroundImage="../../images/overbg.bmp" BackColor="Orange">
                                                                    <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                                </SelectedNodeStyle>
                                                                <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../../images/overbg.bmp">
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
                                                        <td width="50%" style="border: #330000; border-style: dashed; border-top-width: 1px;
                                                            border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px;">

                                                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*80/100+"'>");</script>

                                                            <iframe name="OrgShift" src="HrmDepartmentEditForm.aspx?DepCode=<% =this.HiddenDepCode.Value%>&ModuleCode=<%=base.Request.QueryString["ModuleCode"] == null ? "" : base.Request.QueryString["ModuleCode"].ToString()%>"
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
