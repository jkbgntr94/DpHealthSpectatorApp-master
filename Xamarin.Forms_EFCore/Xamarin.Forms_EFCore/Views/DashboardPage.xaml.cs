﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            tempImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.temperature.png");
            pulseImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.pulse.png");
            dashboardImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.home.png");
            movementImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");
            fallImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.fall.png");
            settingsImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.settings.png");
            PriebStats.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.stats.png");
            RoomStats.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.home.png");


            //statisticsImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.stats.png");

            BindingContext = new DashboardViewModel();


            var temptgr = new TapGestureRecognizer();
            temptgr.Tapped += (s, e) => OnTempClicked();
            tempLayout.GestureRecognizers.Add(temptgr);

            var pulsetgr = new TapGestureRecognizer();
            pulsetgr.Tapped += (s, e) => OnPulseClicked();
            pulseLayout.GestureRecognizers.Add(pulsetgr);

            var dashtgr = new TapGestureRecognizer();
            dashtgr.Tapped += (s, e) => OnDashClicked();
            dashLayout.GestureRecognizers.Add(dashtgr);

            var movtgr = new TapGestureRecognizer();
            movtgr.Tapped += (s, e) => OnMovClicked();
            movementLayout.GestureRecognizers.Add(movtgr);

            var falltgr = new TapGestureRecognizer();
            falltgr.Tapped += (s, e) => OnFallClicked();
            fallLayout.GestureRecognizers.Add(falltgr);

        }

        async void OnTempClicked()
        {
            try
            {

                Navigation.InsertPageBefore(new TemperatureVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }



        }

        async void OnPulseClicked()
        {
            try
            {

                Navigation.InsertPageBefore(new PulseVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }



        }

        async void OnDashClicked()
        {

            try
            {

                Navigation.InsertPageBefore(new DashboardPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }


        }
        async void OnMovClicked()
        {
            try
            {

                Navigation.InsertPageBefore(new MovementVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }



        }

        async void OnFallClicked()
        {
            try
            {

                Navigation.InsertPageBefore(new FallVisualPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
                // await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }



        }
    }
}