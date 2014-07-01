namespace _bot

open System
open canopy
open canopy.core

module Bot =
    let baseURL = "http://gabrielecirulli.github.io/2048/"

    type Position = int * int
    type Value = int
    type Tile = Position * Value
    type Grid = int [][]
    type Move = Left | Right | Up | Down
    type Score = int * string

    let tileToGrid tiles :Grid =
        let defaultGrid = Array.init 4 (fun _ -> Array.init 4 (fun _ -> 0)) in
        tiles
        |> List.fold (fun acc ((x :int, y :int ), v :Value) -> acc.[y-1].SetValue(v,x-1)
                                                               acc ) defaultGrid
        
    let newTile v x y :Tile =
        ((x , y), v)

    let classToTile (html :String) =
        let pattern = html.Split [|' '|]
        let tileValue = pattern.[1]
        let tilePosition = pattern.[2]
        let value = (tileValue.Split [|'-'|]).[1]
        let position= (tilePosition.Split [|'-'|])
        let x = position.[2]
        let y = position.[3]
        newTile (int value) (int x) (int y)

    let htmlToTile html =
        html
        |> List.map (fun (t:OpenQA.Selenium.IWebElement) ->
                        classToTile (t.GetAttribute("class"))
                    )

    let rnd = new System.Random()
    let nextMove (grid :Grid) :Move = match rnd.Next(1, 5) with
                                        | 1 -> Left
                                        | 2 -> Right
                                        | 3 -> Up
                                        | 4 -> Down
                                        | _ -> Left

    let moveToCanopy (m :Move) = match m with
                                    | Left -> left
                                    | Right -> right
                                    | Up -> up
                                    | Down -> down

    let rec doPlay iter :Score =
      match (someElement ".game-over") with
      | Some (x)-> 
          quit()
          (iter, (element ".score-container").Text)
      | None ->
        let steps = element ".tile-container"
                         |> elementsWithin ".tile"
                         |> htmlToTile
                         |> tileToGrid
                        // |> Array.iter (fun x -> printf "%s" (Array.toList(x).ToString()))
                         |> nextMove
                         |> moveToCanopy
                         |> press
                         |> ignore in
         doPlay (iter+1)

    let play :Score =
        start firefox
        url baseURL
        doPlay 0
