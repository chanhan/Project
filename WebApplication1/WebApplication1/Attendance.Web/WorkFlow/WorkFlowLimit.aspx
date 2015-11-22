<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowLimit.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowLimit" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">

        //收縮模塊控制
        $(function() {
            $("#tr_edit").toggle
            (
                function() {
                    $("#div_select").hide();

                },
                function() {
                    $("#div_select").show();

                }
            )
            $(function() {
                $("#tr1").toggle
           (
            function() {
                $("#div_showdata").hide();
            },
            function() {
                $("#div_showdata").show();
            }
           )
            });
        });

        //        $(function() {
        //            $("#tr2").toggle
        //            (
        //                function() {
        //                    $("#div_edit").hide();

        //                },
        //                function() {
        //                    $("#div_edit").show();

        //                }
        //            )

        //        });
        $(function() {
            $("#tr3").toggle
           (
            function() {
                $("#div_down").hide();

            },
            function() {
                $("#div_down").show();

            }
           )
        });

        //查詢
        function Select_EMP() {
            if ($("input[id$='txtOrgCode']")[0].value == "") {
                alert(Message.DepCodeNotNull);
                return false;
            }
            return true;
        }
        //編輯查詢
        function Select() {


            if (!ddlwhere()) {
                return false;
            }

            if ($("input[id$='txtOrgCode_edit']")[0].value == "") {
                alert(Message.DepCodeNotNull);
                return false;
            }
            return true;
        }

        //刪除
        function Delete() {
            var selRow = igtbl_getActiveRow("UltraWebGridBill");
            if (selRow == null) {
                alert(Message.check_delete_data);
                return false;
            }
            else {
                selRow.setSelected(true);
                igtbl_setActiveRow("UltraWebGridBill", igtbl_getElementById("UltraWebGridBill_r_" + selRow.getIndex()));
            }
            return true;
        }

        //保存數據管控
        function Save() {
            var grid = igtbl_getGridById('UltraWebGridBill');
            var gRows = grid.Rows;
            var Count = 0;
            if (!ddlwhere()) {
                return false;
            }
            if ($("input[id$='txtOrgCode_edit']")[0].value == "") {
                alert(Message.DepCodeNotNull);
                return false;
            }
            else {
                return confirm(Message.SaveConfirm)
            }
        }

        function ddlwhere() {
            //tr_leave1.Visible = false;
            //tr_leave2.Visible = false;
            //tr_chucai.Visible = false;
            if ($("select[id$='ddl_doctype_1']")[0].value == "") {
                alert(Message.check_doctype);
                return false;
            }
            else if ($("tr[id$='tr_leave1']")[0] != undefined) {//ddl_leavedays ddl_shiwei ddl_manager ddl_leavetype
                if ($("select[id$='ddl_leavedays']")[0].value == "") {
                    alert(Message.check_leavedays);
                    return false;
                }
                else if ($("select[id$='ddl_shiwei']")[0].value == "") {
                    alert(Message.check_shiwei);
                    return false;
                }
                else if ($("select[id$='ddl_manager']")[0].value == "") {
                    alert(Message.check_manager);
                    return false;
                }
                else if ($("select[id$='ddl_leavetype']")[0].value == "") {
                    alert(Message.check_leavetype);
                    return false;
                }
            }
            else if ($("tr[id$='tr_overtimetype']")[0] != undefined) {
                if ($("select[id$='ddl_overtimetype']")[0].value == "") {
                    alert(Message.check_overtimetype);
                    return false;
                }
            }
            else if ($("tr[id$='tr_chucai']")[0] != undefined) {//ddl_chucai ddl_chucaidays
                if ($("select[id$='ddl_chucai']")[0].value == "") {
                    alert(Message.check_chucai);
                    return false;
                }
                else if ($("select[id$='ddl_chucaidays']")[0].value == "") {
                    alert(Message.check_chucaidays);
                    return false;
                }
            }
            return true;
        }

        //增加
        function Add() {
            var grid = igtbl_getGridById('UltraWebGridAudit');
            var gRows = grid.Rows;
            var Count = 0;
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    Count += 1;
                }
            }
            if (Count == 0) {
                alert(Message.check_limit_add);
                return false;
            }
            return true;
        }
        //調用放大鏡
        function setSelector(ctrlCode, ctrlName, moduleCode) {
            var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:no;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hf_deptid" runat="server" />
    <asp:HiddenField ID="hidemodelcode" runat="server" />
    <div style="float: left; width: 99%;">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr2">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                        <div id="Div2">
                            <img id="img1" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_edit" style="width: 100%;">
            <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
                <tr class="tr_data_1">
                    <td style="width: 10%;">
                        <asp:Label ID="lbl_Unit_edit" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="tb_unit_edit" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtOrgCode_edit" runat="server" Width="100%" Style="display: none;"></asp:TextBox>
                        <asp:Image ID="imgDepCode_edit" runat="server" Style="cursor: pointer;" class="img_hidden"
                            ImageUrl="../CSS/Images_new/search_new.gif" Height="20px" Width="20px"></asp:Image>
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="lbl_doctype_1" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:DropDownList ID="ddl_doctype_1" runat="server" Width="160px" OnSelectedIndexChanged="ddl_doctype_1_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 30%;">
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="Btn_select_edit" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                CommandName="Query" OnClientClick="return Select();" OnClick="Btn_select_edit_Click"
                                CssClass="button_1" />
                            <asp:Button ID="Btn_delete" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                CommandName="Query" OnClientClick="return Delete();" OnClick="Btn_delete_Click"
                                CssClass="button_1" />
                            <asp:Button ID="Btn_save" runat="server" Text="Query" ToolTip="Authority Code:Query"
                                CommandName="Query" OnClientClick="return Save();" OnClick="Btn_save_Click" CssClass="button_1" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr runat="server" id="tr_overtimetype" visible="false">
                    <td>
                        <asp:Label ID="lbl_overtimetype" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_overtimetype" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="tr_leave1" visible="false">
                    <td>
                        <asp:Label ID="lbl_leavedays" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_leavedays" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbl_shiwei" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_shiwei" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="tr_leave2" visible="false">
                    <td>
                        <asp:Label ID="lbl_manger" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_manager" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbl_leavetype" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_leavetype" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="tr_chucai" visible="false">
                    <td>
                        <asp:Label ID="lbl_chucai" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_chucai" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lbl_chucaidays" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_chucaidays" runat="server" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div style="width: 100%; height: 150px;">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td style="vertical-align: top;">

                            <script language="javascript">                                document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 25 / 100 + "'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%">
                                <DisplayLayout Name="UltraWebGridBill" CompactRendering="False" RowHeightDefault="20px"
                                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                    AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                    AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="CellSelect"
                                    StationaryMargins="HeaderAndFooter">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderColor="#6699ff" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
                                    <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
                                    </SelectedRowStyleDefault>
                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                    </RowAlternateStyleDefault>
                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                        CssClass="tr_data">
                                        <Padding Left="3px"></Padding>
                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                    </RowStyleDefault>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="GDS_WF_FLOWSET" Key="GDS_WF_FLOWSET">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_EMPNO" Key="FLOW_EMPNO" IsBound="false"
                                                Width="20%" AllowUpdate="No">
                                                <Header Caption="<%$Resources:ControlText,gvheadempno %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_EMPNAME" Key="FLOW_EMPNAME" IsBound="false"
                                                Width="20%" AllowUpdate="No">
                                                <Header Caption="<%$Resources:ControlText,gvheadsignname %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_NOTES" Key="FLOW_NOTES" IsBound="false"
                                                Width="35%" AllowUpdate="No">
                                                <Header Caption="<%$Resources:ControlText,Notes %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_MANAGER" Key="FLOW_MANAGER" IsBound="false"
                                                Width="20%" AllowUpdate="No">
                                                <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="JavaScript" type="text/javascript">                                document.write("</DIV>");</script>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr3">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_select_1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div3">
                            <img id="img3" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_down" style="">
            <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%">
                <tr class="tr_data_2">
                    <td style="width: 10%;">
                        <asp:Label ID="lbl_Unit" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_unit" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtOrgCode" runat="server" Style="display: none"></asp:TextBox>
                        <asp:Image ID="imgDepCode" runat="server" Style="cursor: pointer;" class="img_hidden"
                            ImageUrl="../CSS/Images_new/search_new.gif" Height="20px" Width="20px"></asp:Image>
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="lbl_empno" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_empno" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="width: 100%; text-align: left;">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Button ID="Btn_select" runat="server" Text="<%$Resources:ControlText,Btn_select %>"
                        OnClientClick="return Select_EMP();" OnClick="Btn_select_Click" CssClass="button_1" />
                    <asp:Button ID="Btn_Add" runat="server" Text="<%$Resources:ControlText,Btn_Add %>"
                        OnClientClick="return Add();" OnClick="Btn_Add_Click" CssClass="button_1" />
                </asp:Panel>
            </div>

            <script language="javascript">                document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>

            <igtbl:UltraWebGrid ID="UltraWebGridAudit" runat="server" Width="100%" Height="100%">
                <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                    Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                    AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                    AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect"
                    StationaryMargins="HeaderAndFooter">
                    <HeaderStyleDefault VerticalAlign="Middle" BorderColor="#6699ff" HorizontalAlign="Left"
                        CssClass="tr_header">
                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                        </BorderDetails>
                    </HeaderStyleDefault>
                    <FrameStyle Width="100%" Height="100%">
                    </FrameStyle>
                    <ClientSideEvents></ClientSideEvents>
                    <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
                    </SelectedRowStyleDefault>
                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                    </RowAlternateStyleDefault>
                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                        CssClass="tr_data">
                        <Padding Left="3px"></Padding>
                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                    </RowStyleDefault>
                </DisplayLayout>
                <Bands>
                    <igtbl:UltraGridBand BaseTableName="gds_att_employee" Key="gds_att_employee">
                        <Columns>
                            <igtbl:UltraGridColumn BaseColumnName="depname" Key="depname" IsBound="false" Width="15%">
                                <Header Caption="<%$Resources:ControlText,gvHeadDept%>">
                                </Header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="workno" Key="workno" IsBound="false" Width="15%">
                                <Header Caption="<%$Resources:ControlText,gvheadempno %>">
                                </Header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="localname" Key="localname" IsBound="false"
                                Width="15%">
                                <Header Caption="<%$Resources:ControlText,gvheadsignname %>">
                                </Header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="notes" Key="notes" IsBound="false" Width="30%">
                                <Header Caption="<%$Resources:ControlText,Notes %>">
                                </Header>
                            </igtbl:UltraGridColumn>
                            <igtbl:UltraGridColumn BaseColumnName="managername" Key="managername" IsBound="false"
                                Width="20%">
                                <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                </Header>
                            </igtbl:UltraGridColumn>
                        </Columns>
                    </igtbl:UltraGridBand>
                </Bands>
            </igtbl:UltraWebGrid>

            <script language="JavaScript" type="text/javascript">                document.write("</DIV>");</script>

        </div>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
