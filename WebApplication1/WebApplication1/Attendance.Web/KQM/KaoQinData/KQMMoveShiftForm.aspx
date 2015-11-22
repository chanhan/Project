<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMMoveShiftForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.KaoQinData.KQMMoveShiftForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>彈性調班</title>
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <script type="text/javascript"><!--
        
       function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGrid_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGrid');
			var gRows = grid.Rows; /// <summary>
     
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
        function UpProgress()
		{
		    var filepath= $("#<%=FileUpload.ClientID %>").val();
		    $("#<%=hidFilePath.ClientID %>").val(filepath);
		    if (filepath!="")
		    {
		        if (filepath.indexOf('\\')>=0)
		        {
		           $("#<%=btnImportSave.ClientID %>").css("display","none");
			       $("#<%=btnImport.ClientID %>").attr("disabled","true");
			       $("#<%=btnExport.ClientID %>").attr("disabled","true");
			       $("#imgWaiting").css("display","");		
			       $("#<%=lblupload.ClientID %>").text(Message.uploading);		
			       return true;
		        }
		        else
		        {
		           $("#<%=lblupload.ClientID %>").text(Message.WrongFilePath);
		           return false;
		        }
		    }
		    else
		    {
		     $("#<%=lblupload.ClientID %>").text(Message.PathIsNull);
		     return false;
		    }
		}
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGrid_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
		   
			if(row != null)
			{	
				igtbl_getElementById("HiddenWorkNo").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				igtbl_getElementById("HiddenWorkDate").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue('yyyy/MM/dd');
				igtbl_getElementById("HiddenNoWorkDate").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue('yyyy/MM/dd');
			}
		}
		function DblClick(gridName, id)
		{
		    var ProcessFlag="Modify";
		    OpenEdit(ProcessFlag)
		    return 0;
		}
		function OpenEdit(ProcessFlag)
		{		
		    var EmployeeNo = igtbl_getElementById("HiddenWorkNo").value;
		    var WorkDate= igtbl_getElementById("HiddenWorkDate").value;
		    var NoWorkDate= igtbl_getElementById("HiddenNoWorkDate").value;
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {
		        var grid = igtbl_getGridById('UltraWebGrid');
			    var gRows = grid.Rows;
			    var Count=0;
			    for(i=0;i<gRows.length;i++)
			    {
				    if(gRows.getRow(i).getSelected())
				    {
				        Count+=1;				    
//    				    if(gRows.getRow(i).getCell(17).getValue()!="0")
//    				    {
//    			            alert(Message.checkapproveflag);
//    			           // alert("發生錯誤:checkapproveflag")
//    			            return false;
//   				         }
				    }
			    }			
			    if(Count==0)
			    {
			        alert(Message.AtLastOneChoose);
			        return false;
			    }
            }
            document.getElementById("iframeEdit").src="KQMMoveShiftEditForm.aspx?EmployeeNo="+EmployeeNo+"&WorkDate="+WorkDate+"&NoWorkDate="+NoWorkDate+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode+"&r="+Math.random();
            document.getElementById("topTable").style.display="none";
            document.getElementById("divEdit").style.display="";
            return false;
		}
       function CheckDelete()
       {
	        var grid = igtbl_getGridById('UltraWebGrid');
	        var gRows = grid.Rows;
	        var Count=0;
	        var State="";
	        for(i=0;i<gRows.length;i++)
	        {
		        if(igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked)
		        {
		             Count+=1;
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.AtLastOneChoose);
	            return false;
	        }
	        if(confirm(Message.DeleteConfirm))
	        {
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
       }
          
          
          function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="dept")
           {
           var url="../BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
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
       $("#td_show_1,#td_show_2,#div_img2").toggle(
            function(){
                $("#tr_display").hide();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#tr_display").show();
                $(".img2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 

    });
    
     $(function(){
       $("#tr_import").toggle(
            function(){
                $("#tr_importshow_1,#tr_importshow_2").hide();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#tr_importshow_1,#tr_importshow_2").show();
                $(".img3").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        ) 

    });
    
    function ButtonReset()
    {
    $("#<%=txtDepCode.ClientID %>").attr("value","");
    $("#<%=txtDepName.ClientID %>").attr("value","");
    $("#<%=txtWorkNo.ClientID %>").attr("value","");
    $("#<%=txtLocalName.ClientID %>").attr("value","");
    $("#<%=txtNoWorkDate1.ClientID %>").attr("value","");
    $("#<%=txtNoWorkDate2.ClientID %>").attr("value","");
    var myDate = new Date();
    var year=myDate.getFullYear();
    var month=myDate.getMonth()+1>9?(myDate.getMonth()+1):"0" + (myDate.getMonth()+1);
    var date=myDate.getDate()>9?myDate.getDate():"0"+myDate.getDate();
    $("#<%=txtWorkDate1.ClientID %>").val(year+"/"+month+"/01");
    $("#<%=txtWorkDate2.ClientID %>").val(year+"/"+month+"/"+date);
     return false;

    }   
        
       
	--></script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server">
    <input id="HiddenWorkDate" type="hidden" name="HiddenWorkDate" runat="server">
    <input id="HiddenNoWorkDate" type="hidden" name="HiddenNoWorkDate" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <asp:HiddenField ID="hidFilePath" runat="server" />
    <table cellspacing="1" id="topTable" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="top_table" cellspacing="0" cellpadding="1" width="100%" align="left">
                    <tr>
                        <td>
                            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                <tr style="cursor: hand">
                                    <td width="100%">
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
                                    </td>
                                </tr>
                            </table>
                            <div id="div_1">
                                <table class="table_data_area" style="width:100%">
                                    <tr>
                                        <td style="width:100%">
                                            <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area" style="table-layout: fixed">
                                                <tr class="tr_data_1">
                                                    <td class="td_label" width="7%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDepcode" runat="server" ResourceID="common.organise"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="20%">
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
                                                                    <asp:Image ID="imgDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                    </asp:Image>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="td_label" width="9%">
                                                        &nbsp; &nbsp;<asp:Label ID="lblWorkDate" runat="server" ResourceID="kqm.moveshift.workdate"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="24%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td id="KQDateFlag" width="50%">
                                                                    <asp:TextBox ID="txtWorkDate1" runat="server" Width="99%" CssClass="input_textBox_1"></asp:TextBox>
                                                                </td>
                                                                <td id="hiddenflag">
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtWorkDate2" runat="server" Width="99%" CssClass="input_textBox_1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="td_label" width="16%">
                                                        &nbsp; &nbsp;<asp:Label ID="lblNoWorkDate" runat="server" ResourceID="kqm.moveshift.noworkdate"></asp:Label>
                                                    </td>
                                                    <td class="td_input" width="24%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td id="Td1" width="50%">
                                                                    <asp:TextBox ID="txtNoWorkDate1" runat="server" Width="99%" CssClass="input_textBox_1"></asp:TextBox>
                                                                </td>
                                                                <td id="Td2">
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtNoWorkDate2" runat="server" Width="99%" CssClass="input_textBox_1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td style="width: 100px">
                                                        &nbsp;
                                                    </td>--%>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="lblEmployeeNo" runat="server" ResourceID="common.employeeno"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                        <asp:Label ID="lblName" runat="server" ResourceID="common.name"></asp:Label>
                                                    </td>
                                                    <td class="td_input">
                                                        <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="input_textBox_2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td class="td_label" colspan="8">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="button_1" OnClientClick="return ButtonReset()">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClientClick="return OpenEdit('Add')">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return OpenEdit('Modify')">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClick="btnDelete_Click"
                                                                            OnClientClick="return CheckDelete()"></asp:Button>
                                                                        <asp:Button ID="btnImport" runat="server" CssClass="button_1" OnClick="btnImport_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click">
                                                                        </asp:Button>
                                                                    </asp:Panel>
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
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelData" runat="server" Width="100%" Visible="true">
                                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                                    <tr style="cursor: hand">
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
                                                        <ess:aspnetpager id="pager" alwaysshow="true" runat="server" showfirstlast="false"
                                                            horizontalalign="Center" pagesize="50" pagingbuttontype="Image" width="300px"
                                                            imagepath="../../CSS/images/" buttonimagenameextension="n" buttonimageextension=".gif"
                                                            disabledbuttonimagenameextension="g" showmorebuttons="false" pagingbuttonspacing="10px"
                                                            buttonimagealign="left" showpageindex="false" showpageindexbox="Always" submitbuttonimageurl="../../CSS/Images_new/search01.gif"
                                                            onpagechanged="pager_PageChanged" showcustominfosection="Left" custominfohtml="<font size='2'>總記錄數：</font>%recordCount%">
                                                        </ess:aspnetpager>
                                                    </div>
                                                </td>
                                                <td style="width: 22px;" id="td_show_2">
                                                    <img id="div_img2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </tr>
                                    <tr id="tr_display">
                                        <td colspan="3">
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

                                                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*64/100+"'>");</script>

                                                        <igtbl:ultrawebgrid id="UltraWebGrid" runat="server" width="100%" height="100%">
                                                            <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
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
                                                                    DataKeyField="" BaseTableName="KQM_MoveShift" Key="KQM_MoveShift">
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
                                                                        <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="140px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftDepName %>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkNo %>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                                            Width="80px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftLocalName %>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkDate" Key="WorkDate" IsBound="false" Width="80px"
                                                                            Format="yyyy/MM/dd">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkDate %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkSTime" Key="WorkSTime" IsBound="false"
                                                                            Width="60px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkSTime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkETime" Key="WorkETime" IsBound="false"
                                                                            Width="60px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkETime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkDate" Key="NoWorkDate" IsBound="false"
                                                                            Format="yyyy/MM/dd" Width="110px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkDate %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkSTime" Key="NoWorkSTime" IsBound="false"
                                                                            Width="60px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkSTime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkETime" Key="NoWorkETime" IsBound="false"
                                                                            Width="60px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkETime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="TimeQty" Key="TimeQty" IsBound="false" Width="60px"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftTimeQty %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftRemark %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Update_User" Key="Update_User" IsBound="false"
                                                                            Width="80px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftUpdateUser %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Update_Date" Key="Update_Date" IsBound="false" Format="yyyy/MM/dd"
                                                                            Width="120px">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftUpdateDate %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:ultrawebgrid>

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
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelImport" runat="server" Width="100%" Visible="false">
                                <table class="table_title_area" cellspacing="0" cellpadding="0" width="100%">
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
                                    <tr id="tr_importshow_1">
                                        <td class="td_label" width="100%" align="left" colspan="2">
                                            <table>
                                                <tr>
                                                    <td width="20%">
                                                        <a href="../../ExcelModel/MoveShiftSample.xls">&nbsp;<asp:Label ID="lblUploadText"
                                                            runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </td>
                                                    <td width="40%">
                                                        <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                                    </td>
                                                    <td width="5%">
                                                        <asp:Button ID="btnImportSave" runat="server" CssClass="button_1" OnClick="btnImportSave_Click"
                                                            OnClientClick=" return UpProgress();"></asp:Button>
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
                                    <tr id="tr_importshow_2">
                                        <td colspan="2"">
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

                                                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*52/100+"'>");</script>

                                                        <igtbl:ultrawebgrid id="UltraWebGridImport" runat="server" width="100%" height="100%">
                                                            <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                                RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single" HeaderClickActionDefault="SortSingle"
                                                                BorderCollapseDefault="Separate" AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland"
                                                                AutoGenerateColumns="false" Name="UltraWebGridImport" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
                                                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="200">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftErrorMsg %>">
                                                                            </Header>
                                                                            <CellStyle ForeColor="red">
                                                                            </CellStyle>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="70">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkNo %>" Fixed="True">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkDate" Key="WorkDate" IsBound="false" Width="80">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkDate %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkSTime" Key="WorkSTime" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkSTime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="WorkETime" Key="WorkETime" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftWorkETime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkDate" Key="NoWorkDate" IsBound="false"
                                                                            Width="100">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkDate %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkSTime" Key="NoWorkSTime" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkSTime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="NoWorkETime" Key="NoWorkETime" IsBound="false"
                                                                            Width="60">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftNoWorkETime %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="TimeQty" Key="TimeQty" IsBound="false" Width="60"
                                                                            Format="0.0">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftTimeQty %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150">
                                                                            <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftRemark %>">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:ultrawebgrid>

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
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript">document.write("<div id='divEdit'style='display:none;height:"+document.body.clientHeight*84/100+"'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="no" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    <%--<igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter" runat="server"
            OnCellExported="UltraWebGridExcelExporter_CellExported" OnHeaderCellExported="UltraWebGridExcelExporter_HeaderCellExported">
        </igtblexp:UltraWebGridExcelExporter>--%>
    </form>

    <script type="text/javascript"><!--
     
         document.getElementById("txtWorkNo").focus();
    document.getElementById("txtWorkNo").select();
        
       
        
	--></script>

</body>
</html>
