module Tests

open NUnit.Framework
open Swensen.Unquote

open QuickSort

[<Test>]
let ``test quicksort``() =
  test <@ quicksort [1;5;23;18;9;1;3] = [1; 1; 3; 5; 9; 18; 23] @>

[<Test>]
let ``test quicksort2``() =
  test <@ quicksort2 [1;5;23;18;9;1;3] = [1; 1; 3; 5; 9; 18; 23] @>
