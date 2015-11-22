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
    public class WFMManagerLeaveData : DALBase<TypeDataModel>, IWFMManagerLeaveData
    {   
        /// <summary>
        /// 根據查詢條件查相應信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            string cmdText = @"SELECT * FROM (Select a.*, b.DName,b.LocalName,b.Notes,b.Flag,  
            (a.EndDate-a.StartDate)+1 DeputyDays,   (Select LocalName From gds_att_employee f Where f.WorkNo=a.DeputyWorkNo) DeputyName,
            (Select Notes From gds_att_employee e Where e.WorkNo=a.DeputyWorkNo) DeputyNotes, (Select Flag From gds_att_employee e Where e.WorkNo=a.DeputyWorkNo) DeputyFlag,
            (select DataValue from GDS_ATT_TYPEDATA c where c.DataType='WFMLeaveType' and c.DataCode=a.LeaveType) LeaveTypeName  from GDS_WFM_MANAGERLEAVE a ,
            gds_att_employee b   Where a.WorkNo=b.WorkNo(+) " + condition + ")  order by WorkNo, StartDate desc";
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount);
            return dt;

        }
        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteData(string id)
        {
            string cmdText = @"DELETE FROM GDS_WFM_MANAGERLEAVE WHERE ID=:id";
            return DalHelper.ExecuteNonQuery(cmdText, new OracleParameter(":id", id)) >= 1;
        }
        /// <summary>
        /// 查詢代理原因
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDeputType(string condition)
        {
            string cmdText = @"SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue 
                             FROM gds_att_typedata " + condition;
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
        /// <summary>
        /// 根據查詢條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            string cmdText = @"SELECT * FROM (Select a.*, b.DName,b.LocalName,b.Notes,b.Flag,  
            (a.EndDate-a.StartDate)+1 DeputyDays,   (Select LocalName From gds_att_employee f Where f.WorkNo=a.DeputyWorkNo) DeputyName,
            (Select Notes From gds_att_employee e Where e.WorkNo=a.DeputyWorkNo) DeputyNotes, (Select Flag From gds_att_employee e Where e.WorkNo=a.DeputyWorkNo) DeputyFlag,
            (select DataValue from GDS_ATT_TYPEDATA c where c.DataType='WFMLeaveType' and c.DataCode=a.LeaveType) LeaveTypeName,gds_get_dept_name (a.deptcode) dept_name  from GDS_WFM_MANAGERLEAVE a ,
            gds_att_employee b   Where a.WorkNo=b.WorkNo(+) " + condition + ")  order by WorkNo, StartDate desc";
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
        /// <summary>
        /// 判斷名字是否存在
        /// </summary>
        /// <param name="emp_name"></param>
        /// <returns></returns>
        public bool CheckEmpName(string emp_name)
        {
            string cmdText = @"SELECT workno, localname FROM gds_att_employee WHERE localname = :emp_name";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":emp_name", emp_name));
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根據工號獲取基本信息
        /// </summary>
        /// <param name="emp_no"></param>
        /// <returns></returns>
        public DataTable GetEmpInfo(string emp_no)
        {
            string cmdText = @"SELECT *
                          FROM (SELECT a.workno, a.localname, a.sex, a.marrystate, a.managercode,
                                       a.notes, a.depcode, a.flag, a.dname,
                                       (SELECT depname
                                          FROM gds_sc_department b
                                         WHERE b.depcode = a.dcode) depname
                                  FROM gds_att_employee a
                                 WHERE a.workno = :emp_no)";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":emp_no", emp_no));
            return dt;
        }
        /// <summary>
        /// 獲取本年度歷史代理記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeputyRecord(string emp_no)
        {
            string cmdText = @"SELECT ID,
                      TO_CHAR (startdate, 'yyyy/mm/dd')|| '-'|| TO_CHAR (enddate, 'yyyy/mm/dd')||remark text  FROM gds_wfm_managerleave
                      WHERE TO_CHAR (modifydate, 'yyyy') = TO_CHAR (SYSDATE, 'yyyy')  and (WORKNO=:emp_no or MODIFIER=:emp_no)";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":emp_no", emp_no));
            return dt;
        }
        /// <summary>
        /// 保存代理信息
        /// </summary>
        /// <param name="prcessFlag">add新增 modify 修改</param>
        /// <param name="dt"></param>
        /// <param name="user">修改人</param>
        /// <returns></returns>
        public bool SaveData(string prcessFlag, DataTable dt,string user)
        {
            if (dt != null && dt.Rows.Count > 0)
            {   
                string cmdText = "begin";
                if (dt.Rows[0]["DeputyFlag"].ToString() == "Y")
                {
                    cmdText += " update gds_att_empsupportout set  NOTES='" + dt.Rows[0]["DeputyNotes"].ToString() + "' ,UPDATE_USER='" + user + "', UPDATE_DATE=sysdate where WORKNO='" + dt.Rows[0]["DeputyWorkNo"].ToString() + "';";
                    cmdText += " update gds_att_employees set notes = '" + dt.Rows[0]["DeputyNotes"].ToString() + "' where WORKNO='" + dt.Rows[0]["DeputyWorkNo"].ToString() + "'; ";
                    cmdText += " update gds_att_twcadre set  NOTES='" + dt.Rows[0]["DeputyNotes"].ToString() + "' ,UPDATE_USER='" + user + "', UPDATE_DATE=sysdate where WORKNO='" + dt.Rows[0]["DeputyWorkNo"].ToString() + "';";
                }
                if (prcessFlag.ToLower() == "add")//新增
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmdText += " insert into gds_WFM_ManagerLeave(WORKNO,STARTDATE,ENDDATE,DEPUTYWORKNO,LEAVETYPE,REMARK,MODIFIER,MODIFYDATE,DEPTCODE) values('" + dt.Rows[i]["WorkNo"].ToString() + "',to_date('" + dt.Rows[i]["StartDate"].ToString() + "','yyyy/mm/dd'),to_date('" + dt.Rows[i]["EndDate"].ToString() + "','yyyy/mm/dd'),'" + dt.Rows[i]["DeputyWorkNo"].ToString() + "','" + dt.Rows[i]["LeaveType"].ToString() + "','" + dt.Rows[i]["Remark"].ToString() + "','" + user + "',sysdate,'" + dt.Rows[i]["DeptCode"].ToString() + "');";
                    }

                }
                else//修改
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {   
                        string id=dt.Rows[i]["id"].ToString();
                        string deptcode=dt.Rows[i]["DeptCode"].ToString();
                        if (GetExist(id, deptcode))//存在更新
                        {
                            cmdText += " update gds_WFM_ManagerLeave set startdate=to_date('" + dt.Rows[i]["StartDate"].ToString() + "','yyyy/mm/dd'),enddate=to_date('" + dt.Rows[i]["EndDate"].ToString() + "','yyyy/mm/dd'),deputyworkno='" + dt.Rows[i]["DeputyWorkNo"].ToString() + "',leavetype='" + dt.Rows[i]["LeaveType"].ToString() + "',remark='" + dt.Rows[i]["Remark"].ToString() + "',MODIFIER='" + user + "',MODIFYDATE=sysdate where id='" + id + "' and deptcode='" + deptcode + "';";
                        }
                        else//不存在插入
                        {
                            cmdText += " insert into gds_WFM_ManagerLeave(WORKNO,STARTDATE,ENDDATE,DEPUTYWORKNO,LEAVETYPE,REMARK,MODIFIER,MODIFYDATE,DEPTCODE) values('" + dt.Rows[i]["WorkNo"].ToString() + "',to_date('" + dt.Rows[i]["StartDate"].ToString() + "','yyyy/mm/dd'),to_date('" + dt.Rows[i]["EndDate"].ToString() + "','yyyy/mm/dd'),'" + dt.Rows[i]["DeputyWorkNo"].ToString() + "','" + dt.Rows[i]["LeaveType"].ToString() + "','" + dt.Rows[i]["Remark"].ToString() + "','" + user + "',sysdate,'" + dt.Rows[i]["DeptCode"].ToString() + "');";
                        }
                    }
                }
                cmdText += " end;";
                return DalHelper.ExecuteNonQuery(cmdText)>=1;
            }
            return false;
        }
        //獲取代理是否存在
        public bool GetExist(string id, string deptcode)
        {
            string cmdText = "select * from gds_WFM_ManagerLeave where id=:id and deptcode=:deptcode";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":id", id), new OracleParameter(":deptcode", deptcode));
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
