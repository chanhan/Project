<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PowerChange.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.PowerChange" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%--<%@ Register TagPrefix="ControlLib" TagName="Title" Src="../ControlLib/Title.ascx" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PowerChangeForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../JavaScript/jquery.js"></script>

    <script type="text/javascript">
   $(function(){
        $("#tr_edit").toggle(
            function(){
                $("#div_1").hide();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03.gif");
            },
            function(){
              $("#div_1").show();
                $(".img1").attr("src","../../CSS/Images_new/left_back_03_a.gif");}) 
   });
   function Confirm()
   {
        var oldPsnCode= $("#<%=txtFromPersoncode.ClientID %>").val();
        var oldPsnName= $("#<%=txtFromPersonName.ClientID %>").val();
        
        var oldPsnRole= $("#<%=txtFromRole.ClientID %>").val();
        var newPsnCode= $("#<%=txtToPersoncode.ClientID %>").val();
        var newPsnName= $("#<%=txtToPersonName.ClientID %>").val();
        
        var newPsnRole= $("#<%=txtToRole.ClientID %>").val();
        var equery= $("#<%=txtFromPersoncode.ClientID %>").val();
        if (oldPsnCode==""||oldPsnName=="")
        {
           alert(Message.PleaseEnterOldPsn);
           return false;
        }
        if(oldPsnRole=="")
        {
           alert(Message.OldPsnRoleNotExist);
           return false;
        }
       if(confirm(Message.CopyConfirm))
        {
            if (newPsnCode=="")
            {
               alert(Message.PleaseEnterNewPsn);
               return false;
            }
            if(newPsnName=="")
            {
               alert(Message.NewPsnNotExist);
               return false;
            }
            if (newPsnRole!="")
            {
               alert(Message.NewPsnRoleIsExist);
               return false;
            }
            if(newPsnName=="")
            {
               alert(Message.NewMustNotOld);
               return false;
            }
        }
        else {return true};
        
   }
   function CheckUserOld()
   {
       var oldPsnCode= $("#<%=txtFromPersoncode.ClientID %>").val();
       if(oldPsnCode=="")
       {
          alert(Message.PersoncodeNotNull);
       }
       return true;
   }
   function CheckUserNew()
   {
       var newPsnCode= $("#<%=txtToPersoncode.ClientID %>").val();
       if(newPsnCode=="")
       {
          alert(Message.PersoncodeNotNull);
       }
       return true;
   }
    </script>

