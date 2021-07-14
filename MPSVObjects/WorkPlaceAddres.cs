using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class WorkPlaceAddres
    {
        public Nullable<int> cisloDomovni;
        public string cisloOrientacni;
        public string dodatekAdresy;
        public Nullable<int> kodAdresnihoMista;
        public string psc;
        public Dictionary<string, string> kraj;
        public Dictionary<string, string> okres;
        public Dictionary<string, string> obec;
        public Dictionary<string, string> ulice;

        public WorkPlaceAddres()
        {
            kraj = new Dictionary<string, string>();
            okres = new Dictionary<string, string>();
            obec = new Dictionary<string, string>();
            ulice = new Dictionary<string, string>();
        }
            
            
    }
}
