using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WorkFlowSetBll : BLLBase<IWorkFlowSetDal>
    {
        /// <summary>
        /// 獲取單據類型
        /// </summary>
        /// <returns></returns>
        public DataTable GetDocNoTypeList(string type)
        {
            return DAL.GetDocNoTypeList(type);
        }

        public DataTable GetDocNoTypeList()
        {
            return DAL.GetDocNoTypeList();
        }

        public DataTable GetDocNoTypeList_new(string type)
        {
            return DAL.GetDocNoTypeList_new(type);
        }

        public DataTable GetOverTimeType(string type)
        {
            return DAL.GetOverTimeType(type);
        }

        /// <summary>
        /// 獲取簽核路徑
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>
        public DataTable GetSignPath(string deptcode, string formtype, List<string> resonlist)
        {
            return DAL.GetSignPath(deptcode, formtype, resonlist);
        }

         /// <summary>
        /// 流程設定保存
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="Formtype"></param>
        /// <param name="resonlist"></param>
        /// <param name="driy"></param>
        /// <returns></returns>
        public bool SaveData(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy)
        {
            return DAL.SaveData(deptid, Formtype, resonlist, driy);
        }

        /// <summary>
        /// 判斷部門代碼是否存在
        /// </summary>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public int IsExistsDeptCode(string deptcode)
        {
            return DAL.IsExistsDeptCode(deptcode);
        }


         /// <summary>
        /// 獲取部門ID 下的所有簽核路徑
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public List<WorkFlowSetModel> GetExpAllSignPath(string deptid)
        {
            return DAL.GetExpAllSignPath(deptid);
        }

        
        /// <summary>
        /// 根據工號獲取管理職
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        public string GetManager(string empno)
        {
            return DAL.GetManager(empno);
        }

        /// <summary>
        /// 刪除全部流程
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public bool DeleteAllWorkFlowData(string deptid)
        {
            return DAL.DeleteAllWorkFlowData(deptid);
        }

        /// <summary>
        /// 導出Excel
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public List<WorkFlowSignModel> GetSignExpData(string deptid)
        {
            return DAL.GetSignExpData(deptid);
        }

        /// <summary>
        /// 批量替換指定工號
        /// </summary>
        /// <param name="info_FormType"></param>
        /// <param name="o_empno"></param>
        /// <param name="n_empno"></param>
        /// <param name="n_name"></param>
        /// <param name="notes"></param>
        /// <param name="n_manager"></param>
        /// <returns></returns>
        public bool ReplaceAllEmpno(string[] info_FormType, string o_empno, string n_empno, string n_name, string notes, string n_manager)
        {
            return DAL.ReplaceAllEmpno(info_FormType, o_empno, n_empno, n_name, notes, n_manager);
        }

         /// <summary>
        /// 查詢數據
        /// </summary>
        /// <param name="dept_list"></param>
        /// <param name="empno"></param>
        /// <param name="empname"></param>
        /// <returns></returns>
        public DataTable GetDeptPerson(List<string> dept_list, string empno, string empname)
        {
            return DAL.GetDeptPerson(dept_list, empno, empname);
        }


        /// <summary>
        /// 獲取天數類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetDaysType(string deptid, string day_type)
        {
            return DAL.GetDaysType(deptid, day_type);
        }
        
        /// <summary>
        /// 獲取天數類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public DataTable GetDaysType_1(string deptid, string day_type)
        {
            return DAL.GetDaysType_1(deptid, day_type);
        }

          /// <summary>
        /// 插入天數數據
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="dayscode"></param>
        /// <param name="daysname"></param>
        /// <param name="mindays"></param>
        /// <param name="maxdays"></param>
        /// <param name="day_type"></param>
        /// <returns></returns>
        public bool InsertDayType(string deptid, string dayscode, string daysname, int mindays, int maxdays, string day_type)
        {
            return DAL.InsertDayType(deptid, dayscode, daysname, mindays, maxdays, day_type);
        }

        /// <summary>
        /// 刪除DAY數據
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="dayscode"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        public bool DeleteDayType(string deptid, string dayscode, string typecode)
        {
            return DAL.DeleteDayType(deptid, dayscode, typecode);
        }

        /// <summary>
        /// 獲取師資位表
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiwei()
        {
            return DAL.GetShiwei();
        }
        /// <summary>
        /// 獲取師資位表
        /// </summary>
        /// <returns></returns>
        public DataTable GetGlz()
        {
            return DAL.GetGlz();
        }

         /// <summary>
        /// 獲取類別
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetKeyValue(string deptid, string type)
        {
            return DAL.GetKeyValue(deptid, type);
        }

    }
}
