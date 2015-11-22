/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMoveShiftDal.cs
 * 檔功能描述： 免卡人員加班導入數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.23
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.KaoQinData
{
    public class KQMMoveShiftDal : DALBase<MoveShiftModel>, IKQMMoveShiftDal
    {
        /// <summary>
        /// 分頁查詢彈性調班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="depCode"></param>
        /// <param name="workDate1"></param>
        /// <param name="workDate2"></param>
        /// <param name="noWorkDate1"></param>
        /// <param name="noWorkDate2"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMoveShiftTable(MoveShiftModel model, string sqlDep,string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            string depName = model.DepName.ToString();
            model.DepName = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno,workdate,workstime,worketime,noworkdate,noworkstime,noworketime,timeqty,remark,create_date,create_user,update_date,update_user
                            , localname,modifyname,depcode,depname FROM gds_att_MoveShift_v  a where 1=1";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(depName))
            {
                cmdText += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                cmdText += " AND a.depcode IN (" + sqlDep + ") ";
            }

            if (!string.IsNullOrEmpty(workDate1) && !string.IsNullOrEmpty(workDate2))
            {
                cmdText += " and (WORKDATE between to_date(:workDate1,'yyyy/mm/dd') and to_date(:workDate2,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":workDate1", workDate1));
                listPara.Add(new OracleParameter(":workDate2", workDate2));
            }

            if (!string.IsNullOrEmpty(noWorkDate1) && !string.IsNullOrEmpty(noWorkDate2))
            {
                cmdText += " and (NOWORKDATE between to_date(:noWorkDate1,'yyyy/mm/dd') and to_date(:noWorkDate2,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":noWorkDate1", noWorkDate1));
                listPara.Add(new OracleParameter(":noWorkDate2", noWorkDate2));
            }

            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, MoveShiftModel moveShiftModel)
        {
            DataTable dt = new DataTable();
            string value = "";
            if (flag == "KQMReGetKaoQin")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(MAX(paravalue),'5') from gds_sc_parameter where paraname='KQMReGetKaoQin'");
            }
            if (flag == "DayWorkHours")
            {
                dt = DalHelper.ExecuteQuery(@"Select nvl(Max(ParaValue),8) from gds_sc_parameter where ParaName='DayWorkHours'");
            }
            if (flag == "condition1")
            {
                dt = DalHelper.ExecuteQuery(@"select to_date('" + DateTime.Now.AddDays((double)(-DateTime.Now.Day - 4)).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')-to_date(:noWorkDate,'yyyy/mm/dd') from dual", new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (flag == "condition2")
            {
                dt = DalHelper.ExecuteQuery(@"select sum(ottotal) from ( select count(1) ottotal from gds_att_advanceapply where workno=:empNo and otdate=to_date(:noWorkDate,'yyyy/mm/dd') and ottype='G2' union all select count(1) ottotal from gds_att_realapply where workno=:empNo and otdate=to_date(:noWorkDate,'yyyy/mm/dd')  and ottype='G2') ", new OracleParameter(":empNo", moveShiftModel.WorkNo.ToString()), new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (flag == "condition3")
            {
                dt = DalHelper.ExecuteQuery(@"select to_date('" + DateTime.Now.AddDays((double)(-DateTime.Now.Day - 4)).ToString("yyyy/MM/dd") + "','yyyy/mm/dd')-to_date(:noWorkDate,'yyyy/mm/dd') from dual", new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (dt != null)
            {
                value = dt.Rows[0][0].ToString().Trim();
            }


            return value;
        }


        /// <summary>
        /// 根據model刪除記錄
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteMoveShift(MoveShiftModel model,SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }


        /// <summary>
        /// 重新計算考勤記錄
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sOrgCode"></param>
        /// <param name="sFromKQDate"></param>
        /// <param name="sToKQDate"></param>
        public void GetKaoQinData(string sWorkNo, string sOrgCode, string sFromKQDate, string sToKQDate)
        {
            try
            {
                int a = DalHelper.ExecuteNonQuery("Prog_ReGet_KaoQinData", CommandType.StoredProcedure, new OracleParameter("p_WorkNo", sWorkNo),
                 new OracleParameter("p_OrgCode", sOrgCode), new OracleParameter("p_FrmKQDate", sFromKQDate), new OracleParameter("p_ToKQDate", sToKQDate));
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo, string sqlDep)
        {
            DataTable dt = DalHelper.ExecuteQuery("select * from gds_att_employee_v a where a.workno=:empNo  AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DCode)", new OracleParameter(":empNo", EmployeeNo));
            return dt;
        }


        /// <summary>
        /// 查詢員工具體調班信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="WorkDate"></param>
        /// <param name="NoWorkDate"></param>
        /// <returns></returns>
        public DataTable GetData(MoveShiftModel moveShiftModel, string condition, string hidWorkDate)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT * FROM (SELECT a.*, b.localname, (SELECT localname  FROM gds_att_employee c WHERE c.workno = a.update_user) modifyname,
                         b.dname depname FROM gds_att_moveshift a, gds_att_employee b WHERE b.workno = a.workno ";
            if (condition == "condition1")
            {
                cmdText += " and a.WorkNO=:empNo AND a.WorkDate=to_date(:workdate,'yyyy/mm/dd') AND a.NoWorkDate=to_date(:noWorkDate,'yyyy/mm/dd') ";
                if (!string.IsNullOrEmpty(hidWorkDate))
                {
                    string sqlDep = hidWorkDate;
                    cmdText += "  AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=b.DCode)";
                }
                cmdText += " ) ";
                
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", moveShiftModel.WorkNo), new OracleParameter(":workDate", moveShiftModel.WorkDate.Value.ToString("yyyy/MM/dd")), new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (condition == "condition2")
            {
                cmdText += "and a.WorkNO=:empNo AND (a.WorkDate = to_date(:workDate,'yyyy/mm/dd') or a.NoWorkDate = to_date(:noWorkDate,'yyyy/mm/dd') or a.NoWorkDate = to_date(:workDate,'yyyy/mm/dd') or a.WorkDate = to_date(:noWorkDate,'yyyy/mm/dd')))";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", moveShiftModel.WorkNo), new OracleParameter(":workDate", moveShiftModel.WorkDate.Value.ToString("yyyy/MM/dd")), new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (condition == "condition3")
            {
                string work = DateTime.Parse(hidWorkDate).ToString("yyyy/MM/dd");
                cmdText += "and a.WorkNO=:empNo AND a.WorkDate <> to_date(:hidWorkDate,'yyyy/mm/dd') AND (a.NoWorkDate = to_date(:noWorkDate,'yyyy/mm/dd') or a.WorkDate = to_date(:noWorkDate,'yyyy/mm/dd') or a.NoWorkDate = to_date(:workDate,'yyyy/mm/dd') or a.WorkDate =to_date(:workDate,'yyyy/mm/dd')) )";

                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", moveShiftModel.WorkNo), new OracleParameter(":hidWorkDate", work), new OracleParameter(":workDate", moveShiftModel.WorkDate.Value.ToString("yyyy/MM/dd")), new OracleParameter(":noWorkDate", moveShiftModel.NoWorkDate.Value.ToString("yyyy/MM/dd")));
            }
            if (condition == "condition4")
            {
                string work = DateTime.Parse(hidWorkDate).ToString("yyyy/MM/dd");
                cmdText += "and a.WorkNO=:empNo AND a.WorkDate = to_date(:hidWorkDate,'yyyy/mm/dd')) ";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":empNo", moveShiftModel.WorkNo), new OracleParameter(":hidWorkDate", work));
            }


            return dt;
        }

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetOTType(string workNo, string date)
        {
            string value = "";
            OracleParameter ottype = new OracleParameter("v_ottype", OracleType.VarChar, 20);
            ottype.Direction = ParameterDirection.Output;
            int a = DalHelper.ExecuteNonQuery("GDS_ATT_getempottype_pro", CommandType.StoredProcedure,
                new OracleParameter("v_workno", workNo), new OracleParameter("v_date", date), ottype);
            if (a > 0)
            {
                value = Convert.ToString(ottype.Value);
            }
            return value;
        }




        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddMoveShift(MoveShiftModel model,SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMoveShiftByKey(MoveShiftModel model,SynclogModel logmodel)
        {

            return DalHelper.UpdateByKey(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateMoveShift(MoveShiftModel model, string workDate,SynclogModel logmodel)
        {
            string sql=@"update gds_att_moveshift set WORKNO=:workNo ,WORKDATE=to_date(:workDate,'YYYY/MM/DD'),WORKSTIME=:workSTime,WORKETIME=:workETime,
            NOWORKDATE=to_date(:noWorkDate,'YYYY/MM/DD'),NOWORKSTIME=:noWorkSTime,
            NOWORKETIME=:noWorkETime,TIMEQTY=:timeQty,REMARK=:remark,UPDATE_DATE=sysdate,UPDATE_USER=:updateUser where 
            WORKNO=:workNo  and to_char(WORKDATE,'YYYY/MM/DD')=:workDateOld";
            List<OracleParameter> list = new List<OracleParameter>();
            list.Add( new OracleParameter(":workNo",model.WorkNo));
            list.Add(new OracleParameter(":workDate", Convert.ToDateTime(model.WorkDate).ToString("yyyy/MM/dd") ));
            list.Add(new OracleParameter(":workSTime", model.WorkSTime));
            list.Add(new OracleParameter(":workETime", model.WorkETime));
            list.Add(new OracleParameter(":noWorkDate", Convert.ToDateTime(model.NoWorkDate).ToString("yyyy/MM/dd")));
            list.Add(new OracleParameter(":noWorkSTime", model.NoWorkSTime));
            list.Add(new OracleParameter(":noWorkETime", model.NoWorkETime));
            list.Add(new OracleParameter(":timeQty", model.TimeQty));
            list.Add(new OracleParameter(":remark", model.Remark));
            list.Add(new OracleParameter(":updateUser",model.UpdateUser ));
            list.Add( new OracleParameter(":workDateOld", workDate));

            return  DalHelper.ExecuteNonQuery(sql, logmodel, list.ToArray())!= -1;
        }


        /// <summary>
        /// 導入彈性調班信息,正確信息插入正式表,錯誤信息返回datatable
        /// </summary>
        /// <param name="createUser">創建人</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetTempTableErrorData(string createUser, out int successNum, out int errorNum,SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable TempErrorTable = DalHelper.ExecuteQuery("gds_att_moveshift_temp_check", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", createUser), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString())
                );
            successNum = Convert.ToInt32(outSuccess.Value);
            errorNum = Convert.ToInt32(outError.Value);
            return TempErrorTable;
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<MoveShiftModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }



        /// <summary>
        /// 查詢彈性調班資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="depCode"></param>
        /// <param name="workDate1"></param>
        /// <param name="workDate2"></param>
        /// <param name="noWorkDate1"></param>
        /// <param name="noWorkDate2"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetMoveShiftTable(MoveShiftModel model,string sqlDep, string depCode, string workDate1, string workDate2, string noWorkDate1, string noWorkDate2)
        {
            string strCon = "";
            string depName = model.DepName.ToString();
            model.DepName = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno,workdate,workstime,worketime,noworkdate,noworkstime,noworketime,timeqty,remark,create_date,create_user,update_date,update_user
                            , localname,modifyname,depcode,depname FROM gds_att_MoveShift_v  a where 1=1";
            cmdText = cmdText + strCon;
            if (!string.IsNullOrEmpty(depName))
            {
                cmdText += "AND a.depcode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                cmdText += " AND a.depcode IN (" + sqlDep + ") ";
            }
           
            if (!string.IsNullOrEmpty(workDate1) && !string.IsNullOrEmpty(workDate2))
            {
                cmdText += " and (WORKDATE between to_date(:workDate1,'yyyy/mm/dd') and to_date(:workDate2,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":workDate1", workDate1));
                listPara.Add(new OracleParameter(":workDate2", workDate2));
            }

            if (!string.IsNullOrEmpty(noWorkDate1) && !string.IsNullOrEmpty(noWorkDate2))
            {
                cmdText += " and (NOWORKDATE between to_date(:noWorkDate1,'yyyy/mm/dd') and to_date(:noWorkDate2,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":noWorkDate1", noWorkDate1));
                listPara.Add(new OracleParameter(":noWorkDate2", noWorkDate2));
            }

            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;

        }

        /// <summary>
        /// 根據工號和調班日期獲取調班信息的LIST
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public List<MoveShiftModel> GetMoveShiftList(string workNo, string workDate)
        {
            List<MoveShiftModel> list = new List<MoveShiftModel>();
            List<OracleParameter> listPara = new List<OracleParameter>();
            DataTable dt = new DataTable();
            string cmdText = "select * from gds_att_moveshift  where workno=upper(:workNo) and to_char(workdate,'yyyy/mm/dd')=:workDate";
            listPara.Add(new OracleParameter(":workDate", workDate));
            listPara.Add(new OracleParameter(":workNo", workNo));
            dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dt);
        }


        /// <summary>
        /// 根據員工工號和日期判斷獲取當天的加班信息
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTDate"></param>
        /// <returns></returns>
        public DataTable GetOTInfo(string workNo, string oTDate)
        {
            List<OracleParameter> listPara = new List<OracleParameter>();
            string cmdText = @"  select workno,ottype,otdate,begintime,endtime from         
                    (select workno,ottype,otdate,to_char(begintime,'hh24:mi') begintime,to_char(endtime,'hh24:mi') endtime from  gds_att_advanceapply where (workno,ottype,otdate,begintime,endtime)
                     NOT IN (SELECT workno,ottype,otdate,begintime,endtime FROM gds_att_realapply  )
                      UNION ( SELECT workno,ottype,otdate,to_char(NVL(ONDUTYTIME,begintime),'hh24:mi') begintime,to_char(NVL(OFFDUTYTIME,ENDTIME),'hh24:mi')  endtime FROM gds_att_realapply)
                      UNION (select workno,ottype,otdate,starttime begintime,endtime from gds_att_activity))
                     WHERE workno = upper(:workNo) AND otdate = TO_DATE (:oTDate, 'yyyy/mm/dd') ";
            listPara.Add(new OracleParameter(":workNo", workNo));
            listPara.Add(new OracleParameter(":oTDate", oTDate));
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 判斷某一時間區間是否屬於另一時間區間
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="begintimeflag"></param>
        /// <param name="endtimeflag"></param>
        /// <returns></returns>
        public string GetTimeSpanFlag(string begintime, string endtime, string begintimeflag, string endtimeflag)
        {
            OracleParameter outString = new OracleParameter("p_result", OracleType.VarChar,100);
            outString.Direction = ParameterDirection.Output;
            DalHelper.ExecuteQuery("gds_att_between_timespan", CommandType.StoredProcedure,
                new OracleParameter("p_begintime", begintime),
                new OracleParameter("p_endtime",endtime),
                new OracleParameter("p_begintimeflag", begintimeflag),
                new OracleParameter("p_endtimeflag", endtimeflag),
                outString
                );
            return outString.Value.ToString();
        }

    }
}
