using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models
{
    public class Profile
    {
        
        public int ProfileId { get; set; }

        public string Meno { get; set; }

        public string Priezvisko { get; set; }

        public string Adresa { get; set; }

        public string DatumNarodenia { get; set; }

        public float Vaha { get; set; }

        public float Vyska { get; set; }

        public string Pohlavie { get; set; }

        public string Ochorenia { get; set; }

        public string Poistovna { get; set; }

        /*public override string ToString()
        {
            return $"({Id}) {Meno}, {Priezvisko}, {Adresa}, {DatumNarodenia}, {Vaha}, {Vyska},  {Pohlavie}, {Ochorenia}, {Poistovna}";
        }*/

    }
}
