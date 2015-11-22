<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowDayset.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowDayset" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title></title>
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function() {

            HideorShow();

        });


        function HideorShow() {
            var showid = $("input[id$='hf_show']").val();
            if (showid == "LeaveDayType") {
                $("#div_1").show();
            }
            else if (showid == "OutDayType") {
                $("#div_2").show();
            }
            else if (showid == "ShiweiType") {
                $("#div_31").show();
            }
            else if (showid == "ManagerType") {
                $("#div_4").show();
            }            
        }
        
        //刪除
        function Delete(clinkid) {
            var selRow = igtbl_getActiveRow(clinkid);
            if (selRow == null) {
                alert("請選擇需要刪除的數據！");
                return false;
            }
            else {
                //			    igtbl_getGridById("UltraWebGridBill").AllowDelete=1;
                //			    igtbl_deleteRow("UltraWebGridBill","UltraWebGridBill_r_"+selRow.getIndex());
                selRow.setSelected(true);
                igtbl_setActiveRow(clinkid, igtbl_getElementById(clinkid + "_r_" + selRow.getIndex()));
            }
            return true;
        }

        function CheckedInt(str) {

            var r = /^\d+$/;         
            if (!r.test(str)) {               
                return false;
            }
            return true;
        }

        function CheckedTb(str1, str2) {
            if (!(CheckedInt(str1) && CheckedInt(str2))) {
                alert("請輸入正整數");
                return false;
            }
            if (parseInt(str1) >= parseInt(str2)) {
                alert("起始天數必須小于結束天數");
                return false;
            }        
            return true;
        }

        function textboxcheck() {
            var str1 = $("input[id$='tb_days_start']").val();
            var str2 = $("input[id$='tb_days_end']").val();
            return CheckedTb(str1, str2);
        }
        function textboxcheck1() {
            var str1 = $("input[id$='tb_out_start']").val();
            var str2 = $("input[id$='tb_out_end']").val();
            return CheckedTb(str1, str2);
        }

        function ddlcheck1() {
            var str1 = $("select[id$='ddl_shiwei_start']").val();
            var str2 = $("select[id$='ddl_shiwei_end']").val();
            if (parseInt(str1) >= parseInt(str2)) {
                alert("起始師位必須小于迄止資位");
                return false;
            } 
        }

        function ddlcheck2() {
            var str1 = $("select[id$='ddl_glz_start']").val();
            var str2 = $("select[id$='ddl_glz_end']").val();
            if (parseInt(str1) >= parseInt(str2)) {
                alert("開始職位必須小于迄止職務");
                return false;
            } 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hf_show" runat="server" />
    <div id="div_1" style="display:none;">
    <div>
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr1">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>                                   
                                    <asp:Label ID="lbl_leavetype_1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div1">
                            <img id="img2" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
           <tr class="tr_data_1">
               <td style=" width:15%;">               
                <asp:Label ID="lbl_wenzi" runat="server" ></asp:Label>
               </td>
               <td style=" width:40%;">
                   <asp:TextBox ID="tb_days_start" Width="40%"  runat="server"></asp:TextBox>
                   ~
                    <asp:TextBox ID="tb_days_end" Width="40%"  runat="server"></asp:TextBox>
               </td>
               <td style=" width:45%; text-align:left;">
                  <asp:Button ID="btn_add_1" runat="server" Text="Button" OnClientClick="return textboxcheck();" onclick="btn_add_Click" />
                   <asp:Button ID="btn_delete_1" runat="server" Text="Button" 
                       onclick="btn_delete_1_Click" OnClientClick="return Delete('UltraWebGridAudit');" />
               </td>               
           </tr>            
         </table>
        </div>
    <div>
    
     <script language="javascript"> document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>
                     <igtbl:UltraWebGrid ID="UltraWebGridAudit" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                                                        Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free" AutoGenerateColumns="false"
                                                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                        <HeaderStyleDefault VerticalAlign="Middle"  BorderColor="#6699ff" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="80%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="GDS_WF_DAYSET" Key="GDS_WF_DAYSET">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_CODE" Key="DAY_CODE" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdayscode%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MIN" Key="DAY_MIN" IsBound="false" Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdaymin %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MAX" Key="DAY_MAX" IsBound="false"
                                                                    Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdaymax %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>                                                              
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                        </igtbl:UltraWebGrid>
                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>
                        
      </div>
      <div style="color:Red;">
          備註：系統自動以“小值《 X《=最大值”而設定；         
      </div>                        
    </div>
     <div id="div_2" style="display:none;">
       <div>
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr2">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>                                   
                                    <asp:Label ID="lbl_day_head" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div2">
                            <img id="img1" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
           <tr class="tr_data_1">
               <td style=" width:15%;">               
                <asp:Label ID="lbl_day_2" runat="server" ></asp:Label>
               </td>
               <td style=" width:40%;">
                   <asp:TextBox ID="tb_out_start" Width="40%"  runat="server"></asp:TextBox>
                   ~
                    <asp:TextBox ID="tb_out_end" Width="40%"  runat="server"></asp:TextBox>
               </td>
               <td style=" width:45%; text-align:left;">
                  <asp:Button ID="btn_add_2" runat="server" Text="Button" 
                       OnClientClick="return textboxcheck1();" onclick="btn_add_2_Click" />
                   <asp:Button ID="btn_delete_2" runat="server" Text="Button" 
                       OnClientClick="return Delete('UltraWebGridAudit1');" 
                       onclick="btn_delete_2_Click" />
               </td>               
           </tr>            
         </table>
        </div>
    <div>
    
     <script language="javascript"> document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>
                     <igtbl:UltraWebGrid ID="UltraWebGridAudit1" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                                                        Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free" AutoGenerateColumns="false"
                                                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                        <HeaderStyleDefault VerticalAlign="Middle"  BorderColor="#6699ff" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="80%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="GDS_WF_DAYSET" Key="GDS_WF_DAYSET">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_CODE" Key="DAY_CODE" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdayscode%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MIN" Key="DAY_MIN" IsBound="false" Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdaymin %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MAX" Key="DAY_MAX" IsBound="false"
                                                                    Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvdaymax %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>                                                              
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                        </igtbl:UltraWebGrid>
                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>
                        
      </div> 
      <div style="color:Red;">
         備註：系統自動以“最小值 X<=最大值”而設定；
      </div>  
     </div>
     <div id="div_31" style="display:none;">
       <div>
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr3">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>                                   
                                    <asp:Label ID="lbl_shiwei_1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div3">
                            <img id="img3" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
           <tr class="tr_data_1">
               <td style=" width:15%;">               
                <asp:Label ID="lbl_shiwei_type" runat="server" ></asp:Label>
               </td>
               <td style=" width:40%;">
                   <asp:DropDownList ID="ddl_shiwei_start" runat="server">
                   </asp:DropDownList>
                   ~
                   <asp:DropDownList ID="ddl_shiwei_end" runat="server">
                   </asp:DropDownList>
               </td>
               <td style=" width:45%; text-align:left;">
                  <asp:Button ID="btn_add_3" runat="server" Text="Button" 
                       OnClientClick="return ddlcheck1();" onclick="btn_add_3_Click" />
                   <asp:Button ID="btn_delete_3" runat="server" Text="Button" 
                       OnClientClick="return Delete('UltraWebGridAudit2');" 
                       onclick="btn_delete_3_Click" />
               </td>               
           </tr>            
         </table>
        </div>
    <div>
    
     <script language="javascript">         document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>
                     <igtbl:UltraWebGrid ID="UltraWebGridAudit2" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                                                        Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free" AutoGenerateColumns="false"
                                                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                        <HeaderStyleDefault VerticalAlign="Middle"  BorderColor="#6699ff" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="80%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="GDS_WF_DAYSET" Key="GDS_WF_DAYSET">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_CODE" Key="DAY_CODE" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvshiweicode%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MIN" Key="DAY_MIN" IsBound="false" Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvshiweistart %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MAX" Key="DAY_MAX" IsBound="false"
                                                                    Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvshiweiend %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>                                                              
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                        </igtbl:UltraWebGrid>
                        <script language="JavaScript" type="text/javascript">                            document.write("</DIV>");</script>
                        
      </div> 
      <div style="color:Red;">
         備註：系統自動以“開始資位<=X<=迄止資位”而設定；
      </div> 
       
     </div>
     <div id="div_4" style="display:none;">
        <div>
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr4">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>                                   
                                    <asp:Label ID="lbl_glz_type" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="Div4">
                            <img id="img4" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
           <tr class="tr_data_1">
               <td style=" width:15%;">               
                <asp:Label ID="lbl_glz_1" runat="server" ></asp:Label>
               </td>
               <td style=" width:40%;">
                   <asp:DropDownList ID="ddl_glz_start" runat="server">
                   </asp:DropDownList>
                   ~
                   <asp:DropDownList ID="ddl_glz_end" runat="server">
                   </asp:DropDownList>
               </td>
               <td style=" width:45%; text-align:left;">
                  <asp:Button ID="btn_add_4" runat="server" Text="Button" 
                       OnClientClick="return ddlcheck2();" onclick="btn_add_4_Click" />
                   <asp:Button ID="btn_delete_4" runat="server" Text="Button" 
                       OnClientClick="return Delete('UltraWebGridAudit3');" onclick="btn_delete_4_Click" 
                        />
               </td>               
           </tr>            
         </table>
        </div>
    <div>
    
     <script language="javascript">         document.write("<DIV id='div_3' style='height:" + document.body.clientHeight * 30 / 100 + "'>");</script>
                     <igtbl:UltraWebGrid ID="UltraWebGridAudit3" runat="server" Width="100%" Height="100%">
                                                    <DisplayLayout Name="UltraWebGridAudit" CompactRendering="False" RowHeightDefault="20px"
                                                        Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                                        AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free" AutoGenerateColumns="false"
                                                        AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="RowSelect" StationaryMargins="HeaderAndFooter">
                                                        <HeaderStyleDefault VerticalAlign="Middle"  BorderColor="#6699ff" HorizontalAlign="Left"
                                                            CssClass="tr_header">
                                                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                        </HeaderStyleDefault>
                                                        <FrameStyle Width="80%" Height="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents></ClientSideEvents>
                                                        <SelectedRowStyleDefault BackgroundImage="../CSS/images/overbg.bmp">
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
                                                        <igtbl:UltraGridBand BaseTableName="GDS_WF_DAYSET" Key="GDS_WF_DAYSET">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_CODE" Key="DAY_CODE" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,gvGlzcode%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MIN" Key="DAY_MIN" IsBound="false" Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvGlzstart %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="DAY_MAX" Key="DAY_MAX" IsBound="false"
                                                                    Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvGlzend %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>                                                              
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                        </igtbl:UltraWebGrid>
                        <script language="JavaScript" type="text/javascript">                            document.write("</DIV>");</script>
                        
      </div>
       <div style="color:Red;">
         備註：系統自動以“開始職位<=X<=迄止職務”而設定；
      </div> 
     </div>
    
    </form>
</body>
</html>
