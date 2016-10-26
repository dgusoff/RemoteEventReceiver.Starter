using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using Microsoft.SharePoint.Client.Utilities;
using System.Diagnostics;
using Microsoft.SharePoint.Client.EventReceivers;
using System.Web.Configuration;

namespace RemoteEventReceiver.Starter
{
    public class Helpers
    {
        public static ClientContext GetAppOnlyContext(string siteUrl)
        {
            try
            {
                Uri siteUri = new Uri(siteUrl);                
                string realm = TokenHelper.GetRealmFromTargetUrl(siteUri);              
                string accessToken = TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, siteUri.Authority, realm).AccessToken;

                return TokenHelper.GetClientContextWithAccessToken(siteUri.ToString(), accessToken);
            }

            catch (Exception ex)
            {
                Trace.TraceInformation("GetAppOnlyContext failed. {0}", ex.Message);
            }
            return null;
        }

        public static ClientContext GetAuthenticatedContext(string siteUrl)
        {
            string userName = WebConfigurationManager.AppSettings.Get("AuthenticatedUserName");
            string password = WebConfigurationManager.AppSettings.Get("AuthenticatedUserPassword");
            return GetAuthenticatedContext(siteUrl, userName, password);
        }

        public static ClientContext GetAuthenticatedContext(string siteUrl, string userName, SecureString password)
        {
            ClientContext ctx = new ClientContext(siteUrl);           
            ctx.Credentials = new SharePointOnlineCredentials(userName, password);
            return ctx;
        }

        public static ClientContext GetAuthenticatedContext(string siteUrl, string userName, string password)
        {
            SecureString securePassword = GetPassword(password);
            return GetAuthenticatedContext(siteUrl, userName, securePassword);
        }

        private static SecureString GetPassword(string passwd)
        {
            var secure = new SecureString();
            foreach (char c in passwd)
            {
                secure.AppendChar(c);
            }
            return secure;
        }

        public static string EmptyIfNull(object obj)
        {
            return obj == null ? "" : obj.ToString();
        }
    }
}