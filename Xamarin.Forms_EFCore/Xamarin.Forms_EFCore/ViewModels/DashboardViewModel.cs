using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class DashboardViewModel : BaseViewModel
    {
        private ObservableCollection<string> alerts = new ObservableCollection<string>();
        public ObservableCollection<string> Alerts
        {
            get
            {
                return alerts;
            }
            set
            {
                alerts = value;

            }
        }

        private string pulseValue;
        public string PulseValue
        {
            get
            {
                return pulseValue;
            }
            set
            {
                pulseValue = value;

            }
        }

        private string tempValue;
        public string TempValue
        {
            get
            {
                return tempValue;
            }
            set
            {
                tempValue = value;

            }
        }

        private string motionValue;
        public string MotionValue
        {
            get
            {
                return motionValue;
            }
            set
            {
                motionValue = value;

            }
        }

        private string fallValue;
        public string FallValue
        {
            get
            {
                return fallValue;
            }
            set
            {
                fallValue = value;

            }
        }

        DatabaseContext _context;

        public DashboardViewModel()
        {
            _context = new DatabaseContext();

            /* PulseValue = 10.ToString();
             TempValue = 15.ToString();
             MotionValue = "Spalna";
             FallValue = "NA";*/
            setValues();

        }

        public void setValues()
        {
            setPulseValue();
            setTemperatureValue();
            setMovementValue();
            setFallValue();

        }

        public void setPulseValue()
        {
            if (_context.Pulse.Any())
            {

                Tep tep = _context.Pulse.FirstOrDefault(t => t.TepId == _context.Pulse.Max(x => x.TepId));

                PulseValue = tep.Hodnota.ToString();

            }
            else
            {
                PulseValue = "NA";

            }

        }

        public void setTemperatureValue()
        {
            if (_context.Temperature.Any())
            {

                Teplota teplota = _context.Temperature.FirstOrDefault(t => t.TeplotaId == _context.Temperature.Max(x => x.TeplotaId));

                TempValue = teplota.Hodnota.ToString();

            }
            else
            {
                TempValue = "NA";

            }
        }

        public void setMovementValue()
        {
            if (_context.Movement.Any())
            {

                Pohyb pohyb = _context.Movement.FirstOrDefault(t => t.PohybId == _context.Movement.Max(x => x.PohybId));

                //MotionValue = pohyb.Hodnota.ToString();
                //TREBA ZISKAT NAZOV IZBY
                MotionValue = "IZBA";
            }
            else
            {
                MotionValue = "NA";

            }


        }

        public void setFallValue()
        {
            if (_context.Akcelerometers.Any())
            {

                Akcelerometer akcelerometer = _context.Akcelerometers.FirstOrDefault(t => t.AkcelerometerID == _context.Akcelerometers.Max(x => x.AkcelerometerID));

                FallValue = akcelerometer.TimeStamp.ToString();
                
            }
            else
            {
                FallValue = "NA";

            }


        }

    }
}
