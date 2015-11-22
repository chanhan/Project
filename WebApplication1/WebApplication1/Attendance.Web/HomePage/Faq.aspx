<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Faq.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.HomePage.Faq" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
        
            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            $("#<%=btnAdd.ClientID %>").click(function() { $("#<%=pnlSelect.ClientID %>").hide(); $("#<%=pnlAddNew.ClientID %>").show(); return false; });
            $("#<%=btnReturn.ClientID %>").click(function() { $("#<%=pnlSelect.ClientID %>").show(); $("#<%=pnlAddNew.ClientID %>").hide(); return false; });
            $("#<%=btnSave.ClientID %>").click(function() {
                var valid = true;
                $("#<%=txtFaqTitle.ClientID %>,#<%=txtFaqContent.ClientID %>,#<%=ddlFaqTypeId.ClientID %>").each(function() {
                    if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { valid = false; $(this).css("border-color", "#ff6666"); }
                }); if ($.browser.msie && $.browser.version < 8) { $("select").each(function() { if ($(this).val()) { $(this).css("background-color", "#ffffff"); } else { $(this).css("background-color", "#ffaaaa"); } }) }
                return valid;
            });
        });
            function OpenDetail(type, id) {
            var sFeature = "resizable:no;dialogHeight:700px; dialogWidth:790px; status:no;"
            if ($.browser.msie) {
                if ($.browser.version < 7) {
                    sFeature = "resizable:no;dialogHeight:733px; dialogWidth:796px; status:no;"
                }
            }
            var url = "";
            switch (type) {
                case "Notice": url = "NoticeDetail.aspx?noticeId=" + id; break;
                case "Faq": url = "FaqDetail.aspx?faqSeq=" + id; break;
                case "Paper": url = "PaperDetail.aspx?PaperSeq=" + id; break;
            }
            window.showModalDialog(url+"&r="+Math.random(), id, sFeature);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td1">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblFaq" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 22px;" id="td2">
                    <div id="img_edit">
                        <img id="div_img_1" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" id="tr_edit" width="100%">
        <tr>
            <td class="tbTopLeft">
            </td>
            <td class="tbTopMiddle">
            </td>
            <td class="tbTopRight">
            </td>
        </tr>
        <tr>
            <td class="tbMiddleLeft">
                &nbsp;
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" class="QueryTable" width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlSelect" runat="server" Width="100%">
                                <table cellpadding="0" cellspacing="0" class="QueryTable" width="100%">
                                    <tr style="background-color: #f5f5f5;">
                                        <td align="right" style="width: 17%;">
                                            <asp:Label ID="lblSelFaqTitle" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 38%;">
                                            <asp:TextBox ID="txtSelFaqTitle" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 15%;">
                                            <asp:Label ID="lblSelFaqType" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 30%;">
                                            <asp:DropDownList ID="ddlSelFaqType" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #fff;">
                                        <td align="right">
                                            <asp:Label ID="lblSelFaqDate" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtSelFaqDateStart" runat="server" Width="120px"></asp:TextBox>~<asp:TextBox
                                                ID="txtSelFaqDateEnd" runat="server" Width="120px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkOnlyFamiliar" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: #f5f5f5; height: 45px;">
                                        <td colspan="4" align="center">
                                            <table>
                                                <tr>
                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                        <td>
                                                            <asp:Button ID="btnAdd" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                ToolTip="Authority Code:Add"></asp:Button>
                                                            <asp:Button ID="btnQuery" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddNew" runat="server" Width="100%" Style="display: none">
                                <table cellpadding="0" cellspacing="0" class="QueryTable" width="100%">
                                    <tr style="background-color: #f5f5f5;">
                                        <td width="17%" align="right">
                                            <asp:Label ID="lblFaqDate" runat="server"></asp:Label>
                                        </td>
                                        <td width="33%" align="left">
                                            <asp:TextBox ID="txtFaqDate" runat="server" Width="150px" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td width="17%" align="right">
                                            <asp:Label ID="lblFaqTypeId" runat="server"></asp:Label>
                                        </td>
                                        <td width="33%" align="left">
                                            <asp:DropDownList ID="ddlFaqTypeId" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #fff;">
                                        <td align="right">
                                            <asp:Label ID="lblEmpNo" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmpNo" runat="server" Width="150px" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmpName" runat="server" Width="150px" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #f5f5f5;">
                                        <td align="right">
                                            <asp:Label ID="lblEmpPhone" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmpPhone" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEmpEmail" runat="server" Text="Mail："></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmpEmail" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #fff;">
                                        <td align="right">
                                            <asp:Label ID="lblFaqTitle" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:TextBox ID="txtFaqTitle" runat="server" Width="540px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #f5f5f5;">
                                        <td align="right">
                                            <asp:Label ID="lblFaqContent" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:TextBox ID="txtFaqContent" runat="server" Width="540px" TextMode="MultiLine"
                                                Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #fff;">
                                        <td align="center" colspan="4">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="420px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #f5f5f5; height: 45px;">
                                        <td colspan="4" align="center">
                                            <table>
                                                <tr>
                                                    <asp:Panel ID="pnlAddPanel" runat="server">
                                                        <td>
                                                            <asp:Button ID="btnSave" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                ToolTip="Authority Code:Save" OnClick="btnSave_Click"></asp:Button>
                                                            <asp:Button ID="btnReturn" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnReturn %>"
                                                                ToolTip="Authority Code:Return"></asp:Button>
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="tbMiddleRight">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbBottomLeft">
            </td>
            <td class="tbBottomMiddle">
            </td>
            <td class="tbBottomRight">
            </td>
        </tr>
    </table>
    <div style="width: 100%" id="PanelData">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tr_title_center" style="width: 300px;">
                    <div>
                        <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                            HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                            ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px;" id="td_show_2">
                    <div id="img_grid">
                        <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                    </div>
                </td>
            </tr>
        </table>
        <div id="tr_show">
            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                <tr style="width: 100%">
                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                        <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19">
                    </td>
                    <td background="../../CSS/Images_new/EMP_07.gif" height="19px">
                    </td>
                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                        <img height="18" src="../../CSS/Images_new/EMP_02.gif" width="19">
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td width="19" background="../../CSS/Images_new/EMP_05.gif">
                        &nbsp;
                    </td>
                    <td>

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridProblem" runat="server" Width="100%" Height="100%"
                            OnDataBound="UltraWebGridProblem_DataBound">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridProblem" TableLayout="Fixed"
                                AutoGenerateColumns="false" CellClickActionDefault="NotSet">
                                <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                    CssClass="tr_header">
                                    <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                    </BorderDetails>
                                </HeaderStyleDefault>
                                <FrameStyle Width="100%" Height="100%">
                                </FrameStyle>
                                <ClientSideEvents DblClickHandler="UltraWebGridProblem_DblClickHandler"></ClientSideEvents>
                                <SelectedRowStyleDefault ForeColor="Black" BackColor="#ffcc00">
                                </SelectedRowStyleDefault>
                                <RowAlternateStyleDefault Cursor="Hand" BackColor="#e7f0ff">
                                </RowAlternateStyleDefault>
                                <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#6699ff" BorderStyle="Solid"
                                    CssClass="tr_data1">
                                    <Padding Left="3px"></Padding>
                                    <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                </RowStyleDefault>
                            </DisplayLayout>
                            <Bands>
                                <igtbl:UltraGridBand BaseTableName="gds_att_info_faqs_v" Key="gds_att_info_faqs_v">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="faq_date" Key="faq_date" IsBound="false" Format="yyyy/MM/dd"
                                            Width="30%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadFaqDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="faq_title" Key="faq_title" IsBound="false"
                                            Width="30%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadFaqTitle %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                            HeaderClickAction="Select" Width="30px" Key="answer_flag" BaseColumnName="answer_flag">
                                            <CellTemplate>
                                                <asp:Image ID="imgActiveFlag" runat="server" />
                                            </CellTemplate>
                                            <Header Caption="<%$Resources:ControlText,gvHeadAnswerFlag %>" ClickAction="Select"
                                                Fixed="true">
                                            </Header>
                                        </igtbl:TemplatedColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="answer_name" Key="answer_name" IsBound="false">
                                            <Header Caption="<%$Resources:ControlText,gvHeadAnswerName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="FAQ_SEQ" Key="FAQ_SEQ" IsBound="false" Hidden="true">
                                            <Header Caption="">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="javascript">document.write("</DIV>");</script>

                    </td>
                    <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="19" background="../../CSS/Images_new/EMP_03.gif" height="18">
                        &nbsp;
                    </td>
                    <td background="../../CSS/Images_new/EMP_08.gif" height="18">
                        &nbsp;
                    </td>
                    <td width="19" background="../../CSS/Images_new/EMP_04.gif" height="18">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
