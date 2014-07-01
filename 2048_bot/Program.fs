open canopy
open System
open _bot


[<EntryPoint>]
let main args =
    let iter, score = Bot.play
    printfn "Bot scored %s in %d moves" score iter
    printfn "press [enter] to exit"
    System.Console.ReadLine() |> ignore
    0
