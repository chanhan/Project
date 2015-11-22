<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeManager.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemData.NoticeManager" %>

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
                function OpenDetail(type, id) {
                $(".messageLabel").text(""); 
            $('#<%=lblMessage.ClientID %>').val(null);
            var sFeature = "resizable:no;dialogHeight:700px; dialogWidth:790px; status:no;"
            if ($.browser.msie) {
                if ($.browser.version < 7) {
                    sFeature = "resizable:no;dialogHeight:733px; dialogWidth:796px; status:no;"
                }
            }
            var url = "";
            switch (type) {
                case "Notice": url = "../../HomePage/NoticeDetail.aspx?noticeId=" + id; break;
                case "Faq": url = "../../HomePage/FaqDetail.aspx?faqSeq=" + id; break;
                case "Paper": url = "../../HomePage/PaperDetail.aspx?PaperSeq=" + id; break;
            }
            window.showModalDialog(url+"&r="+Math.random(), id, sFeature);
        }
    		 function UltraWebGridInfoNotice_DblClickHandler(gridName, cellId)
    		 {
              OpenEdit("Modify")
              return 0;
            }      
    		function UltraWebGridInfoNotice_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
    		function OpenEdit(hidOperate)
		{		    
		    $(".messageLabel").text(""); 
		    var ID;
		    igtbl_getElementById("hidOperate").value=hidOperate;
		    if(hidOperate=="Modify")
		    {
		        ID=igtbl_getElementById("hidNoticeId").value;
		        var grid = igtbl_getGridById('UltraWebGridInfoNotice');
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
            moduleCode= igtbl_getElementById("ModuleCode").value;
             document.getElementById("iframeEdit").src="NoticeEditForm.aspx?NoticeId="+ID+"&hidOperate="+hidOperate+"&NoticeManagerModuleCode="+moduleCode;
            document.getElementById("topTable").style.display="none";
            document.getElementById("div_2").style.display="none";
            document.getElementById("PanelData").style.display="none";
            
            document.getElementById("divEdit").style.display="";
            return false ;
		}
               function AfterSelectChange(gridName, id)
       {
  			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
        
	function	DisplayRowData(row)
		{
	//	$(".messageLabel").text(""); 
		if( $("#<%=hidOperate.ClientID %>").val()!='Add'&&$("#<%=hidOperate.ClientID %>").val()!='Condition')
			{
			var noticeid=row.getCell(4).getValue();
			$('#<%=hidNoticeId.ClientID %>').val(noticeid);
		                $.ajax({
                type: "POST", url: "NoticeManager.aspx", data: { NoticeId: noticeid }, dataType: "json",async: false,
                success: function(item) {
                   $('#<%=ProcessFlag.ClientID %>').val('Y');
                       $("#<%=chkActiveFlag.ClientID %>").attr("checked", item.ActiveFlag == "Y");
                    $.each(item, function(k, v) { $(":text[id$='txt" + k + "'],select[id$='ddl" + k + "'],input:hidden[id$='hid" + k + "']").val(v); });
                    $("#<%=txtNoticeDate.ClientID %>").val($.jsonDateToString(item.NoticeDate));
                    $("#<%=txtNoticeContent.ClientID %>").val(item.NoticeContent);
                }
            });
              $("#<%=txtNoticeDate.ClientID %>").attr("readonly", "readonly"); $("#<%=txtNoticeDate.ClientID %>+:button").attr("disabled", "disabled");
          }
		}
    $(function(){
      $('#<%=txtNoticeContent.ClientID %>').attr('readonly',true);  
          $("#<%=btnCancel.ClientID %>").attr("disabled",true);
      $('select').each(function()
	     	   {
	     	   $(this).attr('disabled',true);
	     	   });
	     	      $(':text').each(function(){
                $(this).attr('readonly',true);
    });
   
           $('#<%=btnDelete.ClientID %>').click(function(){
        
               	if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		        alert(Message.AtLastOneChoose);
		        return false;
	     	 }
	     	 else
	     	 {
               return confirm(Message.DeleteNoticeConfirm);
             }
           });
           
           
    $('#<%=btnCancel.ClientID %>').click(function(){
 
        $('#<%=txtNoticeContent.ClientID %>').attr('readonly',true);  
      $('select').each(function()
	     	   {
	     	   $(this).attr('disabled',true);
	     	   });
	     	      $(':text').each(function(){
                $(this).attr('readonly',true);
              });
      $("#<%=hidOperate.ClientID %>").val('Cancel');    
       $("#<%=txtNoticeDate.ClientID %>").removeAttr("readonly"); 
 $("#<%=txtNoticeDate.ClientID %>+:button").removeAttr("disabled");
  $(":text,input:hidden[id*='hid'],select,textarea").val(null);
    $("#<%=chkActiveFlag.ClientID %>").attr("checked", true);
    
      $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>,#<%=btnCancel.ClientID %>").each(function()
             {
             $(this).attr("disabled",false);
             });
           $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           return false;
    });
    
    
       $('#<%=btnQuery.ClientID %>').click(function(){
          $(".messageLabel").text(""); 
           });
    $('#<%=btnCondition.ClientID %>').click(function(){
   $(".messageLabel").text(""); 
          $('#<%=txtNoticeContent.ClientID %>').attr('readonly',false);  
      $('select').each(function()
	     	   {
	     	   $(this).attr('disabled',false);
	     	   });
	     	      $(':text').each(function(){
                $(this).attr('readonly',false);
    });
     $(":text,input:hidden[id*='hid'],select,textarea").val(null);
         $("#<%=hidOperate.ClientID %>").val('Condition');    
     $(".button_1").each(function(){
                $(this).attr("disabled",true);
            });
             $("#<%=btnQuery.ClientID %>,#<%=btnCancel.ClientID %>").each(function()
             {
             $(this).attr("disabled",false);
             });
            return false;
    });
//    $('#<%=btnCancel.ClientID %>').each(function(){
//    $(this).attr('disabled',true);
//    });
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
              $("#td_show_1,#img_grid").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../../CSS/Images_new/left_back_03_a.gif");
                }
            );
    })
    
    </script>

    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="topTable">
        <asp:HiddenField ID="ProcessFlag" runat="server" Value="N" />
        <asp:HiddenField ID="ModuleCode" runat="server" Value="N" />
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="img_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLastNotice" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div>
                            <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tr_edit" style="width: 100%">
            <table cellspacing="0" cellpadding="0" width="100%" class="table_data_area">
                <tr>
                    <td>
                        <asp:Panel ID="pnlNoticeInfo" runat="server">
                            <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                            <asp:HiddenField ID="hidNoticeId" runat="server" />
                            <asp:HiddenField ID="hidAnnexFilePath" runat="server" />
                            <asp:HiddenField ID="hidBrowseTimes" runat="server" />
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr style="background-color: #f5f5f5;">
                                    <td>
                                        <asp:Label ID="lblNoticeDate" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoticeDate" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNoticeTypeId" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNoticeTypeId" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="background-color: #fff;">
                                    <td>
                                        <asp:Label ID="lblNoticeAuthor" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoticeAuthor" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAuthorTel" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAuthorTel" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: #f5f5f5;">
                                    <td>
                                        <asp:Label ID="lblNoticeDept" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoticeDept" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActiveFlag" runat="server" Checked="true" />
                                    </td>
                                </tr>
                                <tr style="background-color: #fff;">
                                    <td>
                                        <asp:Label ID="lblNoticeTitle" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtNoticeTitle" runat="server" Width="590px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: #fff;">
                                    <td valign="top">
                                        <asp:Label ID="lblNoticeContent" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtNoticeContent" runat="server" Width="590px" TextMode="MultiLine"
                                            Height="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: #f5f5f5;">
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="messageLabel" Width="480px"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="background-color: #fff; height: 45px;">
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <asp:Panel ID="pnlShowPanel" runat="server">
                                                    <td>
                                                        <asp:Button ID="btnCondition" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                            ToolTip="Authority Code:Condition"></asp:Button>
                                                        <asp:Button ID="btnQuery" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnQuery %>"
                                                            ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                        <asp:Button ID="btnAdd" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                            ToolTip="Authority Code:Add" OnClientClick="return OpenEdit('Add')"></asp:Button>
                                                        <asp:Button ID="btnModify" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                            ToolTip="Authority Code:Modify" OnClientClick="return OpenEdit('Modify')"></asp:Button>
                                                        <asp:Button ID="btnDelete" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                            ToolTip="Authority Code:Delete" OnClick="btnDelete_Click"></asp:Button>
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

                                <igtbl:UltraWebGrid ID="UltraWebGridInfoNotice" runat="server" Width="100%" Height="100%"
                                    OnDataBound="UltraWebGridInfoNotice_DataBound">
                                    <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                        AllowSortingDefault="Yes" RowHeightDefault="25px" Version="4.00" SelectTypeRowDefault="Single"
                                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                        AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridInfoNotice" TableLayout="Fixed"
                                        AutoGenerateColumns="false" CellClickActionDefault="RowSelect">
                                        <HeaderStyleDefault VerticalAlign="Middle" HorizontalAlign="Left" BorderColor="#6699ff"
                                            CssClass="tr_header">
                                            <BorderDetails ColorTop="White" WidthLeft="0px" WidthTop="0px" ColorLeft="White">
                                            </BorderDetails>
                                        </HeaderStyleDefault>
                                        <FrameStyle Width="100%" Height="100%">
                                        </FrameStyle>
                                        <ClientSideEvents InitializeLayoutHandler="UltraWebGridInfoNotice_InitializeLayoutHandler"
                                            DblClickHandler="UltraWebGridInfoNotice_DblClickHandler" AfterSelectChangeHandler="AfterSelectChange">
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
                                        <igtbl:UltraGridBand BaseTableName="gds_att_info_notices_v" Key="gds_att_info_notices_v">
                                            <Columns>
                                                <igtbl:UltraGridColumn BaseColumnName="Notice_Date" Key="Notice_Date" IsBound="false"
                                                    Format="yyyy/MM/dd" Width="30%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadNoticeDate %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Notice_Title" Key="Notice_Title" IsBound="false"
                                                    Width="30%">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadNoticeTitle %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="Notice_Type_Name" Key="Notice_Type_Name" IsBound="false">
                                                    <Header Caption="<%$Resources:ControlText,gvHeadNoticeTypeName %>">
                                                    </Header>
                                                </igtbl:UltraGridColumn>
                                                <igtbl:TemplatedColumn AllowGroupBy="No" AllowRowFiltering="False" AllowUpdate="No"
                                                    HeaderClickAction="Select" Width="30px" Key="Active_Flag" BaseColumnName="Active_Flag">
                                                    <CellTemplate>
                                                        <asp:Image ID="imgActiveFlag" runat="server" />
                                                    </CellTemplate>
                                                    <Header Caption="<%$Resources:ControlText,gvHeadActiveFlag %>" ClickAction="Select"
                                                        Fixed="true">
                                                    </Header>
                                                </igtbl:TemplatedColumn>
                                                <igtbl:UltraGridColumn BaseColumnName="NOTICE_ID" Key="NOTICE_ID" IsBound="false"
                                                    Hidden="true">
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
    </div>

    <script language="javascript">document.write("<div id='divEdit' style='display:none;height:"+document.body.clientHeight*87/100+"'>");</script>

    <table cellspacing="1" cellpadding="0" width="98%" height="100%" align="center">
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
