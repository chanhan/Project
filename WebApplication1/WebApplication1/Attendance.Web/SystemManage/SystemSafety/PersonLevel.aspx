<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonLevel.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.PersonLevel" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<%@ Register TagPrefix="ControlLib" TagName="Title" Src="../ControlLib/Title.ascx" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DeplevelAssignForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script>
    function Save()
    {
         $.ajax({
                type: "post", url: "PersonLevel.aspx", dateType: "text", data: {control: "Edit" },
                success: function(msg) {
                    if (msg==1) {alert(Message.UpdateSuccess); } 
                    else
                    {
                      alert(Message.UpdateFailed);
                    }
                  }
           });  
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
        )
         
   });
    </script>

</head>
<body >
    <form id="MyForm" method="post" runat="server">
    <div  style="width:100%">
        <input type="hidden" id="ProcessFlag" runat="server">
        <div style="width:100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area" style="width:100%">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center">
                        &nbsp;
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td>
                    <div id='div_1'>
                        <table>
                            <tr>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                    CssClass="input_linkbutton" OnClick="btnSave_Click">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--<td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td style="height: 25px;width: 45px;">
                                                <asp:LinkButton ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                    CssClass="input_linkbutton"  onclick="btnReset_Click">
                                                </asp:LinkButton>
                                                <input type="reset" runat="server"  ID="btnReset" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>--%>
                            </tr>
                        </table>
                        <ignav:UltraWebTree ID="UltraWebTreeDeplevel" runat="server" ImageDirectory="/ig_common/images/"
                            DefaultSelectedImage="ig_treeFolderOpen.gif" DefaultImage="ig_treeFolder.gif"
                            ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif" WebTreeTarget="ClassicTree"
                            Indentation="20" CheckBoxes="True" EnableViewState="True">
                            <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                BackgroundImage="../images/overbg.bmp">
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
    </div>
    </form>
</body>
</html>
