/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEmployeeShiftDal.cs
 * 檔功能描述： 排班作業數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.KaoQinData
{
    public class EmployeeShiftDal : DALBase<EmployeeShiftModel>, IEmployeeShiftDal
    {
        /// <summary>
        /// 查詢排班信息
        /// </summary>
        /// <param name="model">包含工號，姓名，類別</param>
        /// <param name="pageIndex">分頁索引（第幾頁）</param>
        /// <param name="pageSize">每頁數據量</param>
        /// <param name="totalCount">總記錄數</param>
        /// <param name="depcode">部門代碼</param>
        /// <param name="shiftdate">排班日期</param>
        /// <param name="BatchEmployeeNo">批量查詢時傳入的工號</param>
        /// <param name="shift">班別</param>
        /// <param name="sqlDep">管控代碼</param>
        /// <returns></returns>
        public DataTable GetShiftInfo(EmployeeShiftModel model, int pageIndex, int pageSize, out int totalCount, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep)
        {

            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT *  FROM employeeshift_v a where (status < '2' or leavedate >= TO_DATE (:shiftdate, 'yyyy/mm/dd')) and startdate <=TO_DATE (:shiftdate, 'yyyy/mm/dd') and TO_DATE (:shiftdate, 'yyyy/mm/dd') <= enddate and dcode in (" + sqlDep + ")";
            cmdText = cmdText + strCon;
            if (depcode != "")
            {
                cmdText = cmdText + " AND dcode in (SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depcode + "' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (shift != "")
            {
                cmdText = cmdText + " and shiftno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + shift + "', '§')))";
            }
            listPara.Add(new OracleParameter(":shiftdate", shiftdate));
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 導出查詢排班作業
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetShiftInfo(EmployeeShiftModel model, string depcode, string shiftdate, string BatchEmployeeNo, string shift, string sqlDep)
        {

            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT *  FROM employeeshift_v a where (status < '2' or leavedate >= TO_DATE (:shiftdate, 'yyyy/mm/dd')) and startdate <=TO_DATE (:shiftdate, 'yyyy/mm/dd') and TO_DATE (:shiftdate, 'yyyy/mm/dd') <= enddate and dcode in (" + sqlDep + ")";
            cmdText = cmdText + strCon;
            if (depcode != "")
            {
                cmdText = cmdText + " AND dcode in (SELECT DepCode FROM gds_att_department START WITH depcode = '" + depcode + "' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (shift != "")
            {
                cmdText = cmdText + " and shiftno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + shift + "', '§')))";
            }
            listPara.Add(new OracleParameter(":shiftdate", shiftdate));
            DataTable dt = DalHelper.ExecuteQuery(cmdText,listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 獲得類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetShiftType()
        {
            string str = "SELECT   datatype, datacode, datavalue, datatypedetail, (datacode || '?B' || datavalue) AS newdatavalue FROM gds_att_typedata  WHERE datatype = 'KQMShiftType' ORDER BY orderid";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得權限下的班別類型
        /// </summary>
        /// <param name="PersonDepCode"></param>
        /// <returns></returns>
        public DataTable GetShift(string PersonDepCode)
        {
            string str = "SELECT * FROM (SELECT a.*, a.shiftno  || ':'|| a.shiftdesc|| '['|| (SELECT datavalue FROM gds_att_typedata b WHERE b.datatype = 'ShiftType'  AND b.datacode = a.shifttype)|| ']'|| DECODE (islactation, 'Y', '哺乳期', '') shiftdetail, (SELECT datavalue  FROM gds_att_typedata b WHERE b.datatype = 'ShiftType'" +
                " AND b.datacode = a.shifttype) shifttypename,(SELECT depname FROM gds_sc_department b WHERE b.depcode = a.orgcode) orgname,(SELECT depname  FROM gds_sc_department b WHERE b.depcode = a.shareorgcode) shareorgname FROM gds_att_workshift a WHERE 1 = 1 and (expiredate is null or expiredate>=trunc(sysdate)) and OrgCode in (SELECT DepCode FROM gds_sc_department START WITH depcode = '" + PersonDepCode + "' CONNECT BY PRIOR depcode = parentdepcode) ) order by shiftno ";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmployeeShiftModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }




        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_att_employeeshift_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }


        /// <summary>
        /// 獲得工號的對應信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string workno)
        {
            string str = "select * from employeeshift_employee_select where WorkNO='" + workno + "'";
            return DalHelper.ExecuteQuery(str);
        }


        /// <summary>
        /// 獲得對應工號的例外加班信息
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmployeeShifInfo(string workno)
        {
            string str = "select ID,workno,localname,dcode,dname,shiftno,to_char(startdate,'yyyy/mm/dd') startdate,to_char(enddate,'yyyy/mm/dd') enddate,startenddate,shiftdesc,shiftflag,shifttype,status,leavedate,shiftdays,update_user,update_date from employeeshift_v where flag='employeeshift' and WorkNO='" + workno + "'";
            return DalHelper.ExecuteQuery(str);
        }


        /// <summary>
        /// 修改例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool UpdateEmployeeShiftByKey(EmployeeShiftModel model, SynclogModel logmodel)
        {
            string strCon = "";
            string str = "UPDATE gds_att_employeeshift SET ShiftNo = '" + model.Shift + "',StartDate =to_date( '" + model.StartDate + "','YYYY/MM/DD'),EndDate =to_date( '" + model.EndDate + "','YYYY/MM/DD'),Update_User = '" + model.UpdateUser + "',Update_Date = sysdate WHERE ID = '" + model.ID + "'";
            return DalHelper.ExecuteNonQuery(str, logmodel) != -1;
        }

        /// <summary>
        /// 新增例外排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool AddEmployeeShift(EmployeeShiftModel model, SynclogModel logmodel)
        {
            string str = "INSERT INTO gds_att_employeeshift ( workno, shiftno, startdate, enddate,update_user, update_date ) VALUES ('" + model.WorkNo + "','" + model.Shift + "',to_date('" + model.StartDate + "','yyyy/mm/dd'),to_date('" + model.EndDate + "','yyyy/mm/dd'),'" + model.UpdateUser + "',sysdate)";
            return DalHelper.ExecuteNonQuery(str,logmodel) != -1;
        }


        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteEmployeeShiftByKey(string ID, SynclogModel logmodel)
        {
            string str = "delete from gds_att_employeeshift where ID='" + ID + "'";
            return DalHelper.ExecuteNonQuery(str,logmodel) != -1;
        }


        /// <summary>
        /// 查詢對應部門的組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable SelectOrgShift(string depcode)
        {
            string str = "select a.*,trunc((a.enddate-a.startdate),1) shiftdays, (SELECT shiftno || ':' || shiftdesc || '[' || shifttype || ']' FROM gds_att_workshift c  WHERE c.shiftno = a.shiftno) shiftdesc, (   TO_CHAR (a.startdate, 'yyyy/mm/dd')|| '~'|| TO_CHAR (a.enddate, 'yyyy/mm/dd')) startenddate  from GDS_ATT_ORGSHIFT a where a.orgcode='" + depcode + "'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 新增組織排班
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public bool AddOrgShift(string depcode, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel)
        {
            string str = "INSERT INTO GDS_ATT_ORGSHIFT ( ORGCODE, shiftno, startdate, enddate,update_user, update_date ) VALUES ('" + depcode + "','" + shiftno + "',to_date('" + startdate + "','yyyy/mm/dd'),to_date('" + endate + "','yyyy/mm/dd'),'" + personcode + "',sysdate)";
            return DalHelper.ExecuteNonQuery(str,logmodel) != -1;
        }

        /// <summary>
        /// 更新組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="shiftno"></param>
        /// <param name="startdate"></param>
        /// <param name="endate"></param>
        /// <returns></returns>
        public bool UpdateOrgShift(string ID, string shiftno, string startdate, string endate, string personcode, SynclogModel logmodel)
        {
            string str = "update GDS_ATT_ORGSHIFT set shiftno='" + shiftno + "',startdate=to_date('" + startdate + "','YYYY/MM/DD'),enddate=to_date('" + endate + "','YYYY/MM/DD'),UPDATE_USER='" + personcode + "',UPDATE_DATE=sysdate where ID='" + ID + "'";
            return DalHelper.ExecuteNonQuery(str,logmodel) != -1;
        }

        /// <summary>
        /// 刪除組織排班
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteOrgShift(string ID, SynclogModel logmodel)
        {
            string str = "delete from GDS_ATT_ORGSHIFT where ID='" + ID + "'";
            return DalHelper.ExecuteNonQuery(str,logmodel) != -1;
        }
    }
}
