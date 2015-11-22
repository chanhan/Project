using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// CodeData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CodeData
	{
		public CodeData()
		{}
		#region Model
		private string _code;
		private string _remark;
		private bool _isvalidated;
		private DateTime? _validatedtime;
		private bool _isexchanged;
		private DateTime? _exchangetime;
		private string _phonenumber;
		/// <summary>
		/// 验证代码
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 密、净、准、宝护盖标识
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 是否已经验证
		/// </summary>
		public bool IsValidated
		{
			set{ _isvalidated=value;}
			get{return _isvalidated;}
		}
		/// <summary>
		/// 验证日期
		/// </summary>
		public DateTime? ValidatedTime
		{
			set{ _validatedtime=value;}
			get{return _validatedtime;}
		}
		/// <summary>
		/// 是否已兑换
		/// </summary>
		public bool IsExchanged
		{
			set{ _isexchanged=value;}
			get{return _isexchanged;}
		}
		/// <summary>
		/// 兑换日期
		/// </summary>
		public DateTime? ExchangeTime
		{
			set{ _exchangetime=value;}
			get{return _exchangetime;}
		}
		/// <summary>
		/// 兑换者电话
		/// </summary>
		public string PhoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		#endregion Model

	}
}

