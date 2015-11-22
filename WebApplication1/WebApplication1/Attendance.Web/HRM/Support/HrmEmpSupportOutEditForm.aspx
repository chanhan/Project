<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmEmpSupportOutEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.Support.HrmEmpSupportOutEditForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HrmEmpSupportOutEditForm</title>

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
        })
        
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.PanelData.style.display="";
            window.parent.document.all.div_2.style.display="";
            window.parent.document.all.btnQuery.click();
            return false;
        }
        
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
    var LocalName=document.getElementById("txtLocalName").value;
    var Sex=document.getElementById("ddlSex").value;
//    var obj1=document.all.ddlSex;
//        var Sex=obj1.options[obj1.selectedIndex].value; 
    var SupportDeptName=document.getElementById("txtSupportDeptName").value;
    var DepName=document.getElementById("txtDepName").value;
    var Level=document.getElementById("ddlLevelCode").value;
    var OverTimeType=document.getElementById("ddlOverTimeType").value;
    var State=document.getElementById("ddlState").value;
    var IsKaoQin=document.getElementById("ddlIsKaoQin").value;
    var JoinDate=document.getElementById("txtJoinDate").value;
    var StartDate=document.getElementById("txtStartDate").value;
    var PrepEndDate=document.getElementById("txtPrepEndDate").value;
    var EndDate=document.getElementById("txtEndDate").value;
    var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
    if(WorkNo==""||WorkNo==null)
    {
    alert(Message.WorkNoNotNull);
       return false;
    }
    if(LocalName==""||LocalName==null)
    {
    alert(Message.LocalNameNotNull);
       return false;
    }
    if(Sex==""||Sex==null)
    {
    alert(Message.SexNotNull);
       return false;
    }
    if(SupportDeptName==""||SupportDeptName==null)
    {
    alert(Message.SupportDeptNameNotNull);
       return false;
    }
    if(DepName==""||DepName==null)
    {
    alert(Message.SubDepNameNotNull);
       return false;
    }
    if(Level==""||Level==null)
    {
    alert(Message.LevelNotNull);
       return false;
    }
    if(OverTimeType==""||OverTimeType==null)
    {
    alert(Message.OverTimeTypeNotNull);
       return false;
    }
    if(State==""||State==null)
    {
    alert(Message.StateNotNull);
       return false;
    }
    if(IsKaoQin==""||IsKaoQin==null)
    {
    alert(Message.IsKaoQinNotNull);
       return false;
    }
    if(JoinDate!=""&&JoinDate!=null)
    {
    if(!check.test(JoinDate))
      {
       alert(Message.WrongDate);
       return false;
      }
    if(formatDate(JoinDate)==false)
    {
      alert(Message.WrongDate);
      return false;
    }
    }
    else
    {
      alert(Message.JoinDateNotNull);
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
    <input id="HiddenSeqNo" type="hidden" name="HiddenSeqNo" runat="server" />
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server" /><%--新增完資料保留工號--%>
    <input id="SupportOrderHid" type="hidden" name="SupportOrderHid" runat="server" />
    <input id="SexHid" type="hidden" name="SexHid" runat="server" />
    <input id="LevelCodeHid" type="hidden" name="LevelCodeHid" runat="server" />
    <input id="ManagerCodeHid" type="hidden" name="ManagerCodeHid" runat="server" />
    <input id="OverTimeTypeHid" type="hidden" name="OverTimeTypeHid" runat="server" />
    <input id="IsKaoQinHid" type="hidden" name="IsKaoQinHid" runat="server" />
    <input id="StateHid" type="hidden" name="StateHid" runat="server" />
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
                                            <tr>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lblWorkNo" runat="server" Text="WorkNo*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="11%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_LocalName" runat="server" Text="LocalName*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td width="12%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_Sex" runat="server" Text="Sex*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lbl_SupportDepName" runat="server" Text="SupportDepName*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="100%">
                                                                <asp:TextBox ID="txtSupportDept" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="txtSupportDeptName" runat="server" Width="100%" ReadOnly="true"
                                                                    Style="border: 0"></asp:TextBox>
                                                            </td>
                                                            <td style="cursor: hand">
                                                                <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblSubDepName" runat="server" Text="SubDepName*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDepName" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblJionDate" runat="server" Text="JionDate*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJoinDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblStartDate" runat="server" Text="StartDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblPrepEndDate" runat="server" Text="PrepEndDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrepEndDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblEndDate" runat="server" Text="EndDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEndDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lbl_LevelCode" runat="server" Text="Level*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLevelCode" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lbl_manger" runat="server" Text="Manager"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlManagerCode" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblOttypeCode" runat="server" Text="OverTimeType*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="22%">
                                                    <asp:DropDownList ID="ddlOverTimeType" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lbl_Status" runat="server" Text="Status*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblCardNo" runat="server" Text="CardNo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCardNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblIsKaoQin" runat="server" Text="IsKaoQin*" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlIsKaoQin" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblDeputyNotes" runat="server" Text="Notes"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" MaxLength="80"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                                </td>
                                                <td colspan="3">
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

    <script type="text/javascript"><!--
      
      if(window.parent.document.all.divEdit.style.display=="")
		{
           document.getElementById("txtSupportDeptName").readOnly=true;
           document.getElementById("txtSupportDept").readOnly=true;
           if(document.getElementById("ProcessFlag").value=="Add")
		    {
                document.getElementById("txtWorkNo").readOnly=false;
                document.getElementById("txtWorkNo").focus();
                document.getElementById("txtWorkNo").select();
    	    }
		    else
		    {
                document.getElementById("txtWorkNo").readOnly=true;
                
    	   }
        }
        

	--></script>

</body>
</html>
