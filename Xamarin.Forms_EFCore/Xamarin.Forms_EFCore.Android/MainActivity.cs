using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Plugin.SecureStorage;
using Acr.UserDialogs;
using Plugin.Toasts;
using Xamarin.Forms;

namespace Xamarin.Forms_EFCore.Droid {
    [Activity(Label = "Xamarin.Forms_EFCore", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            UserDialogs.Init(this);
            

            global::Xamarin.Forms.Forms.Init(this, bundle);

            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init(this);
            LoadApplication(new App());
        }
    }
}

