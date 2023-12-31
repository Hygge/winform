﻿
using System;
namespace domain.Model
{
	/// <summary>
	/// DishInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DishInfo
	{
		public DishInfo()
		{}
		#region Model
		private int _did;
		private string _dtitle;
		private string _dtypetitle;
		private int? _dtypeid;
		private decimal? _dprice;
		private string _dchar;
		private bool _disdelete;
		/// <summary>
		/// 
		/// </summary>
		public int DId
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DTitle
		{
			set{ _dtitle=value;}
			get{return _dtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DTypeId
		{
			set{ _dtypeid=value;}
			get{return _dtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DPrice
		{
			set{ _dprice=value;}
			get{return _dprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DChar
		{
			set{ _dchar=value;}
			get{return _dchar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool DIsDelete
		{
			set{ _disdelete=value;}
			get{return _disdelete;}
		}
		public string DTypeTitle
        {
			set{ _dtypetitle = value;}
			get{return _dtypetitle; }
		}

		#endregion Model

	}
}

