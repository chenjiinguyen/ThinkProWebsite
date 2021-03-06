﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THINKPROResource
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="THINKPRO")]
	public partial class ThinkProDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSANPHAM(SANPHAM instance);
    partial void UpdateSANPHAM(SANPHAM instance);
    partial void DeleteSANPHAM(SANPHAM instance);
    #endregion
		
		public ThinkProDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["THINKPROConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ThinkProDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ThinkProDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ThinkProDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ThinkProDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SANPHAM> SANPHAMs
		{
			get
			{
				return this.GetTable<SANPHAM>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SANPHAM")]
	public partial class SANPHAM : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ID_SP;
		
		private string _ID_BRAND;
		
		private string _ID_LOAI;
		
		private string _TENSP;
		
		private string _ANH_SP;
		
		private System.Nullable<double> _GIATIEN;
		
		private string _DONVITINH;
		
		private System.Nullable<int> _SOLUONGTONKHO;
		
		private string _ID_TINHTRANG;
		
		private string _THOIHAN_BH;
		
		private string _THONGTIN;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_SPChanging(string value);
    partial void OnID_SPChanged();
    partial void OnID_BRANDChanging(string value);
    partial void OnID_BRANDChanged();
    partial void OnID_LOAIChanging(string value);
    partial void OnID_LOAIChanged();
    partial void OnTENSPChanging(string value);
    partial void OnTENSPChanged();
    partial void OnANH_SPChanging(string value);
    partial void OnANH_SPChanged();
    partial void OnGIATIENChanging(System.Nullable<double> value);
    partial void OnGIATIENChanged();
    partial void OnDONVITINHChanging(string value);
    partial void OnDONVITINHChanged();
    partial void OnSOLUONGTONKHOChanging(System.Nullable<int> value);
    partial void OnSOLUONGTONKHOChanged();
    partial void OnID_TINHTRANGChanging(string value);
    partial void OnID_TINHTRANGChanged();
    partial void OnTHOIHAN_BHChanging(string value);
    partial void OnTHOIHAN_BHChanged();
    partial void OnTHONGTINChanging(string value);
    partial void OnTHONGTINChanged();
    #endregion
		
		public SANPHAM()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_SP", DbType="VarChar(10) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string ID_SP
		{
			get
			{
				return this._ID_SP;
			}
			set
			{
				if ((this._ID_SP != value))
				{
					this.OnID_SPChanging(value);
					this.SendPropertyChanging();
					this._ID_SP = value;
					this.SendPropertyChanged("ID_SP");
					this.OnID_SPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_BRAND", DbType="VarChar(10)")]
		public string ID_BRAND
		{
			get
			{
				return this._ID_BRAND;
			}
			set
			{
				if ((this._ID_BRAND != value))
				{
					this.OnID_BRANDChanging(value);
					this.SendPropertyChanging();
					this._ID_BRAND = value;
					this.SendPropertyChanged("ID_BRAND");
					this.OnID_BRANDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_LOAI", DbType="VarChar(10)")]
		public string ID_LOAI
		{
			get
			{
				return this._ID_LOAI;
			}
			set
			{
				if ((this._ID_LOAI != value))
				{
					this.OnID_LOAIChanging(value);
					this.SendPropertyChanging();
					this._ID_LOAI = value;
					this.SendPropertyChanged("ID_LOAI");
					this.OnID_LOAIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TENSP", DbType="NVarChar(50)")]
		public string TENSP
		{
			get
			{
				return this._TENSP;
			}
			set
			{
				if ((this._TENSP != value))
				{
					this.OnTENSPChanging(value);
					this.SendPropertyChanging();
					this._TENSP = value;
					this.SendPropertyChanged("TENSP");
					this.OnTENSPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ANH_SP", DbType="VarChar(500)")]
		public string ANH_SP
		{
			get
			{
				return this._ANH_SP;
			}
			set
			{
				if ((this._ANH_SP != value))
				{
					this.OnANH_SPChanging(value);
					this.SendPropertyChanging();
					this._ANH_SP = value;
					this.SendPropertyChanged("ANH_SP");
					this.OnANH_SPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIATIEN", DbType="Float")]
		public System.Nullable<double> GIATIEN
		{
			get
			{
				return this._GIATIEN;
			}
			set
			{
				if ((this._GIATIEN != value))
				{
					this.OnGIATIENChanging(value);
					this.SendPropertyChanging();
					this._GIATIEN = value;
					this.SendPropertyChanged("GIATIEN");
					this.OnGIATIENChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DONVITINH", DbType="NVarChar(20)")]
		public string DONVITINH
		{
			get
			{
				return this._DONVITINH;
			}
			set
			{
				if ((this._DONVITINH != value))
				{
					this.OnDONVITINHChanging(value);
					this.SendPropertyChanging();
					this._DONVITINH = value;
					this.SendPropertyChanged("DONVITINH");
					this.OnDONVITINHChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SOLUONGTONKHO", DbType="Int")]
		public System.Nullable<int> SOLUONGTONKHO
		{
			get
			{
				return this._SOLUONGTONKHO;
			}
			set
			{
				if ((this._SOLUONGTONKHO != value))
				{
					this.OnSOLUONGTONKHOChanging(value);
					this.SendPropertyChanging();
					this._SOLUONGTONKHO = value;
					this.SendPropertyChanged("SOLUONGTONKHO");
					this.OnSOLUONGTONKHOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_TINHTRANG", DbType="VarChar(10)")]
		public string ID_TINHTRANG
		{
			get
			{
				return this._ID_TINHTRANG;
			}
			set
			{
				if ((this._ID_TINHTRANG != value))
				{
					this.OnID_TINHTRANGChanging(value);
					this.SendPropertyChanging();
					this._ID_TINHTRANG = value;
					this.SendPropertyChanged("ID_TINHTRANG");
					this.OnID_TINHTRANGChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_THOIHAN_BH", DbType="NVarChar(20)")]
		public string THOIHAN_BH
		{
			get
			{
				return this._THOIHAN_BH;
			}
			set
			{
				if ((this._THOIHAN_BH != value))
				{
					this.OnTHOIHAN_BHChanging(value);
					this.SendPropertyChanging();
					this._THOIHAN_BH = value;
					this.SendPropertyChanged("THOIHAN_BH");
					this.OnTHOIHAN_BHChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_THONGTIN", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string THONGTIN
		{
			get
			{
				return this._THONGTIN;
			}
			set
			{
				if ((this._THONGTIN != value))
				{
					this.OnTHONGTINChanging(value);
					this.SendPropertyChanging();
					this._THONGTIN = value;
					this.SendPropertyChanged("THONGTIN");
					this.OnTHONGTINChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