</head>
<body class="color_body">
    <form id="MyForm" method="post" runat="server">
    <%-- <input id="hidWorknoFrom" runat="server" type="hidden" name="hidWorknoFrom" />--%>
    <input id="hidRoleFrom" runat="server" type="hidden" name="hidRoleFrom" />
    <%-- <input id="hidWorknoTo" runat="server" type="hidden" name="hidWorknoTo" />--%>
    <input id="hidRoleTo" runat="server" type="hidden" name="hidRoleTo" />
    <div style="width: 100%;">
        <div style="width: 100%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 17px;">
                        <img src="../../CSS/Images_new/left_back_01.gif" width="17px" height="24px" />
                    </td>
                    <td style="width: 100%;" class="tr_title_center">
                        &nbsp;
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td>
                    <div id='div_1'>
                        <table width="100%">
                            <tr valign="Top">
                                <td width="45%">
                                    <fieldset>
                                          <legend>
                                               <asp:Label ID="PowerChangeFrom" runat="server"></asp:Label>
                                                        <%--原權限擁有者--%>
                                                  </legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td  width="15%">
                                                                 &nbsp;<asp:Label ID="lblFromPersoncode" runat="server">FromPersoncode:</asp:Label>
                                                            </td>
                                                            <td  width="30%">
                                                                <asp:TextBox ID="txtFromPersoncode" runat="server" Width="60%" CssClass="input_textBox"></asp:TextBox>
                                                                 <input id="hidWorknoFrom" runat="server" type="hidden" name="hidWorknoFrom" />
                                                            </td>
                                                            <td  width="5%">
                                                              <asp:Button ID="btnFormQuery" runat="server" class="button_2"  OnClick="btnFormQuery_Click" OnClientClick="return CheckUserOld()">
                                                              </asp:Button>
                                                                       
                                                            </td>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblFromPersonName" runat="server">FromPersonName:</asp:Label>
                                                            </td>
                                                            <td  width="35%">
                                                                <asp:TextBox ID="txtFromPersonName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblFromDepName" runat="server">FromDepName:</asp:Label>
                                                            </td>
                                                            <td  width="30%">
                                                                <asp:TextBox ID="txtFromDepName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                            </td>
                                                            <td width="5%"></td>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblFromRoleName" runat="server">FromRoleName:</asp:Label>
                                                            </td>
                                                            <td  width="35%">
                                                                <asp:TextBox ID="txtFromRoleName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                                <asp:TextBox ID="txtFromRole" runat="server" Width="100%" Style="display: none"></asp:TextBox>
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
                                                    <script language="JavaScript" type="text/javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*70/100+"'>");</script>

                                                    <igtbl:UltraWebGrid ID="UltraWebGridFromPerson" runat="server" Width="100%" Height="100%">
                                                        <DisplayLayout Name="UltraWebGridFromPerson" CompactRendering="False" RowHeightDefault="20px"
                                                            Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate" AutoGenerateColumns="false"
                                                            AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                                            AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                            <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                                                CssClass="tr_header">
                                                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                            </HeaderStyleDefault>
                                                            <FrameStyle Width="100%" Height="100%">
                                                            </FrameStyle>
                                                            <SelectedRowStyleDefault BackgroundImage="~/images/overbg.bmp">
                                                            </SelectedRowStyleDefault>
                                                            <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                                            </RowAlternateStyleDefault>
                                                            <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                CssClass="tr_data">
                                                                <Padding Left="3px"></Padding>
                                                                <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                            </RowStyleDefault>
                                                        </DisplayLayout>
                                                        <Bands>
                                                            <igtbl:UltraGridBand BaseTableName="Sys_PowerChange" Key="Sys_PowerChange">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ModuleCode" IsBound="false" Key="ModuleCode"
                                                                        Width="30%" MergeCells="true">
                                                                        <Header Caption="<%$Resources:ControlText,gvModuleCode %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="modulename" IsBound="false" Key="modulename"
                                                                        Width="30%" MergeCells="true">
                                                                        <Header Caption="<%$Resources:ControlText,gvModuleName %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PDEPNAME" IsBound="false" Key="PDEPNAME" Width="30%">
                                                                        <Header  Caption="<%$Resources:ControlText,gvPDepName %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>                                                                   
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>

                                                    <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>
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

                              </fieldset>
                                </td>
                                <td width="10%">
                                    <asp:Panel ID="pnlShowPanel" runat="server">
                                        <asp:Button ID="btnChangeSave" runat="server" class="button_2" OnClick="btnChangeSave_Click"
                                            OnClientClick="return Confirm()"></asp:Button>
                                        <asp:Button ID="btnCopy" runat="server" class="button_2" OnClick="btnCopy_Click"
                                            OnClientClick="return Confirm()"></asp:Button>
                                    </asp:Panel>
                                </td>
                                <td width="45%">
                                    <fieldset>
                                       <legend>
                                           <asp:Label ID="PowerChangeTo" runat="server"></asp:Label>
                                                        <%-- 現權限交接者----%>
                                                </legend>
                                                    <table width="100%">
                                                        <tr>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblToPersoncode" runat="server">ToPersoncode:</asp:Label>
                                                            </td>
                                                            <td  width="30%">
                                                                <asp:TextBox ID="txtToPersoncode" runat="server" Width="60%" CssClass="input_textBox"></asp:TextBox>
                                                                 <input id="hidWorknoTo" runat="server" type="hidden" name="hidWorknoTo" />
                                                            </td>
                                                            <td with="5%">
                                                                    <asp:Button ID="btnToQuery" runat="server" class="button_2"  OnClick="btnToQuery_Click" OnClientClick="return CheckUserNew()">
                                                                    </asp:Button>
                                                            </td>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblToPersonName" runat="server">ToPersonName:</asp:Label>
                                                            </td>
                                                            <td  width="35%">
                                                                <asp:TextBox ID="txtToPersonName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblToDepName" runat="server">ToDepName:</asp:Label>
                                                            </td>
                                                            <td  width="30%">
                                                                <asp:TextBox ID="txtToDepName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                            </td>
                                                            <td  width="5%"></td>
                                                            <td  width="15%">
                                                                &nbsp;<asp:Label ID="lblToRoleName" runat="server">ToRoleName:</asp:Label>
                                                            </td>
                                                            <td  width="35%">
                                                                <asp:TextBox ID="txtToRoleName" runat="server" Width="100%" CssClass="input_textBox_noborder"></asp:TextBox>
                                                                <asp:TextBox ID="txtToRole" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>

<script>
  document.getElementById("txtFromPersonname").readOnly=true; 
  document.getElementById("txtFromRoleName").readOnly=true; 
  document.getElementById("txtFromDepName").readOnly=true; 
</script>

</html>
