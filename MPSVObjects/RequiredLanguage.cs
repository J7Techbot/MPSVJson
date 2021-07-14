using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class RequiredLanguage
    {
        public Dictionary<string, string> urovenZnalosti;
        public Dictionary<string, string> jazyk;
        public string popis;

        public RequiredLanguage()
        {
            urovenZnalosti = new Dictionary<string, string>();
            jazyk = new Dictionary<string, string>();
        }
    }
}
