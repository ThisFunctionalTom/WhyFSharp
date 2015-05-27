(**
- title : Why F#
- description : Blog posts von Scott Wlaschin
- author : Tomas Leko
- theme : league
- transition : default

***

### Why F#

***

### F# syntax
#### Basics
' 1. "Variables" (but not really)
' 2. The "let" keyword defines an (immutable) value
' 3. note that no types needed
*)
// single line comments use a double slash
(* multi line comments use (* . . . *) pair

-end of multi line comment- *)

let myInt = 5
let myFloat = 3.14
let myString = "hello"

(**
---
#### Lists
' 1. Square brackets create a list with semicolon delimiters.
' 2. :: creates list with new 1st element
' 3. @ concats two lists
' 4. IMPORTANT: commas are never used as delimiters, only semicolons!
*)
let twoToFive = [2;3;4;5]

let oneToFive = 1 :: twoToFive
(*** include-value: oneToFive ***)
let zeroToFive = [0;1] @ twoToFive
(*** include-value: twoToFive ***)

(**
---
#### Functions
' 1. The "let" keyword also defines a named function.
' 2. Note that no parens are used.
' 3. don't use add (x,y)! It means something completely different.
*)
let square x = x * x

square 3
(*** include-value: square 3 ***)
let add x y = x + y

add 2 3
(*** include-value: add 2 3 ***)

(**
---
#### Functions
' 1. to define a multiline function, just use indents. No semicolons needed.
' 2. Define "isEven" as a sub function
' 3. List.filter is a library function with two parameters: a boolean function and a list to work on
*)
let evens list =
   let isEven x = x%2 = 0
   List.filter isEven list

evens oneToFive
(*** include-value: evens oneToFive ***)
(**
---
#### Pipe
' 1. You can pipe the output of one operation to the next using "|>"
' 2. Here is the same sumOfSquares function written using pipes "square" was defined earlier
' 3. you can define lambdas (anonymous functions) using the "fun" keyword
' 4. In F# there is no "return" keyword. A function always returns the value of the last expression used.
*)
let sumOfSquaresTo100piped =
   [1..100] |> List.map square |> List.sum

let sumOfSquaresTo100withFun =
   [1..100] |> List.map (fun x->x*x) |> List.sum
(**
---
#### Pattern matching
' 1. Match..with.. is a supercharged case/switch statement.
' 2. underscore matches anything
*)

let simplePatternMatch =
   let x = "a"
   match x with
    | "a" -> printfn "x is a"
    | "b" -> printfn "x is b"
    | _ -> printfn "x is something else"
(**
---
#### Pattern matching
' 1. Some(..) and None are roughly analogous to Nullable wrappers
' 2. In this example, match..with matches the "Some" and the "None", and also unpacks the value in the "Some" at the same time.
*)

let validValue = Some(99)
let invalidValue = None

let optionPatternMatch input =
   match input with
    | Some i -> printfn "input is an int=%d" i
    | None -> printfn "input is missing"

(*** define-output: validValue ***)
optionPatternMatch validValue
(*** include-output: validValue ***)
(*** define-output: invalidValue ***)
optionPatternMatch invalidValue
(*** include-output: invalidValue ***)
(**
---
#### Complex data types
' 1. tuples are quick 'n easy anonymous types
' 2. record types have named fields
' 3. union types have choices
*)
(**Tuples*)
let twoTuple = 1,2
let threeTuple = "a",2,true
(**Records*)
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="Doe"}
(**Discriminated unions*)
type Temp =
    | DegreesC of float
    | DegreesF of float
let temp = DegreesF 98.6
(**
---
#### Complex data types
' 1. types can be combined recursively in complex ways
*)
type Employee =
  | Worker of Person
  | Manager of Employee list
let jdoe = {First="John";Last="Doe"}
let worker = Worker jdoe
(*** include-value: worker ***)
(**
---
#### Printing
' 1. The printf/printfn functions are similar to the Console.Write/WriteLine functions in C#.
' 2. all complex types have pretty printing built in
' 3. There are also sprintf/sprintfn functions for formatting data into a string, similar to String.Format.
*)
(*** define-output: print1 ***)
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]
(*** include-output: print1 ***)

