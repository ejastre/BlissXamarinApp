using System;
using System.Text.RegularExpressions;
using Plugin.Connectivity;

namespace BlissXamarinApp.Utils
{
    public static class Util
    {
        public static bool CheckConnectivity()
        {
            try
            {
                return CrossConnectivity.Current.IsConnected && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckEmail(string emailString)
        {
            return Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
}
