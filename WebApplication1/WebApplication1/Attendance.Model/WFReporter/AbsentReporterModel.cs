/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AbsentReporterModel.cs
 * 檔功能描述：缺勤統計表實體
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.3.15
 * 
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WFReporter
{
    public class AbsentReporterModel : ModelBase
    {
        private string workno;

        [Column("workno")]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }

        private string bgname;

        [Column("bgname")]
        public string BGName
        {
            get { return bgname; }
            set { bgname = value; }
        }

        private string depcode;

        [Column("depcode")]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }

        private string depname;

        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }

        private string localname;

        [Column("localname")]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }

        private Nullable<Decimal> ab;

        [Column("ab")]
        public Nullable<Decimal> AB
        {
            get { return ab; }
            set { ab = value; }
        }
        private Nullable<Decimal> c;

        [Column("c")]
        public Nullable<Decimal> C
        {
            get { return c; }
            set { c = value; }
        }
        private Nullable<Decimal> i;

        [Column("i")]
        public Nullable<Decimal> I
        {
            get { return i; }
            set { i = value; }
        }
        private Nullable<Decimal> t;

        [Column("t")]
        public Nullable<Decimal> T
        {
            get { return t; }
            set { t = value; }
        }
        private Nullable<Decimal> j;

        [Column("j")]
        public Nullable<Decimal> J
        {
            get { return j; }
            set { j = value; }
        }
        private Nullable<Decimal> s;

        [Column("s")]
        public Nullable<Decimal> S
        {
            get { return s; }
            set { s = value; }
        }
        private Nullable<Decimal> k;

        [Column("k")]
        public Nullable<Decimal> K
        {
            get { return k; }
            set { k = value; }
        }
        private Nullable<Decimal> v;

        [Column("v")]
        public Nullable<Decimal> V
        {
            get { return v; }
            set { v = value; }
        }
        private Nullable<Decimal> y;

        [Column("y")]
        public Nullable<Decimal> Y
        {
            get { return y; }
            set { y = value; }
        }
        private Nullable<Decimal> r;

        [Column("r")]
        public Nullable<Decimal> R
        {
            get { return r; }
            set { r = value; }
        }

        private Nullable<Decimal> x;

        [Column("x")]
        public Nullable<Decimal> X
        {
            get { return x; }
            set { x = value; }
        }
        private Nullable<Decimal> z;

        [Column("z")]
        public Nullable<Decimal> Z
        {
            get { return z; }
            set { z = value; }
        }
        private Nullable<Decimal> numcount;

        [Column("numcount")]
        public Nullable<Decimal> Numcount
        {
            get { return numcount; }
            set { numcount = value; }
        }

        private string remark;

        [Column("Remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
