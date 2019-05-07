module Addin

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React


module Api =

    type Msg =
        | AddinStarted
        | DataLoaded of string
        | DispatchFailed of exn

    let toBusinessCentral _ =
        let sub dispatch =
            BusinessCentral.subscribe "StartUp" (fun _ ->
                BusinessCentral.prepareControl()
                AddinStarted |> dispatch)
            BusinessCentral.subscribe "Refresh" (fun _ -> AddinStarted |> dispatch)
            BusinessCentral.subscribe "Recreate" (fun _ -> AddinStarted |> dispatch)
            BusinessCentral.subscribe "LoadData" (DataLoaded >> dispatch)
        Cmd.ofSub sub

    let requestDataCmd() =
        Cmd.OfFunc.attempt (fun () -> BusinessCentral.dispatch "DataRequested" None) () DispatchFailed
    
    let submitDataCmd markdown html = 
        let data = {| html = html; markdown = markdown |}
        Cmd.OfFunc.attempt (BusinessCentral.dispatch "DataChanged") data DispatchFailed

module Types =

    type Msg =
        | ApiMsg of Api.Msg
        | MarkdownChanged of string

    type Model =
        { markdown : string option
          html : string option }

module State = 

    open Types
    let init() =
        { markdown = None
          html = None }, Cmd.none

    let makeHtml text =
        let xssFilter: Showdown.ShowdownExtension = importDefault "showdown-xss-filter"
        let converter  = Showdown.Globals.Converter.Create()
        converter.addExtension (xssFilter, "xssFilter")
        converter.makeHtml text

    let update msg model =
        let doNothing = model, Cmd.none
        match msg with
        | ApiMsg msg ->
            match msg with
            | Api.AddinStarted ->
                model, Cmd.map ApiMsg (Api.requestDataCmd()) 
            | Api.DataLoaded text ->
                model, Cmd.ofMsg (MarkdownChanged text)
            | Api.DispatchFailed _ -> doNothing
        | MarkdownChanged text ->
            let html = makeHtml text
            {model with markdown = Some text; html = Some html}, Cmd.map ApiMsg (Api.submitDataCmd text html)

module View =

    open Types

    let header caption icon = 
        div [ Style [TextTransform "uppercase" ]] 
            [ str caption 
              str " "
              i [ Class ("ms-Icon ms-Icon--" + icon)][ ]]

    let edit text dispatch = 
        textarea
            [ Class "ms-Grid-row"; 
              React.Helpers.valueOrDefault text
              OnChange(fun ev -> ev.target?value |> MarkdownChanged |> dispatch) ] []
              
    let preview html = 
        div [ Id "preview"
              DangerouslySetInnerHTML { __html = html } ] []

    let render model dispatch =

        div [ Class "ms-Grid"; Dir "ltr"  ]
            [ div [ Class "ms-Grid-row" ]
                  [ div [ Class "ms-Grid-col ms-sm12 ms-md6 ";]
                        [ header "Markdown" "Edit"
                          edit model.markdown dispatch ]

                    div [ Class "ms-Grid-col ms-sm12 ms-md6" ]
                        [ header "Preview" "View"
                          preview (model.html |> Option.defaultValue "")]]]

module Main =                       
    open Elmish.HMR

    Program.mkProgram State.init State.update View.render
    |> Program.withSubscription (Api.toBusinessCentral >> Cmd.map Types.ApiMsg)
    #if DEBUG
    |> Program.withConsoleTrace
    #endif
    |> Program.withReactBatched "controlAddIn"
    |> Program.run
