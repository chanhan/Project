/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SynclogDal.cs
 * 檔功能描述： 組織層級設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 何偉 2011.12.06
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data.OracleClient;
namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class WFSignLogAndMap : DALBase<TypeDataModel>,IWFSignLogAndMap
    {   
        /// <summary>
        /// 獲取簽核流程圖
        /// </summary>
        /// <param name="doc">單號</param>
        /// <returns></returns>
        public DataTable GetFlowMap(string doc)
        {
            string cmdText = @"SELECT '開始' level_name, applyman || '(' || localname || ')' verify_name
                          FROM gds_att_bill, gds_att_employee
                         WHERE applyman = workno AND billno =:doc
                        UNION ALL
                        SELECT *
                          FROM (SELECT   managername level_name,
                                         auditman || '(' || localname || ')' verify_name
                                    FROM gds_att_auditstatus, gds_att_employee
                                   WHERE auditman = workno AND billno = :doc
                    ORDER BY orderno ASC)   union all select '結束' level_name,'' verify_name from dual";
          
         
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":doc", doc));
            return dt;
        }
        /// <summary>
        /// 獲取簽核Log
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public DataTable GetFlowLog(string doc)
        {
//            string cmdText = @"SELECT applyman || '(' || localname || ')' deal_person, applydate start_time,
//                                   applydate end_time, '是' ismail, '已簽核' status
//                              FROM gds_att_bill, gds_att_employee
//                                   where applyman = workno AND billno =:doc
//                             union all        
//                            SELECT auditman || '(' || localname || ')' deal_person, applydate start_time,
//                                   auditdate end_time, DECODE (sendnotes, 'N', '否', '是') ismail,
//                                   DECODE (auditstatus, 0, '未簽核', '已簽核') status
//                              FROM gds_att_auditstatus a, gds_att_employee b, gds_att_bill c
//                             WHERE auditman = workno
//                               AND a.billno = :doc
//                               AND a.billno = c.billno
//                               AND orderno = 0
//                             union all
//                            SELECT deal_person,start_time,end_time,ismail,status
//                              FROM (SELECT   auditman || '(' || localname || ')' deal_person,
//                                             LAG (auditdate) OVER (ORDER BY orderno) start_time,
//                                             auditdate end_time,
//                                             DECODE (sendnotes, 'N', '否', '是') ismail,
//                                             DECODE (auditstatus, 0, '未簽核', '已簽核') status  
//                                        FROM gds_att_auditstatus a, gds_att_employee
//                                       WHERE auditman = workno AND a.billno = :doc 
//                                      AND orderno > 0 
//                                    ORDER BY orderno ASC) ";
            string cmdText
                = @" SELECT applyman || '(' || localname || ')' deal_person,
                               applydate start_time,
                               applydate end_time,
                               '是' ismail,
                               remark,
                               '已簽核' status
                          FROM gds_att_bill, gds_att_employee
                         WHERE applyman = workno AND billno = :doc
                        UNION ALL
                        SELECT auditman || '(' || localname || ')' deal_person,
                               applydate start_time,
                               auditdate end_time,
                               DECODE (sendnotes,
                                       'N', '否',
                                       '是'
                                      ) ismail,
                               a.remark,
                               DECODE (auditstatus,
                                       0, '未簽核',
                                       '已簽核'
                                      ) status
                          FROM gds_att_auditstatus a, gds_att_employee b, gds_att_bill c
                         WHERE auditman = workno
                           AND a.billno = :doc
                           AND a.billno = c.billno
                           AND orderno = 0
                        UNION ALL
                        SELECT deal_person,
                               start_time,
                               end_time,
                               ismail,
                               remark,
                               status
                          FROM (SELECT   auditman || '(' || localname || ')' deal_person,
                                         LAG (auditdate) OVER (ORDER BY orderno) start_time,
                                         auditdate end_time,
                                         DECODE (sendnotes,
                                                 'N', '否',
                                                 '是'
                                                ) ismail,
                                         a.remark,
                                         DECODE (auditstatus,
                                                 0, '未簽核',
                                                 '已簽核'
                                                ) status
                                    FROM gds_att_auditstatus a, gds_att_employee
                                   WHERE auditman = workno AND a.billno = :doc AND orderno > 0
                                ORDER BY orderno ASC)";


            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":doc", doc));
            return dt;
        }
    }
}
