using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views.Settings;
using Xamarin.Forms_EFCore.Views.Statistics;

namespace Xamarin.Forms_EFCore.ViewModels.Statistics
{
    class StatisticsSelectionViewModel : BaseViewModel
    {
        public ICommand MainStatisticsCommand { get; private set; }
        public ICommand RoomStatisticsCommand { get; private set; }

        public StatisticsSelectionViewModel()
        {
            MainStatisticsCommand = new Command(toMainStats);
            RoomStatisticsCommand = new Command(roomStatsCommand);


        }

        private async void toMainStats()
        {
           

            await Application.Current.MainPage.Navigation.PushModalAsync(new StatisticsMainPage());


        }

        private void roomStatsCommand()
        {

            DatabaseContext context = new DatabaseContext();

            var SekvList = context.MovementSekv.ToList();

            var results = SekvList.GroupBy(
                p => p.IzbyFK);

            String alertText = "";

            foreach (var r in results)
            {

                try
                {
                    Izby izba = context.Rooms.Where(p => p.IzbaID == r.Key).First();
                    var timeList = context.MovementSekv.Where(f => f.IzbyFK == izba.IzbaID).ToList();
                    double sumTime = 0;
                    int hours = 0; int minutes = 0; int sec = 0;
                    foreach (var a in timeList)
                    {
                        DateTime start = DateTime.Parse(a.TimeStamp);
                        DateTime close = DateTime.Parse(a.TimeStop);
                        TimeSpan fin = close - start;
                        sumTime += Math.Abs(fin.TotalMinutes);
                        hours += Math.Abs(fin.Hours);
                        minutes += Math.Abs(fin.Minutes);
                        sec += Math.Abs(fin.Seconds);
                    }

                    System.Diagnostics.Debug.WriteLine("ROOMS: " + izba.Nazov + " -- " + r.Count() + " " + sumTime + " min");
                    alertText += izba.Nazov + "(" + r.Count() + "x) - "+hours +" h "+minutes+" min " +sec + " sec" +"\n";
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ROOMS STATISTIC: " + e.ToString());
                    System.Diagnostics.Debug.WriteLine("ROOMS: " + " NA " + " -- " + r.Count());

                    var timeList = context.MovementSekv.Where(f => f.IzbyFK == null).ToList();
                    double sumTime = 0;
                    int hours = 0; int minutes = 0; int sec = 0;
                    foreach (var a in timeList)
                    {
                        DateTime start = DateTime.Parse(a.TimeStamp);
                        DateTime close = DateTime.Parse(a.TimeStop);
                        TimeSpan fin = close - start;
                        sumTime += Math.Abs(fin.TotalMinutes);
                        hours += Math.Abs(fin.Hours);
                        minutes += Math.Abs(fin.Minutes);
                        sec += Math.Abs(fin.Seconds);
                    }

                    alertText += "NA" + "(" + r.Count() + "x) - " + hours + " h " + minutes + " min " + sec + " sec" + "\n";
                }



            }

            UserDialogs.Instance.Alert(alertText, "Štatistika pohybu", "OK");

            //await Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());


        }

    }
}
