<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.LeftMenu" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>左菜單</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="LeftNavBG">
        <div class="LeftNavBtn" style="overflow-y:scroll ;height:600px">
            <script language="javascript">document.write("<DIV id='menu_1'>");</script>
                <ignav:UltraWebTree ID="UltraWebTreeStandardMenu" runat="server" ImageDirectory="/ig_common/images/"
                    DefaultSelectedImage="ig_treeFolderOpen.gif" DefaultImage="ig_treeFolder.gif"
                    ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif" WebTreeTarget="ClassicTree"
                    Indentation="20" TargetFrame="WorkForm">
                    <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" ForeColor="Blue" BorderColor="Navy"
                        BorderStyle="None" BackgroundImage="../images/overbg.bmp">
                        <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                    </SelectedNodeStyle>
                    <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../images/overbg.bmp">
                    </HoverNodeStyle>
                    <Levels>
                        <ignav:Level Index="0"></ignav:Level>
                        <ignav:Level Index="1"></ignav:Level>
                    </Levels>
                </ignav:UltraWebTree>
            <script language="javascript">document.write("</DIV>");</script>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
