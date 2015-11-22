<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParameterForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemData.ParameterForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系統參數</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link type="text/css" href="../../CSS/CommonCss.css" />
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .input_textBox
        {
            border: 0;
            display: none;
        }
        .ddl_hiden
        {
            display: none;
        }
    </style>

    <script type="text/javascript">
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridParameter_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
		var operateType=$.trim($("#<%=hidOperate.ClientID%>").val());
		if(operateType!="Add" && operateType!="Modify" && operateType!="Condition")
			{
			igtbl_getElementById("txtParaName").value=row.getCell(0).getValue();
			igtbl_getElementById("txtParaValue").value=row.getCell(1).getValue();
			igtbl_getElementById("txtDescription").value=row.getCell(2).getValue();
			$(".input_textBox").removeClass("input_textBox");   
			}
		}
		$(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img_edit").attr("src","../../CSS/Images/downarrows_white.gif");
                
            },
            function(){
              $("#div_1").show();
                $(".img_edit").attr("src","../../CSS/Images/uparrows_white.gif");
            }
        );
        
        $("#img_2,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_grid").hide();
                $(".img_2").attr("src","../../CSS/Images/downarrows_white.gif");
                
            },
            function(){
              $("#div_grid").show();
                $(".img_2").attr("src","../../CSS/Images/uparrows_white.gif");
            }
        );
        
        $('#<%=btnCondition.ClientID %>').click(function ()
            { 
              $("#<%=btnCondition.ClientID %>,#<%=btnSave.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>").each(function() {
                $(this).attr("disabled","true");
                });
             
             $("#<%=btnQuery.ClientID %>,#<%=btnCancel.ClientID %>").each(function() {
                $(this).removeAttr("disabled");
                });
             $(".input_textBox").removeClass("input_textBox");  

               });
            
            
            $('#<%=btnAdd.ClientID %>').click(function()
            { 
              
             $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>").each(function() {
                $(this).attr("disabled","true");
                });
             
             $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>").each(function() {
                $(this).removeAttr("disabled");
                });
             $(".input_textBox").removeClass("input_textBox");    
          
             $("#<%=hidOperate.ClientID %>").val("Add");
             $("#<%=txtParaName.ClientID %>").removeAttr("readonly");
             $("#<%=txtParaValue.ClientID %>").removeAttr("readonly");
             $("#<%=txtDescription.ClientID %>").removeAttr("readonly");
             $("#<%=txtParaName.ClientID %>").css("border-style","solid");   
                $("#<%=txtParaValue.ClientID %>").css("border-style","solid");
                $("#<%=txtDescription.ClientID %>").css("border-style","solid");
                $("#<%=txtParaName.ClientID %>").val(""); 
               $("#<%=txtParaValue.ClientID %>").val(""); 
                $("#<%=txtDescription.ClientID %>").val("");
            return false;        
             
           }
            );
            
            $('#<%=btnCancel.ClientID %>').click(function()
            { 
              
             $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>").each(function() {
                $(this).removeAttr("disabled");
                });
             
             $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>").each(function() {
                $(this).attr("disabled","true");
                });
              $("#<%=txtParaName.ClientID %>,#<%=txtParaValue.ClientID %>,#<%=txtDescription.ClientID %>").each(function() {
                //$(this).addClass("input_textBox");
                $(this).css("border-style","none");
                }); 
          
             $("#<%=hidOperate.ClientID %>").val("");
             
           }
            );
            
            $('#<%=btnModify.ClientID %>').click(function()
            {
            $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>").each(function() {
                $(this).attr("disabled","true");
                });
             
             $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>").each(function() {
                $(this).removeAttr("disabled");
                });
             $("#<%=txtParaName.ClientID %>").css("border-style", "none");
             $("#<%=txtParaName.ClientID %>").attr("readonly","true");
               $("#<%=txtParaValue.ClientID %>").removeAttr("readonly");
             $("#<%=txtDescription.ClientID %>").removeAttr("readonly");
             

           var ParaName = $.trim($("#<%=txtParaName.ClientID%>").val());
           if (ParaName =="")
           {
            alert(Message.AtLastOneChoose);
            return false;
           }
          else
          { 
            $("#<%=txtParaName.ClientID %>").css("border-style", "none");
            $("#<%=txtParaName.ClientID %>").attr("readonly","true");
            $("#<%=hidOperate.ClientID %>").val("modify");  
            $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
            $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
            $("#<%=txtParaName.ClientID %>").css("border-style", "none");
            $("#<%=hidOperate.ClientID %>").val("Modify");
            $("#<%=txtParaName.ClientID %>").css("border-style","solid");   
                $("#<%=txtParaValue.ClientID %>").css("border-style","solid");
                $("#<%=txtDescription.ClientID %>").css("border-style","solid");
          return false; 
          }
         }
            );
         
   });
   
     function delete_click()
    {
        $("#<%=hidOperate.ClientID %>").val("Delete");
        var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
        var ParaName = $.trim($("#<%=txtParaName.ClientID%>").val());
        if (ParaName =="")
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
    
   function btnCondition_Click()
    {
            $("#<%=txtParaName.ClientID %>").removeAttr("readonly");
             $("#<%=txtParaValue.ClientID %>").removeAttr("readonly");
             $("#<%=txtDescription.ClientID %>").removeAttr("readonly");
             $("#<%=hidOperate.ClientID %>").val("Condition");
              $("#<%=txtParaName.ClientID %>").val(""); 
               $("#<%=txtParaValue.ClientID %>").val(""); 
                $("#<%=txtDescription.ClientID %>").val("");
                $("#<%=txtParaName.ClientID %>").css("border-style","solid");   
                $("#<%=txtParaValue.ClientID %>").css("border-style","solid");
                $("#<%=txtDescription.ClientID %>").css("border-style","solid");
            return false;
    }
    
      function  save_click()
    {
        if (confirm(Message.SaveConfim))
        {
            var alertText;
            var ParaName = $.trim($("#<%=txtParaName.ClientID%>").val());
            $("#<%=hidOperate.ClientID %>").val("");
                if (ParaName =="")
                {
                    alertText = $.trim($("#<%=lblParaName.ClientID%>").text()) +Message.TextBoxNotNull;
                    alert(alertText);
                    return false;
                }
                
                else 
                {
                   return true;  
                }
        }
        else
        {
        return false;
        }
    }
    
      
     function Load()
   {
        $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>").each(function() {
                $(this).removeAttr("disabled");
                });
             
             $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>").each(function() {
                $(this).attr("disabled","true");
                });
              $("#<%=txtParaName.ClientID %>,#<%=txtParaValue.ClientID %>,#<%=txtDescription.ClientID %>").each(function() {
                $(this).addClass("input_textBox");
                }); 
          
             $("#<%=hidOperate.ClientID %>").val(null);
             
          $("#<%=txtParaName.ClientID %>").attr("readonly","true");
          $("#<%=txtParaValue.ClientID %>").attr("readonly","true");
          $("#<%=txtDescription.ClientID %>").attr("readonly","true");
          $("#<%=txtParaName.ClientID %>").css("border-style", "none");
          $("#<%=txtParaValue.ClientID %>").css("border-style", "none");
          $("#<%=txtDescription.ClientID %>").css("border-style", "none")
        }
    </script>

