# Introduction to F#

```F#
// main
    try
        let datedSchedules =
		argv.[0] |> Data.DatedSchedules.Load

        let models = 
            datedSchedules.Rows
                |> Seq.map Mapping.mapCsvToDomainModel
                // choose actuals?
                |> Seq.toList // to immutable list

        printfn "We mapped %i rows of data" models.Length

        // json?

        let avgDeviation = 
            datedSchedules.Rows
                |> Seq.map Mapping.mapCsvToDomainModel
                |> Seq.averageBy (fun x -> Calculations.getDifference x.Schedule.ScheduledDeparture x.Schedule.ProformaDeparture)

        printfn "Average deviation of departures is %f" avgDeviation

        // Different sources

        let portCalls =
            "https://api.maerskline.com/maeu/schedules/port?portGeoId=02R2IYUF20M7Z&fromDate=2021-01-01&toDate=2021-01-04"
            |> Data.PortCallsApi.Load

        let avgPortTime = 
            portCalls.Vessels
            |> Seq.averageBy (fun x -> Calculations.getDifference x.Departure.DateTime x.Arrival.DateTime)
            |> TimeSpan.FromMilliseconds

        0 // return an integer exit code
    with
        | :? System.Exception as ex ->
            eprintfn "%s" ex.Message
            1


// Calculation
open System

let getDifference (dt1 : DateTime) (dt2 : DateTime) =
    let diff = dt1 - dt2
    diff.TotalMilliseconds


// Loading port calls from URI with sample
type PortCallsApi =
    JsonProvider<
        "C:/Users/JMI154/Downloads/vessels_sample.json", SampleIsList=true>


// Recursive functions
let rec fib n =
    match n with
    | 0 | 1 -> n
    | n -> fib (n-1) + fib (n-2)


// F# Interactive
let square x = x *  x;;

square 12;;

printfn "Hello, FSI!"
- ;;
```

### Useful links

* https://docs.microsoft.com/en-us/dotnet/fsharp/
* https://fsharpforfunandprofit.com/
