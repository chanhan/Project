using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.WorkFlow
{
    public class WorkFlowLimitDal : DALBase<WorkFLowLimitModel>, IWorkFlowLimitDal
    {
        /// <summary>
        ///  獲取必簽人員信息列表
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>
        public DataTable GetSignLimitInfo(string deptcode, string formtype, List<string> resonlist)
        {
            string sql
            = "SELECT flow_empno, flow_empname, flow_notes, flow_manager, flow_type, " +
                "       flow_level " +
                "  FROM GDS_WF_FLOWLIMIT " +
                " WHERE formtype = :formtype AND deptcode = :deptcode ";
            if (resonlist != null && resonlist.Count > 0)
            {
                for (int i = 0; i < resonlist.Count; i++)
                {

                    sql += " and reason" + (i + 1).ToString() + "= '" + resonlist[i] + "' ";
                }
            }
            sql += " order by ORDERID ";
            return DalHelper.ExecuteQuery(sql, new OracleParameter(":formtype", formtype), new OracleParameter(":deptcode", deptcode));
        }


        public bool SaveflowlimitInfo(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy, SynclogModel logmodel)
        {
            bool result = false;
            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;
            try
            {
                string sql = " delete from GDS_WF_FLOWLIMIT where formtype = '" + Formtype + "' AND deptcode = '" + deptid + "' ";
                if (resonlist != null && resonlist.Count > 0)
                {
                    for (int i = 0; i < resonlist.Count; i++)
                    {                           
                        sql += " and reason" + (i + 1).ToString() + "= '" + resonlist[i] + "' ";                            
                    }
                }
                command.CommandText = sql;
                command.ExecuteNonQuery();
                SaveLogData("D", "", command.CommandText, command, logmodel);
                foreach (int num in driy.Keys)
                {

                    string sql1
                  = "INSERT INTO GDS_WF_FLOWLIMIT " +
                      "            (formtype, reason1, reason2, reason3, reason4, flow_empno, " +
                      "             flow_empname, flow_notes, flow_manager, flow_type, flow_level, " +
                      "             orderid, deptcode " +
                      "            ) " +
                      "     VALUES ('" + Formtype + "', '" + (resonlist.Count >= 1 ? resonlist[0] : string.Empty) + "', '" + (resonlist.Count >= 2 ? resonlist[1] : string.Empty) + "', '" + (resonlist.Count >= 3 ? resonlist[2] : string.Empty) + "', '" + (resonlist.Count >= 4 ? resonlist[3] : string.Empty) + "' " +
                      "     ,'" + driy[num][0] + "', '" + driy[num][1] + "', '" + driy[num][2] + "', '" + driy[num][3] + "', '', '','" + num + "', '" + deptid + "' " +
                      "            ) ";
                    command.CommandText = sql1;
                    command.ExecuteNonQuery();
                    SaveLogData("I", "", command.CommandText, command, logmodel);
                }
                trans.Commit();
                command.Connection.Close();
                result = true;                    
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result = false;
                command.Connection.Close();
            }               
            return result;
        }
        public void SaveLogData(string strFlag, string DocNo, string LogText, OracleCommand command, SynclogModel logmodel)
        {

            try
            {
                string ProcessFlag = "";

                if (strFlag == "I")
                {
                    ProcessFlag = "INSERT";
                }
                else if (strFlag == "U")
                {
                    ProcessFlag = "UPDATE";
                }
                else if (strFlag == "D")
                {
                    ProcessFlag = "DELETE";
                }

                command.CommandText = "Insert into GDS_SC_SYNCLOG " +
                "   (transactiontype,levelno,FromHost,ToHost,docno,text,logtime,processflag,processowner) " +
                " values('" + logmodel.TransactionType + "','2','" + logmodel.FromHost + "','" + logmodel.ToHost + "','" + DocNo + "','" + LogText.Replace("'", "''") + "',sysdate,'" + ProcessFlag + "','" + logmodel.ProcessOwner + "')";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }
  }
}
