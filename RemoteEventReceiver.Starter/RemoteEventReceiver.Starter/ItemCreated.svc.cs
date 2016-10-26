using Microsoft.SharePoint.Client.EventReceivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.SharePoint.Client;

namespace RemoteEventReceiver.Starter
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemCreated" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ItemCreated.svc or ItemCreated.svc.cs at the Solution Explorer and start debugging.
    public class ItemCreated : IRemoteEventService
    {
        /// <summary>
        /// Handles asynchronous events like ItemUpdating
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            //  throw new NotImplementedException();
            Trace.TraceInformation("ProcessOneWayEvent inside ItemCreated. Item ID: {0}", properties.ItemEventProperties.ListItemId);
            return null;
        }

        /// <summary>
        /// Handles synchronous events like ItemUpdated
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {                        
            string correlationID = properties.CorrelationId.ToString();
            string siteUrl = properties.ItemEventProperties.WebUrl;
            int listItemID = properties.ItemEventProperties.ListItemId;
            Guid listID = properties.ItemEventProperties.ListId;

            Trace.TraceInformation("Calling ProcessOneWayEvent in ItemCreated. Correlation ID is {0}. Site url is {1}. List ID is {2}. List Item ID is {3}", correlationID, siteUrl, listID, listItemID);
            
            //if using App Only Context, use this method, and make sure ClientId and ClientSecret are specified in AppSettings
            using(ClientContext context = Helpers.GetAppOnlyContext(siteUrl))
            {
                //do stuff
            }

            //if not using App Only Context, use this method, and make sure AuthentictedUserName and AuthentictedUserPassword are specified in AppSettings
            using (ClientContext context = Helpers.GetAuthenticatedContext(siteUrl))
            {
                //do stuff
            }
        }
    }
}
