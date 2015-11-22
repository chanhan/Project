<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolPingKQMBellCardForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.BasicData.ToolPingKQMBellCardForm" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ToolPingKQMBellCardForm</title>
    <link href="../images/ui_css/table.css" type="text/css" rel="stylesheet" />
</head>
<body class="color_body">
    <link href="../../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
          function CheckPing()
          {
		    var BellNo= document.getElementById("txtBellNo").value;
		    var PortIP= document.getElementById("txtPortIP").value;
		    if(BellNo==""&&PortIP=="")
		    {
	            if(confirm("您確定不輸入任何查詢條件PING所有的卡鐘嗎？"))
	            {   
		            return true;
		        }
		        else
		        {			    
		            return false;
		        }
		    }		    
	        else
	        {
		            return true;
	        }
          }
          
          
       $(function(){
        $("#img_edit,#tr1").toggle(
            function(){
                $("#div_edit").hide();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03.gif");
                
            },
            function(){
              $("#div_edit").show();
                $("#img_edit").attr("src","../../CSS/Images_new/left_back_03_a.gif");
            }
        )
         
   });
    </script>

    <form id="form1" runat="server">
    <table class="top_table" cellspacing="1" cellpadding="0" width="98%" align="center">
        <tr>
            <td>
                <table class="inner_table" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" class="table_title_area">
                                <tr style="width: 100%;" id="tr1">
                                    <td style="width: 100%;" class="tr_title_center">
                                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/org_main_02.gif');
                                            background-repeat: no-repeat; width: 80px; text-align: center; font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 22px;">
                                        <img id="img_edit" class="img1" width="22px" height="23px" src="../../CSS/Images_new/left_back_03_a.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlContent" runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="td_label" width="15%">
                                                        &nbsp;
                                                        <asp:Label ID="lblBellNo" runat="server">BellNo:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtBellNo" Width="100%" CssClass="input_textBox" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label" width="15%">
                                                        &nbsp;
                                                        <asp:Label ID="lblPortIP" runat="server">PortIP:</asp:Label>
                                                    </td>
                                                    <td class="td_input" width="20%">
                                                        <asp:TextBox ID="txtPortIP" CssClass="input_textBox" Width="100%" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="td_label">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <%--       <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../../CSS/Images_new/EMP_BUTTON_01_new.gif');
                                            background-repeat: no-repeat;  width: 130px; text-align: center;
                                            font-size: 13px;">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnCheck" runat="server" Text="<%$Resources:ControlText,btnCheck %>"
                                                        CssClass="input_linkbutton" OnClick="btnCheck_Click" OnClientClick="return CheckPing();">
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>--%>
                                        <asp:Button ID="btnCheck" runat="server" Text="<%$Resources:ControlText,btnCheck %>"
                                            CssClass="button_mostlarge" OnClick="btnCheck_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
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

                                                    <script language="javascript">document.write("<DIV id='div_2' style='height:"+document.body.clientHeight*53/100+"'>");</script>

                                                    <igtbl:UltraWebGrid ID="UltraWebGridPingR" runat="server" Width="100%" Height="100%">
                                                        <DisplayLayout UseFixedHeaders="true" CompactRendering="False" StationaryMargins="Header"
                                                            AllowSortingDefault="Yes" RowHeightDefault="20px" Version="4.00" SelectTypeRowDefault="Single"
                                                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                                                            AllowRowNumberingDefault="ByDataIsland" Name="UltraWebGridPingR" TableLayout="Fixed"
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
                                                            <igtbl:UltraGridBand AllowRowNumbering="NotSet" AddButtonCaption="" AddButtonToolTipText=""
                                                                DataKeyField="" BaseTableName="KQM_BellCard" Key="KQM_BellCard">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="BellNo" Key="BellNo" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadBellNo %>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="Address" Key="Address" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadAddress %>">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="PortIP" Key="PortIP" IsBound="false" Width="100">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadPortIP %>" Fixed="True">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="IsOnline" Key="IsOnline" IsBound="false" Width="80">
                                                                        <Header Caption="<%$Resources:ControlText,gvHeadIsOnline %>">
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
