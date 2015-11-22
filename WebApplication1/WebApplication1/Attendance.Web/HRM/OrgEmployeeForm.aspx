<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgEmployeeForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.OrgEmployeeForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script>
    function AfterNodeSelChange(treeId, nodeId) {        
        var node = igtree_getNodeById(nodeId);
        if(node == null)
            return;    
    	var ModuleCode = document.getElementById("ModuleCode").value;
	    if(node.getLevel()==0||node.getLevel()==1||node.getLevel()==2)
	    {
	       // return;
	    }
        document.frames("OrgEmployee").location.href="OrgEmployeeEditForm.aspx?OrgCode="+node.getTag()+"&moduleCode="+ModuleCode;
        
    }
    $(document).ready(function()
    {
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
<body>
    <form id="form1" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
                <td style="width: 100%;" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDep" runat="server"></asp:Label>
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
    <div id='tr_edit' style="height: 100%;">
        <table class="inner_table" cellspacing="0" cellpadding="0" width="100%" style="height: 100%;">
            <tr valign="top" style="height: 100%">
                <td width="30%" style="height: 100%; overflow-y: scroll">
                    <div style="height: 600px; overflow-x: scroll; overflow-y: scroll">
                        <ignav:UltraWebTree runat="server" ID="UltraWebTreeDept" CheckBoxes="true" Indentation="20"
                            SingleBranchExpand="True">
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
                            <ClientSideEvents AfterNodeSelectionChange="AfterNodeSelChange"></ClientSideEvents>
                        </ignav:UltraWebTree>
                    </div>
                </td>
                <td style="height: 100%" width="70%">
                    <iframe name="OrgEmployee" src="OrgEmployeeEditForm.aspx?ModuleCode=<%=this.Request.QueryString["moduleCode"] == null ? "" : this.Request.QueryString["moduleCode"].ToString()%>"
                        width="100%" height="100%" scrolling="auto" frameborder="0"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
