using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class Benefit
    {
        public Dictionary<string, string> vyhoda;
        public string popis;

        public Benefit()
        {
            vyhoda = new Dictionary<string, string>();
        }
    }
}
