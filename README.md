# Markdown - ControlAddin for Business Central 
This simple ControlAddin enables the input of formatted text in Markdown with immediate preview as html. Each change is also available again within Business Central.  
Instead of using vanilla Javascript, the development happens in F# using Fable and Elmish and of course AL.

## Setup environment

### Prequesites

* Node https://nodejs.org
* Yarn https://yarnpkg.com
* F# https://fsharp.org/use/windows/

### IDE

VS Code

Reqiured Extensions 
   * AL Language
   * **Ionide** http://ionide.io  

 Optional / recommended  
   * CRS AL Language Extension 
   * AL Code Outline
   * fantomas-fmt
   * Sass
   * Rainbow brackets

## Getting started

Clone Repository and open it with VS Code.
```
git clone https://github.com/SCullman/Al.Fable.Markdown
cd Al.Fable.Markdown
code .
```

### Dependencies
```bash
yarn install          # Node modules via npm
.paket\paket install  # .Net libs and files from nuget and github
```
*  [F1] AL: Download Symbols

#### Cerificates for HMR (Hot Module Reloading)
```
cd .ssl
mkcert -install
mkcert localhost
``` 

### Start developing
1. ``yarn start``
1. [F5] AL: Publish


  

