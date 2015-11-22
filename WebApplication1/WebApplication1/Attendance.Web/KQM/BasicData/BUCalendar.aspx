<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BUCalendar.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.BUCalendar" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>KQMBUCalendarForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-1.5.1.min.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
            border-style: none;
        }
        .img_hidden
        {
            display: none;
        }
        .img_show
        {
            display: block;
        }
        .notput_textBox_noborder
        {
            border: 0;
        }
        .img_show
        {
            visibility: visible;
        }
    </style>

    <script type="text/javascript">
         var strMonday,strTuesday,strWednesday,strThursday,strFriday,strSaturday,strSunday;
    
         strMonday="<%=strMonday %>";
         strTuesday="<%= strTuesday %>";
         strMonday="<%= strMonday %>";
         strWednesday="<%= strWednesday %>";
         strThursday="<%= strThursday %>";
         strFriday="<%= strFriday %>";
         strSaturday="<%= strSaturday %>";
         strSunday="<%= strSunday %>";
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			
			DisplayRowData(row);
			return 0;
		}
		function WeekDate()
		{		    
		    igtbl_getElementById("txtWeekDay").value="";
		    igtbl_getElementById("txtWeekNo").value="";
		       
		    if(igtbl_getElementById("txtWorkDay").value.length==0)
		    {		       
		       return;		       
		    } 
		    var totalDays = 0;
		    //var tttt=document.all("txtWorkDay").value;
		   // alter(tttt);
			var years = Number(parseInt(igtbl_getElementById("txtWorkDay").value));
			var month = Number(igtbl_getElementById("txtWorkDay").value.substr(5,2))-1;
			//alert("Month"+month);
			var day = Number(igtbl_getElementById("txtWorkDay").value.substr(8,2)) 
			//now = new Date(window.all.txtWorkDay.value); 		
			// Array to hold the total days in a month
			
			var days = new Array(12);  
			days[0] = 31; 
			days[2] = 31; 
			days[3] = 30; 
			days[4] = 31; 
			days[5] = 30; 
			days[6] = 31; 
			days[7] = 31; 
			days[8] = 30; 
			days[9] = 31; 
			days[10] = 30; 
			days[11] = 31; 

			//  Check to see if this is a leap year 

			if (Math.round(years/4) == years/4) { 
				days[1] = 29 
			}else{ 
				days[1] = 28 
			} 

			//  If this is January no need for any fancy calculation otherwise figure out the 
			//  total number of days to date and then determine what week 

			if (month == 0) {         
				totalDays = totalDays + day; 
			}else{ 
				var curMonth = month; 
				for (var count = 1; count <= curMonth; count++) { 
					totalDays = totalDays + days[count - 1]; 
				} 
				totalDays = totalDays + day; 
			}			
			var week = Math.round(totalDays/7); 			
			igtbl_getElementById("txtWeekNo").value=week
			//document.all("textBoxWeekNo").value=week;
			//........Get WeekDay........................
				date=new Date(igtbl_getElementById("txtWorkDay").value.replace("-",","));				
				//alert(igtbl_getElementById("txtWorkDay").value.replace("-",","));
				strMonday,strTuesday,strWednesday,strThursday,strFriday,strSaturday,strSunday;
				if(date.getDay()==0)
				{
					igtbl_getElementById("txtWeekDay").value=strSunday;
				}
				if(date.getDay()==1)
				{
					igtbl_getElementById("txtWeekDay").value=strMonday;
				}
				if(date.getDay()==2)
				{
					igtbl_getElementById("txtWeekDay").value=strTuesday;
				}
				if(date.getDay()==3)
				{				   
					igtbl_getElementById("txtWeekDay").value=strWednesday;
				}
				if(date.getDay()==4)
				{
					igtbl_getElementById("txtWeekDay").value=strThursday;
				}
				if(date.getDay()==5)
				{
					igtbl_getElementById("txtWeekDay").value=strFriday;
				}
				if(date.getDay()==6)
				{
					igtbl_getElementById("txtWeekDay").value=strSaturday;
				}
		}
		function UltraWebGridBuCalendar_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(igtbl_getElementById("hidOperate").value!="Add" && igtbl_getElementById("hidOperate").value!="Condition" && row != null)
			{			
			    igtbl_getElementById("hidBUCode").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();	
				igtbl_getElementById("hidWorkDay").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue("yyyy/mm/dd");	
				igtbl_getElementById("txtBUCode").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();	
				igtbl_getElementById("txtDPname").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();	
				igtbl_getElementById("txtWorkDay").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue("yyyy/mm/dd");
				igtbl_getElementById("txtWeekNo").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();	
				igtbl_getElementById("txtWeekDay").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();	
				igtbl_getElementById("ddlWorkFlag").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();	
				igtbl_getElementById("ddlHoliDayFlag").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();	
				igtbl_getElementById("txtRemark").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
				igtbl_getElementById("hidddlWorkFlag").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();	
				igtbl_getElementById("hidddlHoliDayFlag").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
				//igtbl_getElementById("hidWorkDay").value=row.getCellFromKey("WorkDay").getValue("yyyy/mm/dd")==null?"":row.getCellFromKey("WorkDay").getValue("yyyy/mm/dd");	
				//igtbl_getElementById("hidBUCode").value=row.getCellFromKey("BUCode").getValue("yyyy/mm/dd")==null?"":row.getCellFromKey("WorkDay").getValue("yyyy/mm/dd");				
			}
			else
			{
			    igtbl_getElementById("ddlWorkFlag").value=0;
				igtbl_getElementById("txtWorkDay").value="";
				igtbl_getElementById("txtWeekNo").value="";
				igtbl_getElementById("txtWeekDay").value="";
				igtbl_getElementById("txtRemark").value="";
				igtbl_getElementById("ddlHoliDayFlag").value=0;
				igtbl_getElementById("txtBUCode").value="";	
				igtbl_getElementById("hidddlWorkFlag").value="";
				igtbl_getElementById("hidddlHoliDayFlag").value="";						
			}
		}
		$(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
       });
           $(function(){
       $("#tr_show").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
    });
    $(function(){
       $("#tr_showtd").toggle(
            function(){
                $("#div_showdata").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_showdata").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
   });
   $(function(){
    $("#tr_showim").toggle(
            function(){
                $("#div_import").hide();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_import").show();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 
   });
   function setSelector(ctrlCode,ctrlName,moduleCode)
   {
       var url="RelationSelector.aspx?moduleCode="+moduleCode;
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
<body onload="return Load()">
    <form id="Form1" method="post" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="hidddlWorkFlag" type="hidden" name="hidddlWorkFlag" runat="server">
        <input id="hidddlHoliDayFlag" type="hidden" name="hidddlHoliDayFlag" runat="server">
        <input id="hidWorkDay" type="hidden" name="hidWorkDay" runat="server">
        <input id="hidBUCode" type="hidden" name="hidBUCode" runat="server">
        <asp:HiddenField ID="ImportFlag" runat="server" />
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
                                                <td style="width: 8%; height: 24px">
                                                    <asp:Label ID="lblBUCode" runat="server" ForeColor="Blue">Department:</asp:Label>
                                                </td>
                                                <td style="height: 24px;" colspan="5">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="20%" style="height: 24px">
                                                                <asp:TextBox ID="txtBUCode" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                                            </td>
                                                            <td style="height: 24px">
                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td width="1%">
                                                                            <asp:Image ID="imgDPcode" runat="server" class="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                            </asp:Image>
                                                                        </td>
                                                                        <td width="99%">
                                                                            <asp:TextBox ID="txtDPname" runat="server" class="input_textBox_1" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td style="height: 24px" width="12%">
                                                    &nbsp;<asp:Label ID="lblWorkDay" runat="server" ForeColor="Blue">WorkDay:</asp:Label>
                                                </td>
                                                <td style="height: 24px" width="21%">
                                                    <asp:TextBox ID="txtWorkDay" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                </td>
                                                <td style="height: 24px" width="12%">
                                                    <asp:Label ID="lblWeekDay" runat="server">WeekDay:</asp:Label>
                                                </td>
                                                <td style="height: 24px" width="21%">
                                                    <asp:TextBox ID="txtWeekDay" runat="server" Width="100%" class="input_textBox_2"
                                                        TabIndex="200"></asp:TextBox>
                                                </td>
                                                <td style="height: 24px" width="12%">
                                                    <asp:Label ID="lblWeekNo" runat="server">WeekNo:</asp:Label>
                                                </td>
                                                <td style="height: 24px" width="21%">
                                                    <asp:TextBox ID="txtWeekNo" runat="server" Width="100%" class="input_textBox_2" TabIndex="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td width="12%" style="height: 24px">
                                                    &nbsp;<asp:Label ID="lblWorkFlag" ForeColor="Blue" runat="server">WorkFlag:</asp:Label>
                                                </td>
                                                <td width="21%" style="height: 24px">
                                                    <asp:DropDownList ID="ddlWorkFlag" runat="server" class="input_textBox_1" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="12%" style="height: 24px">
                                                    &nbsp;<asp:Label ID="lblHoliDayFlag" ForeColor="Blue" runat="server">HoliDayFlag:</asp:Label>
                                                </td>
                                                <td width="21%" style="height: 24px">
                                                    <asp:DropDownList ID="ddlHoliDayFlag" class="input_textBox_1" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="12%" style="height: 24px">
                                                    &nbsp;<asp:Label ID="lblRemark" runat="server" Width="100%">Remark:</asp:Label>
                                                </td>
                                                <td width="21%" style="height: 24px">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
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
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="btnCondition" runat="server" class="button_2" OnClientClick="return condition_click();">
                            </asp:Button>
                            <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                            </asp:Button>
                            <asp:Button ID="btnAdd" runat="server" class="button_2" OnClientClick="return add_click();">
                            </asp:Button>
                            <asp:Button ID="btnModify" runat="server" class="button_2" OnClientClick="return  modify_click();">
                            </asp:Button>
                            <asp:Button ID="btnDelete" runat="server" class="button_2" OnClientClick="return  delete_click();"
                                OnClick="btnDelete_Click"></asp:Button>
                            <asp:Button ID="btnSave" runat="server" class="button_2" OnClientClick="return save_click();"
                                OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" class="button_2" OnClientClick="return  cancel_click();">
                            </asp:Button>
                            <asp:Button ID="btnImport" runat="server" class="button_2" OnClick="btnImport_Click">
                            </asp:Button>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidOperate" runat="server" />
        </div>
        <div>
            <asp:Panel ID="PanelData" runat="server" Width="100%">
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
                        </td>
                        <td class="tr_title_center" style="width: 300px;">
                            <div>
                                <ess:AspNetPager ID="pager" AlwaysShow="true" runat="server" ShowFirstLast="false"
                                    HorizontalAlign="Center" PageSize="50" PagingButtonType="Image" Width="300px"
                                    ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ShowMoreButtons="false"
                                    ButtonImageAlign="left" ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
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

                                <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*55/100+";'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridBuCalendar" runat="server" Width="100%" Height="100%">
                                    <DisplayLayout CompactRendering="False" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                        BorderCollapseDefault="Separate" AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle"
                                        AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridBuCalendar"
                                        CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter" AutoGenerateColumns="false">
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
                                        <igtbl:UltraGridBand BaseTableName="gds_att_BuCalendar" Key="gds_att_BuCalendar">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="BUCode" Key="BUCode" IsBound="false" HeaderText=""
                                                    Width="10%" Hidden="true">
                                                    <Header Caption="">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="orgname" Key="orgname" IsBound="false" HeaderText=""
                                                    Width="25%">
                                                    <Header Caption="<%$Resources:ControlText,gvBUName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkDay" Key="WorkDay" Format="yyyy/MM/dd"
                                                    IsBound="false" HeaderText="" Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvWorkDay %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WeekNo" Key="WeekNo" IsBound="false" HeaderText=""
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvWeekNo %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WeekDay" Key="WeekDay" IsBound="false" HeaderText=""
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvWeekDay %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="WorkFlag" Key="WorkFlag" IsBound="false" HeaderText=""
                                                    Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvWorkFlag %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="HoliDayFlag" Key="HoliDayFlag" IsBound="false"
                                                    HeaderText="" Width="10%">
                                                    <Header Caption="<%$Resources:ControlText,gvHoliDayFlag %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" HeaderText=""
                                                    Width="25%">
                                                    <Header Caption="<%$Resources:ControlText,gvRemark %>">
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
        </div>
        <div>
            <asp:Panel ID="PanelImport" Visible="false" runat="server" Width="100%">
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
                    <table border="1">
                        <tr>
                            <td class="td_label" width="100%" align="left" colspan="2">
                                <table>
                                    <tr>
                                        <td width="20%">
                                            <a href="/ExcelModel/BUCalendarSample.xls">&nbsp;<asp:Label ID="labelUploadText"
                                                runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </td>
                                        <td width="55%">
                                            <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                        </td>
                                        <td width="5%">
                                            <asp:Button ID="btnImportSave" runat="server" class="button_2" OnClick="btnImportSave_Click">
                                            </asp:Button>
                                        </td>
                                        <td width="5%">
                                            <asp:Button ID="btnExport" runat="server" class="button_2" OnClick="btnExport_Click">
                                            </asp:Button>
                                        </td>
                                        <td width="15%" align="left">
                                            <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" colspan="2">
                                            <asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="height: 25;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"">
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

                                                <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*53/100+"'>");</script>

                                                <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                        RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                        BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                        Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect"
                                                        AutoGenerateColumns="false">
                                                        <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                                            </BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="100%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="KQM_Import" Key="KQM_Import">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvErrorMsg %>">
                                                                    </Header>
                                                                    <CellStyle ForeColor="red">
                                                                    </CellStyle>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="depname" Key="depname" IsBound="false" Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvBUName %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="COSTCODE" Key="COSTCODE" IsBound="false" Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvCostCode %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkDay" HeaderText="WorkDay" IsBound="false"
                                                                    Key="WorkDay" Width="10%" Format="yyyy/MM/dd">
                                                                    <Header Caption="<%$Resources:ControlText,gvWorkDay %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WorkFlag" Key="WorkFlag" IsBound="false" Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvWorkFlag %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HolidayFlag" Key="HolidayFlag" IsBound="false"
                                                                    Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHolidayFlag %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Remark" HeaderText="Remark" IsBound="false"
                                                                    Key="Remark" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvRemark %>">
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
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>

<script type="text/javascript">
  function addReadOnly()
  {
	    igtbl_getElementById("txtBUCode").readOnly=true;
	    igtbl_getElementById("txtDPname").readOnly=true;			
	    if(igtbl_getElementById("hidOperate").value.length==0)
	    {
	        igtbl_getElementById("txtBUCode").readOnly=true;
		    igtbl_getElementById("txtWeekDay").readOnly=true;
	        igtbl_getElementById("txtWeekNo").readOnly=true;
	        igtbl_getElementById("txtWorkDay").readOnly=true;
	        igtbl_getElementById("txtRemark").readOnly=true;
            document.all("ddlWorkFlag").disabled=true;
            document.all("ddlHoliDayFlag").disabled=true;		
	    }
	    if (igtbl_getElementById("hidOperate").value=="Modify")
        {
            document.all("ddlWorkFlag").disabled=false;
            document.all("ddlWorkFlag").value=igtbl_getElementById("hidddlWorkFlag").value;
            document.all("ddlHoliDayFlag").disabled=false;
            document.all("ddlHoliDayFlag").value=igtbl_getElementById("hidddlHoliDayFlag").value;
	    }
	    $(".input_textBox_1").css("border-style", "none");
        $(".input_textBox_2").css("border-style", "none");
        $(".input_textBox_1").css("background-color", "#efefef");
        $(".input_textBox_2").css("background-color", "");
   }
   function removeValue()
   {
      $(".input_textBox_noborder_1").attr("value","");
      $(".input_textBox_noborder_2").attr("value","");
      $(".input_textBox_1").attr("value","");
      $(".input_textBox_2").attr("value","");
   }   
   function addbtnDisable()
   {
       $("#<%=btnCancel.ClientID%>").attr("disabled");
       $("#<%=btnSave.ClientID %>").attr("disabled");
       $("#<%=btnAdd.ClientID %>").attr("disabled","true");
       $("#<%=btnCondition.ClientID %>").attr("disabled","true");
       $("#<%=btnQuery.ClientID %>").attr("disabled","true");
       $("#<%=btnModify.ClientID %>").attr("disabled","true");
       $("#<%=btnDelete.ClientID %>").attr("disabled","true");
   }
   function Load()
   {
    // $(".img_hidden").hide();
    // $(".input_textBox").css("border-style", "none");
    // addReadOnly();
//      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
//      $("#<%=btnSave.ClientID %>").attr("disabled","true");
     return true;
   }
   function removereadonly()
   {
       $(".img_hidden").show();
       igtbl_getElementById("txtWorkDay").readOnly=false; 
       igtbl_getElementById("txtRemark").readOnly=false;
       igtbl_getElementById("ddlWorkFlag").disabled=false;
       igtbl_getElementById("ddlHoliDayFlag").disabled=false;
       $(".input_textBox_1").css("border-style", "solid");
       $(".input_textBox_2").css("border-style", "solid");
       $(".input_textBox_1").css("background-color", "White");
       $(".input_textBox_2").css("background-color", "White");
       
       $("#<%=txtBUCode.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtDPname.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtWorkDay.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=ddlWorkFlag.ClientID %>").css("background-color", "Cornsilk"); 
       $("#<%=ddlHoliDayFlag.ClientID %>").css("background-color", "Cornsilk");           
    }  
    function condition_click()
    {
         $("#<%=hidOperate.ClientID %>").val("Condition");
         removeValue();
         removereadonly();
         addbtnDisable();  
         $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
         $('#<%=btnQuery.ClientID %>').removeAttr("disabled");
         return false; 
    }
    function  add_click()
    {
       $("#<%=hidOperate.ClientID %>").val("Add");
       removeValue();
       removereadonly();
       addbtnDisable();
       $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled");
       return false;
    }
    function  modify_click()
    {  
       $("#<%=hidOperate.ClientID %>").val("Modify");
       var BUCode = $.trim($("#<%=txtBUCode.ClientID%>").val());
       if (BUCode=="")
       {alert(Message.AtLastOneChoose); $("#<%=hidOperate.ClientID %>").val("");return false;}
       removereadonly();
       addbtnDisable();
       $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled");
       return false;
    }
    function  cancel_click()
    {
       removeValue();
       $("#<%=hidOperate.ClientID %>").val("");
       $("#<%=btnCondition.ClientID%>").removeAttr("disabled");
       $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
       $("#<%=btnAdd.ClientID%>").removeAttr("disabled");
       $("#<%=btnModify.ClientID%>").removeAttr("disabled");
       $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
       $("#<%=btnCondition.ClientID%>").removeAttr("disabled");     
       $("#<%=btnCancel.ClientID %>").attr("disabled","true");
       $("#<%=btnSave.ClientID %>").attr("disabled","true");
       addReadOnly();
       $(".input_textBox").css("border-style", "none");
       $(".img_hidden").hide();
       return false;
    }
    function delete_click()
    {
        addReadOnly();
        $("#<%=hidOperate.ClientID %>").val("Delete");
        var BUCode = $.trim($("#<%=txtBUCode.ClientID%>").val());
        if (BUCode=="")
        {alert(Message.AtLastOneChoose);  $("#<%=hidOperate.ClientID %>").val("");return false;}
        else
        {if (confirm(Message.DeleteConfirm))
        {return true;}else{return false;}} 
    }
     function save_click()
     {
        var BUCode=$("#<%=txtBUCode.ClientID %>").val(); 
        var weekDay=$("#<%=txtWeekDay.ClientID%>").val(); 
        var weekNo=$("#<%=txtWeekNo.ClientID %>").val(); 
        var workDay=$("#<%=txtWorkDay.ClientID%>").val(); 
        var remark=$("#<%=txtRemark.ClientID%>").val(); 
        var workFlag=$("#<%=ddlWorkFlag.ClientID %>").val(); 
        var holiDayFlag=$("#<%=ddlHoliDayFlag.ClientID%>").val(); 
        var activeFlag=$("#<%=hidOperate.ClientID%>").val(); 
        var workDayAjax=$("#<%=hidWorkDay.ClientID%>").val(); 
        var bUCodeAjax=$("#<%=hidBUCode.ClientID%>").val(); 
        if(BUCode.length>0)
        {
           if(weekDay.length>0)
           {
              if(workFlag.length>0)
              {
                 if(holiDayFlag.length>0)
                 {
                    var result=0;
                    $.ajax({type: "post", url: "BUCalendar.aspx", dateType: "text", data: {ActiveFlag: activeFlag,WorkDay:workDay,HoliDayFlag:holiDayFlag },async:false,
                         success: function(msg) {
                                if (msg==2) {alert(Message.MustSameWithBU);result=0; } 
                                else{result=1;}}
                       }); 
                    if(result==0){return false;} 
                    else
                    {
                        var result=0;
                        $.ajax({type: "post", url: "BUCalendar.aspx", dateType: "text", data: {ActiveFlag: activeFlag,WorkDay:workDay,BUCodeO:BUCode,WorkDayAjax:workDayAjax,BUCodeAjax:bUCodeAjax },async:false,
                             success: function(msg) {
                                    if (msg==3) {alert(Message.NotOnlyOne);BUCode="";result=0; } 
                                    else{result=1;}}
                           }); 
                        if(result==0){return false;} 
                        else
                        {
                           {if (confirm(Message.SaveConfirm)){return true;}else{return false;}} 
                        }        
                    }        
                 }
                 else
                 {
                    var alertText=$("#<%=lblHoliDayFlag.ClientID %>").text()+Message.TextBoxNotNull;
                    alert(alertText);
                    return false;
                 }
              }
              else
              {
                 var alertText=$("#<%=lblWorkFlag.ClientID %>").text()+Message.TextBoxNotNull;
                 alert(alertText);
                 return false;
              }
           }
           else
           {
              var alertText=$("#<%=lblWorkDay.ClientID %>").text()+Message.TextBoxNotNull;
              alert(alertText);
              return false;
           }
        }
        else
        {
          var alertText=$("#<%=lblBUCode.ClientID %>").text()+Message.TextBoxNotNull;
          alert(alertText);
          return false;
        }    
     }
</script>

</html>
