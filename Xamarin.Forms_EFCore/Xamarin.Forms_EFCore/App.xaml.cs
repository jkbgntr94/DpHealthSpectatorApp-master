using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers;

namespace Xamarin.Forms_EFCore {

    public partial class App : Application {

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new Xamarin.Forms_EFCore.Views.DashboardPage());
        }

        protected override void OnStart() {
            //TODO: START HERE IF EXIST PROFILE/ ELSE START ON INIT FINISH
            
            new DatabaseContext(999);
            DatabaseContext context = new DatabaseContext();
            SettingsController.MeasurementRunning = 0;
            if (context.Profiles.Any())
            {
                SettingsController.MeasurementRunning = 1;
                AsyncDataLoader asyncDataLoader = new AsyncDataLoader();


                 LoadRooms loadRooms = new LoadRooms();
                 loadRooms.LoadRoomsData();

                TestDataDbFiller testDataDbFiller = new TestDataDbFiller();
                testDataDbFiller.loadMandatoryData();
                asyncDataLoader.LoadData();

            }

           
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
