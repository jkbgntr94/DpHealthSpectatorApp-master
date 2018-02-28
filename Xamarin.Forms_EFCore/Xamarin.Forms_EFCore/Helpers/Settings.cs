using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Helpers
{
    public static class Settings
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
    }
}
