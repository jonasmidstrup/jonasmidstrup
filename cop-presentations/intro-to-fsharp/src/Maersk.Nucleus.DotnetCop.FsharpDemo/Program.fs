open Newtonsoft.Json


// Learn more about F# at http://fsharp.org

[<EntryPoint>]
let main argv =
    try
        let datedSchedules =
            argv.[0] |> Data.DatedSchedules.Load

        let models = 
            datedSchedules.Rows
                |> Seq.map Mapping.mapCsvToDomainModel
                // choose actuals?
                |> Seq.toList // to immutable list

        printfn "We mapped %i rows of data" models.Length

        let json = models |> JsonConvert.SerializeObject

        let avgDeviation = 
            datedSchedules.Rows
                |> Seq.map Mapping.mapCsvToDomainModel
                |> Seq.averageBy (fun x -> Calculations.getDifference x.Schedule.ScheduledDeparture x.Schedule.ProformaDeparture)

        printfn "Average deviation of departures is %f" avgDeviation

        0
    with
        | :? System.Exception as ex ->
            eprintfn "%s" ex.Message
            1