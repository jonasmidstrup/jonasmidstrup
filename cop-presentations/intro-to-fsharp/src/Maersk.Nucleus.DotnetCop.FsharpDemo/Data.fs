module Data

open FSharp.Data

type DatedSchedules = 
    CsvProvider<
        "C:\Users\JMI154\Downloads\datedschedules_sample.csv",
        Separators=",",
        Quote=''',
        HasHeaders=true,
        IgnoreErrors=true>
