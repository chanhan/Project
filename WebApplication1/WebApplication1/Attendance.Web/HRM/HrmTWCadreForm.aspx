<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmTWCadreForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.HrmTWCadreForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HrmTWCadreForm</title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/DropDownCheckList.js" type="text/javascript"></script>

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
    };   
        
        function CheckDate()
       {
          var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
          var JoinDateFrom= $("#<%=txtJoinDateFrom.ClientID%>").val();
          var JoinDateTo=$("#<%=txtJoinDateTo.ClientID %>").val();
          var LeaveDateFrom= $("#<%=txtLeaveDateFrom.ClientID%>").val();
          var LeaveDateTo=$("#<%=txtLeaveDateTo.ClientID %>").val();
          if (JoinDateFrom!=null&&JoinDateFrom!="")
          {
             if(!check.test(JoinDateFrom))
             {
               alert(Message.WrongDate);
               return false;
             }
             if(formatDate(JoinDateFrom)==false)
             {
               alert(Message.WrongDate);
               return false;
             }
          }
          if (JoinDateTo!=null&&JoinDateTo!="")
          {
             if(!check.test(JoinDateTo))
             {
               alert(Message.WrongDate);
               return false;
             }
             if(formatDate(JoinDateTo)==false)
             {
               alert(Message.WrongDate);
               return false;
             }
          }
          if (LeaveDateFrom!=null&&LeaveDateFrom!="")
          {
             if(!check.test(LeaveDateFrom))
             {
               alert(Message.WrongDate);
               return false;
             }
             if(formatDate(LeaveDateFrom)==false)
             {
               alert(Message.WrongDate);
               return false;
             }
          }
          if (LeaveDateTo!=null&&LeaveDateTo!="")
          {
             if(!check.test(LeaveDateTo))
             {
               alert(Message.WrongDate);
               return false;
             }
             if(formatDate(LeaveDateTo)==false)
             {
               alert(Message.WrongDate);
               return false;
             }
          }
          if((FromDate!=null&&FromDate!="")&&(ToDate!=null&&ToDate!=""))
          {
            if(ToDate<FromDate)
            {
               alert(Message.ToLaterThanFrom);
               return false;
            }
          }
          return true;
       }
      
        
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
			var chk=document.getElementById("UltraWebGrid_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGrid');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGrid_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
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
				document.getElementById("HiddenWorkNo").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
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
		    var WorkNo = igtbl_getElementById("HiddenWorkNo").value;
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
				    }
			    }			
			    if(Count==0)
			    {
			        return false;
			    }
            }
            document.getElementById("iframeEdit").src="HrmTWCadreEditForm.aspx?WorkNo="+WorkNo+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
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
		
		function DeleteConfirm()
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
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server" />
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
                                                <td width="10%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDeptDept" runat="server">Department:</asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand" onclick="setSelector();">
                                                                <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="8%">
                                                    &nbsp;
                                                    <asp:Label ID="lblReGetWorkNo" runat="server">WorkNo:</asp:Label>
                                                </td>
                                                <td width="18%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="8%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server">LocalName:</asp:Label>
                                                </td>
                                                <td width="27%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="8%">
                                                    &nbsp;
                                                    <asp:Label ID="lblSex" runat="server" Text="Sex"></asp:Label>
                                                </td>
                                                <td width="30%" style="height: 31px">
                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="100%">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="<%$Resources:ControlText,FitSexNameMan %>"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="<%$Resources:ControlText,FitSexNameWoman %>"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <%--<td style="width: 100px">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>--%>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblIdentityNo" runat="server" Text="IdentityNo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIdentityNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblLevelCode" runat="server" Text="LevelCode"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="DropDownCheckListLevelCode" Width="300" RepeatColumns="3"
                                                        CssClass="input_textBox" DropImageSrc="../../CSS/Images/expand.gif" TextWhenNoneChecked=""
                                                        DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblManager" runat="server" Text="Manager"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="DropDownCheckListManager" Width="200" RepeatColumns="1"
                                                        CssClass="input_textBox" DropImageSrc="../../CSS/Images/expand.gif" TextWhenNoneChecked=""
                                                        DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <%--<td style="width: 90px">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>--%>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc1:DropDownCheckList ID="DropDownCheckListStatus" Width="200" RepeatColumns="1"
                                                        CssClass="input_textBox" DropImageSrc="../../CSS/Images/expand.gif" TextWhenNoneChecked=""
                                                        DisplayTextWidth="300" ClientCodeLocation="../../JavaScript/DropDownCheckList.js"
                                                        runat="server" CheckListCssStyle="background-image: url(../../CSS/images/inputbg.bmp);height: 144px;overflow: scroll;">
                                                    </cc1:DropDownCheckList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblJoinDate" runat="server" Text="JoinDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td class="td_label" width="50%">
                                                                <asp:TextBox ID="txtJoinDateFrom" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                &nbsp;&nbsp;~&nbsp;
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtJoinDateTo" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLeaveDate" runat="server" Text="LeaveDate"></asp:Label>
                                                </td>
                                                <td style="cursor: hand" >
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtLeaveDateFrom" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                &nbsp;&nbsp;~&nbsp;
                                                            </td>
                                                            <td width="50%">
                                                                <asp:TextBox ID="txtLeaveDateTo" runat="server" Width="99%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td style="width: 90px">
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
                                        <asp:Button ID="btnQuery" runat="server" CssClass="button_1" OnClick="btnQuery_Click">
                                        </asp:Button>
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
                            ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                            DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                            ShowPageIndex="false" ShowPageIndexBox="Always" ShowMoreButtons="false" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
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

                        <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*54/100+"'>");</script>

                        <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%">
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
                                    DataKeyField="" BaseTableName="gds_att_twcadre_v" Key="gds_att_twcadre_v"">
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
                                        <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="90">
                                            <Header Caption="<%$Resources:ControlText,gvOrgName %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="SexName" Key="SexName" IsBound="false" Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvSex %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="IdentityNo" Key="IdentityNo" IsBound="false"
                                            Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvIdentityNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ByName" Key="ByName" IsBound="false" Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvByName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LevelName" Key="LevelName" IsBound="false"
                                            Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvLevel %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ManagerName" Key="ManagerName" IsBound="false"
                                            Width="75">
                                            <Header Caption="<%$Resources:ControlText,gvManager %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Extension" Key="Extension" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvExtension %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Notes" Key="Notes" IsBound="false" Width="120">
                                            <Header Caption="<%$Resources:ControlText,gvHeadNotes %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="JoinDate" Key="JoinDate" Format="yyyy/MM/dd"
                                            IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvJoinDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LeaveDate" Key="LeaveDate" Format="yyyy/MM/dd"
                                            IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvLeaveDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="StatusName" Key="StatusName" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvStatus %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CardNo" Key="CardNo" IsBound="false" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvCardNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="IsKaoQin" Key="IsKaoQin" IsBound="false" Width="50">
                                            <Header Caption="<%$Resources:ControlText,gvIsKaoQin %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Update_User" Key="Update_User" IsBound="false"
                                            Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvModifier %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Update_Date" Key="Update_Date" Format="yyyy/MM/dd"
                                            IsBound="false" Width="110">
                                            <Header Caption="<%$Resources:ControlText,gvModifyDate %>">
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
                    <td class="td_label" width="100%" align="left" colspan="2">
                        <a href="../ExcelModel/HRMTWCadreSample.xls">&nbsp;<%=Resources.ControlText.templateddown%>
                        </a>&nbsp;
                        <asp:FileUpload ID="FileUpload" runat="server" Width="30%" />
                        <asp:Button ID="btnImportSave" CssClass="button_1" runat="server" Text="ImportSave"
                            OnClick="btnImportSave_Click" />
                        <img id="imgWaiting" src="<%=sAppPath%>/images/clocks.gif" border="0" style="display: none;
                            height: 20px;" />
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

                        <script language="javascript">document.write("<DIV id='div_3'  style='height:"+document.body.clientHeight*46/100+"'>");</script>

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
                                    DataKeyField="" BaseTableName="HRM_Import" Key="HRM_Import">
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="280">
                                            <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg %>" Fixed="true">
                                            </Header>
                                            <CellStyle ForeColor="red">
                                            </CellStyle>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="true">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Sex" Key="Sex" IsBound="false" Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvSex %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="IdentityNo" Key="IdentityNo" IsBound="false"
                                            Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvIdentityNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ByName" Key="ByName" IsBound="false" Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvByName %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LevelName" Key="LevelName" IsBound="false"
                                            Width="70">
                                            <Header Caption="<%$Resources:ControlText,gvLevel %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="90">
                                            <Header Caption="<%$Resources:ControlText,gvHeadDepCode %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="ManagerName" Key="ManagerName" IsBound="false"
                                            Width="75">
                                            <Header Caption="<%$Resources:ControlText,gvManager %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Extension" Key="Extension" IsBound="false"
                                            Width="60">
                                            <Header Caption="<%$Resources:ControlText,gvExtension %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Notes" Key="Notes" IsBound="false" Width="120">
                                            <Header Caption="<%$Resources:ControlText,gvHeadNotes %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="JoinDate" Key="JoinDate" IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvJoinDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="Status" Key="Status" IsBound="false" Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvStatus %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="LeaveDate" Key="LeaveDate" IsBound="false"
                                            Width="80">
                                            <Header Caption="<%$Resources:ControlText,gvLeaveDate %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="CardNo" Key="CardNo" IsBound="false" Width="100">
                                            <Header Caption="<%$Resources:ControlText,gvCardNo %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="IsKaoQin" Key="IsKaoQin" IsBound="false" Width="50">
                                            <Header Caption="<%$Resources:ControlText,gvIsKaoQin %>">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
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

    <table cellspacing="1" cellpadding="0" width="100%" height="100%" align="center">
        <tr>
            <td>
                <iframe id="iframeEdit" class="top_table" src="" width="100%" height="100%" frameborder="0"
                    scrolling="auto" style="border: 0"></iframe>
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">document.write("</div>");</script>

    </form>
</body>
</html>
