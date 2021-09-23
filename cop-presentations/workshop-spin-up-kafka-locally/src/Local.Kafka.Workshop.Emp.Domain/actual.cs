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
	
	public partial class actual : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse(@"{""type"":""record"",""name"":""actual"",""namespace"":""emp.maersk.com"",""fields"":[{""name"":""actualArrival"",""type"":""string""},{""name"":""actualDeparture"",""type"":""string""},{""name"":""arrivalAtPilotStation"",""type"":""string""},{""name"":""firstPilotOnBoard"",""type"":""string""},{""name"":""pilotOff"",""type"":""string""}],""connect.name"":""emp.maersk.com.actual""}");
		private string _actualArrival;
		private string _actualDeparture;
		private string _arrivalAtPilotStation;
		private string _firstPilotOnBoard;
		private string _pilotOff;
		public virtual Schema Schema
		{
			get
			{
				return actual._SCHEMA;
			}
		}
		public string actualArrival
		{
			get
			{
				return this._actualArrival;
			}
			set
			{
				this._actualArrival = value;
			}
		}
		public string actualDeparture
		{
			get
			{
				return this._actualDeparture;
			}
			set
			{
				this._actualDeparture = value;
			}
		}
		public string arrivalAtPilotStation
		{
			get
			{
				return this._arrivalAtPilotStation;
			}
			set
			{
				this._arrivalAtPilotStation = value;
			}
		}
		public string firstPilotOnBoard
		{
			get
			{
				return this._firstPilotOnBoard;
			}
			set
			{
				this._firstPilotOnBoard = value;
			}
		}
		public string pilotOff
		{
			get
			{
				return this._pilotOff;
			}
			set
			{
				this._pilotOff = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.actualArrival;
			case 1: return this.actualDeparture;
			case 2: return this.arrivalAtPilotStation;
			case 3: return this.firstPilotOnBoard;
			case 4: return this.pilotOff;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.actualArrival = (System.String)fieldValue; break;
			case 1: this.actualDeparture = (System.String)fieldValue; break;
			case 2: this.arrivalAtPilotStation = (System.String)fieldValue; break;
			case 3: this.firstPilotOnBoard = (System.String)fieldValue; break;
			case 4: this.pilotOff = (System.String)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
