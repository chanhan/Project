<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrmTWCadreEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.HRM.HrmTWCadreEditForm" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HrmTWCadreEditForm</title>

    <script src="../JavaScript/jquery.js" type="text/javascript"></script>

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
                type: "post", url: "HrmTWCadreEditForm.aspx",dataType:"text",
                data: { WorkNo: WorkNo},
                success: function(msg) {
                if(msg==1)
                {
                alert(Message.NotOnlyOne);
                document.getElementById("txtWorkNo").focus();
                $("#<%=txtWorkNo.ClientID %>").val(null);
                }
                }
                });
        } 
        })
    })
    
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
    
        function Checkdata()
        {
        var WorkNo=$("#<%=txtWorkNo.ClientID%>").val();
        var LocalName=$("#<%=txtLocalName.ClientID%>").val();
        var DepName=$("#<%=txtDepName.ClientID%>").val();
        var obj1=document.all.ddlIsKaoQin;
        var IsKaoQin=obj1.options[obj1.selectedIndex].value; 
          var check=/^\d{4}[\/]\d{2}[\/]\d{2}$/;
          var JoinDate= $("#<%=txtJoinDate.ClientID%>").val();
          var LeaveDate=$("#<%=txtLeaveDate.ClientID %>").val();
          if(WorkNo!=null&&WorkNo!="")
          {
            if(LocalName!=null&&LocalName!="")
            {
              if(DepName!=null&&DepName!="")
              {
                if(IsKaoQin!=null&&IsKaoQin!="")
                {
                  return true;
                }
                else
                {
                 alert(Message.IsKaoQinNotNull);
                 return false;
                }
              }
              else
              {
               alert(Message.DepNameNotNull);
               return false;
              }
            }
            else
            {
             alert(Message.LocalNameNotNull);
             return false;
            }
          }
          else
          {
           alert(Message.WorkNoNotNull);
           return false;
          }
          
          if (JoinDate!=null&&JoinDate!="")
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
          if (LeaveDate!=null&&LeaveDate!="")
          {
             if(!check.test(LeaveDate))
             {
               alert(Message.WrongDate);
               return false;
             }
             if(formatDate(LeaveDate)==false)
             {
               alert(Message.WrongDate);
               return false;
             }
          }
          return true;
        }  
        
        function Return()
        {
            window.parent.document.all.topTable.style.display="";
            window.parent.document.all.PanelData.style.display="";
            window.parent.document.all.divEdit.style.display="none";
            window.parent.document.all.div_2.style.display="";
            window.parent.document.all.btnQuery.click();
            return false;
        }
        
        function setSelector()
        {
        var modulecode=$("#<%=ModuleCode.ClientID %>").val();
        var url="/KQM/BasicData/RelationSelector.aspx?moduleCode="+modulecode;
        var fe="dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
        var info=window.showModalDialog(url,null,fe);
        if(info)
        {
        $("#txtDepCode").val(info.codeList);
        $("#txtDepName").val(info.nameList);
        }
        return false;
        }
    
--></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="ProcessFlag" type="hidden" name="ProcessFlag" runat="server" />
    <input id="HiddenSave" type="hidden" name="HiddenSave" runat="server" />
    <asp:HiddenField ID="ModuleCode" runat="server" />
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
                                    <asp:Label ID="lblEdit" runat="server"></asp:Label>
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
                                            <tr calss="tr_data_1">
                                                <td width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblWorkNo" runat="server" ForeColor="Blue">WorkNo*</asp:Label>
                                                </td>
                                                <td width="18%">
                                                    <asp:TextBox ID="txtWorkNo" runat="server" Width="100%" MaxLength="10"></asp:TextBox>
                                                </td>
                                                <td width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lbl_LocalName" runat="server" ForeColor="Blue">LocalName*</asp:Label>
                                                </td>
                                                <td width="18%">
                                                    <asp:TextBox ID="txtLocalName" runat="server" Width="100%" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td width="15%">
                                                    &nbsp;
                                                    <asp:Label ID="lblSex" runat="server">Sex</asp:Label>
                                                </td>
                                                <td width="18%">
                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="100%">
                                                        <asp:ListItem Value="1" Text="<%$Resources:ControlText,FitSexNameMan %>"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="<%$Resources:ControlText,FitSexNameWoman %>"></asp:ListItem>
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr calss="tr_data_2">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblIdentityNo" runat="server">IdentityNo</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIdentityNo" runat="server" Width="100%" MaxLength="18"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblByName" runat="server">ByName</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtByName" runat="server" Width="100%" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblLevelCode" runat="server">LevelCode</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLevelCode" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr calss="tr_data_1">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblManager" runat="server">Manager</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlManagerCode" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblDepName" runat="server" ForeColor="Blue">Department*</asp:Label>
                                                    <asp:TextBox ID="txtDepCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                </td>
                                                <td colspan="3">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td width="99%">
                                                                <asp:TextBox ID="txtDepName" runat="server" Width="100%" Style="border: 0"></asp:TextBox>
                                                            </td>
                                                            <td width="1%" style="cursor: hand" onclick="setSelector();">
                                                                <asp:Image ID="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />
                                                                <%--<img id="imgDepCode" runat="server" src="../../CSS/Images_new/search_new.gif" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr calss="tr_data_2">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblExtension" runat="server">Extension</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExtension" runat="server" Width="100%" MaxLength="20"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="Notes" runat="server">Notes</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" MaxLength="80"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr calss="tr_data_1">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblJoinDate" runat="server">JoinDate</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJoinDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblLeaveDate" runat="server">LeaveDate</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveDate" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr calss="tr_data_2">
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblCardNo" runat="server">CardNo</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCardNo" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="lblIsKaoQin" runat="server" ForeColor="Blue">IsKaoQin*</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlIsKaoQin" runat="server" Width="100%">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                        <asp:Button ID="btnSave" runat="server" CssClass="button_1" OnClientClick="return Checkdata();"
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
            document.getElementById("txtWorkNo").readOnly=true;
            document.getElementById("txtWorkNo").focus();
            document.getElementById("txtWorkNo").select();
            document.getElementById("txtDepName").readOnly=true;
            document.getElementById("txtDepCode").readOnly=true;
		    if(document.getElementById("ProcessFlag").value=="Add")
		    {
                document.getElementById("txtWorkNo").readOnly=false;
                document.getElementById("txtWorkNo").focus();
                document.getElementById("txtWorkNo").select();
    	    }
    	    else
    	    {
    	        document.getElementById("txtWorkNo").readOnly=true;
                document.getElementById("txtLocalName").focus();
                document.getElementById("txtLocalName").select();
    	    }
        }
	--></script>

</body>
</html>
