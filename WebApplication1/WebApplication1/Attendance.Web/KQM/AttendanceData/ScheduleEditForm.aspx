<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleEditForm.aspx.cs"
    Inherits="GDSBG.MiABU.Attendance.Web.KQM.AttendanceData.ScheduleEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=this.GetResouseValue("bfw.kqm_employeeshift.title")%>
    </title>
    <style type="text/css">
        .a
        {
            border: 0;
        }
        .b
        {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
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
		if(igtbl_getElementById("ProcessFlag").value.length==0 && row != null)
		{
			igtbl_getElementById("txtStartDate").value=row.getCell(3).getValue()==null?"":row.getCell(3).getValue('yyyy/mm/dd');
			igtbl_getElementById("txtBoxEndDate").value=row.getCell(4).getValue()==null?"":row.getCell(4).getValue('yyyy/mm/dd');
			igtbl_getElementById("HiddenShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("ddlShiftNo").value=row.getCell(5).getValue()==null?"":row.getCell(5).getValue();
			document.all("HiddenID").value=row.getCell(6).getValue()==null?"":row.getCell(6).getValue();
			document.all("HiddenStartDate").value=igtbl_getElementById("txtStartDate").value;
			document.all("HiddenEndDate").value=igtbl_getElementById("txtEndDate").value;

		}
    }
    
     $('#<%=btnAdd.ClientID %>').click(function ()
     {
          $("#btnQuery,#btnAdd,#btnModify,#btnDelete").attr("disabled","disabled");
          $("#topTable").addClass("b");
          $("#bottomTable").removeClass("b");
          $("#<%=hidOperate.ClientID %>").val("Add");
          $("#textBoxEmployeeNo,#textBoxChineseName,#textBoxSYC,#ddlShiftNo,#textBoxStartDate,#textBoxEndDate").val(null);
     }
     
       $('#<%=btnModify.ClientID %>').click(function()
            {
             $("#<%=txtWorkNo.ClientID %>").css("border-style", "none");
             $("#<%=txtWorkNo.ClientID %>").removeAttr("readonly");

           var WorkNo = $.trim($("#<%=txtWorkNo.ClientID%>").val());
           if (WorkNo =="")
           {
            alert(Message.AtLastOneChoose);
            return false;
           }
          else
          { 
            $("#<%=txtWorkNo.ClientID %>").css("border-style", "none");
            $("#<%=txtWorkNo.ClientID %>").attr("readonly","true");
            $("#<%=btnCancel.ClientID %>").removeAttr("disabled");
            $("#<%=btnSave.ClientID %>").removeAttr("disabled"); 
            $("#<%=hidOperate.ClientID %>").val("Modify");
          return false; 
          }
         }
            );
         
   });
   
    </script>

</head>
<body class="color_body">
    <form id="form1" runat="server">
    </form>
</body>
</html>
