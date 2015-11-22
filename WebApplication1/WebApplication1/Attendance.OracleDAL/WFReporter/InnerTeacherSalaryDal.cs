/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： InnerTeacherSalaryDal.cs
 * 檔功能描述： 內部講師費數據訪問
 * 
 * 版本：1.0
 * 創建標識： 張明強 2012.03.19
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.WFReporter;
using GDSBG.MiABU.Attendance.IDAL.WFReporter;
using System.Data.OleDb;

namespace GDSBG.MiABU.Attendance.OracleDAL.WFReporter
{
    public class InnerTeacherSalaryDal : DALBase<TeacherInModel>, IInnerTeacherSalaryDal
    {
    }
}
