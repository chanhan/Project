<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Synclog.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety.Synclog" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ExceptionForm</title>
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../JavaScript/jquery-ui-custom.js"></script>

    <script type="text/javascript" src="../../JavaScript/jquery_ui_lang.js"></script>

    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />

    <script>
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
      var FromDate= $("#<%=txtFromDate.ClientID%>").val();
      var ToDate=$("#<%=txtToDate.ClientID %>").val();
      if (FromDate!=null&&FromDate!="")
      {
         if(!check.test(FromDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtFromDate.ClientID%>").val("");
           return false;
         }
      }
      if (ToDate!=null&&ToDate!="")
      {
         if(!check.test(ToDate))
         {
           alert(Message.WrongDate);
           $("#<%=txtToDate.ClientID %>").val("");
           return false;
         }
      }
      if((FromDate!=null&&FromDate!="")&&(ToDate!=null&&ToDate!=""))
      {
        if(ToDate<FromDate)
        {
           alert(Message.ToLaterThanFrom);
           $("#<%=txtToDate.ClientID %>").val("");
           return false;
        }
      }
      return true;
   }
   function Load()
   {
      $(".input_textBox").css("border-style", "none");
     return true;
   }
    </script>

</head>
<body class="color_body" onload="return Load()">
    <form id="Form1" method="post" runat="server">
    <div>
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
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr class="tr_data_1">
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblDocno" runat="server">Docno:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDocNo" class="input_textBox_1" runat="server" Width="100%" EnableViewState="False"></asp:TextBox>
                        </td>
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblTransactiontype" runat="server">Transaction Type:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtTransactionType" runat="server" class="input_textBox_1" Width="100%"
                                EnableViewState="False"></asp:TextBox>
                        </td>
                        <td rowspan="4" width="20%">
                            <asp:CheckBox ID="chkException" runat="server"></asp:CheckBox>
                            <br>
                            <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                            <br>
                            <asp:CheckBox ID="chkError" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr class="tr_data_2">
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblFromdate" runat="server">From Date:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtFromDate" runat="server" class="input_textBox_2" Width="100%"
                                EnableViewState="False" onchange="return CheckDate()"></asp:TextBox>
                        </td>
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblTodate" runat="server">To Date:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtToDate" runat="server" class="input_textBox_2" Width="100%" EnableViewState="False"
                                onchange="return CheckDate()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_data_1">
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblProcessflag" runat="server">Process Flag:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtProcessFlag" runat="server" class="input_textBox_1" Width="100%"
                                EnableViewState="False"></asp:TextBox>
                        </td>
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblProcessowner" runat="server">Process Owner:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtProcessOwner" runat="server" class="input_textBox_1" Width="100%"
                                EnableViewState="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_data_2">
                        <td width="20%">
                            &nbsp;
                            <asp:Label ID="lblFromhost" runat="server">labelFromhost</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtFromHost" runat="server" Width="100%" class="input_textBox_2"
                                EnableViewState="False"></asp:TextBox>
                        </td>
                        <td width="20%">
                            &nbsp;<asp:Label ID="lblTohost" runat="server">To Host:</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtToHost" runat="server" class="input_textBox_2" Width="100%" EnableViewState="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_data_1">
                        <td colspan="5">
                            <asp:Button ID="btnQuery" runat="server" class="button_2" OnClick="btnQuery_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="width: 100%">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;">
                    <td style="width: 100%;" class="tr_title_center" id="tr_show">
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
                                DisabledButtonImageNameExtension="g" PagingButtonSpacing="10px" ButtonImageAlign="left"
                                ShowPageIndex="false" ShowMoreButtons="false" ShowPageIndexBox="Always" SubmitButtonImageUrl="../../CSS/Images_new/search01.gif"
                                OnPageChanged="pager_PageChanged" ShowCustomInfoSection="Left" CustomInfoHTML="<font size='2'>總記錄數：</font>%recordCount%">
                            </ess:AspNetPager>
                        </div>
                    </td>
                    <td style="width: 22px;">
                        <img id="tr_showtd" class="img2" width="22px" height="24px" src="../../CSS/Images_new/left_back_03_a.gif" />
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

                            <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*59/100+"'>");</script>

                            <igtbl:UltraWebGrid ID="UltraWebGridException" runat="server" Width="100%" Height="100%">
                                <DisplayLayout CompactRendering="False" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                    BorderCollapseDefault="Separate" AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle"
                                    AllowColSizingDefault="Free" AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridException"
                                    CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter" TableLayout="Fixed"
                                    AutoGenerateColumns="false">
                                    <HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Left"
                                        CssClass="tr_header">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                                        </BorderDetails>
                                    </HeaderStyleDefault>
                                    <FrameStyle Width="100%" Height="100%">
                                    </FrameStyle>
                                    <SelectedRowStyleDefault ForeColor="Black" BackgroundImage="~/images/overbg.bmp">
                                    </SelectedRowStyleDefault>
                                    <RowAlternateStyleDefault Cursor="Hand" CssClass="tr_data1">
                                    </RowAlternateStyleDefault>
                                    <RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"
                                        CssClass="tr_data">
                                        <Padding Left="3px"></Padding>
                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                    </RowStyleDefault>
                                </DisplayLayout>
                                <Bands>
                                    <igtbl:UltraGridBand BaseTableName="gds_sc_synclog" Key="gds_sc_synclog">
                                        <Columns>
                                            <igtbl:UltraGridColumn BaseColumnName="DOCNO" Key="DOCNO" IsBound="False" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadDocNo %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TRANSACTIONTYPE" Key="TRANSACTIONTYPE" IsBound="false"
                                                Width="15%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadTransactionType %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TEXT" Key="TEXT" IsBound="False" Width="25%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadText %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PROCESSFLAG" Key="PROCESSFLAG" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadProcessFlag %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="PROCESSOWNER" Key="PROCESSOWNER" IsBound="false"
                                                Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadProcessOwner %>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="LOGTIME" Key="LOGTIME" IsBound="false" Width="10%" Format="yyyy/MM/dd">
                                                <Header Caption="<%$Resources:ControlText,gvHeadLogTime%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="FROMHOST" Key="FROMHOST" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadFromHost%>">
                                                </Header>
                                            </igtbl:UltraGridColumn>
                                            <igtbl:UltraGridColumn BaseColumnName="TOHOST" Key="TOHOST" IsBound="false" Width="10%">
                                                <Header Caption="<%$Resources:ControlText,gvHeadToHost%>">
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
    </form>
</body>
</html>
