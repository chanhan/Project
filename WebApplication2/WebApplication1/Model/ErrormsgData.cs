using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// ErrormsgData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ErrormsgData
    {
        public ErrormsgData()
        { }
        #region Model
        private DateTime? _returntime;
        private string _phonenumber;
        private int _returnedid;
        private string _errormsg;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnTime
        {
            set { _returntime = value; }
            get { return _returntime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber
        {
            set { _phonenumber = value; }
            get { return _phonenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReturnedID
        {
            set { _returnedid = value; }
            get { return _returnedid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Errormsg
        {
            set { _errormsg = value; }
            get { return _errormsg; }
        }
        #endregion Model

    }
}

