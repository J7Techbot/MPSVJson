using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class WorkPlace
    {
        
        public string email;
        public string nazev;
        public string telefon;
        public WorkPlaceAddres adresa;

        public WorkPlace()
        {
            adresa = new WorkPlaceAddres();
        }
    }
}
