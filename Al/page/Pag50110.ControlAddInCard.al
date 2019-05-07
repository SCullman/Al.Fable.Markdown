// Welcome to your new AL extension.
// Remember that object names and IDs should be unique across all extensions.
// AL snippets start with t*, like tpageext - give them a try and happy coding!
page 50110 "Control Add-In Card"
{
    PageType = Card;
    SourceTable = Item;

    layout
    {
        area(Content)
        {
            group(General)
            {
                field("No."; "No.") { ApplicationArea = All; }

            }
            usercontrol(MarkdownControlAddIn; MarkdownControlAddIn)
            {
                ApplicationArea = All;

                trigger DataRequested()
                begin
                    CurrPage.MarkdownControlAddIn.LoadData('FromBC');
                end;

                trigger DataChanged(o: JsonObject)
                var
                    html: Text;
                    markDown: Text;
                    outStream: OutStream;
                    token: JsonToken;
                begin
                    if o.get('markdown', token) then begin
                        markDown := token.AsValue().AsText();
                    end;
                    if o.get('html', token) then begin
                        html := token.AsValue().AsText();
                    end;
                end;
            }
            group(Other)
            {
                Caption = 'Noch was';
                field(Description; Description)
                {
                    ApplicationArea = All;
                }
            }
        }
    }
}