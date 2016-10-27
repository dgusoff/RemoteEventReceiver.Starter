# RemoteEventReceiver.Starter
A minimal starter project for remote event receiver in Office 365

This is a minimal ASP.NET web application configured for use as a SharePoint Remote Receiver. To use this project, clone and deploy to an Azure App Service.

# Authentication back to SharePoint

If your Remote Event Receiver needs to call back into SharePoint, you'll need to obtain a ClientContext object.  This project provides two methods for doing so:

## App Only Context.


```C#
//if using App Only Context, use this method, and make sure ClientId and ClientSecret are specified in AppSettings
 using(ClientContext context = Helpers.GetAppOnlyContext(siteUrl))
{
    //do stuff
}
```

## Authenticated Credentials. 
If you do not use an app, the only other way to obtain a context is to use stored credentials. In this case the event reciever will have the permissions associated with those credentials.  To use stored credentials, create and populate these App Settings on the web site's configuration: AuthenticatedUserName and AutheticatedUserPassword.

```C#
 //if using Authenticated Context, use this method, and make sure AuthentictedUserName and AuthentictedUserPassword are specified in AppSettings
using (ClientContext context = Helpers.GetAuthenticatedContext(siteUrl))
{
     //do stuff
}
```

# attaching the event receiver to SharePoint
The easiest way to attach this event receiver to SharePoint is to use the PnP PowerShell Cmdlet for this:

```powershell

Add-SPOEventReceiver -List ListName -Url http://websitename.azurewebsites.net/ItemAdded.svc -Name "MyEventReceiver" -E
ventReceiverType ItemAdded
```

You can, in theory, use this project for any type of Remote Event Receiver in SharePoint, but this has only been tested using List Item Events. Your mileage may vary.