(*** define-output: print2 ***)
printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A"
         twoTuple person1 temp worker
(*** include-output: print2 ***)
(**

***

### Comparing C# with F#
#### Sum of squares - F#
*)
// define the square function
let square x = x * x

// define the sumOfSquares function
let sumOfSquares n =
   [1..n]
   |> List.map square
   |> List.sum

sumOfSquares 100
(**

---
#### Sum of squares - C#

    [lang=cs]
    public static class SumOfSquaresHelper {
       public static int Square(int i) {
          return i * i;
       }

       public static int SumOfSquares(int n) {
          int sum = 0;
          for (int i = 1; i <= n; i++) {
             sum += Square(i);
          }
          return sum;
       }
    }

---

#### Sum of squares - C# LINQ

    [lang=cs]
    public static class FunctionalSumOfSquaresHelper
    {
       public static int SumOfSquares(int n)
       {
          return Enumerable.Range(1, n)
             .Select(i => i * i)
             .Sum();
       }
    }

---

#### Sum of squares
- The F# code is more compact
- The F# code didn't have any type declarations
- F# can be developed interactively

***

### Comparing C# with F#
#### Quick sort

<ul>
  <li>If the list is empty, there is nothing to do.</li>
  <li>Otherwise:
  <ol>
    <li>Take the first element of the list</li>
    <li>Find all elements in the rest of the list that are less than the first element, and sort them.</li>
    <li>Find all elements in the rest of the list that are >= than the first element, and sort them</li>
    <li>Combine the three parts together to get the final result: (sorted smaller elements + firstElement + sorted larger elements)</li>
  </ol>
  </li>
</ul>

---

#### Quick sort - F#

<div class='smaller'>
*)

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

(*** define-output: quicksort ***)
printfn "%A" (quicksort [1;5;23;18;9;1;3])
(*** include-output: quicksort ***)
(**
</div>

---

#### Quick sort - C#

<div class="smaller">

    [lang=cs]
    public class QuickSortHelper {
       public static List<T> QuickSort<T>(List<T> values) where T : IComparable {
          if (values.Count == 0) {
             return new List<T>();
          }

          T firstElement = values[0];

          var smallerElements = new List<T>();
          var largerElements = new List<T>();
          for (int i = 1; i < values.Count; i++) {
             var elem = values[i];
             if (elem.CompareTo(firstElement) < 0) {
                smallerElements.Add(elem);
             }
             else {
                largerElements.Add(elem);
             }
          }

          var result = new List<T>();
          result.AddRange(QuickSort(smallerElements.ToList()));
          result.Add(firstElement);
          result.AddRange(QuickSort(largerElements.ToList()));
          return result;
       }
    }

</div>

---

#### Quick sort - C# LINQ
<div class="smaller">

    [lang=cs]
    public class QuickSortHelper2 {
      public static List<T> QuickSort<T>(List<T> values) where T : IComparable {
        if (!values.Any()) {
          return new List<T>();
        }

        var x = values.First();
        var xs = values.Skip(1);
        var smaller = xs.Where(a => a.CompareTo(x) < 0).ToList();
        var larger = xs.Where(a => a.CompareTo(x) >= 0).ToList();

        return QuickSort(smaller)
          .Concat(new[] { x })
          .Concat(QuickSort(larger))
          .ToList();
      }
    }

</div>

---

#### Quick sort - F#

<div class="smaller">
*)
let rec quicksort2 = function
   | [] -> []
   | first::rest ->
        let smaller,larger = List.partition ((>=) first) rest
        List.concat [quicksort2 smaller; [first]; quicksort2 larger]
(**
</div>

***

### Comparing C# with F#
#### Downloading a web page - F#

' "open" brings a .NET namespace into visibility
*)
open System.Net
open System
open System.IO

