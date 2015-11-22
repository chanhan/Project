<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.Index" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script>
   function UltraWebGridInfoFormCellClickHandler(gridName)
    {
     //  var row = igtbl_getActiveRow(gridName);
   // 	var path=row.getCell(2).getValue();
    //	down(path);
    }
     function down(f) { 
     $("#frameDown").attr("src", "Index.aspx?path=" + encodeURI(f));
      }
            function OpenDetail(type, id) {
            var sFeature = "resizable:no;dialogHeight:700px; dialogWidth:790px; status:no;"
            if ($.browser.msie) {
                if ($.browser.version < 7) {
                    sFeature = "resizable:no;dialogHeight:733px; dialogWidth:796px; status:no;"
                }
            }
            var url = "";
            switch (type) {
                case "Notice": url = "HomePage/NoticeDetail.aspx?noticeId=" + id; break;
                case "Faq": url = "HomePage/FaqDetail.aspx?faqSeq=" + id; break;
                case "Paper": url = "HomePage/PaperDetail.aspx?PaperSeq=" + id; break;
            }
            window.showModalDialog(url+"&r="+Math.random(), id, sFeature);
        }
        $(function(){
                          $("#img_grid,#img_grid_div1").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid2,#img_grid2_div2").toggle(
                function(){
                    $("#tr_show2").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_show2").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
                          $("#img_grid3,#img_grid3_div3").toggle(
                function(){
                    $("#tr_show3").hide();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_show3").show();
                    $("#div_img_3").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            
                                      $("#img_grid4,#img_grid4_div4").toggle(
                function(){
                    $("#tr_show4").hide();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_show4").show();
                    $("#div_img_4").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
        })
    </script>

    <style>
        .hyLinkCss
        {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <iframe id="frameDown" style="display: none;"></iframe>
    <div>
        <asp:HiddenField ID="hidProblem" runat="server" Value="1" />
        <div style="width: 100%" id="PanelData">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 95%;" class="tr_title_center" id="img_grid">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLastNotice" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5%;">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="lnkMoreNotice" runat="server" NavigateUrl="~/HomePage/Notice.aspx"
                                        CssClass="hyLinkCss" Text="<%$Resources:ControlText,lnkMoreInfo %>"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;" id="img_grid_div1">
                        <div>
                            <img id="div_img_1" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/230+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridInfoNotice" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridInfoNotice_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoNotice" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="NotSet">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents DblClickHandler="UltraWebGridInfoNotice_DblClickHandler"></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_info_notices_v" Key="gds_att_info_notices_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="Notice_Date" Key="Notice_Date" IsBound="false"
                                                Format="yyyy/MM/dd" Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadNoticeDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Notice_Title" Key="Notice_Title" IsBound="false"
                                                Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadNoticeTitle %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Notice_Type_Name" Key="Notice_Type_Name" IsBound="false"
                                                Width="34%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadNoticeTypeName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="NOTICE_ID" Key="NOTICE_ID" IsBound="false"
                                                Hidden="true">
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
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 95%;" class="tr_title_center" id="img_grid2">
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
                    <td style="width: 5%;">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HomePage/Faq.aspx" Text="<%$Resources:ControlText,lnkMoreInfo %>"
                                        CssClass="hyLinkCss"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;" id="img_grid2_div2">
                        <div>
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show2">
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/230+"'>");</script>

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
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridProblem_InitializeLayoutHandler"
                                        DblClickHandler="UltraWebGridProblem_DblClickHandler" AfterSelectChangeHandler="AfterSelectChange">
                                    </ClientSideEvents>
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
                                                Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFaqDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="faq_title" Key="faq_title" IsBound="false"
                                                Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFaqTitle %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="answer_name" Key="answer_name" IsBound="false"
                                                Width="33%">
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
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 95%;" class="tr_title_center" id="img_grid3">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblFormDownLoad" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5%;">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/HomePage/Form.aspx"
                                        Text="<%$Resources:ControlText,lnkMoreInfo %>" CssClass="hyLinkCss"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;" id="img_grid3_div3">
                        <div>
                            <img id="div_img_3" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show3">
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/230+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridInfoForm" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridInfoForm_DataBound" OnItemCommand="UltraWebGridInfoForm_ItemCommand">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoForm" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="CellSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents   CellClickHandler="UltraWebGridInfoFormCellClickHandler"></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_info_forms_v" Key="gds_att_info_forms_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="Type_Name" Key="Type_Name" IsBound="false"
                                                Format="yyyy/MM/dd" Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadTypeName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Form_Name" Key="Form_Name" IsBound="false"
                                                Width="33%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormFormName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <%--<igtbl:TemplatedColumn AllowResize="Free" ChangeLinksColor="True" Width="15%" Key="Form_Path"
                                                BaseColumnName="Form_Path">
                                                
                                                <CellTemplate>
                                                    <asp:ImageButton ID="ImageButton1" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Form_Path") %>'
                                                        CommandName="DownFrom" runat="server" ImageUrl="~/CSS/Images_new/DownLoad2.gif" />
                                                </CellTemplate>
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormPath %>" Fixed="true">
                                                </Header>
                                            </igtbl:TemplatedColumn>--%>
                         <%--                   <igtbl:UltraGridColumn BaseColumnName="FORM_SEQ" Key="FORM_SEQ" IsBound="false">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormPath %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>--%>
                                               <igtbl:UltraGridColumn BaseColumnName="Form_Path" Key="Form_Path" IsBound="false"  Width="34%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormPath %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                        <Columns>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <%--  <igtbl:UltraWebGrid ID="UltraWebGridInfoForm" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridInfoForm_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoForm" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="NotSet">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    
                                    <ClientSideEvents DblClickHandler="UltraWebGridInfoForm_DblClickHandler"></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_info_forms_v" Key="gds_att_info_forms_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="Type_Name" Key="Type_Name" IsBound="false"
                                                Format="yyyy/MM/dd" Width="30%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadTypeName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Form_Name" Key="Form_Name" IsBound="false"
                                                Width="30%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Form_Path" Key="Form_Path" IsBound="false" >
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormPath %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>--%>

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
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 95%;" class="tr_title_center" id="img_grid4">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblServiceHotline" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5%;">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/HomePage/Service.aspx"
                                        Text="<%$Resources:ControlText,lnkMoreInfo %>" CssClass="hyLinkCss"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;" id="img_grid4_div4">
                        <div>
                            <img id="div_img_4" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show4">
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/230+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridServiceHotline" runat="server" Width="100%" Height="100%">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridServiceHotline" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="NotSet">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents DblClickHandler="UltraWebGridServiceHotline_DblClickHandler"></ClientSideEvents>
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_info_services" Key="gds_att_info_services">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="Service_Name" Key="Service_Name" IsBound="false"
                                                Width="50%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadServiceName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Service_Phone" Key="Service_Phone" IsBound="false"
                                                Width="50%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadServicePhone %>">
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
    </div>
    </form>
</body>
</html>
