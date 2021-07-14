using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class JobLocation
    {
        
        public Dictionary<string, string> obec;
        public List<Dictionary<string, string>> okresy;
        public string adresaText;
        public List<WorkPlace> pracoviste;

        public JobLocation()
        {            
            obec = new Dictionary<string, string>();
            okresy = new List<Dictionary<string, string>>();
            pracoviste = new List<WorkPlace>();
        }
    }
}
