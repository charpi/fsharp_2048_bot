module Charpi

open System
open _bot

let rnd = new System.Random()
let random (grid :Bot.Grid) = match rnd.Next(1, 5) with
                                | 1 -> Bot.Left
                                | 2 -> Bot.Right
                                | 3 -> Bot.Up
                                | 4 -> Bot.Down
                                | _ -> Bot.Left

[<EntryPoint>]
let main args =
    let iter, score = Bot.play random
    printfn "Bot scored %s in %d moves" score iter
    printfn "press [enter] to exit"
    System.Console.ReadLine() |> ignore
    0
