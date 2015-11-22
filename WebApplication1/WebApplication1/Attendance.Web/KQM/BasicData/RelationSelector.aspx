<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationSelector.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.RelationSelector" %>

<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>RelationSelector</title>
    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>
    <link href="../../CSS/CommonCss.css" rel="stylesheet" type="text/css"></link>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <base target="_self"></base>

    <script type="text/javascript">
		function NodeClick(treeId, nodeId, button)
		{
		    var node = igtree_getNodeById(nodeId);
			if(node != null)
			{

			    var deptName = node.getText().split("[")[0];
			    var  deptValue= node.getTag();
			    window.returnValue = { codeList: deptValue, nameList: deptName };
			    window.close();
			}
		}
	 $(function(){
       $("#tr_show").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images/downarrows_white.gif");
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images/uparrows_white.gif");
            }
        ) 
     });
    </script>

</head>
<body class="color_body">
    <form id="MyForm" method="post" runat="server">
    <asp:HiddenField ID="hidDataType" runat="server" />
    <table class="top_table" cellspacing="1" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand; background-color: #66CCCC;" id="tr_show">
                        <td class="tr_table_title" align="left">
                            <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                        </td>
                        <td class="tr_table_title" align="right">
                            <img class="img1" src="../../CSS/Images/uparrows_white.gif">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkAbate" runat="server" AutoPostBack="True" OnCheckedChanged="chkAbate_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_1">
                                <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
                                    WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
                                    DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
                                    ImageDirectory="/ig_common/images/">
                                    <selectednodestyle cursor="Hand" borderwidth="1px" bordercolor="Navy" borderstyle="None"
                                        forecolor="Blue" backgroundimage="../images/overbg.bmp" backcolor="Navy">
                                            <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                        </selectednodestyle>
                                    <clientsideevents nodeclick="NodeClick" />
                                    <hovernodestyle cursor="Hand" forecolor="Black" backgroundimage="/images/overbg.bmp">
                                        </hovernodestyle>
                                    <levels>
                                            <ignav:Level Index="0"></ignav:Level>
                                            <ignav:Level Index="1"></ignav:Level>
                                        </levels>
                                    <images>
                                            <DefaultImage Url="ig_treeFolder.gif" />
                                            <SelectedImage Url="ig_treeFolderOpen.gif" />
                                            <CollapseImage Url="ig_treeMinus.gif" />
                                            <ExpandImage Url="ig_treePlus.gif" />
                                        </images>
                                </ignav:UltraWebTree>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="TxtRoleCode" type="hidden" name="TxtRoleCode" runat="server">
    </form>
</body>
</html>
