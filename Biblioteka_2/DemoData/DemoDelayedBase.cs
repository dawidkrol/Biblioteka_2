using System;
using System.Collections.Generic;

namespace Biblioteka_2.DemoData
{
    public class DemoDelayedBase
    {
        public string ISBN { get; set; }
        public int dni { get; set; }
        public DateTime Spodziewana_Data_Zwrotu { get; set; }
        public DateTime Data_Wypożyczenia { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Tytuł { get; set; }
        public string Autor { get; set; }
        public string Wydawca { get; set; }
        public string oplata { get; set; }
    }
}