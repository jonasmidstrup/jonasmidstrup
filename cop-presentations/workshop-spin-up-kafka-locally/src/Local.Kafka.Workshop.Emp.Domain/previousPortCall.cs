// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.10.0.0
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace emp.maersk.com
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Avro;
	using Avro.Specific;
	
	public partial class previousPortCall : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse(@"{""type"":""record"",""name"":""previousPortCall"",""namespace"":""emp.maersk.com"",""fields"":[{""name"":""cityCode"",""type"":""string""},{""name"":""terminalCode"",""type"":""string""},{""name"":""cityName"",""type"":""string""},{""name"":""terminalName"",""type"":""string""},{""name"":""cityGeoCode"",""type"":""string""},{""name"":""terminalGeoCode"",""type"":""string""},{""name"":""previousScheduleEntryKey"",""type"":""string""},{""name"":""arrivalVoyage"",""type"":""string""},{""name"":""departureVoyage"",""type"":""string""}],""connect.name"":""emp.maersk.com.previousPortCall""}");
		private string _cityCode;
		private string _terminalCode;
		private string _cityName;
		private string _terminalName;
		private string _cityGeoCode;
		private string _terminalGeoCode;
		private string _previousScheduleEntryKey;
		private string _arrivalVoyage;
		private string _departureVoyage;
		public virtual Schema Schema
		{
			get
			{
				return previousPortCall._SCHEMA;
			}
		}
		public string cityCode
		{
			get
			{
				return this._cityCode;
			}
			set
			{
				this._cityCode = value;
			}
		}
		public string terminalCode
		{
			get
			{
				return this._terminalCode;
			}
			set
			{
				this._terminalCode = value;
			}
		}
		public string cityName
		{
			get
			{
				return this._cityName;
			}
			set
			{
				this._cityName = value;
			}
		}
		public string terminalName
		{
			get
			{
				return this._terminalName;
			}
			set
			{
				this._terminalName = value;
			}
		}
		public string cityGeoCode
		{
			get
			{
				return this._cityGeoCode;
			}
			set
			{
				this._cityGeoCode = value;
			}
		}
		public string terminalGeoCode
		{
			get
			{
				return this._terminalGeoCode;
			}
			set
			{
				this._terminalGeoCode = value;
			}
		}
		public string previousScheduleEntryKey
		{
			get
			{
				return this._previousScheduleEntryKey;
			}
			set
			{
				this._previousScheduleEntryKey = value;
			}
		}
		public string arrivalVoyage
		{
			get
			{
				return this._arrivalVoyage;
			}
			set
			{
				this._arrivalVoyage = value;
			}
		}
		public string departureVoyage
		{
			get
			{
				return this._departureVoyage;
			}
			set
			{
				this._departureVoyage = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.cityCode;
			case 1: return this.terminalCode;
			case 2: return this.cityName;
			case 3: return this.terminalName;
			case 4: return this.cityGeoCode;
			case 5: return this.terminalGeoCode;
			case 6: return this.previousScheduleEntryKey;
			case 7: return this.arrivalVoyage;
			case 8: return this.departureVoyage;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.cityCode = (System.String)fieldValue; break;
			case 1: this.terminalCode = (System.String)fieldValue; break;
			case 2: this.cityName = (System.String)fieldValue; break;
			case 3: this.terminalName = (System.String)fieldValue; break;
			case 4: this.cityGeoCode = (System.String)fieldValue; break;
			case 5: this.terminalGeoCode = (System.String)fieldValue; break;
			case 6: this.previousScheduleEntryKey = (System.String)fieldValue; break;
			case 7: this.arrivalVoyage = (System.String)fieldValue; break;
			case 8: this.departureVoyage = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
