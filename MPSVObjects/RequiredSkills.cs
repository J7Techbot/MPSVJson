using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.MPSVObjects
{
    class RequiredSkills
    {
        public Dictionary<string, string> dovednost;
        public string popis;

        public RequiredSkills()
        {
            dovednost = new Dictionary<string, string>();
        }
    }
}
