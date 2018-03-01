using MailKit.Net.Smtp;
using MimeKit;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class AlertSelectionViewModel : BaseViewModel
    {
        public ICommand SaveAlerts { get; private set; }

        /*
        0- Push notifikácia
        1- Notifikácia správou
        2- Zvukový alarm
        */
        private int alertPulseSlabe;
        public int AlertPulseSlabe
        {
            get { return alertPulseSlabe; }
            set
            {
                alertPulseSlabe = value;
                OnPropertyChanged();
            }
        }

        private int alertPulseStredne;
        public int AlertPulseStredne
        {
            get { return alertPulseStredne; }
            set
            {
                alertPulseStredne = value;
                OnPropertyChanged();
            }
        }

        private int alertPulseVysoke;
        public int AlertPulseVysoke
        {
            get { return alertPulseVysoke; }
            set
            {
                alertPulseVysoke = value;
                OnPropertyChanged();
            }
        }

        private int longTimeNoMovement;
        public int LongTimeNoMovement
        {
            get { return longTimeNoMovement; }
            set
            {
                longTimeNoMovement = value;
                OnPropertyChanged();
            }
        }

        private int signalLost;
        public int SignalLost
        {
            get { return signalLost; }
            set
            {
                signalLost = value;
                OnPropertyChanged();
            }
        }

        private int fallDetected;
        public int FallDetected
        {
            get { return fallDetected; }
            set
            {
                fallDetected = value;
                OnPropertyChanged();
            }
        }



        public AlertSelectionViewModel()
        {

            SaveAlerts = new Command(saveAlerts);

        }

        public void saveAlerts()
        {
            
            SettingsController.AlertPulseSlabe = AlertPulseSlabe;
            SettingsController.AlertPulseStredne = AlertPulseStredne;
            SettingsController.AlertPulseVysoke = AlertPulseVysoke;
            SettingsController.LongTimeNoMovement = LongTimeNoMovement;
            SettingsController.SignalLost = SignalLost;
            SettingsController.FallDetected = FallDetected;
            
            //Console.WriteLine("seruuuusssssssssssss {0} {1} {2} {3} {4} {5}", Settings.AlertPulseSlabe, Settings.AlertPulseStredne, Settings.AlertPulseVysoke, Settings.LongTimeNoMovement, Settings.SignalLost, Settings.FallDetected);
            
            //TODO: screen kde prida ostatne potrebne nastavenia, cas pre pohyb, email 

            //SendEmailMessage();
            //GeneratePushAlert();
        }


        public void GeneratePushAlert()
        {
            CrossLocalNotifications.Current.Show("title", "body");


        }

        public void SendEmailMessage()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "ahojakosamas94@gmail.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "ginter.jakub@gmail.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ahojakosamas94@gmail.com", "Jakub1994");

                client.Send(message);
                client.Disconnect(true);
            }


        }

    }
}
