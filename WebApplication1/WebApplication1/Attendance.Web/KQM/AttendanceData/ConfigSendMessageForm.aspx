<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSendMessageForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.ConfigSendMessageForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript">
    
		function CheckSave()
        {
            if(confirm(Message.ConfirmBatchConfirm)){return true;}
            else{return false;}
        }
        function NodeCheck(treeId, nodeId, bChecked)
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
 
		function NodeClick(treeId, nodeId, button)
		{
		    var node = igtree_getNodeById(nodeId);
		    var X=screen.availWidth*25/100;
		    var Y=screen.availHeight*50/125;
		   
			if(node != null && node.getTargetFrame()=="1")
			{
			    document.frames("Authority").location.href="ConfigSendMessageEditForm.aspx?moduleCode="+node.getTag()+"&SqlDep=true&RoleCheck="+node.getChecked()+"&Flag=1";
				//var ReValue=window.showModalDialog("ConfigSendMessageEditForm.aspx?moduleCode="+node.getTag()+"&SqlDep=true&RoleCheck="+node.getChecked(),window,"dialogWidth=650px;dialogHeight=460px;dialogLeft="+X+"px;dialogTop="+Y+";help=no;status=no;scrollbars=no"); 
			}
			else
			{
			    document.frames("Authority").location.href="ConfigSendMessageEditForm.aspx?moduleCode="+node.getTag()+"&SqlDep=true&RoleCheck="+node.getChecked()+"&Flag=0";
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
    <table class="top_table" cellpadding="0" cellspacing="1" width="100%" align="center">
        <tr>
            <td width="55%" valign="top">
                <div style="width: 100%;">
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
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
                                <div>
                                    <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="td_label" colspan="6">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                            <asp:Button ID="btnSave" runat="server" class="button_2" OnClick="btnSave_Click">
                                            </asp:Button>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="div_1">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="100%" valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>

                                            <script language="javascript">document.write("<DIV id='div_2' style='height:150'>");</script>

                                            <%--<asp:CheckBoxList ID="CheckBoxListModuleCode" runat="server" RepeatColumns="1" RepeatLayout="Flow">
                                                                        </asp:CheckBoxList>--%>
                                            <ignav:UltraWebTree ID="UltraWebTreeModule" runat="server" CheckBoxes="True" Indentation="20"
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
                                                <ClientSideEvents NodeClick="NodeClick" NodeChecked="NodeCheck"></ClientSideEvents>
                                            </ignav:UltraWebTree>

                                            <script language="javascript">document.write("</DIV>");</script>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td width="45%" valign="top">
                <iframe name="Authority" src="ConfigSendMessageEditForm.aspx" width="100%" height="500px"
                    scrolling="auto" id="Iframe1" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
