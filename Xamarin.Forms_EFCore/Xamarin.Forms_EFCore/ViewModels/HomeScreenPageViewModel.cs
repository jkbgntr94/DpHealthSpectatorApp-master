using Java.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers;
using Xamarin.Forms_EFCore.Views;
using PCLStorage;
using System.Threading;
using System.Reflection;
using Xamarin.Forms_EFCore.Models;
using Newtonsoft.Json;
using System.Linq;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Helpers.XmlHelpers;
using System.Collections;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class HomeScreenPageViewModel : BaseViewModel
    {
        public ICommand Nacitaj { get; private set; }
        public ICommand LoadLimits { get; private set; }
        public ICommand PulseSekvencia { get; private set; }
        public ICommand ShowPulzSekv { get; private set; }
        public ICommand TempSekv { get; private set; }
        public ICommand XmlGeo { get; private set; }
        public ICommand Initialization { get; private set; }
        public ICommand TepHranice { get; private set; }


        private DatabaseContext _context;

        public HomeScreenPageViewModel()
        {

            _context = new DatabaseContext();
            Nacitaj = new Command(LoadData);
            LoadLimits = new Command(LoadLimit);
            PulseSekvencia = new Command(SekvencePulse);
            ShowPulzSekv = new Command(showPulzSekv);
            TempSekv = new Command(tempSekv);
            XmlGeo = new Command(xmlgeo);
            Initialization = new Command(initialization);
            TepHranice = new Command(tepHranice);
        }


        async void LoadData()
        {
             LoadPulse loader = new LoadPulse();
             loader.LoadPulseJson(_context);
             
            LoadTemperature temploader = new LoadTemperature();
            temploader.LoadTemperatureJson(_context);
            
           
             var all = _context.Temperature.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine("teplota " + a.TeplotaId + " " + a.TimeStamp + " " + a.Hodnota);


            }

            var all1 = _context.Pulse.ToList();
            foreach (var a in all1)
            {
                System.Diagnostics.Debug.WriteLine("pulz " + a.TepId + " " + a.TimeStamp + " " + a.Hodnota);


            }


        }
        async void LoadLimit()
        {
            Helpers.JsonLoaderHelpers.LoadLimits loader = new Helpers.JsonLoaderHelpers.LoadLimits();
            loader.LoadPulseLimits(_context);
            loader.LoadTemperatureLimits(_context);
            

        }

        async void SekvencePulse()
        {
            SequenceCreator sq = new SequenceCreator();
            sq.PulseSequencer(_context);
            
        }


        async void tempSekv()
        {
            SequenceCreator sq = new SequenceCreator();
            sq.TemperatureSequencer(_context);

        }




        async void showPulzSekv()
        {
           
            var allsekv = _context.PulseSekv.ToList();
            
            foreach (var c in allsekv)
            {
                System.Diagnostics.Debug.WriteLine("Moje sekvencei " + c.TepSekvId + " " + c.Sekvencia + "-" + c.TimeStart + "-" + c.TimeClose);

            }

        }

        async void xmlgeo()
        {
            XmlParser parser = new XmlParser();
            parser.LoadXMLData();
        }

        async void tepHranice()
        {

            Hashtable TepTable = new Hashtable();
            var all1 = _context.Pulse.ToList();
            foreach (var a in all1)
            {
                int hod = (int)Math.Round(a.Hodnota);

                if (TepTable.ContainsKey(hod))
                {
                    int pocet = int.Parse(TepTable[hod].ToString());
                    TepTable[hod] = pocet + 1;
                   // System.Diagnostics.Debug.WriteLine("Moje stare " + " " + hod + " " +  TepTable[hod]);

                     
                }
                else
                {
                    TepTable.Add(hod, 1);
                    //System.Diagnostics.Debug.WriteLine("Moje stare update " + " " + hod + " " + TepTable[hod]);

                }


            }

            var tablelist = TepTable.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList();
            foreach(var l in tablelist)
            {
                System.Diagnostics.Debug.WriteLine("Moje " + " " + l.Key + " " + l.Value);


            }


        }

        async void initialization()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreatePacientPage());

        }
    }
}
