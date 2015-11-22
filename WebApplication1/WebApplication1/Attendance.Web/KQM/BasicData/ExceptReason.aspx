<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptReason.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.ExceptReason" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>KQMExceptreasonForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script type="text/javascript">
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridDegree_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(igtbl_getElementById("hidOperate").value=="" && row != null)
			{
				igtbl_getElementById("txtReasonNo").value=row.getCell(0).getValue();
				igtbl_getElementById("txtReasonName").value=row.getCell(1).getValue();
				igtbl_getElementById("txtSalaryFlag").value=row.getCell(2).getValue();
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
    </script>

</head>
<body onload="return OnLoad()">
    <form id="Form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
    <input id="ModuleCode" type="hidden" name="ProcessFlag" runat="server">
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
                    <div id="img_edit">
                        <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_1">
        <asp:Panel ID="pnlContent" runat="server">
            <table class="table_data_area">
                <tr>
                    <td width="14%">
                        &nbsp;<asp:Label ID="lblReasonNo" runat="server" ForeColor="Blue">ReasonNo:</asp:Label>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="txtReasonNo" class="input_textBox_1" runat="server" Width="100%"
                            MaxLength="1" onkeyup="value=value.replace(/[^a-zA-Z]/,'')"></asp:TextBox>
                    </td>
                    <td width="10%">
                        &nbsp;<asp:Label ID="lblReasonName" runat="server" ForeColor="Blue">ReasonName:</asp:Label>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="txtReasonName" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="10%">
                        &nbsp;<asp:Label ID="lblSalaryFlag" runat="server" ForeColor="Blue">SalaryFlag:</asp:Label>
                    </td>
                    <td width="18%">
                        <asp:TextBox ID="txtSalaryFlag" class="input_textBox_1" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="width: 100%">
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlShowPanel" runat="server">
                        <asp:Button ID="btnCondition" runat="server" class="button_2" OnClientClick="return condition_click()">
                        </asp:Button>
                        <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                        </asp:Button>
                        <asp:Button ID="btnAdd" runat="server" class="button_2" OnClientClick="return add_click()">
                        </asp:Button>
                        <asp:Button ID="btnModify" runat="server" class="button_2" OnClientClick="return  modify_click()">
                        </asp:Button>
                        <asp:Button ID="btnDelete" runat="server" class="button_2" OnClientClick="return  delete_click()"
                            OnClick="btnDelete_Click"></asp:Button>
                        <asp:Button ID="btnSave" runat="server" class="button_2" OnClick="btnSave_Click" OnClientClick="return save_click()">
                        </asp:Button>
                        <asp:Button ID="btnCancel" runat="server" class="button_2" OnClientClick="return  cancel_click()">
                        </asp:Button>
                        <asp:Button ID="btnDisable" runat="server" class="button_2" OnClick="btnDisable_Click"
                            OnClientClick="return  disable_click()"></asp:Button>
                        <asp:Button ID="btnEnable" runat="server" class="button_2" OnClick="btnEnable_Click"
                            OnClientClick="return  enable_click()"></asp:Button>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidOperate" runat="server" />
        <div>
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*65/100+";'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridDegree" runat="server" Width="100%" Height="100%">
                                <DisplayLayout CompactRendering="False" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    BorderCollapseDefault="Separate" AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle"
                                    AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridDegree"
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
                                    <igtbl:UltraGridBand BaseTableName="kqm_exceptreason" Key="kqm_exceptreason">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="REASONNO" Key="REASONNO" IsBound="false" Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvReasonNo %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="REASONNAME" Key="REASONNAME" IsBound="false"
                                                Width="40%">
                                                <Header Caption="<%$Resources:ControlText,gvReasonName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="SALARYFLAG" Key="SALARYFLAG" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvSalaryFlag %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvModifier %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_date" Key="update_date" IsBound="false"
                                                Width="20%">
                                                <Header Caption="<%$Resources:ControlText,gvModifyDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EFFECTFLAG" Key="EFFECTFLAG" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="gvEffectFlag">
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

    <script type="text/javascript">
        function removeValue()
        {
          $(".input_textBox_1").attr("value","");
        }
    	function addreadonly()
    	{		
	        igtbl_getElementById("txtReasonNo").readOnly=true;
		    igtbl_getElementById("txtReasonName").readOnly=true;
		    igtbl_getElementById("txtSalaryFlag").readOnly=true;
		}
		function removereadonly()
    	{
    	    $(".img_hidden").show();
	        igtbl_getElementById("txtReasonNo").readOnly=false;
		    igtbl_getElementById("txtReasonName").readOnly=false;
		    igtbl_getElementById("txtSalaryFlag").readOnly=false;
		    $(".input_textBox_1").css("border-style", "solid");
            $(".input_textBox_1").css("background-color", "Cornsilk");
		    if (igtbl_getElementById("hidOperate").value=="Modify")
            {
                igtbl_getElementById("txtReasonNo").readOnly=true;
                $("#<%=txtReasonNo.ClientID%>").css("border-style", "none");
		    }
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
           $("#<%=btnEnable.ClientID %>").attr("disabled","true");
           $("#<%=btnDisable.ClientID %>").attr("disabled","true");
      }
      function OnLoad()
      {
           $(".input_textBox_1").css("border-style", "none");
           addreadonly();
           $("#<%=btnCancel.ClientID%>").attr("disabled","true");
           $("#<%=btnSave.ClientID %>").attr("disabled","true");
           return true;
      }
      function condition_click()
      {
        $("#<%=hidOperate.ClientID%>").val("Condition");
        removereadonly();
        removeValue();
        addbtnDisable();
        $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
        $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
        return false;
      }
      function add_click()
      {
        $("#<%=hidOperate.ClientID%>").val("Add");
        removereadonly();
        removeValue();
        addbtnDisable();
        $("#<%=btnSave.ClientID %>").removeAttr("disabled");
        $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
        return false;
      }
      function modify_click()
      {
           $("#<%=hidOperate.ClientID %>").val("Modify");
           removereadonly();
           var ReasonNo = $.trim($("#<%=txtReasonNo.ClientID%>").val());
           if (ReasonNo=="")
           {alert(Message.AtLastOneChoose); $("#<%=hidOperate.ClientID %>").val("");return false;}
           addbtnDisable();
           $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
           $("#<%=btnSave.ClientID %>").removeAttr("disabled");
           return false;
      }
      
     function  cancel_click()
     {
           addreadonly();
           $(".input_textBox_1").css("border-style", "none");
           $("#<%=hidOperate.ClientID %>").val("");
           $("#<%=btnCondition.ClientID%>").removeAttr("disabled");
           $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
           $("#<%=btnAdd.ClientID%>").removeAttr("disabled");
           $("#<%=btnModify.ClientID%>").removeAttr("disabled");
           $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
           $("#<%=btnEnable.ClientID%>").removeAttr("disabled");
           $("#<%=btnDisable.ClientID %>").removeAttr("disabled");
           $("#<%=btnCondition.ClientID%>").removeAttr("disabled");       
           $("#<%=btnCancel.ClientID %>").attr("disabled","true");
           $("#<%=btnSave.ClientID %>").attr("disabled","true");
           return false;
     }
     function enable_click()
     {
            addreadonly();
            $("#<%=hidOperate.ClientID %>").val("Enable");
            var ReasonNo = $.trim($("#<%=txtReasonNo.ClientID%>").val());
            if (ReasonNo=="")
            {alert(Message.AtLastOneChoose); $("#<%=hidOperate.ClientID %>").val("");return false;}
            else{if (confirm(Message.EnableConfirm))
            {return true;}else{return false;}}
     }
     function disable_click()
     {      
            addreadonly();
            $("#<%=hidOperate.ClientID %>").val("Disable");
            var ReasonNo = $.trim($("#<%=txtReasonNo.ClientID%>").val());
            if (ReasonNo=="")
            {alert(Message.AtLastOneChoose); $("#<%=hidOperate.ClientID %>").val("");return false;}
            else{if (confirm(Message.DisableConfirm))
            {return true;}else{return false;}}
     }
     function delete_click()
     {
            addreadonly();
            $("#<%=hidOperate.ClientID %>").val("Delete");
            var ReasonNo = $.trim($("#<%=txtReasonNo.ClientID%>").val());
            if (ReasonNo=="")
            {alert(Message.AtLastOneChoose);  $("#<%=hidOperate.ClientID %>").val("");return false;}
            else
            {if (confirm(Message.DeleteConfirm))
            {return true;}else{return false;}} 
     }
     function save_click()
     {
           var activeFlag= $("#<%=hidOperate.ClientID %>").val();
           var reasonNo=$.trim($("#<%=txtReasonNo.ClientID%>").val());
           var reasonName=$.trim($("#<%=txtReasonName.ClientID%>").val());
           var salaryFlag=$.trim($("#<%=txtSalaryFlag.ClientID%>").val());
           if(reasonNo.length>0)
           {
                 var result=0;
                 $.ajax({type: "post", url: "ExceptReason.aspx", dataType: "text", data: {ReasonNo: reasonNo, ActiveFlag: activeFlag},async:false,
                         success: function(msg) {
                                if (msg==0) {alert(Message.NotOnlyOne);result=0; } 
                                else{result=1;}}
                       }); 
                  if(result==0){return false;} 
                  else{
                         if(reasonName.length>0)
                         {
                              if(salaryFlag.length>0)
                              {
                                if ((salaryFlag != "Y") && (salaryFlag != "N"))
                                    {alert(Message.OnlyYOrN);return false;}
                                    else
                                    {if (confirm(Message.SaveConfirm)){return true;} else{return false;}}
                              }
                              else
                              {alert(Message.IsSalaryNoNull);return false;}
                         }
                         else
                         {alert(Message.ExceptionNameNoNull);return false;}
                      }        
           }
           else
           {alert(Message.ExceptionCodeNoNull);return false;}
     }
    </script>

</body>
</html>
