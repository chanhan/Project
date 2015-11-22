using System;
//using ASP;
//using eBFW.Sys;
using System.Data;
using System.Web.Profile;
using System.Web.UI.WebControls;

namespace GDSBG.MiABU.Attendance.Web.ControlLib
{
    public partial class Title : BaseControl
    {
        //protected DataSet dataSet;
        ////protected Label LabelPath;
        //public string moduleCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!base.IsPostBack)
            //    {
            //        if (this.moduleCode.Trim().Length == 0)
            //        {
            //            this.moduleCode = base.Request["ModuleCode"];
            //        }
            //        if (this.moduleCode != null)
            //        {
            //            string CS40001 = this.moduleCode;
            //            if (CS40001 == null)
            //            {
            //                goto Label_00C0;
            //            }
            //            if (!(CS40001 == "Desktop"))
            //            {
            //                if (CS40001 == "Option")
            //                {
            //                    goto Label_00A4;
            //                }
            //                goto Label_00C0;
            //            }
            //            this.LabelPath.Text = base.GetResouseValue("bfw.menu.homepage");
            //        }
            //    }
            //    return;
            //Label_00A4:
            //    this.LabelPath.Text = base.GetResouseValue("bfw.menu.option");
            //    return;
            //Label_00C0:
            //    //this.dataSet = ((ServiceLocator) base.Session["serviceLocator"]).GetModuleData().GetPathData(this.moduleCode);
                

            //    bool firstPath = true;
            //    foreach (DataRow row in this.dataSet.Tables["bfw_module"].Rows)
            //    {
            //        if (firstPath)
            //        {
            //            this.LabelPath.Text = base.GetResouseValue("bfw.correntpath") + ":" + base.GetResouseValue(Convert.ToString(row["modulecode"]));
            //            firstPath = false;
            //        }
            //        else
            //        {
            //            this.LabelPath.Text = this.LabelPath.Text + "-->" + base.GetResouseValue(Convert.ToString(row["modulecode"]));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    base.Response.Write("Title.ascx" + ex.Message.ToString());
            //}
        }


        //protected global_asax ApplicationInstance
        //{
        //    get
        //    {
        //        return (global_asax)this.Context.ApplicationInstance;
        //    }
        //}

        //protected DefaultProfile Profile
        //{
        //    get
        //    {
        //        return (DefaultProfile)this.Context.Profile;
        //    }
        //}
    }
}