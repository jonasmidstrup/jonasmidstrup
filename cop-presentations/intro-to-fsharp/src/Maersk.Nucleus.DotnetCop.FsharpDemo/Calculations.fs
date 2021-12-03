module Calculations

open System

let getDifference (dt1 : DateTime) (dt2 : DateTime) =
    let diff = dt1 - dt2
    diff.TotalMilliseconds