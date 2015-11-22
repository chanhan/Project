﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpSupportInForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.Support.HrmEmpSupportInForm" %>

<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EmpSupportInForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .b
        {
            display: none;
        }
    </style>

    <script type="text/javascript"><!--
     $(function(){
        $("#btnReturn").addClass("b");
        var importflag=$("#<%=ImportFlag.ClientID %>").val();
        if(importflag=="Import")
        {
          $("#btnQuery,#btnReset,#btnAdd,#btnModify,#btnDelete").attr("disabled","true");
          $("#btnImport").addClass("b");
          $("#btnReturn").removeClass("b");
          document.getElementById("PanelData").style.display="none";
		  document.getElementById("PanelImport").style.display="";
        }
        
        $("#<%=btnImport.ClientID %>").click(function(){
        $("#btnQuery,#btnReset,#btnAdd,#btnModify,#btnDelete").attr("disabled","true");
        $("#btnImport").addClass("b");
        $("#btnReturn").removeClass("b");
        return false;
        })
        
        $("#<%=btnReturn.ClientID %>").click(function(){
        $("#btnQuery,#btnReset,#btnAdd,#btnModify,#btnDelete").removeAttr("disabled");
        $("#btnImport").removeClass("b");
        $("#btnReturn").addClass("b");
        $("#ImportFlag").val(null);
        return false;
        })
        
         $("#tr_edit").toggle(function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_select").hide()},function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_select").show()});
        
        $("#div_img_2,#td_show_1,#td_show_2").toggle(function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_showdata").hide()},function(){
        $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_showdata").show()});
        
        $("#tr_editimport").toggle(function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#tr_show_1,#tr_show_2,#tr_show_3").hide()},function(){
        $("#div_img_3").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#tr_show_1,#tr_show_2,#tr_show_3").show()});
        
        })
        
        function setSelector()
        {
        var modulecode=$("#<%=ModuleCode.ClientID %>").val();
        var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+modulecode;
        var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
        var info=window.showModalDialog(url,null,fe);
        if(info)
        {
         $("#txtDepCode").val(info.codeList);
        $("#txtDepName").val(info.nameList);
        }
        return false;
        }
        
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridSupportIn_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridSupportIn');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridSupportIn_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridSupportIn_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
         function UpProgress()
		{
			document.getElementById("btnImportSave").style.display="none";
			document.getElementById("btnImport").disabled="disabled";
			document.getElementById("btnExport").disabled="disabled";			
			document.getElementById("imgWaiting").style.display="";
		
		}

		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridSupportIn_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(row != null)
			{	
				document.getElementById("HiddenWorkNo").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				document.getElementById("HiddenSupportOrder").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
			}
		}
		function DblClick(gridName, id)//雙擊修改
		{
		    var ProcessFlag="Modify";
		    OpenEdit(ProcessFlag)
		    return 0;
		}
		function OpenEdit(ProcessFlag)//彈出新增或修改頁面
		{		
		    var EmployeeNo = igtbl_getElementById("HiddenWorkNo").value;
		    var SupportOrder= igtbl_getElementById("HiddenSupportOrder").value;
		  
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {
		        var grid = igtbl_getGridById('UltraWebGridSupportIn');
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
			        alert(Message.NoItemSelected);
			        return false;
			    }
            }
            document.getElementById("iframeEdit").src="HrmEmpSupportInEditForm.aspx?EmployeeNo="+EmployeeNo+"&SupportOrder="+SupportOrder+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
            document.getElementById("topTable").style.display="none";
            document.getElementById("PanelData").style.display="none";
            document.getElementById("divEdit").style.display="";
            document.getElementById("div_2").style.display="none";
            return false;
            }
            
            function Import()
		{
		  document.getElementById("PanelData").style.display="none";
		  document.getElementById("PanelImport").style.display="";
		  return false;
		}
		
		function Return()
		{
		  document.getElementById("PanelData").style.display="";
		  document.getElementById("PanelImport").style.display="none";
		  return false;
		}
		
		function valDate(M,D,Y){   
      Months=new Array(31,28,31,30,31,30,31,31,30,31,30,31);   
      Leap=false;   
      if((Y%4==0)&&((Y%100!=0)||(Y%400==0)))   
      Leap=true;   
      if((D<1)||(D>31)||(M < 1)||(M>12)||(Y < 0))   
      return(false);   
      if((D> Months[M-1])&&!((M==2)&&(D>28)))   
      return(false);   
      if(!(Leap)&&(M==2)&&(D>28))   
      return(false);   
      if((Leap)&&(M==2)&&(D>29))   
      return(false);   
      };   

    function checkdata()
    {
    var StartDatefrom=document.getElementById("txtStartDate1").value;
    var StartDateto=document.getElementById("txtStartDate2").value;
    var PrepEndDatefrom=document.getElementById("txtPrepEndDate1").value;
    var PrepEndDateto=document.getElementById("txtPrepEndDate2").value;
    var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
    if(StartDatefrom!=""&&StartDatefrom!=null)
    {
    if(!check.test(StartDatefrom))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(StartDatefrom)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(StartDateto!=""&&StartDateto!=null)
    {
    if(!check.test(StartDateto))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(StartDateto)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(PrepEndDatefrom!=""&&PrepEndDatefrom!=null)
    {
    if(!check.test(PrepEndDatefrom))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(PrepEndDatefrom)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(PrepEndDateto!=""&&PrepEndDateto!=null)
    {
    if(!check.test(PrepEndDateto))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(PrepEndDateto)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    }

    function   formatDate(date){   
    cDate   =   date;   
    dSize   =   cDate.length;   
    sCount=   0;   


    idxBarI   =   cDate.indexOf( "/");   
    idxBarII=   cDate.lastIndexOf( "/");   
    strY   =   cDate.substring(0,idxBarI);  
    strM   =   cDate.substring(idxBarI+1,idxBarII);   
    strD   =   cDate.substring(idxBarII+1,dSize);   
    ok   =   valDate(strM,   strD,   strY);   
    if(ok==false){          
    return(false);   
    };   
    } 
    
    function DeleteConfirm()
    {
    var grid = igtbl_getGridById('UltraWebGridSupportIn');
	        var gRows = grid.Rows;
	        var Count=0;
	        var State="";
	        for(i=0;i<gRows.length;i++)
	        {
		        if(igtbl_getElementById("UltraWebGridSupportIn_ci_0_0_"+i+"_CheckBoxCell").checked)
		        {
		             Count+=1;
		        }
	        }			
	        if(Count==0)
	        {
	            alert(Message.NoItemSelected);
	            return false;
	        }
    return confirm(Message.RulesDeleteConfirm);
    }
	--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenWorkNo" runat="server" name="HiddenWorkNo" type="hidden" />
    <input id="HiddenSupportOrder" runat="server" name="HiddenSupportOrder" type="hidden" />
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server" />
    <asp:HiddenField ID="ImportFlag" runat="server" />
    <div id="topTable">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%; cursor: hand;" id="tr_edit">
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
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_select" style="width: 100%">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <asp:Panel ID="pnlContent" runat="server">
                                        <table class="table_data_area">
                                            <tr class="tr_data_1">
                                                <td style="width: 9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblSupportDepName" runat="server">Department:</asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand" onclick="setSelector();">
                                                                <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 8%">
                                                    &nbsp;
                                                    <asp:Label ID="lblReGetStatus" runat="server">Status:</asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_empno" runat="server">EmployeeNo:</asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server">Name:</asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblFromdate" runat="server">StartDate:</asp:Label>
                                                </td>
                                                <td width="32%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtStartDate1" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtStartDate2" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               <%-- <td style="width: 100px">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>--%>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td style="width: 9%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLevelCode" runat="server">LevelCode:</asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <cc1:DropDownCheckList ID="ddlLevelName" Width="300" RepeatColumns="3" DropImageSrc="../../CSS/Images/expand.gif"
                                                        TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td style="width: 9%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_manger" runat="server">ManagerCode:</asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <cc1:DropDownCheckList ID="ddlManagerName" Width="300" RepeatColumns="3" DropImageSrc="../../CSS/Images/expand.gif"
                                                        TextWhenNoneChecked="" DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td style="width: 10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblPrepEndDate" runat="server">PrepEndDate:</asp:Label>
                                                </td>
                                                <td style="width: 32%">
                                                    <table cellspacing="0" cellpadding="0" width="98%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtPrepEndDate1" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtPrepEndDate2" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td style="width: 100px">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClientClick="return checkdata();"
                                            OnClick="btnQuery_Click"></asp:Button>
                                        <asp:Button ID="btnReset" runat="server" CssClass="button_1" OnClick="btnReset_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="button_1" OnClientClick="return OpenEdit('Add')">
                                        </asp:Button>
                                        <asp:Button ID="btnModify" runat="server" CssClass="button_1" OnClientClick="return OpenEdit('Modify')">
                                        </asp:Button>
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClientClick="return DeleteConfirm();"
                                            OnClick="btnDelete_Click"></asp:Button>
                                        <asp:Button ID="btnImport" runat="server" CssClass="button_1" OnClientClick="return Import();">
                                        </asp:Button>
                                        <asp:Button ID="btnReturn" runat="server" CssClass="button_1" OnClientClick="return Return();">
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
        </div>
    </div>
    <div style="width: 100%;" id="PanelData">
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
                            ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                        </ess:AspNetPager>
                    </div>
                </td>
                <td style="width: 22px; cursor: hand;" id="td_show_2">
                    <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*58/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridSupportIn" runat="server" Width="100%" Height="100%">
                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridSupportIn" TableLayout="Fixed"
                                AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                    CssClass="tr_header">
                                    <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                    </BorderDetails>
                                </HeaderStyleDefault>
                                <FrameStyle Width="100%" Height="100%">
                                </FrameStyle>
                                <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                    AfterSelectChangeHandler="AfterSelectChange" DblClickHandler="DblClick"></ClientSideEvents>
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
                                    DataKeyField="" BaseTableName="GDS_ATT_EmpSupportIn_v" Key="GDS_ATT_EmpSupportIn_v">
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
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                            Key="WorkNo" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" HeaderText="LocalName" IsBound="false"
                                            Key="LocalName" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SexName" HeaderText="SexName" IsBound="false"
                                            Key="SexName" Width="5%">
                                            <Header Caption="<%$Resources:ControlText,gvSex %>" Fixed="true">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SupportOrder" HeaderText="SupportOrder" IsBound="false"
                                            Key="SupportOrder" Hidden="true">
                                            <Header Caption="<%$Resources:ControlText,gvSupportOrder %>">
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="3" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SupportDeptName" HeaderText="SupportDeptName"
                                            IsBound="false" Key="SupportDeptName" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvSupportDeptName %>">
                                                <RowLayoutColumnInfo OriginX="4" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="4" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StartDate" Format="yyyy/MM/dd" HeaderText="StartDate"
                                            IsBound="false" Key="StartDate" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvEmpStartDate %>">
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="5" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PrepEndDate" Format="yyyy/MM/dd" HeaderText="PrepEndDate"
                                            IsBound="false" Key="PrepEndDate" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvPrepEndDate %>">
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="6" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EndDate" Format="yyyy/MM/dd" HeaderText="EndDate"
                                            IsBound="false" Key="EndDate" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvRealEndDate %>">
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="7" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StateName" HeaderText="StateName" IsBound="false"
                                            Key="State" Width="10%">
                                            <Header Caption="<%$Resources:ControlText,gvStateName %>">
                                                <RowLayoutColumnInfo OriginX="8" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="8" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Remark" HeaderText="Remark" IsBound="false"
                                            Key="Remark" Width="25%">
                                            <Header Caption="<%$Resources:ControlText,gvRemark %>">
                                                <RowLayoutColumnInfo OriginX="9" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="9" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
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
    <div style="display: none" id="PanelImport">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%; cursor: hand;" id="tr_editimport">
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
                        <div id="div_editimg">
                            <img id="div_img_3" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
                <tr id="tr_show_1">
                    <td width="100%" align="left" colspan="2">
                        <a href="../../ExcelModel/EmpSupportInSample.xls">&nbsp;<%=Resources.ControlText.templateddown%>
                        </a>&nbsp;
                        <asp:FileUpload ID="FileUpload" runat="server" Width="30%" />
                        <asp:Button ID="btnImportSave" CssClass="button_1" runat="server" Text="ImportSave"
                            OnClick="btnImportSave_Click" />
                        <img id="imgWaiting" src="/images/clocks.gif" border="0" style="display: none; height: 20px;" />
                        <asp:Label ID="lblUpload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblUploadMsg" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr id="tr_show_2">
                    <td align="left" colspan="2" style="height: 25;">
                        &nbsp;<%=Resources.ControlText.importremark%>
                    </td>
                </tr>
                <tr id="tr_show_3">
                    <td colspan="2"">

                        <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*54/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
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
                                <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler">
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
                                <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                    DataKeyField="" BaseTableName="HRM_Import" Key="HRM_Import">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" HeaderText="ErrorMsg" IsBound="false"
                                            Key="ErrorMsg" Width="280px">
                                            <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg %>">
                                            </Header>
                                            <CellStyle ForeColor="Red">
                                            </CellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                            Key="WorkNo" Width="80px">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CostCode" HeaderText="CostCode" IsBound="false"
                                            Key="CostCode" Width="80px">
                                            <Header Caption="<%$Resources:ControlText,gvCostCode %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SUPPORTDEPTNAME" HeaderText="SUPPORTDEPTNAME"
                                            IsBound="false" Key="DepName" Width="80px">
                                            <Header Caption="<%$Resources:ControlText,gvSupportDeptName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StartDate" Format="yyyy/MM/dd" HeaderText="StartDate"
                                            IsBound="false" Key="StartDate" Width="100px">
                                            <Header Caption="<%$Resources:ControlText,gvEmpStartDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PrepEndDate" Format="yyyy/MM/dd" HeaderText="PrepEndDate"
                                            IsBound="false" Key="PrepEndDate" Width="100px">
                                            <Header Caption="<%$Resources:ControlText,gvPrepEndDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="EndDate" Format="yyyy/MM/dd" HeaderText="EndDate"
                                            IsBound="false" Key="EndDate" Width="120px">
                                            <Header Caption="<%$Resources:ControlText,gvEndDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Remark" HeaderText="Remark" IsBound="false"
                                            Key="Remark" Width="120px">
                                            <Header Caption="<%$Resources:ControlText,gvRemark %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                        </igtbl:UltraWebGrid>

                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>

                    </td>
                </tr>
            </table>
        </div>
    </div>

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

    </form>
</body>
</html>