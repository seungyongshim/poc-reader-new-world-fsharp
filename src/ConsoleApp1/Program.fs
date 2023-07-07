// https://fsharpforfunandprofit.com/posts/elevated-world-6/
// designing your own elevated world


//  Open API connection
//  Get product ids purchased by customer id using the API
//  For each product id:
//      get the product info for that id using the API
//  Close API connection
//  Return the list of product infos



type CustomerId = CustomerId of string
type ProductId = ProductId of string
type ProductInfo = {
    ProductName: string;
}

type ApiClient() =
    static let mutable data = Map.empty<string, obj>

    /// Return Success of the value or Failure on failure
    member private this.TryCast<'a> key (value:obj) =
        match value with
        | :? 'a as a -> Ok a
        | _  ->
            let typeName = typeof<'a>.Name
            Error [sprintf "Can't cast value at %s to %s" key typeName]

    /// Get a value
    member this.Get<'a> (id:obj) =
        let key =  sprintf "%A" id
        printfn "[API] Get %s" key
        match Map.tryFind key data with
        | Some o ->
            this.TryCast<'a> key o
        | None ->
            Error [sprintf "Key %s not found" key]

    /// Set a value
    member this.Set (id:obj) (value:obj) =
        let key =  sprintf "%A" id
        printfn "[API] Set %s" key
        if key = "bad" then  // for testing failure paths
            Error [sprintf "Bad Key %s " key]
        else
            data <- Map.add key value data
            Ok ()

    member this.Open() =
        printfn "[API] Opening"

    member this.Close() =
        printfn "[API] Closing"

    interface System.IDisposable with
        member this.Dispose() =
            printfn "[API] Disposing"

do
use api = new ApiClient()

api.Get "K1" |> printfn "[K1] %A"
api.Set "K2" "hello" |> ignore
api.Get<string> "K2" |> printfn "[K2] %A"

api.Set "K3" "hello" |> ignore
api.Get<int> "K3" |> printfn "[K3] %A"