let fetchUrl callback url =
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    callback reader url

(**

---

#### Downloading a web page - C#

<div class='smaller'>

    [lang=cs]
    class WebPageDownloader {
      public TResult FetchUrl<TResult>(
        string url,
        Func<string, StreamReader, TResult> callback) {

        var req = WebRequest.Create(url);
        using (var resp = req.GetResponse()) {
          using (var stream = resp.GetResponseStream()) {
            using (var reader = new StreamReader(stream)) {
                return callback(url, reader);
            }
          }
        }
      }
    }

</div>

---

#### Downloading a web page - Testing

*)
let myCallback (reader:IO.StreamReader) url =
    let html = reader.ReadToEnd()
    let html100 = html.Substring(0, 100)
    printfn "Downloaded %s. First 100 is\n%s" url html100
    html

(*** define-output: fetchUrl1 ***)
let google = fetchUrl myCallback "http://google.com"
(*** include-output: fetchUrl1 ***)

(**

---

#### Downloading a web page - Testing

*)


// build a function with the callback "baked in"
let fetchUrl2 = fetchUrl myCallback

(*** define-output: fetchUrl2 ***)
let google = fetchUrl2 "http://www.google.com"
let bbc    = fetchUrl2 "http://news.bbc.co.uk"
(*** include-output: fetchUrl2 ***)

(**

---

#### Downloading a web page - Testing

*)

// test with a list of sites
let sites = ["http://www.bing.com";
             "http://www.google.com";
             "http://www.yahoo.com"]

// process each site in the list
sites |> List.map fetchUrl2

(**

---

#### Downloading a web page - Testing in C#

<div class='smaller'>

    [lang=cs]
    [Test]
    public void TestFetchUrlWithCallback() {
        Func<string, StreamReader, string> myCallback = (url, reader) => {
            var html = reader.ReadToEnd();
            var html1000 = html.Substring(0, 1000);
            Console.WriteLine(
                "Downloaded {0}. First 1000 is {1}", url,
                html1000);
            return html;
        };

        var downloader = new WebPageDownloader();
        var google = downloader.FetchUrl("http://www.google.com", myCallback);

        var sites = new List<string> {
            "http://www.bing.com",
            "http://www.google.com",
            "http://www.yahoo.com"};

        sites.ForEach(site => downloader.FetchUrl(site, myCallback));
    }

</div>

' noisier
' no partial application

***

### Comparing C# with F#
#### Max by

Get an element from a list with maximum 'Size' property.

----

#### Max by - C#

<div class='smaller'>

    [lang=cs]
    public class NameAndSize {
      public string Name;
      public int Size;
    }

    public static NameAndSize MaxNameAndSize(IList<NameAndSize> list) {
      if (list.Count() == 0) {
        return default(NameAndSize);
      }

      var maxSoFar = list[0];
      foreach (var item in list) {
        if (item.Size > maxSoFar.Size) {
          maxSoFar = item;
        }
      }
      return maxSoFar;
    }

</div>

---

#### Max by - C# LINQ

<div class='smaller'>

    [lang=cs]
    public class NameAndSize {
        public string Name;
        public int Size;
    }

    public static NameAndSize MaxNameAndSize(IList<NameAndSize> list) {
        if (!list.Any()) {
            return default(NameAndSize);
        }

        var initialValue = list[0];
        Func<NameAndSize, NameAndSize, NameAndSize> action =
            (maxSoFar, x) => x.Size > maxSoFar.Size ? x : maxSoFar;
        return list.Aggregate(initialValue, action);
    }

</div>

---

<div class='smaller'>

#### Max by - F#

*)

type NameAndSize= { Name:string; Size:int }

let maxNameAndSize list =
    let innerMaxNameAndSize initialValue rest =
        let action maxSoFar x =
          if maxSoFar.Size < x.Size then x else maxSoFar
        rest |> List.fold action initialValue

    // handle empty lists
    match list with
    | [] -> None
    | first::rest ->
        let max = innerMaxNameAndSize first rest
        Some max

