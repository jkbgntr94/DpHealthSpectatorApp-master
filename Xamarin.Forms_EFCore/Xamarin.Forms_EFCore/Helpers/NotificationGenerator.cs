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

        public void GenerateNotification(String velicina, float hodnota, string timestamp, int upozornenie, int ID)
        {
            if (velicina == "Teplota" || velicina == "Tep")
            {
                int alertKind = 0;

                switch (upozornenie)
                {
                    case -1:
                        alertKind = SettingsController.AlertPulseSlabe;
                        if (SettingsController.AlertLow)
                        {
                            SendEmailMessagePandT(velicina, hodnota, timestamp, upozornenie, ID);
                        }
                        break;
                    case 1:
                        alertKind = SettingsController.AlertPulseSlabe;
                        if (SettingsController.AlertLow)
                        {
                            SendEmailMessagePandT(velicina, hodnota, timestamp, upozornenie, ID);
                        }
                        break;
                    case 2:
                        alertKind = SettingsController.AlertPulseStredne;
                        if (SettingsController.AlertMiddle)
                        {
                            SendEmailMessagePandT(velicina, hodnota, timestamp, upozornenie, ID);
                        }
                        break;
                    case 3:
                        alertKind = SettingsController.AlertPulseVysoke;
                        if (SettingsController.AlertHigh)
                        {
                            SendEmailMessagePandT(velicina, hodnota, timestamp, upozornenie, ID);
                        }
                        break;
                }

                if (alertKind == 0) {
                    GeneratePushAlert(velicina,  hodnota,  timestamp,  upozornenie,  ID);

                }else if(alertKind == 1)
                {
                    alarmNotification(velicina, hodnota, timestamp, upozornenie, ID);

                }
                
            }

        }

        public void GeneratePustAlertMovOut(string timestamp, int ID)
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

            string title = "Pacient opustil domov";

            string body = "V čase " + cas + " boli prekročené okrajové hranice";
            CrossLocalNotifications.Current.Show(title, body, ID);

            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Volume = 1;
            player.Load("tap_tap_tap.mp3");
            player.Play();

            if (SettingsController.AlertMovement)
            {
                SendEmailMessageMovOut(timestamp, ID);


            }

        }

        public void GeneratePustAlertMovTime(string roomName, string timestamp,string durationTime, int ID)
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

            string title = "Prekročený časový limit v " + roomName;

            string body = "V čase " + cas + " bol pacient bez pohybu už " + durationTime +" min";
            CrossLocalNotifications.Current.Show(title, body, ID);

            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Volume = 1;
            player.Load("tap_tap_tap.mp3");
            player.Play();

            if (SettingsController.AlertMovementTime)
            {
                SendEmailMessageMovTime(roomName, timestamp, durationTime, ID);


            }

        }



        public void GeneratePustAlertFall(string izbaName, string timestamp, int ID)
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

            string title = "V miestnosti " + izbaName + "nastal pád";

            string body = "V čase " + cas + " nastal pád";
            CrossLocalNotifications.Current.Show(title, body, ID);

            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Volume = 1;
            player.Load("tap_tap_tap.mp3");
            player.Play();

            if (SettingsController.AlertFall)
            {
                SendEmailMessageFall(izbaName,timestamp,ID);


            }
            
        }



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



        

        public void SendEmailMessagePandT(String velicina, float hodnota, string timestamp, int upozornenie, int ID)
        {
            SettingsController.NameEmail = "Jakub Ginter";
            SettingsController.Email = "ginter.jakub@gmail.com";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HEALTH SPECTATOR", "ahojakosamas94@gmail.com"));
            message.To.Add(new MailboxAddress(SettingsController.NameEmail, SettingsController.Email));

            string upozornenieNazov = new LimitCheck().getStringValuePulseAndTempLimit(upozornenie);
            message.Subject = "Upozornenie: " + velicina + " - "+upozornenieNazov;

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

            string hodnotaUprav = "";
            if (velicina == "Teplota")
            {
                hodnotaUprav = hodnota.ToString("n2") + " °C ";
            }
            else if (velicina == "Tep")
            {
                hodnotaUprav = (int)Math.Round(hodnota) + " BPM ";


            }

            message.Body = new TextPart("plain")
            {
                Text = @"Aplikácia Health Spectator práve zaznamenala prekročenie hodnôt!
            Veličina: " + velicina + " Upozornenie: " + upozornenieNazov + " Hodnota: " + hodnotaUprav + " Čas: " + cas
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

        public void SendEmailMessageFall(String izba, string timestamp, int ID)
        {
            SettingsController.NameEmail = "Jakub Ginter";
            SettingsController.Email = "ginter.jakub@gmail.com";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HEALTH SPECTATOR", "ahojakosamas94@gmail.com"));
            message.To.Add(new MailboxAddress(SettingsController.NameEmail, SettingsController.Email));

            message.Subject = "Upozornenie: V miestnosti " + izba + " nastal pád";

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

           
            message.Body = new TextPart("plain")
            {
                Text = @"Aplikácia Health Spectator práve zaznamenala pád!
            V čase " + cas + " nastal v miestnosti " + izba + "pád"
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


        public void SendEmailMessageMovOut( string timestamp, int ID)
        {
            SettingsController.NameEmail = "Jakub Ginter";
            SettingsController.Email = "ginter.jakub@gmail.com";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HEALTH SPECTATOR", "ahojakosamas94@gmail.com"));
            message.To.Add(new MailboxAddress(SettingsController.NameEmail, SettingsController.Email));

            message.Subject = "Upozornenie: Boli prekročené okrajové hranice domu";

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


            message.Body = new TextPart("plain")
            {
                Text = @"Aplikácia Health Spectator práve zaznamenala prekročenie hraníc!
            V čase " + cas + " pacient opustil domov"
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

        public void SendEmailMessageMovTime(string roomName, string timestamp, string durationTime, int ID)
        {
            SettingsController.NameEmail = "Jakub Ginter";
            SettingsController.Email = "ginter.jakub@gmail.com";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HEALTH SPECTATOR", "ahojakosamas94@gmail.com"));
            message.To.Add(new MailboxAddress(SettingsController.NameEmail, SettingsController.Email));

            message.Subject = "Upozornenie: V miestnosti " + roomName + " bol prekročený časový limit";

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


            message.Body = new TextPart("plain")
            {
                Text = @"Aplikácia Health Spectator práve zaznamenala prekročenie časovéhu limitu bez výrazného pohybu!
            V čase " + cas + " sa pacient výrazne nehýbal už " + durationTime + " minút."
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
