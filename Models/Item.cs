using MPSVJson.CodeLists;
using MPSVJson.MPSVObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPSVJson.Models
{
    class Item
    {
        
        public int portalId;
        public string id;
        public string referencniCislo;
        public bool azylant;
        public bool cizinecMimoEu;
        public DateTime datumVlozeni;
        public DateTime datumZmeny; 
        public Nullable<float> mesicniMzdaDo; 
        public Nullable<float> mesicniMzdaOd;
        public bool modraKarta;
        public Nullable<float> pocetHodinTydne;  
        public int pocetMist;
        public Dictionary<string,string> pozadovanaProfese;                         
        public bool statniSpravaSamosprava;
        public Nullable<DateTime> terminUkonceniPracovnihoPomeru;
        public Nullable<DateTime> terminZahajeniPracovnihoPomeru;
        public bool souhlasAgenturyAgentura;
        public bool souhlasAgenturyUzivatel;
        public Dictionary<string, string> upresnujiciInformace; 
        public string urlAdresa; 
        public bool zamestnaneckaKarta;
        public Nullable<DateTime> expirace;
        public Dictionary<string, string> minPozadovaneVzdelani;
        public Dictionary<string, string> smennost;
        public Dictionary<string, string> typMzdy;
        public Dictionary<string, string> zverejnovat;
        public JobLocation mistoVykonuPrace;
        public Dictionary<string, string> zamestnavatel;
        public EmployerContact kontaktniOsobaZamestnavatele;
        public Dictionary<string, string> profeseCzIsco;
        public List<Dictionary<string, string>> pracovnePravniVztahy;
        public List<Dictionary<string, string>> vhodnostiPracovnihoMista;
        public Dictionary<string, string> kontaktniPracoviste;
        public List<Benefit> vyhodyVolnehoMista;
        public List<RequiredSkills> pozadovanaDovednost;
        public List<RequiredLanguage> pozadovanaJazykovaZnalost;
        public List<Dictionary<string, string>> pozadovanePovolani;
        public List<Dictionary<string, string>> pozadovaneVzdelani;
        public Dictionary<string, string> prvniKontaktSeZamestnavatelem;

        public FItem Format(List<CodeList> cl,Dictionary<string,string> portaly, Dictionary<string, string> hosts, List<Dictionary<string, object>> companies)
        {
            FItem fitem = new FItem();
            fitem.state = 3;
            fitem.azylant = azylant ? 1 : 0;
            fitem.cizinecMimoEu = cizinecMimoEu ? 1 : 0;
            fitem.datumVlozeni = datumVlozeni;
            fitem.datumZmeny = datumZmeny;
            fitem.expirace = expirace;            
            fitem.modraKarta = modraKarta ? 1 : 0;            
            fitem.pocetMist = pocetMist;
            fitem.portalID = portalId;
            fitem.referencniCislo = referencniCislo;
            fitem.souhlasAgenturyAgentura = souhlasAgenturyAgentura ? 1 : 0;
            fitem.souhlasAgenturyUzivatel = souhlasAgenturyUzivatel ? 1 : 0;
            fitem.statniSpravaSamosprava = statniSpravaSamosprava ? 1 : 0;
            fitem.terminUkonceniPracovnihoPomeru = terminUkonceniPracovnihoPomeru;
            fitem.terminZahajeniPracovnihoPomeru = terminZahajeniPracovnihoPomeru;
            fitem.uid = id;
            fitem.urlAdresa = urlAdresa;
            fitem.zamestnaneckaKarta = zamestnaneckaKarta ? 1 : 0;
            

            if (mesicniMzdaDo != null)
                fitem.mesicniMzdaDo = mesicniMzdaDo;
            if (mesicniMzdaOd != null)
                fitem.mesicniMzdaOd = mesicniMzdaOd;
            if (pocetHodinTydne != null)
                fitem.pocetHodinTydne = pocetHodinTydne;

            if (pozadovanaProfese != null)
            {
                fitem.pozadovanaProfese = pozadovanaProfese["cs"];
                fitem.title_url = CleanString(fitem.pozadovanaProfese) + "-" + portalId.ToString();
            }
            if (upresnujiciInformace != null)
                fitem.upresnujiciInformace = upresnujiciInformace["cs"].Replace("\n", "|"); 

            if (kontaktniPracoviste != null)
                fitem.kontaktniPracoviste = GetCodeList(cl, "kontaktniPracoviste").GetName(kontaktniPracoviste["id"]);
            if (minPozadovaneVzdelani != null)
            {
                fitem.minPozadovaneVzdelani = GetCodeList(cl, "vzdelaniDetailniKategorie").GetName(minPozadovaneVzdelani["id"]);
                fitem.pozadovaneVzdelani_uniweb = GetCodeList(cl, "vzdelaniDetailniKategorie").GetUniweb(minPozadovaneVzdelani["id"]);
            }
            if (profeseCzIsco != null)
            {
                fitem.profeseCzIsco = GetCodeList(cl, "czIsco").GetName(profeseCzIsco["id"]);
                fitem.profeseCzIsco_uniweb = GetCodeList(cl, "czIsco").GetCode(profeseCzIsco["id"]);
                List<string> obory = new List<string>();
                obory = GetCodeList(cl, "czIsco").GetObory(profeseCzIsco["id"]);

                int count = 1;
                foreach (string s in obory)
                {
                    fitem.obor_uniweb = GetCodeList(cl, "obory").GetCode(s);
                    fitem.obor_nazev = GetCodeList(cl, "obory").GetName(s);
                    if (count >= 1 && count != obory.Count)
                        fitem.obor_uniweb += ";";
                        count++;
                }                
            }
            if (smennost != null)
            {
                fitem.smennost = GetCodeList(cl, "smennosti").GetName(smennost["id"]);
                fitem.smennost_uniweb = GetCodeList(cl, "smennosti").GetUniweb(smennost["id"]);
            }
            if (typMzdy != null)
                fitem.typMzdy = GetCodeList(cl, "typyMzdy").GetName(typMzdy["id"]);
            if (zverejnovat != null)
                fitem.zverejnovat = GetCodeList(cl, "zverejnovatVpm").GetName(zverejnovat["id"]);

            if (zamestnavatel != null)
            {
                if (zamestnavatel["ico"] != null)
                {
                    fitem.zamestnavatel_ico = zamestnavatel["ico"];                    
                    //fitem.company = companies[zamestnavatel["ico"]]; creg = ico company = code
                    foreach(Dictionary<string,object> x in companies)
                    {
                        if (x["creg"].ToString() == zamestnavatel["ico"])
                        {
                            int val;
                            if (int.TryParse(x["code"].ToString(), out val))
                            {
                                fitem.company = val;                                                               
                            }
                            if (x["tarif"] != null)
                            {
                                fitem.tarif = (int)x["tarif"];
                            }
                            else
                                fitem.tarif = 0;

                            break;
                        }
                            
                    }
                    
                }
                if (zamestnavatel["nazev"] != null)
                    fitem.zamestnavatel_nazev = zamestnavatel["nazev"];
            }
            if (kontaktniOsobaZamestnavatele != null)
            {
                if(kontaktniOsobaZamestnavatele.jmeno != null)                
                    fitem.kontaktni_osoba_jmeno = kontaktniOsobaZamestnavatele.jmeno;
                if (kontaktniOsobaZamestnavatele.prijmeni != null)
                    fitem.kontaktni_osoba_prijmeni = kontaktniOsobaZamestnavatele.prijmeni;                
                if (kontaktniOsobaZamestnavatele.titulPredJmenem != null)
                    fitem.kontaktni_osoba_titul = kontaktniOsobaZamestnavatele.titulPredJmenem;

            }            
            if(prvniKontaktSeZamestnavatelem != null)
            {
                if (prvniKontaktSeZamestnavatelem["jmenoKontaktniOsoby"] != null)
                    fitem.kontakt_osoba_jmeno = prvniKontaktSeZamestnavatelem["jmenoKontaktniOsoby"];
                if (prvniKontaktSeZamestnavatelem["prijmeniKontaktniOsoby"] != null)
                    fitem.kontakt_osoba_prijmeni = prvniKontaktSeZamestnavatelem["prijmeniKontaktniOsoby"];
                if (prvniKontaktSeZamestnavatelem["titulPredJmenem"] != null)
                    fitem.kontakt_osoba_titul = prvniKontaktSeZamestnavatelem["titulPredJmenem"];
                if (prvniKontaktSeZamestnavatelem["adresaKontaktu"] != null)
                    fitem.kontakt_adresa = prvniKontaktSeZamestnavatelem["adresaKontaktu"];
                if (prvniKontaktSeZamestnavatelem["kontaktniTelefon"] != null)
                    fitem.kontakt_telefon = prvniKontaktSeZamestnavatelem["kontaktniTelefon"];
                if (prvniKontaktSeZamestnavatelem["mistoKontaktu"] != null)
                    fitem.kontakt_misto = prvniKontaktSeZamestnavatelem["mistoKontaktu"];
                if (prvniKontaktSeZamestnavatelem["kontaktniEmail"] != null)
                    fitem.kontakt_email = prvniKontaktSeZamestnavatelem["kontaktniEmail"];                
            }
            if (mistoVykonuPrace != null)
            {
                if (mistoVykonuPrace.adresaText != null)
                    fitem.mistoVykonuPrace_adresaText = mistoVykonuPrace.adresaText;
                if (mistoVykonuPrace.obec != null)
                    fitem.mistoVykonuPrace_obec = GetCodeList(cl, "obce").GetName(mistoVykonuPrace.obec["id"]);
                if (mistoVykonuPrace.okresy != null)
                {
                    int count = 1;
                    foreach (Dictionary<string, string> d in mistoVykonuPrace.okresy)
                    {
                        fitem.mistoVykonuPrace_okresy += GetCodeList(cl, "okresy").GetName(d["id"]);
                        if (count >= 1 && count != mistoVykonuPrace.okresy.Count)
                            fitem.dovednost += ";";
                        count++;
                    }
                }
                if (mistoVykonuPrace.pracoviste != null)
                {
                    if (mistoVykonuPrace.pracoviste[0].email != null)
                        fitem.pracoviste_email = mistoVykonuPrace.pracoviste[0].email;
                    if (mistoVykonuPrace.pracoviste[0].nazev != null)
                        fitem.pracoviste_nazev = mistoVykonuPrace.pracoviste[0].nazev;
                    if (mistoVykonuPrace.pracoviste[0].telefon != null)
                        fitem.pracoviste_telefon = mistoVykonuPrace.pracoviste[0].telefon;

                    if (mistoVykonuPrace.pracoviste[0].adresa != null)
                    {
                        if (mistoVykonuPrace.pracoviste[0].adresa.cisloDomovni != null)
                            fitem.pracoviste_adresa_cislo_domovni = mistoVykonuPrace.pracoviste[0].adresa.cisloDomovni;
                        if (mistoVykonuPrace.pracoviste[0].adresa.cisloOrientacni != null)
                            fitem.pracoviste_adresa_cislo_orientacni = mistoVykonuPrace.pracoviste[0].adresa.cisloOrientacni;
                        if (mistoVykonuPrace.pracoviste[0].adresa.psc != null)
                            fitem.pracoviste_adresa_psc = mistoVykonuPrace.pracoviste[0].adresa.psc;
                        if (mistoVykonuPrace.pracoviste[0].adresa.obec != null)
                            fitem.pracoviste_adresa_obec = GetCodeList(cl, "obce").GetName(mistoVykonuPrace.pracoviste[0].adresa.obec["id"]);
                        if (mistoVykonuPrace.pracoviste[0].adresa.okres != null)
                        {
                            fitem.pracoviste_adresa_okres = GetCodeList(cl, "okresy").GetName(mistoVykonuPrace.pracoviste[0].adresa.okres["id"]);
                            fitem.host = hosts[GetCodeList(cl, "okresy").GetCode(mistoVykonuPrace.pracoviste[0].adresa.okres["id"])];
                        }
                            
                        if (mistoVykonuPrace.pracoviste[0].adresa.kraj != null)
                            fitem.pracoviste_adresa_kraj = GetCodeList(cl, "kraje").GetName(mistoVykonuPrace.pracoviste[0].adresa.kraj["id"]);
                        if (mistoVykonuPrace.pracoviste[0].adresa.ulice != null)
                            fitem.pracoviste_adresa_ulice = mistoVykonuPrace.pracoviste[0].adresa.ulice["nazev"];

                        string nutz = "";
                        if (mistoVykonuPrace.pracoviste[0].adresa.okres != null)
                            nutz = GetCodeList(cl, "okresy").GetLau(mistoVykonuPrace.pracoviste[0].adresa.okres["id"]);
                        if (mistoVykonuPrace.pracoviste[0].adresa.obec != null)
                            nutz += GetCodeList(cl, "obce").GetCode(mistoVykonuPrace.pracoviste[0].adresa.obec["id"]);

                        if (nutz != null && nutz != "" && portaly.ContainsKey(nutz))
                        {
                            fitem.mportal = portaly[nutz];                            
                        }
                    }
                }
            }

            if (vyhodyVolnehoMista != null)
            {
                int count = 1;
                foreach (Benefit b in vyhodyVolnehoMista)
                {
                    fitem.vyhodyVolnehoMista += GetCodeList(cl, "vyhodyVolnehoMista").GetName(b.vyhoda["id"]);
                    if (count >= 1 && count != vyhodyVolnehoMista.Count)
                        fitem.vyhodyVolnehoMista += ";";
                    count++;
                }

            }
            if (vhodnostiPracovnihoMista != null)
            {
                int count = 1;
                foreach (Dictionary<string, string> d in vhodnostiPracovnihoMista)
                {
                    fitem.vhodnostiPracovnihoMista += GetCodeList(cl, "vhodnostiProTypZamestnance").GetName(d["id"]);
                    if (count >= 1 && count != vhodnostiPracovnihoMista.Count)
                        fitem.vhodnostiPracovnihoMista += ";";
                    count++;
                }

            }
            if (pracovnePravniVztahy != null)
            {
                int count = 1;
                foreach (Dictionary<string, string> d in pracovnePravniVztahy)
                {
                    fitem.pracovnePravniVztahy += GetCodeList(cl, "pracovnepravniVztahy").GetName(d["id"]);
                    fitem.pracovnePravniVztahy_uniweb = GetCodeList(cl, "pracovnepravniVztahy").GetUniweb(d["id"]);
                    if (count >= 1 && count != pracovnePravniVztahy.Count)
                    {
                        fitem.pracovnePravniVztahy += ";";
                        fitem.pracovnePravniVztahy_uniweb += ";";
                    }
                        
                    count++;
                }
                

            }
            if (pozadovaneVzdelani != null)
            {
                int count = 1;
                foreach (Dictionary<string, string> d in pozadovaneVzdelani)
                {
                    fitem.pozadovaneVzdelani += GetCodeList(cl, "vzdelaniKkov").GetName(d["id"]);
                    if (count >= 1 && count != pozadovaneVzdelani.Count)
                        fitem.pozadovaneVzdelani += ";";
                    count++;
                }

            }
            if (pozadovanePovolani != null)
            {
                int count = 1;
                foreach (Dictionary<string,string> d in pozadovanePovolani)
                {
                    fitem.pozadovanePovolani += GetCodeList(cl, "czIsco").GetName(d["id"]);
                    if (count >= 1 && count != pozadovanePovolani.Count)
                        fitem.pozadovanePovolani += ";";
                    count++;
                }

            }
            if (pozadovanaDovednost != null)
            {
                int count = 1;
                foreach (RequiredSkills r in pozadovanaDovednost)
                {
                    fitem.dovednost += GetCodeList(cl, "dovednosti").GetName(r.dovednost["id"]);
                    if (count >= 1 && count != pozadovanaDovednost.Count)
                        fitem.dovednost += ";";
                    count++;
                }

            }
            if (pozadovanaJazykovaZnalost != null)
            {
                int count = 1;
                foreach (RequiredLanguage r in pozadovanaJazykovaZnalost)
                {
                    fitem.jazykovaZnalost_jazyk += GetCodeList(cl, "jazyky").GetName(r.jazyk["id"]);
                    fitem.jazykovaZnalost_uroven += GetCodeList(cl, "jazyky").GetName(r.urovenZnalosti["id"]);
                    fitem.jazykovaZnalost_jazyk_uniweb += GetCodeList(cl, "jazyky").GetUniweb(r.jazyk["id"]);
                    if (count >= 1 && count != pozadovanaJazykovaZnalost.Count)
                    {
                        fitem.jazykovaZnalost_jazyk_uniweb += ";";
                        fitem.jazykovaZnalost_jazyk += ";";
                        fitem.jazykovaZnalost_uroven += ";";
                    }
                    count++;
                }
            }
            
            return fitem;
        }

        private CodeList GetCodeList(List<CodeList> codeListCollection, string codeListType)
        {
            var cL = (from codeList in codeListCollection
                      where (codeList.Type == codeListType)
                      select codeList).First();
            return cL;
        }
        private string CleanString(string toclean)
        {
            string r;

            toclean = toclean.Normalize(NormalizationForm.FormD);
            StringBuilder bezdia = new StringBuilder();

            for (int i = 0; i < toclean.Length; i++)
            {

                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(toclean[i]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    bezdia.Append(toclean[i]);
                }
            }

            r = bezdia.ToString().Replace(",", "");
            r = r.Replace(" ", "-");
            r = r.Remove(r.Length - 1);
            return r;
        }
    }
}
