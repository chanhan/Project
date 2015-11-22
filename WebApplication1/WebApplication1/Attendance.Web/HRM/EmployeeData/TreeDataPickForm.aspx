<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeDataPickForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.EmployeeData.TreeDataPickForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript">
		function NodeClick(treeId, nodeId, button)
		{
		    var node = igtree_getNodeById(nodeId);
			if(node != null)
			{
                    var temVal= node.getText().split("[");
			        window.returnValue=node.getTag()+";"+temVal[0];
			    window.close();
			}
		}
		$(function(){
				            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
		});
    </script>

</head>
<base target="_self"></base>
<body class="color_body">
    <form id="MyForm" method="post" runat="server">
    <input id="HiddenDataType" type="hidden" name="HiddenDataType" runat="server">
    <table class="top_table" cellspacing="1" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="img_edit">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div>
                                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkAbate" Text="" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxAbate_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="tr_edit">
                                <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
                                    WebTreeTarget="ClassicTree" CollapseImage="../../CSS/Images/ig_treeMinus.gif"
                                    ExpandImage="../../CSS/Images/expand.gif" DefaultImage="../../CSS/Images/ig_treefolder.gif"
                                    DefaultSelectedImage="../../CSS/Images/ig_treefolderopen.gif" ImageDirectory="/ig_common/images/"
                                    LoadOnDemand="ManualSmartCallbacks" CompactRendering="False" SingleBranchExpand="True">
                                    <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Orange" BorderStyle="None"
                                        ForeColor="Blue" BackgroundImage="../images/overbg.bmp" BackColor="Orange">
                                        <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                    </SelectedNodeStyle>
                                    <ClientSideEvents NodeClick="NodeClick" />
                                    <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../../CSS/Images/overbg.bmp">
                                    </HoverNodeStyle>
                                    <Levels>
                                        <ignav:Level Index="0"></ignav:Level>
                                        <ignav:Level Index="1"></ignav:Level>
                                    </Levels>
                                    <Images>
                                        <DefaultImage Url="../../CSS/Images/ig_treefolder.gif" />
                                        <SelectedImage Url="../../CSS/Images/ig_treefolderopen.gif" />
                                        <CollapseImage Url="../../CSS/Images/ig_treeMinus.gif" />
                                        <ExpandImage Url="../../CSS/Images/expand.gif" />
                                    </Images>
                                </ignav:UltraWebTree>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="txtRoleCode" type="hidden" name="txtRoleCode" runat="server">
    </form>
</body>
</html>
