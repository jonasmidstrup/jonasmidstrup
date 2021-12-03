module Model

open System

type Vessel = {
    VesselCode: string;
    ImoNumber: string;
    VesselName: string;
    VesselOperatorCode: string;
    VesselFlag: string;
    VesselCallSign: string; }

type Schedule = {
    ProformaArrival: DateTime;
    ProformaDeparture: DateTime;
    ScheduledArrival: DateTime;
    ScheduledDeparture: DateTime; }

    // using options
type Actual = {
    ActualArrival: DateTime option;
    ActualDeparture: DateTime option; }

type PortCall = {
    CityCode: string;
    CityName: string; }

type Voyage = {
    VoyageNumber: string;
    TransportMode: string; }

type Service = {
    Code: string; }

type DatedSchedule = {
    Id: string;
    Timestamp: DateTimeOffset; 
    Version: int64; 
    Vessel: Vessel;
    Schedule: Schedule;
    Actual: Actual option;
    CurrentPortCall: PortCall;
    ArrivalVoyage: Voyage;
    DepartureVoyage: Voyage;
    Service: Service; }