<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMMonthTotalEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.OTM.OTMMonthTotalEditForm" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTMMonthTotalEditForm</title>
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--		
        function CheckRelSalary() {
            var G2Apply=document.getElementById("txtG2Apply").value;
            if(igedit_getById("txtG2RelSalary")!=null)
            {
                var G2RelSalary=igedit_getById("txtG2RelSalary").getValue()==null?"0":igedit_getById("txtG2RelSalary").getValue(); 
                var MRelAdjust=document.getElementById("HiddenMRelAdjust").value;
                var G2RelSalaryOld=document.getElementById("HiddenG2RelSalary").value;
                var Temp=parseFloat(G2Apply)-parseFloat(G2RelSalary);               
                var TempOld=parseFloat(G2Apply)-parseFloat(G2RelSalaryOld);
                document.getElementById("txtMRelAdjust").value=Math.round((parseFloat(MRelAdjust)+Temp-TempOld)*10)/10;            
            }
            var SpecG2Apply=document.getElementById("txtG2SpecApply").value;  
            if(igedit_getById("txtSpecG2RelSalary")!=null)
            {
                var SpecG2RelSalary=igedit_getById("txtSpecG2RelSalary").getValue()==null?"0":igedit_getById("txtSpecG2RelSalary").getValue(); 
                var sMRelAdjust=document.getElementById("HiddenMRelAdjust").value;
                var SpecG2RelSalaryOld=document.getElementById("HiddenSpecG2RelSalary").value;
                var sTemp=parseFloat(SpecG2Apply)-parseFloat(SpecG2RelSalary);
                var sTempOld=parseFloat(SpecG2Apply)-parseFloat(SpecG2RelSalaryOld);
                document.getElementById("txtMRelAdjust").value=Math.round((parseFloat(document.getElementById("txtMRelAdjust").value)+sTemp-sTempOld)*10)/10;           
            }
            
        }
         function CheckRelSalary2() {
            var SpecG2Apply=document.getElementById("txtG2SpecApply").value;  
            if(igedit_getById("txtSpecG2RelSalary")!=null)
            {
                var SpecG2RelSalary=igedit_getById("txtSpecG2RelSalary").getValue()==null?"0":igedit_getById("txtSpecG2RelSalary").getValue(); 
                var sMRelAdjust=document.getElementById("HiddenMRelAdjust").value;
                var SpecG2RelSalaryOld=document.getElementById("HiddenSpecG2RelSalary").value;
                var sTemp=parseFloat(SpecG2Apply)-parseFloat(SpecG2RelSalary);
                var sTempOld=parseFloat(SpecG2Apply)-parseFloat(SpecG2RelSalaryOld);
                document.getElementById("txtMRelAdjust").value=Math.round((parseFloat(sMRelAdjust)+sTemp-sTempOld)*10)/10;            
            }
            var G2Apply=document.getElementById("txtG2Apply").value;
            if(igedit_getById("txtG2RelSalary")!=null)
            {
                var G2RelSalary=igedit_getById("txtG2RelSalary").getValue()==null?"0":igedit_getById("txtG2RelSalary").getValue(); 
                var MRelAdjust=document.getElementById("HiddenMRelAdjust").value;
                var G2RelSalaryOld=document.getElementById("HiddenG2RelSalary").value;
                var Temp=parseFloat(G2Apply)-parseFloat(G2RelSalary);               
                var TempOld=parseFloat(G2Apply)-parseFloat(G2RelSalaryOld);
                document.getElementById("txtMRelAdjust").value=Math.round((parseFloat(document.getElementById("txtMRelAdjust").value)+Temp-TempOld)*10)/10;            
            }
        }		
        function SaveRelSalary() {
            var G2Apply=document.getElementById("txtG2Apply").value;                 
            var G2RelSalary=igedit_getById("txtG2RelSalary").getValue();
            var SpecG2Apply=document.getElementById("txtG2SpecApply").value; 
            var SpecG2RelSalary=igedit_getById("txtSpecG2RelSalary").getValue()==null?"0":igedit_getById("txtSpecG2RelSalary").getValue(); 

            //var G3RelSalary=igedit_getById("txtG3RelSalary").getValue(); 
            //if(parseFloat(G2Apply)<parseFloat(G2RelSalary)||parseFloat(G3Apply)<parseFloat(G3RelSalary))
            
            if(parseFloat(G2Apply)<parseFloat(G2RelSalary))
            {
		        alert('實發加班時數不能大於申報加班時數');//實發加班時數不能大於申報加班時數
                return false;
            }
            if(parseFloat(SpecG2Apply)<parseFloat(SpecG2RelSalary))
            {
		        alert('實發加班時數不能大於申報加班時數');//實發加班時數不能大於申報加班時數
                return false;
            }
		    //FormSubmit("<%=sAppPath%>");
            return true;
        }
	    function Close()
	    {
		    //window.opener.document.all.ButtonQuery.click();
		    window.close();
           return false;
	    }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenYearMonth" type="hidden" name="HiddenYearMonth" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HiddenMRelAdjust" type="hidden" name="HiddenMRelAdjust" runat="server">
    <input id="HiddenG2RelSalary" type="hidden" name="HiddenG2RelSalary" runat="server">
    <input id="HiddenG3RelSalary" type="hidden" name="HiddenG3RelSalary" runat="server">
    <input id="HiddenSpecG2RelSalary" type="hidden" name="HiddenSpecG2RelSalary" runat="server">
    <input id="HiddenSpecG3RelSalary" type="hidden" name="HiddenSpecG3RelSalary" runat="server">
    <input id="HiddenIsSpec" type="hidden" name="HiddenIsSpec" runat="server">
    <table cellspacing="0" cellpadding="1" width="100%" align="left">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr class="tr_data">
                                    <td>
                                        <asp:Panel ID="pnlContent" runat="server">
                                            <table class="table_data_area">
                                                <tr class="tr_data_1">
                                                    <td colspan="3">
                                                    </td>
                                                    <td class="input_textBox_1">
                                                        G1
                                                    </td>
                                                    <td class="input_textBox_1">
                                                        G2
                                                    </td>
                                                    <td class="input_textBox_1">
                                                        G3
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td width="15%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server">WorkNo:</asp:Label>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtWorkNo" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        &nbsp;
                                                        <asp:Label ID="lblApply" runat="server">Apply:</asp:Label>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG1Apply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG2Apply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG3Apply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_1">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblLocalName" runat="server">textBoxLocalName:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLocalName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblRelSalary" runat="server">RelSalary:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtG1RelSalary" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="txtG2RelSalary" class="input_textBox_1" runat="server"
                                                            Width="100%" HorizontalAlign="Left" MaxValue="200" MinValue="0">
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="txtG3RelSalary" class="input_textBox_1" runat="server"
                                                            Width="100%" HorizontalAlign="Left" MaxValue="200" MinValue="0">
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                </tr>
                                                <tr id="SpeOTM1" class="tr_data_2">
                                                    <td width="15%">
                                                        &nbsp;
                                                    </td>
                                                    <td width="20%">
                                                        &nbsp;
                                                    </td>
                                                    <td width="20%">
                                                        &nbsp;
                                                        <asp:Label ID="lblSpcApply" runat="server">Apply:</asp:Label>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG1SpecApply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG2SpecApply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtG3SpecApply" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="SpeOTM2" class="tr_data_1">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblSpcRelSalary" runat="server">RelSalary:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSpecG1RelSalary" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="txtSpecG2RelSalary" class="input_textBox_1" runat="server"
                                                            Width="100%" HorizontalAlign="Left" MaxValue="200" MinValue="0">
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="txtSpecG3RelSalary" class="input_textBox_1" runat="server"
                                                            Width="100%" HorizontalAlign="Left" MaxValue="200" MinValue="0">
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblOverTimeType" runat="server">OverTimeType:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOverTimeType" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblMRelAdjust" runat="server">MRelAdjust:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMRelAdjust" class="input_textBox_2" runat="server" Width="100%"></asp:TextBox>
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
                        <td width="10%" colspan="6">
                            &nbsp;
                            <asp:Button ID="btnSave" class="button_2" runat="server" OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnClose" class="button_2" runat="server" OnClientClick="return Close()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript"><!--  
         
	    document.getElementById("txtWorkNo").readOnly=true;
        document.getElementById("txtLocalName").readOnly=true
        document.getElementById("txtOverTimeType").readOnly=true;
        document.getElementById("txtG1Apply").readOnly=true;
        document.getElementById("txtG2Apply").readOnly=true;
        document.getElementById("txtG3Apply").readOnly=true;
        document.getElementById("txtG1SpecApply").readOnly=true;
        document.getElementById("txtG2SpecApply").readOnly=true;
        document.getElementById("txtG3SpecApply").readOnly=true;

        document.getElementById("txtG1RelSalary").readOnly=true;
        igedit_getById("txtG3RelSalary").setReadOnly(true);
        document.getElementById("txtSpecG1RelSalary").readOnly=true;
        igedit_getById("txtSpecG3RelSalary").setReadOnly(true);

        document.getElementById("txtMRelAdjust").readOnly=true;
        if(document.getElementById("HiddenIsSpec").value=="N")
        {
            document.getElementById("SpeOTM1").style.display="none";
            document.getElementById("SpeOTM2").style.display="none";
        }
	--></script>

</body>
</html>
