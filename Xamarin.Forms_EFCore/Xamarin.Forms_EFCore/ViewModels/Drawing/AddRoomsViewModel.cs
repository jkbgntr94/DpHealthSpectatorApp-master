using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels.Drawing
{
    public class AddRoomsViewModel : BaseViewModel
    {
      /*  private string maxX;
        public string MaxX
        {
            get
            {
                return maxX;
            }
            set
            {
                maxX = value;

            }
        }

        private string maxY;
        public string MaxY
        {
            get
            {
                return maxY;
            }
            set
            {
                maxY = value;

            }
        }*/

        private string nameRoom;
        public string NameRoom
        {
            get
            {
                return nameRoom;
            }
            set
            {
                nameRoom = value;

            }
        }

        private string leftDownX;
        public string LeftDownX
        {
            get
            {
                return leftDownX;
            }
            set
            {
                leftDownX = value;

            }
        }

        private string leftDownY;
        public string LeftDownY
        {
            get
            {
                return leftDownY;
            }
            set
            {
                leftDownY = value;

            }
        }

        private string rightUpX;
        public string RightUpX
        {
            get
            {
                return rightUpX;
            }
            set
            {
                rightUpX = value;

            }
        }

        private string rightUpY;
        public string RightUpY
        {
            get
            {
                return rightUpY;
            }
            set
            {
                rightUpY = value;

            }
        }

        private ObservableCollection<string> rooms = new ObservableCollection<string>();
        public ObservableCollection<string> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                rooms = value;

            }
        }


        public ICommand SaveRoom { get; private set; }
        public ICommand Finish { get; private set; }

        DatabaseContext _context;

        public AddRoomsViewModel()
        {
            _context = new DatabaseContext();
            SaveRoom = new Command(saveRoom);
            Finish = new Command(finish);

            TestDataDbFiller filler = new TestDataDbFiller();

            //System.Diagnostics.Debug.WriteLine("****************************** finished ");
            /*RoomsDetection rd = new RoomsDetection(); 
            var poh = _context.Movement.ToList();
            foreach(var p in poh)
            {
                var izba = rd.findRoom(p);

                System.Diagnostics.Debug.WriteLine("DETEGOVANA IZBA " + izba.Nazov + " poh x: " + p.Xhodnota + " poh y: " + p.Yhodnota + " lava izba: " + izba.LavaXhodnota + "/" + izba.LavaYhodnota + " prava izba: " + izba.PravaXhodnota + "/" + izba.PravaYhodnota);

            }*/
            

        }

        async void finish()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new DrawHomePage());

           

        }

        public void saveRoom()
        {
            int index = 0;
            if (!_context.Rooms.Any())
            {
                index = 1;

            }
            else
            {
                Izby izba = _context.Rooms.FirstOrDefault(t => t.IzbaID == _context.Rooms.Max(r => r.IzbaID));
                index = izba.IzbaID;
                index++;

            }

            Izby izbaNova = new Izby()
            {
                IzbaID = index,
                Nazov = NameRoom,
                LavaXhodnota = float.Parse(LeftDownX, CultureInfo.InvariantCulture.NumberFormat),
                LavaYhodnota = float.Parse(LeftDownY, CultureInfo.InvariantCulture.NumberFormat),
                PravaXhodnota = float.Parse(RightUpX, CultureInfo.InvariantCulture.NumberFormat),
                PravaYhodnota = float.Parse(RightUpY, CultureInfo.InvariantCulture.NumberFormat)
         
            };

            _context.Rooms.Add(izbaNova);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("****************************** " + e.ToString());
            }

            Rooms.Add(NameRoom);

        }
    }
}
