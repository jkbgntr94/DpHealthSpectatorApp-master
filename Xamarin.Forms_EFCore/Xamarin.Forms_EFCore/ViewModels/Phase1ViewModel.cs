using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class Phase1ViewModel : BaseViewModel
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

        /*---------------------------------------*/
        private Timer _timer;

        private TimeSpan _totalSeconds = new TimeSpan(0, 0, 15, 0);

        public TimeSpan TotalSeconds

        {

            get { return _totalSeconds; }

            set { _totalSeconds = value;
                OnPropertyChanged();

            }

        }

        public Command StartCommand { get; set; }

        public Command PauseCommand { get; set; }

        public Command StopCommand { get; set; }

        /*--------------------------------------*/




        public ICommand CancelPhase1 { get; private set; }
        public ICommand ToPhase2 { get; private set; }

        DatabaseContext _context;

        public Phase1ViewModel()
        {
            _context = new DatabaseContext();
            CancelPhase1 = new Command(cancelPhase1);
            ToPhase2 = new Command(toPhase2);

          
            StartCommand = new Command(StartTimerCommand);

            PauseCommand = new Command(PauseTimerCommand);

            StopCommand = new Command(StopTimerCommand);


            _timer = new Timer(TimeSpan.FromSeconds(1), CountDown);

            TotalSeconds = _totalSeconds;

            StartTimerCommand();

            for (int i = 1; i <= 20; i++)
            {
                MeasureValues(i);
            }
            //TODO: ukladanie nameranych dat do suboru
        }

        public void MeasureValues(int k)
        {
           
            measuredValues.Add(k.ToString());
            

        }

        async void cancelPhase1()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ChooseSecPhasePage());

        }

        async void toPhase2()
        {


            await Application.Current.MainPage.Navigation.PushAsync(new ChooseSecPhasePage());
            
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

            else

            {

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

            TotalSeconds = new TimeSpan(0, 0, 0, 10);

            _timer.Stop();

        }




    }

}
