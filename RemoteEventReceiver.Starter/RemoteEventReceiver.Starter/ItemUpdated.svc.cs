using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OfficeDevPnP.Core;
using System.Diagnostics;
using System.Web.Configuration;

namespace RemoteEventReceiver.Starter
{   
    public class ItemUpdated : IRemoteEventService
    {     
        /// <summary>
        /// Handles asynchronous events like ItemUpdating
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles synchronous events like ItemUpdated
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            if (!WasUpdatedByEventReceiver(properties))
            {
                //do stuff
            }
        }

        private bool WasUpdatedByEventReceiver(SPRemoteEventProperties properties)
        {
            return (properties.ItemEventProperties.AfterProperties.Count == 2 && properties.ItemEventProperties.AfterProperties.ContainsKey("Title"));
        }





    }
}
