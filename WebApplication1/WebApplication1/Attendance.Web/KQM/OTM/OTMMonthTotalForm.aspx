<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMMonthTotalForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMMonthTotalForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMMonthTotal</title>
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
function CheckAll()
{
	var sValue=false;
	var chk=document.getElementById("UltraWebGridOTMMonthTotal_ctl00_CheckBoxAll");
	if(chk.checked)
	{
		sValue=true;
	}				
	var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
	var gRows = grid.Rows;
	for(i=0;i<gRows.length;i++)
	{
		if(!igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").disabled)
		{
		    igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
		}
	}
}
function UpProgress()
{
	document.getElementById("lbluploadMsg").innerText = "";
	document.getElementById("btnImportSave").style.display="none";
	document.getElementById("btnImport").disabled="disabled";
	document.getElementById("btnExport").disabled="disabled";			
	document.getElementById("imgWaiting").style.display="";
	//document.getElementById("labelupload").innerText = "<%=this.GetResouseValue("common.message.uploading")%>";			
}
function AfterSelectChange(gridName, id)
{
	var row = igtbl_getRowById(id);
	
	DisplayRowData(row);
	return 0;
}
function UltraWebGridOTMMonthTotal_InitializeLayoutHandler(gridName)
{
	var row = igtbl_getActiveRow(gridName);
	DisplayRowData(row);
}
function DisplayRowData(row)
{
	if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
	{	
		igtbl_getElementById("HiddenWorkNo").value=row.getCellFromKey("WorkNo").getValue()==null?"":row.getCellFromKey("WorkNo").getValue();
		var ApproveFlag=row.getCellFromKey("ApproveFlag").getValue()==null?"":row.getCellFromKey("ApproveFlag").getValue();
		if(ApproveFlag=="0")
		{
		    igtbl_getElementById("txtModifyWorkNo").value=row.getCellFromKey("WorkNo").getValue()==null?"":row.getCellFromKey("WorkNo").getValue();				    
		    igtbl_getElementById("txtLocalName2").value=row.getCellFromKey("LocalName").getValue()==null?"":row.getCellFromKey("LocalName").getValue();
		    igtbl_getElementById("txtOverTimeType").value=row.getCellFromKey("OverTimeType").getValue()==null?"":row.getCellFromKey("OverTimeType").getValue();
		    igtbl_getElementById("txtG1Apply").value=row.getCellFromKey("G1Apply").getValue()==null?"":row.getCellFromKey("G1Apply").getValue();
		    igtbl_getElementById("txtG2Apply").value=row.getCellFromKey("G2Apply").getValue()==null?"":row.getCellFromKey("G2Apply").getValue();
		    igtbl_getElementById("txtG3Apply").value=row.getCellFromKey("G3Apply").getValue()==null?"":row.getCellFromKey("G3Apply").getValue();
		    igtbl_getElementById("txtG1RelSalary").value=row.getCellFromKey("G1RelSalary").getValue()==null?"":row.getCellFromKey("G1RelSalary").getValue();
            igedit_getById("txtG2RelSalary").setValue(row.getCellFromKey("G2RelSalary").getValue());
            igedit_getById("txtG3RelSalary").setValue(row.getCellFromKey("G3RelSalary").getValue());
		    igtbl_getElementById("txtMRelAdjust").value=row.getCellFromKey("MRelAdjust").getValue()==null?"":row.getCellFromKey("MRelAdjust").getValue();	
		    document.getElementById("HiddenMRelAdjust").value=row.getCellFromKey("MRelAdjust").getValue()==null?"":row.getCellFromKey("MRelAdjust").getValue();	
		    document.getElementById("HiddenG2RelSalary").value=row.getCellFromKey("G2RelSalary").getValue()==null?"0":row.getCellFromKey("G2RelSalary").getValue();
		    document.getElementById("HiddenG3RelSalary").value=row.getCellFromKey("G3RelSalary").getValue()==null?"0":row.getCellFromKey("G3RelSalary").getValue();
		    document.getElementById("HiddenBillNo").value=row.getCellFromKey("BillNo").getValue()==null?"":row.getCellFromKey("BillNo").getValue();	
		    document.getElementById("HiddenOrgCode").value=row.getCellFromKey("depcode").getValue()==null?"":row.getCellFromKey("depcode").getValue();			    		    
        }
        else
        {
            document.getElementById("HiddenBillNo").value=row.getCellFromKey("BillNo").getValue()==null?"":row.getCellFromKey("BillNo").getValue();	
        }
	}
}
function GetSignMap() {
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
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
    var Doc = igtbl_getElementById("HiddenBillNo").value;
    var windowWidth = 600, windowHeight = 600;
    var X = (screen.availWidth - windowWidth) / 2;
    var Y = (screen.availHeight - windowHeight) / 2;
    var Revalue = window.showModalDialog("/WorkFlow/SignLogAndMap.aspx?Doc=" +
      Doc, window, "dialogWidth=" + windowWidth + "px;dialogHeight=" + windowHeight + "px;dialogLeft=" + X + "px;dialogTop=" + Y + "px;help=no;status=no;scrollbars=no");
    return false;
}        
function OpenEdit()//彈出設定頁面
{		    
    var ModuleCode = igtbl_getElementById("HiddenModuleCode").value;
    var WorkNo=igtbl_getElementById("HiddenWorkNo").value;
    var YearMonth=igtbl_getElementById("HiddenYearMonth").value;		    
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
	var gRows = grid.Rows;
	var Count=0;
	for(i=0;i<gRows.length;i++)
	{
		if(gRows.getRow(i).getSelected())
		{
		    Count+=1;	
		    var Status=gRows.getRow(i).getCellFromKey("ApproveFlag").getValue();			    
		    switch (Status){case "0": break; case "3":break; default: alert(Message.OnlyNoCanModify); return false; break; }	
		}
	}			
	if(Count==0)
	{
	    alert(Message.AtLastOneChoose);
	    return false;
	}
      var width;
      var height;
      width=screen.width*0.4;
      height=screen.height*0.2;
      var iTop = (window.screen.availHeight-30-height)/2; //获得窗口的垂直位置;
      var iLeft = (window.screen.availWidth-10-width)/2; //获得窗口的水平位置;
     // window.open("OTMMonthTotalEditForm.aspx?WorkNo="+WorkNo+"&YearMonth="+YearMonth+"&ModuleCode="+ModuleCode,"OTMMonthTotalEdit",width,height);
      var url="OTMMonthTotalEditForm.aspx?WorkNo="+WorkNo+"&YearMonth="+YearMonth+"&ModuleCode="+ModuleCode;
     // var fe="height:500px;width:350px; screenTop:100px; screenLeft:500px;status:no;scroll:yes;";

      window.open("OTMMonthTotalEditForm.aspx?WorkNo="+WorkNo+"&YearMonth="+YearMonth+"&ModuleCode="+ModuleCode,null,'height='+height+',,innerHeight='+height+',width='+width+',innerWidth='+width+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');
      return false;
}

function Recalculate()//重新計算
{
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
	var gRows = grid.Rows;
	var Count=0;
	for(i=0;i<gRows.length;i++)
	{
        if(igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").checked)
        {
		    Count+=1;				    
		    if(gRows.getRow(i).getCellFromKey("ApproveFlag").getValue()!="0")
		    {
	            alert(Message.UnAudit);return false;
		    }
		}
	}			
	if(Count==0)
	{
	    alert(Message.AtLastOneChoose);
	    return false;
	}
	if(Count>1)
	{
	    alert(Message.CanOnlyChooseOne);
	    return false;
	}
    if(confirm(Message.ConfirmReturn))
    {
        return true;
    }
    else
    {			    
        return false;
    }
}
function copyToClipboard()
{
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
    var gRows = grid.Rows;
    var Count=0;
    for(i=0;i<gRows.length;i++)
    {
	    if(gRows.getRow(i).getSelected())
	    {
	        Count+=1;			        		
	    }
    }			
    if(Count==0)
    {
        alert(Message.AtLastOneChoose);
        return false;
    }
    var BillNo = igtbl_getElementById("HiddenBillNo").value;
    clipboardData.setData('Text',BillNo);
    alert(Message.billno_copy_success);
            return false;
}

function OrgRecal()//組織重新計算
{
	if(document.getElementById("txtDepCode").value.length==0)
	{
	    alert(Message.DepCodeNotNull);
	    return false;
	}
    return true;
}
  function ShowBatchWorkNo() {
      document.all("pnlBatchWorkNo").style.display="";
      document.all("pnlBatchWorkNo").style.top=document.all("txtWorkNo").style.top;
      document.getElementById("txtBatchEmployeeNo").style.display="";
      document.getElementById("txtWorkNo").value="";
      document.getElementById("txtBatchEmployeeNo").value="";
      document.getElementById("txtBatchEmployeeNo").focus();
      return false;
  }
  function HiddenBatchWorkNo() {
      document.all("pnlBatchWorkNo").style.display="none";
      document.getElementById("txtBatchEmployeeNo").style.display="none";
      document.getElementById("txtBatchEmployeeNo").value="";
  }
  $(function(){
        $("#tr_edit").toggle(
            function(){$("#div_1").hide(); $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_1").show();$(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
       });
    $(function(){
       $("#tr_show").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif"); },
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif"); } ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){$("#div_showdata").hide(); $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");},
            function(){$("#div_showdata").show(); $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");} ) 
   });
    function setSelector(ctrlCode,ctrlName,moduleCode)
   {
       var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
       var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
       var info=window.showModalDialog(url,null,fe);
       if(info)
       {
           $("#"+ctrlCode).val(info.codeList);
           $("#" + ctrlName).val(info.nameList);
       }
       return false;
   }
function CheckSendAudit()//送簽
{
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
    var gRows = grid.Rows;
    var Count=0;
    var Status="";
    for(i=0;i<gRows.length;i++)
    {
	    if(igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").checked)
	    {
	         Count+=1;			        
	         Status=gRows.getRow(i).getCell(26).getValue();
	         switch (Status)
                {case "0":  break; default: alert(Message.UnSendAudit);return false; break;}	 
	    }
    }			
    if(Count==0)
    {alert(Message.NoSelect);return false;}
    if(confirm(Message.ConfirmReturn))
    {return true; }else{return false; }
}
function CheckCancelAudit()//取消核準
{
    var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
    var gRows = grid.Rows;
    var Count=0;
    var Status="";
    for(i=0;i<gRows.length;i++)
    {
	    if(igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").checked)
	    {
	         Count+=1;			        
	         Status=gRows.getRow(i).getCell(26).getValue();
	         switch (Status)
                {case "2": break; default: alert(Message.UnCancelAudit); return false;  break; } 
        }
    }			
    if(Count==0)
    { alert(Message.AtLastOneChoose);return false;}
    if(confirm(Message.ConfirmReturn))
    {return true;}
    else
    {return false; }
} 
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="QueryFlag" type="hidden" name="QueryFlag" runat="server">
    <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HiddenBillNo" type="hidden" name="HiddenBillNo" runat="server">
    <input id="HiddenOrgCode" type="hidden" name="HiddenOrgCode" runat="server">
    <input id="HiddenMRelAdjust" type="hidden" name="HiddenMRelAdjust" runat="server">
    <input id="HiddenG2RelSalary" type="hidden" name="HiddenG2RelSalary" runat="server">
    <input id="HiddenG3RelSalary" type="hidden" name="HiddenG3RelSalary" runat="server">
    <input id="HiddenModuleCode" type="hidden" name="HiddenModuleCode" runat="server">
    <div style="width: 100%;">
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
                    <div>
                        <img class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_1">
        <table class="table_data_area" style="width: 100%">
            <tr style="width: 100%">
                <td>
                    <table style="width: 100%">
                        <tr class="tr_data">
                            <td>
                                <asp:Panel ID="pnlContent" runat="server">
                                    <table class="table_data_area">
                                        <tr class="tr_data_1">
                                            <td width="10%">
                                                &nbsp;
                                                <asp:Label ID="lbl_Unit" runat="server">Department:</asp:Label>
                                            </td>
                                            <td width="15%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                        </td>
                                                        <td width="100%">
                                                            <asp:TextBox ID="txtDepName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
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
                                                <asp:Label ID="lblIsSupporter" runat="server" Text="Supporter"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlIsSupporter" class="input_textBox_1" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td width="10%">
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblEmployeeNo" runat="server" Text="Label"></asp:Label>
                                                <asp:Image ID="imgBatchWorkNo" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                </asp:Image>
                                            </td>
                                            <td width="25%">
                                                <asp:TextBox ID="txtWorkNo" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                <div id="pnlBatchWorkNo" style="padding-right: 3px; width: 250px; padding-left: 3px;
                                                    z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                                    border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                                    border-bottom: #0000ff 1px solid; position: absolute; left: 38%; float: left;
                                                    display: none;">
                                                    <table cellspacing="0" cellpadding="1" width="100%" align="left">
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
                                                                                    <td width="100%">
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
                                            <td width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtLocalName" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblOTTypeQryCode" runat="server">OTTypeCode:</asp:Label>
                                            </td>
                                            <td width="25%">
                                                <cc1:DropDownCheckList ID="ddlOTTypeCode" class="input_textBox_2" Width="150" RepeatColumns="1"
                                                    TextWhenNoneChecked="" DisplayTextWidth="300" DropImageSrc="../../CSS/Images/expand.gif"
                                                    ClientCodeLocation="../../JavaScript/DropDownCheckList.js" runat="server">
                                                </cc1:DropDownCheckList>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblYearMonth" runat="server">YearMonth:</asp:Label>
                                            </td>
                                            <td>
                                                <igtxt:WebDateTimeEdit ID="txtYearMonth" class="input_textBox_1" runat="server" EditModeFormat="yyyy/MM">
                                                </igtxt:WebDateTimeEdit>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblApproveFlag" runat="server">ApproveFlag:</asp:Label>
                                            </td>
                                            <td>
                                                <cc1:DropDownCheckList ID="ddlApproveFlag" class="input_textBox_1" Width="150" RepeatColumns="1"
                                                    DropImageSrc="../../CSS/Images/expand.gif" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                    TextWhenNoneChecked="" DisplayTextWidth="300" runat="server">
                                                </cc1:DropDownCheckList>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Label ID="lblStatus" runat="server">EmpStatus:</asp:Label>
                                            </td>
                                            <td>
                                                <cc1:DropDownCheckList ID="ddlEmpStatus" class="input_textBox_1" Width="100" RepeatColumns="1"
                                                    DropImageSrc="../../CSS/Images/expand.gif" TextWhenNoneChecked="" DisplayTextWidth="300"
                                                    ClientCodeLocation="../../JavaScript/DropDownCheckList.js" runat="server">
                                                </cc1:DropDownCheckList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnQuery" class="button_2" runat="server" OnClick="btnQuery_Click">
                                </asp:Button>
                                <asp:Button ID="btnNormalQuery" class="button_morelarge" runat="server" OnClick="btnNormalQuery_Click">
                                </asp:Button>
                                <asp:Button ID="btnSpecQuery" class="button_morelarge" runat="server" OnClick="btnSpecQuery_Click">
                                </asp:Button>
                                <asp:Button ID="btnReset" class="button_2" runat="server" OnClick="btnReset_Click">
                                </asp:Button>
                            </td>
                            <td class="td_seperator">
                            </td>
                            <td>
                                <asp:Button ID="btnModify" class="button_2" runat="server" OnClientClick="return OpenEdit()" />
                                <asp:Button ID="btnImport" class="button_2" runat="server" OnClick="btnImport_Click">
                                </asp:Button>
                                <asp:Button ID="btnExport" class="button_2" runat="server" OnClick="btnExport_Click">
                                </asp:Button>
                                <asp:Button ID="btnAudit" class="button_2" runat="server" OnClientClick="return ShowAudit()">
                                </asp:Button>
                                <asp:Button ID="btnCancelAudit" class="button_large" OnClientClick="return CheckCancelAudit()"
                                    runat="server" OnClick="btnCancelAudit_Click"></asp:Button>
                                <asp:Button ID="btnBatchCancelAudit" class="button_morelarge" runat="server" OnClick="btnBatchCancelAudit_Click"
                                    OnClientClick="return ShowAuditAll()"></asp:Button>
                                <asp:Button ID="btnAuditAll" class="button_large" runat="server" OnClick="btnAuditAll_Click"
                                    OnClientClick="return ShowAuditAll()"></asp:Button>
                                <asp:Button ID="btnRecalculate" class="button_large" runat="server" OnClientClick="return Recalculate()"
                                    OnClick="btnRecalculate_Click"></asp:Button>
                                <asp:Button ID="btnOrgRecal" class="button_large" runat="server" OnClientClick="return OrgRecal()"
                                    OnClick="btnOrgRecal_Click"></asp:Button>
                                <div id="PanelAudit" style="padding-right: 3px; width: 200px; padding-left: 3px;
                                    z-index: 12; right: 2px; padding-bottom: 3px; padding-top: 3px; background-color: #ffffee;
                                    border-right: #0000ff 1px solid; border-top: #0000ff 1px solid; border-left: #0000ff 1px solid;
                                    border-bottom: #0000ff 1px solid; position: absolute; left: 20%; float: left;
                                    display: none;">
                                    <table cellspacing="0" cellpadding="1" width="100%" align="left">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td width="30%">
                                                                        &nbsp;
                                                                        <asp:Label ID="lblApprover" runat="server">Approver:</asp:Label>
                                                                    </td>
                                                                    <td width="70%">
                                                                        <asp:TextBox ID="txtApprover" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                        <asp:Label ID="lblApproveDate" runat="server">ApproveDate:</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtApproveDate" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="10%" colspan="2">
                                                                        &nbsp;
                                                                        <asp:Button ID="btnSave" runat="server" class="button_2" OnClick="btnSave_Click" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" class="button_2" OnClientClick="return HiddenAudit()" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td class="td_seperator">
                            </td>
                            <td>
                                <asp:Button ID="btnSendAudit" runat="server" OnClientClick="return CheckSendAudit()"
                                    class="button_2" OnClick="btnSendAudit_Click" />
                                <asp:Button ID="btnOrgAudit" runat="server" class="button_morelarge" OnClientClick="return CheckSendAudit()"
                                    OnClick="btnOrgAudit_Click" />
                                <asp:Button ID="btnCopyBillNo" class="button_morelarge" OnClientClick="return  copyToClipboard()"
                                    runat="server"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;">
                <td style="width: 100%;" id="tr_show" class="tr_title_center">
                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                        background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                        font-size: 13px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDisplayArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="OTMmsg" runat="server"></asp:Label>
                </td>
                <td class="tr_title_center" style="width: 300px;">
                    <div>
                        <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                            HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px" 
                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            ShowMoreButtons="false" DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" OnPageChanged="pager_PageChanged"
                            ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px;">
                    <img class="img2" id="tr_showtd" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                </td>
            </tr>
        </table>
        <div id="div_showdata">
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

                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*60/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridOTMMonthTotal" runat="server" Width="100%" Height="100%"
                            OnDataBound="UltraWebGridOTMMonthTotal_DataBound" OnInitializeLayout="UltraWebGridOTMMonthTotal_InitializeLayout">
                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridOTMMonthTotal" TableLayout="Fixed"
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
                                        <igtbl:UltraGridColumn BaseColumnName="BillNo" Key="BillNo" IsBound="false" Width="100px"
                                            HeaderText="BillNo">
                                            <Header Caption="<%$Resources:ControlText,gvBillNo %>" Fixed="True">
                                                <RowLayoutColumnInfo OriginX="19" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="19" />
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
                                        <igtbl:UltraGridColumn BaseColumnName="ApproveFlagName" HeaderText="ApproveFlagName"
                                            IsBound="false" Key="ApproveFlagName" Width="50px">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <Header Caption="<%$Resources:ControlText,gvHeadApproveFlagName %>">
                                                <RowLayoutColumnInfo OriginX="16" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="16" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ApproveFlag" HeaderText="ApproveFlag" IsBound="false"
                                            Key="ApproveFlag" Width="50px" Hidden="true">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <Header Caption="">
                                                <RowLayoutColumnInfo OriginX="16" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="16" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="APPROVER" Key="auditer" IsBound="false" Width="60px"
                                            HeaderText="auditer">
                                            <Header Caption="<%$Resources:ControlText,gvAuditer %>">
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="APPROVEDATE" Key="auditdate" IsBound="false"
                                            Width="150px" HeaderText="auditdate">
                                            <Header Caption="<%$Resources:ControlText,gvAuditDate %>">
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="APREMARK" Key="auditidea" IsBound="false"
                                            Width="100px" HeaderText="auditidea">
                                            <Header Caption="<%$Resources:ControlText,gvAuditIdea %>">
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="14" />
                                            </Footer>
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
                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                            HeaderClickAction="Select" Width="100px" Key="jindutu">
                                            <CellTemplate>
                                                <asp:LinkButton ID="lb_jindu" OnClientClick="return GetSignMap();" runat="server">查看進度</asp:LinkButton>
                                            </CellTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ControlText,jindutu%>"></asp:Label>
                                            </HeaderTemplate>
                                            <Header Caption="<%$Resources:ControlText,jindutu%>">
                                            </Header>
                                        </igtbl:TemplatedColumn>
                                    </Columns>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

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
    <asp:Panel ID="PanelImport" runat="server" Width="100%" Visible="false">
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr style="cursor: hand">
                <td>
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                        <tr style="width: 100%;" id="tr_showim">
                            <td style="width: 100%;" class="tr_title_center">
                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                    background-repeat: no-repeat; background-position-x: center; width: 80px; text-align: center;
                                    font-size: 13px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblImport" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tr_table_title" align="right">
                                <img class="img3" src="../../CSS/Images_new/left_back_03_a.gif">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div_import">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="100%" align="left" colspan="2">
                        <a href="/ExcelModel/OTMMonthTotalSample.xls">
                            <asp:Label ID="lblUploadText" runat="server" Font-Bold="true"></asp:Label>
                        </a>
                        <asp:FileUpload ID="FileUpload" runat="server" />
                        <asp:Button ID="btnImportSave" runat="server" class="button_2" OnClick="btnImportSave_Click"
                            OnClientClick="javascript:UpProgress();" />
                        <img id="imgWaiting" src="../../CSS/Images/clocks.gif" border="0" style="display: none;
                            height: 20px;" />
                        <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="height: 25;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div id="div1">
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

                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                    RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                    BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                    Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                    AutoGenerateColumns="false">
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
                                    <ActivationObject BorderColor="" BorderWidth="">
                                    </ActivationObject>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="30%">
                                                <Header Caption="<%$Resources:ControlText,gvErrorMsg %>">
                                                </Header>
                                                <CellStyle ForeColor="red">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="YearMonth" Key="YearMonth" IsBound="false"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveYear %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2RelSalary" HeaderText="G2RelSalary" IsBound="false"
                                                Key="G2RelSalary" Width="120">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvMonthG2RelSalary %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SpecG2RelSalary" HeaderText="SpecG2RelSalary"
                                                IsBound="false" Key="SpecG2RelSalary" Width="120">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Header Caption="<%$Resources:ControlText,gvSpecG2RelSalary %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

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
    </asp:Panel>
    <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    </form>

    <script type="text/javascript"><!--  
        document.all("PanelAudit").style.display="none";
        function ShowAudit() {
        var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
		var gRows = grid.Rows;
			var Count=0;
			for(i=0;i<gRows.length;i++)
			{
				if(igtbl_getElementById("UltraWebGridOTMMonthTotal_ci_0_0_"+i+"_CheckBoxCell").checked)
				{
				    Count+=1;				    
				    if(gRows.getRow(i).getCellFromKey("ApproveFlag").getValue()!="0")
				    {
			            alert(Message.UnAudit);
			            return false;
				    }				   
				}
			}			
			if(Count==0)
			{
			    alert( Message.AtLastOneChoose);
			    return false;
			}	
            document.all("PanelAudit").style.display="";
            document.all("PanelAudit").style.top=document.all("btnAudit").style.top;
            document.getElementById("txtApprover").value="";
            document.getElementById("txtApprover").focus();
            document.getElementById("ProcessFlag").value="Audit";
            return false;
        }
        function ShowAuditAll()
        {
            var grid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
	        var gRows = grid.Rows;
	        var Count=0;			
	        if(gRows.length==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        else
	        { 
	            if(confirm(Message.ConfirmReturn))
                {
                    return true;
	            }
	            else
                {			    
                    return false;
                }
	         }
        }
        function HiddenAudit() {
            document.all("PanelAudit").style.display="none";
            document.getElementById("ProcessFlag").value="";
            return false;
        }
        if(document.getElementById("btnQuery").disabled=="false")
        {
            HiddenColumns();         
        }
        
        document.getElementById("txtWorkNo").focus();
        document.getElementById("txtWorkNo").select();             
        function getDaysInMonth(year,month){
              month = parseInt(month,10)+1;
              var temp = new Date(year+"/"+month+"/0");
              return temp.getDate();
        }
		function HiddenColumns() {
		    var YearMonth=document.getElementById("txtYearMonth").value;
		    if(YearMonth.length!=0)
		    {
		        var temVal = new Array();   
                temVal =YearMonth.split("/");
                var days=getDaysInMonth(temVal[0],temVal[1],1)                
                var oGrid = igtbl_getGridById('UltraWebGridOTMMonthTotal');
                var oBands = oGrid.Bands;
                var oBand = oBands[0];
                var oColumns = oBand.Columns;
                if(days==28)
                {
                    oColumns[54].setHidden(true);
                    oColumns[55].setHidden(true);
                    oColumns[56].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_54").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="none";
                    
                     if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_121").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_122").style.display="none";
                    }
                    /*if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_89")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_89").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_90").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_91").style.display="none";
                    }*/
                }
                if(days==29)
                {
                    oColumns[54].setHidden(false);
                    oColumns[55].setHidden(true);
                    oColumns[56].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_54").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="none";
                    
                     if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_121").style.display="none";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_122").style.display="none";
                    }
                }
                if(days==30)
                {
                    oColumns[54].setHidden(false);
                    oColumns[55].setHidden(false);
                    oColumns[56].setHidden(true);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_54").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="none";
                    if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_121").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_122").style.display="none";
                    }
                }
                if(days==31)
                {
                    oColumns[54].setHidden(false);
                    oColumns[55].setHidden(false);
                    oColumns[56].setHidden(false);
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_54").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_55").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_c_0_56").style.display="";
                   
                     if(document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120")!=null)
                    {
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_120").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_121").style.display="";
                    document.getElementById("UltraWebGridOTMMonthTotal_hb_0_122").style.display="";
                    }
                }
            }
        }     
	--></script>

</body>
</html>
