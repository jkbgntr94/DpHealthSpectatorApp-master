using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class DrugsHelper
    {
        DatabaseContext context;

        public DrugsHelper()
        {
            context = new DatabaseContext();

        }

        public string FindActiveDrug(string timestamp)
        {
            DateTime convertedDate = DateTime.Parse(timestamp);
            var allDrugs = context.DrugTakeTime.ToList();

            foreach(var d in allDrugs)
            {
                
                try
                {
                    DateTime takeDate = DateTime.Parse(d.Cas);
                    DateTime takeTime = DateTime.Parse(takeDate.ToShortTimeString());
                    DateTime convTime = DateTime.Parse(convertedDate.ToShortTimeString());

                    TimeSpan diff = convTime - takeTime;

                    if (diff.TotalMinutes <= 120)
                    {
                        Lieky liek = context.Drugs.Where(x => x.LiekId == d.LiekyFK).First();
                        return liek.Nazov;
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                }


                }
            return "";

        }

    }
}
