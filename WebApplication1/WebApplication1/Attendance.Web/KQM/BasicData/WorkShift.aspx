<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkShift.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.WorkShift" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ContractForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>

    <script src="../../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>

    <style type="text/css">
        .input_textBox
        {
            border: 0;
            border-style: none;
        }
        .img_hidden
        {
            display: none;
        }
        .img_show
        {
            display: block;
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
		var shiftNo;
		var time1,time2,tt1,tt2,tm1,tm2,timecut0,timecut1,timecut;
			if(igtbl_getElementById("hidOperate").value==""&& row != null)
			{
			    igtbl_getElementById("hidShiftNo").value=row.getCellFromKey("ShiftNo").getValue()==null?"":row.getCellFromKey("ShiftNo").getValue();
			if(igtbl_getElementById("hidShiftNo").value.length>0)
			{
			    igtbl_getElementById("hidShiftNoType").value=igtbl_getElementById("hidShiftNo").value.substr(0,1);
			}
			igtbl_getElementById("ddlShiftNoType").value=igtbl_getElementById("hidShiftNoType").value;
			igtbl_getElementById("txtShiftNo").value=row.getCellFromKey("ShiftNo").getValue()==null?"":row.getCellFromKey("ShiftNo").getValue();
			igtbl_getElementById("ddlShiftType").value=row.getCellFromKey("ShiftType").getValue()==null?"":row.getCellFromKey("ShiftType").getValue();
			igtbl_getElementById("hidShiftType").value=row.getCellFromKey("ShiftType").getValue()==null?"":row.getCellFromKey("ShiftType").getValue();
			igtbl_getElementById("ddlIsLactation").value=row.getCellFromKey("IsLactation").getValue()==null?"":row.getCellFromKey("IsLactation").getValue();
			igtbl_getElementById("hidIsLactation").value=row.getCellFromKey("IsLactation").getValue()==null?"":row.getCellFromKey("IsLactation").getValue();
			igtbl_getElementById("txtShiftDesc").value=row.getCellFromKey("ShiftDesc").getValue()==null?"":row.getCellFromKey("ShiftDesc").getValue();
					
			igedit_getById("txtOffDutyTime").setValue(row.getCellFromKey("OffDutyTime").getValue()==null?"":row.getCellFromKey("OffDutyTime").getValue());
			igedit_getById("txtOnDutyTime").setValue(row.getCellFromKey("OnDutyTime").getValue()==null?"":row.getCellFromKey("OnDutyTime").getValue());
			igedit_getById("txtAMRestSTime").setValue(row.getCellFromKey("AMRestSTime").getValue()==null?"":row.getCellFromKey("AMRestSTime").getValue());
			igedit_getById("txtAMRestETime").setValue(row.getCellFromKey("AMRestETime").getValue()==null?"":row.getCellFromKey("AMRestETime").getValue());
			igedit_getById("txtPMRestSTime").setValue(row.getCellFromKey("PMRestSTime").getValue()==null?"":row.getCellFromKey("PMRestSTime").getValue());
			igedit_getById("txtPMRestETime").setValue(row.getCellFromKey("PMRestETime").getValue()==null?"":row.getCellFromKey("PMRestETime").getValue());
			
			igtbl_getElementById("txtOrgCode").value=row.getCellFromKey("OrgCode").getValue()==null?"":row.getCellFromKey("OrgCode").getValue();
//			igtbl_getElementById("hidDepCode").value=row.getCellFromKey("OrgCode").getValue()==null?"":row.getCellFromKey("OrgCode").getValue();
			igtbl_getElementById("txtDepName").value=row.getCellFromKey("orgname").getValue()==null?"":row.getCellFromKey("orgname").getValue();
			igedit_getById("NETimeQty").setValue(row.getCellFromKey("TimeQty").getValue()==null?"":row.getCellFromKey("TimeQty").getValue());
			igtbl_getElementById("txtEffectDate").value=row.getCellFromKey("EffectDate").getValue()==null?"":row.getCellFromKey("EffectDate").getValue('yyyy/mm/dd');
			igtbl_getElementById("txtExpireDate").value=row.getCellFromKey("ExpireDate").getValue()==null?"":row.getCellFromKey("ExpireDate").getValue('yyyy/mm/dd');
			igtbl_getElementById("txtShareDepcode").value=row.getCellFromKey("ShareOrgCode").getValue()==null?"":row.getCellFromKey("ShareOrgCode").getValue();
			igtbl_getElementById("hidShareDepCode").value=row.getCellFromKey("ShareOrgCode").getValue()==null?"":row.getCellFromKey("ShareOrgCode").getValue();
			igtbl_getElementById("txtShareOrgName").value=row.getCellFromKey("ShareOrgName").getValue()==null?"":row.getCellFromKey("ShareOrgName").getValue();
			igtbl_getElementById("txtShareDepcode").value=row.getCellFromKey("ShareOrgCode").getValue()==null?"":row.getCellFromKey("ShareOrgCode").getValue();
			igtbl_getElementById("txtRemark").value=row.getCellFromKey("Remark").getValue()==null?"":row.getCellFromKey("Remark").getValue();
			
			time1=igedit_getById("txtOnDutyTime").getValue();
			time2=igedit_getById("txtOffDutyTime").getValue();
			
		    if(time1!=null&&time2!=null&&time1!=""&&time2!="")
		    {
		       tt1=parseInt(time1.substr(0,2),10);
               tt2=parseInt(time2.substr(0,2),10);
                        if(tt2<tt1)
		               {
		                   tt2=tt2 + 24;
		               } 
               tm1=parseInt(time1.substr(3,5),10);
               tm2=parseInt(time2.substr(3,5),10);
                
               timecut0=Math.abs(tt2-tt1);
               timecut1=(tm2-tm1)/60;
               
               timecut=timecut0+timecut1
               igedit_getById("NEHours1").setValue(timecut);
             }
             else
             {
              igedit_getById("NEHours1").setValue("");
             }
            //2 
            time1=igedit_getById("txtAMRestSTime").getValue();
			time2=igedit_getById("txtAMRestETime").getValue();
			
	    	if(time1!=null&&time2!=null&&time1!=""&&time2!="")
	    	{
               tt1=parseInt(time1.substr(0,2),10);
               tt2=parseInt(time2.substr(0,2),10);
                         if(tt2<tt1)
		               {
		                   tt2=tt2 + 24;
		               } 
               tm1=parseInt(time1.substr(3,5),10);
               tm2=parseInt(time2.substr(3,5),10);
                
               timecut0=Math.abs(tt2-tt1);
               timecut1=(tm2-tm1)/60;
               
               timecut=timecut0+timecut1;
               
               igedit_getById("NEHours2").setValue(timecut);
             }
             else
             {
              igedit_getById("NEHours2").setValue("");
             }
//             //3
            time1=igedit_getById("txtPMRestSTime").getValue();
			time2=igedit_getById("txtPMRestETime").getValue();
			
	    	if(time1!=null&&time2!=null&&time1!=""&&time2!="")
	    	{
			
               tt2=parseInt(time2.substr(0,2),10);
               tt1=parseInt(time1.substr(0,2),10);
                         if(tt2<tt1)
		               {
		                   tt2=tt2 + 24;
		               } 
               tm1=parseInt(time1.substr(3,5),10);
               tm2=parseInt(time2.substr(3,5),10);
                
               timecut0=Math.abs(tt2-tt1);
               timecut1=(tm2-tm1)/60;
               
               timecut=timecut0+timecut1;
               
               igedit_getById("NEHours3").setValue(timecut);
           
             }   
             else
             {
              igedit_getById("NEHours3").setValue("");
             }
		     igtbl_getElementById("ddlShiftNoType").value=igtbl_getElementById("hidShiftNoType").value;
			 igedit_getById("NEHours3").setValue(row.getCellFromKey("PMRestQty").getValue()==null?"":row.getCellFromKey("PMRestQty").getValue());			   
			}
		}
		
		function DutyTimeChange()
		{
		    var shiftType,time1,time2,tt1,tt2,tm1,tm2,timecut0,timecut1,timecut;
		    var ShiftNoType;
		    if(igtbl_getElementById("ProcessFlag").value=="Add")
			{
			    ShiftNoType=igtbl_getElementById("ddlShiftNoType").value;
			    igtbl_getElementById("hidShiftNoType").value=igtbl_getElementById("ddlShiftNoType").value;
			}
			else
			{
			    ShiftNoType=igtbl_getElementById("ddlShiftNoType").value;
			}
		    switch (ShiftNoType)
	    	{	    	
             case "":
                 shiftType="";
                     break;
	         case "A":
                 shiftType=igtbl_getElementById("hidShiftNoType1").value;
                     break;
	         case "B":
                 shiftType=igtbl_getElementById("hidShiftNoType2").value;
                     break;
	         case "C":
                 shiftType=igtbl_getElementById("hidShiftNoType3").value;
                     break;
            }
			time1=igedit_getById("txtOnDutyTime").getValue();
			time2=igedit_getById("txtOffDutyTime").getValue();
			
		        if(time1!=null&&time2!=null&&time1!=""&&time2!="")
		        {
		           tt1=parseInt(time1.substr(0,2),10);
                   tt2=parseInt(time2.substr(0,2),10);

                       if(tt2<tt1)
		               {
		                   tt2=tt2 + 24;
		               } 

                   tm1=parseInt(time1.substr(3,5),10);
                   tm2=parseInt(time2.substr(3,5),10);
                    
                   timecut0=Math.abs(tt2-tt1);
                   timecut1=(tm2-tm1)/60;
                   
                   timecut=timecut0+timecut1;
                   
                   igedit_getById("NEHours1").setValue(timecut);
                   
               }  
               else
               {
                time1="";
                time2="";
                igedit_getById("NEHours1").setValue("");
               }   
            ShifDesc(shiftType,time1,time2);
            timeQty();
		}		
		function AMRestChange()
		{
		    var time1,time2,tt1,tt2,tm1,tm2,timecut0,timecut1,timecut;
		
		    time1=igedit_getById("txtAMRestSTime").getValue();
			time2=igedit_getById("txtAMRestETime").getValue();
			
	    	if(time1!=null&&time2!=null&&time1!=""&&time2!="")
	    	{
               tt1=parseInt(time1.substr(0,2),10);
               tt2=parseInt(time2.substr(0,2),10);
        
               tm1=parseInt(time1.substr(3,5),10);
               tm2=parseInt(time2.substr(3,5),10);
               
               if(tt2<tt1)
               {
                tt2=tt2 + 24;
               }
               
               timecut0=Math.abs(tt2-tt1);
               timecut1=(tm2-tm1)/60;
               
               timecut=timecut0+timecut1;
               
               igedit_getById("NEHours2").setValue(timecut);
             } 
             else
              {
                time1="";
                time2="";
                igedit_getById("NEHours2").setValue("");
              }
             timeQty();  
		}
		
		function PMRestChange()
		{
		var time1,time2,tt1,tt2,tm1,tm2,timecut0,timecut1,timecut;
		
		    time1=igedit_getById("txtPMRestSTime").getValue();
			time2=igedit_getById("txtPMRestETime").getValue();
			
	    	if(time1!=null&&time2!=null&&time1!=""&&time2!="")
	    	{
			
               tt2=parseInt(time2.substr(0,2),10);
               tt1=parseInt(time1.substr(0,2),10);
           
               tm1=parseInt(time1.substr(3,5),10);
               tm2=parseInt(time2.substr(3,5),10);
               
               if(tt2<tt1)
               {
                tt2=tt2 + 24;
               }
                
               timecut0=Math.abs(tt2-tt1);
               timecut1=(tm2-tm1)/60;
               
               timecut=timecut0+timecut1;
               igedit_getById("NEHours3").setValue(timecut);
               if(igtbl_getElementById("ddlShiftType").value=="N"&&timecut<1)
               {
                    alert(Message.PMMoreThanOneH);
                    return;
               }
               else if(igtbl_getElementById("ddlShiftType").value=="N"&&timecut>5)
               {
                    alert(Message.PMMoreThanFiveH);
                    return;
               }           
             }
             else
             {
                igedit_getById("NEHours3").setValue("");
             }
           timeQty();
		}
		
		function timeQty()
		{
		    var NEHours1,NEHours2,NEHour3;
		    NEHours1= igedit_getById("NEHours1").getValue();
		    NEHours2= igedit_getById("NEHours2").getValue();
		    NEHours3= igedit_getById("NEHours3").getValue();
		    igedit_getById("NETimeQty").setValue(NEHours1-NEHours2);		    
		}		
		function ShifDesc(shiftType,time1,time2)
		{
		    igtbl_getElementById("txtShiftDesc").value=shiftType+time1+"~"+time2;
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
   function CheckDate()
   {
      var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
      var EffectDate= $("#<%=txtEffectDate.ClientID%>").val();
      var ExpireDate=$("#<%=txtExpireDate.ClientID %>").val();
      if (EffectDate!=null&&EffectDate!="")
      {
         if(!check.test(EffectDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtEffectDate.ClientID%>").val("");
           return false;
         }
      }
      if (ExpireDate!=null&&ExpireDate!="")
      {
         if(!check.test(ExpireDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtExpireDate.ClientID%>").val();
           return false;
         }
      }
      if((EffectDate!=null&&EffectDate!="")&&(ExpireDate!=null&&ExpireDate!=""))
      {
        if(ExpireDate<EffectDate)
        {
           alert(Message.ToLaterThanFrom);
           $("#<%=txtExpireDate.ClientID %>").val("");
           return false;
        }
      }
      return true;
   }
    </script>

</head>
<body onload="return Load();">
    <form id="form1" runat="server">
    <div>
        <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server">
        <input id="hidShiftNoType" type="hidden" name="hidShiftNoType" runat="server">
        <input id="hidShiftType" type="hidden" name="hidShiftType" runat="server">
        <input id="hidIsLactation" type="hidden" name="hidIsLactation" runat="server">
        <input id="hidDepCode" type="hidden" name="hidDepCode" runat="server">
        <input id="hidDepName" type="hidden" name="hidDepName" runat="server">
        <input id="hidInUsing" type="hidden" name="hidInUsing" runat="server">
        <input id="hidShiftNo" name="hidShiftNo" type="hidden" runat="server" />
        <input id="hidShiftNoType1" type="hidden" name="hidShiftNoType1" runat="server">
        <input id="hidShiftNoType2" type="hidden" name="hidShiftNoType2" runat="server">
        <input id="hidShiftNoType3" type="hidden" name="hidShiftNoType3" runat="server">
        <input id="hidShareDepCode" type="hidden" name="hidShareDepCode" runat="server">
        <input id="hidSetFlag" type="hidden" name="hidSetFlag" runat="server">
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
                                                    <asp:Label ID="lblDepcode" runat="server" ForeColor="Blue">Department:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtOrgCode" runat="server" Width="100%" class="input_textBox_1"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtDepName" runat="server" class="input_textBox_1" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" class="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblShiftType" runat="server" ForeColor="Blue">ShiftType:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlShiftType" class="input_textBox_1" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblIsLactation" runat="server" ForeColor="Blue">IsLactation:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlIsLactation" runat="server" class="input_textBox_1" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblShiftNoType" runat="server" ForeColor="Blue">ShiftType:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlShiftNoType" runat="server" class="input_textBox_2" onchange="DutyTimeChange()"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblShiftNo" runat="server">ShiftNo:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtShiftNo" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                </td>
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblShiftDesc" runat="server">ShiftDesc:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtShiftDesc" runat="server" Width="100%" class="input_textBox_2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblTimeCut0" runat="server" ForeColor="Blue">TimeCut0:</asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtOnDutyTime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="DutyTimeChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtOffDutyTime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="DutyTimeChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td width="25%">
                                                                <igtxt:WebNumericEdit Width="90%" ID="NEHours1" runat="server" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="timeQty" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                            <td width="5%">
                                                                (H)
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblTimeCut1" runat="server" ForeColor="Blue">TimeCut1:</asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtAMRestSTime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="AMRestChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtAMRestETime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="AMRestChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td width="25%">
                                                                <igtxt:WebNumericEdit Width="90%" ID="NEHours2" runat="server" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="timeQty" />
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                            <td width="5%">
                                                                (H)
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_label">
                                                    &nbsp;<asp:Label ID="lblTimeCut2" runat="server">TimeCut2:</asp:Label><asp:Label
                                                        ID="lblTimeCut3" Style="display: none" runat="server">TimeCut2:</asp:Label>
                                                </td>
                                                <td class="td_input">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtPMRestSTime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="PMRestChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td>
                                                                ~
                                                            </td>
                                                            <td width="35%">
                                                                <igtxt:WebDateTimeEdit Width="100%" ID="txtPMRestETime" runat="server" DataMode="DateOrDBNull"
                                                                    EditModeFormat="HH:mm" class="input_textBox_1">
                                                                    <ClientSideEvents ValueChange="PMRestChange" />
                                                                </igtxt:WebDateTimeEdit>
                                                            </td>
                                                            <td width="25%">
                                                                <igtxt:WebNumericEdit Width="90%" ID="NEHours3" runat="server" class="input_textBox_1">
                                                                </igtxt:WebNumericEdit>
                                                            </td>
                                                            <td width="5%">
                                                                (H)
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td>
                                                    &nbsp;<asp:Label ID="lblEffectDate" runat="server">EffectDate:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEffectDate" runat="server" onchange="return CheckDate()" class="input_textBox_2"
                                                        Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblExpireDate" runat="server">ExpireDate:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExpireDate" runat="server" Width="100%" onchange="return CheckDate()"
                                                        class="input_textBox_2"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblTimeQty" runat="server">TimeQty:</asp:Label>
                                                </td>
                                                <td align="right">
                                                    <igtxt:WebNumericEdit Width="72%" ID="NETimeQty" runat="server" class="input_textBox_2">
                                                    </igtxt:WebNumericEdit>
                                                    <asp:Label ID="lblTimeQtyHours" runat="server">Hours:</asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td width="10%">
                                                    &nbsp;<asp:Label ID="lblShareDepcode" runat="server">Department:</asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtShareDepcode" runat="server" Width="100%" class="input_textBox_1"
                                                                    Style="display: none"></asp:TextBox>
                                                            </td>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtShareOrgName" runat="server" class="input_textBox_1" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgShareDepcode" runat="server" class="img_hidden" ImageUrl="../../CSS/Images_new/search_new.gif">
                                                                </asp:Image>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;<asp:Label ID="lblRemark" runat="server">Remark:</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" class="input_textBox_1"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hidOperate" runat="server" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <asp:Panel ID="pnlShowPanel" runat="server">
                            <asp:Button ID="btnCondition" runat="server" class="button_2" OnClientClick="return condition_click();">
                            </asp:Button>
                            <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                            </asp:Button>
                            <asp:Button ID="btnAdd" runat="server" class="button_2" OnClientClick="return add_click();">
                            </asp:Button>
                            <asp:Button ID="btnModify" runat="server" class="button_2" OnClientClick="return  modify_click();">
                            </asp:Button>
                            <asp:Button ID="btnDelete" runat="server" class="button_2" OnClientClick="return delete_click();"
                                OnClick="btnDelete_Click"></asp:Button>
                            <asp:Button ID="btnSave" runat="server" class="button_2" OnClientClick="return save_click();"
                                OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" class="button_2" OnClientClick="return  cancel_click();">
                            </asp:Button>
                            <asp:Button ID="btnExport" runat="server" class="button_2" OnClick="btnExport_Click">
                            </asp:Button>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
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

                            <script language="JavaScript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*53/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGrid" runat="server" Width="100%" Height="100%" OnDataBound="UltraWebGrid_DataBound">
                                <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                    AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                    AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGrid" TableLayout="Fixed"
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
                                    <igtbl:UltraGridBand BaseTableName="GDS_ATT_WorkShift" Key="GDS_ATT_WorkShift">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftNo" Key="ShiftNo" IsBound="false" Width="50">
                                                <Header Caption="<%$Resources:ControlText,gvShiftNo %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftType" Key="ShiftType" IsBound="false"
                                                HeaderText="" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvShiftTypeName %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="IsLactation" Key="IsLactation" IsBound="false"
                                                HeaderText="" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvIsLactation %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftDesc" Key="ShiftDesc" IsBound="false"
                                                Width="125px" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvShiftDesc %>" Fixed="true">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime" Key="OnDutyTime" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvOnDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime" Key="OffDutyTime" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvOffDutyTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AMRestSTime" Key="AMRestSTime" IsBound="false"
                                                HeaderText="" Width="60">
                                                <Header Caption="<%$Resources:ControlText,gvAMRestSTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="AMRestETime" Key="AMRestETime" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvAMRestETime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PMRestSTime" Key="PMRestSTime" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvPMRestSTime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PMRestETime" Key="PMRestETime" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvPMRestETime %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PMRestQty" Key="PMRestQty" IsBound="false"
                                                Width="60" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvPMRestQty %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="orgname" Key="orgname" IsBound="false" Width="100px"
                                                HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvOrgName %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TimeQty" Key="TimeQty" IsBound="false" Width="40px"
                                                HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvTimeQty %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="EffectDate" Key="EffectDate" IsBound="false"
                                                Width="80px" Format="yyyy/MM/dd" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvEffectDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ExpireDate" Key="ExpireDate" IsBound="false"
                                                Width="80px" Format="yyyy/MM/dd" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvExpireDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_user" Key="update_user" IsBound="false"
                                                Width="80px" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvModifier %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="update_date" Key="update_date" IsBound="false"
                                                Width="80px" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvModifyDate %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="Remark" Key="Remark" IsBound="false" Width="150px"
                                                HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvRemark %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShareOrgCode" Key="ShareOrgCode" IsBound="false"
                                                Width="150px" Hidden="True" HeaderText="">
                                                <Header Caption="">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OrgCode" Key="OrgCode" IsBound="false" Width="150px"
                                                Hidden="True" HeaderText="">
                                                <Header Caption="">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShiftType" Key="ShiftType" IsBound="false"
                                                Hidden="True">
                                                <Header Caption="">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OnDutyTime1" Key="OnDutyTime1" IsBound="false"
                                                Width="80px" HeaderText="" Hidden="true">
                                                <Header Caption="">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="OffDutyTime1" Key="OffDutyTime1" IsBound="false"
                                                Width="80px" HeaderText="" Hidden="true">
                                                <Header Caption="">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="ShareOrgName" Key="ShareOrgName" IsBound="false"
                                                Width="100px" HeaderText="">
                                                <Header Caption="<%$Resources:ControlText,gvShareOrgCode %>">
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
            </div>
        </div>
    </div>
    </form>
</body>

<script type="text/javascript">

   function Load()
   {
    var Flag= $("#<%=hidSetFlag.ClientID %>").val();
    if(Flag!="Fail")
    {
      $(".input_textBox").css("border-style", "none");
      addReadonly();
      $("#<%=btnCancel.ClientID %>").attr("disabled","true");
      $("#<%=btnSave.ClientID %>").attr("disabled","true");
      return true;
     }
     else
     {
        modify_click();
     }
   }
   function addDisable()
   {
       $("#<%=btnAdd.ClientID %>").attr("disabled","true");
       $("#<%=btnCondition.ClientID %>").attr("disabled","true");
       $("#<%=btnQuery.ClientID %>").attr("disabled","true");
       $("#<%=btnModify.ClientID %>").attr("disabled","true");
       $("#<%=btnDelete.ClientID %>").attr("disabled","true");
   }
   function addReadonly()
   {
    igtbl_getElementById("txtDepName").readOnly=true;
    igtbl_getElementById("txtShareOrgName").readOnly=true;
    igtbl_getElementById("txtShiftNo").readOnly=true;
    igtbl_getElementById("txtShiftDesc").readOnly=true;
    igedit_getById("NEHours1").setReadOnly(true);
    igedit_getById("NEHours2").setReadOnly(true);
    igedit_getById("NEHours3").setReadOnly(true);
    igedit_getById("NETimeQty").setReadOnly(true);
    igtbl_getElementById("ddlShiftNoType").disabled=true; 
    igtbl_getElementById("ddlShiftType").disabled=true; 
    igtbl_getElementById("ddlIsLactation").disabled=true; 
    igedit_getById("txtOnDutyTime").setReadOnly(true);
    igedit_getById("txtOffDutyTime").setReadOnly(true);
    igedit_getById("txtAMRestSTime").setReadOnly(true);
    igedit_getById("txtAMRestETime").setReadOnly(true);
    igedit_getById("txtPMRestSTime").setReadOnly(true);
    igedit_getById("txtPMRestETime").setReadOnly(true);
    igtbl_getElementById("txtEffectDate").readOnly=true;
    igtbl_getElementById("txtExpireDate").readOnly=true;
    igtbl_getElementById("txtRemark").readOnly=true;
    $(".input_textBox_1").css("border-style", "none");
    $(".input_textBox_2").css("border-style", "none");
    $(".input_textBox_1").css("background-color", "#efefef");
    $(".input_textBox_2").css("background-color", "");
    }
    function removeValue()
    {
      $(".input_textBox_noborder_1").attr("value","");
      $(".input_textBox_noborder_2").attr("value","");
      $(".input_textBox_1").attr("value","");
      $(".input_textBox_2").attr("value","");
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
    }
    function removereadonly()
    {
       $(".img_hidden").show();
       igtbl_getElementById("ddlShiftNoType").disabled=false; 
       igtbl_getElementById("ddlShiftType").disabled=false; 
       igtbl_getElementById("ddlIsLactation").disabled=false; 
       igedit_getById("txtOnDutyTime").setReadOnly(true);
       igtbl_getElementById("txtEffectDate").readOnly=false;
       igtbl_getElementById("txtExpireDate").readOnly=false;
       igtbl_getElementById("txtRemark").readOnly=false;
       igedit_getById("txtOnDutyTime").setReadOnly(false);
       igedit_getById("txtOffDutyTime").setReadOnly(false);
       igedit_getById("txtAMRestSTime").setReadOnly(false);
       igedit_getById("txtAMRestETime").setReadOnly(false);
       igedit_getById("txtPMRestSTime").setReadOnly(false);
       igedit_getById("txtPMRestETime").setReadOnly(false);
       $(".input_textBox_1").css("border-style", "solid");
       $(".input_textBox_2").css("border-style", "solid");
       $(".input_textBox_1").css("background-color", "White");
       $(".input_textBox_2").css("background-color", "White");
       $("#<%=ddlShiftNoType.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=ddlShiftType.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=ddlIsLactation.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtOnDutyTime.ClientID %>").css("background-color", "Cornsilk"); 
        
       $("#<%=txtOffDutyTime.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtAMRestSTime.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtAMRestETime.ClientID %>").css("background-color", "Cornsilk");
       $("#<%=txtDepName.ClientID %>").css("background-color", "Cornsilk");             
    }
    function condition_click()
    {
         $("#<%=hidOperate.ClientID %>").val("Condition");
         removeValue();
         removereadonly();
         igtbl_getElementById("txtShiftNo").readOnly=false;
         addbtnDisable();  
         $('#<%=btnCancel.ClientID %>').removeAttr("disabled");
         $('#<%=btnQuery.ClientID %>').removeAttr("disabled");
         return false; 
    }
    function  add_click()
    {
       $("#<%=hidOperate.ClientID %>").val("Add");
       removeValue();
       removereadonly();
       addbtnDisable();
       $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled");
       return false;
    }
    function  modify_click()
    {  
        $("#<%=hidOperate.ClientID %>").val("Modify");
        var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
        var shiftNo = $.trim($("#<%=txtShiftNo.ClientID%>").val());
        if (shiftNo=="")
        {alert(Message.AtLastOneChoose); $("#<%=hidOperate.ClientID %>").val("");return false;}
       removereadonly();
       var shiftNoType=$("#<%=hidShiftNoType.ClientID%>").val();
       igtbl_getElementById("ddlShiftNoType").disabled=true; 
       $("#<%=ddlShiftNoType.ClientID%>").val(shiftNoType);
       addbtnDisable();
       $("#<%=btnCancel.ClientID%>").removeAttr("disabled");
       $("#<%=btnSave.ClientID %>").removeAttr("disabled");
       return false;
    }
    function  cancel_click()
    {
       removeValue();
       $("#<%=hidOperate.ClientID %>").val("");
       $("#<%=btnCondition.ClientID%>").removeAttr("disabled");
       $("#<%=btnQuery.ClientID %>").removeAttr("disabled");
       $("#<%=btnAdd.ClientID%>").removeAttr("disabled");
       $("#<%=btnModify.ClientID%>").removeAttr("disabled");
       $("#<%=btnDelete.ClientID %>").removeAttr("disabled");
       $("#<%=btnCondition.ClientID%>").removeAttr("disabled");     
       $("#<%=btnCancel.ClientID %>").attr("disabled","true");
       $("#<%=btnSave.ClientID %>").attr("disabled","true");
       addReadonly();
       return false;
    }
    
   function setSelector(ctrlCode,ctrlName,moduleCode)
   {
       var url="RelationSelector.aspx?moduleCode="+moduleCode;
       var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
       var info=window.showModalDialog(url,null,fe);
       if(info)
       {
           $("#"+ctrlCode).val(info.codeList);
           $("#" + ctrlName).val(info.nameList);
       }
       return false;
   }
   function delete_click()
    {
        $("#<%=hidOperate.ClientID %>").val("Delete");
        var actionFlag = $.trim($("#<%=hidOperate.ClientID%>").val());
        var shiftNo = $.trim($("#<%=txtShiftNo.ClientID%>").val());
        if (shiftNo=="")
        {alert(Message.AtLastOneChoose);  $("#<%=hidOperate.ClientID %>").val("");return false;}
        else
        {if (confirm(Message.DeleteConfirm))
        {
            var result=0;
               $.ajax({
                type: "post", url: "WorkShift.aspx", dataType: "text", data: {ShiftNo:shiftNo,ActiveFlag:actionFlag },async: false,
                success: function(msg) {
                    if (msg==0) {alert(Message.ShiftIsUsedNoDelete);result=0; }
                    else
                    {
                      result=1;
                    }
                  }
                 });
                 if(result==0) 
                 { 
                   return false;
                 } 
                 else
                 {
                     return true;
                 }    
        }else{return false;}} 
    }
    function save_click()
    {
        var orgCode=$.trim($("#<%=txtOrgCode.ClientID %>").val());
        var depName=$.trim($("#<%=txtDepName.ClientID %>").val());
        var ShareOrgName=$.trim($("#<%=txtShareOrgName.ClientID %>").val());
        var shiftNo=$.trim($("#<%=txtShiftNo.ClientID %>").val());
        var activeFlag=$("#<%=hidOperate.ClientID %>").val();
        var shiftNoType=$("#<%=ddlShiftNoType.ClientID %>").val();
        var shiftType=$("#<%=ddlShiftType.ClientID %>").val();
        var neTimeQty=igedit_getById("NETimeQty").getValue();
        var isHidLactation=$("#<%=hidIsLactation.ClientID %>").val();
        var isLactation=$("#<%=ddlIsLactation.ClientID %>").val();
        var offDutyTime=igedit_getById("txtOffDutyTime").getValue();
		var onDutyTime=igedit_getById("txtOnDutyTime").getValue();
		var amRestStime=igedit_getById("txtAMRestSTime").getValue();
		var amRestETime=igedit_getById("txtAMRestETime").getValue();
		var pmRestStime=igedit_getById("txtPMRestSTime").getValue();
		var pmRestETime=igedit_getById("txtPMRestETime").getValue();
		if(depName.length>0)
		{
		     if(shiftType.length>0)
		     {
		         if(isLactation.length>0)
		         {
		            if(shiftNoType.length>0)
		            {
		               if(onDutyTime.length>0&&offDutyTime.length>0)
		               {
	                       if(amRestStime.length>0&&amRestETime.length>0)
	                       { 
	                              if(onDutyTime>offDutyTime)
                                   {        
                                        if (amRestETime < amRestStime)
                                        {
                                            if ((((amRestStime <= onDutyTime) || (amRestStime <= offDutyTime)) || (amRestETime >= onDutyTime)) || (amRestETime >= offDutyTime))
                                            {
                                                alert(Message.MustBwtOnTime);
                                                return false;
                                            }
                                        }
                                        else if (amRestStime > onDutyTime)
                                        {
                                            if ((((amRestStime> amRestETime) || (amRestStime < offDutyTime)) || (amRestETime < offDutyTime)) || (amRestETime < onDutyTime))
                                            {
                                                alert(Message.MustBwtOnTime);
                                                return false;
                                            }
                                        }
                                        else if ((((amRestStime > amRestETime) || (amRestStime >offDutyTime))|| (amRestETime >offDutyTime)) || (amRestETime > onDutyTime))
                                        {
                                            alert(Message.MustBwtOnTime);
                                            return false;
                                        }
                                        if (shiftNoType!="C"&&offDutyTime!="00:00")
                                        {
                                            alert(Message.OnlyNightTwoDays);
                                            return false;
                                        }
                                  }
                                  else
                                  {
                                        if ((((amRestStime <= onDutyTime) || (amRestStime >= offDutyTime)) || (amRestETime <= onDutyTime)) || (amRestETime >= offDutyTime))
                                        {
                                            alert(Message.MustBwtOnTime);
                                            return false;
                                        }
                                        if (shiftNoType=="C"&&onDutyTime!="00:00")
                                        {
                                            alert(Message.NightMustTwoDays);
                                            return false;
                                        }
                                  }
                                  if(shiftType=="N")
                                  {
                                    if(pmRestStime.length>0&&pmRestETime.length>0)
                                    {
                                       if(pmRestStime==offDutyTime)
                                       {
                                            if(isLactation.length>0)
                                            {    
                                             var shiftNo=$.trim($("#<%=txtShiftNo.ClientID %>").val());
                                             var activeFlag=$("#<%=hidOperate.ClientID %>").val(); 
                                             var isLactation=$("#<%=ddlIsLactation.ClientID %>").val();            
                                              var result=0;
                                               $.ajax({
                                                type: "post", url: "WorkShift.aspx", dataType: "text", data: {IsLactation: isLactation,IsHidLactation: isHidLactation,NETimeQty: neTimeQty,ShiftNo:shiftNo,ActiveFlag:activeFlag },async: false,
                                                success: function(msg) {
                                                    if (msg==1) {alert(Message.WorkMoreThanEightH);result=0; } 
                                                    else if(msg==2){alert(Message.WorkMustEightH);result=0;}
                                                    else if(msg==3){ alert(Message.CantChangToLac);result=0;}
                                                    else if(msg==4){alert( Message.CanntLowerThanIsLan);result=0;}
                                                    else{result=1;}}
                                                   });
                                                 if(result==0) { return false;} 
                                                 else{if (confirm(Message.SaveConfirm)){return true;}else{return false;}}    
                                             }            
                                       }
                                       else
                                       {
                                           alert(Message.PMLaterThanOffTime);
                                           return false;
                                       }
                                    }
                                    else
                                    {
                                      var alertTxt=$("#<%=lblTimeCut2.ClientID %>").text()+Message.TextBoxNotNull;
                                      alert(alertTxt);
                                      return false;		                   
                                    }
                                 }
                                 if(isLactation.length>0)
                                 {     
                                      var shiftNo=$.trim($("#<%=txtShiftNo.ClientID %>").val());
                                      var activeFlag=$("#<%=hidOperate.ClientID %>").val(); 
                                      var isLactation=$("#<%=ddlIsLactation.ClientID %>").val();           
                                      var result=0;
                                       $.ajax({
                                        type: "post", url: "WorkShift.aspx", dataType: "text", data: {IsLactation: isLactation,IsHidLactation: isHidLactation,NETimeQty: neTimeQty,ShiftNo:shiftNo,ActiveFlag:activeFlag},async: false,
                                        success: function(msg) {
                                            if (msg==1) {alert(Message.WorkMoreThanEightH);result=0; } 
                                            else if(msg==2){alert(Message.WorkMustEightH);result=0;}
                                            else if(msg==3){alert(Message.CantChangToLac);result=0;}
                                            else if(msg==4){alert( Message.CanntLowerThanIsLan);result=0;}
                                            else{result=1;}
                                          }
                                         });
                                         if(result==0) { return false;} 
                                         else{if (confirm(Message.SaveConfirm)){return true;}else{return false;}}       
                                 }                   
		                   }
		                   else
		                   {
		                      var alertTxt=$("#<%=lblTimeCut1.ClientID %>").text()+Message.TextBoxNotNull;
		                      alert(alertTxt);
		                      return false;		                   
		                   }
		               }
		               else
		               {
		                  var alertTxt=$("#<%=lblTimeCut0.ClientID %>").text()+Message.TextBoxNotNull;
		                  alert(alertTxt);
		                  return false;		                   
		               }
		            }
		            else
		            {
		                var alertTxt=$("#<%=lblShiftNoType.ClientID %>").text()+Message.TextBoxNotNull;
		                alert(alertTxt);
		                return false;
		            }
		         }
		         else
		         {
		            var alertTxt=$("#<%=lblIsLactation.ClientID %>").text()+Message.TextBoxNotNull;
		            alert(alertTxt);
		            return false;
		         }
		     }
		     else
		     {
		        var alertTxt=$("#<%=lblShiftType.ClientID %>").text()+Message.TextBoxNotNull;
		        alert(alertTxt);
		        return false;
		     }
		}
		else
		{
		  var alertTxt=$("#<%=lblDepcode.ClientID %>").text()+Message.TextBoxNotNull;
		  alert(alertTxt);
		  return false;
		}
    }
</script>

</html>
