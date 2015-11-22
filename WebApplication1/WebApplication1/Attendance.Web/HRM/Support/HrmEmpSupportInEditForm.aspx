<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpSupportInEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.Support.HrmEmpSupportInEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EmpSupportInEditForm</title>

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
    </style>

    <script type="text/javascript"><!--
    
     $(function(){
        var flag=$("#<%=ProcessFlag.ClientID %>").val();
        if(flag=="Modify")
        {
              $("#txtWorkNo").addClass("a");
        }
        
        
        $("#tr_edit").toggle(function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03.gif")
        $("#div_select").hide()},function(){
        $("#div_img_1").attr("src","../../CSS/Images_new/left_back_03_a.gif")
        $("#div_select").show()});
        
         $("#<%=txtWorkNo.ClientID %>").blur(function()
        {
        var WorkNo = $.trim($("#<%=txtWorkNo.ClientID %>").val());
        if ($.trim($("#<%=txtWorkNo.ClientID %>").val())) 
        {  
         $.ajax({
                type: "post", url: "HrmEmpSupportInEditForm.aspx",dataType:"json",
                data: { WorkNo: WorkNo},
                success: function(item) {
                if(item==""||item==null)
                {
                $("#<%=txtWorkNo.ClientID %>").val(null);
                alert(Message.WorkNoIsWrong);
                }
                else
                {
                
                $.each(item, function(k, v) { $(":text[id*='txt" + k + "']").val(v); });
  
                }
                }
                });
        } 
        })
        })
        
        function GetTreeDataValue(ReturnValueBoxName,moduleCode,ReturnDescBoxName)
{
    var windowWidth=500,windowHeight=600;
	var X=(screen.availWidth-windowWidth)/2;
	var Y=(screen.availHeight-windowHeight)/2;
	var Revalue=window.showModalDialog("SingleDataPickForm.aspx?ModuleCode="+moduleCode,window,"dialogWidth="+windowWidth+"px;dialogHeight="+windowHeight+"px;dialogLeft="+X+"px;dialogTop="+Y+"px;help=no;status=no;scrollbars=no");
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
       
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.PanelData.style.display="";
            window.parent.document.all.div_2.style.display="";
            window.parent.document.all.btnQuery.click();
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
        
        function checkdata()
    {
    var WorkNo=document.getElementById("txtWorkNo").value;
    var SupportDeptName=document.getElementById("txtSupportDeptName").value;
    var State=document.getElementById("ddlState").value;
    var StartDate=document.getElementById("txtStartDate").value;
    var PrepEndDate=document.getElementById("txtPrepEndDate").value;
    var EndDate=document.getElementById("txtEndDate").value;
    var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
    if(WorkNo==""||WorkNo==null)
    {
    alert(Message.WorkNoNotNull);
       return false;
    }
    if(SupportDeptName==""||SupportDeptName==null)
    {
    alert(Message.SupportDeptNameNotNull);
       return false;
    }
    if(State==""||State==null)
    {
    alert(Message.StateNotNull);
       return false;
    }
    if(StartDate!=""&&StartDate!=null)
    {
    if(!check.test(StartDate))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(StartDate)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(PrepEndDate!=""&&PrepEndDate!=null)
    {
    if(!check.test(PrepEndDate))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(PrepEndDate)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(EndDate!=""&&EndDate!=null)
    {
    if(!check.test(EndDate))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(EndDate)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    if(StartDate!=""&&StartDate!=null&&PrepEndDate!=""&&PrepEndDate!=null)
    {
    if(StartDate>PrepEndDate)
    {
      alert(Message.EndDateLaterThanStartDate);
      return false;
      }
    }
    if(StartDate!=""&&StartDate!=null&&EndDate!=""&&EndDate!=null)
    {
    if(StartDate>EndDate)
    {
      alert(Message.EndDateLaterThanStartDate);
      return false;
      }
    }
    }

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
--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenSupportOrder" type="hidden" name="HiddenSupportOrder" runat="server" />
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server" /><%--新增完資料保留工號--%>
    <input id="HiddenState" type="hidden" name="HiddenState" runat="server" />
    <asp:HiddenField ID="HiddenAllow" runat="server" />
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
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblWorkNo" runat="server" Text="WorkNo*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLocalName" runat="server" Text="LocalName"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblSex" runat="server" Text="Sex"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtSex" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblLevelCode" runat="server" Text="Level"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtLevelName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_manger" runat="server" Text="Manager"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtManagerName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblTechnicalName" runat="server" Text="TechnicalName"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtTechnicalName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_SupportDepName" runat="server" Text="SupportDepName*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtSupportDept" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="txtSupportDeptName" runat="server" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_2">
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblStartDate" runat="server" Text="StartDate"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtStartDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblPrepEndDate" runat="server" Text="PrepEndDate"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtPrepEndDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lblEndDate" runat="server" Text="EndDate"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtEndDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="tr_data_1">
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_Status" runat="server" Text="Status*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                                </td>
                                                <td colspan="3" width="56%">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%"></asp:TextBox>
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
                                        <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClientClick="return checkdata();"
                                            OnClick="btnSave_Click"></asp:Button>
                                        <asp:Button ID="btnReturn" runat="server" CssClass="button_1" OnClientClick="return Return();">
                                        </asp:Button>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
