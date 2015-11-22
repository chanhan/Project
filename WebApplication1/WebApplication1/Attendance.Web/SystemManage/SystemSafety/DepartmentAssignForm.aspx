<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentAssignForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.DepartmentAssignForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用戶關聯組織</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
        function DepNodeCheck(treeId, nodeId, bChecked)
        {
//            var node = igtree_getNodeById(nodeId);
//	        if(node != null)
//	        {
//	            var nodes=node.getChildNodes();
//                var i=0,enabled=true;
//                var iLevel=node.getLevel();
//                if(node.getChecked() || !node.getEnabled())
//                {
//                    enabled=false;
//                }
//                for(i=0;i<nodes.length;i++)
//                {
//                    if(nodes[i].getLevel()-iLevel<2)
//                    {
//                        nodes[i].setEnabled(enabled);
//                        nodes[i].setChecked(false);  
//                    }
//                }
//	        }
            var count = 0;
            var node = igtree_getNodeById(nodeId);
            if (node != null) {
                var nodes = node.getChildNodes();
                var i = 0;
                for (i = 0; i < nodes.length; i++) {
                    nodes[i].setChecked(bChecked);
                }
            }
            checkParentNode(node);
        }
        
        function checkParentNode(node) {
            var pnode = node.getParent();
            if (pnode != null) {
                var nodes = pnode.getChildNodes();
                var i = 0, chk = false;
                for (i = 0; i < nodes.length; i++) {
                    if (nodes[i].getChecked()) {
                        chk = true;
                        break;
                    }
                }
                if (chk) {
                    $("#"+pnode.Id+">:checkbox").attr("checked", true);
                      //node.setChecked(chk);
                }
               else{
                   $("#"+pnode.Id+">:checkbox").attr("checked", false);
                }
                checkParentNode(pnode);
            }
        }
        
    </script>

</head>
<body class="color_body" >
    <form id="formPersonDept" method="post" runat="server">
    <table class="top_table" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td>
                <div id="div_1">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        
                        <tr>
                            <!-----------------Module---------------->
                            <td width="32%" valign="top">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand" class="tr_data_1">
                                        <td class="tr_title_center" width="100%" height="25">
                                            <asp:Label ID="lblModule" runat="server" Text="模組代碼"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <ignav:UltraWebTree ID="UltraWebTreeModule" runat="server" Indentation="20" WebTreeTarget="ClassicTree"
                                                    CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif" DefaultImage="ig_treeFolder.gif"
                                                    DefaultSelectedImage="ig_treeFolderOpen.gif" ImageDirectory="/ig_common/images/"
                                                    OnNodeClicked="UltraWebTreeModule_NodeClicked">
                                                    <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Orange" BorderStyle="None"
                                                        ForeColor="Blue" BackgroundImage="../images/overbg.bmp" BackColor="Orange">
                                                        <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                    </SelectedNodeStyle>
                                                    <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../images/overbg.bmp">
                                                    </HoverNodeStyle>
                                                    <Levels>
                                                        <ignav:Level Index="0"></ignav:Level>
                                                        <ignav:Level Index="1"></ignav:Level>
                                                    </Levels>
                                                </ignav:UltraWebTree>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-----------------Company---------------->
                            <td width="32%" valign="top">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand" class="tr_data_1">
                                        <td class="tr_title_center" width="100%" height="25">
                                            <asp:Label ID="lblCompany" runat="server" Text="公司代碼"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ignav:UltraWebTree ID="UltraWebTreeCompany" runat="server" ImageDirectory="/ig_common/images/"
                                                DefaultSelectedImage="ig_treeFolderOpen.gif" DefaultImage="ig_treeFolder.gif"
                                                ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif" WebTreeTarget="ClassicTree"
                                                Indentation="20" CheckBoxes="false" EnableViewState="True" OnNodeClicked="UltraWebTreeCompany_NodeClicked">
                                                <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Orange" BorderStyle="None"
                                                    ForeColor="Blue" BackgroundImage="../images/overbg.bmp" BackColor="Orange">
                                                    <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                </SelectedNodeStyle>
                                                <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../images/overbg.bmp">
                                                </HoverNodeStyle>
                                                <Levels>
                                                    <ignav:Level Index="0"></ignav:Level>
                                                    <ignav:Level Index="1"></ignav:Level>
                                                </Levels>
                                            </ignav:UltraWebTree>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-----------------Orginazation---------------->
                            <td width="32%" valign="top">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand" class="tr_data_1">
                                        <td class="tr_title_center" width="100%" height="25">
                                            <asp:Label ID="lblDEep" runat="server" Text="單位"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tr_title_center" width="100%" height="25">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr class="tr_data_1">
                                                    <td width="60%">
                                                        <asp:Label ID="lblDepSelect" runat="server" Text="請選擇要顯示的組織層級"></asp:Label>
                                                    </td>
                                                    <td width="40%">
                                                        <asp:DropDownList ID="ddlDepLevel" runat="server" Width="100%" AutoPostBack="true"
                                                            CssClass="input_textBox" OnSelectedIndexChanged="ddlDepLevel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                            <td >
                                <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click">
                                </asp:Button>
                            </td>
                        </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <ignav:UltraWebTree ID="UltraWebTreeDepartment" runat="server" CheckBoxes="True"
                                                    Indentation="20" WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif"
                                                    ExpandImage="ig_treePlus.gif" DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
                                                    ImageDirectory="/ig_common/images/">
                                                    <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                                        ForeColor="Blue" BackgroundImage="../images/overbg.bmp" BackColor="Navy">
                                                        <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                                    </SelectedNodeStyle>
                                                    <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../images/overbg.bmp">
                                                    </HoverNodeStyle>
                                                    <Levels>
                                                        <ignav:Level Index="0"></ignav:Level>
                                                        <ignav:Level Index="1"></ignav:Level>
                                                    </Levels>
                                                    <ClientSideEvents NodeChecked="DepNodeCheck" NodeClick=""></ClientSideEvents>
                                                </ignav:UltraWebTree>
                                            </div>
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
    </form>
</body>
</html>
