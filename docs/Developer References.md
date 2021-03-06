This page will contain some information for 3rd party developers to hook into HAP+, or register there api to use as a live tile, or other app inside HAP.

## CSS/JS Plugin Registration

{"HAP v10+ Supports Late loading of DLLs into HAP+ to register CSS/JS at runtime"}

In your DLL add a class using the HAP.Web.Configuration.IRegister interface, example:
{code:c#}
    public class Register : IRegister
    {
        public RegistrationPath[]() RegisterCSS()
        {
            return new RegistrationPath[]() { 
                new RegistrationPath { Path = "~/style/fullcalendar.css", LoadOn = new string[]() { "/timetable.aspx" }}, 
                new RegistrationPath { Path = "~/style/timetable.css", LoadOn = new string[]() { "/timetable.aspx" }} 
            };
        }

        public RegistrationPath[]() RegisterJSStart()
        {
            throw new NotImplementedException();
        }

        public RegistrationPath[]() RegisterJSBeforeHAP()
        {
            throw new NotImplementedException();
        }

        public RegistrationPath[]() RegisterJSAfterHAP()
        {
            return new RegistrationPath[]() {
                new RegistrationPath { Path = "~/Scripts/fullcalendar.min.js", LoadOn = new string[]() { "/timetable.aspx" } }
            };
        }

        public RegistrationPath[]() RegisterJSEnd()
        {
            throw new NotImplementedException();
        }
    }
{code:c#}


## WCF Service Registration

{"HAP v8+ Supports Late Loading of DLLs into HAP+ for WCF Service Registration"}

Add:

{code:c#}
using HAP.Web.Configuration;
[ServiceAPI(%api url%)](ServiceAPI(%api-url%))
[ServiceContract()](ServiceContract())
public class SomeService
{
}
{code:c#}

where the %api url% starts with "api/".

## HTTP Handler Registration

{"HAP v10.1+ Supports Late Loading of DLLs into HAP+ for HTTP Handler Registration"}

Add:

{code:c#}
using HAP.Web.Configuration;
[HandlerAPI(%api url%)](HandlerAPI(%api-url%))
public class Handler : IRouteHandler
{
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler()
        }
}
public class HttpHandler : IHttpHandler
{
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
        }
}
{code:c#}

where the %api url% starts with "api/".

## Where can I find what API's HAP has available to use?

There are some simple JSON/XML APIs you can call via REST, these can be found by going to %yourhapinstall%/api/  *v8 Feature only

## Where can I get Configuration Data from?

Add a reference to the HAP.Web.Configuration dll;

{code:c#}
HapConfig config = HapConfig.Current;
{code:c#}

## How to Video

{video:url=http://www.youtube.com/watch?v=2DKGzF-RgFw,type=youtube,width=640,height=380}

## Plugin Example

[HAP.Plugin.Example.zip](Developer References_HAP.Plugin.Example.zip)

## How can I manually create the Event Log Source?

{{
New-EventLog -LogName Application -Source "Home Access Plus+"
Write-EventLog -LogName Application -Source "Home Access Plus+" -EntryType Error -Message "test" -EventID 1
}}