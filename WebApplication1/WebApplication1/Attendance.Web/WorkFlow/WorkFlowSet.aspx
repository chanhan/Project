<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlowSet.aspx.cs" Inherits="GDSBG.MiABU.Attendance.Web.WorkFlow.WorkFlowSet" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="DropDownCheckList" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"  Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>
    </title>

   
<script src="../JavaScript/jquery.js" type="text/javascript"></script>

    <link href="../CSS/CommonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-ui-custom.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery_ui_lang.js" type="text/javascript"></script>
    
    <script type="text/javascript">  
      //節點點擊   
		function NodeClick(treeId, nodeId, button)
		{
		    var node = igtree_getNodeById(nodeId);
			if(node != null)
			{
                    var temVal= node.getText().split("[");
                   $("input[id$='hf_deptid']").val(node.getTag()+";"+temVal[0]);   
			}

       }
       //收縮模塊控制
       $(function() 
        {
            $("#tr_edit").toggle
            (
                function()
                {
                    $("#div_select").hide();

                },
                function() 
                {
                    $("#div_select").show();

                }
            )
                $(function() 
           {
               $("#tr1").toggle
           (
            function() {
                $("#div_showdata").hide();
            },
            function() {
                $("#div_showdata").show();
            }
           )         
        });
       });

       $(function() {
           $("#tr2").toggle
            (
                function() {
                   $("#div_edit").hide();

                },
                function() {
                $("#div_edit").show();

                }
            )

            });
         $(function() {
                $("#tr3").toggle
           (
            function() {
                $("#div_down").hide();

            },
            function() {
                $("#div_down").show();
              
            }
           )
        });
            
         //上下移動
        function MakeImgFlow(tp)
		{
			var myrow = igtbl_getActiveRow("UltraWebGridBill");
			if (myrow == null)
			{
			    alert(Message.check_limit_add);
				return;
			}
			var grid = igtbl_getGridById('UltraWebGridBill');
			var rows = grid.Rows;
			var index = myrow.getIndex();
			var cindex;
			var cnt = rows.length;
			
			if (tp ==1)
			{
				if (index == 0)
				{
				    return;
				}
				cindex=  index -1;
				
			}
			else if (tp == 2)
			{
				if (index == rows.length-1)
				{
					return;
				}
				cindex =index + 1;
			}
			
			var tmp;
			var crow = rows.getRow(cindex);
			var ccell;
			var cell;
			for (i = 0;i< 6;i++)
			{
				ccell = crow.getCell(i);
				cell = myrow.getCell(i);
				tmp = cell.getValue();
				cell.setValue(ccell.getValue());
				ccell.setValue(tmp);
			}
			crow.setSelected(true);
			igtbl_setActiveRow("UltraWebGridBill",igtbl_getElementById("UltraWebGridBill_r_"+cindex));

}
       //刪除流程
       function Delete()
		{
		    var selRow = igtbl_getActiveRow("UltraWebGridBill");
			if (selRow == null)
			{
			    alert(Message.check_delete_data);
				return false;
			}
			else
			{
//			    igtbl_getGridById("UltraWebGridBill").AllowDelete=1;
//			    igtbl_deleteRow("UltraWebGridBill","UltraWebGridBill_r_"+selRow.getIndex());
			    selRow.setSelected(true);
			    igtbl_setActiveRow("UltraWebGridBill",igtbl_getElementById("UltraWebGridBill_r_"+selRow.getIndex()));
			}
		    return true;
		}
		
		//保存數據管控
		function Save()
		{
	        var grid = igtbl_getGridById('UltraWebGridBill');		        
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			   Count+=1;
		    }
		    if (Count == 0) {
		        alert(Message.clear);
		        return false;
		    }
		    else
		    {
		        return confirm(Message.data)
		    }
		}
		//檢測復制流程的數據
		function CheckCopy()
		{
	        var grid = igtbl_getGridById('UltraWebGridBill');		        
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			   Count+=1;
		    }			
		    if(Count==0)
		    {
		        alert(Message.notdatacopy);
			    return false;
		    }
		    else
		    {
		        if (confirm(Message.surecancopyflow))
	            {
		          
		            return true;
		        }
		        else
		        {			    
		            return false;
		        }
		    }
		}
		//增加
		function Add()
		{
	        var grid = igtbl_getGridById('UltraWebGridAudit');		        
		    var gRows = grid.Rows;
		    var Count=0;
		    for(i=0;i<gRows.length;i++)
		    {
			    if(gRows.getRow(i).getSelected())
			    {
			        Count+=1;			        
			    }
		    }			
		    if(Count==0)
		    {
		        alert(Message.check_limit_add);
			    return false;
		    }
		    return true;
		}
		//刪除全部部門
		function CheckDeleteAll() {
		    if ($("input[id$='hf_deptid']").val() == "") {
		        alert(Message.chosedept);
		        return false;
		    }

		    if (confirm(Message.surehaddeletedeptallsigndata))
	        {
		     
		        return true;
		    }
		    else
		    {			    
		        return false;
		    }
		}
		//-->		
	 //導入簽核數據
		function OpenImport() {
		    var ModuleCode = document.getElementById("hidemodelcode").value;
		    var width = screen.width * 0.95;
		    var height = screen.height * 0.8;
		    //openEditWin("WFMBillAuditFlowImportForm.aspx?ModuleCode=" + ModuleCode, "WFMImport", width, height);
		    window.open("WorkFlowImportForm.aspx?ModuleCode=" + ModuleCode, 'WFMImport', 'height='+height+', width='+width+', top=0,left=0, toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');	
		    return false;
		}	
		//工號批量替換
		 function CheckChageWorkNo()
       {
        var OldWorkNo = document.getElementById("tb_y_empno").value;  
		var NewWorkNo = document.getElementById("tb_n_newpno").value;
		if(OldWorkNo.length==0)
		{
		    alert(Message.oldempisnotnull);
            document.getElementById("tb_y_empno").focus();
            document.getElementById("tb_y_empno").select();
            return false;
		}
		if(NewWorkNo.length==0)
		{
		    alert(Message.newempisnotnull);
            document.getElementById("tb_n_newpno").focus();
            document.getElementById("tb_n_newpno").select();
            return false;
		}
		if(OldWorkNo==NewWorkNo)
		{
		    alert(Message.oldempandnewempnotsame);
            document.getElementById("tb_n_newpno").focus();
            document.getElementById("tb_n_newpno").select();
            return false;
		}
		if (confirm(Message.surereplacethisempno))
        {
	        return true;
	    }
	    else
	    {			    
	        return false;
	    }
	}

	function setSelector(ctrlCode, ctrlName, moduleCode) {
	    var url = "../KQM/BasicData/RelationSelector.aspx?moduleCode=" + moduleCode;
	    var fe = "dialogHeight:500px; dialogWidth:350px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
	    var info = window.showModalDialog(url, null, fe);
	    if (info) {
	        $("#" + ctrlCode).val(info.codeList);
	        $("#" + ctrlName).val(info.nameList);
	    }
	    return false;
	}

	function setSelector_1(str) {
	    var dept = $("input[id$='hf_deptid']").val();
	    var url = "WorkFlowDayset.aspx?DeptId=" + dept + "&Type=" + str;
	    var fe = "dialogHeight:500px; dialogWidth:600px; dialogTop:100px; dialogLeft:500px;status:no;scroll:yes;";
	    var info = window.showModalDialog(url, null, fe);
//	    if (info) {
//	        $("#" + ctrlCode).val(info.codeList);
//	        $("#" + ctrlName).val(info.nameList);
//	    }
//	    return false;
	}
    </script>
