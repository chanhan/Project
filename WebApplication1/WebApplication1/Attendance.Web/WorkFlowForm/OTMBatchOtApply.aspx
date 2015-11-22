<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMBatchOtApply.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlowForm.OTMBatchOtApply" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
    
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

     <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"><!--

		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			window.returnValue=row.getCell(0).getValue()+";"+row.getCell(1).getValue();
			window.close();
			
		}
       function GetOTType()
       {            
            var OTDate=document.getElementById("textBoxOTDate").value;
	        var WorkNo=document.getElementById("HiddenWorkNo").value; 
            if (OTDate.length>0&&WorkNo.length>0)
            {
               // KQM_OTM_OTMBatchOtApply.FindOTType(OTDate, WorkNo, docallback);

                $.ajax(
                {
                    type: "post",
                    url: "AjaxProcess/GetOTType.ashx?OTDate=" + escape(OTDate) + "&WorkNo=" + escape(WorkNo),
                    dataType: "text",
                    async: false,
                    success: function(data) {
                        if (data == "-1") {
                            alert("計算時數出現異常,請檢查是否排班或排班開始結束時間和加班開始結束時間是否有沖突");
                        }
                        else {
                            document.getElementById("textBoxOTType").value = data;
                        }
                    }
                }
                );
		    }		
       }
       function docallback(res) {
        document.getElementById("textBoxOTType").value = res.value;
       }
	    function WeekDate()
	    {		    
	        igtbl_getElementById("textBoxWeek").value="";
    	    
	        if(igtbl_getElementById("textBoxOTDate").value.length==0)
	        {		       
	           return;		       
	        }
	        if(document.getElementById("HiddenWorkNo").value.length==0)
	        {
	            alert(Message.common_message_data_no_select_employees);	 
	           igtbl_getElementById("textBoxOTDate").value='';   
	           igtbl_getElementById("textBoxWeek").value='';
	           igtbl_getElementById("textBoxOTType").value='';   
	           return;		       
	        }    	    
	        // Get OverTime Type
	        GetOTType();    	   
		    //........Get WeekDay........................
		    date=new Date(igtbl_getElementById("textBoxOTDate").value.replace("-",","));				
		    //alert(igtbl_getElementById("textBoxWorkDay").value.replace("-",","));
    		
		    if(date.getDay()==0)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_sunday;
		    }
		    if(date.getDay()==1)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_monday;
		    }
		    if(date.getDay()==2)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_tuesday;
		    }
		    if(date.getDay()==3)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_wednesday;
		    }
		    if(date.getDay()==4)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_thursday;
		    }
		    if(date.getDay()==5)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_friday;
		    }
		    if(date.getDay()==6)
		    {
		        document.getElementById("textBoxWeek").value = Message.common_saturday;
		    }
		    return;
    		
	    }		
		function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("WebGroupBoxNo_GridEmployee_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('WebGroupBoxNo_GridEmployee');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("WebGroupBoxNo_GridEmployee_ci_0_3_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("WebGroupBoxNo_GridEmployee_ci_0_3_"+i+"_CheckBoxCell").checked=sValue;
				}
				
			}
		}		
		function CheckSelAll()
		{
			var sValue=false;
			var chk=document.getElementById("WebGroupBoxYes_GridSelEmployee_ctl00_CheckBoxSelAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('WebGroupBoxYes_GridSelEmployee');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("WebGroupBoxYes_GridSelEmployee_ci_0_3_"+i+"_CheckBoxSelCell").disabled)
				{
				    igtbl_getElementById("WebGroupBoxYes_GridSelEmployee_ci_0_3_"+i+"_CheckBoxSelCell").checked=sValue;
				}
				
			}
		}
        function textBoxBeginTime_TextChanged(oEdit, newText, oEvent)
        {
       
	        if(igedit_getById("textBoxBeginTime").getValue()!=null&&igedit_getById("textBoxEndTime").getValue()!=null)
	        {        
	            //getDays();
	            var WorkNo=document.getElementById("HiddenWorkNo").value;
	            var OTDate=document.getElementById("textBoxOTDate").value; 
	            var BeginTime=document.getElementById("textBoxBeginTime").value; 
	            var EndTime=document.getElementById("textBoxEndTime").value; 
	            var OTType=document.getElementById("textBoxOTType").value;
	           // KQM_OTM_OTMBatchOtApply.GetOTHours(WorkNo, OTDate, BeginTime, EndTime, OTType, docallback_gethour);
	            $.ajax(
                {
                    type: "post",
                    url: "AjaxProcess/GetOtHours.ashx?WorkNo=" + escape(WorkNo) + "&OTDate=" + escape(OTDate) + "&BeginTime=" + escape(BeginTime) + "&EndTime=" + escape(EndTime) + "&OTType=" + escape(OTType),
                    dataType: "text",
                    async: false,
                    success: function(data) {
                    document.getElementById("textBoxHours").value = data;
                    }
                }
                );
	        }
        }
         function textBoxEndTime_TextChanged(oEdit, newText, oEvent)
        {
           var WorkNo=document.getElementById("HiddenWorkNo").value;
            var OTDate=document.getElementById("textBoxOTDate").value; 
            var BeginTime=document.getElementById("textBoxBeginTime").value; 
            var EndTime=document.getElementById("textBoxEndTime").value; 
            var OTType=document.getElementById("textBoxOTType").value;
            if(WorkNo.length>0&&OTDate.length>0&&BeginTime.length>0&&EndTime.length>0)
            {
                // KQM_OTM_OTMBatchOtApply.GetOTHours(WorkNo,OTDate,BeginTime,EndTime,OTType,docallback_gethour);
                $.ajax(
                {
                    type: "post",
                    url: "AjaxProcess/GetOtHours.ashx?WorkNo=" + escape(WorkNo) + "&OTDate=" + escape(OTDate) + "&BeginTime=" + escape(BeginTime) + "&EndTime=" + escape(EndTime) + "&OTType=" + escape(OTType),
                    dataType: "text",
                    async: false,
                    success: function(data) {
                    document.getElementById("textBoxHours").value = data;
                    }
                }
                );
	        }
        }
        function docallback_gethour(res) 
        {
            document.getElementById("textBoxHours").value = res.value;
        }
        
        function setSelector(ctrlCode, ctrlName, moduleCode) {
            var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
            var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
            var info = window.showModalDialog(url, null, fe);
            if (info) {
                $("#" + ctrlCode).val(info.codeList);
                $("#" + ctrlName).val(info.nameList);
            }
            return false;
        }
