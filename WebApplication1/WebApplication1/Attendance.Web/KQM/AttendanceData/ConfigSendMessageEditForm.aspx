<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSendMessageEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.ConfigSendMessageEditForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>定制系統Mail提醒功能</title>
    <base target="_self"></base>

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript">   
    function DepNodeCheck(treeId, nodeId, bChecked)
    {
        var node = igtree_getNodeById(nodeId);
        if(node != null)
        {
            var nodes=node.getChildNodes();
            var i=0;
            for(i=0;i<nodes.length;i++)
            {
                nodes[i].setChecked(bChecked);
            }
        }
    }
   $(function(){
    $("#tr_edit").toggle(
        function(){
            $("#div_1").hide();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#div_1").show();
            $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
        }
    )}) 
    </script>

</head>
<body class="color_body">
    <form id="Form1" method="post" runat="server">
    <div>
        <tr style="cursor: hand">
            <td class="tr_table_title" width="100%" height="25">
                <table style="width: 100%">
                    <tr>
                        <td style="cursor: hand;">
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="img_edit">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblReGetDepcode" runat="server"></asp:Label>
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
    </div>
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="td_label" colspan="6">
                <asp:Button ID="btnSave" runat="server" class="button_2" OnClick="btnSave_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    <div id="div_1">
        <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td>

                    <script language="javascript">document.write("<DIV id='div_Orginazation' style='height:"+document.body.clientHeight*82/100+"'>");</script>

                    <ignav:UltraWebTree ID="UltraWebTreeDep" runat="server" CheckBoxes="True" Indentation="20"
                        WebTreeTarget="ClassicTree" CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif"
                        DefaultImage="ig_treeFolder.gif" DefaultSelectedImage="ig_treeFolderOpen.gif"
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
                        <ClientSideEvents NodeChecked="DepNodeCheck"></ClientSideEvents>
                    </ignav:UltraWebTree>

                    <script language="javascript">document.write("</DIV>");</script>

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