(**
</div>

---

#### Max by - F# with List.maxBy

*)

let maxNameAndSize2 list =
  list |> List.maxBy (fun item -> item.Size)

(**

***

### Four key concepts

![Four key concepts](images/four_key_concepts.png)

---

#### Function-oriented

*)
// building blocks
let add2 x = x + 2
let mult3 x = x * 3
let square x = x * x

// new composed functions
let add2ThenMult3 = add2 >> mult3
let mult3ThenSquare = mult3 >> square

(**

---

#### Function-oriented

*)

// helper functions;
let logMsg msg x = printf "%s%i" msg x; x     //without linefeed
let logMsgN msg x = printfn "%s%i" msg x; x   //with linefeed

// new composed function with new improved logging!
let mult3ThenSquareLogged =
   logMsg "before="
   >> mult3
   >> logMsg " after mult3="
   >> square
   >> logMsgN " result="

(*** define-output: log1 ***)
mult3ThenSquareLogged 5
(*** include-output: log1 ***)

(**

---

#### Expression rather then statements

    [lang=cs]
    // statement-based code in C#
    int result;
    if (aBool) {
      result = 42;
    }
    Console.WriteLine("result={0}", result);

- variable must be defined outside the statement
- but assigned to from inside the statement,
- What initial value should result be set to?
- What if I forget to assign to the result variable?
- What is the value of the result variable in the "else" case?

---

#### Expression rather then statements

    [lang=cs]
    // expression-based code in C#
    int result = (aBool) ? 42 : 0;
    Console.WriteLine("result={0}", result);

- The result variable is declared at the same time that it is assigned. No variables have to be set up "outside" the expression and there is no worry about what initial value they should be set to.
- The "else" is explicitly handled. There is no chance of forgetting to do an assignment in one of the branches.
- It is not possible to forget to assign result, because then the variable would not even exist!

---

#### Algerbraic types

*)
// product type
type IntAndBool = {intPart: int; boolPart: bool}

let x = {intPart=1; boolPart=false}

// sum type
type IntOrBool =
    | IntChoice of int
    | BoolChoice of bool

let y = IntChoice 42
let z = BoolChoice true

(**

---

#### Algerbraic types

<div class='smaller'>
*)
type PersonalName = {FirstName:string; LastName:string}

type StreetAddress = {Line1:string; Line2:string; Line3:string }

type ZipCode =  ZipCode of string
type StateAbbrev =  StateAbbrev of string
type ZipAndState =  {State:StateAbbrev; Zip:ZipCode }
type USAddress = {Street:StreetAddress; Region:ZipAndState}

type UKPostCode =  PostCode of string
type UKAddress = {Street:StreetAddress; Region:UKPostCode}

type InternationalAddress = {
    Street:StreetAddress; Region:string; CountryName:string}

type Address = USAddress | UKAddress | InternationalAddress

type Email = Email of string

type CountryPrefix = Prefix of int
type Phone = {CountryPrefix:CountryPrefix; LocalNumber:string}

type Contact = {
  PersonalName: PersonalName;
  Address: Address option;
  Email: Email option;
  Phone: Phone option;
}

(**
</div>

---

#### Pattern matching for flow of control

*)
type Shape =
| Circle of int
| Rectangle of int * int
| Polygon of (int * int) list
| Point of (int * int)

let draw shape =
  match shape with
  | Circle radius ->
      printfn "The circle has a radius of %d" radius
  | Rectangle (height,width) ->
      printfn "The rectangle is %d high by %d wide" height width
  | Polygon points ->
      printfn "The polygon is made of these points %A" points
  | _ -> printfn "I don't recognize this shape"

(**

---

#### Pattern matching for flow of control

*)

let circle = Circle(10)
let rect = Rectangle(4,5)
let polygon = Polygon( [(1,1); (2,2); (3,3)])
let point = Point(2,3)

(*** define-output: pattern1 ***)
[circle; rect; polygon; point] |> List.iter draw
(*** include-output: pattern1 ***)
