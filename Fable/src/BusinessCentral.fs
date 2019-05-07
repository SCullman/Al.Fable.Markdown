[<RequireQualifiedAccessAttribute>]
module BusinessCentral

open Fable.Core
open Fable.Core.JsInterop
open Browser

[<Emit("window[$0]=$1")>]
let subscribe (name : string) (fkt:'a ->unit) = jsNative

[<Emit("Microsoft.Dynamics.NAV.InvokeExtensibilityMethod($0, {{[$1]}});")>]
let inline dispatch (name : string) (arg: 'a) = ()

let prepareControl() =
    let iframe = window.frameElement
    let parent = iframe.parentElement
    parent?style?display <- "flex"
    parent?style?flexDirection <- "column"
    parent?style?flexGrow <- "1"
    let iframeStyle = iframe?style
    iframeStyle?removeProperty "height"
    iframeStyle?removeProperty "min-height"
    iframeStyle?removeProperty "max-height"
    iframeStyle?flexGrow <- "1"
    iframeStyle?flexShrink <- "1"
    iframeStyle?flexBasis <- "auto"
    iframeStyle?paddingBottom <- "42px"

    document.body.classList.add ("ms-Fabric");
