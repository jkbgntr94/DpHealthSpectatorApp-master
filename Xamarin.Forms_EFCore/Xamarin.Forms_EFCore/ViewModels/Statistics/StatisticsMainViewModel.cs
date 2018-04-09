using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;
using Xamarin.Forms_EFCore.Views.Statistics;

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


        private RoomStatisticsObj selectedSequence;
        public RoomStatisticsObj SelectedSequence
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

        private ObservableCollection<RoomStatisticsObj> roomsList = new ObservableCollection<RoomStatisticsObj>();
        public ObservableCollection<RoomStatisticsObj> RoomsList
        {
            get
            {
                return roomsList;
            }
            set
            {
                roomsList = value;

            }
        }

        DatabaseContext _context;

        public StatisticsMainViewModel()
        {
            _context = new DatabaseContext();
            //UserDialogs.Instance.ShowLoading("Loading ...");

            FindRoomsStatisticsList();
            
        }

        private void HandleSelectedItem()
        {
            //System.Diagnostics.Debug.WriteLine("Selected item: " + SelectedSequence.RoomName + " " + SelectedSequence.StartTime);

            Application.Current.MainPage.Navigation.PushAsync(new StatisticVisualPage(SelectedSequence));
        }

        private async void LoadData()
        {
           
            FindRoomsStatisticsList();
         
        }

        private void FindRoomsStatisticsList()
        {
            RoomsDetection roomsDetection = new RoomsDetection();


            if (_context.MovementSekv.Any())
            {
                var movSekvList = _context.MovementSekv.ToList();
                int counter = 0;
                foreach (var movSekv in movSekvList)
                {
                   // if (++counter > 20) break;
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
                    List<Pohyb_Sekvencia> valueList = new List<Pohyb_Sekvencia>();

                    if (!RoomsList.Any())
                    {
                        valueList.Add(movSekv);
                        RoomStatisticsObj roomStatisticsObj = new RoomStatisticsObj
                        {
                            RoomName = izbaName,
                            StartDate = convertedDate.ToShortDateString(),
                            StartTime = convertedDate.ToLongTimeString(),
                            Sekvencie = valueList

                        };
                        RoomsList.Add(roomStatisticsObj);
                        System.Diagnostics.Debug.WriteLine("ROOMS STATS FIRST " + movSekv.PohSekvId);

                    }
                    else
                    {
                        RoomStatisticsObj rObj = RoomsList.Last();

                        if(rObj.RoomName == izbaName)
                        {
                            valueList = rObj.Sekvencie;
                            valueList.Add(movSekv);
                            rObj.Sekvencie = valueList;
                            //System.Diagnostics.Debug.WriteLine("ROOMS STATS Adding " + movSekv.PohSekvId + " " + izbaName + " " + valueList.Count);

                        }
                        else
                        {

                            if(rObj.StartDate == convertedDate.ToShortDateString())
                            {
                                rObj.EndDate = "";

                            }
                            else
                            {
                                rObj.EndDate = convertedDate.ToShortDateString();
                            }

                            // rObj.EndDate = convertedDate.ToShortDateString();

                            //rObj.EndTime = convertedDate.ToLongTimeString();
                            try
                            { 
                                rObj.EndTime = DateTime.Parse(movSekv.TimeStamp).ToLongTimeString();
                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(StatisticsMainViewModel) + " " + e.ToString());
                                rObj.EndTime = convertedDate.ToLongTimeString();
                            }

                            valueList.Add(movSekv);
                            RoomStatisticsObj roomStatisticsObj = new RoomStatisticsObj
                            {
                                RoomName = izbaName,
                                StartDate = convertedDate.ToShortDateString(),
                                StartTime = convertedDate.ToLongTimeString(),
                                Sekvencie = valueList

                            };
                            RoomsList.Add(roomStatisticsObj);

                            //System.Diagnostics.Debug.WriteLine("ROOMS STATS New " + movSekv.PohSekvId + " " + izbaName + " " + valueList.Count);

                        }

                    }

                }

                RoomsList = new ObservableCollection<RoomStatisticsObj>(RoomsList.Reverse());
            }
            
        }

        


    }
}