</head>
<body onload="return Load()">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <table class="top_table" cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
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
                            <div id="Div1">
                                <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                        </td>
                    </tr>
                </table>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <div id="div_1">
                                <asp:Panel ID="pnlContent" runat="server">
                                    <asp:HiddenField ID="hidOperate" runat="server" />
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <tr class="tr_data_1">
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblParaName" runat="server"></asp:Label>
                                            </td>
                                            <td class="td_input" width="35%">
                                                <asp:TextBox ID="txtParaName" Width="100%" CssClass="input_textBox_1" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="td_label" width="15%">
                                                &nbsp;
                                                <asp:Label ID="lblParaValue" runat="server"></asp:Label>
                                            </td>
                                            <td class="td_input" width="35%">
                                                <asp:TextBox ID="txtParaValue" CssClass="input_textBox_1" Width="100%" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="tr_data_2">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                            </td>
                                            <td class="td_input" colspan="3">
                                                <asp:TextBox ID="txtDescription" Width="100%" CssClass="input_textBox_2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label" colspan="4">
                                                <table>
                                                    <tr>
                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                            <td>
                                                                <asp:Button ID="btnCondition" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                                    CssClass="button_1" OnClientClick="return btnCondition_Click()"></asp:Button>
                                                                <asp:Button ID="btnQuery" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                    CssClass="button_1" OnClick="btnQuery_Click" ></asp:Button>
                                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                    CssClass="button_1"></asp:Button>
                                                                <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                    CssClass="button_1"></asp:Button>
                                                                <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                    CssClass="button_1" OnClientClick="return  delete_click()" OnClick="btnDelete_Click">
                                                                </asp:Button>
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                                    CssClass="button_1" Enabled="false"></asp:Button>
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                    CssClass="button_1" OnClientClick="return  save_click()" OnClick="btnSave_Click"
                                                                    Enabled="false"></asp:Button>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
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
                                    ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                    DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                    ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                    OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
                                </ess:AspNetPager>
                            </div>
                        </td>
                        <td style="width: 22px;" id="td_show_2">
                            <img id="div_img2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </td>
                    </tr>
                </table>
                <div id='div_grid'>
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

                                <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*55/100+"'>");</script>

                                <igtbl:UltraWebGrid ID="UltraWebGridParameter" runat="server" Width="100%" Height="100%">
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
                                        <igtbl:UltraGridBand BaseTableName="gds_sc_parameter" Key="gds_sc_parameter">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="PARANAME" Key="PARANAME" IsBound="false" Width="20%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadParaName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="PARAVALUE" Key="PARAVALUE" IsBound="false"
                                                    Width="30%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadParaValue %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Description" Key="Description" IsBound="false"
                                                    Width="50%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadParaDescription %>">
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
    </form>
</body>
</html>
