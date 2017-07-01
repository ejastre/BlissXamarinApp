using Android.OS;
using BlissXamarinApp.Droid;
using BlissXamarinApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidMethods))]
namespace BlissXamarinApp.Droid
{
    public class AndroidMethods : IAndroidMethods
    {
        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}