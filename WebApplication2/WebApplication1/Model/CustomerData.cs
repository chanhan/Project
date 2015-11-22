using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// CustomerData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CustomerData
	{
		public CustomerData()
		{}
		#region Model
		private string _phonenumber;
		private string _customername;
		private string _address;
		private int _miavailable=0;
		private int _jingavailable=0;
		private int _zhunavailable=0;
		private int _baoavaivable=0;
		private int _giftpacks=0;
		private int _productpacks=0;
		/// <summary>
		/// 手机号
		/// </summary>
		public string PhoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string CustomerName
		{
			set{ _customername=value;}
			get{return _customername;}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// '密'可用数
		/// </summary>
		public int MiAvailable
		{
			set{ _miavailable=value;}
			get{return _miavailable;}
		}
		/// <summary>
		/// '净'可用数
		/// </summary>
		public int JingAvailable
		{
			set{ _jingavailable=value;}
			get{return _jingavailable;}
		}
		/// <summary>
		/// '准'可用数
		/// </summary>
		public int ZhunAvailable
		{
			set{ _zhunavailable=value;}
			get{return _zhunavailable;}
		}
		/// <summary>
		/// '宝护盖'可用数
		/// </summary>
		public int BaoAvaivable
		{
			set{ _baoavaivable=value;}
			get{return _baoavaivable;}
		}
		/// <summary>
		/// 已兑换便携装礼包数
		/// </summary>
		public int GiftPacks
		{
			set{ _giftpacks=value;}
			get{return _giftpacks;}
		}
		/// <summary>
		/// 已兑换正装奶粉数
		/// </summary>
		public int ProductPacks
		{
			set{ _productpacks=value;}
			get{return _productpacks;}
		}
		#endregion Model

	}
}

