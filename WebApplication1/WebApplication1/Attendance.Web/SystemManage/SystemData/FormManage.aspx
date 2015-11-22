<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormManage.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemData.FormManage" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script>
                   function AfterSelectChange(gridName, id)
               {
  		        	var row = igtbl_getRowById(id);
			        DisplayRowData(row);
			       return 0;
		      }
			function	DisplayRowData(row)
	    	{
	    	if( $("#<%=hidOperate.ClientID %>").val()!="Add"&&$("#<%=hidOperate.ClientID %>").val()!="Condition")
	    	{
	    	       	var nid=row.getCell(3).getValue();
	    	       			$('#<%=hidFormSeq.ClientID %>').val(nid);
	    	             $(".messageLabel").text(""); 
	    	             $("#<%=fileAnnexFile.ClientID %>").val(null);
	    	              $(":text,select,#<%=fileAnnexFile.ClientID %>").css("border-color", "silver"); 
	    	              $("select").css("background-color", "#ffffff");
            $.ajax({
                type: "POST", url: "FormManage.aspx", data: { formId: nid }, dataType: "json",
                success: function(item) {
                     $('#<%=ProcessFlag.ClientID %>').val('Y');
                    $("#<%=chkActiveFlag.ClientID %>").attr("checked", item.ActiveFlag == "Y");
                    $.each(item, function(k, v) { $(":text[id$='txt" + k + "'],select[id$='ddl" + k + "'],input:hidden[id$='hid" + k + "']").val(v); });
                    $("#<%=txtUploadDate.ClientID %>").val($.jsonDateToString(item.UploadDate));
                    $("#<%=lnkbtnAnnexFile.ClientID %>").text(item.FormPath); $("#annexRow").show();
                             }
            });
            $("#<%=txtUploadDate.ClientID %>").attr("readonly", "readonly"); 
            $("#<%=txtUploadDate.ClientID %>+:button").attr("disabled", "disabled");
            }
		    }
		    
    $(function(){
    
      $("#<%=btnCancel.ClientID %>").attr("disabled",true);
        $("#<%=btnSave.ClientID %>").attr("disabled",true);
        
        $('#<%=btnCondition.ClientID %>').click(function(){
     $(":text,input:hidden[id*='hid'],select,textarea").val(null);
            $("#annexRow").hide();
         $("#<%=hidOperate.ClientID %>").val('Condition');    
     $(".button_1").each(function(){
                $(this).attr("disabled",true);
            });
             $("#<%=btnQuery.ClientID %>,#<%=btnCancel.ClientID %>").each(function()
             {
             $(this).attr("disabled",false);
             });
             RemoveReadOnly();//移除只讀 F3228823
            return false;
    });
        $('#<%=btnAdd.ClientID %>').click(function(){
        
    $("#<%=hidOperate.ClientID %>").val("Add");
       $("#annexRow").hide();
        $('.button_1').each(function(){
    $(this).attr('disabled',true);
    });
         $("#<%=btnCancel.ClientID %>").attr("disabled",false);
           $("#<%=btnSave.ClientID %>").attr("disabled",false);
           RemoveReadOnly();//移除只讀 F3228823
     return false;
    });
           $('#<%=btnSave.ClientID %>').click(
           function(){
                           $(".messageLabel").text("");
                var valid = true; var op = $("#<%=hidOperate.ClientID %>").val();
                if (op == "Add") {
                    $("#<%=txtUploadDate.ClientID %>,#<%=txtFormName.ClientID %>,#<%=fileAnnexFile.ClientID %>,select").each(function() {
                        if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { valid = false; $(this).css("border-color", "#ff6666"); }
                    });
                }
                else {
                    $("#<%=txtUploadDate.ClientID %>,#<%=txtFormName.ClientID %>,select").each(function() {
                        if ($.trim($(this).val())) { $(this).css("border-color", "silver"); } else { valid = false; $(this).css("border-color", "#ff6666"); }
                    });
                }
                                if ($.browser.msie && $.browser.version < 8) { $("select").each(function() { if ($(this).val()) { $(this).css("background-color", "#ffffff"); } else { $(this).css("background-color", "#ffaaaa"); } }) }
                if (valid && op == "Add") {
                    var today = new Date(); var ndinfo = $("#<%=txtUploadDate.ClientID %>").val().split('/');
                    if (today.getFullYear() != parseInt(ndinfo[0]) || (today.getMonth() + 1) != parseInt(ndinfo[1]) || today.getDate() != parseInt(ndinfo[2])) { valid = false; $(".messageLabel").text(Message.UploadDateMustBeToday); }
                }
                if (valid && op == "Modify" && $("#<%=fileAnnexFile.ClientID %>").val()) {
                    return confirm(Message.ConfirmNewFormOverride);
                }
                return valid;
           }
           );
              
               $('#<%=btnDelete.ClientID %>').click(function(){
               	if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		         {
		          alert(Message.AtLastOneChoose);
		           return false;
	     	      }
	     	     else
	     	     {
                 return confirm(Message.DeleteFormConfirm);
                 }
                });
         $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
         $("#<%=btnCancel.ClientID %>").click(function() {
          $("#<%=hidOperate.ClientID %>").val('Cancel');
          $('.button_1').each(function(){
    $(this).attr('disabled',false);
    });  
       $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           $("#<%=btnSave.ClientID %>").attr("disabled",true);
                $("#<%=txtUploadDate.ClientID %>").removeAttr("readonly"); $("#<%=txtUploadDate.ClientID %>+:button").removeAttr("disabled"); $("#annexRow").hide();
                $(".messageLabel,#<%=lnkbtnAnnexFile.ClientID %>").text(""); $(":text,select,#<%=fileAnnexFile.ClientID %>").val(null).css("border-color", "silver"); $("input:hidden[id*='hid']").val(null);
                $("select").css("background-color", "#ffffff"); $("#<%=chkActiveFlag.ClientID %>").attr("checked", true); 
                AddReadOnly();//添加只讀   F3228823
                return false;
            
            
            });
            
            $("#<%=btnModify.ClientID %>").click(function(){
            
            RemoveReadOnly();//移除只讀 F3228823
                 $("#<%=hidOperate.ClientID %>").val('Modify');
                  	if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		          {
		            alert(Message.AtLastOneChoose);
	     	       }
	     	       else
	     	       {
	     	       	       $('.button_1').each(function(){
                $(this).attr('disabled',true);
                 });
             $("#<%=btnCancel.ClientID %>").attr("disabled",false);
             $("#<%=btnSave.ClientID %>").attr("disabled",false);
	     	       }
	     	          return false;
                 });
    });
    
    //添加只讀F3228823
   function AddReadOnly()
   {
       $("#<%=txtUploadDate.ClientID %>").attr("readonly","true");
       $("#<%=txtFormName.ClientID %>").attr("readonly","true");
       $("#<%=txtFormRemark.ClientID %>").attr("readonly","true");
       $('#<%=ddlTypeId.ClientID %>').attr("disabled","true");
      // $('#<%=chkActiveFlag.ClientID %>').attr("disabled","true");
       $('#<%=fileAnnexFile.ClientID %>').attr("disabled","true");
       $(".input_textBox_1").css("border-style", "none");
      $(".input_textBox_2").css("border-style", "none");
       $(".input_textBox_1").css("background-color", "#f5f5f5");
      $(".input_textBox_2").css("background-color", "");
   }
   
   
   //移除只讀F3228823
   function RemoveReadOnly()
   {
    
       $("#<%=txtUploadDate.ClientID %>").removeAttr("readonly");
       $("#<%=txtFormName.ClientID %>").removeAttr("readonly");
       $("#<%=txtFormRemark.ClientID %>").removeAttr("readonly");
       $('#<%=ddlTypeId.ClientID %>').removeAttr("disabled");
      // $('#<%=chkActiveFlag.ClientID %>').removeAttr("disabled");
        $('#<%=fileAnnexFile.ClientID %>').removeAttr("disabled");
        $(".input_textBox_1").css("border-style", "solid");
      $(".input_textBox_2").css("border-style", "solid");
      $(".input_textBox_1").css("background-color", "White");
      $(".input_textBox_2").css("background-color", "White");
        
   }
    </script>

