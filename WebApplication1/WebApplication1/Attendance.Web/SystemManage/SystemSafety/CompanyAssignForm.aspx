<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyAssignForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.CompanyAssignForm" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CompanyAssignForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    $(function(){
    $("#tr_edit").toggle(function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
    $("#div_select").hide()},function(){
    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
    $("#div_select").show()});
    })</script>

</head>
<body class="color_body">
    <form id="Form1" method="post" runat="server">
    <div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%; cursor: hand;" id="tr_edit">
                    <td style="width: 17px;">
                    </td>
                    <td style="width: 100%;" class="tr_title_center">
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img_1" class="div_img_1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id='div_select'>
            <div>
                <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClick="btnSave_Click">
                </asp:Button>
            </div>
            <ignav:UltraWebTree ID="UltraWebTreeCompany" runat="server" ImageDirectory="/ig_common/images/"
                DefaultSelectedImage="ig_treeFolderOpen.gif" DefaultImage="ig_treeFolder.gif"
                ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif" WebTreeTarget="ClassicTree"
                Indentation="20" CheckBoxes="True" EnableViewState="True">
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
            </ignav:UltraWebTree>
        </div>
    </div>
    </form>
</body>
</html>
