<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleSelector.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.ModuleSelector" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模組選擇</title>

    <script type="text/javascript" src="../../JavaScript/jquery.js"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
     function AfterNodeSelChange(treeId, nodeId)
     {        
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;
        var deptName = node.getText().split("[")[0];
			    var  deptValue= node.getTag();
			    window.returnValue = { codeList: deptValue, nameList: deptName };
			    window.close();
		}
    
    
    $(function(){
      $("#tr_edit").toggle(
        function(){
            $("#div_showdata").hide();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#div_showdata").show();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
        }
    )
       });
    
    
        $(function() {
            $(".moduleTreeTableStyle td").height(20);
            $(":checkbox").click(function() {
                var dnameList = "", didList = "";
                $(":checked+a").each(function() {
                    dnameList= $(this).text();
                    didList = $(this).attr("href").replace("javascript:SelectModuleNode('", "").replace("')", "");
                });
                     
                window.returnValue = { codeList: didList, nameList: dnameList };
                window.close();
            });
        });
        //節點選中
        function SelectModuleNode(fid) {
            var chk = $("a[href='javascript:SelectmoduleNode('" + fid + "')']").prev();
            $(chk).attr("checked", !$(chk).attr("checked"));
        }
    </script>

</head>
<body style="background: #FFF;">
    <form id="form1" runat="server">
    <div  style="width:100%">
     <table cellspacing="0" cellpadding="0" class="table_title_area" style="width:100%">
            <tr style="width: 100%;" id="tr_edit">
                
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
                    <div id="Div1">
                        <img id="Img1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
       <%-- <asp:TreeView ID="treeModules" runat="server" ShowLines="true" NodeStyle-ForeColor="Black"
            CssClass="moduleTreeTableStyle">
        </asp:TreeView>--%>
<div id="div_showdata">
        <script language="javascript">document.write("<DIV id='div_2' style='height:390px;overflow-y:auto'>");</script>

        <ignav:UltraWebTree ID="UltraWebTreeData" runat="server" CheckBoxes="false" Indentation="20"
            WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
            DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
            ImageDirectory="../../CSS/Images/" LoadOnDemand="ManualSmartCallbacks" CompactRendering="False"
            SingleBranchExpand="True">
            <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                ForeColor="Blue" BackgroundImage="../../CSS/Images/overbg.bmp" BackColor="Navy">
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
                <DefaultImage Url="ig_treeFolder.gif" />
                <SelectedImage Url="ig_treeFolderOpen.gif" />
                <CollapseImage Url="ig_treeMinus.gif" />
                <ExpandImage Url="ig_treePlus.gif" />
            </Images>
            <ClientSideEvents AfterNodeSelectionChange="AfterNodeSelChange"></ClientSideEvents>
        </ignav:UltraWebTree>

        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>
</div>
    </div>
    </form>
</body>
</html>
