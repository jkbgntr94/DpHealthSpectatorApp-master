using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Helpers
{
    public static class SettingsController
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static int AlertPulseSlabe
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertPulseSlabe), 9);
            set => AppSettings.AddOrUpdateValue(nameof(AlertPulseSlabe), value);
        }

        public static int AlertPulseStredne
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertPulseStredne), 9);
            set => AppSettings.AddOrUpdateValue(nameof(AlertPulseStredne), value);
        }

        public static int AlertPulseVysoke
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertPulseVysoke), 9);
            set => AppSettings.AddOrUpdateValue(nameof(AlertPulseVysoke), value);
        }

        public static int LongTimeNoMovement
        {
            get => AppSettings.GetValueOrDefault(nameof(LongTimeNoMovement), 9);
            set => AppSettings.AddOrUpdateValue(nameof(LongTimeNoMovement), value);
        }

        public static int SignalLost
        {
            get => AppSettings.GetValueOrDefault(nameof(SignalLost), 9);
            set => AppSettings.AddOrUpdateValue(nameof(SignalLost), value);
        }

        public static int FallDetected
        {
            get => AppSettings.GetValueOrDefault(nameof(FallDetected), 9);
            set => AppSettings.AddOrUpdateValue(nameof(FallDetected), value);
        }

        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }

        public static string NameEmail
        {
            get => AppSettings.GetValueOrDefault(nameof(NameEmail), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(NameEmail), value);
        }
        public static int MaxX
        {
            get => AppSettings.GetValueOrDefault(nameof(MaxX), 0);
            set => AppSettings.AddOrUpdateValue(nameof(MaxX), value);
        }
        public static int MaxY
        {
            get => AppSettings.GetValueOrDefault(nameof(MaxY), 0);
            set => AppSettings.AddOrUpdateValue(nameof(MaxY), value);
        }

        public static int MeasurementRunning
        {
            get => AppSettings.GetValueOrDefault(nameof(MeasurementRunning), 0);
            set => AppSettings.AddOrUpdateValue(nameof(MeasurementRunning), value);
        }

        public static string SleepTime
        {
            get => AppSettings.GetValueOrDefault(nameof(SleepTime), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(SleepTime), value);
        }


        //email
        public static bool AlertLow
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertLow), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertLow), value);
        }

        public static bool AlertMiddle
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertMiddle), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertMiddle), value);
        }

        public static bool AlertHigh
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertHigh), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertHigh), value);
        }

        public static bool AlertMovement
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertMovement), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertMovement), value);
        }
        public static bool AlertMovementTime
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertMovementTime), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertMovementTime), value);
        }

        public static bool AlertFall
        {
            get => AppSettings.GetValueOrDefault(nameof(AlertFall), false);
            set => AppSettings.AddOrUpdateValue(nameof(AlertFall), value);
        }
    }
}
