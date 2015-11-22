<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFMManagerLeaveForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WFMManagerLeaveForm" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ControlLib" TagName="PageNavigator" Src="~/ControlLib/PageNavigator.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WFMManagerLeaveForm</title>
     <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery_ui_lang.js"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript">
            //單位樹
            function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
            {
                var windowWidth=500,windowHeight=600;
	            var X=(screen.availWidth-windowWidth)/2;
	            var Y=(screen.availHeight-windowHeight)/2;
	            var Revalue=window.showModalDialog("../HRM/EmployeeData/TreeDataPickForm.aspx?modulecode="+moduleCode,window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
	            if(Revalue!=undefined)
	            {	
	                var arrValue=Revalue.split(";");
			            document.all(ReturnValueBoxName).value=arrValue[0];
			            if(arrValue.length>1)
			            {
			                document.all(ReturnDescBoxName).innerText=arrValue[1];
			            }
	            }
            }
          //顯示隱藏
          $(function()
          {
             $("#tr_edit").toggle
             (
                function()
                {
                    $("#div_select").hide();
                    $(".img1").attr("src","../CSS/Images_new/left_back_03.gif");
                    
                },
                function()
                {
                  $("#div_select").show();
                  $(".img1").attr("src","../CSS/Images_new/left_back_03_a.gif");
               
                }
             ) 
               $(function(){
                $("#div_img2,#td_show_1,#td_show_2").toggle(
                  function()
                  {
                    $("#div_showdata").hide();
                    $(".img2").attr("src","../CSS/Images_new/left_back_03.gif");
            
                  },
                 function()
                 {
                     $("#div_showdata").show();
                     $(".img2").attr("src","../CSS/Images_new/left_back_03_a.gif");
                 }
                 ) })        
             
          }
         );
         //全選事件
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridWFM_ManagerLeave_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridWFM_ManagerLeave');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridWFM_ManagerLeave_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridWFM_ManagerLeave_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
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
				document.getElementById("HiddenID").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
			}
		}
		
	   //日曆驗證
	   function CheckDate()
       {
          var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
          var FromDate= $("#<%=textBoxStartDate.ClientID%>").val();
          var ToDate=$("#<%=textBoxEndDate.ClientID %>").val();
          if (FromDate!=null&&FromDate!="")
          {
             if(!check.test(FromDate))
             {
               alert(Message.WrongDate);
               $("#<%=textBoxStartDate.ClientID%>").val("");
               return false;
             }
          }
          if (ToDate!=null&&ToDate!="")
          {
             if(!check.test(ToDate))
             {
               alert(Message.WrongDate);
               $("#<%=textBoxEndDate.ClientID %>").val("");
               return false;
             }
          }
          if((FromDate!=null&&FromDate!="")&&(ToDate!=null&&ToDate!=""))
          {
            if(ToDate<FromDate)
            {
               alert(Message.ToLaterThanFrom);
               $("#<%=textBoxEndDate.ClientID %>").val("");
               return false;
            }
          }
          return true;
       }
       //刪除時，是否確定
       function delete_click()
       {
          if (confirm(Message.DeleteConfirm))
          {     
                var grid = igtbl_getGridById('UltraWebGridWFM_ManagerLeave');
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
                return true;
          }
           else
           {
                return false;
           }
             
        }
        //雙擊修改
        function DblClick(gridName, id)
		{
		    var ProcessFlag="Modify";
		    OpenEdit(ProcessFlag)
		    return 0;
		}
		//彈出新增或修改頁面
		function OpenEdit(ProcessFlag)
		{	
		    var ID = igtbl_getElementById("HiddenID").value;
		    var ModuleCode = igtbl_getElementById("ModuleCode").value;
		    igtbl_getElementById("ProcessFlag").value=ProcessFlag;
		    if(ProcessFlag=="Modify")
		    {                                 
		        var grid = igtbl_getGridById('UltraWebGridWFM_ManagerLeave');
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
            }
            document.getElementById("iframeEdit").src="WFM_WFMManagerLeaveEditForm.aspx?ID="+ID+"&ProcessFlag="+ProcessFlag+"&ModuleCode="+ModuleCode;
            document.getElementById("topTable").style.display="none";
            document.getElementById("divEdit").style.display="";
            return false;
		}
    </script>

