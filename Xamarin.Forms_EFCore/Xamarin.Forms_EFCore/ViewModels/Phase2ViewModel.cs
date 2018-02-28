using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class Phase2ViewModel : BaseViewModel
    {
        private ObservableCollection<string> measuredValues = new ObservableCollection<string>();
        public ObservableCollection<string> MeasuredValues
        {
            get
            {
                return measuredValues;
            }
            set
            {
                measuredValues = value;

            }
        }

        private string actualActivity;
        public string ActualActivity
        {
            get
            {
                return actualActivity;
            }
            set
            {
                actualActivity = value;

            }
        }

        private List<int> valuesForList = new List<int>();
        private int loopCounter;

        /*---------------------------------------*/
        private Timer _timer;

        private TimeSpan _totalSeconds = new TimeSpan(0, 0, 0, 40);

        public TimeSpan TotalSeconds

        {

            get { return _totalSeconds; }

            set
            {
                _totalSeconds = value;
                OnPropertyChanged();

            }

        }

        public Command StartCommand { get; set; }

        public Command PauseCommand { get; set; }

        public Command StopCommand { get; set; }

        /*--------------------------------------*/
        private Queue<Activities> aktivity { get; set; }



        public ICommand CancelPhase1 { get; private set; }
        public ICommand ToPhase2 { get; private set; }

        DatabaseContext _context;
        private Activities currentAct;
        private string measuredValuesForDb;

        public Phase2ViewModel(Queue<Activities> aktivity_in)
        {
            aktivity = aktivity_in;
            foreach (var aktiv in aktivity)
            {
                System.Diagnostics.Debug.WriteLine("aktivitka " + aktiv.ActSwitchIsChecked + " " + aktiv.Name + " " + aktiv.StressIsChecked);
            }
            if (aktivity.Count > 0)
            {
                currentAct = aktivity.Dequeue();
                ActualActivity = "Zostáva činností: " + aktivity.Count + " - " + currentAct.Name;
            }
            else
            {
                storeActivityToDb();
                Application.Current.MainPage.Navigation.PushAsync(new FinalInitializationPage());
            }

            _context = new DatabaseContext();
            CancelPhase1 = new Command(cancelPhase1);
            ToPhase2 = new Command(toPhase2);


            StartCommand = new Command(StartTimerCommand);

            PauseCommand = new Command(PauseTimerCommand);

            StopCommand = new Command(StopTimerCommand);


            _timer = new Timer(TimeSpan.FromSeconds(1), CountDown);

            TotalSeconds = _totalSeconds;

            StartTimerCommand();

            Measurement m = new Measurement();
            valuesForList = m.getValuesForList();

            measuredValuesForDb = string.Empty;
        }

        public void MeasureValues()
        {

            int k = 0;
            try
            {
                if (valuesForList.Count > 0)
                {
                    k = valuesForList[0];

                    measuredValues.Add(k.ToString());

                    measuredValuesForDb = measuredValuesForDb + k.ToString() + " ";
                    valuesForList.RemoveAt(0);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception : " + e.Source);
            }

        }

        async void cancelPhase1()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ChooseSecPhasePage());

        }

        async void toPhase2()
        {
            storeActivityToDb();
            await Application.Current.MainPage.Navigation.PushAsync(new ChooseSecPhasePage());

        }

        private void storeActivityToDb()
        {
            Activities act = currentAct;
            act.Value = measuredValuesForDb;

            _context.Activities.Add(act);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++ activity " + act.Name + " " + act.ActSwitchIsChecked + " " + act.Value + " " + act.StressIsChecked);
            

        }

        /*------------------------------*/
        private void StartTimerCommand()

        {

            _timer.Start();

        }




        /// <summary>

        /// Counts down the timer

        /// </summary>

        private void CountDown()

        {

            if (_totalSeconds.TotalSeconds == 0)
            {

                //do something after hitting 0, in this example it just stops/resets the timer

                StopTimerCommand();
            }
            else if (loopCounter == 10)
            { //TODO: SWAP TO PULSE MEASURE INTERVAL
                loopCounter = 0;
                MeasureValues();
            }

            else

            {
                loopCounter++;
                TotalSeconds = _totalSeconds.Subtract(new TimeSpan(0, 0, 0, 1));

            }

        }




        /// <summary>

        /// Pauses the timer

        /// </summary>

        private void PauseTimerCommand()

        {

            _timer.Stop();

        }




        /// <summary>

        /// Stops and resets the timer

        /// </summary>

        private void StopTimerCommand()

        {

            TotalSeconds = new TimeSpan(0, 0, 0, 0);

            _timer.Stop();

            if (aktivity.Count > 0)
            {
                storeActivityToDb();
                Application.Current.MainPage.Navigation.PushAsync(new Phase2Page(aktivity));
            } else
            {
                storeActivityToDb();

                Application.Current.MainPage.Navigation.PushAsync(new FinalInitializationPage());
            }

        }


    }
}
