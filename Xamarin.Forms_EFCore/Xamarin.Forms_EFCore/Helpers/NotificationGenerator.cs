using MimeKit;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;


namespace Xamarin.Forms_EFCore.Helpers
{
    public class NotificationGenerator
    {
        public void GeneratePushAlert(String velicina, float hodnota, string timestamp, int upozornenie,int ID)
        {
            string cas = "NA";
            try
            {
                DateTime convertedDate = DateTime.Parse(timestamp);
                cas = convertedDate.ToShortTimeString();
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Parsovanie datumu " + e.ToString());

            }
            string upozornenieNazov = new LimitCheck().getStringValuePulseAndTempLimit(upozornenie);
            string title = "";
            if (velicina == "Teplota")
            {
                title = velicina + ": " + upozornenieNazov + " upozornenie. Hodnota: " + hodnota.ToString("n2") + " °C";
            }else if(velicina == "Tep")
            {
                title = velicina + ": " + upozornenieNazov + " upozornenie. Hodnota: " + (int)Math.Round(hodnota) + " BPM";


            }
            string body = "V čase " + cas + " bolo vytvorené " + upozornenieNazov + " upozornenie.";
            CrossLocalNotifications.Current.Show(title, body, ID);

            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Volume = 1;
            player.Load("tap_tap_tap.mp3");
            player.Play();

        }

        public void alarmNotification(String velicina, float hodnota, string timestamp, int upozornenie, int ID)
        {
                string cas = "NA";
                try
                {
                    DateTime convertedDate = DateTime.Parse(timestamp);
                    cas = convertedDate.ToShortTimeString();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Parsovanie datumu " + e.ToString());

                }
                string upozornenieNazov = new LimitCheck().getStringValuePulseAndTempLimit(upozornenie);
                string title = "";
                if (velicina == "Teplota")
                {
                    title = "ALARM " + velicina + ": " + upozornenieNazov + " upozornenie. " + hodnota.ToString("n2") + " °C";
                }
                else if (velicina == "Tep")
                {
                    title ="ALARM " + velicina + ": " + upozornenieNazov + " upozornenie. " + (int)Math.Round(hodnota) + " BPM";


                }
                string body = "V čase " + cas + " bolo vytvorené " + upozornenieNazov + " upozornenie.";
                CrossLocalNotifications.Current.Show(title, body, ID);

                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Volume = 1;
                player.Load("alarm.mp3");
                player.Play();

        }



        

        public void SendEmailMessage()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HEALTH SPECTATOR", "ahojakosamas94@gmail.com"));
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
