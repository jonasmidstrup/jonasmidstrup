module Mapping

open System
open System.Globalization
open Model

let toDateTime (s : string) =
    let parsed, dt = DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None)

    match parsed with
    | true -> Some(dt)
    | false -> None

let private mapVessel (dsc : Data.DatedSchedules.Row) =
    // Using records
    { VesselCode = dsc.VesselCode;
      ImoNumber = String.Empty;
      VesselName = dsc.VesselName;
      VesselOperatorCode = dsc.VesselOperatorCode;
      VesselFlag = dsc.VesselFlagCode;
      VesselCallSign = String.Empty; }

let private mapSchedule (dsc : Data.DatedSchedules.Row) =
    { ProformaArrival = dsc.ProformaArrivalDate;
      ProformaDeparture = dsc.ProformaDepartureDate;
      ScheduledArrival = dsc.EstimatedArrivalDate;
      ScheduledDeparture = dsc.EstimatedDepartureDate; }

let private mapActual (dsc : Data.DatedSchedules.Row) =
    // Using tuples
    let actual = (dsc.ActualArrivalDate |> toDateTime, dsc.ActualDepartureDate |> toDateTime)

    // Match and deconstructing tuples
    match actual with
    | (arrival, departure) when arrival.IsNone && departure.IsNone ->
        None
    | (arrival, departure) ->
        Some({ ActualArrival = arrival; ActualDeparture = departure })

let private mapCurrentPortCall (dsc : Data.DatedSchedules.Row) =
    { CityCode = dsc.CityCode;
      CityName = dsc.City; }

let private mapArrivalVoyage (dsc : Data.DatedSchedules.Row) =
    { VoyageNumber = dsc.ArrivalVoyageCode;
      TransportMode = dsc.ArrivalVoyageTransportMode; }

let private mapDepartureVoyage (dsc : Data.DatedSchedules.Row) =
    { VoyageNumber = dsc.DepartureVoyageCode;
      TransportMode = dsc.DepartureVoyageTransportMode; }

let private mapService (dsc : Data.DatedSchedules.Row) =
    { Code = dsc.ArrivalServiceCode; }

let mapCsvToDomainModel (dsc : Data.DatedSchedules.Row) = 
    let utcNow = DateTimeOffset.UtcNow

    let datedSchedule = { Id = dsc.GsisKey |> System.Convert.ToString;
                          Timestamp = utcNow;
                          Version = utcNow.ToUnixTimeMilliseconds();
                          Vessel = dsc |> mapVessel;
                          Schedule = dsc |> mapSchedule;
                          Actual = dsc |> mapActual;
                          CurrentPortCall = dsc |> mapCurrentPortCall;
                          ArrivalVoyage = dsc |> mapArrivalVoyage;
                          DepartureVoyage = dsc |> mapDepartureVoyage;
                          Service = dsc |> mapService; }
    datedSchedule