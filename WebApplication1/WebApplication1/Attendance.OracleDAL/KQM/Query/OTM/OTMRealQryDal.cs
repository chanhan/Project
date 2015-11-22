/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQtyDal.cs
 * 檔功能描述： 加班實報查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query;


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query.OTM
{
    public class OTMRealQryDal : DALBase<OTMRealApplyQryModel>, IOTMRealQryDal
    {

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA ";
            if (condition == "condition1")
            {
                cmdText += " WHERE DataType='OTMAdvanceApplyStatus' ORDER BY OrderId ";
            }
            if (condition == "condition2")
            {
                cmdText += " WHERE DataType='OverTimeType' ORDER BY OrderId ";
            }


            dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }


        /// <summary>
        /// 加班實報查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMRealQryList(OTMRealApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_otmrealapply_v a where 1=1 and  a.RealType='A' ";
            cmdText += strCon;

            if (!String.IsNullOrEmpty(dCode))
            {
                cmdText += " AND a.dCode   IN  ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depCode CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depCode", dCode));
            }
            else
            {
                cmdText += " AND a.dcode in (" + sqlDep + ")";
            }   


            if (!String.IsNullOrEmpty(batchEmployeeNo))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + batchEmployeeNo + "','§')))";
            }

            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.OTDATE between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }

            if (!String.IsNullOrEmpty(hoursCondition) && !String.IsNullOrEmpty(hours))
            {
                cmdText += " and  a.Hours " + hoursCondition + " :hours ";
                listPara.Add(new OracleParameter(":hours", hours));
            }

            if (!String.IsNullOrEmpty(model.IsProject))
            {
                if (model.IsProject.Equals("Y"))
                {
                    cmdText += " AND a.diffreason='D' ";
                }
                else
                {
                    cmdText += " AND a.diffreason is null ";
                }
                model.IsProject = null;
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }


        /// <summary>
        /// 獲取卡鐘信息
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="KQDate"></param>
        /// <param name="ShiftNo"></param>
        /// <returns></returns>
        public DataTable GetBellCardData(string WorkNo, string KQDate, string ShiftNo)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT b.workno, b.localname,TO_CHAR (a.cardtime, 'yyyy/mm/dd hh24:mi:ss') AS cardtime, a.bellno,        TO_CHAR (a.readtime, 'yyyy/mm/dd hh24:mi:ss') AS readtime, a.cardno   FROM gds_att_bellcarddata a,gds_att_employee b where (b.CardNo=a.CardNo or b.workno=a.workno) ";
            if (ShiftNo.StartsWith("A") || ShiftNo.StartsWith("B"))
            {
                string KQDate1 = Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd");
                cmdText += " and b.WorkNo=:WorkNo and a.cardtime>=to_date(:KQDate,'yyyy/mm/dd') and a.cardtime<to_date(:KQDate1,'yyyy/mm/dd')  ";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":WorkNo", WorkNo), new OracleParameter(":KQDate", KQDate), new OracleParameter(":KQDate1", KQDate1));
            }
            else
            {
                KQDate += " 12:00:00";
                string KQDate1 = Convert.ToDateTime(KQDate).AddDays(1.0).ToString("yyyy/MM/dd") + " 12:00:00";
                cmdText += " and b.WorkNo=:WorkNo and a.cardtime>=to_date(:KQDate,'yyyy/mm/dd hh:mi:ss') and a.cardtime<=to_date(:KQDate1,'yyyy/mm/dd hh:mi:ss')  ";
                dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":WorkNo", WorkNo), new OracleParameter(":KQDate", KQDate), new OracleParameter(":KQDate1", KQDate1));
            }
            return dt;
        }



        /// <summary>
        /// 根據SQL語句查詢
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTdate"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string workNo, string oTdate)
        {
            DataTable dt = new DataTable();
            string cmdText = @"select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,
                        nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,
                        nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total 
                        from gds_att_realApply where WorkNo=:workNo 
                        and otdate>last_day(add_months(to_date(:oTdate,'yyyy/MM/dd'),-1)) 
                        and OTDate<=to_date(:oTdate,'yyyy/MM/dd') and status<'3' ";
            dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":workNo", workNo), new OracleParameter(":oTdate", oTdate));
            return dt;
        }


        /// <summary>
        /// 加班預報查詢(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public DataTable GetOTMRealQryList(OTMRealApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_otmrealapply_v a where 1=1 and  a.RealType='A' ";
            cmdText += strCon;

            if (!String.IsNullOrEmpty(dCode))
            {
                cmdText += " AND a.dCode   IN  ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depCode CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depCode", dCode));
            }
            else
            {
                cmdText += " AND a.dcode in (" + sqlDep + ")";
            }   


            if (!String.IsNullOrEmpty(batchEmployeeNo))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + batchEmployeeNo + "','§')))";
            }

            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.OTDATE between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }

            if (!String.IsNullOrEmpty(hoursCondition) && !String.IsNullOrEmpty(hours))
            {
                cmdText += " and  a.Hours " + hoursCondition + " :hours ";
                listPara.Add(new OracleParameter(":hours", hours));
            }

            if (!String.IsNullOrEmpty(model.IsProject))
            {
                if (model.IsProject.Equals("Y"))
                {
                    cmdText += " AND a.diffreason='D' ";
                }
                else
                {
                    cmdText += " AND a.diffreason is null ";
                }
                model.IsProject = null;
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;

        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<OTMRealApplyQryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<BellCardDataModel> GetBellCardList(DataTable dt)
        {

            BellCardDataModel model;
            List<BellCardDataModel> list = new List<BellCardDataModel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new BellCardDataModel();
                    model.WorkNo = dt.Rows[i]["workno"].ToString().Trim();
                    model.LocalName = dt.Rows[i]["LocalName"].ToString().Trim();
                    if (dt.Rows[i]["CardTime"].ToString().Trim().Length > 0)
                    {
                        model.CardTime = DateTime.Parse(dt.Rows[i]["CardTime"].ToString().Trim());
                    }
                    model.BellNo = dt.Rows[i]["BellNo"].ToString().Trim();
                    if (dt.Rows[i]["ReadTime"].ToString().Trim().Length > 0)
                    {
                        model.ReadTime = DateTime.Parse(dt.Rows[i]["ReadTime"].ToString().Trim());
                    }
                    model.CardNo = dt.Rows[i]["CardNo"].ToString().Trim();
                    list.Add(model);

                }

            }
            return list;


        }

    }
}
