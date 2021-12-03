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
	
	public partial class scheduleEntryID : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"scheduleEntryID\",\"namespace\":\"emp.maersk.com\",\"fields\":[" +
				"{\"name\":\"scheduleEntryKey\",\"type\":\"string\"},{\"name\":\"scheduleEntryIdentifier\",\"t" +
				"ype\":{\"type\":\"record\",\"name\":\"scheduleEntryIdentifier\",\"namespace\":\"emp.maersk.c" +
				"om\",\"fields\":[{\"name\":\"vessel\",\"type\":{\"type\":\"record\",\"name\":\"vessel\",\"namespac" +
				"e\":\"emp.maersk.com\",\"fields\":[{\"name\":\"vesselCode\",\"type\":\"string\"},{\"name\":\"IMO" +
				"Number\",\"type\":\"string\"},{\"name\":\"vesselName\",\"type\":\"string\"},{\"name\":\"vesselOp" +
				"eratorCode\",\"type\":\"string\"},{\"name\":\"vesselFlag\",\"type\":\"string\"},{\"name\":\"vess" +
				"elCallSign\",\"type\":\"string\"}],\"connect.name\":\"emp.maersk.com.vessel\"}},{\"name\":\"" +
				"arrivalVoyage\",\"type\":{\"type\":\"record\",\"name\":\"arrivalVoyage\",\"namespace\":\"emp.m" +
				"aersk.com\",\"fields\":[{\"name\":\"voyage\",\"type\":\"string\"},{\"name\":\"direction\",\"type" +
				"\":\"string\"}],\"connect.name\":\"emp.maersk.com.arrivalVoyage\"}},{\"name\":\"departureV" +
				"oyage\",\"type\":{\"type\":\"record\",\"name\":\"departureVoyage\",\"namespace\":\"emp.maersk." +
				"com\",\"fields\":[{\"name\":\"voyage\",\"type\":\"string\"},{\"name\":\"direction\",\"type\":\"str" +
				"ing\"}],\"connect.name\":\"emp.maersk.com.departureVoyage\"}},{\"name\":\"arrivalService" +
				"\",\"type\":{\"type\":\"record\",\"name\":\"arrivalService\",\"namespace\":\"emp.maersk.com\",\"" +
				"fields\":[{\"name\":\"code\",\"type\":\"string\"},{\"name\":\"name\",\"type\":\"string\"},{\"name\"" +
				":\"transportMode\",\"type\":\"string\"}],\"connect.name\":\"emp.maersk.com.arrivalService" +
				"\"}},{\"name\":\"departureService\",\"type\":{\"type\":\"record\",\"name\":\"departureService\"" +
				",\"namespace\":\"emp.maersk.com\",\"fields\":[{\"name\":\"code\",\"type\":\"string\"},{\"name\":" +
				"\"name\",\"type\":\"string\"},{\"name\":\"transportMode\",\"type\":\"string\"}],\"connect.name\"" +
				":\"emp.maersk.com.departureService\"}},{\"name\":\"previousPortCall\",\"type\":{\"type\":\"" +
				"record\",\"name\":\"previousPortCall\",\"namespace\":\"emp.maersk.com\",\"fields\":[{\"name\"" +
				":\"cityCode\",\"type\":\"string\"},{\"name\":\"terminalCode\",\"type\":\"string\"},{\"name\":\"ci" +
				"tyName\",\"type\":\"string\"},{\"name\":\"terminalName\",\"type\":\"string\"},{\"name\":\"cityGe" +
				"oCode\",\"type\":\"string\"},{\"name\":\"terminalGeoCode\",\"type\":\"string\"},{\"name\":\"prev" +
				"iousScheduleEntryKey\",\"type\":\"string\"},{\"name\":\"arrivalVoyage\",\"type\":\"string\"}," +
				"{\"name\":\"departureVoyage\",\"type\":\"string\"}],\"connect.name\":\"emp.maersk.com.previ" +
				"ousPortCall\"}},{\"name\":\"currentPortCall\",\"type\":{\"type\":\"record\",\"name\":\"current" +
				"PortCall\",\"namespace\":\"emp.maersk.com\",\"fields\":[{\"name\":\"cityCode\",\"type\":\"stri" +
				"ng\"},{\"name\":\"terminalCode\",\"type\":\"string\"},{\"name\":\"cityName\",\"type\":\"string\"}" +
				",{\"name\":\"terminalName\",\"type\":\"string\"},{\"name\":\"cityGeoCode\",\"type\":\"string\"}," +
				"{\"name\":\"terminalGeoCode\",\"type\":\"string\"}],\"connect.name\":\"emp.maersk.com.curre" +
				"ntPortCall\"}},{\"name\":\"nextPortCall\",\"type\":{\"type\":\"record\",\"name\":\"nextPortCal" +
				"l\",\"namespace\":\"emp.maersk.com\",\"fields\":[{\"name\":\"cityCode\",\"type\":\"string\"},{\"" +
				"name\":\"terminalCode\",\"type\":\"string\"},{\"name\":\"cityName\",\"type\":\"string\"},{\"name" +
				"\":\"terminalName\",\"type\":\"string\"},{\"name\":\"cityGeoCode\",\"type\":\"string\"},{\"name\"" +
				":\"terminalGeoCode\",\"type\":\"string\"},{\"name\":\"nextScheduleEntryKey\",\"type\":\"strin" +
				"g\"},{\"name\":\"arrivalVoyage\",\"type\":\"string\"},{\"name\":\"departureVoyage\",\"type\":\"s" +
				"tring\"}],\"connect.name\":\"emp.maersk.com.nextPortCall\"}}],\"connect.name\":\"emp.mae" +
				"rsk.com.scheduleEntryIdentifier\"}}],\"connect.name\":\"emp.maersk.com.scheduleEntry" +
				"ID\"}");
		private string _scheduleEntryKey;
		private emp.maersk.com.scheduleEntryIdentifier _scheduleEntryIdentifier;
		public virtual Schema Schema
		{
			get
			{
				return scheduleEntryID._SCHEMA;
			}
		}
		public string scheduleEntryKey
		{
			get
			{
				return this._scheduleEntryKey;
			}
			set
			{
				this._scheduleEntryKey = value;
			}
		}
		public emp.maersk.com.scheduleEntryIdentifier scheduleEntryIdentifier
		{
			get
			{
				return this._scheduleEntryIdentifier;
			}
			set
			{
				this._scheduleEntryIdentifier = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.scheduleEntryKey;
			case 1: return this.scheduleEntryIdentifier;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.scheduleEntryKey = (System.String)fieldValue; break;
			case 1: this.scheduleEntryIdentifier = (emp.maersk.com.scheduleEntryIdentifier)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
