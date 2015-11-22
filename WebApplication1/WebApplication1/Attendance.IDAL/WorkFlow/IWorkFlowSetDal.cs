using System;
using System.Collections.Generic;

using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WorkFLowSetDal")]
    public interface IWorkFlowSetDal
    {
        /// <summary>
        /// 單據類型
        /// </summary>
        /// <returns></returns>
        DataTable GetDocNoTypeList(string type);

        DataTable GetDocNoTypeList_new(string type);

        DataTable GetDocNoTypeList();

        DataTable GetOverTimeType(string type);

        /// <summary>
        /// 獲取簽核路徑
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>
        DataTable GetSignPath(string deptcode, string formtype, List<string> resonlist);

        /// <summary>
        /// 流程保存設定
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="Formtype"></param>
        /// <param name="resonlist"></param>
        /// <param name="driy"></param>
        /// <returns></returns>
        bool SaveData(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy);


        /// <summary>
        /// 判讀部門代碼是否存在
        /// </summary>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        int IsExistsDeptCode(string deptcode);


        /// <summary>
        /// 獲取所有的簽核路徑
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        List<WorkFlowSetModel> GetExpAllSignPath(string deptid);

        /// <summary>
        /// 獲取管理職
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        string GetManager(string empno);

        /// <summary>
        /// 刪除全部流程
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        bool DeleteAllWorkFlowData(string deptid);

        /// <summary>
        /// 導出Excel
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        List<WorkFlowSignModel> GetSignExpData(string deptid);

        /// <summary>
        /// 批量替換工號
        /// </summary>
        /// <param name="info_FormType"></param>
        /// <param name="o_empno"></param>
        /// <param name="n_empno"></param>
        /// <param name="n_name"></param>
        /// <param name="notes"></param>
        /// <param name="n_manager"></param>
        /// <returns></returns>
        bool ReplaceAllEmpno(string[] info_FormType, string o_empno, string n_empno, string n_name, string notes, string n_manager);

        /// <summary>
        /// 查詢數據
        /// </summary>
        /// <param name="dept_list"></param>
        /// <param name="empno"></param>
        /// <param name="empname"></param>
        /// <returns></returns>
        DataTable GetDeptPerson(List<string> dept_list, string empno, string empname);


        DataTable GetDaysType(string deptid, string day_type);
        DataTable GetDaysType_1(string deptid, string day_type);

        bool InsertDayType(string deptid, string dayscode, string daysname, int mindays, int maxdays, string day_type);

        bool DeleteDayType(string deptid, string dayscode, string typecode);

        DataTable GetShiwei();

        DataTable GetGlz();

        DataTable GetKeyValue(string deptid, string type);

    }
}
