/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkShiftDal.cs
 * 檔功能描述： 班別定義維護數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */

using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class WorkShiftDal : DALBase<WorkShiftModel>, IWorkShiftDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetWorkShiftList(WorkShiftModel model, string deptCode, string orderType, string SQLDep,string effectDate,string expireDate, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT shiftno, shiftdesc, timeqty, ondutytime, offdutytime, amreststime,amrestetime, ondutytime1, 
                    offdutytime1, pmreststime, pmrestetime,pmrestqty, otondutytime, otoffdutytime, orgcode, effectdate,
                    expiredate, remark, create_user, create_date, update_date, update_user,shifttype, islactation, shareorgcode, shiftdetail, shifttypename,
                    orgname, shareorgname FROM gds_att_workshift_v a where 1=1 ";
            cmdText = cmdText + strCon;
            cmdText = cmdText + " AND orgcode IN(" + SQLDep + ")";
            if (!string.IsNullOrEmpty(deptCode))
            {
                cmdText = cmdText + " AND orgcode IN (SELECT depcode FROM gds_sc_department START WITH DEPCODE =:deptCode CONNECT BY PRIOR depcode = parentdepcode)";
            }
            if (!string.IsNullOrEmpty(effectDate))
            {
                cmdText = cmdText + " AND a.effectdate >= to_date('" + DateTime.Parse(effectDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(expireDate))
            {
                cmdText = cmdText + " AND a.expiredate <= to_date('" + DateTime.Parse(expireDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            //if (orderType == "1")
            //{
                cmdText = cmdText + "  order by shiftno";
            //}
            //else
            //{
            //    cmdText = cmdText + "order by update_date desc";
            //}
            listPara.Add(new OracleParameter(":deptCode", deptCode));
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="deptCode">部門代碼</param>
        /// <param name="orderType">排序</param>
        /// <returns></returns>
        public DataTable GetWorkShiftList(WorkShiftModel model, string deptCode, string SQLDep,string effectDate,string expireDate)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT shiftno, shiftdesc, timeqty, ondutytime, offdutytime, amreststime,amrestetime, ondutytime1, 
                    offdutytime1, pmreststime, pmrestetime,pmrestqty, otondutytime, otoffdutytime, orgcode, effectdate,
                    expiredate, remark, create_user, create_date, update_date, update_user,shifttype, islactation, shareorgcode, shiftdetail, shifttypename,
                    orgname, shareorgname FROM gds_att_workshift_v a where 1=1 ";
            cmdText = cmdText + strCon;
            cmdText = cmdText + " AND orgcode IN(" + SQLDep + ")";
            if (!string.IsNullOrEmpty(effectDate))
            {
                cmdText = cmdText + " AND a.effectdate >= to_date('" + DateTime.Parse(effectDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') or ";
            }
            if (!string.IsNullOrEmpty(expireDate))
            {
                cmdText = cmdText + " AND a.expiredate <= to_date('" + DateTime.Parse(expireDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(deptCode))
            {
                cmdText = cmdText + " AND orgcode IN (SELECT depcode FROM gds_sc_department START WITH DEPCODE =:deptCode CONNECT BY PRIOR depcode = parentdepcode)";
            }
            listPara.Add(new OracleParameter(":deptCode", deptCode));
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        ///查詢全部的記錄
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetWorkShiftListAll()
        {
            return DalHelper.SelectAll();
        }
        /// <summary>
        /// 刪除一個功能及子功能
        /// </summary>
        /// <param name="functionId">要刪除的功能Id</param>
        /// <returns>刪除功能條數</returns>
        public int DeleteShiftByKey(WorkShiftModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

        /// <summary>
        ///查詢部門層級進行管控
        /// </summary>
        /// <param name="model">部門層級代碼</param>
        /// <returns>部門層級數</returns>
        public DataTable GetDepLevel(string personCode)
        {
            DataTable dt = DalHelper.ExecuteQuery("select deplevel from gds_sc_person where personcode=:personCode", new OracleParameter(":personCode", personCode));
            return dt;
        }
        /// <summary>
        ///查詢失效日期進行管控
        /// </summary>
        /// <param name="shiftNo">班別編號</param>
        /// <returns>班別的有效日期數</returns>
        public DataTable GetTypeDay(string shiftNo)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT COUNT (1) v_num
                           FROM gds_att_orgshift WHERE shiftno = :shiftNo
                           AND startdate <= TRUNC (SYSDATE) AND enddate >= TRUNC (SYSDATE)", new OracleParameter(":shiftNo", shiftNo));
            return dt;
        }
        /// <summary>
        ///查詢班別是不是已在使用中
        /// </summary>
        /// <param name="shiftNo">班別編號</param>
        /// <returns>辨別是不是在使用中</returns>
        public DataTable GetTypeShift(string shiftNo)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT COUNT (*) tmp FROM gds_att_employeeshift
                                        WHERE shiftno = :shiftno  UNION SELECT COUNT (*) tmp FROM gds_att_orgshift 
                                        WHERE shiftno = :shiftno", new OracleParameter(":shiftNo", shiftNo));
            return dt;
        }
        /// <summary>
        /// 插入一條新的班別定義記錄
        /// </summary>
        /// <param name="functionId">要插入的班別定義組成的model</param>
        /// <returns>插入是否成功</returns>
        public int InsertShiftByKey(WorkShiftModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel);
        }
        /// <summary>
        /// 查詢表中最大的班別
        /// </summary>
        /// <param name="functionId">辨別類型</param>
        /// <returns></returns>
        public string SelectMaxShiftNo(string shiftType)
        {
            return DalHelper.ExecuteScalar("select MAX (ShiftNo) from GDS_ATT_WorkShift where ShiftNo like '" + shiftType+ "%'").ToString();
        }
        /// <summary>
        /// 更新一條班別定義記錄
        /// </summary>
        /// <param name="functionId">要更新別定義組成的model</param>
        /// <returns>更新是否成功</returns>
        public int UpdateShiftByKey(WorkShiftModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model,logmodel);
        }
        /// <summary>
        /// 將datatable轉換成list
        /// </summary>
        /// <param name="dt">需要轉換的DataTable</param>
        /// <returns>modelList</returns>
        public List<WorkShiftModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 獲取派別集
        /// </summary>
        /// <param name="model">排班Model</param>
        /// <returns>班別Model集</returns>
        public List<WorkShiftModel> GetShiftType(WorkShiftModel model)
        {
            DataTable dtbl = DalHelper.Select(model, null);
            return OrmHelper.SetDataTableToList(dtbl);
        }
    }
}
