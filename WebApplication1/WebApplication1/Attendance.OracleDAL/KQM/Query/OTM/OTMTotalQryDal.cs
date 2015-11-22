/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTotalQryDal.cs
 * 檔功能描述： 加班匯總數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.30
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
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data.OleDb;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query.OTM
{
    public class OTMTotalQryDal : DALBase<OTMTotalQryModel>, IOTMTotalQryDal
    {
        #region 查詢語句
        /// <summary>
        /// 加班匯總查詢
        /// </summary>
        /// <param name="model">加班匯總model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno,yearmonth,g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain,billno,apremark,approver,approvedate,
                             mreladjust, approveflag, specg1apply,specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,depcode,dissignrmark,
                             overtimetype, localname, dname, day1, day2, day3, day4, day5, day6,day7, day8, day9, day10, day11, day12, day13, day14, day15, day16,
                             day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,day27, day28, day29, day30, day31, buname, approveflagname
                             FROM gds_att_monthtotal_v a where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "','§')))";
            }
            if (overTimeType != "")
            {
                cmdText = cmdText + " and a.overTimeType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + overTimeType + "', '§')))";
            }
            if (approveFlag != "")
            {
                cmdText = cmdText + " and a.approveflag  in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + approveFlag + "', '§')))";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 月加班匯總查詢
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="empStatus">人员类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string flag, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, string empStatus, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno,yearmonth, g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain, mreladjust, approveflag, specg1apply,
                               specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,overtimetype, localname, advanceadjust, restadjust, dname, day1, day2,depcode,
                               day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,day14, day15, day16, day17, day18, day19, day20, day21, day22, day23,
                               day24, day25, day26, day27, day28, day29, day30, day31, specday1, specday2, specday3, specday4, specday5, specday6, specday7, specday8,
                               specday9, specday10, specday11, specday12, specday13, specday14,specday15, specday16, specday17, specday18, specday19, specday20,dissignrmark,
                               specday21, specday22, specday23, specday24, specday25, specday26,specday27, specday28, specday29, specday30, specday31, buname,billno,apremark,approver,approvedate,
                               approveflagname, flag  FROM gds_att_monthtotal_v a WHERE 1 = 1  ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "','§')))";
            }
            if (overTimeType != "")
            {
                cmdText = cmdText + " and a.overTimeType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + overTimeType + "', '§')))";
            }
            if (approveFlag != "")
            {
                cmdText = cmdText + " and a.approveflag  in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + approveFlag + "', '§')))";
            }
            if (empStatus != "")
            {
                cmdText = cmdText + "and a.STATUS in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + empStatus + "', '§')))";
            }
            if (flag != "")
            {
                if (flag.Equals("N"))
                {
                    cmdText = cmdText + " AND a.flag='Local' ";
                }
                else
                {
                    cmdText = cmdText + " AND a.flag='Supporter' ";
                }
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 加班匯總查詢
        /// </summary>
        /// <param name="model">加班匯總model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, yearmonth,g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain, dissignrmark,
                             mreladjust, approveflag, specg1apply,specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,depcode,billno,apremark,approver,approvedate, 
                             overtimetype, localname, dname, day1, day2, day3, day4, day5, day6,day7, day8, day9, day10, day11, day12, day13, day14, day15, day16,
                             day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,day27, day28, day29, day30, day31, buname, approveflagname
                             FROM gds_att_monthtotal_v a where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "','§')))";
            }
            if (overTimeType != "")
            {
                cmdText = cmdText + " and a.overTimeType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + overTimeType + "', '§')))";
            }
            if (approveFlag != "")
            {
                cmdText = cmdText + " and a.approveflag  in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + approveFlag + "', '§')))";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 月加班匯總查詢(导出)
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="empStatus">人员类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string flag, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, string empStatus)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno,yearmonth, g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain, mreladjust, approveflag, specg1apply,
                               specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,overtimetype, localname, advanceadjust, restadjust, dname, day1, day2,depcode,
                               day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,day14, day15, day16, day17, day18, day19, day20, day21, day22, day23,
                               day24, day25, day26, day27, day28, day29, day30, day31, specday1, specday2, specday3, specday4, specday5, specday6, specday7, specday8,
                               specday9, specday10, specday11, specday12, specday13, specday14,specday15, specday16, specday17, specday18, specday19, specday20,dissignrmark,
                               specday21, specday22, specday23, specday24, specday25, specday26,specday27, specday28, specday29, specday30, specday31, buname,billno,apremark,approver,approvedate,
                               approveflagname, flag  FROM gds_att_monthtotal_v a WHERE 1 = 1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "','§')))";
            }
            if (overTimeType != "")
            {
                cmdText = cmdText + " and a.overTimeType in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + overTimeType + "', '§')))";
            }
            if (approveFlag != "")
            {
                cmdText = cmdText + " and a.approveflag  in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + approveFlag + "', '§')))";
            }
            if (empStatus != "")
            {
                cmdText = cmdText + "and a.STATUS in (SELECT char_list FROM TABLE (gds_sc_chartotable ('" + empStatus + "', '§')))";
            }
            if (flag != "")
            {
                if (flag.Equals("N"))
                {
                    cmdText = cmdText + " AND a.flag='Local' ";
                }
                else
                {
                    cmdText = cmdText + " AND a.flag='Supporter' ";
                }
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        #endregion
        #region getLIst
        /// <summary>
        /// 返回ModelLIst  導出
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OTMTotalQryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
        #endregion
        #region 根據sql查詢相應的信息
        /// <summary>
        /// 查詢固定信息（是否是專案加班）
        /// </summary>
        /// <returns></returns>
        public string GetParemeterValue()
        {
            DataTable dt = DalHelper.ExecuteQuery("SELECT nvl(max(paravalue),'Y') paravalue FROM gds_sc_parameter WHERE paraname='KQMSpecLeaveUCheck'");
            if (dt != null)
            {
                return dt.Rows[0]["paravalue"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根據sql語句查詢相應的數據
        /// </summary>
        /// <returns></returns>
        public string GetValue(string sql)
        {
            if (DalHelper.ExecuteScalar(sql) != null)
            {
                return Convert.ToString(DalHelper.ExecuteScalar(sql));
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region 查詢單人信息（修改頁面） 組織權限內
        /// <summary>
        /// 查詢單人信息（修改頁面）
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string SQLDep)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, yearmonth, g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain, mreladjust, approveflag, specg1apply,
                               specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,overtimetype, localname, advanceadjust, restadjust, dname, buname,
                               approveflagname, flag,depcode  FROM gds_att_monthtotal_v a WHERE 1 = 1  ";
            cmdText += strCon;
            cmdText = cmdText + "AND exists (SELECT 1 FROM (" + SQLDep + ") e where e.DepCode=a.depcode)";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        #endregion
        #region 查詢單人信息（修改頁面保存按鈕）
        /// <summary>
        /// 查詢單人信息（修改頁面保存按鈕）
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model)
        {
            return DalHelper.Select(model);
        }
        #endregion
        #region  驗證修改的實發G2時數是否符合條件  和數據庫中數據對比
        /// <summary>
        /// 驗證修改部份的信息 
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="dt">需要修改的datatable</param>
        /// <returns></returns>
        public bool SaveData(string processFlag, string personCode, DataTable dataTable)
        {
            bool bValue = true;
            foreach (DataRow newRow in dataTable.Rows)
            {
                string KQMLeaveUCheck = GetParemeterValue();
                KQMLeaveUCheck = "N";
                if (processFlag.Equals("Modify"))
                {
                    double dG12UPLMT;
                    string OverTimeType;
                    string DCode;
                    double G2MLimit;
                    double dG2RelOld = Convert.ToDouble(GetValue("select nvl(max(G2RelSalary),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                    double G2RelEdit = 0.0;
                    if (Convert.ToDecimal(GetValue("SELECT count(1) FROM gds_att_monthtotalEdit WHERE  WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) > 0M)
                    {
                        G2RelEdit = (Convert.ToDouble(GetValue("select nvl(max(G2RelEdit),0) from gds_att_monthtotalEdit Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) + dG2RelOld) - Convert.ToDouble(newRow["G2RelSalary"]);
                        if (G2RelEdit < 0.0)
                        {
                            if (KQMLeaveUCheck.Equals("N"))
                            {
                                return false;
                            }
                            dG12UPLMT = Convert.ToDouble(GetValue("select nvl(max(G12UPLMT),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                            if ((Convert.ToDouble(GetValue("select nvl(max(G1RelSalary),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) + Convert.ToDouble(newRow["G2RelSalary"])) > dG12UPLMT)
                            {
                                return false;
                            }
                            OverTimeType = Convert.ToString(GetValue("select nvl(max(OverTimeType),'F') from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                            DCode = Convert.ToString(GetValue("select nvl(DCode,DepCode) from gds_att_Employee Where WorkNo='" + newRow["WorkNo"] + "'"));
                            G2MLimit = 0.0;
                            G2MLimit = Convert.ToDouble(GetValue("select G2MLimit from(select nvl(a.G2MLimit,-1) G2MLimit from gds_att_type a,gds_sc_department b,gds_sc_deplevel c where a.OrgCode=b.DepCode(+) and b.LevelCode=c.LevelCode(+)  and a.OTTypeCode='" + OverTimeType + "' AND a.EffectFlag='Y' and b.depcode in (SELECT DepCode FROM gds_sc_department  e   START WITH e.depCode ='" + DCode + "'  CONNECT BY e.depcode = PRIOR e.parentdepcode)  order by c.orderid desc) where rownum<=1 "));
                            if ((G2MLimit >= 0.0) && (Convert.ToDouble(newRow["G2RelSalary"]) > G2MLimit))
                            {
                                return false;
                            }
                        }
                        DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotalEdit set G2RelEdit='" + G2RelEdit.ToString() + "', Modifier='" + personCode + "', ModifyDate=sysdate Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'");
                    }
                    else
                    {
                        G2RelEdit = dG2RelOld - Convert.ToDouble(newRow["G2RelSalary"]);
                        if (G2RelEdit < 0.0)
                        {
                            if (KQMLeaveUCheck.Equals("N"))
                            {
                                return false;
                            }
                            dG12UPLMT = Convert.ToDouble(GetValue("select nvl(max(G12UPLMT),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                            if ((Convert.ToDouble(GetValue("select nvl(max(G1RelSalary),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) + Convert.ToDouble(newRow["G2RelSalary"])) > dG12UPLMT)
                            {
                                return false;
                            }
                            OverTimeType = Convert.ToString(GetValue("select nvl(max(OverTimeType),'F') from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                            DCode = Convert.ToString(GetValue("select nvl(DCode,DepCode) from gds_att_Employee Where WorkNo='" + newRow["WorkNo"] + "'"));
                            G2MLimit = 0.0;
                            G2MLimit = Convert.ToDouble(GetValue("select G2MLimit from(select nvl(a.G2MLimit,-1) G2MLimit from gds_att_type a,gds_sc_department b,gds_sc_deplevel c where a.OrgCode=b.DepCode(+) and b.LevelCode=c.LevelCode(+)  and a.OTTypeCode='" + OverTimeType + "' AND a.EffectFlag='Y' and b.depcode in (SELECT DepCode FROM gds_sc_department  e   START WITH e.depCode ='" + DCode + "'  CONNECT BY e.depcode = PRIOR e.parentdepcode)  order by c.orderid desc) where rownum<=1 "));
                            if ((G2MLimit >= 0.0) && (Convert.ToDouble(newRow["G2RelSalary"]) > G2MLimit))
                            {
                                return false;
                            }
                        }
                        DalHelper.ExecuteNonQuery("insert into gds_att_monthtotalEdit(workNo,YearMonth,G2RelEdit,Modifier,ModifyDate) values('" + newRow["WorkNo"] + "','" + newRow["YearMonth"] + "'," + G2RelEdit.ToString() + ",'" + personCode + "',sysdate)");
                    }
                    DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotal set G2RelSalary='" + newRow["G2RelSalary"] + "', MRelAdjust='" + newRow["MRelAdjust"] + "', G2ReMain='" + newRow["G2ReMain"] + "' Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'");
                    if (Convert.ToString(GetValue("SELECT nvl(max(paravalue),'Y') FROM gds_sc_parameter WHERE paraname='KQMSpecLeaveUCheck'")).Equals("Y"))
                    {
                        double dSpecG2RelOld = Convert.ToDouble(GetValue("select nvl(max(SPECG2SALARY),0) from gds_att_monthtotal Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'"));
                        double SpecG2RelEdit = 0.0;
                        if (Convert.ToDecimal(GetValue("SELECT count(1) FROM gds_att_monthtotalEdit WHERE  WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) > 0M)
                        {
                            SpecG2RelEdit = (Convert.ToDouble(GetValue("select nvl(max(SpecG2RelEdit),0) from gds_att_monthtotalEdit Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'")) + dSpecG2RelOld) - Convert.ToDouble(newRow["SPECG2SALARY"]);
                            if (SpecG2RelEdit < 0.0)
                            {
                                bValue = false;
                                return bValue;
                            }
                            DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotalEdit set SpecG2RelEdit='" + SpecG2RelEdit.ToString() + "', Modifier='" + personCode + "', ModifyDate=sysdate Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'");
                        }
                        else
                        {
                            SpecG2RelEdit = dSpecG2RelOld - Convert.ToDouble(newRow["SPECG2SALARY"]);
                            if (SpecG2RelEdit < 0.0)
                            {
                                bValue = false;
                                return bValue;
                            }
                            DalHelper.ExecuteNonQuery("insert into gds_att_monthtotalEdit(workNo,YearMonth,SpecG2RelEdit,Modifier,ModifyDate) values('" + newRow["WorkNo"] + "','" + newRow["YearMonth"] + "'," + SpecG2RelEdit.ToString() + ",'" + personCode + "',sysdate)");
                        }
                        DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotal set SPECG2SALARY='" + newRow["SPECG2SALARY"] + "', MRelAdjust='" + newRow["MRelAdjust"] + "' Where WorkNo='" + newRow["WorkNo"] + "' and YearMonth='" + newRow["YearMonth"] + "'");
                    }
                }
            }
            return bValue;
        }
        #endregion
        #region 人員計算  組織計算
        /// <summary>
        /// 更改對應的信息（gds_att_monthotal表等）
        /// </summary>
        /// <param name="workNo">工號  主鍵1</param>
        /// <param name="yearMonth">年月  主鍵2</param>
        public void CountCanAdjlasthy(string workNo, string yearMonth)
        {
            yearMonth = yearMonth.Insert(4, "/");
            DateTime CountDate = Convert.ToDateTime(yearMonth + "/01").AddMonths(1).AddDays(-1.0);
            OracleParameter[] paramList = new OracleParameter[] { new OracleParameter("p_date", OracleType.DateTime, 0, CountDate.ToString("yyyy/MM/dd")), new OracleParameter("v_adddays", OracleType.Int32, 0, "0"), new OracleParameter("v_dcode", OracleType.VarChar, 20, ""), new OracleParameter("v_empno", OracleType.VarChar, 10, workNo) };
            DataTable dt = DalHelper.ExecuteQuery("gds_att_otmonthtotaldataPro", CommandType.StoredProcedure, paramList);
        }
        /// <summary>
        /// 組織計算 
        /// </summary>
        /// <param name="sWorkNo">工號</param>
        /// <param name="YearMonth">年月</param>
        /// <param name="sDCode">部門代碼</param>
        public void CountCanAdjlasthy(string sWorkNo, string YearMonth, string sDCode)
        {
            try
            {
                YearMonth = YearMonth.Insert(4, "/");
                DateTime CountDate = Convert.ToDateTime(YearMonth + "/01").AddMonths(1).AddDays(-1.0);
                OracleParameter[] paramList = new OracleParameter[] { new OracleParameter("p_date", OracleType.DateTime, 0, CountDate.ToString("yyyy/MM/dd")), new OracleParameter("v_adddays", OracleType.Int32, 0, "0"), new OracleParameter("v_dcode", OracleType.VarChar, 20, sDCode), new OracleParameter("v_empno", OracleType.VarChar, 10, sWorkNo) };
                DataTable dt = DalHelper.ExecuteQuery("gds_att_otmonthtotaldataPro", CommandType.StoredProcedure, paramList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 修改
        /// <summary>
        /// 核准的保存按鈕
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="Status">狀態</param>
        /// <param name="model">加班匯總查詢model</param>
        /// <param name="logmodel">管理日誌的model</param>
        /// <returns></returns>
        public int UpdateMonthTal(string workNo, string yearMonth, string status, OTMTotalQryModel model, SynclogModel logmodel)
        {
            string cmdText = " YearMonth ='" + yearMonth + "'";
            if (workNo == null && yearMonth == null)
            {
                if (model.Approver != "" && model.Approver != null)
                {
                    return DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotal SET ApproveFlag='" + status + "', Approver=' " + model.Approver + "',ApproveDate=trunc(sysdate) where WorkNo='" + workNo + "' and YearMonth ='" + yearMonth + "' ");
                }
                else
                {
                    return DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotal SET ApproveFlag='" + status + "', Approver=' " + model.Approver + "',ApproveDate=null where WorkNo='" + workNo + "' and YearMonth ='" + yearMonth + "'");
                }
            }
            else
            {
                return DalHelper.ExecuteNonQuery("UPDATE gds_att_monthtotal SET ApproveFlag='" + status + "', Approver=' " + model.Approver + "',ApproveDate=trunc(sysdate)   where WorkNo='" + workNo + "' and YearMonth ='" + yearMonth + "'");
            }
        }
        #endregion
        #region 導入
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, string sqlDep, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_att_monthtotal_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_sqldep", sqlDep), outCursor, outSuccess, outError, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }

        /// <summary>
        /// 獲取數據源 ---導入
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public DataView ExceltoDataView(string strFilePath)
        {
            DataView dv;
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'");
                conn.Open();
                object[] CSs0s0001 = new object[4];
                CSs0s0001[3] = "TABLE";
                DataTable tblSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, CSs0s0001);
                string tableName = Convert.ToString(tblSchema.Rows[0]["TABLE_NAME"]);
                if (tblSchema.Rows.Count > 1)
                {
                    tableName = "sheet1$";
                }
                string sql_F = "SELECT * FROM [{0}]";
                OleDbDataAdapter adp = new OleDbDataAdapter(string.Format(sql_F, tableName), conn);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Excel");
                dv = ds.Tables[0].DefaultView;
                conn.Close();
            }
            catch (Exception)
            {
                Exception strEx = new Exception("請確認是否使用模板上傳(上傳的Excel中第一個工作表名稱是否為Sheet1)");
                throw strEx;
            }
            return dv;
        }
        #endregion

        #region  電子簽核部份,送簽
        /// <summary>
        /// 根據單據類型和組織ID查詢可以簽核的最近的部門ID
        /// </summary>
        /// <param name="OrgCode">組織</param>
        /// <param name="billTypeCode"></param>
        /// <returns></returns>
        public string GetWorkFlowOrgCode(string OrgCode, string billTypeCode)
        {
            string ReturnOrgCode = "";
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT depcode FROM (SELECT   b.depcode, a.orderid FROM gds_wf_flowset a, (SELECT 
                                                 LEVEL orderid, depcode FROM gds_sc_department  START WITH depcode =:OrgCode CONNECT BY 
                                                 PRIOR parentdepcode = depcode   ORDER BY LEVEL) b  WHERE a.deptcode = b.depcode  AND
                                                 a.formtype =:billTypeCode  ORDER BY orderid) WHERE ROWNUM <= 1",
                                                 new OracleParameter("OrgCode", OrgCode), new OracleParameter("billTypeCode", billTypeCode));
            if (dt.Rows.Count > 0)
            {
                ReturnOrgCode = dt.Rows[0][0].ToString();
            }
            return ReturnOrgCode;
        }
        /// <summary>
        /// 送簽的流程   發起表單
        /// </summary>
        /// <param name="processFlag">標識位（是新發起的表單還是簽核中）</param>
        /// <param name="ID">數據的ID</param>
        /// <param name="billNoType">單據類型</param>
        /// <param name="auditOrgCode">要簽核單位</param>
        /// <param name="billTypeCode">單據類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        public string SaveAuditData(string processFlag, string WorkNo, string YearMonth, string billNoType, string auditOrgCode, string billTypeCode, string Person, string QueryFlag, SynclogModel logmodel)
        {

            string strMax = "";
            string num = "0";
            string num1 = "0";
            if (processFlag.Equals("Add"))
            {
                try
                {
                    billNoType = billNoType + auditOrgCode;
                    string sql = "SELECT nvl(MAX (billno),'0') strMax  FROM gds_att_monthtotal WHERE billno LIKE '" + billNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                    DataTable dt_str = DalHelper.ExecuteQuery(sql);
                    if (dt_str != null && dt_str.Rows.Count > 0)
                    {
                        if (dt_str.Rows[0]["strMax"].ToString() == "0")
                        {
                            strMax = string.Empty;
                        }
                        else
                        {
                            strMax = dt_str.Rows[0]["strMax"].ToString();
                        }
                    }
                    if (strMax.Length == 0)
                    {
                        strMax = billNoType + DateTime.Now.ToString("yyMM") + "0001";
                    }
                    else
                    {
                        int i = Convert.ToInt32(strMax.Substring(billNoType.Length + 4)) + 1;
                        strMax = i.ToString().PadLeft(4, '0');
                        strMax = billNoType + DateTime.Now.ToString("yyMM") + strMax;
                    }
                    string sql2 = "SELECT count(1) num FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";
                    DataTable dt_1 = DalHelper.ExecuteQuery(sql2);

                    if (dt_1 != null && dt_1.Rows.Count > 0)
                    {
                        num = dt_1.Rows[0]["num"].ToString();
                    }
                    string sql4 = "SELECT count(1) num FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";
                    DataTable dt_2 = DalHelper.ExecuteQuery(sql4);

                    if (dt_2 != null && dt_2.Rows.Count > 0)
                    {
                        num1 = dt_1.Rows[0]["num"].ToString();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }


            OracleCommand command = new OracleCommand();
            command.Connection = DalHelper.Connection;
            command.Connection.Open();
            OracleTransaction trans = command.Connection.BeginTransaction();
            command.Transaction = trans;

            try
            {
                if (processFlag.Equals("Add"))
                {

                    command.CommandText = "UPDATE gds_att_monthtotal SET APPROVEFLAG='1' , BillNo =  '" + strMax + "' Where workNo='" + WorkNo + "' and yearmonth='" + YearMonth + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", strMax, command.CommandText, command, logmodel);
                    if (num == "0")
                    {
                        command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + auditOrgCode + "','" + Person + "',sysdate,'0','" + billTypeCode + "')";
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command, logmodel);
                    }
                    else
                    {
                        command.CommandText = "update GDS_ATT_BILL set OrgCode='" + auditOrgCode + "',ApplyMan='" + Person + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + billTypeCode + "' where BillNo='" + strMax + "'";
                        command.ExecuteNonQuery();
                        SaveLogData("U", strMax, command.CommandText, command, logmodel);
                    }

                    if (num1 != "0")
                    {
                        command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                        command.ExecuteNonQuery();
                        SaveLogData("D", strMax, command.CommandText, command, logmodel);
                    }
                    // command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + auditOrgCode + "' and REASON1='" + OTType + "' ";
                    //command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes)  " +
                    //                  "select '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'  from (  " +
                    //                          " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + billTypeCode + "' and DEPTCODE='" + auditOrgCode + "') " +
                    //                          " where  FLOW_EMPNO!='" + WorkNo + "'or (FLOW_EMPNO='" + WorkNo + "' and FLOW_LEVEL not in ('課級主管','部級主管'))";
                    command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes,OLDAUDITMAN)  " +
                                    "select '" + strMax + "',  nvl(getagentempno(FLOW_EMPNO,'" + auditOrgCode + "'),FLOW_EMPNO) as FLOW_EMPNO, ORDERID,'0','N',decode(NVL (getagentempno (flow_empno, '" + auditOrgCode + "'), flow_empno),flow_empno,'',flow_empno) as oldauditman  from (  " +
                                            " select * from GDS_WF_FLOWSET WHERE FORMTYPE='" + billTypeCode + "' and DEPTCODE='" + auditOrgCode + "') " +
                                            " where  FLOW_EMPNO!='" + WorkNo + "'or (FLOW_EMPNO='" + WorkNo + "' and FLOW_LEVEL not in ('課級主管','部級主管'))";
                    command.ExecuteNonQuery();
                    SaveLogData("I", strMax, command.CommandText, command, logmodel);
                }
                else if (processFlag.Equals("Modify"))
                {
                    strMax = billNoType;
                    command.CommandText = "UPDATE gds_att_monthtotal SET APPROVEFLAG='1' , BillNo =  '" + strMax + "' where workNo='" + WorkNo + "' and yearmonth='" + YearMonth + "'";
                    command.ExecuteNonQuery();
                    SaveLogData("U", strMax, command.CommandText, command, logmodel);
                }
                trans.Commit();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                command.Connection.Close();
                throw ex;

            }
            return strMax;
        }

        /// <summary>
        /// 根據數據的ID查詢單號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDtByID(string WorkNo, string YearMonth)
        {
            return DalHelper.ExecuteQuery(@"select workNo,BILLNO from gds_att_monthtotal where APPROVEFLAG in('0','3') AND workNo='" + WorkNo + "' and yearmonth='" + YearMonth + "'");
        }
        #endregion
        #region  組織送簽
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="diry"></param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="BillTypeCode">單據類型代碼</param>
        /// <param name="Person">簽核人</param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<OTMTotalQryModel>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
        {
            string strMax = "";
            string num = "0";
            string num1 = "0";
            int k = 0;
            foreach (string key in diry.Keys)
            {
                string AuditOrgCode = key;
                if (processFlag.Equals("Add"))
                {
                    try
                    {
                        BillNoType = BillNoType + AuditOrgCode;
                        string sql = "SELECT nvl(MAX (billno),'0') strMax  FROM GDS_ATT_ADVANCEAPPLY WHERE billno LIKE '" + BillNoType + "'|| TO_CHAR (SYSDATE, 'yymm')|| '%'";
                        DataTable dt_str = DalHelper.ExecuteQuery(sql);
                        if (dt_str != null && dt_str.Rows.Count > 0)
                        {
                            if (dt_str.Rows[0]["strMax"].ToString() == "0")
                            {
                                strMax = string.Empty;
                            }
                            else
                            {
                                strMax = dt_str.Rows[0]["strMax"].ToString();
                            }
                        }
                        if (strMax.Length == 0)
                        {
                            strMax = BillNoType + DateTime.Now.ToString("yyMM") + "0001";
                        }
                        else
                        {
                            int i = Convert.ToInt32(strMax.Substring(BillNoType.Length + 4)) + 1;
                            strMax = i.ToString().PadLeft(4, '0');
                            strMax = BillNoType + DateTime.Now.ToString("yyMM") + strMax;
                        }
                        string sql2 = "SELECT count(1) num FROM GDS_ATT_BILL WHERE BillNo='" + strMax + "'";
                        DataTable dt_1 = DalHelper.ExecuteQuery(sql2);

                        if (dt_1 != null && dt_1.Rows.Count > 0)
                        {
                            num = dt_1.Rows[0]["num"].ToString();
                        }
                        string sql4 = "SELECT count(1) num FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "'";
                        DataTable dt_2 = DalHelper.ExecuteQuery(sql4);

                        if (dt_2 != null && dt_2.Rows.Count > 0)
                        {
                            num1 = dt_1.Rows[0]["num"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                OracleCommand command = new OracleCommand();
                command.Connection = DalHelper.Connection;
                command.Connection.Open();
                OracleTransaction trans = command.Connection.BeginTransaction();
                command.Transaction = trans;

                try
                {
                    if (processFlag.Equals("Add"))
                    {
                        foreach (OTMTotalQryModel model in diry[key])
                        {
                            command.CommandText = "UPDATE gds_att_monthtotal SET APPROVEFLAG='1' , BillNo =  '" + strMax + "' where  workNo='" + model.WorkNo + "' and yearmonth='" + model.YearMonth + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        if (num == "0")
                        {
                            command.CommandText = "insert into GDS_ATT_BILL(BillNo,OrgCode,ApplyMan,ApplyDate,Status,BillTypeCode) values('" + strMax + "','" + AuditOrgCode + "','" + Person + "',sysdate,'0','" + BillTypeCode + "')";
                            command.ExecuteNonQuery();
                            SaveLogData("I", strMax, command.CommandText, command, logmodel);
                        }
                        else
                        {
                            command.CommandText = "update GDS_ATT_BILL set OrgCode='" + AuditOrgCode + "',ApplyMan='" + Person + "',ApplyDate=sysdate,Status='0',BillTypeCode='" + BillTypeCode + "'  where BillNo='" + strMax + "'";
                            command.ExecuteNonQuery();
                            SaveLogData("U", strMax, command.CommandText, command, logmodel);
                        }

                        if (num1 != "0")
                        {
                            command.CommandText = "delete FROM GDS_ATT_AUDITSTATUS WHERE BillNo='" + strMax + "' ";
                            command.ExecuteNonQuery();
                            SaveLogData("D", strMax, command.CommandText, command, logmodel);
                        }
                        command.CommandText = "insert into GDS_ATT_AUDITSTATUS(BillNo,AuditMan,OrderNo,AuditStatus,SendNotes) SELECT '" + strMax + "', FLOW_EMPNO, ORDERID,'0','N'   FROM GDS_WF_FLOWSET WHERE FORMTYPE='" + BillTypeCode + "' and DEPTCODE='" + AuditOrgCode + "'";
                        command.ExecuteNonQuery();
                        SaveLogData("I", strMax, command.CommandText, command, logmodel);
                    }
                    else if (processFlag.Equals("Modify"))
                    {
                        foreach (OTMTotalQryModel model in diry[key])
                        {
                            strMax = BillNoType;
                            command.CommandText = "UPDATE gds_att_monthtotal SET APPROVEFLAG='1' , BillNo =  '" + strMax + "' where  workNo='" + model.WorkNo + "' and yearmonth='" + model.YearMonth + "'";
                            command.ExecuteNonQuery();
                        }
                        SaveLogData("U", strMax, command.CommandText, command, logmodel);
                    }
                    trans.Commit();
                    command.Connection.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    command.Connection.Close();
                    throw ex;

                }
                k++;
            }
            return k;
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
        #endregion
    }
}
