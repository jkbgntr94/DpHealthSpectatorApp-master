using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.SekvenceHelper
{
    public class SequenceCreator
    {

        public void TemperatureSequencer(DatabaseContext context)
        {

            var all1 = context.Temperature.ToList();
            LimitCheck limitCheck = new LimitCheck();

            Hranice_Teplota htep = context.TemperatureLimit.FirstOrDefault(t => t.Hranice_TeplotaId == context.TemperatureLimit.Max(x => x.Hranice_TeplotaId));

            foreach (var a in all1)
            {/*Inicializacia ak ziadna sekvencia neexistuje*/
                if (!context.TemperatureSekv.Any())
                {

                    Teplota_Sekvencia tmps = new Teplota_Sekvencia
                    {
                        TeplSekvId = 1,
                        Sekvencia = a.Hodnota,
                        TimeStart = a.TimeStamp,
                        TimeClose = "",
                        Upozornenie = limitCheck.CheckTemperatureLimits(context, a.Hodnota),
                        //Hranice_Teplota = htep,
                        Hranice_TeplotaFk = htep.Hranice_TeplotaId
                    };

                    Teplota t = context.Temperature.Where(c => c.TeplotaId == a.TeplotaId).First();
                    t.TeplSekvFk = 1;
                    //System.Diagnostics.Debug.WriteLine("Novasekvencia " + t.TeplotaId + " " + a.TeplotaId + " " + t.Hodnota + " + " + t.TeplSekvFk);

                    context.TemperatureSekv.Add(tmps);
                    context.Temperature.Update(t);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else /*Ak existuju sekvencie*/
                {
                    /*Najdi otvorenu sekvenciu*/

                    Teplota_Sekvencia tmps = null;
                    try
                    {
                        tmps = context.TemperatureSekv.FirstOrDefault(t => t.TimeClose == "");
                        //System.Diagnostics.Debug.WriteLine("otvorena sekvencia " + tmps.TeplSekvId + " " + tmps.Sekvencia);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    /*Spracovavana hodnota patri do aktualne otvorenej sekvencie*/
                    if (limitCheck.CheckTemperatureLimits(context, a.Hodnota) == tmps.Upozornenie)
                    {
                        var allInSekv = context.Temperature.Where(p => p.TeplSekvFk == tmps.TeplSekvId).ToList();
                        float median = 0;
                        /*Aktualizuj hodnotu sekvencie*/

                        foreach (var allIN in allInSekv)
                        {
                            median += allIN.Hodnota;

                        }
                        median += a.Hodnota;
                        median = median / (allInSekv.Count + 1);

                        a.TeplSekvFk = tmps.TeplSekvId; //naviaz teplotu
                        context.Temperature.Update(a);

                        tmps.Sekvencia = median;
                        context.TemperatureSekv.Update(tmps);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                    else /*Spracovavana hodnota nepatri do aktualne otvorenej sekvencie*/
                    {
                        /* Zatvor poslednu sekvenciu*/

                        //najdi poslednu
                        Teplota_Sekvencia ts = context.TemperatureSekv.FirstOrDefault(t => t.TeplSekvId == context.TemperatureSekv.Max(x => x.TeplSekvId));

                        var allInSekv1 = context.Temperature.Where(p => p.TeplSekvFk == ts.TeplSekvId).ToList();// najdi list pre poslednu
                        var last = allInSekv1[allInSekv1.Count - 1]; //zober posledny 
                        ts.TimeClose = last.TimeStamp; // nastav konecny TS podla TS posledneho v sekvencii
                        context.TemperatureSekv.Update(ts);
                        context.Temperature.RemoveRange(allInSekv1);


                        /*Vytvor novu sekvenciu*/
                        Teplota_Sekvencia tmps1 = new Teplota_Sekvencia
                        {
                            TeplSekvId = ts.TeplSekvId + 1,
                            Sekvencia = a.Hodnota,
                            TimeStart = a.TimeStamp,
                            TimeClose = "",
                            Upozornenie = limitCheck.CheckTemperatureLimits(context, a.Hodnota),
                            //Hranice_Teplota = htep,
                            Hranice_TeplotaFk = htep.Hranice_TeplotaId
                        };

                        context.TemperatureSekv.Add(tmps1);

                        a.TeplSekvFk = ts.TeplSekvId + 1;  //naviaz pulz na sekvenciu
                        context.Temperature.Update(a);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }

                }

            }
        }


        public void PulseSequencer(DatabaseContext context)
        {
            var all1 = context.Pulse.ToList();
            LimitCheck limitCheck = new LimitCheck();

            Hranice_Tep ht = context.PulseLimit.FirstOrDefault(h => h.Hranica_TepId == context.PulseLimit.Max(x => x.Hranica_TepId));


            foreach (var a in all1)
            {
                /*Inicializacia ak ziadna sekvencia neexistuje*/
                if (!context.PulseSekv.Any())
                {
                    Tep_Sekvencia tepS = new Tep_Sekvencia
                    {
                        TepSekvId = 1,
                        Sekvencia = (int)Math.Round(a.Hodnota),
                        TimeStart = a.TimeStamp,
                        TimeClose = "",
                        Upozornenie = limitCheck.CheckPulseLimits(context, (int)Math.Round(a.Hodnota)),
                        //Hranice_Tep = ht,
                        Hranica_TepFK = ht.Hranica_TepId,
                        

                    };

                    Tep t = context.Pulse.Where(c => c.TepId == a.TepId).First();
                    t.TepSekvId = 1;


                    //System.Diagnostics.Debug.WriteLine("Novasekvencia " + t.TepId + " " + a.TepId + " " + t.Hodnota + " + " + t.TepSekvId);

                    context.PulseSekv.Add(tepS);
                    context.Pulse.Update(t);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Nemozem najst otvorenu sekvenciu");
                    }

                }
                /*Ak existuju sekvencie*/
                else
                {
                    /*Najdi otvorenu sekvenciu*/
                    Tep_Sekvencia tepS = null;
                    try
                    {
                        tepS = context.PulseSekv.FirstOrDefault(t => t.TimeClose == "");
                        //System.Diagnostics.Debug.WriteLine("otvorena sekvencia " + tepS.TepSekvId + " " + tepS.Sekvencia);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Nemozem najst otvorenu sekvenciu");
                    }

                    /*Spracovavana hodnota patri do aktualne otvorenej sekvencie*/
                    if (limitCheck.CheckPulseLimits(context, (int)Math.Round(a.Hodnota)) == tepS.Upozornenie)
                    {
                        var allInSekv = context.Pulse.Where(p => p.TepSekvId == tepS.TepSekvId).ToList();
                        int median = 0;
                        /*Aktualizuj hodnotu sekvencie*/

                        foreach (var allIN in allInSekv)
                        {
                            median += (int)Math.Round(allIN.Hodnota);

                        }
                        median += (int)Math.Round(a.Hodnota);
                        median = median / (allInSekv.Count + 1);

                        a.TepSekvId = tepS.TepSekvId; //naviaz tep
                        //a.Tep_Sekvencia = tepS;
                        context.Pulse.Update(a);

                        tepS.Sekvencia = median;
                        context.PulseSekv.Update(tepS);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Nemozem najst otvorenu sekvenciu");
                        }
                    }
                    else /*Spracovavana hodnota nepatri do aktualne otvorenej sekvencie*/
                    {
                        /* Zatvor poslednu sekvenciu*/
                        Tep_Sekvencia ts = context.PulseSekv.FirstOrDefault(t => t.TepSekvId == context.PulseSekv.Max(x => x.TepSekvId)); //najdi poslednu

                        var allInSekv1 = context.Pulse.Where(p => p.TepSekvId == ts.TepSekvId).ToList();// najdi list pre poslednu
                        var last = allInSekv1[allInSekv1.Count - 1]; //zober posledny pulz
                        ts.TimeClose = last.TimeStamp; // nastav konecny TS podla TS posledneho v sekvencii
                        context.PulseSekv.Update(ts);
                        context.Pulse.RemoveRange(allInSekv1);

                        /*Vytvor novu sekvenciu*/
                        Tep_Sekvencia tepS1 = new Tep_Sekvencia
                        {
                            TepSekvId = ts.TepSekvId + 1,
                            Sekvencia = (int)Math.Round(a.Hodnota),
                            TimeStart = a.TimeStamp,
                            TimeClose = "",
                            Upozornenie = limitCheck.CheckPulseLimits(context, (int)Math.Round(a.Hodnota)),
                            //Hranice_Tep = ht,
                            Hranica_TepFK = ht.Hranica_TepId

                        };

                        context.PulseSekv.Add(tepS1);
                        

                        a.TepSekvId = ts.TepSekvId + 1;  //naviaz pulz na sekvenciu
                        //a.Tep_Sekvencia = tepS1;

                        context.Pulse.Update(a);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Nemozem najst otvorenu sekvenciu");
                        }
                    }

                }

            }
        }

        public void MovementSequencer(DatabaseContext context)
        {
            var allMovements = context.Movement.ToList();
            LimitCheck limitCheck = new LimitCheck();

            Hranice_Pohyb hpoh = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));

            foreach (var mov in allMovements)
            {
                Izby izba = new RoomsDetection().findRoom(mov);
                
               
                /*Inicializacia ak ziadna sekvencia neexistuje*/
                if (!context.MovementSekv.Any())
                {
                    Pohyb_Sekvencia pohS = null;
                    if (izba == null)
                    {
                        pohS = new Pohyb_Sekvencia
                        {
                            PohSekvId = 1,
                            Xhodnota = mov.Xhodnota,
                            Yhodnota = mov.Yhodnota,
                            TimeStamp = mov.TimeStamp,
                            Cas_Zotrvania = "",
                            Upozornenie_Cas = 0,
                            Upozornenie_Hranica = limitCheck.checkIfOutside(context, mov),
                            //Hranice_Pohyb = hpoh,
                            HranicePohybFK = hpoh.HranicePohybId,
                            
                        };

                    }
                    else
                    {
                        pohS = new Pohyb_Sekvencia
                        {
                            PohSekvId = 1,
                            Xhodnota = mov.Xhodnota,
                            Yhodnota = mov.Yhodnota,
                            TimeStamp = mov.TimeStamp,
                            Cas_Zotrvania = "",
                            Upozornenie_Cas = 0,
                            Upozornenie_Hranica = limitCheck.checkIfOutside(context, mov),
                            //Hranice_Pohyb = hpoh,
                            HranicePohybFK = hpoh.HranicePohybId,
                            //Izby = izba,
                            IzbyFK = izba.IzbaID
                        };

                    }

                    

                    //TODO: ked je izba prazdna vytvor to bez izby

                    Pohyb p = context.Movement.Where(c => c.PohybId == mov.PohybId).First();
                    p.PohSekvFK = 1;
                    System.Diagnostics.Debug.WriteLine("Inicializacia ak ziadna sekvencia neexistuje");

                    context.MovementSekv.Add(pohS);
                    //context.Movement.Remove(mov);

                    //context.Movement.Update(p);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                /*Ak existuju sekvencie*/
                else
                {
                    /*Najdi otvorenu sekvenciu*/
                    Pohyb_Sekvencia pohS = null;
                    try
                    {
                        pohS = context.MovementSekv.FirstOrDefault(p => p.PohSekvId == context.MovementSekv.Max(x => x.PohSekvId));

                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Nemozem najst otvorenu sekvenciu");
                    }

                    /*Spracovavana hodnota patri do aktualne otvorenej sekvencie*/
                    if (limitCheck.checkMovementValue(context, mov, pohS))
                    {
                        System.Diagnostics.Debug.WriteLine("Spracovavana hodnota patri do aktualne otvorenej sekvencie");
                        //mov.Pohyb_Sekvencia = pohS;//naviaz pohyb
                        mov.PohSekvFK = pohS.PohSekvId;

                        //cas zotrvania
                        DateTime convertedDate = DateTime.Parse(pohS.TimeStamp);
                        String durationTime = null;
                        try
                        {
                            DateTime endtime = DateTime.Parse(mov.TimeStamp);
                            durationTime = (endtime - convertedDate).TotalMinutes.ToString();
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                            durationTime = "NA";
                        }

                        pohS.Cas_Zotrvania = durationTime;


                        //casove upozornenie

                        pohS.Upozornenie_Cas = limitCheck.checkTimeLimitMovement(context, pohS);

                       
                        context.MovementSekv.Update(pohS);
                        context.Movement.Remove(mov);
                        //context.Movement.Update(mov);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                    }
                    else /*Spracovavana hodnota nepatri do aktualne otvorenej sekvencie*/
                    {
                        
                        Pohyb_Sekvencia last = context.MovementSekv.FirstOrDefault(t => t.PohSekvId == context.MovementSekv.Max(x => x.PohSekvId));
                        Pohyb_Sekvencia pohnew = null;
                        if (izba == null)
                        {
                            pohnew = new Pohyb_Sekvencia
                            {
                                PohSekvId = last.PohSekvId + 1,
                                Xhodnota = mov.Xhodnota,
                                Yhodnota = mov.Yhodnota,
                                TimeStamp = mov.TimeStamp,
                                Cas_Zotrvania = "",
                                Upozornenie_Cas = 0,
                                Upozornenie_Hranica = limitCheck.checkIfOutside(context, mov),
                                //Hranice_Pohyb = hpoh,
                                HranicePohybFK = hpoh.HranicePohybId,
                               
                            };

                        }
                        else
                        {
                            pohnew = new Pohyb_Sekvencia
                            {
                                PohSekvId = last.PohSekvId + 1,
                                Xhodnota = mov.Xhodnota,
                                Yhodnota = mov.Yhodnota,
                                TimeStamp = mov.TimeStamp,
                                Cas_Zotrvania = "",
                                Upozornenie_Cas = 0,
                                Upozornenie_Hranica = limitCheck.checkIfOutside(context, mov),
                                //Hranice_Pohyb = hpoh,
                                HranicePohybFK = hpoh.HranicePohybId,
                                //Izby = izba,
                                IzbyFK = izba.IzbaID
                            };

                        }

                        

                        Pohyb p = context.Movement.Where(c => c.PohybId == mov.PohybId).First();
                        //p.PohSekvFK = last.PohSekvId + 1;
                        //p.Pohyb_Sekvencia = pohnew;

                        System.Diagnostics.Debug.WriteLine("Spracovavana hodnota nepatri do aktualne otvorenej sekvencie");

                        context.MovementSekv.Add(pohnew);
                        //context.Movement.Remove(mov);
                        //context.Movement.Update(p);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                    }


                }



            }

        }
    }
}
