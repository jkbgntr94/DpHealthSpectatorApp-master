using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;

namespace Xamarin.Forms_EFCore.ViewModels.Statistics
{
    class StatisticsMainViewModel : BaseViewModel
    {

        private ObservableCollection<MovementObj> sequenceList = new ObservableCollection<MovementObj>();
        public ObservableCollection<MovementObj> SequenceList
        {
            get
            {
                return sequenceList;
            }
            set
            {
                sequenceList = value;

            }
        }


        private MovementObj selectedSequence;
        public MovementObj SelectedSequence
        {
            get
            {
                return selectedSequence;
            }
            set
            {
                if (selectedSequence != value)
                {
                    selectedSequence = value;
                    HandleSelectedItem();

                }

            }
        }
        DatabaseContext _context;

        public StatisticsMainViewModel()
        {
            _context = new DatabaseContext();


        }

        private void HandleSelectedItem()
        {

        }

        private void fillList()
        {
            RoomsDetection roomsDetection = new RoomsDetection();

            if (_context.MovementSekv.Any())
            {
                var movSekvList = _context.MovementSekv.ToList();

                foreach (var movSekv in movSekvList)
                {
                    DateTime convertedDate = DateTime.Parse(movSekv.TimeStamp);

                    Izby izba = null;
                    String izbaName = "";
                    try
                    {
                        izba = _context.Rooms.Where(t => t.IzbaID == movSekv.IzbyFK).First();
                        izbaName = izba.Nazov;
                    }
                    catch (Exception e)
                    {
                        izbaName = "Vonku";

                    }
                    string alert = "NA";

                    if (movSekv.Upozornenie_Cas != 0)
                    {
                        alert = "Čas";

                    }
                    else if (movSekv.Upozornenie_Hranica != 0)
                    {
                        alert = "Hranica";

                    }


                    MovementObj movObj = new MovementObj
                    {
                        PohId = movSekv.PohSekvId,
                        RoomName = izbaName,
                        Alert = alert,
                        LongDate = convertedDate.ToLongDateString(),
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        Duration = movSekv.Cas_Zotrvania,
                        xValue = movSekv.Xhodnota,
                        yValue = movSekv.Yhodnota
                    };

                    SequenceList.Add(movObj);

                }

            }
            else
            {
                UserDialogs.Instance.Alert("Neexistuje žiadna sekvencia", "", "OK");

            }

            SequenceList = new ObservableCollection<MovementObj>(SequenceList.Reverse());


        }


    }
}
