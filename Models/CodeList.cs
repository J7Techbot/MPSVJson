using System;
using System.Collections.Generic;
using System.Text;

namespace MPSVJson.CodeLists
{
    class CodeList 
    {        
        public string FileName { get; private set; }
        public string Type { get; private set; }

        public List<CodeListData> polozky;

        public CodeList(string filename,string type)
        {
            FileName = filename;
            Type = type;
            polozky = new List<CodeListData>();
        }

        public string GetName(string id)
        {
            string r = "";
            foreach (CodeListData x in polozky)
            {
                if (id == x.id)
                {
                    r = x.nazev["cs"];
                    
                }
            }
            return r;
        }
        public string GetUniweb(string id)
        {
            string r = "";
            foreach (CodeListData x in polozky)
            {
                if (id == x.id)
                {
                    r = x.uni_kod;
                }                
            }
            return r;
        }
        public string GetCode(string id)
        {
            string r = "";
            foreach (CodeListData x in polozky)
            {
                if (id == x.id)
                {
                    r = x.kod;
                }
            }
            return r;
        }
        public string GetLau(string id)
        {
            string r = "";
            foreach (CodeListData x in polozky)
            {
                if (id == x.id)
                {
                    r = x.kodLau;
                }
            }
            return r;
        }
        public List<string> GetObory(string id)
        {
            List<string> r = new List<string>();
            
            foreach (CodeListData x in polozky)
            {
                if (id == x.id)
                {
                    if(x.oboryCinnosti != null)
                    {
                        foreach(Dictionary<string,string> oid in x.oboryCinnosti)
                        {
                            r.Add(oid["id"]);
                        }                        
                    }                   
                }
            }
            return r;
        }
        
    }
}
