namespace _bot
module Bot =
    type Grid = int [][]
    type Move = Left | Right | Up | Down
    type Score = int * string
    val play : (Grid -> Move) -> Score
  
