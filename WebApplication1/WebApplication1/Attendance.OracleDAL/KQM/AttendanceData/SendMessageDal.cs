/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SendMessageDal.cs
 * 檔功能描述： 短信息提醒數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.03
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.AttendanceData
{
    public class SendMessageDal : DALBase<SendMessageModel>, ISendMessageDal
    {
        /// <summary>
        /// 按工號發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public int GetWorkNoData(string workNo)
        {
            DataTable dt = new DataTable();
            dt = DalHelper.ExecuteQuery("select count(WorkNO) num from gds_att_Employee where WorkNO='" + workNo + "' and status='0'");
            return Convert.ToInt32(dt.Rows[0]["num"].ToString());
        }
        /// <summary>
        /// 插入到表格中準備發送短信
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="RemindContent">短信內容</param>
        /// <returns></returns>
        public int GetSendMsgByWork(string workNo, string RemindContent,SynclogModel logmodel)
        {
            return DalHelper.ExecuteNonQuery("INSERT INTO gds_att_REMIND (WORKNO, REMINDCONTENT, REMINDDATE,FLAG)VALUES('" + workNo + "','" + RemindContent + "',sysdate,'N')",logmodel);
        }
        /// <summary>
        /// 按照選擇的方式發送短信
        /// </summary>
        /// <param name="RemindContent">短信內容</param>
        /// <param name="sendTo">發送方式</param>
        /// <returns></returns>
        public int GetSendMsgByStyle(string RemindContent, string sendTo, SynclogModel logmodel)
        {
            string condition = "";
            if (!(sendTo == "A"))
            {
                if (sendTo == "B")
                {
                    condition = "select personcode,'" + RemindContent + "',sysdate,'N'from gds_sc_person a ,gds_att_employee b where a.personcode=b.workno and a.deleted='N' and b.status='0' and b.flag='Local' ";
                    goto Label_0264;
                }
                if (sendTo == "C")
                {
                    condition = "select personcode,'" + RemindContent + "',sysdate,'N'from gds_sc_person a ,gds_att_employee b where a.personcode=b.workno and a.deleted='N' and b.status='0'";
                    goto Label_0264;
                }
                if (sendTo == "D")
                {
                    condition = "select distinct a.personcode,'" + RemindContent + "',sysdate,'N'from gds_sc_person a ,gds_sc_person_login b where  a.deleted='N'  and a.personcode like 'F%' and a.personcode= b.personcode and b.logintime>sysdate-0.03";
                    goto Label_0264;
                }
                if (sendTo == "E")
                {
                    condition = "select distinct loginuser,'" + RemindContent + "',sysdate,'N'from gds_sc_person_login  where personcode like 'F%' AND logintime>sysdate-0.03";
                    goto Label_0264;
                }
            }
            else
            {
                condition = "select personcode,'" + RemindContent + "',sysdate,'N'from gds_sc_person a ,gds_att_employee b where a.personcode=b.workno and a.deleted='N' and b.status='0' and b.flag='Local' and b.managercode='0003' ";
                goto Label_0264;
            }
        Label_0264:
            if (condition.Length > 0)
            {
            return DalHelper.ExecuteNonQuery("insert into gds_att_remind (workno, remindcontent, reminddate,flag)" + condition,logmodel);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根據工號查詢用戶信息
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public DataTable GetBasic(string workNo)
        {
            return DalHelper.ExecuteQuery("select a.workno, a.dname,a.tel,a.localname, b.remindcontent from gds_att_employee a ,gds_att_remind b where a.workno=b.workno and  a.workno='" + workNo + "'  and status='0'");
        }
        /// <summary>
        /// 按照選擇的方式查找用戶信息
        /// </summary>
        /// <param name="sendTo">選擇的代碼</param>
        /// <returns></returns>
        public DataTable GetWorkDataByDll(string sendTo)
        {
            OracleParameter outCursor = new OracleParameter("dt", OracleType.Cursor);
            outCursor.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_getworkdatabyddl", CommandType.StoredProcedure,
                new OracleParameter("sendto", sendTo), outCursor);
            return dt;
        }
    }
}
