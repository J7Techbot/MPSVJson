using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.Models
{
    class JobsPackage
    {
        public List<Item> polozky { get; private set; }
        public JobsPackage()
        {
            polozky = new List<Item>();
        }
        
    }
}
