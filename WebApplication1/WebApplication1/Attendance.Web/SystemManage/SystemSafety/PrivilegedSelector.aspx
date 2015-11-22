<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivilegedSelector.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.PrivilegedSelector" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>權限選擇</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

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
       
       $(function(){
        $("#tr_yes").click(
            function(){
                
                 var dataValue="Y";
                 var desc="YES";
     window.returnValue = { codeList: dataValue, nameList: desc };
                window.close();
              return false;   
            }           
        )
       });
        $(function(){
        $("#tr_no").click(
            function(){
                
                 var dataValue="N";
                 var desc="NO";
     window.returnValue = { codeList: dataValue, nameList: desc };
                window.close();
              return false;   
            }           
        )
       });


    </script>

</head>
<base target="_self" />
<body>
    <form id="form1" runat="server">
    <div>
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
                    <div id="Div1">
                        <img id="Img1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
<%--        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="cursor: hand" id="tr_edit" bgcolor="lightblue">
                <td colspan="2" align="left">
                    <asp:Label ID="lblEditArea" runat="server"></asp:Label>
                </td>
                <td colspan="2" align="right">
                    <div id="img_edit">
                        <img id="div_img" class="img1" src="../../CSS/Images_new/left_back_03_a.gif" align="right"></div>
                </td>
            </tr>
        </table>--%>
    </div>
    <div id="div_showdata">
        <table  style=" width:100%;" border="1px" bordercolor="#6699ff" cellpadding="0" cellspacing="0" class="table_data_area" >
            <tr  style=" background-color:#BDDBFF">
                <td width="10%">
                    &nbsp;
                </td>
                <td width="45%">
                    <center>
                        <asp:Label ID="lblDataValue" runat="server"></asp:Label></center>
                </td>
                <td width="45%">
                    <center>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label></center>
                </td>
            </tr>
            <tr id="tr_yes" class="tr_data_1">
                <td width="10%">
                    <center>
                        1</center>
                </td>
                <td width="45%">
                    <center>
                        Y</center>
                </td>
                <td width="45%">
                    <center>
                        YES</center>
                </td>
            </tr>
            <tr id="tr_no" class="tr_data_2">
                <td width="10%">
                    <center>
                        2
                    </center>
                </td>
                <td width="45%">
                    <center>
                        N</center>
                </td>
                <td width="45%">
                    <center>
                        NO</center>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
