controladdin MarkdownControlAddIn
{
    // In development (yarn start)
    Scripts = 'https://localhost:8080/addin.js', 'https://localhost:8080/style.js';
    StyleSheets = 'https://static2.sharepointonline.com/files/fabric/office-ui-fabric-core/9.6.1/css/fabric.min.css';


    // In Production (yarn build) 
    /* Scripts = 'ControlAddin/addin.js';
    StyleSheets = 'ControlAddIn/Stylesheet.css'
                , 'https://static2.sharepointonline.com/files/fabric/office-ui-fabric-core/9.6.1/css/fabric.min.css'; */


    // Remains the same
    StartupScript = 'ControlAddin/static/Startup.js';
    RecreateScript = 'ControlAddin/static/Recreate.js';
    RefreshScript = 'ControlAddin/static/Refresh.js';

    HorizontalStretch = true;
    HorizontalShrink = true;
    MinimumWidth = 250;

    event DataRequested();
    event DataChanged(o: JsonObject);
    procedure LoadData(value: text);

}