</head>
<body onload="return AddReadOnly();">
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="ProcessFlag" runat="server" Value="N" />
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
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
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit" style="width: 100%">
            <table class="table_data_area" style="width: 100%">
                <tr style="width: 100%">
                    <td>
                        <table style="width: 100%">
                            <tr class="tr_data">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFormInfo" runat="server">
                                                    <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                                    <asp:HiddenField ID="hidFormSeq" runat="server" />
                                                    <asp:HiddenField ID="hidFormPath" runat="server" />
                                                    <table cellpadding="0" cellspacing="0" class="QueryTable" width="100%">
                                                        <tr style="background-color: #f5f5f5;">
                                                            <td align="right" style="width: 17%">
                                                                <asp:Label ID="lblUploadDate" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:TextBox ID="txtUploadDate" runat="server" Width="150px" CssClass="input_textBox_1" ></asp:TextBox>
                                                            </td>
                                                            <td align="right" style="width: 17%">
                                                                <asp:Label ID="lblTypeId" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="width: 33%">
                                                                <asp:DropDownList ID="ddlTypeId" runat="server" Width="150px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #fff;">
                                                            <td align="right">
                                                                <asp:Label ID="lbllblFormName" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtFormName" runat="server" Width="85%" CssClass="input_textBox_2"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkActiveFlag" runat="server" Checked="true" />
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #f5f5f5;">
                                                            <td align="right">
                                                                <asp:Label ID="lblFormRemark" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtFormRemark" runat="server" Width="540px" CssClass="input_textBox_1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="annexRow" style="background-color: #fff; display: none;">
                                                            <td align="right">
                                                                <asp:Label ID="lblFormFile" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:LinkButton ID="lnkbtnAnnexFile" runat="server" OnClick="lnkbtnAnnexFile_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #fff;">
                                                            <td align="right">
                                                                <asp:Label ID="lblUploadForm" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:FileUpload ID="fileAnnexFile" runat="server" Width="540px" Style="border: solid 1px Silver;" />
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #f5f5f5;">
                                                            <td colspan="4" align="center">
                                                                <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="420px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #fff; height: 45px;">
                                                            <td colspan="4" align="center">
                                                                <table>
                                                                    <tr>
                                                                        <asp:Panel ID="pnlShowPanel" runat="server">
                                                                            <td>
                                                                                <asp:Button ID="btnCondition" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                                                    ToolTip="Authority Code:Condition"></asp:Button>
                                                                                <asp:Button ID="btnQuery" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                                                    ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                                                <asp:Button ID="btnAdd" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                                    ToolTip="Authority Code:Add"></asp:Button>
                                                                                <asp:Button ID="btnModify" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                                    ToolTip="Authority Code:Modify"></asp:Button>
                                                                                <asp:Button ID="btnDelete" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                                    ToolTip="Authority Code:Delete" OnClick="btnDelete_Click"></asp:Button>
                                                                                <asp:Button ID="btnSave" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                                    ToolTip="Authority Code:Save" OnClick="btnSave_Click"></asp:Button>
                                                                                <asp:Button ID="btnCancel" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                                                    ToolTip="Authority Code:Cancel"></asp:Button>
                                                                            </td>
                                                                        </asp:Panel>
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
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%" id="PanelData">
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
                                ShowMoreButtons="false" HorizontalAlign="Center" PageSize="10" PagingButtonType="Image"
                                Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;" id="td_show_2">
                        <div id="img_grid">
                            <img id="div_img_2" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="tr_show">
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridInfoForm" runat="server" Width="100%" Height="100%"
                                OnDataBound="UltraWebGridInfoForm_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoForm" TableLayout="Fixed"
                                    AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                    <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <ClientSideEvents InitializeLayoutHandler="UltraWebGrid_InitializeLayoutHandler"
                                        DblClickHandler="UltraWebGrid_DblClickHandler" AfterSelectChangeHandler="AfterSelectChange">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_info_forms_v" Key="gds_att_info_forms_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="Type_Name" Key="Type_Name" IsBound="false"
                                                Width="40%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadTypeName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Form_Name" Key="Form_Name" IsBound="false"
                                                Width="40%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFormFormName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                HeaderClickAction="Select" Width="20%" Key="Active_Flag" BaseColumnName="Active_Flag">
                                                <CellTemplate>
                                                    <asp:Image ID="imgActiveFlag" runat="server" />
                                                </CellTemplate>
                                                <Header Caption="<%$Resources:ControlText,gvHeadActiveFlag %>" ClickAction="Select"
                                                    Fixed="true">
                                                </Header>
                                            </igtbl:TemplatedColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="form_seq" Key="form_seq" IsBound="false" Hidden="true">
                                                <Header Caption="">
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
    </div>
    </form>
</body>
</html>
