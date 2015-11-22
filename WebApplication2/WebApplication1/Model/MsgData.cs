using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// MsgData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MsgData
	{
		public MsgData()
		{}
		#region Model
		private int _msgid;
		private string _phonenumber;
		private string _msg;
		private DateTime _msgtime;
		private bool _isrecivedmsg;
		private string _recivemsgstatus;
		private string _returnedid;
		/// <summary>
		/// 流水号
		/// </summary>
		public int MsgID
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// 手机号
		/// </summary>
		public string PhoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		/// <summary>
		/// 接收或发送短信信息
		/// </summary>
		public string Msg
		{
			set{ _msg=value;}
			get{return _msg;}
		}
		/// <summary>
		/// 接收或发送短信时间
		/// </summary>
		public DateTime MsgTime
		{
			set{ _msgtime=value;}
			get{return _msgtime;}
		}
		/// <summary>
		/// 是否为从客户那里接收到的短信
		/// </summary>
		public bool IsRecivedMsg
		{
			set{ _isrecivedmsg=value;}
			get{return _isrecivedmsg;}
		}
		/// <summary>
		/// 接收短信格式是否正确验证标识 0已过期 1已验证 2已兑换 3无效代码
		/// </summary>
		public string ReciveMsgStatus
		{
			set{ _recivemsgstatus=value;}
			get{return _recivemsgstatus;}
		}
		/// <summary>
		/// 发送消息到短信平台时，短信平台回复的消息ID
		/// </summary>
		public string ReturnedID
		{
			set{ _returnedid=value;}
			get{return _returnedid;}
		}
		#endregion Model

	}
}

