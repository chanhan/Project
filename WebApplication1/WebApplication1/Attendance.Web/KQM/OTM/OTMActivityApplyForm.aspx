﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMActivityApplyForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMActivityApplyForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>免卡人員加班導入</title>
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript">

        function CheckAll() {
            var sValue = false;
            var chk = document.getElementById("UltraWebGrid_ctl00_CheckBoxAll");
            if (chk.checked) {
                sValue = true;
            }
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;
            for (i = 0; i < gRows.length; i++) {
                if (!igtbl_getElementById("UltraWebGrid_ci_0_0_" + i + "_CheckBoxCell").disabled) {
                    igtbl_getElementById("UltraWebGrid_ci_0_0_" + i + "_CheckBoxCell").checked = sValue;
                }
            }
        }
        function UpProgress() {
            var filepath = $("#<%=FileUpload.ClientID %>").val();
            $("#<%=hidFilePath.ClientID %>").val(filepath);
            if (filepath != "") {
                if (filepath.indexOf('\\') >= 0) {
                    $("#<%=btnImportSave.ClientID %>").css("display", "none");
                    $("#<%=btnImport.ClientID %>").attr("disabled", "true");
                    $("#<%=btnExport.ClientID %>").attr("disabled", "true");
                    $("#imgWaiting").css("display", "");
                    $("#<%=lblupload.ClientID %>").text(Message.uploading);
                    return true;
                }
                else {
                    $("#<%=lblupload.ClientID %>").text(Message.WrongFilePath);
                    return false;
                }
            }
            else {
                $("#<%=lblupload.ClientID %>").text(Message.PathIsNull);
                return false;
            }
        }
        function AfterSelectChange(gridName, id) {
            var row = igtbl_getRowById(id);
            DisplayRowData(row);
            return 0;
        }
        function UltraWebGrid_InitializeLayoutHandler(gridName) {
            var row = igtbl_getActiveRow(gridName);
            DisplayRowData(row);
        }
        function DisplayRowData(row) {
            if (row != null) {
                igtbl_getElementById("HiddenWorkNo").value = row.getCell(1).getValue() == null ? "" : row.getCell(2).getValue();
                igtbl_getElementById("HidID").value = row.getCell(4).getValue() == null ? "" : row.getCell(4).getValue();
                igtbl_getElementById("HidStatus").value = row.getCell(5).getValue() == null ? "" : row.getCell(5).getValue();
            }
        }
        function DblClick(gridName, id) {
            var ProcessFlag = "Modify";
            OpenEdit(ProcessFlag)
            return 0;
        }
        function OpenEdit(ProcessFlag) {
            var EmployeeNo = $.trim($("#HiddenWorkNo").val());
            var ID = $.trim($("#HidID").val());
            var ModuleCode = $.trim($("#ModuleCode").val());
            $("#ProcessFlag").val(ProcessFlag);
            if (ProcessFlag == "Modify") {
                var grid = igtbl_getGridById('UltraWebGrid');
                var gRows = grid.Rows;
                var Count = 0;
                for (i = 0; i < gRows.length; i++) {
                    if (gRows.getRow(i).getSelected()) {
                        Count += 1;
                    }
                }
                if (Count == 0) {
                    alert(Message.AtLastOneChoose);
                    return false;
                }
                else
                {
                  var status = $.trim($("#HidStatus").val()); 
                  if (status !="0" &&status !="3")
                  {
                     alert(Message.UpdateStatusNotIs03);
                    return false;
                  }
                }
                
               
            }

            $("#iframeEdit").attr("src", "OTMActivityApplyEditForm.aspx?EmployeeNo=" + EmployeeNo + "&ID=" + ID + "&ProcessFlag=" + ProcessFlag + "&ModuleCode=" + ModuleCode + "&r=" + Math.random());
            $("#topTable").css("display", "none");
            $("#div_2").css("display", "none");
            $("#divEdit").css("display", "");
            return false;
        }
        function CheckAudit() {
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;
            var Count = 0;
            var State = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGrid_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                    State = gRows.getRow(i).getCell(5).getValue();
                    switch (State) {
                        case "0":
                        case "3":
                            break;
                        default:
                            alert(Message.AuditUnaudit);
                            return false;
                            break;
                    }
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            if (confirm(Message.DataReturn)) {
                // FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function CheckDelete() {
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;
            var Count = 0;
            var State = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGrid_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                    State = gRows.getRow(i).getCell(5).getValue();
                    switch (State) {
                        case "0":
                        case "3":
                            break;
                        default:
                            alert(Message.DeleteApplyovertimeEnd);
                            return false;
                            break;
                    }
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            if (confirm(Message.DataReturn)) {
                //FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function CheckCancelAudit() {
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;
            var Count = 0;
            var State = "";
            for (i = 0; i < gRows.length; i++) {
                if (igtbl_getElementById("UltraWebGrid_ci_0_0_" + i + "_CheckBoxCell").checked) {
                    Count += 1;
                    State = gRows.getRow(i).getCell(5).getValue();
                    switch (State) {
                        case "2":
                            break;
                        default:
                            alert(Message.AuditUncancelaudit);
                            return false;
                            break;
                    }
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            if (confirm(Message.DataReturn)) {
                // FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function CheckSendAudit() {
            return false;
        }
        function OpenAuditStatus()//查看簽核進度
        {
            return false;
        }

        function OrgAudit()//選擇組織送簽
        {

            return false;
        }
        function ShowBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display = "";
            document.all("PanelBatchWorkNo").style.top = document.all("txtEmployeeNo").style.top;
            document.getElementById("txtBatchEmployeeNo").style.display = "";
            document.getElementById("txtEmployeeNo").value = "";
            document.getElementById("txtBatchEmployeeNo").value = "";
            document.getElementById("txtBatchEmployeeNo").focus();
            return false;
        }
        function HiddenBatchWorkNo() {
            document.all("PanelBatchWorkNo").style.display = "none";
            document.getElementById("txtBatchEmployeeNo").style.display = "none";
            //document.getElementById("txtBatchEmployeeNo").value="";
            var empno = $.trim($("#<%=txtBatchEmployeeNo.ClientID%>").val());
            var i;
            var result = "";
            var c;
            for (i = 0; i < empno.length; i++) {
                c = empno.substr(i, 1);
                if (c == "\n")
                    result = result + "§";
                else if (c != "\r")
                    result = result + c;
            }
            //document.getElementById("txtEmployeeNo").value=result;
            document.getElementById("txtBatchEmployeeNo").value = result;

        }


        function BatchLoseFocus() {
            HiddenBatchWorkNo();
            return true;
        }
        function setSelector(ctrlCode, ctrlName, flag, moduleCode) {
            var code = $("#" + ctrlCode).val();
            if (flag == "dept") {
                var url = "../BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            }
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;

        }

        function CheckDate() {
            var check = /^\d{4}[\/]\d{2}[\/]\d{2}$/;
            var dateFrom = $("#<%=txtOTDateFrom.ClientID%>").val();
            var dateTo = $("#<%=txtOTDateTo.ClientID %>").val();
            if (dateFrom != null && dateFrom != "") {
                if (!check.test(dateFrom)) {
                    alert(Message.WrongDate);
                    $("#<%=txtOTDateFrom.ClientID%>").val("");
                    return false;
                }
            }
            if (dateTo != null && dateTo != "") {
                if (!check.test(dateTo)) {
                    alert(Message.WrongDate);
                    $("#<%=txtOTDateTo.ClientID%>").val();
                    return false;
                }
            }
            if ((dateFrom != null && dateFrom != "") && (dateTo != null && dateTo != "")) {
                if (dateTo < dateFrom) {
                    alert(Message.ToLaterThanFrom);
                    $("#<%=txtOTDateFrom.ClientID %>").val("");
                    return false;
                }
            }
            return true;
        }

        $(function() {
            $("#tr_edit").toggle(
            function() {
                $("#div_1").hide();
                $(".img1").attr("src", "../../CSS/Images_new/left_back_03.gif");

            },
            function() {
                $("#div_1").show();
                $(".img1").attr("src", "../../CSS/Images_new/left_back_03_a.gif");
            }
        )
        });

        $(function() {
            $("#td_show_1,#td_show_2,#div_img2").toggle(
            function() {
                $("#table_show").hide();
                $(".img2").attr("src", "../../CSS/Images_new/left_back_03.gif");
            },
            function() {
                $("#table_show").show();
                $(".img2").attr("src", "../../CSS/Images_new/left_back_03_a.gif");
            }
        )

        });

        $(function() {
            $("#tr_import").toggle(
            function() {
                $("#div_import").hide();
                $(".img3").attr("src", "../../CSS/Images_new/left_back_03.gif");
            },
            function() {
                $("#div_import").show();
                $(".img3").attr("src", "../../CSS/Images_new/left_back_03_a.gif");
            }
        )

        });

        function ButtonReset() {
            $("#<%=txtDepCode.ClientID %>").attr("value", "");
            $("#<%=txtDepName.ClientID %>").attr("value", "");
            $("#<%=txtEmployeeNo.ClientID %>").attr("value", "");
            $("#<%=txtName.ClientID %>").attr("value", "");
            $("#<%=txtBatchEmployeeNo.ClientID %>").attr("value", "");
            $("#ddlOTStatus").get(0).selectedIndex = 1;
            var myDate = new Date();
            var year = myDate.getFullYear();
            var month = myDate.getMonth() + 1 > 9 ? (myDate.getMonth() + 1) : "0" + (myDate.getMonth() + 1);
            var date = myDate.getDate() > 9 ? myDate.getDate() : "0" + myDate.getDate();
            var yesterday = myDate.getDate() - 1 > 9 ? (myDate.getDate() - 1) : "0" + (myDate.getDate() - 1);
            $("#<%=txtOTDateFrom.ClientID %>").val(year + "/" + month + "/" + yesterday);
            $("#<%=txtOTDateTo.ClientID %>").val(year + "/" + month + "/" + date);
            return false;
        }

        function GetSignMap() {
            var grid = igtbl_getGridById('UltraWebGrid');
            var gRows = grid.Rows;

            var Count = 0;
            var BillNo = "";
            for (i = 0; i < gRows.length; i++) {

                if (gRows.getRow(i).getSelected()) {

                    BillNo = gRows.getRow(i).getCellFromKey("BillNo").getValue() == null ? undefined : gRows.getRow(i).getCell(16).getValue();
                    
                    Count += 1;
                }
            }

            if (Count == 0) {
                alert(Message.common_message_data_select);
                return false;
            }

            // alert("BillNo:"+BillNo)
            if (BillNo == undefined || BillNo == null) {

                alert("<%=Resources.Message.wfm_nosign_message %>");

                return false;
            }
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
           
            var Revalue = window.showModalDialog("../../WorkFlow/SignLogAndMap.aspx?Doc=" +
	          BillNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HidID" type="hidden" name="HidID" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="HidStatus" type="hidden" name="HiddenOrgCode" runat="server">
    <asp:HiddenField ID="hidFilePath" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <div style="width: 100%;">
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                        <tr style="width: 100%;" id="tr_edit">
                            <td style="width: 100%;" class="tr_title_center">
                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
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
                                <div id="img_edit">
                                    <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div_1">
                    <table style="width: 100%" class="table_data_area">
                        <tr>
                            <td style="width: 100%">
                                <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area" style="table-layout: fixed">
                                    <tr class="tr_data_1">
                                        <td width="11%">
                                            &nbsp;
                                            <asp:Label ID="lblDep" runat="server"></asp:Label>
                                        </td>
                                        <td width="22%">
                                            <table cellspacing="0" cellpadding="0" width="98%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                            Style="display: none"></asp:TextBox>
                                                    </td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td style="cursor: hand">
                                                        <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                        </asp:Image>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblYearMonth" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <igtxt:WebDateTimeEdit ID="txtYearMonth" runat="server" EditModeFormat="yyyy/MM"
                                                Width="98%" CssClass="input_textBox">
                                            </igtxt:WebDateTimeEdit>
                                        </td>
                                        <td width="11%">
                                            &nbsp;
                                            <asp:Label ID="lblOTStatus" runat="server"></asp:Label>
                                        </td>
                                        <td width="23%">
                                            <asp:DropDownList ID="ddlOTStatus" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <%--<td style="width: 90px">
                                            &nbsp;
                                        </td>--%>
                                    </tr>
                                    <tr class="tr_data_2">
                                        <td width="11%">
                                            &nbsp;
                                            <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>
                                            <asp:ImageButton ID="ImageBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif"
                                                OnClientClick="return ShowBatchWorkNo();"></asp:ImageButton>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            <div id="PanelBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                border-bottom: #0000ff 1px solid; position: absolute; left: 11%; float: left;
                                                display: none;">
                                                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td width="100%" align="left" style="cursor: hand" onclick="HiddenBatchWorkNo()">
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
                                                <iframe src="JavaScript:false" style="position: absolute; visibility: hidden; top: 0px;
                                                    left: 0px; width: 225px; height: 100px; z-index: -1; filter: 'progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';">
                                                </iframe>
                                            </div>
                                        </td>
                                        <td class="td_label" width="11%">
                                            &nbsp;&nbsp;<asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="txtName" runat="server" Width="99%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="11%">
                                            &nbsp;
                                            <asp:Label ID="lbllOTDateFrom" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_input" width="23%">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td width="46%">
                                                        <asp:TextBox ID="txtOTDateFrom" runat="server" Width="99%" onchange="return CheckDate()"
                                                            class="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td width="8%">
                                                        ~
                                                    </td>
                                                    <td width="46%">
                                                        <asp:TextBox ID="txtOTDateTo" runat="server" Width="99%" onchange="return CheckDate()"
                                                            class="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlShowPanel" runat="server">
                                                <asp:Button ID="btnQuery" runat="server" class="button_1" OnClick="btnQuery_Click"
                                                    OnClientClick="return BatchLoseFocus()"></asp:Button>
                                                <asp:Button ID="btnReset" runat="server" class="button_1" OnClientClick="return ButtonReset()">
                                                </asp:Button>
                                                <asp:Button ID="btnAdd" runat="server" class="button_1" OnClientClick="return OpenEdit('Add')">
                                                </asp:Button>
                                                <asp:Button ID="btnModify" runat="server" class="button_1" OnClientClick="return OpenEdit('Modify')" >
                                                </asp:Button>
                                                <asp:Button ID="btnDelete" runat="server" class="button_1" OnClick="btnDelete_Click"
                                                    OnClientClick="return CheckDelete()"></asp:Button>
                                                <asp:Button ID="btnAudit" runat="server" class="button_1" OnClick="btnAudit_Click"
                                                    OnClientClick="return CheckAudit()"></asp:Button>
                                                <asp:Button ID="btnCancelAudit" runat="server" class="button_large" OnClick="btnCancelAudit_Click"
                                                    OnClientClick="return CheckCancelAudit()"></asp:Button>
                                                <asp:Button ID="btnImport" runat="server" class="button_1" OnClick="btnImport_Click">
                                                </asp:Button>
                                                <asp:Button ID="btnExport" runat="server" class="button_1" OnClick="btnExport_Click">
                                                </asp:Button>
                                                <asp:Button ID="btnSendAudit" runat="server" class="button_1" OnClick="btnSendAudit_Click">
                                                </asp:Button>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table cellspacing="0" cellpadding="0" width="100%" align="center">
                        <tr>
                            <td width="100%">
                                <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                        <tr style="width: 100%">
                                            <td style="width: 100%">
                                                <table cellspacing="0" cellpadding="0" class="table_title_area" width="100%">
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
                                                                    ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                                                    DisabledButtonImageNameExtension="g" ShowMoreButtons="false" PagingButtonSpacing="10px"
                                                                    ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                                                                </ess:AspNetPager>
                                                            </div>
                                                        </td>
                                                        <td style="width: 22px;" id="td_show_2">
                                                            <img id="div_img2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="table_show" style="width: 100%" cellspacing="0" cellpadding="0">
                                        <tr style="width: 100%">
                                            <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                                                <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19">
                                            </td>
                                            <td background="../../CSS/Images_new/EMP_07.gif" style="background-repeat: repeat-x"
                                                height="18">
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

                                                <script language="JavaScript" type="text/javascript">                                                    document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 60 / 100 + "'>");</script>

                                                <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGrid_DataBound">
                                                    <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                                        AllowSortingDefault="No" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
                                                        AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                                        <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                            </BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="100%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                            AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
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
                                                        <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                            DataKeyField="" BaseTableName="OTM_Activity" Key="OTM_Activity">
                                                            <Columns>
                                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                    HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                                    <CellTemplate>
                                                                        <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                    </CellTemplate>
                                                                    <HeaderTemplate>
                                                                        <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                    </HeaderTemplate>
                                                                    <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                                    </Header>
                                                                </igtbl:TemplatedColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="120">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityDepName %>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkNo %>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                    Width="60">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityLocalName %>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="0" Hidden="true">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityID %>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="0"
                                                                    Hidden="true">
                                                                    <Header Caption="Status" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80"
                                                                    Format="yyyy/MM/dd">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityOTDate %>" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="95">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityOTType %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ConfirmHours" Key="ConfirmHours" IsBound="false"
                                                                    Width="50" Format="0.0">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityConfirmHours %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                                    Width="70">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityStatusName %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkDesc %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                                    Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,lblStartTime %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,lblEndTime %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Update_User" Key="Update_User" IsBound="false"
                                                                    Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityUpdateUser %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Update_Date" Key="Update_Date" IsBound="false"
                                                                    Format="yyyy/MM/dd" Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadActivityUpdateDate %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DEPCODE" Key="DEPCODE" IsBound="false" Width="0"
                                                                    Hidden="true">
                                                                    <Header Caption="DEPCODE" Fixed="True">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Hidden="false"
                                                                    Width="100">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadBillNo %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ApproverName" Key="ApproverName" IsBound="false"
                                                                    Width="60">
                                                                    <Header Caption="<%$Resources:ControlText,gvheadsignname%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ApproveDate" Key="ApproveDate" IsBound="false"
                                                                    Width="110">
                                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApproveDate%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ApRemark" Key="ApRemark" IsBound="false" Width="120">
                                                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                    HeaderClickAction="Select" Width="100px">
                                                                    <CellTemplate>
                                                                        <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server"
                                                                            Text="<%$Resources:ControlText,BtnViewSchedule %>"></asp:LinkButton>
                                                                    </CellTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                                    </Header>
                                                                </igtbl:TemplatedColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                                <script language="JavaScript" type="text/javascript">                                                    document.write("</DIV>");</script>

                                            </td>
                                            <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="width: 100%">
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
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <asp:Panel class="inner_table" ID="PanelImport" runat="server" Width="100%" Visible="false">
                                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                        <tr style="cursor: hand" id="tr_import">
                                            <td style="width: 100%;" class="tr_title_center">
                                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                                    background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                                    font-size: 13px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblImportArea" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 22px;">
                                                <div id="Div1">
                                                    <img id="Img1" class="img3" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="div_import" style="width: 100%" cellspacing="0" cellpadding="0">
                                        <table style="width: 100%" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="td_label" width="100%" align="left">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td width="20%">
                                                                <a href="../../ExcelModel/OtmActivityApplySample.xls">&nbsp;<asp:Label ID="lblUploadText"
                                                                    runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </td>
                                                            <td width="40%">
                                                                <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                                            </td>
                                                            <td width="5%">
                                                                <asp:Button ID="btnImportSave" runat="server" class="button_1" OnClick="btnImportSave_Click"
                                                                    OnClientClick="return UpProgress();" />
                                                                <img id="imgWaiting" src="../../CSS/images/clocks.gif" border="0" style="display: none;
                                                                    height: 20px;" />
                                                            </td>
                                                            <td width="35%" align="left">
                                                                <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr width="100%">
                                                            <td width="100%" colspan="2">
                                                                <asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%" id="table_display" cellspacing="0" cellpadding="0" align="center"
                                            border="0">
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

                                                    <script language="javascript">                                                        document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 52 / 100 + "'>");</script>

                                                    <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                        <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                            AutoGenerateColumns="false" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                            AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridImport" TableLayout="Fixed"
                                                            CellClickActionDefault="RowSelect">
                                                            <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                                                CssClass="tr_header">
                                                                <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                                                </BorderDetails>
                                                            </HeaderStyleDefault>
                                                            <FrameStyle Width="100%" Height="100%">
                                                            </FrameStyle>
                                                            <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                                                AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
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
                                                            <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="280">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityErrorMsg %>">
                                                                        </Header>
                                                                        <CellStyle ForeColor="red">
                                                                        </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkNo %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityLocalName %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityOTDate %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityOTType %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConfirmHours" Key="ConfirmHours" IsBound="false"
                                                                        Width="60">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityConfirmHours %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="false" Width="200">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkDesc %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                                        Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,lblStartTime %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,lblEndTime %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>

                                                    <script language="JavaScript" type="text/javascript">                                                        document.write("</DIV>");</script>

                                                </td>
                                                <td width="19" background="../../CSS/Images_new/EMP_06.gif">
                                                    &nbsp;
                                                </td>
                                                <tr>
                                                </tr>
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

    <script language="javascript">        document.write("<div id='divEdit'style='display:none;height:" + document.body.clientHeight * 84 / 100 + "'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">        document.write("</div>");</script>

    </form>

    <script type="text/javascript"><!--
        document.getElementById("txtEmployeeNo").focus();
        document.getElementById("txtEmployeeNo").select();
	--></script>

</body>
</html>