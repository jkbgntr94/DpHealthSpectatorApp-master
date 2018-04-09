using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class AsyncDataLoader
    {
        LoadTemperature loadTemperature;
        LoadPulse loadPulse;
        LoadMovement loadMovement;
        //DatabaseContext context;
        SequenceCreator sequenceCreator;
        LoadFall loadFall;


        public AsyncDataLoader()
        {
            loadTemperature = new LoadTemperature();
            //context = new DatabaseContext();
            sequenceCreator = new SequenceCreator();
            loadPulse = new LoadPulse();
            loadMovement = new LoadMovement();
            loadFall = new LoadFall();
        }

        
        public async void LoadData()
        {
            loadTemperature.createFileFromFile();
            loadPulse.createFileFromFile();


            System.Timers.Timer tempTimer = new System.Timers.Timer();
            tempTimer.Elapsed += new ElapsedEventHandler(OnTempTimeEvent);
            tempTimer.Interval = 20000;
            tempTimer.Enabled = true;

            Thread.Sleep(5000);

            System.Timers.Timer pulseTimer = new System.Timers.Timer();
            pulseTimer.Elapsed += new ElapsedEventHandler(OnPulseTimeEvent);
            pulseTimer.Interval = 60000;
            pulseTimer.Enabled = true;

            Thread.Sleep(5000);

            System.Timers.Timer movementTimer = new System.Timers.Timer();
            movementTimer.Elapsed += new ElapsedEventHandler(OnMovementTimeEvent);
            movementTimer.Interval = 60000;
            movementTimer.Enabled = true;

            Thread.Sleep(5000);

            System.Timers.Timer fallTimer = new System.Timers.Timer();
            fallTimer.Elapsed += new ElapsedEventHandler(OnFallTimeEvent);
            fallTimer.Interval = 60000;
            fallTimer.Enabled = true;


            await Task.Run(async () =>
            {
                


            }).ConfigureAwait(false); 

        }

        private void OnTempTimeEvent(object source, ElapsedEventArgs e)
        {
            loadTemperature.readFileByLines();


            DatabaseContext seqContext = new DatabaseContext();
            
            sequenceCreator.TemperatureSequencer(seqContext);
            //System.Diagnostics.Debug.WriteLine("!!! Hello World !!!");
        }

        private void OnPulseTimeEvent(object source, ElapsedEventArgs e)
        {
            loadPulse.readFileByLines();


            DatabaseContext seqContext = new DatabaseContext();

            sequenceCreator.PulseSequencer(seqContext);
            //System.Diagnostics.Debug.WriteLine("!!! Hello World !!!");
        }
        private void OnMovementTimeEvent(object source, ElapsedEventArgs e)
        {
            loadMovement.GenerateMovementOneSample();


            DatabaseContext seqContext = new DatabaseContext();

            sequenceCreator.MovementSequencer(seqContext);
            //System.Diagnostics.Debug.WriteLine("!!! Hello World !!!");
        }

        private void OnFallTimeEvent(object source, ElapsedEventArgs e)
        {
            loadFall.GenerateFallOneSample();

           
        }

    }
}