</head>
<body class="color_body">
    <form id="form1" style="height: 100%;" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenID" type="hidden" name="HiddenID" runat="server">
    <input id="ModuleCode" type="hidden" name="ModuleCode" runat="server">
    <div id="topTable">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                        <div id="Div1">
                            <img id="Img1" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_select" style="width: 100%">
            <table class="table_data_area">
                <tr class="tr_data_2">
                    <td width="10%">
                        &nbsp;
                        <asp:Label ID="lbl_Unit" runat="server"></asp:Label>
                    </td>
                    <td  width="20%">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="textBoxDepCode" runat="server" CssClass="input_textBox_1" Style="display: none"></asp:TextBox>
                                </td>
                                <td width="100%">
                                    <asp:TextBox ID="textBoxDepName" runat="server" CssClass="input_textBox_1" Width="100%"></asp:TextBox>
                                </td>
                                <td style="cursor: hand">
                                    <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../CSS/Images_new/search_new.gif"></asp:Image>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tr_data_2">
                    <td  width="10%">
                        &nbsp;
                        <asp:Label ID="lblDeputyName" runat="server"></asp:Label>
                    </td>
                    <td  width="20%">
                        <asp:TextBox ID="textBoxLocalName" runat="server" Width="100%" 
                            CssClass="input_textBox_1" ontextchanged="textBoxLocalName_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td  width="10%">
                        &nbsp;
                        <asp:Label ID="gvHeadDeputyName" runat="server"></asp:Label>
                    </td>
                    <td  width="20%">
                        <asp:TextBox ID="textBoxDeputyName" runat="server" Width="100%" 
                            CssClass="input_textBox_1" ontextchanged="textBoxDeputyName_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td  width="10%">
                        &nbsp;
                        <asp:Label ID="lblDeputyDate" runat="server"></asp:Label>
                    </td>
                    <td  width="30%">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="td_label" width="50%">
                                    <asp:TextBox ID="textBoxStartDate" runat="server" Width="100%" CssClass="input_textBox_1" onchange="return CheckDate()"></asp:TextBox>
                                </td>
                                <td style="cursor: hand">
                                    &nbsp;&nbsp;~&nbsp;
                                </td>
                                <td width="50%">
                                    <asp:TextBox ID="textBoxEndDate" runat="server" Width="100%" CssClass="input_textBox_1" onchange="return CheckDate()"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                    CssClass="input_linkbutton" OnClick="ButtonQuery_Click">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnReset" runat="server" Text="<%$Resources:ControlText,btnReset %>"
                                                    CssClass="input_linkbutton" OnClick="ButtonReset_Click">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                    CssClass="input_linkbutton" CommandName="Add" OnClientClick="return OpenEdit('Add')">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                    CssClass="input_linkbutton" OnClientClick="return  OpenEdit('Modify')">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 45px">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/EMP_BUTTON_01.gif');
                                        background-repeat: no-repeat; background-position-x: center; width: 45px; text-align: center;
                                        font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                    CssClass="input_linkbutton" CommandName="Delete" OnClientClick="return  delete_click()"
                                                    OnClick="ButtonDelete_Click">
                                                </asp:LinkButton>
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
        <div style="width: 100%">
           
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" id="td_show_1" class="tr_title_center">
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
                                ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;" id="td_show_2">
                        <img id="div_img2" class="img2" width="22px" height="24px" src="../CSS/Images_new/left_back_03_a.gif" />
                    </td>
                </tr>
            </table>
            <div id="div_showdata">
                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr style="width: 100%">
                        <td valign="top" width="19px" background="../CSS/Images_new/EMP_05.gif" height="18">
                            <img height="18px" src="../CSS/Images_new/EMP_01.gif" width="19">
                        </td>
                        <td background="../CSS/Images_new/EMP_07.gif" height="19px">
                        </td>
                        <td valign="top" width="19px" background="../../CSS/Images_new/EMP_06.gif" height="18">
                            <img height="18" src="../CSS/Images_new/EMP_02.gif" width="19">
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td width="19px" background="../CSS/Images_new/EMP_05.gif">
                            &nbsp;
                        </td>
                        <td >

                            <script language="javascript" type="text/javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*63/100+";'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridWFM_ManagerLeave" runat="server" Width="100%" Height="100%">
                            
                             <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="NotSet" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland"  TableLayout="Fixed"
                                     CellClickActionDefault="RowSelect" AutoGenerateColumns="false" Name="UltraWebGrid">
                                    <HeaderStyleDefault VerticalAlign="Middle"  HorizontalAlign="Left" BorderColor="#6699ff"
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
                                    <igtbl:UltraGridBand 
                                        BaseTableName="WFM_ManagerLeave" Key="WFM_ManagerLeave">
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
                                            <igtbl:UltraGridColumn BaseColumnName="DName" Key="DName" IsBound="false" Width="100px">
                                                <Header Caption="<%$Resources:ControlText,gvDepName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadActivityWorkNo %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderLocalName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            
                                            <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" IsBound="false" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvHeadActivityID %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>

                                            <igtbl:UltraGridColumn BaseColumnName="LeaveTypeName" Key="LeaveTypeName" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvLeaveTypeName %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            
                                            <igtbl:UltraGridColumn BaseColumnName="StartDate" Key="StartDate" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvStartDate %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" IsBound="false" Width="80px">
                                                <Header Caption="<%$Resources:ControlText,lblTodate %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DeputyDays" Key="DeputyDays" IsBound="false"
                                                Width="40px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadShiftDays %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DeputyWorkNo" Key="DeputyWorkNo" IsBound="false"
                                                Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvProxy %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DeputyName" Key="DeputyName" IsBound="false"
                                                Width="80px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadDeputyName %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="DeputyNotes" Key="DeputyNotes" IsBound="false"
                                                Width="200px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadNotes %>" >
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="120px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Modifier" Key="Modifier" IsBound="false" Width="60px">
                                                <Header Caption="<%$Resources:ControlText,gvModifierMan %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ModifyDate" Key="ModifyDate" IsBound="false"
                                                Width="110px">
                                                <Header Caption="<%$Resources:ControlText,gvHeadMoveShiftUpdateDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                        </Columns>                                    
                                   
                                    </igtbl:UltraGridBand>
                                </Bands>
                            </igtbl:UltraWebGrid>

                          <script language="javascript" type="text/javascript">document.write("</DIV>");</script>

                        </td>
                        <td width="19" background="../CSS/Images_new/EMP_06.gif">
                            &nbsp;
                        </td>
                        <tr>
                        </tr>
                    </tr>
                    <tr>
                        <td width="19" background="../CSS/Images_new/EMP_03.gif" height="18">
                            &nbsp;
                        </td>
                        <td background="../CSS/Images_new/EMP_08.gif" height="18">
                            &nbsp;
                        </td>
                        <td width="19" background="../CSS/Images_new/EMP_04.gif" height="18">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
           
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
