<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFMBillCenterApproveForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WFMBillCenterApproveForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"><!--
   function openProgress(billNo)
    {
            var windowWidth = 600, windowHeight = 600;
            var X = (screen.availWidth - windowWidth) / 2;
            var Y = (screen.availHeight - windowHeight) / 2;
            window.showModalDialog("SignLogAndMap.aspx?Doc=" +billNo, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
    }
    
     function down(f) { $("#frameDown").attr("src", "WFMBillCenterApproveForm.aspx.aspx?fileName=" + encodeURI(f)); } 
function CheckAllLeaveApply()
{
  var sValue=false;
			var chk=document.getElementById("UltraWebGridLeaveApply_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridLeaveApply');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridLeaveApply_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
}
        function UltraWebGridKQL_DblClickHandler(gridName, cellId) {
            OpenDetailKQL()
            return 0;
        }
        function OpenDetailKQL()//請假明細
        {
            var grid = igtbl_getGridById('UltraWebGridKQL');
            var gRows = grid.Rows;
            var Count = 0;
            var WorkNo = "";
            var ID = "";
            for (i = 0; i < gRows.length; i++) {
                if (gRows.getRow(i).getSelected()) {
                    Count = 1;
                    WorkNo = gRows.getRow(i).getCellFromKey("WorkNo").getValue();
                    ID = gRows.getRow(i).getCellFromKey("ID").getValue();
                }
            }
            if (Count == 0) {
                alert(Message.AtLastOneChoose);
                return false;
            }
            var ModuleCode = "";
            var width = screen.availWidth * 0.9;
            var height = screen.availHeight * 0.8;
            openEditWin("WFMBillLeaveDetailForm.aspx?ModuleCode=" + ModuleCode + "&WorkNo=" + WorkNo + "&ID=" + ID, "LeaveDetail", width, height);
            return false;
        }
        function CheckAll()//專案加班G2
        {
            var sValue = false;
            var chk = document.getElementById("UltraWebGridOTA_ctl00_CheckBoxAll");
            if (chk.checked) {
                sValue = true;
            }
            var grid = igtbl_getGridById('UltraWebGridOTA');
            var gRows = grid.Rows;
            for (i = 0; i < gRows.rows.length; i++) {
                if (!igtbl_getElementById("UltraWebGridOTA_ci_0_14_" + i + "_CheckBoxCellISPay").disabled) {
                    igtbl_getElementById("UltraWebGridOTA_ci_0_14_" + i + "_CheckBoxCellISPay").checked = sValue;
                }
            }
        }
        function CheckOTA(cindex, UltraWebGrid)//拒簽判斷
        {
            var grid = igtbl_getGridById(UltraWebGrid);
            var rows = grid.Rows;
            var crow = rows.getRow(cindex);
            var chk = document.getElementById(UltraWebGrid + "_ci_0_0_" + cindex + "_CheckBoxCell");
            if (chk.checked) {
                if (!confirm(Message.yousurecannotthisdate+"？(" + crow.getCellFromKey("LocalName").getValue() + ")")) {
                    igtbl_getElementById(UltraWebGrid + "_ci_0_0_" + cindex + "_CheckBoxCell").checked = false;
                }
            }
        }
        function CheckDisApprove(UltraWebGrid)//拒簽判斷
        {
            if (UltraWebGrid != "N") {
                var grid = igtbl_getGridById(UltraWebGrid);
                var gRows = grid.Rows;
                var Count = 0;
                for (i = 0; i < gRows.length; i++) {
                    if (igtbl_getElementById(UltraWebGrid + "_ci_0_0_" + i + "_CheckBoxCell").checked) {
                        Count += 1;
                    }
                }
                if (Count > 0) {
                    alert(Message.youcanclickagreenotsignsave);
                    return false;
                }
            }
            var ApRemark = igtbl_getElementById("textBoxApRemark").value;
            if (ApRemark.trim().length == 0) {
                alert(Message.wfm_message_data_disapprove);
                igtbl_getElementById("textBoxApRemark").focus();
                return false;
            }
            else {
                if (confirm(Message.wfm_message_disaudit)) {
                    //FormSubmit("<%=sAppPath%>");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function CheckApprove() {
            if (confirm(Message.ConfirmBatchConfirm)) {
                // FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function OpenAdvance()//報表
        {
            var ModuleCode = "";
            var OrgCode = igtbl_getElementById("HiddenOrgCode").value;
            var width = 680;
            var height = 550;
            openEditWin("WFMAdvanceWeekForm.aspx?ModuleCode=" + ModuleCode + "&OrgCode=" + OrgCode, "AdvanceWeek", width, height);
        }
        function CheckClose()//關閉
        {
            window.close();
            return false;
        }
        function ShowCountersigning()//打開會簽
        {
            document.getElementById("Countersigning").style.display = "";
            return false;
        }
        function CloseCountersigning()//關閉會簽
        {
            document.getElementById("Countersigning").style.display = "none";
            return false;
        }
        function CheckSave() {
            var grid = igtbl_getGridById('UltraWebGridAudit');
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
            if (confirm(Message.ConfirmBatchConfirm)) {
                // FormSubmit("<%=sAppPath%>");
                return true;
            }
            else {
                return false;
            }
        }
        function GetEmp() {
            var LocalName = document.getElementById("textBoxSignLocalName").value;
            var BillNo = document.getElementById("HiddenBillNo").value;
            if (LocalName.length > 1 && IsChinese(LocalName)) {
                WFM_WFMBillCenterApproveForm.GetEmp(LocalName, BillNo, docallback)
            }
        }
        function docallback(response) {
            var grid = igtbl_getGridById('UltraWebGridAudit');
            var gRows = grid.Rows;
            var rlength = gRows.length;
            var newrow;
            //alert(gRows.length)
            while (rlength > 0) {
                gRows.remove(0);
                rlength = gRows.length;
            }
            if (response.value != null) {
                var ds = response.value;
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        newrow = gRows.addNew();
                        newrow.getCellFromKey("DepName").setValue(ds.Tables[0].Rows[i].DEPNAME);
                        newrow.getCellFromKey("AuditMan").setValue(ds.Tables[0].Rows[i].AUDITMAN);
                        newrow.getCellFromKey("LocalName").setValue(ds.Tables[0].Rows[i].LOCALNAME);
                        newrow.getCellFromKey("Notes").setValue(ds.Tables[0].Rows[i].NOTES);
                        newrow.getCellFromKey("ManagerName").setValue(ds.Tables[0].Rows[i].MANAGERNAME);
                    }
                }
            }

        }
        function IsChinese(str)//是否含有中文字符
        {
            if (escape(str).indexOf("%u") != -1) {
                return true;
            }
            return false;
        }
        function CommendView() {
            var RequestNo = document.getElementById("HiddenBillNo").value;
            var ModuleCode = "";
            openPrintWin("../CEM/OVERSEA/CEMEvectionCommendPrintForm.aspx?RequestNo=" + RequestNo + "&ModuleCode=" + ModuleCode, "Print");
        }
        function CountCost() {
            var Me = igedit_getById("textBoxForeseeMe").getValue() == null ? 0 : igedit_getById("textBoxForeseeMe").getValue();
            var Abode = igedit_getById("textBoxForeseeAbode").getValue() == null ? 0 : igedit_getById("textBoxForeseeAbode").getValue();
            var Meal = igedit_getById("textBoxForeseeMeal").getValue() == null ? 0 : igedit_getById("textBoxForeseeMeal").getValue();
            var Arm = igedit_getById("textBoxForeseeArm").getValue() == null ? 0 : igedit_getById("textBoxForeseeArm").getValue();
            var Sm = igedit_getById("textBoxForeseeSm").getValue() == null ? 0 : igedit_getById("textBoxForeseeSm").getValue();
            var Ap = igedit_getById("textBoxForeseeAp").getValue() == null ? 0 : igedit_getById("textBoxForeseeAp").getValue();
            var Isa = igedit_getById("textBoxForeseeIsa").getValue() == null ? 0 : igedit_getById("textBoxForeseeIsa").getValue();
            var Cfc = igedit_getById("textBoxForeseeCfc").getValue() == null ? 0 : igedit_getById("textBoxForeseeCfc").getValue();
            var Other = igedit_getById("textBoxForeseeOther").getValue() == null ? 0 : igedit_getById("textBoxForeseeOther").getValue();

            var CY = igedit_getById("WebNumericEditCY").getValue() == null ? 0 : igedit_getById("WebNumericEditCY").getValue();
            var MeCY = document.getElementById("DropDownListMeCY").value;
            var AbodeCY = document.getElementById("DropDownListAbodeCY").value;
            var MealCY = document.getElementById("DropDownListMealCY").value;
            var ArmCY = document.getElementById("DropDownListArmCY").value;
            var SmCY = document.getElementById("DropDownListSmCY").value;
            var ApCY = document.getElementById("DropDownListApCY").value;
            var IsaCY = document.getElementById("DropDownListIsaCY").value;
            var CfcCY = document.getElementById("DropDownListCfcCY").value;
            var OtherCY = document.getElementById("DropDownListOtherCY").value;


            var rAll = 0, uAll = 0;
            if (MeCY == "RMB") {
                rAll = rAll + parseInt(Me);
            }
            else {
                uAll = uAll + parseInt(Me);
            }
            if (AbodeCY == "RMB") {
                rAll = rAll + parseInt(Abode);
            }
            else {
                uAll = uAll + parseInt(Abode);
            }
            if (MealCY == "RMB") {
                rAll = rAll + parseInt(Meal);
            }
            else {
                uAll = uAll + parseInt(Meal);
            }
            if (ArmCY == "RMB") {
                rAll = rAll + parseInt(Arm);
            }
            else {
                uAll = uAll + parseInt(Arm);
            }
            if (SmCY == "RMB") {
                rAll = rAll + parseInt(Sm);
            }
            else {
                uAll = uAll + parseInt(Sm);
            }
            if (ApCY == "RMB") {
                rAll = rAll + parseInt(Ap);
            }
            else {
                uAll = uAll + parseInt(Ap);
            }
            if (IsaCY == "RMB") {
                rAll = rAll + parseInt(Isa);
            }
            else {
                uAll = uAll + parseInt(Isa);
            }
            if (CfcCY == "RMB") {
                rAll = rAll + parseInt(Cfc);
            }
            else {
                uAll = uAll + parseInt(Cfc);
            }
            if (OtherCY == "RMB") {
                rAll = rAll + parseInt(Other);
            }
            else {
                uAll = uAll + parseInt(Other);
            }
            igedit_getById("textBoxForeseeAll").setValue(rAll);
            igedit_getById("textBoxForeseeAllU").setValue(uAll);

        }
        function ERCCountCost() {
            var CY = document.getElementById("textBoxERCEXCHANGERATE").value;

            var Abode = document.getElementById("textBoxERCForeseeAbode").value == "" ? 0 : document.getElementById("textBoxERCForeseeAbode").value;
            var Meal = document.getElementById("textBoxERCForeseeMeal").value == "" ? 0 : document.getElementById("textBoxERCForeseeMeal").value;
            var Arm = document.getElementById("textBoxERCForeseeArm").value == "" ? 0 : document.getElementById("textBoxERCForeseeArm").value;
            var Sm = document.getElementById("textBoxERCForeseeSm").value == "" ? 0 : document.getElementById("textBoxERCForeseeSm").value;
            var Ap = document.getElementById("textBoxERCForeseeAp").value == "" ? 0 : document.getElementById("textBoxERCForeseeAp").value;
            var Isa = document.getElementById("textBoxERCForeseeIsa").value == "" ? 0 : document.getElementById("textBoxERCForeseeIsa").value;
            var Cfc = document.getElementById("textBoxERCForeseeCfc").value == "" ? 0 : document.getElementById("textBoxERCForeseeCfc").value;
            var Me = document.getElementById("textBoxERCForeseeMe").value == "" ? 0 : document.getElementById("textBoxERCForeseeMe").value;
            var Other = document.getElementById("textBoxERCForeseeOther").value == "" ? 0 : document.getElementById("textBoxERCForeseeOther").value;



            var Abode2 = document.getElementById("textBoxERCActualAbode").value == "" ? 0 : document.getElementById("textBoxERCActualAbode").value;
            var Meal2 = document.getElementById("textBoxERCActualMeal").value == "" ? 0 : document.getElementById("textBoxERCActualMeal").value;
            var Arm2 = document.getElementById("textBoxERCActualArm").value == "" ? 0 : document.getElementById("textBoxERCActualArm").value;
            var Sm2 = document.getElementById("textBoxERCActualSm").value == "" ? 0 : document.getElementById("textBoxERCActualSm").value;
            var Ap2 = document.getElementById("textBoxERCActualAp").value == "" ? 0 : document.getElementById("textBoxERCActualAp").value;
            var Isa2 = document.getElementById("textBoxERCActualIsa").value == "" ? 0 : document.getElementById("textBoxERCActualIsa").value;
            var Cfc2 = document.getElementById("textBoxERCActualCfc").value == "" ? 0 : document.getElementById("textBoxERCActualCfc").value;
            var Me2 = document.getElementById("textBoxERCActualMe").value == "" ? 0 : document.getElementById("textBoxERCActualMe").value;
            var Other2 = document.getElementById("textBoxERCActualOther").value == "" ? 0 : document.getElementById("textBoxERCActualOther").value;




            var diffAbode = Abode2 - Abode;
            var diffMeal = Meal2 - Meal;
            var diffArm = Arm2 - Arm;
            var diffSm = Sm2 - Sm;
            var diffAp = Ap2 - Ap;
            var diffIsa = Isa2 - Isa;
            var diffCfc = Cfc2 - Cfc;
            var diffMe = Me2 - Me;
            var diffOther = Other2 - Other;
            document.getElementById("textBoxERCdiffAbode").value = diffAbode;
            document.getElementById("textBoxERCdiffMeal").value = diffMeal;
            document.getElementById("textBoxERCdiffArm").value = diffArm;
            document.getElementById("textBoxERCdiffSm").value = diffSm;
            document.getElementById("textBoxERCdiffAp").value = diffAp;
            document.getElementById("textBoxERCdiffIsa").value = diffIsa;
            document.getElementById("textBoxERCdiffMe").value = diffMe;
            document.getElementById("textBoxERCdiffCfc").value = diffCfc;
            document.getElementById("textBoxERCdiffOther").value = diffOther;

            var Abode3 = document.getElementById("textBoxERCdiffAbode").value == "" ? 0 : document.getElementById("textBoxERCdiffAbode").value;
            var Meal3 = document.getElementById("textBoxERCdiffMeal").value == "" ? 0 : document.getElementById("textBoxERCdiffMeal").value;
            var Arm3 = document.getElementById("textBoxERCdiffArm").value == "" ? 0 : document.getElementById("textBoxERCdiffArm").value;
            var Sm3 = document.getElementById("textBoxERCdiffSm").value == "" ? 0 : document.getElementById("textBoxERCdiffSm").value;
            var Ap3 = document.getElementById("textBoxERCdiffAp").value == "" ? 0 : document.getElementById("textBoxERCdiffAp").value;
            var Isa3 = document.getElementById("textBoxERCdiffIsa").value == "" ? 0 : document.getElementById("textBoxERCdiffIsa").value;
            var Cfc3 = document.getElementById("textBoxERCdiffCfc").value == "" ? 0 : document.getElementById("textBoxERCdiffCfc").value;
            var Me3 = document.getElementById("textBoxERCdiffMe").value == "" ? 0 : document.getElementById("textBoxERCdiffMe").value;
            var Other3 = document.getElementById("textBoxERCdiffOther").value == "" ? 0 : document.getElementById("textBoxERCdiffOther").value;

            var MeCY = document.getElementById("textBoxERCForeseeMe2").value;
            var AbodeCY = document.getElementById("textBoxERCForeseeAbode2").value;
            var MealCY = document.getElementById("textBoxERCForeseeMeal2").value;
            var ArmCY = document.getElementById("textBoxERCForeseeArm2").value;
            var SmCY = document.getElementById("textBoxERCForeseeSm2").value;
            var ApCY = document.getElementById("textBoxERCForeseeAp2").value;
            var IsaCY = document.getElementById("textBoxERCForeseeIsa2").value;
            var CfcCY = document.getElementById("textBoxERCForeseeCfc2").value;
            var OtherCY = document.getElementById("textBoxERCForeseeOther2").value;



            var rAll = 0, uAll = 0;
            if (MeCY == "RMB") {
                rAll = rAll + parseInt(Me);
            }
            else {
                uAll = uAll + parseInt(Me);
            }
            if (AbodeCY == "RMB") {
                rAll = rAll + parseInt(Abode);
            }
            else {
                uAll = uAll + parseInt(Abode);
            }
            if (MealCY == "RMB") {
                rAll = rAll + parseInt(Meal);
            }
            else {
                uAll = uAll + parseInt(Meal);
            }
            if (ArmCY == "RMB") {
                rAll = rAll + parseInt(Arm);
            }
            else {
                uAll = uAll + parseInt(Arm);
            }
            if (SmCY == "RMB") {
                rAll = rAll + parseInt(Sm);
            }
            else {
                uAll = uAll + parseInt(Sm);
            }
            if (ApCY == "RMB") {
                rAll = rAll + parseInt(Ap);
            }
            else {
                uAll = uAll + parseInt(Ap);
            }
            if (IsaCY == "RMB") {
                rAll = rAll + parseInt(Isa);
            }
            else {
                uAll = uAll + parseInt(Isa);
            }
            if (CfcCY == "RMB") {
                rAll = rAll + parseInt(Cfc);
            }
            else {
                uAll = uAll + parseInt(Cfc);
            }
            if (OtherCY == "RMB") {
                rAll = rAll + parseInt(Other);
            }
            else {
                uAll = uAll + parseInt(Other);
            }
            document.getElementById("textBoxERCForeseeAll").value = rAll;
            document.getElementById("textBoxERCForeseeAllU").value = uAll;


            MeCY = document.getElementById("textBoxERCActualMe2").value;
            AbodeCY = document.getElementById("textBoxERCActualAbode2").value;
            MealCY = document.getElementById("textBoxERCActualMeal2").value;
            ArmCY = document.getElementById("textBoxERCActualArm2").value;
            SmCY = document.getElementById("textBoxERCActualSm2").value;
            ApCY = document.getElementById("textBoxERCActualAp2").value;
            IsaCY = document.getElementById("textBoxERCActualIsa2").value;
            CfcCY = document.getElementById("textBoxERCActualCfc2").value;
            OtherCY = document.getElementById("textBoxERCActualOther2").value;


            var rAll2 = 0, uAll2 = 0;
            if (MeCY == "RMB") {
                rAll2 = rAll2 + parseInt(Me2);
            }
            else {
                uAll2 = uAll2 + parseInt(Me2);
            }
            if (AbodeCY == "RMB") {
                rAll2 = rAll2 + parseInt(Abode2);
            }
            else {
                uAll2 = uAll2 + parseInt(Abode2);
            }
            if (MealCY == "RMB") {
                rAll2 = rAll2 + parseInt(Meal2);
            }
            else {
                uAll2 = uAll2 + parseInt(Meal2);
            }
            if (ArmCY == "RMB") {
                rAll2 = rAll2 + parseInt(Arm2);
            }
            else {
                uAll2 = uAll2 + parseInt(Arm2);
            }
            if (SmCY == "RMB") {
                rAll2 = rAll2 + parseInt(Sm2);
            }
            else {
                uAll2 = uAll2 + parseInt(Sm2);
            }
            if (ApCY == "RMB") {
                rAll2 = rAll2 + parseInt(Ap2);
            }
            else {
                uAll2 = uAll2 + parseInt(Ap2);
            }
            if (IsaCY == "RMB") {
                rAll2 = rAll2 + parseInt(Isa2);
            }
            else {
                uAll2 = uAll2 + parseInt(Isa2);
            }
            if (CfcCY == "RMB") {
                rAll2 = rAll2 + parseInt(Cfc2);
            }
            else {
                uAll2 = uAll2 + parseInt(Cfc2);
            }
            if (OtherCY == "RMB") {
                rAll2 = rAll2 + parseInt(Other2);
            }
            else {
                uAll2 = uAll2 + parseInt(Other2);
            }
            document.getElementById("textBoxERCActualAll").value = rAll2;
            document.getElementById("textBoxERCActualAllU").value = uAll2;

            MeCY = document.getElementById("textBoxERCdiffMe2").value;
            AbodeCY = document.getElementById("textBoxERCdiffAbode2").value;
            MealCY = document.getElementById("textBoxERCdiffMeal2").value;
            ArmCY = document.getElementById("textBoxERCdiffArm2").value;
            SmCY = document.getElementById("textBoxERCdiffSm2").value;
            ApCY = document.getElementById("textBoxERCdiffAp2").value;
            IsaCY = document.getElementById("textBoxERCdiffIsa2").value;
            CfcCY = document.getElementById("textBoxERCdiffCfc2").value;
            OtherCY = document.getElementById("textBoxERCdiffOther2").value;


            var rAll3 = 0, uAll3 = 0;
            if (MeCY == "RMB") {
                rAll3 = rAll3 + parseInt(Me3);
            }
            else {
                uAll3 = uAll3 + parseInt(Me3);
            }
            if (AbodeCY == "RMB") {
                rAll3 = rAll3 + parseInt(Abode3);
            }
            else {
                uAll3 = uAll3 + parseInt(Abode3);
            }
            if (MealCY == "RMB") {
                rAll3 = rAll3 + parseInt(Meal3);
            }
            else {
                uAll3 = uAll3 + parseInt(Meal3);
            }
            if (ArmCY == "RMB") {
                rAll3 = rAll3 + parseInt(Arm3);
            }
            else {
                uAll3 = uAll3 + parseInt(Arm3);
            }
            if (SmCY == "RMB") {
                rAll3 = rAll3 + parseInt(Sm3);
            }
            else {
                uAll3 = uAll3 + parseInt(Sm3);
            }
            if (ApCY == "RMB") {
                rAll3 = rAll3 + parseInt(Ap3);
            }
            else {
                uAll3 = uAll3 + parseInt(Ap3);
            }
            if (IsaCY == "RMB") {
                rAll3 = rAll3 + parseInt(Isa3);
            }
            else {
                uAll3 = uAll3 + parseInt(Isa3);
            }
            if (CfcCY == "RMB") {
                rAll3 = rAll3 + parseInt(Cfc3);
            }
            else {
                uAll3 = uAll3 + parseInt(Cfc3);
            }
            if (OtherCY == "RMB") {
                rAll3 = rAll3 + parseInt(Other3);
            }
            else {
                uAll3 = uAll3 + parseInt(Other3);
            }
            document.getElementById("textBoxERCdiffAll").value = rAll3;
            document.getElementById("textBoxERCdiffAllU").value = uAll3;



        }


    
    
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server" />
    <input id="HiddenBillTypeNo" type="hidden" name="HiddenBillTypeNo" runat="server" />
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server" />
    <input id="HiddenEUEmpNo" type="hidden" name="HiddenEUEmpNo" runat="server" />
    <input id="HiddenFlag" type="hidden" name="HiddenFlag" runat="server" />
    <input id="HiddenisProject" type="hidden" name="HiddenisProject" runat="server" />
    <input id="HiddenSFlag" type="hidden" name="HiddenSFlag" runat="server" />
    <input id="Hiddentrid" type="hidden" name="HiddenSFlag" runat="server" />
    <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server" />
        <iframe id="frameDown" style="display: none;"></iframe>
    <table cellspacing="1" id="topTable" cellpadding="0" width="100%" align="center"
        class="top_table">
        <tr id="BillTitle" runat="server" visible="true">
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand">
                        <td class="tr_table_title">
                            &nbsp; <big>[<asp:Label ID="labelBillTypeName" runat="server" Font-Bold="true" ForeColor="Blue">BillTypeName:</asp:Label>]</big>
                        </td>
                        <td class="tr_table_title" align="right">
                            <img id="div_img_1" src="../CSS/images/uparrows_white.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_1">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="td_label" width="11%">
                                            &nbsp;
                                            <asp:Label ID="lbllblBillNo" runat="server">BillNo:</asp:Label>
                                        </td>
                                        <td class="td_input" width="23%">
                                            <asp:TextBox ID="textBoxBillNo" Enabled="false" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelApplyMan" runat="server">ApplyMan:</asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxApplyMan" Enabled="false" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                        <td class="td_label" width="11%">
                                            &nbsp;
                                            <asp:Label ID="labelApplyDate" runat="server">ApplyDate:</asp:Label>
                                        </td>
                                        <td class="td_input" width="22%">
                                            <asp:TextBox ID="textBoxApplyDate" Enabled="false" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label">
                                            &nbsp;
                                            <asp:Label ID="labelOrgName" runat="server">OrgName:</asp:Label>
                                        </td>
                                        <td class="td_input" colspan="5">
                                            <asp:TextBox ID="textBoxOrgName" Enabled="false" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_label" colspan="6" height="25">
                                            &nbsp;
                                            <asp:Label ID="labelMessage" runat="server"></asp:Label>
                                            <a id="LinkAdvance" visible="false" runat="server" href="#" onclick="OpenAdvance()">
                                                點擊查看本周加班預報匯總 </a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server">
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand">
                        <div style="width: 100%;">
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr1">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lan_mx" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <div id="Div1">
                                            <img id="img2" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <script language="javascript">                                document.write("<DIV id='div_2'  style='height:" + document.body.clientHeight * 53 / 100 + "'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridOTA" runat="server" Width="100%" Height="100%"
                                Visible="false" OnDataBound="UltraWebGridOTA_DataBound">
                                <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridOTA" CompactRendering="False"
                                    RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                    AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="Edit" StationaryMargins="HeaderAndFooter"
                                    AllowUpdateDefault="No">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
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
                                    <igtbl:UltraGridBand BaseTableName="OTM_AdvanceApply" Key="OTM_AdvanceApply">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="50px" Key="CheckBoxAll">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <Header Caption="<%$Resources:ControlText,lan_juqian %>" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="true"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="true" Width="65">
                                                <Header Caption="<%$Resources:ControlText,lblWorkNo %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="true"
                                                Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LevelName" Key="LevelName" IsBound="true"
                                                Width="40">
                                                <Header Caption="<%$Resources:ControlText,gvLevel %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PersonType" Key="PersonType" IsBound="true"
                                                Width="60">
                                                <Header Caption="<%$Resources:ControlText,lan_persontype %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="true" Width="180">
                                                <Header Caption="<%$Resources:ControlText,lblSYB %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTDate" Key="OTDate" IsBound="true" Width="80">
                                                <Header Caption="<%$Resources:ControlText,lbllOTDateFrom %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" Key="OverTimeType" IsBound="true"
                                                Width="40">
                                                <Header Caption="<%$Resources:ControlText,lblPersonType %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Week" Key="Week" IsBound="true" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvHeadWeek %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTType" Key="OTType" IsBound="true" Width="100">
                                                <Header Caption="<%$Resources:ControlText,lbl_overtimetype %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BeginTime" Key="BeginTime" IsBound="true"
                                                Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceBeginTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="true" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceEndTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Hours" Key="Hours" IsBound="true" Width="40"
                                                Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceHours %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="90px" Key="CheckBoxISPay">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCellISPay" runat="server" />
                                                </CellTemplate>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <HeaderTemplate>
                                                    <span id="CheckBoxISPayLabel">是否發薪 </span>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBoxISPay" ClickAction="Select">
                                                </Header>
                                                <CellStyle BackColor="LightSteelBlue">
                                                </CellStyle>
                                                <HeaderStyle ForeColor="Red" />
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TodayTotal" Key="TodayTotal" IsBound="true"
                                                Width="60" Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,lan_todaytall %>">
                                                </Header>
                                                <CellStyle BackColor="skyBlue" ForeColor="blueviolet">
                                                </CellStyle>
                                                <HeaderStyle ForeColor="black" />
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1Total" Key="G1Total" IsBound="true" Width="60"
                                                Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,G1todaytall %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Total" Key="G2Total" IsBound="true" Width="60"
                                                Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,G2todaytall %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3Total" Key="G3Total" IsBound="true" Width="60"
                                                Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,G3todaytall %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="True"
                                                Key="MAdjust1" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvMAdjust1 %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WeekTotal" Key="WeekTotal" IsBound="true"
                                                Width="50" Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,zhouhj %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WeekWorkDays" Key="WeekWorkDays" IsBound="true"
                                                Width="70" Format="0.0">
                                                <Header Caption="<%$Resources:ControlText,weekworkday %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkDesc" Key="WorkDesc" IsBound="true" Width="200">
                                                <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkDesc %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="true" Width="350">
                                                <Header Caption="<%$Resources:ControlText,labbeiz %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTMSGFlag" Key="OTMSGFlag" IsBound="true"
                                                Width="0" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ModifyName" Key="ModifyName" IsBound="true"
                                                Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvgvModifier %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="true"
                                                Width="110">
                                                <Header Caption="<%$Resources:ControlText,gvgvModifyDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PlanAdjust" Key="PlanAdjust" IsBound="true"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,PlanAdjust %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="true" Width="0" Hidden="true">
                                                <Header Caption="ID">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ISPay" Key="ISPay" IsBound="true" Width="0"
                                                Hidden="true">
                                                <Header Caption="ISPay">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridKQT" runat="server" Width="100%" Height="100%"
                                Visible="false">
                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridEvectionApply" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridEvectionApply_InitializeLayoutHandler"
                                        AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="UltraWebGridEvectionApply_DblClickHandler">
                                    </ClientSideEvents>
                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp">
                                    </SelectedRowStyleDefault>
                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                    </RowAlternateStyleDefault>
                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                        CssClass="tr_data">
                                        <Padding Left="3px"></Padding>
                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                    </RowStyleDefault>
                                    <ActivationObject BorderColor="" BorderWidth="">
                                    </ActivationObject>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="gds_att_applyout" Key="gds_att_applyout">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll" HeaderText="CheckBox">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="false"
                                                Width="130px" HeaderText="ID">
                                                <Header Caption="<%$Resources:ControlText,gvHeadOrderID %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <%--<igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="100px"
                                                HeaderText="BillNo">
                                                <Header Caption="<%$Resources:ControlText,gvBillNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>--%>
                                            <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80px"
                                                HeaderText="WORKNO">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                Width="80px" HeaderText="LocalName">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="dcode" Key="dcode" IsBound="false" Hidden="True"
                                                HeaderText="dcode">
                                                <Header Caption="DCode">
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Hidden="True"
                                                Width="100px" HeaderText="buName">
                                                <Header Caption="<%$Resources:ControlText,gvBUName %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="60px"
                                                HeaderText="DEPNAME">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionTypeName" Key="EvectionTypeName" IsBound="false"
                                                Width="60px" HeaderText="EvectionTypeName">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionTypeName%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionType" Key="EvectionType" Hidden="true"
                                                IsBound="false" HeaderText="EvectionType">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionTypeName%>">
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionReason" Key="EvectionReason" IsBound="false"
                                                Width="100px" HeaderText="EvectionReason">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionReason%>">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionTime" Key="EvectionTime" IsBound="false"
                                                Width="130px" HeaderText="EvectionTime" Format="yyyy/MM/dd HH:mm">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionTime %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionTel" Key="EvectionTel" IsBound="false"
                                                Width="80px" HeaderText="EvectionTel">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionTel %>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionAddress" Key="EvectionAddress" IsBound="false"
                                                Width="100px" HeaderText="EvectionAddress">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionAddress %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionTask" Key="EvectionTask" IsBound="false"
                                                Width="80px" HeaderText="EvectionTask">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionTask %>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionRoad" Key="EvectionRoad" IsBound="false"
                                                Width="60px" HeaderText="EvectionRoad">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionRoad%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReturnTime" Key="ReturnTime" IsBound="false"
                                                Width="130px" HeaderText="ReturnTime" Format="yyyy/MM/dd HH:mm">
                                                <Header Caption="<%$Resources:ControlText,gvReturnTime%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EvectionBy" Key="EvectionBy" IsBound="false"
                                                Width="50px" HeaderText="EvectionBy">
                                                <Header Caption="<%$Resources:ControlText,gvEvectionBy%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="motorman" Key="motorman" IsBound="false" Width="60px"
                                                HeaderText="motorman">
                                                <Header Caption="<%$Resources:ControlText,gvMotorMan%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="100px"
                                                HeaderText="Remark">
                                                <Header Caption="<%$Resources:ControlText,gvRemark %>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="True"
                                                HeaderText="Status">
                                                <Header Caption="<%$Resources:ControlText,gvStatus %>">
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                Width="50px" HeaderText="StatusName">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName %>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="create_user" Key="create_user" IsBound="false"
                                                Width="70px" HeaderText="create_user">
                                                <Header Caption="<%$Resources:ControlText,gvCreateUser %>">
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="create_Date" Key="create_Date" IsBound="false"
                                                Width="110px" HeaderText="create_Date">
                                                <Header Caption="<%$Resources:ControlText,gvCreateDate%>">
                                                    <RowLayoutColumnInfo OriginX="16" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="16" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <%--  <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="100px" Key="CheckBoxAll">
                                                <CellTemplate>
                                                    <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" Text="<%$Resources:ControlText,lb_jindu%>"
                                                        runat="server"></asp:LinkButton>
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                                </HeaderTemplate>
                                                <Header Caption="<%$Resources:ControlText,jindutu%>">
                                                </Header>
                                            </igtbl:TemplatedColumn>--%>
                                        </Columns>
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridLeaveApply" runat="server" Width="100%" Height="100%"
                                Visible="false" OnDataBound="UltraWebGridLeaveApply_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
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
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridLeaveApply_InitializeLayoutHandler"
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_LeaveApply" Key="KQM_LeaveApply">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll" HeaderText="CheckBox">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAllLeaveApply();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WORKNO" Key="WORKNO" IsBound="false" Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo%>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvLocalName%>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="True"
                                                HeaderText="Status">
                                                <Header Caption="Status">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="dcode" Key="dcode" IsBound="false" Hidden="True"
                                                HeaderText="dcode">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeCode" Key="LVTypeCode" IsBound="false"
                                                Hidden="True" HeaderText="LVTypeCode">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SEXName" Key="SEXName" IsBound="false" Width="40px"
                                                HeaderText="SEXName">
                                                <Header Caption="<%$Resources:ControlText,gvSexName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="buName" Key="buName" IsBound="false" Width="100px"
                                                HeaderText="buName">
                                                <Header Caption="<%$Resources:ControlText,gvgvbuName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DEPNAME" Key="DEPNAME" IsBound="false" Width="100px"
                                                HeaderText="DEPNAME">
                                                <Header Caption="<%$Resources:ControlText,gvgvDepName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTypeName" Key="LVTypeName" IsBound="false"
                                                Width="80px" HeaderText="LVTypeName">
                                                <Header Caption="<%$Resources:ControlText,gvLVTypeName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                Width="80px" HeaderText="StartDate" Format="yyyy-MM-dd">
                                                <Header Caption="<%$Resources:ControlText,gvStartDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartTime" Key="StartTime" IsBound="false"
                                                Width="60px" HeaderText="StartTime">
                                                <Header Caption="<%$Resources:ControlText,gvStartTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="80px"
                                                HeaderText="EndDate" Format="yyyy-MM-dd">
                                                <Header Caption="<%$Resources:ControlText,gvgvEndDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndTime" Key="EndTime" IsBound="false" Width="60px"
                                                HeaderText="EndTime">
                                                <Header Caption="<%$Resources:ControlText,gvEndTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotal" Key="LVTotal" IsBound="false" Width="50px"
                                                HeaderText="LVTotal">
                                                <Header Caption="<%$Resources:ControlText,gvgvLVTotal%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ThisLVTotal" HeaderText="ThisLVTotal" IsBound="False"
                                                Key="ThisLVTotal" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvgvThisLVTotal%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVTotalDays" HeaderText="LVTotalDays" IsBound="False"
                                                Key="LVTotalDays" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvgvLVTotalDays%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LVWorkDays" HeaderText="LVWorkDays" IsBound="False"
                                                Key="LVWorkDays" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvLVWorkDays%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Reason" Key="Reason" IsBound="false" Width="100px"
                                                HeaderText="Reason">
                                                <Header Caption="<%$Resources:ControlText,gvReason%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="proxyworkno" Key="proxyworkno" IsBound="false"
                                                Width="70px" HeaderText="ProxyWorkNo">
                                                <Header Caption="<%$Resources:ControlText,gvProxyWorkNo%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Proxy" Key="Proxy" IsBound="false" Width="70px"
                                                HeaderText="Proxy">
                                                <Header Caption="<%$Resources:ControlText,gvProxyName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TestifyFile" Key="TestifyFile" IsBound="false"
                                                Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvTestifyFile%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="UploadFile" Key="UploadFile" IsBound="false"
                                                Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvUploadFile%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApplyTypeName" Key="ApplyTypeName" IsBound="false"
                                                Width="60px" HeaderText="ApplyTypeName">
                                                <Header Caption="<%$Resources:ControlText,gvgvApplyTypeName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                Width="60px" HeaderText="StatusName">
                                                <Header Caption="<%$Resources:ControlText,gvStatusName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsLastYear" Key="IsLastYear" IsBound="false"
                                                Width="50px" HeaderText="IsLastYear">
                                                <Header Caption="<%$Resources:ControlText,gvIsLastYear%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150"
                                                HeaderText="BillNo">
                                                <Header Caption="<%$Resources:ControlText,gvBillNo%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                Width="70px" HeaderText="Modifier">
                                                <Header Caption="<%$Resources:ControlText,gvgvModifier%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_datestr" Key="update_datestr" IsBound="false"
                                                Width="110px" HeaderText="ModifyDate">
                                                <Header Caption="<%$Resources:ControlText,gvgvModifyDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150"
                                                HeaderText="BillNo">
                                                <Header Caption="<%$Resources:ControlText,gvProgress%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LevelCode" Key="LevelCode" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="LevelCode">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ManagerCode" Key="ManagerCode" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="ManagerCode">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ProxyStatus" Key="ProxyStatus" IsBound="false"
                                                Hidden="true">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true"
                                                HeaderText="ID">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ISTestify" Key="ISTestify" IsBound="false"
                                                Hidden="True" HeaderText="ISTestify">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ProxyNotes" Key="ProxyNotes" IsBound="false"
                                                Hidden="True" HeaderText="ProxyNotes">
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridKQE" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridKQE_DataBound" Visible="false">
                                <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridKQE" AutoGenerateColumns="false"
                                    CompactRendering="False" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    BorderCollapseDefault="Separate" AllowSortingDefault="No" HeaderClickActionDefault="SortSingle"
                                    AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect"
                                    StationaryMargins="HeaderAndFooter">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents></ClientSideEvents>
                                    <SelectedRowStyleDefault BackgroundImage="~/images/overbg.bmp">
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
                                    <igtbl:UltraGridBand BaseTableName="KQM_KaoQinData" Key="KQM_KaoQinData">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll" HeaderText="CheckBox">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" AllowUpdate="No" HeaderText="WorkNo"
                                                IsBound="false" Key="WorkNo" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvAttendHandleWorkNo%>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" AllowUpdate="No" HeaderText="LocalName"
                                                IsBound="false" Key="LocalName" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvAttendHandleLocalName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="180">
                                                <Header Caption="<%$Resources:ControlText,gvDepName%>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" AllowUpdate="No" HeaderText="KQDate"
                                                IsBound="false" Key="KQDate" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvHeadKQDate%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" AllowUpdate="No" HeaderText="ShiftDesc"
                                                IsBound="false" Key="ShiftDesc" Width="180">
                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc%>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" AllowUpdate="No" HeaderText="OnDutyTime"
                                                IsBound="false" Key="OnDutyTime" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvHeadOnDutyTime%>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" AllowUpdate="No" HeaderText="OffDutyTime"
                                                IsBound="false" Key="OffDutyTime" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime%>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AbsentQty" AllowUpdate="No" HeaderText="AbsentQty"
                                                IsBound="false" Key="AbsentQty" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvAbsentQty%>">
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="8" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExceptionTypeName" AllowUpdate="No" HeaderText="ExceptionTypeName"
                                                IsBound="false" Key="ExceptionTypeName" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvExceptionTypeName%>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExcepTionQty" AllowUpdate="No" HeaderText="ExcepTionQty"
                                                IsBound="false" Key="ExcepTionQty" Width="50">
                                                <Header Caption="<%$Resources:ControlText,workFlowDeTimes%>">
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="9" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" AllowUpdate="No" HeaderText="ReasonRemark"
                                                IsBound="false" Key="ReasonRemark" Width="200">
                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark%>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="True"
                                                HeaderText="ID">
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridTMA" runat="server" Width="100%" Height="100%"
                                Visible="false" OnDataBound="UltraWebGridTMA_DataBound">
                                <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridTMA" CompactRendering="False"
                                    AutoGenerateColumns="false" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    BorderCollapseDefault="Separate" AllowSortingDefault="No" HeaderClickActionDefault="SortSingle"
                                    AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="Edit"
                                    StationaryMargins="HeaderAndFooter" AllowUpdateDefault="No">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_activity" Key="gds_att_activity">
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
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
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
                                            <igtbl:UltraGridColumn BaseColumnName="STARTTIME" Key="STARTTIME" IsBound="false"
                                                Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvHeadBeginTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ENDTIME" Key="ENDTIME" IsBound="false" Format="yyyy/MM/dd"
                                                Width="100">
                                                <Header Caption="<%$Resources:ControlText, gvHeadEndTime%>">
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
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Width="0" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvHeadActivityID %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="remark" Key="remark" IsBound="false" Width="100">
                                                <Header Caption="<%$Resources:ControlText,labbeiz %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DEPCODE" Key="DEPCODE" IsBound="false" Width="0"
                                                Hidden="true">
                                                <Header Caption="DEPCODE" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridKQU" runat="server" Width="100%" Height="100%"
                                Visible="false" OnDataBound="UltraWebGridKQU_DataBound">
                                <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridKQU" CompactRendering="False"
                                    AutoGenerateColumns="false" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    BorderCollapseDefault="Separate" AllowSortingDefault="No" HeaderClickActionDefault="SortSingle"
                                    AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="Edit"
                                    StationaryMargins="HeaderAndFooter" AllowUpdateDefault="No">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
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
                                    <igtbl:UltraGridBand BaseTableName="GDS_ATT_MAKEUP" Key="GDS_ATT_MAKEUP">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="true">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="120" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DName" Key="DName" IsBound="false" Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkNo %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                Width="170px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDesc %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="KQDate" Key="KQDate" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvKQDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="CardTime" Key="CardTime" IsBound="false" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvOTMRealCardTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MakeupTypeName" Key="MakeupTypeName" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,cardMakeupType%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DecSalary" Key="DecSalary" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,workFlowDecSalary%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                                Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvApproveFlagName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonName" Key="ReasonName" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonName%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ReasonRemark" Key="ReasonRemark" IsBound="false"
                                                Width="150px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadReasonRemark%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DeTimes" Key="DeTimes" IsBound="false" Width="60">
                                                <Header Caption="<%$Resources:ControlText,workFlowDeTimes%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="150">
                                                <Header Caption="<%$Resources:ControlText,gvHeadBillNo%>">
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
                                            <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="70">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifier%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                Width="110">
                                                <Header Caption="<%$Resources:ControlText,gvOTMAdvanceModifyDate%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Hidden="true">
                                                <Header Caption="Status">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DCode" Key="DCode" IsBound="false" Hidden="true">
                                                <Header Caption="DCode">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                <Header Caption="ID">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>
                            <igtbl:UltraWebGrid ID="UltraWebGridKQM" runat="server" Width="100%" Height="100%"  Visible="false"
                                OnDataBound="UltraWebGridKQM_DataBound" OnInitializeLayout="UltraWebGridKQM_InitializeLayout">
                                <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridKQM" TableLayout="Fixed"
                                    CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
                                    <HeaderStyleDefault Height="25px" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGridOTMMonthTotal_InitializeLayoutHandler"
                                        AfterSelectChangeHandler="AfterSelectChange"></ClientSideEvents>
                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                    </SelectedRowStyleDefault>
                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                    </RowAlternateStyleDefault>
                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                        CssClass="tr_data">
                                        <Padding Left="3px"></Padding>
                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                    </RowStyleDefault>
                                    <ActivationObject BorderColor="" BorderWidth="">
                                    </ActivationObject>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="OTM_MonthTotal" Key="OTM_MonthTotal">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" HeaderText="CheckBox" Key="CheckBoxAll" Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DISSIGNRMARK" Key="DISSIGNRMARK" IsBound="false"
                                                Width="65" AllowUpdate="Yes">
                                                <Header Caption="<%$Resources:ControlText,lab_dissign %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="BuName" HeaderText="BuName" IsBound="false"
                                                Key="BuName" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvBuOTMQryName%>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="depcode" HeaderText="depcode" IsBound="false"
                                                Key="depcode" Width="120px" Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="dname" HeaderText="DName" IsBound="false"
                                                Key="dname" Width="120px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="1" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                Key="WorkNo" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="2" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                Key="LocalName" Width="70px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="3" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                Key="OverTimeType" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadOverTimeType %>">
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="4" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1Apply" HeaderText="G1Apply" IsBound="false"
                                                Key="G1Apply" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Apply" HeaderText="G2Apply" IsBound="false"
                                                Key="G2Apply" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3Apply" HeaderText="G3Apply" IsBound="false"
                                                Key="G3Apply" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1RelSalary" HeaderText="G1RelSalary" IsBound="false"
                                                Key="G1RelSalary" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                Key="G2RelSalary" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3RelSalary" HeaderText="G3RelSalary" IsBound="false"
                                                Key="G3RelSalary" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG1Apply" HeaderText="SpecG1Apply" IsBound="false"
                                                Key="SpecG1Apply" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="5" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG2Apply" HeaderText="SpecG2Apply" IsBound="false"
                                                Key="SpecG2Apply" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="6" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG3Apply" HeaderText="SpecG3Apply" IsBound="false"
                                                Key="SpecG3Apply" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="7" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG1Salary" HeaderText="SpecG1Salary" IsBound="false"
                                                Key="SpecG1Salary" Width="50px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG1Apply %>">
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="11" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG2Salary" HeaderText="SpecG2Salary" IsBound="false"
                                                Key="SpecG2Salary" Width="95px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Apply %>">
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="12" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG3Salary" HeaderText="SpecG3Salary" IsBound="false"
                                                Key="SpecG3Salary" Width="105px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG3Apply %>">
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="13" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2Remain" HeaderText="G2Remain" IsBound="false"
                                                Key="G2Remain" Width="80">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadG2Remain %>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MAdjust1" HeaderText="MAdjust1" IsBound="false"
                                                Key="MAdjust1" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadMAdjust1 %>">
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="14" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="MRelAdjust" HeaderText="MRelAdjust" IsBound="false"
                                                Key="MRelAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvHeadMRelAdjust %>">
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="15" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AdvanceAdjust" HeaderText="AdvanceAdjust"
                                                IsBound="false" Key="AdvanceAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvAdvanceAdjust %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="RestAdjust" HeaderText="RestAdjust" IsBound="false"
                                                Key="RestAdjust" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvRestAdjust %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" IsBound="false" Key="ApproveFlag"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="ApproveFlag">
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="YearMonth" IsBound="false" Key="YearMonth"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="YearMonth">
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day1" HeaderText="1" IsBound="false" Key="Day1"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="1">
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="17" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day2" HeaderText="2" IsBound="false" Key="Day2"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="2">
                                                    <RowLayoutColumnInfo OriginX="18" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="18" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day3" HeaderText="3" IsBound="false" Key="Day3"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="3">
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="19" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day4" HeaderText="4" IsBound="false" Key="Day4"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="4">
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="20" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day5" HeaderText="5" IsBound="false" Key="Day5"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="5">
                                                    <RowLayoutColumnInfo OriginX="21" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="21" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day6" HeaderText="6" IsBound="false" Key="Day6"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="6">
                                                    <RowLayoutColumnInfo OriginX="22" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="22" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day7" HeaderText="7" IsBound="false" Key="Day7"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="7">
                                                    <RowLayoutColumnInfo OriginX="23" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="23" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day8" HeaderText="8" IsBound="false" Key="Day8"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="8">
                                                    <RowLayoutColumnInfo OriginX="24" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="24" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day9" HeaderText="9" IsBound="false" Key="Day9"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="9">
                                                    <RowLayoutColumnInfo OriginX="25" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="25" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day10" HeaderText="10" IsBound="false" Key="Day10"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="10">
                                                    <RowLayoutColumnInfo OriginX="26" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="26" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day11" HeaderText="11" IsBound="false" Key="Day11"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="11">
                                                    <RowLayoutColumnInfo OriginX="27" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="27" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day12" HeaderText="12" IsBound="false" Key="Day12"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="12">
                                                    <RowLayoutColumnInfo OriginX="28" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="28" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day13" HeaderText="13" IsBound="false" Key="Day13"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="13">
                                                    <RowLayoutColumnInfo OriginX="29" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="29" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day14" HeaderText="14" IsBound="false" Key="Day14"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="14">
                                                    <RowLayoutColumnInfo OriginX="30" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="30" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day15" HeaderText="15" IsBound="false" Key="Day15"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="15">
                                                    <RowLayoutColumnInfo OriginX="31" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="31" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day16" HeaderText="16" IsBound="false" Key="Day16"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="16">
                                                    <RowLayoutColumnInfo OriginX="32" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="32" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day17" HeaderText="17" IsBound="false" Key="Day17"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="17">
                                                    <RowLayoutColumnInfo OriginX="33" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="33" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day18" HeaderText="18" IsBound="false" Key="Day18"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="18">
                                                    <RowLayoutColumnInfo OriginX="34" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="34" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day19" HeaderText="19" IsBound="false" Key="Day19"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="19">
                                                    <RowLayoutColumnInfo OriginX="35" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="35" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day20" HeaderText="20" IsBound="false" Key="Day20"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="20">
                                                    <RowLayoutColumnInfo OriginX="36" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="36" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day21" HeaderText="21" IsBound="false" Key="Day21"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="21">
                                                    <RowLayoutColumnInfo OriginX="37" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="37" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day22" HeaderText="22" IsBound="false" Key="Day22"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="22">
                                                    <RowLayoutColumnInfo OriginX="38" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="38" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day23" HeaderText="23" IsBound="false" Key="Day23"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="23">
                                                    <RowLayoutColumnInfo OriginX="39" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="39" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day24" HeaderText="24" IsBound="false" Key="Day24"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="24">
                                                    <RowLayoutColumnInfo OriginX="40" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="40" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day25" HeaderText="25" IsBound="false" Key="Day25"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="25">
                                                    <RowLayoutColumnInfo OriginX="41" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="41" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day26" HeaderText="26" IsBound="false" Key="Day26"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="26">
                                                    <RowLayoutColumnInfo OriginX="42" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="42" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day27" HeaderText="27" IsBound="false" Key="Day27"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="27">
                                                    <RowLayoutColumnInfo OriginX="43" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="43" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day28" HeaderText="28" IsBound="false" Key="Day28"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="28">
                                                    <RowLayoutColumnInfo OriginX="44" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="44" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day29" HeaderText="29" IsBound="false" Key="Day29"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="29">
                                                    <RowLayoutColumnInfo OriginX="45" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="45" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day30" HeaderText="30" IsBound="false" Key="Day30"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="30">
                                                    <RowLayoutColumnInfo OriginX="46" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="46" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Day31" HeaderText="31" IsBound="false" Key="Day31"
                                                Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="31">
                                                    <RowLayoutColumnInfo OriginX="47" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="47" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay1" HeaderText="1" IsBound="false" Key="SpecDay1"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay2" HeaderText="2" IsBound="false" Key="SpecDay2"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay3" HeaderText="3" IsBound="false" Key="SpecDay3"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay4" HeaderText="4" IsBound="false" Key="SpecDay4"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay5" HeaderText="5" IsBound="false" Key="SpecDay5"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay6" HeaderText="6" IsBound="false" Key="SpecDay6"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay7" HeaderText="7" IsBound="false" Key="SpecDay7"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay8" HeaderText="8" IsBound="false" Key="SpecDay8"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay9" HeaderText="9" IsBound="false" Key="SpecDay9"
                                                Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay10" HeaderText="10" IsBound="false"
                                                Key="SpecDay10" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay11" HeaderText="11" IsBound="false"
                                                Key="SpecDay11" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay12" HeaderText="12" IsBound="false"
                                                Key="SpecDay12" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay13" HeaderText="13" IsBound="false"
                                                Key="SpecDay13" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay14" HeaderText="14" IsBound="false"
                                                Key="SpecDay14" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay15" HeaderText="15" IsBound="false"
                                                Key="SpecDay15" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay16" HeaderText="16" IsBound="false"
                                                Key="SpecDay16" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay17" HeaderText="17" IsBound="false"
                                                Key="SpecDay17" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay18" HeaderText="18" IsBound="false"
                                                Key="SpecDay18" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay19" HeaderText="19" IsBound="false"
                                                Key="SpecDay19" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay20" HeaderText="20" IsBound="false"
                                                Key="SpecDay20" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay21" HeaderText="21" IsBound="false"
                                                Key="SpecDay21" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay22" HeaderText="22" IsBound="false"
                                                Key="SpecDay22" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay23" HeaderText="23" IsBound="false"
                                                Key="SpecDay23" Hidden="true" Width="30px">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay24" HeaderText="24" IsBound="false"
                                                Key="SpecDay24" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay25" HeaderText="25" IsBound="false"
                                                Key="SpecDay25" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay26" HeaderText="26" IsBound="false"
                                                Key="SpecDay26" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay27" HeaderText="27" IsBound="false"
                                                Key="SpecDay27" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay28" HeaderText="28" IsBound="false"
                                                Key="SpecDay28" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay29" HeaderText="29" IsBound="false"
                                                Key="SpecDay29" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay30" HeaderText="30" IsBound="false"
                                                Key="SpecDay30" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecDay31" HeaderText="31" IsBound="false"
                                                Key="SpecDay31" Hidden="true" Width="30px">
                                            </igtbl:UltraGridColumn>
                                            <%--<igtbl:UltraGridColumn BaseColumnName="ID" IsBound="false" Key="ID"
                                                Hidden="true">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="ID">
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Header>
                                                <Footer>
                                                    <RowLayoutColumnInfo OriginX="48" />
                                                </Footer>
                                            </igtbl:UltraGridColumn>--%>
                                        </Columns>
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="JavaScript" type="text/javascript">                                document.write("</DIV>");</script>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr id="KQT" style=" display:none;">
            <td>
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

                            <script language="javascript">                                document.write("<div id='div_2' class='KQT' style='height:" + document.body.clientHeight * 59 / 100 + ";display:none;'>");</script>

                            

                            <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

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
            </td>
        </tr>--%>
        <%-- <tr id="KQL" style=" display:none;">
            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                <tr style="width: 100%">
                    <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                        <img height="18" src="../../../CSS/Images_new/EMP_01.gif" width="19">
                    </td>
                    <td background="../../../CSS/Images_new/EMP_07.gif" height="19px">
                    </td>
                    <td valign="top" width="19px" background="../../../CSS/Images_new/EMP_06.gif" height="18">
                        <img height="18" src="../../../CSS/Images_new/EMP_02.gif" width="19">
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td width="19" background="../../../CSS/Images_new/EMP_05.gif">
                        &nbsp;
                    </td>
                    <td>

                        <script language="javascript">                            document.write("<DIV id='div_select2' class='KQL' style='height:" + document.body.clientHeight * 59 / 100 + ";display:none;'>");</script>

                        

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
        </tr>--%>
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="td_label" colspan="2" height="10">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_label" width="11%">
                            &nbsp;
                            <asp:Label ID="labelApRemark" runat="server">ApRemark:</asp:Label>
                        </td>
                        <td class="td_label" width="89%">
                            <asp:TextBox ID="textBoxApRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="ButtonApproveAgree" Width="50px" runat="server" Text="Approve" ToolTip="Authority Code:Approve"
                                CommandName="Approve" OnClick="ButtonApprove_Click" OnClientClick="return CheckApprove()">
                            </asp:Button>
                            <asp:Button ID="ButtonDisApprove" Width="50px" runat="server" Text="DisApprove" ToolTip="Authority Code:DisApprove"
                                CommandName="DisApprove" OnClick="ButtonDisApprove_Click"></asp:Button>
                            <%--<asp:Button ID="ButtonCountersigning" Width="50px" runat="server" Text="Close" ToolTip="Authority Code:Countersigning"
                                    CommandName="Countersigning" OnClientClick="return ShowCountersigning()"></asp:Button>--%>
                            <asp:Button ID="ButtonClose" Width="50px" runat="server" Text="Close" ToolTip="Authority Code:Close"
                                CommandName="Close" OnClientClick="return CheckClose()"></asp:Button>
                            <asp:Label ID="labelAuditRemark" runat="server" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <div style="width: 100%;">
                <table cellspacing="0" cellpadding="0" class="table_title_area">
                    <tr style="width: 100%;" id="tr2">
                        <td style="width: 100%;" class="tr_title_center">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_sign_log1" runat="server" Text="Label"></asp:Label>
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
        </tr>
        <tr>
            <td colspan="2">

                <script language="javascript">                    document.write("<DIV id='div_status' style='height:" + document.body.clientHeight * 18 / 100 + "'>");</script>

                <igtbl:UltraWebGrid ID="UltraWebGridStatus" runat="server" Width="100%" Height="100%">
                    <DisplayLayout UseFixedHeaders="true" Name="UltraWebGridStatus" CompactRendering="False"
                        RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
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
                        <igtbl:UltraGridBand BaseTableName="WFM_AuditStatus" Key="WFM_AuditStatus">
                            <Columns>
                                <igtbl:UltraGridColumn BaseColumnName="AuditMan" Key="AuditMan" IsBound="true" Width="8%">
                                    <Header Caption="<%$Resources:ControlText,lbllWorkNo %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="AuditManName" Key="AuditManName" IsBound="true"
                                    Width="8%">
                                    <Header Caption="<%$Resources:ControlText,gvAuditer %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="true" Width="10%">
                                    <Header Caption="<%$Resources:ControlText,lblToDepName %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="AuditTypeName" Key="AuditTypeName" IsBound="true"
                                    Width="15%">
                                    <Header Caption="<%$Resources:ControlText,signtypename %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="AuditDate" Key="AuditDate" IsBound="true"
                                    Width="14%">
                                    <Header Caption="<%$Resources:ControlText,gvHeadApproveDate %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="true"
                                    Width="8%">
                                    <Header Caption="<%$Resources:ControlText,gvStatusName %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="true" Width="10%">
                                    <Header Caption="<%$Resources:ControlText,gvOTMAdvanceApRemark %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="CounterRemark" Key="CounterRemark" IsBound="true"
                                    Width="10%">
                                    <Header Caption="<%$Resources:ControlText,labbeiz %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="SendTime" Key="SendTime" IsBound="true" Width="10%">
                                    <Header Caption="<%$Resources:ControlText,NotesSendtime %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="StayHours" Key="StayHours" IsBound="true"
                                    Width="7%">
                                    <Header Caption="<%$Resources:ControlText,StayHours %>">
                                    </Header>
                                </igtbl:UltraGridColumn>
                            </Columns>
                        </igtbl:UltraGridBand>
                    </Bands>
                </igtbl:UltraWebGrid>

                <script language="JavaScript" type="text/javascript">                    document.write("</DIV>");</script>

            </td>
        </tr>
    </table>
    </form>
</body>
</html>
