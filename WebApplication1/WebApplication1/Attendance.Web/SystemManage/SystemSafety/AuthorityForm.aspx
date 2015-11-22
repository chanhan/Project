<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorityForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.AuthorityForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <style>
        </style>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script>


        function NodeCheck(treeId, nodeId, bChecked) {
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
        function NodeClick(treeId, nodeId, button) {
            var node = igtree_getNodeById(nodeId);
            var X = screen.availWidth * 25 / 100;
            var Y = screen.availHeight * 50 / 125;
         //   if (node != null && node.getTargetFrame() == "1")

            if (node != null)
             {
                var RoleCode = $("#<%=hidRoleCode.ClientID %>").val();
                var startIndex = node.Element.innerText.indexOf("(");
                var endIndex = node.Element.innerText.lastIndexOf(")");
                if (RoleCode != '') {
                    if (startIndex >= 0 && endIndex >= 0 && startIndex < endIndex && node.getChildNodes().length == 0) {
                        var functionList = node.Element.innerText.substring(startIndex + 1, endIndex);
                        var info = window.window.showModalDialog("AuthorityDetailForm.aspx?RoleCode=" + RoleCode + "&ModuleCode=" + node.getTag() + "&RoleCheck=" + node.getChecked() + "&functionList=" + functionList, window, "dialogWidth=650px;dialogHeight=260px;dialogLeft=" + X + "px;dialogTop=" + Y + ";help=no;status=no;scrollbars=no");
                        if (info) {
                            var value = $("#<%=hidFuncList.ClientID %>").val();
                            $("#<%=hidFuncList.ClientID %>").val(value + info.pmoduleCode + "|" + info.pfunctionList + "§");
                        }
                    }
                }
                else {
                    alert(Message.ModuleEditCheck);
                }
            }
        }
        
  function confirmsave()
    {
    var diasbleinfo = $("#<%=DisableInfo.ClientID %>").val();
    if(diasbleinfo=='Y')
    {
    return confirm(Message.EnableAndSaveRole);
    }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input  runat="server" id="hidSelectModuleCode"  type="hidden" value=""/>
    <input id="DisableInfo" type="hidden" name="DisableInfo" runat="server" />
    <div>
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
                                            <asp:Label ID="lbllblModuleCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="table_data_area" style="width: 100%">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" align="left" border="0" style="height: 25px;
                        background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif'); background-repeat: no-repeat;
                        background-position-x: center; width: 45px; text-align: center; font-size: 13px;">
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                    CssClass="input_linkbutton" OnClick="btnSave_Click" OnClientClick="return confirmsave();">
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="table_data_area">
                        <tr>
                            <td>
                                <ignav:UltraWebTree runat="server" ID="UltraWebTreeModule" CheckBoxes="true" Indentation="20">
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidRoleCode" runat="server" />
        <asp:HiddenField ID="hidFuncList" runat="server" />
    </div>
    </form>
</body>
</html>
