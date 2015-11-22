<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMAbsentMonthForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.Hr.KQM.Query.KQMAbsentMonthForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KQMAbsentMonthForm</title>
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
    
     function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="dept")
           {
           var url="../../../KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
           }
           var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
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
                    <tr style="cursor: hand">
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
                                    <tr class="tr_data_1" style="width:100%">
                                        <td class="td_label" width="10%">
                                            <asp:Label ID="lblWorkNo" runat="server"></asp:Label>
                                            <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../../CSS/Images_new/search_new.gif"></asp:Image>
                                        </td>
                                        <td class="td_input" width="10%">
                                            <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                            <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                border-bottom: #0000ff 1px solid; position: absolute; left: 8%; float: left;
                                                display: none;">
                                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                    <tr>
                                                        <td>
                                                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td class="td_label" width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
                                                                                    <font color="red">Ⅹ</font>
                                                                                    <asp:Label ID="Labelquerybatchworkno" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td_label" width="100%">
                                                                                    <asp:TextBox ID="txtBatchEmployeeNo" runat="server" TextMode="MultiLine" Height="100"
                                                                                        Width="100%" Style="display: none"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <iframe src="JavaScript:false" style="position: absolute; visibility: inherit; top: 0px;
                                                    left: 0px; width: 225px; height: 100px; z-index: -1; filter='progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                </iframe>
                                            </div>
                                        </td>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                            <asp:Label runat="server" id="lblDep" resourceid="common.syb" ></asp:Label>
                                        </td>
                                        <td class="td_input" width="15%">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox_1"
                                                            Style="display: none"></asp:TextBox>
                                                    </td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td style="cursor: hand">
                                                        <asp:Image ID="imgDepCode" runat="server" ImageUrl="~/CSS/Images_new/search_new.gif"></asp:Image>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                            <asp:Label runat="server" id="lblFromdate" resourceid="common.fromdate" ></asp:Label>
                                        </td>
                                        <td class="td_input" width="15%">
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="10%">
                                            &nbsp;
                                           <asp:Label runat="server" id="lblTodate" resourceid="common.todate" >
                                           </asp:Label>
                                        </td>
                                        <td class="td_input" width="15%">
                                            <asp:TextBox ID="txtEndDate" runat="server" Width="100%" CssClass="input_textBox_1"></asp:TextBox>
                                        </td>
                                        <td class="td_label" align="left" width="5%">
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnQuery" runat="server" Text="Query" ToolTip="Authority Code:Query" CssClass="button_2"
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
        //2011-8-16 工號可以多選查詢
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="";
            document.all("PanelBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display="";
            document.getElementById("txtWorkNo").value="";
            document.getElementById("txtBatchEmployeeNo").value="";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        
         function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").style.display="none";
            document.getElementById("txtBatchEmployeeNo").value="";
        }
	--></script>

</body>
</html>
