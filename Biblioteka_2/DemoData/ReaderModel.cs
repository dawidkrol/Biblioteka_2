using System;
using System.Collections.Generic;
using System.Text;
using static Biblioteka_2.Data.GetDeomoData;

namespace Biblioteka_2.DemoData
{
    public class ReaderModel
    {
        public int Nr_czytelnika { get; set; }
        public string Tytuł { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public float Opłata { get; set; }
        public float Wartość { get; set; }
        public string Profil { get; set; }
    }
}
