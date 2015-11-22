/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmParameterEditDal.cs
 * 檔功能描述： 考勤參數設定(單位)數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.13
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class KqmParameterEmpEditDal : DALBase<AttKQParamsEmpEditModel>, IKqmParameterEmpEditDal
    {
        /// <summary>
        /// 查詢人員考勤參數信息
        /// </summary>
        /// <param name="model">要查詢的model</param>
        /// <returns>返回的datatable</returns>
        public DataTable GetKQMParamsEmpData(AttKQParamsEmpEditModel model)
        {
            return DalHelper.Select(model, null);
        }


        /// <summary>
        /// 查詢員工信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetVDataByCondition(string employeeNo, string sqlDep)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT * FROM (
SELECT a.workno, a.localname, a.marrystate, a.dname, a.levelcode, a.managercode, a.identityno, a.notes, a.flag,
       (SELECT datavalue  FROM gds_att_typedata c  WHERE c.datatype = 'Sex' AND c.datacode = a.sex) AS sex, a.sex sexcode, a.technicalname, a.levelname, a.managername,
       (SELECT technicaltypename FROM gds_att_technical b, gds_att_technicaltype c WHERE c.technicaltypecode = b.technicaltype AND b.technicalcode = a.technicalcode) AS technicaltype,
       (SELECT costcode FROM gds_sc_department b WHERE b.depcode = a.depcode) costcode, a.technicalcode, a.depcode, a.dcode, a.dname depname, a.depname sybname,
       getdepname ('2', a.depcode) syc, getdepname ('1', a.depcode) bgname, getdepname ('0', a.depcode) cbgname,
       (SELECT professionalname FROM gds_Att_professional n  WHERE n.professionalcode = a.professionalcode) AS professionalname,
        ROUND (  (MONTHS_BETWEEN (SYSDATE, a.joindate) - NVL (a.deductyears, 0) ) / 12, 1 ) AS comeyears,
       (SELECT (SELECT datavalue FROM gds_att_typedata b WHERE b.datatype = 'AssessLevel' AND e.asseslevel = b.datacode) FROM gds_att_empassess e
         WHERE e.workno = a.workno  AND e.assesdate = (SELECT MAX (assesdate) FROM gds_att_empassess w WHERE w.workno = e.workno) AND ROWNUM <= 1) AS asseslevel,
       (SELECT leveltype FROM gds_att_level j WHERE j.levelcode = a.levelcode) AS leveltype, TO_CHAR (a.joindate, 'yyyy/mm/dd') AS joindate, a.overtimetype,
       (SELECT datavalue FROM gds_att_typedata t WHERE t.datatype = 'OverTimeType' AND t.datacode = a.overtimetype) AS overtimetypename FROM gds_att_employee a ";

            cmdText += " WHERE a.WorkNO=:employeeNo ";
            cmdText += " AND exists (SELECT 1 FROM (" + sqlDep + ") e where e.DepCode=a.DCode) ) ";
            dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":employeeNo", employeeNo));
            return dt;
        }



        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool AddKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的考勤參數Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateKQMParamsEmpByKey(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model,logmodel) != -1;
        }

        /// <summary>
        /// 刪除考勤參數
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteKQMParamsEmpData(AttKQParamsEmpEditModel model,SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }



    }
}
