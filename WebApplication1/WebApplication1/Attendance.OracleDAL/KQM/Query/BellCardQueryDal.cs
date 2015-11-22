/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BellCardQueryDal.cs
 * 檔功能描述： 刷卡原始數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.26
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class BellCardQueryDal : DALBase<BellCardDataModel>, IBellCardQueryDal
    {
        /// <summary>
        /// 獲得卡鐘數據
        /// </summary>
        /// <returns></returns>
        public DataTable GetBellNo()
        {
            string str = "SELECT * FROM (SELECT a.*,(SELECT datavalue FROM gds_att_typedata WHERE datatype = 'BellType' AND datacode = a.belltype) belltypename FROM gds_att_bellcard a) WHERE belltype = 'KQM' ORDER BY bellno";
            return DalHelper.ExecuteQuery(str);
        }
        /// <summary>
        /// 查詢刷卡的詳細信息（在重新計算的頁面使用）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BellCardDataModel> GetBellDataReList(BellCardDataModel model, string KQdate, string shiftNo)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT b.workno, b.localname,TO_CHAR (a.cardtime, 'yyyy/mm/dd hh24:mi:ss') AS cardtime, a.bellno,
                             TO_CHAR (a.readtime, 'yyyy/mm/dd hh24:mi:ss') AS readtime, a.cardno FROM gds_att_bellcarddata a, gds_att_employee b
                             WHERE (b.cardno = a.cardno OR b.workno = a.workno)";
            cmdText = cmdText + strCon;
            if ((shiftNo.IndexOf("A") >= 0) || (shiftNo.IndexOf("B") >= 0))
            {
                cmdText = cmdText + "and a.cardtime>=to_date('" + KQdate + "','yyyy/mm/dd') and a.cardtime<to_date('" + Convert.ToDateTime(KQdate).AddDays(1.0).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            else
            {
                cmdText = cmdText + " and a.cardtime>=to_date('" + KQdate + " 12:00:00','yyyy/mm/dd hh:mi:ss') and a.cardtime<=to_date('" + Convert.ToDateTime(KQdate).AddDays(1.0).ToString("yyyy/MM/dd") + " 12:00:00','yyyy/mm/dd hh:mi:ss')";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return OrmHelper.SetDataTableToList(dt);
        }



        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetBellCardData(BellCardDataModel model, string sql, bool ischecked, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "order by workno,cardtime desc";
            string cmdText = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            if (ischecked == true)
            {
                cmdText = @"select workno,localname, depname,cardtime,cardtimemm,cardno,bellno,address from gds_att_bellcarddata_his_v a where 1=1 ";
            }
            else
            {
                cmdText = @"select workno,localname, depname,cardtime,cardtimemm,cardno,bellno,address from gds_att_bellcarddata_v  a where 1=1 ";
            }
            cmdText = cmdText + strCon;
            if (FromDate != DateTime.MinValue)
            {
                cmdText = cmdText + " and to_char(cardtime,'yyyy/mm/dd')>= to_char(:FromDate,'yyyy/mm/dd')";
                listPara.Add(new OracleParameter(":FromDate", FromDate));
            }
            if (ToDate != DateTime.MaxValue)
            {
                cmdText = cmdText + " and to_char(cardtime,'yyyy/mm/dd')<= to_char(:ToDate,'yyyy/mm/dd')";
                listPara.Add(new OracleParameter(":ToDate", ToDate));
            }
            string condition = " and a.depcode in (" + sql + ")";
            cmdText = cmdText + condition;
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="ischecked"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public DataTable GetBellCardDataForExport(BellCardDataModel model, string sql, bool ischecked, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate)
        {
            string strCon = "order by workno,cardtime desc";
            string cmdText = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            if (ischecked == true)
            {
                cmdText = @"select workno,localname, depname,cardtime,cardtimemm,cardno,bellno,address from gds_att_bellcarddata_his_v a where 1=1 ";
            }
            else
            {
                cmdText = @"select workno,localname, depname,cardtime,cardtimemm,cardno,bellno,address from gds_att_bellcarddata_v  a where 1=1 ";
            }
            cmdText = cmdText + strCon;
            if (FromDate != DateTime.MinValue)
            {
                cmdText = cmdText + " and to_char(cardtime,'yyyy/mm/dd')>= to_char(:FromDate,'yyyy/mm/dd')";
                listPara.Add(new OracleParameter(":FromDate", FromDate));
            }
            if (ToDate != DateTime.MaxValue)
            {
                cmdText = cmdText + " and to_char(cardtime,'yyyy/mm/dd')<= to_char(:ToDate,'yyyy/mm/dd')";
                listPara.Add(new OracleParameter(":ToDate", ToDate));
            }
            string condition = " and a.depcode in (" + sql + ")";
            cmdText = cmdText + condition;
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }


        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<BellCardDataModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
