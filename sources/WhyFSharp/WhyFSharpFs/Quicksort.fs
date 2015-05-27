module QuickSort

let rec quicksort list =
  match list with
  | [] -> []
  | firstElem::otherElements ->
    let smallerElements =
      otherElements
      |> List.filter (fun e -> e < firstElem)
      |> quicksort
    let largerElements =
      otherElements
      |> List.filter (fun e -> e >= firstElem)
      |> quicksort
    List.concat [smallerElements; [firstElem]; largerElements]

let rec quicksort2 = function
   | [] -> []                         
   | first::rest -> 
        let smaller,larger = List.partition ((>=) first) rest 
        List.concat [quicksort2 smaller; [first]; quicksort2 larger]