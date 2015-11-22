using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// DeliveryData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DeliveryData
	{
		public DeliveryData()
		{}
		#region Model
		private string _phonenumber;
		private DateTime _deliverydate;
		private string _customername;
		private string _province;
		private string _city;
		private string _address;
		private string _courierid;
		private string _couriercompanyname;
		private int? _giftpacks;
		private int? _productpacks;
		/// <summary>
		/// 
		/// </summary>
		public string PhoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime DeliveryDate
		{
			set{ _deliverydate=value;}
			get{return _deliverydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerName
		{
			set{ _customername=value;}
			get{return _customername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourierID
		{
			set{ _courierid=value;}
			get{return _courierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourierCompanyName
		{
			set{ _couriercompanyname=value;}
			get{return _couriercompanyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GiftPacks
		{
			set{ _giftpacks=value;}
			get{return _giftpacks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProductPacks
		{
			set{ _productpacks=value;}
			get{return _productpacks;}
		}
		#endregion Model

	}
}

