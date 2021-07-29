using MPSVJson.CodeLists;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace MPSVJson.Models
{
    class JSONHandler
    {
        string url = "https://data.mpsv.cz/od/soubory/volna-mista/volna-mista.json";
        string jobs_path = @"D:\exe\jobs\code_lists\volna-mista.json";               
        string code_list_path = @"D:\exe\jobs\code_lists\";
        //string code_list_path = @"D:\Dropbox\Uniweb\Files\";
        //string jobs_path = @"D:\Dropbox\Uniweb\Files\volna-mista.json";

        public JobsPackage jobsPackage { get; private set; }
        public CodeList benefits;
        public CodeList branchActivities;
        public CodeList contactWorkPlaces;
        public CodeList educationDetail;
        public CodeList educationKKov;
        public CodeList employmentRelations;
        public CodeList languages;
        public CodeList levelOfLanguage;
        public CodeList publishVpm;
        public CodeList shifts;
        public CodeList skills;
        public CodeList suitability;
        public CodeList suitabilityFor;
        public CodeList typeOfPlaces;
        public CodeList typeOfWages;
        public CodeList isco;
        public CodeList obce;
        public CodeList okresy;
        public CodeList kraje;
        private List<CodeList> codeLists;
        
        public JSONHandler()
        {
            
            jobsPackage = new JobsPackage();
            codeLists = new List<CodeList>();

            benefits = new CodeList("vyhody-volneho-mista.json", "vyhodyVolnehoMista");
            codeLists.Add(benefits);
            branchActivities = new CodeList("obory-cinnosti-pro-vm.json","obory");
            codeLists.Add(branchActivities);
            contactWorkPlaces = new CodeList("kontaktni-pracoviste.json", "kontaktniPracoviste");
            codeLists.Add(contactWorkPlaces);
            educationDetail = new CodeList("vzdelani-detailni-kategorie.json", "vzdelaniDetailniKategorie");
            codeLists.Add(educationDetail);
            educationKKov = new CodeList("vzdelani-kkov.json", "vzdelaniKkov");
            codeLists.Add(educationKKov);
            employmentRelations = new CodeList("pracovnepravni-vztahy.json", "pracovnepravniVztahy");
            codeLists.Add(employmentRelations);
            languages = new CodeList("jazyky.json", "jazyky");
            codeLists.Add(languages);
            levelOfLanguage = new CodeList("urovne-znalosti-jazyka.json", "urovneZnalostiJazyka");
            codeLists.Add(levelOfLanguage);
            publishVpm = new CodeList("zverejnovat-vpm.json", "zverejnovatVpm");
            codeLists.Add(publishVpm);
            shifts = new CodeList("smennosti.json", "smennosti");
            codeLists.Add(shifts);
            skills = new CodeList("dovednosti.json", "dovednosti");
            codeLists.Add(skills);
            suitability = new CodeList("vhodnosti.json", "vhodnosti");
            codeLists.Add(suitability);
            suitabilityFor = new CodeList("vhodnosti-pro-typ-zamestnance.json", "vhodnostiProTypZamestnance");
            codeLists.Add(suitabilityFor);
            typeOfPlaces = new CodeList("typy-mista-vykonu-prace.json", "typyMistaVykonuPrace");
            codeLists.Add(typeOfPlaces);
            typeOfWages = new CodeList("typy-mzdy.json", "typyMzdy");
            codeLists.Add(typeOfWages);
            isco = new CodeList("cz-isco.json", "czIsco");
            codeLists.Add(isco);
            obce = new CodeList("obce.json", "obce");
            codeLists.Add(obce);
            okresy = new CodeList("okresy.json", "okresy");
            codeLists.Add(okresy);
            kraje = new CodeList("kraje.json", "kraje");
            codeLists.Add(kraje);
        }
        public JobsPackage GetJsonFromFile()
        {
            using (StreamReader file = File.OpenText(jobs_path))
            {
                JsonSerializer serializer = new JsonSerializer();
                jobsPackage = (JobsPackage)serializer.Deserialize(file, typeof(JobsPackage));
            }
            return jobsPackage;
        }
        public List<CodeList> LoadCodeLists()
        {
            for (int i = 0; i < codeLists.Count; i++)
            {                
                using (StreamReader file = File.OpenText(code_list_path + codeLists[i].FileName))
                {
                    
                    JsonSerializer serializer = new JsonSerializer();
                    CodeList newList = (CodeList)serializer.Deserialize(file, typeof(CodeList));
                    codeLists[i].polozky = newList.polozky;                    
                }
            }
            return codeLists;
        }

        public JobsPackage GetJsonFromUrl()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "application/json; charset=utf-8";
            var response = (HttpWebResponse)httpWebRequest.GetResponse();            
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JsonSerializer serializer = new JsonSerializer();
                jobsPackage = (JobsPackage)serializer.Deserialize(sr, typeof(JobsPackage));
            }
            
            return jobsPackage;
        }


    }
}
