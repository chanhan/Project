<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KQMBellCardForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.KQMBellCardForm" %>

<%--
<%@ Register TagPrefix="ControlLib" TagName="Title" Src="~/ControlLib/title.ascx" %>
<%@ Register TagPrefix="ControlLib" TagName="PageNavigator" Src="~/ControlLib/PageNavigator.ascx" %>--%>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BellCardForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
        }
        .img_hidden
        {
            display: none;
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
    
     function condition_click()
     {
        $("#<%=hidOperate.ClientID %>").attr("value","condition")
        removeValue();
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         removeReadonly();
          addcolor();
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
          $("#<%=btnSave.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         
         return false;
     } 
    
    function removeValue()
    {
      $(".input_textBox").attr("value","");
       $("#<%=ddlBellType.ClientID %>").attr("value","");
    }
    
    function removeReadonly()
    { 
      $(".input_textBox").removeClass("input_textBox");
      $("#<%=txtBellNo.ClientID %>").removeAttr("readonly");
      $("#<%=txtPortIP.ClientID %>").removeAttr("readonly");
      $("#<%=txtProduceID.ClientID %>").removeAttr("readonly");
      $("#<%=txtManufacturer.ClientID %>").removeAttr("readonly");
      $("#<%=txtAddress.ClientID %>").removeAttr("readonly");
      $("#<%=txtBellSize.ClientID %>").removeAttr("readonly");
      $("#<%=txtPickDataIP.ClientID %>").removeAttr("readonly");
      $("#<%=txtPickComputeUser.ClientID %>").removeAttr("readonly");
      $("#<%=txtPickComputePW.ClientID %>").removeAttr("readonly");
      $("#<%=txtContactMan.ClientID %>").removeAttr("readonly");
      $("#<%=txtContactTel.ClientID %>").removeAttr("readonly");
      $("#<%=txtUseDept.ClientID %>").removeAttr("readonly");
      $("#<%=txtUserYM.ClientID %>").removeAttr("readonly");
      $("#<%=ddlBellType.ClientID %>").removeAttr("disabled");
      $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
      $("#<%=btnSave.ClientID %>").removeAttr("disabled");
      $("#<%=txtRemark.ClientID %>").removeAttr("readonly");
    }
           
    function  addcolor()
    {         
        $("#<%=txtBellNo.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtPortIP.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtProduceID.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtManufacturer.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtAddress.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtBellSize.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtPickDataIP.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=ddlBellType.ClientID %>").css("background-color", "Cornsilk");
    }
    
    function add_click()
    {
         $("#<%=hidOperate.ClientID %>").attr("value","add")
         removeValue();
         removeReadonly();
          addcolor();
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");

         
         return false;
    
    }
    
    function modify_click()
    {
    if($("#<%=txtBellNo.ClientID %>").val()!="")
    {
         $("#<%=hidOperate.ClientID %>").attr("value","modify")
         removeReadonly();
         addcolor();
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=txtBellNo.ClientID %>").attr("readonly","true");
         return false;
     }
     else
     {
      alert(Message.AtLastOneChoose);
      return false;
     }
    }
    
     function delete_click()
    {
        var BellNo = $.trim($("#<%=txtBellNo.ClientID%>").val());
        if (BellNo=="")
        {
            alert(Message.AtLastOneChoose);
            return false;
        }
        else
        {
          if (confirm(Message.DeleteConfirm))
            {
            return true;
            }
            else
            {
            return false;
            }
        } 
    }
    
    function save_click()
    {
    if ($("#<%=hidOperate.ClientID %>").val()=="add")
    {
                    $.ajax({type: "post", url: "KQMBellCardForm.aspx", dateType: "text", data: {BellNo: $("#<%=txtBellNo.ClientID%>").val()},async:false,
                             success: function(msg) {
                                    if (msg!=0) {alert(Message.NotOnlyOne);result=0; } 
                                    else{result=1;}}
                           }); 
                        if(result==0){return false;} 
    }
        if ( $("#<%=txtBellNo.ClientID %>").val()=="")
        {
            alert(Message.BellNoNotNull);
            return false;
        }
        else
        {
            if ( $("#<%=txtPortIP.ClientID %>").val()=="")
            {
                alert(Message.PortIPNotNull);
                return false;
            }
            else
            {
                if ($("#<%=txtProduceID.ClientID %>").val()=="")
                {
                    alert(Message.ProduceIDNotNull);
                    return false;
                }
                else
                {
                    if($("#<%=txtManufacturer.ClientID %>").val()=="")
                    {
                        alert(Message.ManufacturerNotNull);
                        return false;
                    }
                    else
                    {
                        if ($("#<%=txtAddress.ClientID %>").val()=="")
                        {
                             alert(Message.AddressNotNull);
                             return false;
                        }
                        else
                        {
                            if($("#<%=txtBellSize.ClientID %>").val()=="")
                            {
                                alert(Message.BellSizeNotNull);
                                return false;
                            }
                            else
                            {
                                if ($("#<%=txtPickDataIP.ClientID %>").val()=="")
                                {
                                     alert(Message.PickDataIPNotNull);
                                     return false;
                                }
                                else  
                                {
                                    if ($("#<%=ddlBellType.ClientID %>").val()=="")
                                    {
                                         alert(Message.BellTypeNotNull);
                                        return false;
                                     }
                                    else
                                    { 
                                         return true;
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    };
    
     function cancel_click()
     {
         window.location.href="PersonManage.aspx";

     } 
    
    
    
    
       $(function(){
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
       $(function(){
        $("#img_grid,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_grid").hide();
                $("#img_grid").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_grid").show();
                $("#img_grid").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
       }); 
    
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridUnit_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);    
		}
		function DisplayRowData(row)
		{
			if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
			{
				igtbl_getElementById("txtBellNo").value=row.getCell(0).getValue();
				igtbl_getElementById("txtPortIP").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				igtbl_getElementById("txtProduceID").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				igtbl_getElementById("txtAddress").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
				igtbl_getElementById("txtManufacturer").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
				igtbl_getElementById("txtBellSize").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
				igtbl_getElementById("txtPickDataIP").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
				igtbl_getElementById("txtPickComputeUser").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
				igtbl_getElementById("txtPickComputePW").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();
				igtbl_getElementById("txtContactMan").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
				igtbl_getElementById("txtContactTel").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
				igtbl_getElementById("txtUseDept").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue();
				igtbl_getElementById("txtUserYM").value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue();
				igtbl_getElementById("ddlBellType").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();
				igtbl_getElementById("HiddenddlBellType").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();
				igtbl_getElementById("txtRemark").value=row.getCell(15).getValue()==null?"":row.getCell(15).getValue();
			}
		}
		function OpenWindow()//彈出頁面
		{	    
		    var url="ToolPingKQMBellCardForm.aspx";
	     
            var info=window.open("ToolPingKQMBellCardForm.aspx",'ToolPingKQMBellCardForm', 'height=500, width=800, top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
            return false;
		}
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="HiddenddlBellType" type="hidden" name="HiddenddlBellType" runat="server">
    <table class="top_table" cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand">
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr1">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div_edit">
                                <asp:Panel ID="pnlContent" runat="server">
                                    <asp:HiddenField ID="hidOperate" runat="server" />
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr class="tr_data_1">
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblBellNo" runat="server" ForeColor="Blue">BellNo:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtBellNo" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblPortIP" runat="server" ForeColor="Blue">PortIP:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtPortIP" CssClass="input_textBox" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblProduceID" runat="server" ForeColor="Blue">ProduceID:</asp:Label>
                                            </td>
                                            <td class="td_input" width="20%">
                                                <asp:TextBox ID="txtProduceID" CssClass="input_textBox" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblManufacturer" runat="server" ForeColor="Blue">Manufacturer:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtManufacturer" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblAddress" runat="server" ForeColor="Blue">Address:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtAddress" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblBellSize" runat="server" ForeColor="Blue">BellSize:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtBellSize" CssClass="input_textBox" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblPickDataIP" runat="server" ForeColor="Blue">PickDataIP:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtPickDataIP" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblPickComputeUser" runat="server">PickComputeUser:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtPickComputeUser" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblPickComputePW" runat="server">PickComputePW:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtPickComputePW" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblContactMan" runat="server">ContactMan:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtContactMan" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblContactTel" runat="server">ContactTel:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtContactTel" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblUseDept" runat="server">UseDept:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtUseDept" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_1">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblUserYM" runat="server">UserYM:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtUserYM" Width="50%" CssClass="input_textBox" runat="server"></asp:TextBox><asp:Label
                                                    ID="labelDate" runat="server"></asp:Label>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblBellType" runat="server" ForeColor="Blue">BellType:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:DropDownList ID="ddlBellType" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtRemark" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="td_label" colspan="6">
                                        <table>
                                            <tr>
                                                <td> 
                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                        <asp:Button ID="btnCondition" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                            CssClass="button_2" OnClientClick="return condition_click()"></asp:Button>
                                                        <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                            CssClass="button_2" OnClick="btnQuery_Click"></asp:Button>
                                                        <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                            CssClass="button_2" OnClientClick="return add_click()"></asp:Button>
                                                        <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                            CssClass="button_2" OnClientClick="return  modify_click()"></asp:Button>
                                                        <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                            CssClass="button_2" OnClientClick="return delete_click()" OnClick="btnDelete_Click">
                                                        </asp:Button>
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                            CssClass="button_2" OnClientClick="return  cancel_click()"></asp:Button>
                                                        <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                            CssClass="button_2" OnClientClick="return  save_click()" OnClick="btnSave_Click">
                                                        </asp:Button>
                                                        <asp:Button ID="btnDisable" runat="server" Text="<%$Resources:ControlText,btnDisable %>"
                                                            CssClass="button_2" OnClick="btnDisable_Click"></asp:Button>
                                                        <asp:Button ID="btnEnable" runat="server" Text="<%$Resources:ControlText,btnEnable %>"
                                                            CssClass="button_2" OnClick="btnEnable_Click"></asp:Button>
                                                        <asp:Button ID="btnCheck" runat="server" Text="<%$Resources:ControlText,btnCheck %>"
                                                            CssClass="button_mostlarge" OnClientClick="return OpenWindow();"></asp:Button>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </div>
                </table>
            </td>
        </tr>
    </table>
    <tr>
        <td>
            <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                <tr style="cursor: hand">
                    <td>
                        <table cellspacing="0" cellpadding="0" class="table_title_area">
                            <tr style="width: 100%;" id="tr_edit">
                                <td style="width: 100%;" class="tr_title_center" id="td_show_1">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                        background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblGrid" runat="server"></asp:Label>
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
                                            ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                            OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                                        </ess:AspNetPager>
                                    </div>
                                </td>
                                <td style="width: 22px;" id="td_show_2">
                                    <img id="img_grid" class="img2" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 244px">
                        <div id="div_grid">
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

                                        <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*53/100+"'>");</script>

                                        <igtbl:UltraWebGrid ID="UltraWebGridBellCard" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGridBellCard_DataBound">
                                            <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridBellCard" TableLayout="Fixed"
                                                CellClickActionDefault="RowSelect" AutoGenerateColumns="false">
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
                                                    DataKeyField="" BaseTableName="KQM_BellCard" Key="KQM_BellCard">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn BaseColumnName="BellNo" Key="BellNo" IsBound="false" Width="100">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadBellNo %>" Fixed="True">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="PortIP" Key="PortIP" IsBound="false" Width="100">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadPortIP %>" Fixed="True">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ProduceID" Key="ProduceID" IsBound="false"
                                                            Width="70">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadProduceID %>" Fixed="True">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Address" Key="Address" IsBound="false" Width="100">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadAddress %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Manufacturer" Key="Manufacturer" IsBound="false"
                                                            Width="60">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadManufacturer %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="BellSize" Key="BellSize" IsBound="false" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadBellSize %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="PickDataIP" Key="PickDataIP" IsBound="false"
                                                            Width="100">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadPickDataIP %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="PickComputeUser" Key="PickComputeUser" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadPickComputeUser %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="PickComputePW" Key="PickComputePW" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadPickComputePW %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ContactMan" Key="ContactMan" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadContactMan %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="ContactTel" Key="ContactTel" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadContactTel %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="UseDept" Key="UseDept" IsBound="false" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadUseDept %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="UserYM" Key="UserYM" IsBound="false" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadUserYM %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="BellType" Key="BellType" IsBound="false" Width="80"
                                                            Hidden="true">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadBellType %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="BellTypeName" Key="BellTypeName" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadBellTypeName %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadRemark %>">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn BaseColumnName="EffectFlag" Key="EffectFlag" IsBound="false"
                                                            Width="80">
                                                            <Header Caption="<%$Resources:ControlText,gvHeadEffectFlag %>">
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
