<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTMTypeForm.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.OTMTypeForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script>
  function DisableClick()
    {
    if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
		      return false;
	     	 }
	     	 return true;
    }
 function   EnableClick()
 {
         	if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
		      return false;
	     	 }
	     	 return true;
 }
    function DeleteClick()
    {
        	if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
		      return false;
	     	 }
	     	 else
	     	 {
            return confirm(Message.DeleteAttTypeConfirm);
           }
    }
  function  ModifyClick()
    {
    	   if( $('#<%=ProcessFlag.ClientID %>').val()=='N')
		     {
		      alert(Message.AtLastOneChoose);
	     	 }
	     	 else
	     	 {
	     	      $(':text').removeClass('input_textBox');  
	            $("#<%=hidOperate.ClientID %>").val("Modify");
		        $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>,#<%=btnCancel.ClientID %>,#<%=btnSave.ClientID %>,#<%=btnDisable.ClientID %>,#<%=btnEnable.ClientID %>").each(function()
                   {
                      $(this).attr("disabled",true);
                   });
                   $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>,#<%=btnDisable.ClientID %>,#<%=btnEnable.ClientID %>,#<%=txtG13mLimit.ClientID %>,").each(function()
                   {
                      $(this).attr("disabled",false);
                 });
                 $('#<%=txtOttypeDetail.ClientID %>,#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>,#<%=txtRemark.ClientID %>').each(function()
                 {
                     $(this).attr('readonly',false);
                });
            }
            return false;
    }
    	function AfterSelectChange(gridName, id)
		{
			var row = igtbl_getRowById(id);	
			DisplayRowData(row);
			return 0;
		}
				function UltraWebGridOTMType_InitializeLayoutHandler(gridName)
		{
			var row = igtbl_getActiveRow(gridName);
			DisplayRowData(row);
		}
			function DisplayRowData(row)
		{
		if( $("#<%=hidOperate.ClientID %>").val()!='Add'&&$("#<%=hidOperate.ClientID %>").val()!='Condition')
		{
			igtbl_getElementById("<%=txtOrgCode.ClientID %>").value=row.getCell(12).getValue()==null?"":row.getCell(12).getValue();
		    igtbl_getElementById("<%=txtEffectFlag.ClientID %>").value=row.getCell(11).getValue()==null?"":row.getCell(11).getValue();
		    igtbl_getElementById("<%=txtDepname.ClientID %>").value=row.getCell(0).getValue()==null?"":row.getCell(0).getValue();
		    igtbl_getElementById("<%=ddlIsAllowProject.ClientID %>").value=row.getCell(9).getValue()==null?"":row.getCell(9).getValue();
		    igtbl_getElementById("<%=ddlOttypeCode.ClientID %>").value=row.getCell(1).getValue()==null?"":row.getCell(1).getValue();
		    igtbl_getElementById("<%=txtOttypeDetail.ClientID %>").value=row.getCell(2).getValue()==null?"":row.getCell(2).getValue();
		    igtbl_getElementById("<%=txtG1dLimit.ClientID %>").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue();
		    igtbl_getElementById("<%=txtG2dLimit.ClientID %>").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue();
		    igtbl_getElementById("<%=txtG3dLimit.ClientID %>").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
		    igtbl_getElementById("<%=txtG1mLimit.ClientID %>").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
		    igtbl_getElementById("<%=txtG2mLimit.ClientID %>").value=row.getCell(7).getValue()==null?"":row.getCell(7).getValue();
		    igtbl_getElementById("<%=txtG12mLimit.ClientID %>").value=row.getCell(8).getValue()==null?"":row.getCell(8).getValue();	
		    igtbl_getElementById("<%=txtG13mLimit.ClientID %>").value=row.getCell(13).getValue()==null?"":row.getCell(13).getValue();		    
		    igtbl_getElementById("<%=txtG123mLimit.ClientID %>").value=row.getCell(14).getValue()==null?"":row.getCell(14).getValue();		    
		    igtbl_getElementById("<%=txtRemark.ClientID %>").value=row.getCell(10).getValue()==null?"":row.getCell(10).getValue();
		   $('#<%=ProcessFlag.ClientID %>').val('Y');
		   $('#HiddenOTTypeCode').val(row.getCell(1).getValue());
		   }
		}
		$(document).ready(function (){
		$("#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>").each(function(){
		$(this).blur(function(){
		 var regStr= /^\d+(\.\d{1,1})?$/;
		 if($.trim($(this).val()))
		  {
		   if(!regStr.exec($.trim($(this).val())))
          {
           $(this).val(null);
         }
		}
		});
		});
		$("#<%=btnCancel.ClientID %>").click(function()
		{
		   $(':text').addClass('input_textBox');  
		   $("#<%=hidOperate.ClientID %>").val("Cancel");
		   $('#<%=ProcessFlag.ClientID %>').val('N');
		            $('#<%=txtDepname.ClientID %>,#<%=txtOttypeDetail.ClientID %>,#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>,#<%=txtRemark.ClientID %>').each(function(){
            $(this).attr('readonly',true);
            $(this).val(null);
            });
            $("#<%=ddlIsAllowProject.ClientID %>").get(0).selectedIndex=0
            $("#<%=ddlOttypeCode.ClientID %>").get(0).selectedIndex=0
           $("#<%=ddlIsAllowProject.ClientID %>").attr("disabled",true);
           $("#<%=ddlOttypeCode.ClientID %>").attr("disabled",true);     
              $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>,#<%=btnCancel.ClientID %>,#<%=btnSave.ClientID %>,#<%=btnDisable.ClientID %>,#<%=btnEnable.ClientID %>").each(function()
             {
             $(this).attr("disabled",false);
             });
           $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           $("#<%=btnSave.ClientID %>").attr("disabled",true);
           return false;
		});
				$("#<%=btnSave.ClientID %>").click(function()
		
		{
		  var valid=true;
		   $("#<%=txtDepname.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>").each(function() {
		 
                        if ($.trim($(this).val())) 
                        { $(this).css("border-color", "silver"); }
                        else
                         { valid = false; $(this).css("border-color", "#ff6666"); }
                    });
                    if($("#<%=ddlOttypeCode.ClientID %>").get(0).selectedIndex==0)
                    {
                    valid = false;
                    $("#<%=ddlOttypeCode.ClientID %>").css("background-color", "#ff6666");
                    }
                     if($("#<%=ddlIsAllowProject.ClientID %>").get(0).selectedIndex==0)
                    {
                    valid = false;
                    $("#<%=ddlIsAllowProject.ClientID %>").css("background-color", "#ff6666");
                    }
                    
                        var regStr= /^\d+(\.\d{1,1})?$/;
                        var G1dLimitStr=$.trim($("#<%=txtG1dLimit.ClientID %>").val());
                        var G2dLimitStr=$.trim($("#<%=txtG2dLimit.ClientID %>").val());
                        var G3dLimitStr=$.trim($("#<%=txtG3dLimit.ClientID %>").val());
                        var G1mLimitStr=$.trim($("#<%=txtG1mLimit.ClientID %>").val());
                        var G2mLimitStr=$.trim($("#<%=txtG2mLimit.ClientID %>").val());
                        var G12mLimitStr=$.trim($("#<%=txtG12mLimit.ClientID %>").val());
                        var G13mLimitStr=$.trim($("#<%=txtG13mLimit.ClientID %>").val());
                        var G123mLimitStr=$.trim($("#<%=txtG123mLimit.ClientID %>").val());
                         G1dLimitStr=G1dLimitStr==""?0:G1dLimitStr;
                         G2dLimitStr=G2dLimitStr==""?0:G2dLimitStr;
                         G3dLimitStr=G3dLimitStr==""?0:G3dLimitStr;
                         G1mLimitStr=G1mLimitStr==""?0:G1mLimitStr;
                         G2mLimitStr=G2mLimitStr==""?0:G2mLimitStr;
                         G12mLimitStr=G12mLimitStr==""?0:G12mLimitStr;
                         G13mLimitStr=G13mLimitStr==""?0:G13mLimitStr;
                         G123mLimitStr=G123mLimitStr==""?0:G123mLimitStr;
                                                    
                                          if(parseFloat(G1dLimitStr)<=parseFloat(G1mLimitStr)) 
                                          {
                                             if(parseFloat(G2dLimitStr)<=parseFloat(G2mLimitStr))
                                             {
                                               if(parseFloat(G12mLimitStr)==parseFloat(G1mLimitStr)+parseFloat(G2mLimitStr))
                                               {
                                                  if(parseFloat(G13mLimitStr)>=parseFloat(G1mLimitStr)+parseFloat(G3dLimitStr))
                                                  {
                                                     if(parseFloat(G123mLimitStr)>=parseFloat(G1mLimitStr)+parseFloat(G2mLimitStr)+parseFloat(G3dLimitStr))
                                                     {
                                                     
                                                     }
                                                     else
                                                     {
                                                            valid=false;
                                                            alert(Message.G123mAndG1mAndG2mAndG3d);
                                                     }
                                                  }
                                                  else
                                                  {
                                                               valid=false;
                                                               alert(Message.G13mAndG1dAndG1mAndG3d);
                                                  }
                                               }
                                               else
                                               {
                                                    valid=false;
                                                   alert(Message.G12mAndG1mAndG2m);
                                               }
                                             }
                                             else
                                             {
                                                      valid=false;
                                                   alert(Message.G2mAndG2d);
                                             }
                                          }
                                          else
                                          {
                                            valid=false;
                                           alert(Message.G1mAndG1d);
                                          }        
//                                                  if(parseFloat(G1dLimitStr)>parseFloat(G1mLimitStr))
//                                                    {
//                                                     valid=false;
//                                                       alert(Message.G1mAndG1d);
//                                                   return valid;                                                                                                                                                                                                     
//                                                    }
//                  
//                                                     if(parseFloat(G2dLimitStr)>parseFloat(G2mLimitStr))
//                                                    {
//                                                    valid=false;
//                                                   alert(Message.G2mAndG2d);
//                                                     return valid;     
//                                                     }
//                                                          if(parseFloat(G12mLimitStr)!=parseFloat(G1mLimitStr)+parseFloat(G2mLimitStr))
//                                                              {
//                                                                valid=false;
//                                                               alert(Message.G12mAndG1mAndG2m);
//                                                                    return valid;     
//                                                              }
//                                                           if(parseFloat(G13mLimitStr)<parseFloat(G1mLimitStr)+parseFloat(G3dLimitStr))
//                                                              {
//                                                                valid=false;
//                                                               alert(Message.G13mAndG1dAndG1mAndG3d);
//                                                                    return valid;     
//                                                              }

//                                                              if(parseFloat(G123mLimitStr)<parseFloat(G1mLimitStr)+parseFloat(G2mLimitStr)+parseFloat(G3dLimitStr))
//                                                              {
//                                                             valid=false;
//                                                            alert(Message.G123mAndG1mAndG2mAndG3d);
//                                                               return valid;     
//                                                               }
                     
                     if (valid)
                   {
                      return confirm(Message.SaveConfirm);
                   }
                   return false;
              
		});
			$("#<%=btnAdd.ClientID %>").click(function(){
			 $(':text').removeClass('input_textBox');  
			         $("#<%=hidOperate.ClientID %>").val("Add");
			             $(':text').val(null);
			                 $('select').val(null);
	             $("#<%=btnCondition.ClientID %>,#<%=btnQuery.ClientID %>,#<%=btnAdd.ClientID %>,#<%=btnModify.ClientID %>,#<%=btnDelete.ClientID %>,#<%=btnCancel.ClientID %>,#<%=btnSave.ClientID %>,#<%=btnDisable.ClientID %>,#<%=btnEnable.ClientID %>").each(function()
             {
             $(this).attr("disabled",true);
             });
              
               $("#<%=btnSave.ClientID %>,#<%=btnCancel.ClientID %>,select").each(function()
             {
             $(this).attr("disabled",false);
             });
                         $('#<%=txtDepname.ClientID %>,#<%=txtOttypeDetail.ClientID %>,#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>,#<%=txtRemark.ClientID %>').each(function(){
            $(this).attr('readonly',false);
            });
            return false;
	});
	
		            $("#img_edit").toggle(
                function(){
                    $("#tr_edit").hide();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif");
                 
                },
                function(){
                  $("#tr_edit").show();
                    $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
              $("#img_grid,#img_div").toggle(
                function(){
                    $("#tr_show").hide();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03.gif");
                },
                function(){
                  $("#tr_show").show();
                    $("#div_img_2").attr("src","../../CSS/Images_new/left_back_03_a.gif");
                }
            );
            //頁面加載設置
            $('#<%=txtDepname.ClientID %>,#<%=txtOttypeDetail.ClientID %>,#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>,#<%=txtRemark.ClientID %>').each(function(){
            $(this).attr('readonly',true);
            });
           $("#<%=ddlIsAllowProject.ClientID %>").attr("disabled",true);
           $("#<%=btnCancel.ClientID %>").attr("disabled",true);
           $("#<%=btnSave.ClientID %>").attr("disabled",true);
           
           $("#<%=btnCondition.ClientID %>").click(function(){//條件按鈕
         $(':text').removeClass('input_textBox');           
         $("#<%=hidOperate.ClientID %>").val('Condition');
            $('#<%=txtDepname.ClientID %>,#<%=txtOttypeDetail.ClientID %>,#<%=txtG1dLimit.ClientID %>,#<%=txtG2dLimit.ClientID %>,#<%=txtG3dLimit.ClientID %>,#<%=txtG1mLimit.ClientID %>,#<%=txtG2mLimit.ClientID %>,#<%=txtG12mLimit.ClientID %>,#<%=txtG13mLimit.ClientID %>,#<%=txtG123mLimit.ClientID %>,#<%=txtRemark.ClientID %>').each(
             function(){
                         $(this).attr('readonly',false);
                      });
           $("select").attr("disabled",false);        
             $(".button_1").each(function(){
                $(this).attr("disabled",true);
            });
             $("#<%=btnQuery.ClientID %>,#<%=btnCancel.ClientID %>,#<%=btnEnable.ClientID %>,#<%=btnDisable.ClientID %>").each(function()
             {
             $(this).attr("disabled",false);
             });
            return false;
           });
		});
		function getDeptTree(moduleCode)
	{
	 var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	  var info=window.window.showModalDialog("RelationSelector.aspx?moduleCode="+moduleCode+"&r="+ Math.random(),window,"dialogWidth=300px;dialogHeight=450px;dialogLeft="+X+"px;dialogTop="+Y+";help=no;status=no;scrollbars=no"); 
	 if(info)
	 {
	  var deptcode=info.codeList;
	  var deptname=info.nameList;
	  $("#<%=txtOrgCode.ClientID %>").val(deptcode);
	  $("#<%=txtDepname.ClientID %>").val(deptname);
	  }
	}
    </script>

</head>
<style type="text/css">
    .input_textBox
    {
        border: 0;
        background-color:White;
    }
    .ddl_hiden
    {
        display: none;
    }
</style>
<body class="color_Body">
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="ProcessFlag" runat="server" Value="N" />
    <input id="HiddenOTTypeCode" type="hidden" name="HiddenOTTypeCode" runat="server" />
    <input id="HiddenG123Limit" type="hidden" name="HiddenG123Limit" runat="server" />
    <div style="width: 100%;">
        <table cellspacing="0" cellpadding="0" class="table_title_area">
            <tr style="width: 100%;" id="img_edit">
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
                    <div>
                        <img id="div_img_1" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
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
                                    <asp:Panel ID="inputPanel" runat="server">
                                        <asp:HiddenField ID="hidOperate" runat="server" Value="" />
                                        <tr>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblBUCode" runat="server" ForeColor="Blue">Department:</asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 90%">
                                                            <asp:TextBox ID="txtDepname" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="cursor: hand; width: 10%">
                                                            <asp:Image ID="ImageDepCode" runat="server" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="td_label">
                                                <asp:Label ID="lblISAllowProject" runat="server" Width="68%" ForeColor="Blue">ISAllowProject:</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:DropDownList runat="server" ID="ddlIsAllowProject" Width="100%">
                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemDefault %>" Value="">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemYes %>" Value="Y">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="<%$Resources:ControlText,ddlItemNo %>" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOrgCode" runat="server" Width="0%" CssClass="input_textBox" Style="display: none"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEffectFlag" runat="server" Width="0%" CssClass="input_textBox"
                                                    Text="Y" Style="display: none"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblOttypeCode" runat="server" ForeColor="Blue">OTTypeCode</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:DropDownList ID="ddlOttypeCode" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lbllOttypeDetail" runat="server">OTTypeDetail</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtOttypeDetail" runat="server" CssClass="input_textBox" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG1dLimit" runat="server">G1DLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG1dLimit" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG2dLimit" runat="server">G2DLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG2dLimit" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG3dLimit" runat="server">G3DLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG3dLimit" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG1mLimit" runat="server">G1MLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG1mLimit" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG2mLimit" runat="server">G2MLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG2mLimit" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG12mLimit" runat="server">G12MLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG12mLimit" runat="server" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="LHZBG123">
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG13mLimit" runat="server" ForeColor="Blue">G13MLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG13mLimit" runat="server"  Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblG123mLimit" runat="server" ForeColor="Blue">G123MLimit</asp:Label>
                                            </td>
                                            <td class="td_input">
                                                <asp:TextBox ID="txtG123mLimit" runat="server" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_label">
                                                &nbsp;
                                                <asp:Label ID="lblRemark" runat="server" Width="100%">Remark:</asp:Label>
                                            </td>
                                            <td class="td_input" colspan="5">
                                                <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="input_textBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="td_label" colspan="6">
                                            <table>
                                                <tr>
                                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                                        <td>
                                                            <asp:Button ID="btnCondition" CssClass="button_1" runat="server" Text="<%$Resources:ControlText,btnCondition %>"
                                                                ToolTip="Authority Code:Condition"></asp:Button>
                                                            <asp:Button ID="btnQuery" runat="server" CssClass="button_1" Text="<%$Resources:ControlText,btnQuery %>"
                                                                ToolTip="Authority Code:Query" OnClick="btnQuery_Click"></asp:Button>
                                                            <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:ControlText,btnAdd %>"
                                                                ToolTip="Authority Code:Add" CssClass="button_1"></asp:Button>
                                                            <asp:Button ID="btnModify" runat="server" Text="<%$Resources:ControlText,btnModify %>"
                                                                ToolTip="Authority Code:Modify" CssClass="button_1" OnClientClick="return ModifyClick();">
                                                            </asp:Button>
                                                            <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:ControlText,btnDelete %>"
                                                                ToolTip="Authority Code:Delete" CssClass="button_1" OnClick="btnDelete_Click"
                                                                OnClientClick="return DeleteClick();"></asp:Button>
                                                            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ControlText,btnCancel %>"
                                                                CssClass="button_1" ToolTip="Authority Code:Cancel"></asp:Button>
                                                            <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ControlText,btnSave %>"
                                                                CssClass="button_1" ToolTip="Authority Code:Save" OnClick="btnSave_Click"></asp:Button>
                                                            <asp:Button ID="btnDisable" runat="server" Text="<%$Resources:ControlText,btnDisable %>"
                                                                CssClass="button_1" ToolTip="Authority Code:Disable" OnClick="btnDisable_Click"
                                                                OnClientClick="return DisableClick();"></asp:Button>
                                                            <asp:Button ID="btnEnable" runat="server" Text="<%$Resources:ControlText,btnEnable %>"
                                                                CssClass="button_1" ToolTip="Authority Code:Enable" OnClick="btnEnable_Click"
                                                                OnClientClick="return EnableClick();"></asp:Button>
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                            </table>
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
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" class="tr_title_center" id="img_grid">
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
                                ShowMoreButtons="false" HorizontalAlign="Center" PageSize="50" PagingButtonType="Image"
                                Width="300px" ImagePath="../../CSS/images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif"
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_div">
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

                            <script language="javascript">document.write("<DIV id='div_select2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridOTMType" runat="server" Width="100%" Height="100%">
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
                                    <igtbl:UltraGridBand BaseTableName="gds_att_type_v" Key="gds_att_type_v">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="depname" Key="depname" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderDeptName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTTypeCode" Key="OTTypeCode" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderOTTypeCode %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OTTypeDetail" Key="OTTypeDetail" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderOTTypeDetail %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1DLimit" Key="G1DLimit" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG1DLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2DLimit" Key="G2DLimit" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG2DLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G3DLimit" Key="G3DLimit" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG3DLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G1MLimit" Key="G1MLimit" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG1MLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G2MLimit" Key="G2MLimit" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG2MLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G12MLimit" Key="G12MLimit" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG12MLimit %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ISAllowProject" Key="ISAllowProject" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderISAllowProject %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EFFECTFLAG" Key="EFFECTFLAG" IsBound="false"
                                                Hidden="true">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderEffectFlag %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OrgCode" Key="OrgCode" Hidden="true">
                                                <Header Caption="<%$Resources:ControlText, gvHeaderOrgCode%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G13MLimit" Key="G13MLimit" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText, gvHeaderG13MLimit%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="G123MLimit" Key="G123MLimit" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeaderG123MLimit %>">
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
    </div>
    </table>
    </form>
</body>
</html>
