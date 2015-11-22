<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportKaoQinSumForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.Query.ReportKaoQinSumForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EmpAccidentForm</title>
    <link href="../../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">
     
        $(function(){
    $("#tr_edit").toggle(
        function(){
            $("#div_1").hide();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03.gif");
            
        },
        function(){
          $("#div_1").show();
            $(".img1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
       
        }
    )
    }
    )
    
     function setSelector(ctrlCode,ctrlName,flag)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="dept")
           {
           var url="../../../KQM/BasicData/RelationSelector.aspx?moduleCode="+'KQMSYS306';
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
           var info=window.showModalDialog(url,null,fe);
           if(info)
           {
               $("#"+ctrlCode).val(info.codeList);
               $("#" + ctrlName).val(info.nameList);
           }
           return false;           
           
       }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="1" id="Table1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand" onclick="turnit('div_1','div_img_1','<%=sAppPath%>');">
                        <td class="tr_table_title">
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                    <td style="width: 100%;">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCondition" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div id="Div1">
                                            <img id="Img1" class="img1" width="22px" height="23px" src="../../../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_1" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr class="tr_data_1">
                                        <td class="td_label" width="8%" style="display: none">
                                            &nbsp;
                                            <asp:Label runat="server" ID="lblDep"  Style="display: none"></asp:Label>
                                        </td>
                                        <td class="td_input" width="12%" style="display: none">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox_1"
                                                            Style="display: none"></asp:TextBox>
                                                    </td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox_1" Width="100%"
                                                            Style="display: none"></asp:TextBox>
                                                    </td>
                                                    <td style="cursor: hand">
                                                        <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif"
                                                            Style="display: none"></asp:Image>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="td_label" width="8%">
                                            &nbsp;
                                            <%--<asp:Label runat="server" ID="lblToDate"></asp:Label>--%>
                                            <asp:Label runat="server" ID="gvEndDate"></asp:Label>
                                        </td>
                                        <td class="td_input" width="12%">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="8%" style="display: none">
                                            &nbsp;
                                            <asp:Label ID="lblArea" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_input" width="12%" style="display: none">
                                            <asp:DropDownList ID="ddlArea" Width="100%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_label" align="left">
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnQuery"  runat="server" CssClass="button_1" Text="Query" ToolTip="Authority Code:Query"
                                                CommandName="Query" OnClick="btnQuery_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>

                <script type="text/javascript">document.write("<DIV id='DivReportViewer' style='height:"+document.body.clientHeight*85/100+"'>");</script>

                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowBackButton="True" ShowFindControls="False"
                    Width="100%" Height="90%" ShowZoomControl="False" ProcessingMode="Remote" ExportContentDisposition="AlwaysInline"
                    Font-Names="Verdana" SizeToReportContent="True" HyperlinkTarget="" BackColor=""
                    CssClass="td_label" OnDrillthrough="ReportViewer1_Drillthrough" OnBack="ReportViewer1_Back">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>

                <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript"><!-- 
        document.getElementById("txtDepName").readOnly=true;
	--></script>

</body>
</html>
