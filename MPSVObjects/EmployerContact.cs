using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class EmployerContact
    {
        public string jmeno;
        public Dictionary<string, string> poziceVeSpolecnosti;
        public string prijmeni;
        public string titulPredJmenem;
        public string titulZaJmenem;

        public EmployerContact()
        {
            poziceVeSpolecnosti = new Dictionary<string, string>();
        }
    }
}
