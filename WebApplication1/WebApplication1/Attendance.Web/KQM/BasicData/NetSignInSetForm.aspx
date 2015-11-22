<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetSignInSetForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.NetSignInSetForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KQMNetSignInSetForm</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .a
        {
            border: 0;
        }
        .b
        {
            display: none;
        }
    </style>

    <script type="text/javascript"><!--
        $(function(){
        $("#btnReturn,#innerTable_2,#bottomTable").addClass("b");
        $("#txtEditLocalName,#txtEditDepName").addClass("a").attr("readonly","true").val(null);
        var importflag=$("#<%=ImportFlag.ClientID %>").val();
        var hidOperate=$("#<%=hidOperate.ClientID %>").val();
        if(importflag=="Import")
        {
          $("#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnReset").attr("disabled","disabled");
          $("#btnImport,#innerTable_1").addClass("b");
          $("#btnReturn,#innerTable_2").removeClass("b");
        }
        if(hidOperate=="Add")
        {
        $("#topTable,#innerTable_1,#innerTable_2").addClass("b");
        $("#bottomTable").removeClass("b");
        $("#txtStartDate,#txtEndDate").val(null);
        $("#txtEditWorkNo").focus();
        }
        
        $("#<%=btnImport.ClientID %>").click(function(){
        $("#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnReset").attr("disabled","disabled");
        $("#btnImport,#innerTable_1").addClass("b");
        $("#btnReturn,#innerTable_2").removeClass("b");
        return false;
        })
        
        $("#<%=btnReturn.ClientID %>").click(function(){
        $("#btnQuery,#btnAdd,#btnModify,#btnDelete,#btnReset").removeAttr("disabled");
        $("#btnImport,#innerTable_1").removeClass("b");
        $("#btnReturn,#innerTable_2").addClass("b");
        $("#<%=ImportFlag.ClientID %>").val(null);
        return false;
        })
        
        $("#<%=btnEditReturn.ClientID %>").click(function(){
        $("#topTable,#innerTable_1").removeClass("b");
        $("#bottomTable").addClass("b");
        $("#<%=hidOperate.ClientID %>").val(null);
        return false;
        })
        
        $("#<%=btnAdd.ClientID %>").click(function(){
        $("#topTable,#innerTable_1,#innerTable_2").addClass("b");
        $("#bottomTable").removeClass("b");
        $("#<%=hidOperate.ClientID %>").val("Add");
        $("#txtEditWorkNo,#txtEditLocalName,#txtEditDepName,#txtStartDate,#txtEndDate").val(null);
        $("#txtEditWorkNo").focus();
        return false;
        })
        
        $("#<%=btnModify.ClientID %>").click(function(){
        $("#topTable,#innerTable_1,#innerTable_2").addClass("b");
        $("#bottomTable").removeClass("b");
        $("#<%=hidOperate.ClientID %>").val("Modify");
        $("#txtEditWorkNo").addClass("a").attr("readonly","true");
        return false;
        })
        
        
        $("#<%=txtEditWorkNo.ClientID %>").blur(function()
        {
        var WorkNo = $.trim($("#<%=txtEditWorkNo.ClientID %>").val());
        if ($.trim($("#<%=txtEditWorkNo.ClientID %>").val())) 
        {  
         var processFlag = "check";
         var modulecode=$("#<%=ModuleCode.ClientID %>").val();
         $.ajax({
                type: "post", url: "NetSignInSetForm.aspx",dataType: "json",
                data: { WorkNo: WorkNo,ProcessFlag: processFlag,ModuleCode:modulecode },
                success: function(item) {
                if(item==null)
                {
                alert(Message.EmpNotExist);
                }
                else
                {
                $.each(item, function(k, v) { $(":text[id$='txtEdit" + k + "']").val(v); });
                }
                }
                });
        } 
        })
        
        $("#<%=btnReset.ClientID %>").click(function(){
        $("#txtWorkNo,#txtLocalname,#txtDepName").val(null);
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
        
        $("#tb_show_1").toggle(function(){
        $("#div_img_4").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_show_4").hide()},function(){
        $("#div_img_4").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_show_4").show()});
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

        function checkdate()
        {
        var workno=document.getElementById("txtEditWorkNo").value;
        var date1=document.getElementById("txtStartDate").value;
        var date2=document.getElementById("txtEndDate").value;
        var check=/^\d{4}\/\d{2}\/\d{2}$/;
        if(workno==""||workno==null)
        {
        alert(Message.WorkNoNotNull);
        return false;
        }
        
        if(date1!=""&&date1!=null)
    {
    if(!check.test(date1))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(date1)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    else
    {
    alert(Message.StartDateNotNull);
    return false;
    }
    
    if(date2!=""&&date2!=null)
    {
    if(!check.test(date2))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(date2)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    else
    {
    alert(Message.EndDateNotNull);
    return false;
    }
    
    if(date2!=""&&date2!=null&&date1!=""&&date1!=null)
    {
    if(date1>date2)
    {
    alert(Message.EndLaterThanStart);
    return false;
    }
    }
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
			  var startandend=row.getCell(4).getValue();
			  var startend=startandend.split('~');
			  var startdate=startend[0];
			  var enddate=startend[1];
					igtbl_getElementById("txtEditWorkNo").value=row.getCell(1).getValue();
					igtbl_getElementById("txtEditLocalName").value=row.getCell(2).getValue();
					igtbl_getElementById("txtEditDepName").value=row.getCell(3).getValue();
					igtbl_getElementById("txtStartDate").value=startdate;
					igtbl_getElementById("txtEndDate").value=enddate;
					$("#<%=StartDate.ClientID %>").val(startdate);
					$("#<%=EndDate.ClientID %>").val(enddate);
			        igtbl_getElementById("HiddenWorkNo").value=row.getCellFromKey("WorkNo").getValue()==null?"":row.getCellFromKey("WorkNo").getValue();	
				
			}
		}
		function DblClick(gridName, id)
		{
		 $("#topTable,#innerTable_1,#innerTable_2").addClass("b");
        $("#bottomTable").removeClass("b");
        $("#<%=hidOperate.ClientID %>").val("Modify");
        $("#txtEditWorkNo").addClass("a").attr("readonly","true");
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
    <div>
        <asp:HiddenField ID="hidOperate" runat="server" />
        <asp:HiddenField ID="ImportFlag" runat="server" />
        <asp:HiddenField ID="ModuleCode" runat="server" />
        <asp:HiddenField ID="StartDate" runat="server" />
        <asp:HiddenField ID="EndDate" runat="server" />
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
                                        <asp:Label ID="lblEditArea" runat="server"></asp:Label>
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
                                                    <td width="8%">
                                                        &nbsp;
                                                        <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>
                                                    </td>
                                                    <td width="17%">
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                </td>
                                                                <td width="90%">
                                                                    <asp:TextBox ID="txtDepName" runat="server" Width="100%" Style=""></asp:TextBox>
                                                                </td>
                                                                <td width="10%" style="cursor: hand" onclick="setSelector();">
                                                                    <img id="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="8%">
                                                        &nbsp;
                                                        <asp:Label ID="lblFromPersoncode" runat="server" Text="Personcode"></asp:Label>
                                                    </td>
                                                    <td width="17%">
                                                        <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="8%">
                                                        &nbsp;
                                                        <asp:Label ID="lblFromPersonName" runat="server" Text="PersonName"></asp:Label>
                                                    </td>
                                                    <td width="17%">
                                                        <asp:TextBox ID="txtLocalname" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
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
                                            <asp:Button ID="btnReset" runat="server" CssClass="button_1"></asp:Button>
                                            <asp:Button ID="btnAdd" runat="server" CssClass="button_1"></asp:Button>
                                            <asp:Button ID="btnModify" runat="server" CssClass="button_1"></asp:Button>
                                            <asp:Button ID="btnDelete" runat="server" CssClass="button_1" OnClientClick="return DeleteConfirm();"
                                                OnClick="btnDelete_Click"></asp:Button>
                                            <asp:Button ID="btnImport" runat="server" CssClass="button_1"></asp:Button>
                                            <asp:Button ID="btnReturn" runat="server" CssClass="button_1"></asp:Button>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="width: 100%" id="innerTable_1">
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
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
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

                            <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*68/100+"'>");</script>

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
                                        DataKeyField="" BaseTableName="gds_att_signemployee" Key="gds_att_signemployee">
                                        <Columns>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="30px" Key="CheckBoxAll">
                                                <CellTemplate>
                                                    <asp:CheckBox ID="CheckBoxCell" runat="server"></asp:CheckBox>
                                                </CellTemplate>
                                                <HeaderTemplate>
                                                    <input id="CheckBoxAll" onclick="javascript:CheckAll();" runat="server" type="checkbox" />
                                                </HeaderTemplate>
                                                <Header Caption="CheckBox" ClickAction="Select" Fixed="True">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="13%">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Localname" Key="Localname" IsBound="false"
                                                Width="13%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Depname" Key="Depname" IsBound="false" Width="30%">
                                                <Header Caption="<%$Resources:ControlText,gvOrgName %>" Fixed="True">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Startenddate" IsBound="false" Key="Startenddate"
                                                Width="25%">
                                                <Header Caption="<%$Resources:ControlText,gvStartEndDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Flagname" Key="Flagname" IsBound="false" Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvFlag %>">
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
        </div>
        <div id="innerTable_2">
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
                            <a href="../../ExcelModel/NetSignInSetSample.xls">&nbsp;<%=Resources.ControlText.templateddown%>
                            </a>&nbsp;
                            <asp:FileUpload ID="FileUpload" runat="server" Width="30%" />
                            <asp:Button ID="btnImportSave" runat="server" CssClass="button_1" Text="ImportSave"
                                OnClick="btnImportSave_Click" />
                            <asp:Button ID="btnExport" runat="server" CssClass="button_1" OnClick="btnExport_Click" />
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

                            <script language="javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*60/100+"'>");</script>

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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_errorMsg" Key="gds_att_errorMsg">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="350">
                                                <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg %>">
                                                </Header>
                                                <CellStyle ForeColor="red">
                                                </CellStyle>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvWorkNo %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                Width="100">
                                                <Header Caption="<%$Resources:ControlText,gvHeadLocalName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" IsBound="false" Key="StartDate"
                                                Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvStartDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" IsBound="false" Key="EndDate" Width="80">
                                                <Header Caption="<%$Resources:ControlText,gvEndDate %>">
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
        <div id="bottomTable">
            <div style="width: 100%;">
                <table cellspacing="0" cellpadding="0" class="table_title_area" id="tb_show_1">
                    <tr style="width: 100%; cursor: hand;" id="tr_editdata">
                        <td style="width: 100%;" class="tr_title_center">
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <div id="div_img">
                                <img id="div_img_4" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_show_4" style="width: 100%">
                <table class="table_data_area" style="width: 100%">
                    <tr style="width: 100%">
                        <td>
                            <table style="width: 100%">
                                <tr class="tr_data">
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table class="table_data_area">
                                                <tr class="tr_data_1">
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEditWorkNo" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtEditWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="11%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEditLocalName" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtEditLocalName" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="12%">
                                                        &nbsp;
                                                        <asp:Label ID="lblEditDepName" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="22%">
                                                        <asp:TextBox ID="txtEditDepName" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="tr_data_2">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblNetSignDate" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtStartDate" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    ~
                                                                </td>
                                                                <td width="50%">
                                                                    <asp:TextBox ID="txtEndDate" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
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
                                        <asp:Panel ID="pnlShowPanel2" runat="server">
                                            <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClientClick="return checkdate();"
                                                OnClick="btnSave_Click"></asp:Button>
                                            <asp:Button ID="btnEditReturn" runat="server" CssClass="button_1"></asp:Button>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript"><!--
     
         document.getElementById("txtWorkNo").focus();
    document.getElementById("txtWorkNo").select();
        
       
        
	--></script>

</body>
</html>
