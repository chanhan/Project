<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmManagerInfoForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.HrmManagerInfoForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics2.WebUI.UltraWebTab.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>HrmManagerInfoForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

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
         $("#<%=btnImport.ClientID %>").attr("disabled","true");
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         return false;
     } 
    
    function removeValue()
    {
      $(".input_textBox").attr("value","");
      $(".notput_textBox_noborder").attr("value","");
      $("#<%=ddlManager.ClientID %>").attr("value","");
    }
    
    function removeReadonly()
    { 
      $(".input_textBox").css("border-style", "solid");
      $(".img_hidden").show();
      $("#<%=txtWorkNo.ClientID %>").removeAttr("readonly");
      $("#<%=txtNotes.ClientID %>").removeAttr("readonly");
      $("#<%=ddlManager.ClientID %>").removeAttr("disabled");
      $("#<%=txtDeputy.ClientID %>").removeAttr("readonly");
      $("#<%=txtDeputyNotes.ClientID %>").removeAttr("readonly");
      $("#<%=chkIsDirectlyUnder.ClientID %>").parent("span").removeAttr("disabled");
      $("#<%=chkIsBGAudit.ClientID %>").parent("span").removeAttr("disabled");
      $("#PanelImport").hide();
    }
    
    function  addcolor()
    {
        $("#<%=txtWorkNo.ClientID %>").css("background-color", "Cornsilk");
        $("#<%=txtDepName.ClientID %>").css("background-color", "Cornsilk");
    }
    
    function add_click()
    {
         $("#<%=hidOperate.ClientID %>").attr("value","add")
         removeValue();
         removeReadonly();
         addcolor();
         $("#<%=txtWorkNo.ClientID %>").removeAttr("readonly");
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnImport.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");
          $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         return false;
    
    }
    
    
    function modify_click()
    {
    if($("#<%=txtWorkNo.ClientID %>").val()!="")
    {
         $("#<%=hidOperate.ClientID %>").attr("value","modify")
         removeReadonly();
         addcolor();
         $("#<%=btnCondition.ClientID %>").attr("disabled","true");
         $("#<%=btnQuery.ClientID %>").attr("disabled","true");
         $("#<%=btnAdd.ClientID %>").attr("disabled","true");
         $("#<%=btnDelete.ClientID %>").attr("disabled","true");
         $("#<%=btnModify.ClientID %>").attr("disabled","true");
         $("#<%=btnImport.ClientID %>").attr("disabled","true");
         $("#<%=btnSave.ClientID %>").removeAttr("disabled");
         $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
         return false;
     }
     else
     {
      alert(Message.AtLastOneChoose);
      return false;
     }
    }
    
    function  checkDelete()//刪除
    {
    var grid = igtbl_getGridById('UltraWebGridManagerInfo');
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(igtbl_getElementById("UltraWebGridManagerInfo_ci_0_0_"+i+"_CheckBoxCell").checked)
			    {
			         Count+=1;			        
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.AtLastOneChoose);
		        return false;
		    }
	       return confirm(Message.DeleteConfirm);

    }
    
    function save_click()
    {

       if ( $("#<%=txtDepCode.ClientID %>").val()=="")
       {
            alert(Message.DepCodeNotNull);
            return false;
       }
       else if ($("#<%=txtWorkNo.ClientID %>").val()=="")
       {
            alert(Message.WorkNoNotNull);
            return false;
       }
       else if ($("#<%=txtNotes.ClientID %>").val()=="")
       {
            alert(Message.NotesNotNull);
            return false;
       }
       else     if ($("#<%=hidOperate.ClientID %>").val()=="add")
    {
     $.ajax({type: "post", url: "HrmManagerInfoForm.aspx", dateType: "text", data: {WorkNo: $("#<%=txtWorkNo.ClientID%>").val(), DepCode:$("#<%=txtDepCode.ClientID %>").val()},async:false,
                             success: function(msg) {
                                    if (msg!=0) {alert(Message.NotOnlyOne);result=0; } 
                                    else{result=1;}}
                           }); 
                        if(result==0){return false;} 
    }
    else
       {
            return true;
       }
    }

         function cancel_click()
         {
            $(".img_hidden").hide();
         } 
    
            function vailWorkNo()
            {
               if ($("#<%=hidOperate.ClientID %>").val()!="condition")
               {
                var empno;
                empno=$.trim($("#<%=txtWorkNo.ClientID %>").val());
                    if (empno!="")
                        {
                            $.ajax(
                                   {
                                     type:"post",url:"HrmManagerInfoForm.aspx",dataType:"json",data:{Empno:empno},  
                                     success:function(msg) 
                                        {
                                        if (msg.LocalName!=null)
                                        {
                                            $("#<%=txtLocalName.ClientID %>").val(msg.LocalName);
                                            $("#<%=ddlManager.ClientID %>").val(msg.Manager);
                                             $("#<%=txtNotes.ClientID %>").val(msg.Notes);
                                        }
                                        else
                                        {
                                        $("#<%=txtWorkNo.ClientID %>").val("");
                                        $("#<%=txtWorkNo.ClientID %>").val("");
                                        alert(Message.EmpNotExist);
                                        }
                                        }
                                   });
                        }
                        return false;
                }
             }


            function vailDeputy()
            {
               if ($("#<%=hidOperate.ClientID %>").val()!="condition")
               {
                var empno;
                empno=$.trim($("#<%=txtDeputy.ClientID %>").val());
                    if (empno!="")
                        {
                            $.ajax(
                                   {
                                     type:"post",url:"HrmManagerInfoForm.aspx",dataType:"json",data:{Deputy:empno},  
                                     success:function(msg) 
                                        {
                                        if (msg.DeputyName!=null)
                                        {
                                            $("#<%=txtDeputyName.ClientID %>").val(msg.DeputyName);
 
                                        }
                                        else
                                        {
                                        $("#<%=txtDeputy.ClientID %>").val("");
                                        alert(Message.EmpNotExist);
                                        }
                                        }
                                   });
                        }
                        return false;
                }
             }
       function setSelector(ctrlCode,ctrlName,flag,moduleCode)
       {
           var code=$("#"+ctrlCode).val();
           if (flag=="DepCode")
           {
           var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+moduleCode;
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
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $("#img_edit").attr("src","../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $("#img_edit").attr("src","../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
       $(function(){
        $("#img_grid,#td_show_1,#td_show_2").toggle(
            function(){
                $("#div_grid").hide();
                $("#img_grid").attr("src","../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_grid").show();
                $("#img_grid").attr("src","../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   }); 
   
       $(function(){
        $("#div_img_3,#tr2").toggle(
            function(){
                $("#div_import").hide();
                $("#div_img_3").attr("src","../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_import").show();
                $("#div_img_3").attr("src","../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
   
   
        function CheckAll()
		{
			var sValue=false;
			var chk=document.getElementById("UltraWebGridManagerInfo_ctl00_CheckBoxAll");
			if(chk.checked)
			{
				sValue=true;
			}				
			var grid = igtbl_getGridById('UltraWebGridManagerInfo');
			var gRows = grid.Rows;
			for(i=0;i<gRows.length;i++)
			{
				if(!igtbl_getElementById("UltraWebGridManagerInfo_ci_0_0_"+i+"_CheckBoxCell").disabled)
				{
				    igtbl_getElementById("UltraWebGridManagerInfo_ci_0_0_"+i+"_CheckBoxCell").checked=sValue;
				}
			}
		}
		function GetEmp()
		{
            var EmployeeNo=document.getElementById("txtEmployeeNo").value;
            if(EmployeeNo.length>0&&(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify"))
            {
               return;
            }
		}
		function docallbackemp(response)
		{  
            var dsapply = response.value;
            if(dsapply != null && typeof(dsapply) == "object" && dsapply.Tables != null && dsapply.Tables[0].Rows.length>0)
            {
                for(var i=0; i<dsapply.Tables[0].Rows.length; i++)
                {
                    document.getElementById("txtLocalName").value=dsapply.Tables[0].Rows[i].LOCALNAME==null?"":dsapply.Tables[0].Rows[i].LOCALNAME;
                    document.getElementById("txtNotes").value=dsapply.Tables[0].Rows[i].NOTES==null?"":dsapply.Tables[0].Rows[i].NOTES;
                    document.getElementById("ddlManager").value=dsapply.Tables[0].Rows[i].MANAGERNAME==null?"":dsapply.Tables[0].Rows[i].MANAGERNAME;
                    document.getElementById("HiddenEmpFlag").value=dsapply.Tables[0].Rows[i].FLAG==null?"":dsapply.Tables[0].Rows[i].FLAG;
                  }
            }
            else
            {
                document.getElementById("txtEmployeeNo").value="";
                document.getElementById("txtLocalName").value="";
                document.getElementById("txtNotes").value="";
                document.getElementById("ddlManager").value="";
                document.getElementById("HiddenEmpFlag").value="";
              
            }        
            return;		
		}
		function GetDeputy()
		{
            var Deputy=document.getElementById("txtDeputy").value;
            if(Deputy.length==0&&(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify"))
            {
                document.getElementById("txtDeputyName").value="";
                document.getElementById("txtDeputyNotes").value="";
            }
            if(Deputy.length>0&&(document.getElementById("ProcessFlag").value=="Add"||document.getElementById("ProcessFlag").value=="Modify"))
            {
                
            }
		}
		function docallbackdeputy(response)
		{  
            var dsapply = response.value;
            if(dsapply != null && typeof(dsapply) == "object" && dsapply.Tables != null && dsapply.Tables[0].Rows.length>0)
            {
                for(var i=0; i<dsapply.Tables[0].Rows.length; i++)
                {
                   document.getElementById("txtDeputyName").value=dsapply.Tables[0].Rows[i].LOCALNAME==null?"":dsapply.Tables[0].Rows[i].LOCALNAME;
                   document.getElementById("txtDeputyNotes").value=dsapply.Tables[0].Rows[i].NOTES==null?"":dsapply.Tables[0].Rows[i].NOTES;
                    document.getElementById("HiddenDeputyFlag").value=dsapply.Tables[0].Rows[i].FLAG==null?"":dsapply.Tables[0].Rows[i].FLAG;
                 }
            }
            else
            {
                document.getElementById("txtDeputy").value="";
                document.getElementById("txtDeputyName").value="";
                document.getElementById("txtDeputyNotes").value="";
                document.getElementById("HiddenDeputyFlag").value="";
                
            }        
            return;		
		}		
		
		function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);
			DisplayRowData(row);
			return 0;
		}
		function UltraWebGridManagerInfo_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
		function DisplayRowData(row)
		{
			if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
			{
			    document.getElementById("txtDepname").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
				document.getElementById("txtDepcode").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				document.getElementById("HiddenDepCode").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
				document.getElementById("txtWorkNo").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
			    document.getElementById("HiddenWorkNo").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
				document.getElementById("txtLocalName").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
				document.getElementById("ddlManager").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
				document.getElementById("txtNotes").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();
				document.getElementById("txtDeputy").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
				document.getElementById("txtDeputyName").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
				document.getElementById("ChkIsDirectlyUnder").checked=row.getCell(7).getValue()=="Y"?true:false;
				document.getElementById("HiddenManagerCode").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
				document.getElementById("HiddenIsDirectlyUnder").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
				document.getElementById("txtDeputyNotes").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue();
				document.getElementById("HiddenEmpFlag").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();
				document.getElementById("HiddenDeputyFlag").value=row.getCell(14).getValue()==null?"":row.getCell(14).getValue();
				document.getElementById("chkIsBGAudit").checked=row.getCell(12).getValue()=="Y"?true:false;
				document.getElementById("HiddenIsBGAudit").value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue();
			}
		}
    </script>

</head>
<body class="color_Body">
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenManagerCode" type="hidden" name="HiddenManagerCode" runat="server" />
    <input id="HiddenIsDirectlyUnder" type="hidden" name="HiddenIsDirectlyUnder" runat="server" />
    <input id="HiddenIsBGAudit" type="hidden" name="HiddenIsBGAudit" runat="server" />
    <input id="HiddenEmpFlag" type="hidden" name="HiddenEmpFlag" runat="server" />
    <input id="HiddenDeputyFlag" type="hidden" name="HiddenDeputyFlag" runat="server" />
    <input id="HiddenDepCode" type="hidden" name="HiddenDepCode" runat="server" />
    <input id="HiddenWorkNo" type="hidden" name="HiddenWorkNo" runat="server" />
    <table class="top_table" cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr style="cursor: hand" id="tr1">
                        <%--      <td class="tr_table_title" height="25">
                            <asp:Label ID="lblEdit" runat="server"></asp:Label>
                        </td>
                        <td class="tr_table_title" align="right">
                            <img id="img_edit" src="../CSS/Images/uparrows_white.gif">
                        </td>--%>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                background-repeat: no-repeat;  width: 75px; text-align: center;
                                font-size: 13px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 22px;">
                            <img id="img_edit" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlContent" runat="server">
                    <div id="div_edit">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <asp:HiddenField ID="hidOperate" runat="server" />
                            <asp:HiddenField ID="HiddenAjaxFlag" runat="server" />
                            <asp:HiddenField ID="ImportFlag" runat="server" />
                            <tr class="tr_data_1">
                                <td class="td_label" width="11%">
                                    &nbsp;<asp:Label ID="lblDepcode" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDepCode" runat="server" Width="100%" CssClass="input_textBox"
                                                    Style="display: none"></asp:TextBox>
                                            </td>
                                            <td width="100%">
                                                <asp:TextBox ID="txtDepName" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="cursor: hand">
                                                &nbsp;
                                                <asp:Image ID="imgDepCode" runat="server" CssClass="img_hidden" ImageUrl="../CSS/Images_new/search_new.gif">
                                                </asp:Image>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="td_label" width="67%" colspan="4">
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td class="td_label" width="11%">
                                    &nbsp;<asp:Label ID="lblWorkNo" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" CssClass="input_textBox"
                                        onblur="return vailWorkNo()"></asp:TextBox>
                                </td>
                                <td class="td_label" width="11%">
                                    &nbsp;<asp:Label ID="lblLocalName" runat="server"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" CssClass="notput_textBox_noborder"></asp:TextBox>
                                </td>
                                <td class="td_label" width="11%">
                                    &nbsp;
                                    <asp:Label ID="lblManager" runat="server"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <asp:DropDownList ID="ddlManager" Width="90%" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td class="td_label" width="11%">
                                    &nbsp;<asp:Label ID="lblNotes" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_input" width="56%" colspan="3">
                                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                                <td class="td_label" width="33%" colspan="2">
                                    &nbsp;
                                    <asp:CheckBox ID="chkIsDirectlyUnder" runat="server" />
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td class="td_label" width="11%">
                                    &nbsp;<asp:Label ID="lblDeputy" runat="server"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <asp:TextBox ID="txtDeputy" runat="server" Width="100%" CssClass="input_textBox"
                                        onblur="return vailDeputy()"></asp:TextBox>
                                </td>
                                <td class="td_label" width="11%">
                                    &nbsp;
                                    <asp:Label ID="lblDeputyName" runat="server"></asp:Label>
                                </td>
                                <td class="td_input" width="22%">
                                    <asp:TextBox ID="txtDeputyName" runat="server" Width="100%" CssClass="notput_textBox_noborder"></asp:TextBox>
                                </td>
                                <td class="td_label" width="33%" colspan="2">
                                    &nbsp;
                                    <asp:CheckBox ID="chkIsBGAudit" runat="server" />
                                </td>
                            </tr>
                            <tr class="tr_data_1">
                                <td class="td_label">
                                    &nbsp;<asp:Label ID="lblDeputyNotes" runat="server"></asp:Label>
                                </td>
                                <td class="td_input" colspan="3">
                                    <asp:TextBox ID="txtDeputyNotes" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tr_data_2">
                                <td class="td_label" colspan="6">
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
                                            CssClass="button_2" OnClientClick="return checkDelete()" OnClick="btnDelete_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                            CssClass="button_2" OnClick="btnCancel_Click" OnClientClick="return cancel_click()"></asp:Button>
                                         <asp:Button ID="btnSave" runat="server" CssClass="button_2" OnClientClick="return save_click()" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnImport" runat="server" Text="<%$Resources:ControlText,btnImport %>"
                                            CssClass="button_2" OnClick="btnImport_Click"></asp:Button>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel class="inner_table" ID="PanelData" runat="server" Width="100%">
        <table cellspacing="1" cellpadding="0" width="98%" align="center">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                        <tr style="width: 100%;" id="tr_edit">
                            <td style="width: 100%;" id="td_show_1" class="tr_title_center">
                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                    background-repeat: no-repeat;  width: 75px; text-align: center;
                                    font-size: 13px;">
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
                                        ImagePath="../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                        DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                        ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../CSS/Images_new/search01.gif"
                                        OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2' >總記錄數：</font>%recordCount%">
                                    </ess:AspNetPager>
                                </div>
                            </td>
                            <td style="width: 22px;" id="td_show_2">
                                <img id="img_grid" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="div_grid">
                        <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                            <tr style="width: 100%">
                                <td valign="top" width="19px" background="../../CSS/Images_new/EMP_05.gif" height="18">
                                    <img height="18" src="../../CSS/Images_new/EMP_01.gif" width="19">
                                </td>
                                <td background="../../CSS/Images_new/EMP_07.gif" height="19px">
                                </td>
                                <td valign="top" width="19px" background="../CSS/Images_new/EMP_06.gif" height="18">
                                    <img height="18" src="../CSS/Images_new/EMP_02.gif" width="19">
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="19" background="../../CSS/Images_new/EMP_05.gif">
                                    &nbsp;
                                </td>
                                <td>

                                    <script type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*52/100+"'>");</script>

                                    <igtbl:UltraWebGrid ID="UltraWebGridManagerInfo" runat="server" Width="100%" Height="100%">
                                        <DisplayLayout UseFixedHeaders="True" CompactRendering="False" StationaryMargins="Header"
                                            AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                            AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridManagerInfo" TableLayout="Fixed"
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
                                            <igtbl:UltraGridBand BaseTableName="HRM_OrgManager" Key="HRM_OrgManager">
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
                                                    <igtbl:UltraGridColumn BaseColumnName="DepName" Key="DepName" IsBound="false" Width="120">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadDepName%>" Fixed="True">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="DepCode" Key="DepCode" IsBound="false" Width="0"
                                                        Hidden="true">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadDepCode%>" Fixed="True">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="WorkNo" Key="WorkNo" IsBound="false" Width="80">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>" Fixed="True">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="LocalName" Key="LocalName" IsBound="false"
                                                        Width="80">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadLocalName%>" Fixed="True">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="ManagerName" Key="ManagerName" IsBound="false"
                                                        Width="80">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadManagerName%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="ManagerCode" Key="ManagerCode" IsBound="false"
                                                        Width="0" Hidden="true">
                                                        <Header Caption="ManagerCode">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="IsDirectlyUnder" Key="IsDirectlyUnder" IsBound="false"
                                                        Width="60">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadIsDirectlyUnder%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Notes" Key="Notes" IsBound="false" Width="150">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadNotes%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Deputy" Key="Deputy" IsBound="false" Width="80">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadDeputy%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="DeputyName" Key="DeputyName" IsBound="false"
                                                        Width="80">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadDeputyName%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="DeputyNotes" Key="DeputyNotes" IsBound="false"
                                                        Width="150">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadNotes%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="IsBGAudit" Key="IsBGAudit" IsBound="false"
                                                        Width="120">
                                                        <Header Caption="<%$Resources:ControlText,gvHeadIsBGAudit%>">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Flag" Key="Flag" IsBound="false" Width="0"
                                                        Hidden="true">
                                                        <Header Caption="Flag">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="DeputyFlag" Key="DeputyFlag" IsBound="false"
                                                        Width="0" Hidden="true">
                                                        <Header Caption="DeputyFlag">
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
    </asp:Panel>
    <table cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <asp:Panel class="img_hidden" ID="PanelImport" runat="server" Width="100%">
                    <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                        <tr style="cursor: hand">
                            <%--    <td class="tr_table_title" width="100%">
                            <asp:Label ID="lblImport" runat="server"></asp:Label>
                        </td>
                        <td class="tr_table_title" align="right">
                            <img id="div_img_3" src="../CSS/Images/uparrows_white.gif" alt="" />
                        </td>--%>
                            <td>
                                <table cellspacing="0" cellpadding="0" class="table_title_area">
                                    <tr style="width: 100%;" id="tr2">
                                        <td style="width: 100%;" class="tr_title_center">
                                            <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                                background-repeat: no-repeat;  width: 75px; text-align: center;
                                                font-size: 13px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblImport" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 22px;">
                                            <img id="div_img_3" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" width="100%" align="left" colspan="2">
                                <div id="div_import">
                                    <table border="1">
                                        <tr>
                                            <td class="td_label" width="100%" align="left" colspan="2">
                                                <table>
                                                    <tr>
                                                        <td width="20%">
                                                            <a href="/ExcelModel/HrmOrgManagerSample.xls">&nbsp;<asp:Label ID="lblUploadText"
                                                                runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </td>
                                                        <td width="55%">
                                                            <asp:FileUpload ID="FileUpload" runat="server" Width="100%" />
                                                        </td>
                                                        <td width="5%">
                                                            <asp:Button ID="btnImportSave" runat="server" Text="<%$Resources:ControlText,btnImportSave %>"
                                                                CssClass="button_2" OnClick="btnImportSave_Click"></asp:Button>
                                                        </td>
                                                        <td width="5%" align="left">
                                                            <asp:Button ID="btnExport" runat="server" Text="<%$Resources:ControlText,btnExport %>"
                                                                CssClass="button_2" OnClick="btnExport_Click"></asp:Button>
                                                        </td>
                                                        <td width="15%" align="left">
                                                            <asp:Label ID="lblupload" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%" colspan="2">
                                                            <asp:Label ID="lbluploadMsg" runat="server" ForeColor="red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="height: 25;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
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

                                                            <script type="text/javascript">document.write("<DIV id='div_3' style='height:"+document.body.clientHeight*52/100+"'>");</script>

                                                            <igtbl:UltraWebGrid ID="UltraWebGridImport" runat="server" Width="100%" Height="100%">
                                                                <DisplayLayout CompactRendering="False" StationaryMargins="Header" AllowSortingDefault="Yes"
                                                                    UseFixedHeaders="true" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridImport" TableLayout="Fixed"
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
                                                                    <igtbl:UltraGridBand BaseTableName="HRM_OrgManager" Key="HRM_OrgManager">
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ErrorMsg" Key="ErrorMsg" IsBound="false" Width="150">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadErrorMsg%>" Fixed="True">
                                                                                </Header>
                                                                                <CellStyle ForeColor="red">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DepName" HeaderText="DepName" IsBound="false"
                                                                                Key="DepName" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadDepName%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DepCode" HeaderText="DepCode" IsBound="false"
                                                                                Key="DepCode" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadDepCode%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <%-- <igtbl:UltraGridColumn BaseColumnName="DepCode" HeaderText="DepCode" IsBound="false"
                                                        Key="DepCode" Width="100" Hidden="true">
                                                        <Header Caption="DepCode" Fixed="True">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>--%>
                                                                            <igtbl:UltraGridColumn BaseColumnName="WorkNo" HeaderText="WorkNo" IsBound="false"
                                                                                Key="WorkNo" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadWorkNo%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Notes" HeaderText="Notes" IsBound="false"
                                                                                Key="Notes" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadNotes%>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IsDirectlyUnder" HeaderText="IsDirectlyUnder"
                                                                                IsBound="false" Key="IsDirectlyUnder" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadIsDirectlyUnder%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IsBGAudit" HeaderText="IsBGAudit" IsBound="false"
                                                                                Key="IsBGAudit" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadIsBGAudit%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Deputy" HeaderText="Deputy" IsBound="false"
                                                                                Key="Deputy" Width="100">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadDeputy%>" Fixed="True">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DeputyNotes" HeaderText="DeputyNotes" IsBound="false"
                                                                                Key="ProxyNotes" Width="150">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadProxyNotes%>" Fixed="True">
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
    </table>
    </asp:Panel> </tr> </table>
    </form>
</body>
</html>
