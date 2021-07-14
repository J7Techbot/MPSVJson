using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.CodeLists
{
    class CodeListData 
    {
        public string id;
        public string kod;
        public string uni_kod;
        public string kodLau;
        public Dictionary<string, string> nazev;
        public Dictionary<string, string> vhodnostProNevidome;
        public Dictionary<string, string> vhodnostProSlabozrake;
        public List<Dictionary<string, string>> oboryCinnosti;

    }
}
