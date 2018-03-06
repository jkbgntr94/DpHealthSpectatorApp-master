using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class TestDataDbFiller
    {
        DatabaseContext _context;
        public TestDataDbFiller()
        {
            _context = new DatabaseContext();

            LoadPulse loader = new LoadPulse();
            loader.LoadPulseJson(_context);

            LoadTemperature temploader = new LoadTemperature();
            temploader.LoadTemperatureJson(_context);

            Helpers.JsonLoaderHelpers.LoadLimits loaderL = new Helpers.JsonLoaderHelpers.LoadLimits();
            loaderL.LoadPulseLimits(_context);
            loaderL.LoadTemperatureLimits(_context);
            loaderL.LoadMovementLimits(_context);

            SequenceCreator sq = new SequenceCreator();
            sq.PulseSequencer(_context);
            
            sq.TemperatureSequencer(_context);

            loader.LoadPulseJson(_context);
            temploader.LoadTemperatureJson(_context);

            LoadMovement moveLoader = new LoadMovement();
            moveLoader.GenerateMovementData(_context);

            LoadRooms loadRooms = new LoadRooms();
            loadRooms.LoadRoomsData(_context);



        }
    }
}