</head>
<body>
    <form id="form1" style=" height:100%;" runat="server">

    <div >
      <div style="width: 99%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr_edit">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_replaceempno" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 22px;">
                        <div id="img_edit">
                            <img id="div_img" class="img1" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                    </td>
                </tr>
            </table>
        </div>
       <div id="div_select" style=" width:99%;">
           <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
           <tr >
               <td    style="width:10%;">
                   <asp:Label ID="lbl_doctype" runat="server" Text="Label"></asp:Label>
               </td>
               <td >
                   <cc1:DropDownCheckList ID="ddlDocnoType" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="250" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="250" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                        runat="server">
                    </cc1:DropDownCheckList>
               </td>
               <td style="width:10%;">
                   <asp:Label ID="lbl_Y_empno" runat="server" Text="Label"></asp:Label>
               </td>
               <td >
                   <asp:TextBox ID="tb_y_empno" runat="server"></asp:TextBox>
               </td>
               <td  style="width:10%;">
                   <asp:Label ID="lbl_N_empno" runat="server" Text="Label"></asp:Label>
               </td>
               <td > 
                   <asp:TextBox ID="tb_n_newpno" runat="server"></asp:TextBox>
               </td>
               <td>
                   <asp:Button ID="btn_replace" runat="server" Text="Button" ToolTip="Authority Code:ChangeWorkNo"  CssClass="button_morelarge"
                                                                CommandName="ChangeWorkNo"  
                       OnClientClick="return CheckChageWorkNo()" onclick="btn_replace_Click" />
               </td>
               </tr>
           </table>
       
       </div>
       <br />
       <div style="width: 99%;">
            <table cellspacing="0" cellpadding="0" class="table_title_area">
                <tr style="width: 100%;" id="tr1">
                    <td style="width: 100%;" class="tr_title_center">
                        <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                            background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                            font-size: 13px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_zuzhi" runat="server"></asp:Label>
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
        <div id="div_showdata">
           <div style=" width:99%; text-align:left;">
             <asp:Button ID="btn_Exp" runat="server" Text="Button" 
                   ToolTip="Authority Code:Export" CommandName="Export" onclick="btn_Exp_Click" CssClass="button_morelarge"/>
            <asp:Button ID="Btn_Imp" runat="server" Text="Button"  ToolTip="Authority Code:Import"  CommandName="Import" OnClientClick="return OpenImport()" CssClass="button_1"/>
           </div>
           <div style=" float:left; width:30%; overflow:auto">
               <asp:HiddenField ID="hf_deptid" runat="server" />
               <asp:HiddenField ID="hidemodelcode" runat="server" />
            <script language="javascript">document.write("<DIV id='menu_1' style='height:"+document.body.clientHeight*80/100+"'>");</script>
                <ignav:UltraWebTree ID="UltraWebTreeDept" runat="server" CheckBoxes="false" Indentation="20"
                                    WebTreeTarget="ClassicTree" CollapseImage="../CSS/Images/ig_treeMinus.gif"
                                    ExpandImage="../CSS/Images/expand.gif" DefaultImage="../CSS/Images/ig_treefolder.gif"
                                    DefaultSelectedImage="../CSS/Images/ig_treefolderopen.gif" ImageDirectory="/ig_common/images/"
                                    LoadOnDemand="ManualSmartCallbacks" CompactRendering="False" SingleBranchExpand="True"   OnNodeClicked="UltraWebTreeDept_NodeClicked">
                                    <SelectedNodeStyle Cursor="Hand" BorderWidth="1px" BorderColor="Navy" BorderStyle="None"
                                        ForeColor="Blue" BackgroundImage="../CSS/images/overbg.bmp" BackColor="Navy">
                                        <Padding Bottom="2px" Left="2px" Top="2px" Right="2px"></Padding>
                                    </SelectedNodeStyle>
                                    <ClientSideEvents NodeClick="NodeClick" />
                                    <HoverNodeStyle Cursor="Hand" ForeColor="Black" BackgroundImage="../CSS/Images/overbg.bmp">
                                    </HoverNodeStyle>
                                    <Levels>
                                        <ignav:Level Index="0"></ignav:Level>
                                        <ignav:Level Index="1"></ignav:Level>
                                    </Levels>
                                    <Images>
                                        <DefaultImage Url="../CSS/Images/ig_treefolder.gif" />
                                        <SelectedImage Url="../CSS/Images/ig_treefolderopen.gif" />
                                        <CollapseImage Url="../CSS/Images/ig_treeMinus.gif" />
                                        <ExpandImage Url="../CSS/Images/expand.gif" />
                                    </Images>
                                </ignav:UltraWebTree>
               <script language="javascript">document.write("</DIV>");</script>
            </div>
            
            
            <div style=" float:left; width:69%; ">
                 <div style="width: 99%;">
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                        <tr style="width: 100%;" id="tr2">
                            <td style="width: 100%;" class="tr_title_center">
                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
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
                                <div id="Div2">
                                    <img id="img1" class="img2" width="22px" height="23px" src="../CSS/Images_new/left_back_03_a.gif" /></div>
                            </td>
                        </tr>
                    </table>
                </div>
               
             
                <div id="div_edit" style=" width:99%;">
                   
                  <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
                   <tr class="tr_data_1">
                      <td style=" width:15%;">
                          <asp:Label ID="lbl_doctype_1" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td colspan="3">
                      
                          <asp:DropDownList ID="ddl_doctype_1" runat="server" Width="200px" 
                              onselectedindexchanged="ddl_doctype_1_SelectedIndexChanged" AutoPostBack="true">
                          </asp:DropDownList>
                          
                      </td>                      
                   </tr>
                   <tr runat="server" id="tr_overtimetype"   visible="false">
                    <td>
                        <asp:Label ID="lbl_overtimetype" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td colspan="3">                        
                        <asp:DropDownList ID="ddl_overtimetype" runat="server" Width="200px" 
                            >
                        </asp:DropDownList>                        
                    </td>                    
                   </tr>
                   <tr runat="server" id="tr_leave1" visible="false">
                     <td>
                         <asp:Label ID="lbl_leavedays" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddl_leavedays" runat="server" Width="200px" 
                             >
                         </asp:DropDownList>
                         <a href="javascript:setSelector_1('LeaveDayType');">配置類型</a>
                     </td>
                     <td>
                     <asp:Label ID="lbl_shiwei" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddl_shiwei" runat="server" Width="200px" 
                            >
                         </asp:DropDownList>
                         <a href="javascript:setSelector_1('ShiweiType');">配置類型</a>
                     </td>
                   </tr>
                   <tr runat="server" id="tr_leave2" visible="false">
                     <td>
                         <asp:Label ID="lbl_manger" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddl_manager" runat="server" Width="200px" 
                              >
                         </asp:DropDownList>
                         <a href="javascript:setSelector_1('ManagerType');">配置類型</a>
                     </td>
                     <td>
                         <asp:Label ID="lbl_leavetype" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddl_leavetype" runat="server" Width="200px" 
                            >
                         </asp:DropDownList>
                     </td>
                   </tr>
                   <tr runat="server" id="tr_chucai" visible="false">
                   <td>
                       <asp:Label ID="lbl_chucai" runat="server" Text="Label"></asp:Label>
                       </td>
                       <td>
                           <asp:DropDownList ID="ddl_chucai" runat="server" Width="200px" 
                               >
                           </asp:DropDownList>
                       </td>
                       <td>
                           <asp:Label ID="lbl_chucaidays" runat="server" Text="Label"></asp:Label>
                       </td>
                       <td>
                           <asp:DropDownList ID="ddl_chucaidays" runat="server" Width="200px" 
                               >
                           </asp:DropDownList>
                           <a href="javascript:setSelector_1('OutDayType');">配置類型</a>
                       </td>
                   </tr>
                   </table>
                   
                   <div style=" width:100%;">
                       <asp:Button ID="Btn_All" runat="server" Text="Button" ToolTip="Authority Code:All"
                                                                CommandName="All" CssClass="button_1"
                           onclick="Btn_All_Click" />
                       <asp:Button ID="Btn_delete" runat="server" Text="Button" 
                           ToolTip="Authority Code:Delete" CommandName="Delete" CssClass="button_1"
                           OnClientClick="return Delete();" onclick="Btn_delete_Click" />
                       <asp:Button ID="Btn_save" runat="server" Text="Button" 
                           ToolTip="Authority Code:Save"  CommandName="Save" CssClass="button_1"
                           OnClientClick="return Save();" onclick="Btn_save_Click" />
                   </div>
                   <div style=" width:99%;">  
                                    
                  <table cellspacing="0" cellpadding="0" width="99%">
                                                    <tr>
                                                        <td  style="vertical-align:top; ">   
                                                         <script language="javascript"> document.write("<DIV id='div_2' style='height:" + document.body.clientHeight * 25 / 100 + "'>");</script>                                                  
                                                            <igtbl:UltraWebGrid ID="UltraWebGridBill" runat="server" Width="100%" Height="100%">
                                                                <DisplayLayout Name="UltraWebGridBill" CompactRendering="False" RowHeightDefault="20px"
                                            Version="4.00" SelectTypeRowDefault="Single" BorderCollapseDefault="Separate"
                                            AllowSortingDefault="No" HeaderClickActionDefault="SortSingle" AllowColSizingDefault="Free"
                                            AutoGenerateColumns="false" AllowRowNumberingDefault="ByDataIsland" CellClickActionDefault="CellSelect"
                                            StationaryMargins="HeaderAndFooter">
                                                                    <HeaderStyleDefault VerticalAlign="Middle"  BorderColor="#6699ff" HorizontalAlign="Left"
                                                                        CssClass="tr_header">
                                                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                                                    </HeaderStyleDefault>
                                                                    <FrameStyle Width="100%" Height="100%">
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
                                                                    <igtbl:UltraGridBand BaseTableName="GDS_WF_FLOWSET" Key="GDS_WF_FLOWSET" >
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_EMPNO" Key="FLOW_EMPNO" IsBound="false" Width="15%"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvheadempno %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_EMPNAME" Key="FLOW_EMPNAME" IsBound="false"
                                                                                Width="15%" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvheadsignname %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_NOTES" Key="FLOW_NOTES" IsBound="false" Width="30%"
                                                                                AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,Notes %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_MANAGER" Key="FLOW_MANAGER" IsBound="false"
                                                                                Width="10%" AllowUpdate="No">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                           
                                                                            <igtbl:UltraGridColumn BaseColumnName="FLOW_LEVEL" Key="FLOW_LEVEL" IsBound="false" 
                                                                                Width="20%"  Type="Custom" EditorControlID="ddlAuditManType" AllowUpdate="Yes">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadJuese %>">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                             <igtbl:UltraGridColumn BaseColumnName="FLOW_TYPE" Key="FLOW_TYPE" IsBound="false"
                                                                                Width="10%" Type="Custom" EditorControlID="ddlAuditType" AllowUpdate="Yes">
                                                                                <Header Caption="<%$Resources:ControlText,gvHeadtype %>" >
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>     
                                                            <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>  

                                                            
                                                           <igcmbo:WebCombo ID="ddlAuditType" OnInitializeDataSource="ddlAuditType_OnInitializeDataSource"
                                                                CssClass="input_ddl" runat="server" Version="4.00" Width="100%" ComboTypeAhead="Suggest">
                                                                <DropDownLayout ColHeadersVisible="No" RowSelectors="No" ColFootersVisible="No" DropdownWidth="50px"
                                                                    BorderCollapse="Separate" RowHeightDefault="23px" DropdownHeight="100px" TableLayout="Fixed"
                                                                    StationaryMargins="Header" BaseTableName="HRM_FixedlType" Version="4.00" AutoGenerateColumns="false">
                                                                    <RowAlternateStyle Cursor="Hand" CssClass="tr_data1">
                                                                    </RowAlternateStyle>
                                                                    <FrameStyle Width="100%" Height="100px">
                                                                    </FrameStyle>
                                                                    <RowStyle Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                        CssClass="tr_data_new">
                                                                        <Padding Left="3px" Right="3px"></Padding>
                                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                    </RowStyle>
                                                                    <SelectedRowStyle ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp"></SelectedRowStyle>
                                                                </DropDownLayout>
                                                                <ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DataValue" HeaderText="DataValue" IsBound="false"
                                                                        Width="100%" Key="DataValue">
                                                                        <header caption="DataValue"></header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DataCode" HeaderText="DataCode" IsBound="false"
                                                                        Key="DataCode" Hidden="true">
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igcmbo:WebCombo>
                                                            
                                                            
                                                            <igcmbo:WebCombo ID="ddlAuditManType" OnInitializeDataSource="ddlAuditManType_OnInitializeDataSource"
                                                                CssClass="input_ddl" runat="server" Version="4.00" Width="100%" ComboTypeAhead="Suggest">
                                                                <DropDownLayout ColHeadersVisible="No" RowSelectors="No" ColFootersVisible="No" DropdownWidth="110px"
                                                                    BorderCollapse="Separate" RowHeightDefault="23px" DropdownHeight="100px" TableLayout="Fixed"
                                                                    StationaryMargins="Header" BaseTableName="HRM_FixedlType" Version="4.00" AutoGenerateColumns="false">
                                                                    <RowAlternateStyle Cursor="Hand" CssClass="tr_data1">
                                                                    </RowAlternateStyle>
                                                                    <FrameStyle Width="100%" Height="100px">
                                                                    </FrameStyle>
                                                                    <RowStyle Cursor="Hand" BorderWidth="1px" BorderColor="#DBE1F9" BorderStyle="Solid"
                                                                        CssClass="tr_data_new">
                                                                        <Padding Left="3px" Right="3px"></Padding>
                                                                        <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                                                                    </RowStyle>
                                                                    <SelectedRowStyle ForeColor="Black" BackgroundImage="../CSS/images/overbg.bmp"></SelectedRowStyle>
                                                                </DropDownLayout>
                                                                <ExpandEffects ShadowColor="LightGray"></ExpandEffects>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DataValue" HeaderText="DataValue" IsBound="false"
                                                                        Width="100%" Key="DataValue">
                                                                        <header caption="DataValue"></header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="DataCode" HeaderText="DataCode" IsBound="false"
                                                                        Key="DataCode" Hidden="true">
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igcmbo:WebCombo>
                                                        </td>
                                                        <td class="td_label" width="10">
                                                            <p>
                                                                <img id="imgUp" style="cursor: hand" onclick="MakeImgFlow(1)" alt="" src="../CSS/Images/up.bmp"></p>
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                <img id="imgDown" style="cursor: hand" onclick="MakeImgFlow(2)" alt="" src="../CSS/Images/down.bmp"></p>
                                                        </td>
                                                    </tr>
                                     </table>
                                      
                                               
                   
                   
                   </div>
                  
                   <br />
                   <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
                   
                   <tr class="tr_data_1">
                      <td style=" width:15%;">
                          <asp:Label ID="lbl_doctype1" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td colspan="3">
                          <asp:DropDownList ID="ddl_doctype1" runat="server" Width="200px" 
                              onselectedindexchanged="ddl_doctype1_SelectedIndexChanged" AutoPostBack="true">
                          </asp:DropDownList>
                        
                      </td>                      
                   </tr>
                   
                   <tr runat="server" id="tr_overtimetype1"  visible="false">
                    <td>
                        <asp:Label ID="lbl_leavetype1" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DropDownCheckList ID="ddlCopyBillType" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                    </td>                    
                   </tr>
                   <tr runat="server" id="tr_leavedaystype11"  visible="false">
                     <td>
                         <asp:Label ID="lbl_leavedaystype1" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                          <cc1:DropDownCheckList ID="ddcl_leavedaystype1" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                     </td>
                     <td>
                     <asp:Label ID="lbl_shiwei1" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                          <cc1:DropDownCheckList ID="ddcl_shiwei1" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                     </td>
                   </tr>                   
                   <tr runat="server" id="tr_leavedaystype12"  visible="false">
                     <td>
                         <asp:Label ID="lblManager" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                         <cc1:DropDownCheckList ID="ddcl_manager" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                     </td>
                     <td>
                         <asp:Label ID="lblLeaveType1" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td>
                        <cc1:DropDownCheckList ID="ddcl_leavetype" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                     </td>
                   </tr>
                   <tr runat="server" id="tr_chucai1"  visible="false">
                   <td>
                       <asp:Label ID="lbl_chucai1" runat="server" Text="Label"></asp:Label>
                       </td>
                       <td>
                        <cc1:DropDownCheckList ID="ddcl_chucai1" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                       </td>
                       <td>
                           <asp:Label ID="lbl_chucaidays1" runat="server" Text="Label"></asp:Label>
                       </td>
                       <td>
                        <cc1:DropDownCheckList ID="ddcl_chucaidays1" CheckListCssStyle="background-image: url(../CSS/images/inputbg.bmp);height: 200px;overflow: scroll;"
                            Width="200" RepeatColumns="1" CssClass="input_textBox" DropImageSrc="../CSS/Images/expand.gif"
                            TextWhenNoneChecked="" DisplayTextWidth="200" ClientCodeLocation="../JavaScript/DropDownCheckList.js"
                            runat="server">
                        </cc1:DropDownCheckList>
                       </td>
                   </tr>
                   </table>
                   <div style=" width:100%; text-align:left;">
                     <asp:Button ID="btn_copy" runat="server" Text="Button" OnClientClick="return CheckCopy();" onclick="btn_copy_Click" CssClass="button_1"/>
                       <asp:Button ID="btn_allclear" runat="server" Text="Button" 
                         OnClientClick="return CheckDeleteAll()"   onclick="btn_allclear_Click" CssClass="button_morelarge"/>
                       <asp:Button ID="btn_ExpA" runat="server" Text="Button" 
                           onclick="btn_ExpA_Click" CssClass="button_1"/>
                   </div>
                </div>
                 <div style="width: 100%;">
                    <table cellspacing="0" cellpadding="0" class="table_title_area">
                        <tr style="width: 100%;" id="tr3">
                            <td style="width: 100%;" class="tr_title_center">
                                <table cellspacing="0" cellpadding="0" border="0" style="height: 25px; background-image: url('../CSS/Images_new/org_main_02.gif');
                                    background-repeat: no-repeat; background-position-x: center; width: 75px; text-align: center;
                                    font-size: 13px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_select_1" runat="server"></asp:Label>
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
                
                <div id="div_down" >
                 <table class="table_data_area" cellspacing="0" cellpadding="0" width="100%" >
                    <tr class="tr_data_2">
                      <td style=" width:10%;">
                          <asp:Label ID="lbl_Unit" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td>
                          <asp:TextBox ID="tb_unit" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtOrgCode" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                             <asp:Image ID="imgDepCode" runat="server" style=" cursor:pointer;" class="img_hidden" ImageUrl="../CSS/Images_new/search_new.gif"></asp:Image>
                       </td>
                       <td style=" width:10%;">
                          <asp:Label ID="lbl_empno" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td>
                          <asp:TextBox ID="tb_empno" runat="server"></asp:TextBox>
                       </td>
                       <td style=" width:10%;">
                          <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td>
                          <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                       </td>
                    </tr>
                 </table>
                 <div style=" width:100%; text-align:left;">
                     <asp:Button ID="Btn_select" runat="server" Text="Button" onclick="Btn_select_Click" ToolTip="Authority Code:Query" CssClass="button_1"/>
                     <asp:Button ID="Btn_Add" runat="server" Text="Button" 
                         OnClientClick="return Add();"  ToolTip="Authority Code:Add" CssClass="button_1"
                                                                CommandName="Add" 
                         onclick="Btn_Add_Click"/>
                 </div>
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
                                                        <FrameStyle Width="100%" Height="100%">
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
                                                        <igtbl:UltraGridBand BaseTableName="gds_att_employee" Key="gds_att_employee">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="depname" Key="depname" IsBound="false" Width="30%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadDept%>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="workno" Key="workno" IsBound="false" Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvheadempno %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="localname" Key="localname" IsBound="false"
                                                                    Width="15%">
                                                                    <Header Caption="<%$Resources:ControlText,gvheadsignname %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="notes" Key="notes" IsBound="false" Width="25%">
                                                                    <Header Caption="<%$Resources:ControlText,Notes %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="managername" Key="managername" IsBound="false"
                                                                    Width="10%">
                                                                    <Header Caption="<%$Resources:ControlText,gvHeadmanager %>">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                        </igtbl:UltraWebGrid>
                        <script language="JavaScript" type="text/javascript">document.write("</DIV>");</script>
               
                </div>
                
            </div>
         </div>
    </div>
   
    </form>
</body>
</html>