// -->
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
        <input id="HiddenOTDate" type="hidden" name="HiddenOTDate" runat="server">
        <table class="top_table" cellpadding="0" cellspacing="1" border="1" width="100%"
            align="center">
            <tr>
                <td>
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr >
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;" id="tr_edit" class="tr_title_center">
                                        <td style="width: 100%;">
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
                                            <div id="Div1">
                                                <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                                        </td>
                                    </tr>
                                </table>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="div_1" style="height: 500;">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="td_label" width="11%">
                                                &nbsp;
                                                <asp:Label ID="labelDepcode" runat="server" Text="Label"></asp:Label>                                               
                                            </td>
                                            <td class="td_input" width="22%">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="textBoxDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                                Style="display: none"></asp:TextBox></td>
                                                        <td width="100%">
                                                            <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox></td>
                                                        <td style="cursor: hand">
                                                             <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif">
                                                            </asp:Image>
                                                            
                                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="td_label" width="11%">
                                                &nbsp;
                                                <asp:Label ID="UserLabelBatchOtDate" runat="server" Text="Label"></asp:Label>                                               
                                            </td>
                                            <td class="td_input" width="22%">
                                                <asp:TextBox ID="textBoxBatchOtDate" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="34%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label" width="11%">
                                                &nbsp;
                                                <asp:Label ID="labelPersonType" runat="server" Text="Label"></asp:Label>
                                                
                                            </td>
                                            <td class="td_input" width="22%">
                                                <asp:DropDownList ID="ddlPersonType" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_label" colspan="3">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonQuery" 
                                                     runat="server" 
                                                   CssClass="button_1"  Text="Query" ToolTip="Authority Code:Query" 
                                                    CommandName="Query" onclick="ButtonQuery_Click">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td colspan="4" height="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="39%" valign="top">
                                                <igmisc:WebGroupBox ID="WebGroupBoxNo" runat="server" Width="100%" Height="400px">
                                                    <Template>
                                                        <igtbl:UltraWebGrid ID="GridEmployee" runat="server" Width="100%" Height="100%">
                                                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                                AllowColSizingDefault="Free" Name="GridEmployee" TableLayout="Fixed" CellClickActionDefault="RowSelect" AutoGenerateColumns="false"
                                                                RowSelectorsDefault="No">
                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                    CssClass="tr_header">
                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                </HeaderStyleDefault>
                                                                <FrameStyle Width="100%" Height="100%" BackColor="AliceBlue">
                                                                </FrameStyle>
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
                                                                <igtbl:UltraGridBand BaseTableName="Employees" Key="Employees">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                                            Key="WorkNo" Width="70">
                                                                            <Header Caption="<%$Resources:ControlText,lblFromPersoncode%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                                            Key="LocalName" Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,lblFromPersonName%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                                            Key="OverTimeType" Width="40">
                                                                            <Header Caption="<%$Resources:ControlText,lblPersonType%>" Fixed="true">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                            HeaderClickAction="Select" HeaderText="CheckBox" Key="CheckBoxAll" Width="30">
                                                                            <CellTemplate>
                                                                                <asp:CheckBox ID="CheckBoxCell" runat="server" />
                                                                            </CellTemplate>
                                                                            <HeaderTemplate>
                                                                                <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                            <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:TemplatedColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="false"
                                                                            Key="ShiftDesc" Width="200">
                                                                             <Header Caption="<%$Resources:ControlText,lblShift%>" >
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>
                                                    </Template>
                                                </igmisc:WebGroupBox>
                                            </td>
                                            <td width="6%" align="center" valign="middle">
                                                <asp:Button ID="Button_Add" runat="server"  Text="－＞" CssClass="button_1" 
                                                    onclick="ButtonAdd_Click" /><br />
                                                <br />
                                                <asp:Button ID="ButtonDecrease" runat="server"  Text="＜－" CssClass="button_1" onclick="ButtonDecrease_Click"
                                                    />
                                            </td>
                                            <td width="25%" valign="top">
                                                <igmisc:WebGroupBox ID="WebGroupBoxYes" runat="server" Width="100%" Height="400px">
                                                    <Template>
                                                        <igtbl:UltraWebGrid ID="GridSelEmployee" runat="server" Width="100%" Height="100%">
                                                            <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                                AllowColSizingDefault="Free" Name="GridSelEmployee" TableLayout="Fixed" CellClickActionDefault="RowSelect" AutoGenerateColumns="false"
                                                                RowSelectorsDefault="No">
                                                                <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                    CssClass="tr_header">
                                                                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                </HeaderStyleDefault>
                                                                <FrameStyle Width="100%" Height="100%" BackColor="AliceBlue">
                                                                </FrameStyle>
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
                                                                <igtbl:UltraGridBand BaseTableName="Employees" Key="Employees">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                                            Key="WorkNo" Width="38%">
                                                                            <Header Caption="<%$Resources:ControlText,lblFromPersoncode%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                                                            Key="LocalName" Width="27%">
                                                                            <Header Caption="<%$Resources:ControlText,lblFromPersonName%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="OverTimeType" HeaderText="OverTimeType" IsBound="false"
                                                                            Key="OverTimeType" Width="20%">
                                                                            <Header Caption="<%$Resources:ControlText,lblPersonType%>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                                            HeaderClickAction="Select" HeaderText="CheckBox" Key="CheckBoxSelCell" Width="15%">
                                                                            <CellTemplate>
                                                                                <asp:CheckBox ID="CheckBoxSelCell" runat="server" />
                                                                            </CellTemplate>
                                                                            <HeaderTemplate>
                                                                                <input id="CheckBoxSelAll" onclick="javascript:CheckSelAll();" runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                        </igtbl:TemplatedColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" HeaderText="ShiftDesc" IsBound="True"
                                                                            Key="ShiftDesc" Hidden="true">                                                                            
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>
                                                    </Template>
                                                </igmisc:WebGroupBox>
                                            </td>
                                            <td width="30%" valign="top">
                                                <table cellspacing="0" cellpadding="0" width="98%" align="right">
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelApplyDate" runat="server" Text="Label"></asp:Label>                                                            
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <asp:TextBox ID="textBoxApplyDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelOTDateFrom" runat="server" Text="Label"></asp:Label>                                                           
                                                        </td>
                                                        <td class="td_label_view" width="70%">
                                                            <asp:TextBox ID="textBoxOTDate" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelWeek" runat="server" Text="Label"></asp:Label>                                                            
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <asp:TextBox ID="textBoxWeek" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelOTType" runat="server" Text="Label"></asp:Label>                                                           
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <asp:TextBox ID="textBoxOTType" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelBeginTime" runat="server" Text="Label"></asp:Label>                                                           
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <igtxt:WebDateTimeEdit ID="textBoxBeginTime" runat="server" EditModeFormat="HH:mm"
                                                                Width="100%" CssClass="input_textBox" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                                <ClientSideEvents ValueChange="textBoxBeginTime_TextChanged" />
                                                            </igtxt:WebDateTimeEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelEndTime" runat="server" Text="Label"></asp:Label>                                                            
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <igtxt:WebDateTimeEdit ID="textBoxEndTime" runat="server" EditModeFormat="HH:mm"
                                                                Width="100%" CssClass="input_textBox" onkeydown="if(event.keyCode==13) event.keyCode=9">
                                                                <ClientSideEvents ValueChange="textBoxEndTime_TextChanged" />
                                                            </igtxt:WebDateTimeEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelHours" runat="server" Text="Label"></asp:Label>                                                            
                                                        </td>
                                                        <td class="td_input" width="70%">
                                                            <asp:TextBox ID="textBoxHours" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_label_view" width="30%">
                                                            &nbsp;
                                                            <asp:Label ID="labelWorkDesc" runat="server" Text="Label"></asp:Label>                                                           
                                                        </td>
                                                        <td class="td_input" width="70%" colspan="5">
                                                            <asp:TextBox ID="textBoxWorkDesc" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="28">
                                                            <asp:Button ID="ButtonSave" runat="server" CommandName="Save" 
                                                                  
                                                                Text="Save" ToolTip="Authority Code:Save" CssClass="button_1" 
                                                                onclick="ButtonSave_Click" />&nbsp;
                                                            <asp:Button ID="ButtonExit" runat="server" CommandName="Exit" 
                                                                 
                                                                Text="Exit" ToolTip="Authority Code:Exit"  CssClass="button_1"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="28">
                                                            <asp:Label ID="labMessage" runat="server" Text="Succeed" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
