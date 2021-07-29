using System;
using System.Reflection;


namespace MPSVJson.Models
{
    class FItem
    {
        public string host { get; set; }
        public string mportal { get; set; }
        public int portalID { get; set; }
        public string uid { get; set; }
        public string referencniCislo { get; set; }
        public int azylant { get; set; }
        public int cizinecMimoEu { get; set; }
        public DateTime datumVlozeni { get; set; }
        public DateTime datumZmeny { get; set; }
        public float? mesicniMzdaDo { get; set; }
        public float? mesicniMzdaOd { get; set; }
        public int modraKarta { get; set; }
        public float? pocetHodinTydne { get; set; }
        public int pocetMist { get; set; }
        public string pozadovanaProfese { get; set; }
        public int statniSpravaSamosprava { get; set; }
        public DateTime? terminUkonceniPracovnihoPomeru { get; set; }
        public DateTime? terminZahajeniPracovnihoPomeru { get; set; }
        public int souhlasAgenturyAgentura { get; set; }
        public int souhlasAgenturyUzivatel { get; set; }
        public string upresnujiciInformace { get; set; }
        public string urlAdresa { get; set; }
        public int zamestnaneckaKarta { get; set; }
        public DateTime? expirace { get; set; }
        public string minPozadovaneVzdelani { get; set; }
        public string smennost { get; set; }
        public string typMzdy { get; set; }
        public string zverejnovat { get; set; }
        public string mistoVykonuPrace_obec { get; set; }
        public string mistoVykonuPrace_okresy { get; set; }
        public string mistoVykonuPrace_adresaText { get; set; }
        public string pracoviste_email { get; set; }
        public string pracoviste_nazev { get; set; }
        public string pracoviste_telefon { get; set; }
        public int? pracoviste_adresa_cislo_domovni { get; set; }
        public string pracoviste_adresa_cislo_orientacni { get; set; }
        public string pracoviste_adresa_psc { get; set; }
        public string pracoviste_adresa_kraj { get; set; }
        public string pracoviste_adresa_okres { get; set; }
        public string pracoviste_adresa_obec { get; set; }
        public string pracoviste_adresa_ulice { get; set; }
        public string zamestnavatel_ico { get; set; }
        public string zamestnavatel_nazev { get; set; }
        public string kontaktni_osoba_jmeno { get; set; }
        public string kontaktni_osoba_prijmeni { get; set; }
        public string kontaktni_osoba_titul { get; set; }
        public string profeseCzIsco { get; set; }
        public string pracovnePravniVztahy { get; set; }
        public string vhodnostiPracovnihoMista { get; set; }
        public string kontaktniPracoviste { get; set; }
        public string vyhodyVolnehoMista { get; set; }        
        public string dovednost { get; set; }        
        public string jazykovaZnalost_uroven { get; set; }
        public string jazykovaZnalost_jazyk { get; set; }        
        public string pozadovanePovolani { get; set; }
        public string pozadovaneVzdelani { get; set; }
        public string kontakt_misto { get; set; }
        public string kontakt_adresa { get; set; }
        public string kontakt_telefon { get; set; }
        public string kontakt_email { get; set; }
        public string kontakt_osoba_jmeno { get; set; }
        public string kontakt_osoba_prijmeni { get; set; }
        public string kontakt_osoba_titul { get; set; } 
        public string smennost_uniweb { get; set; }
        public string obor_uniweb { get; set; }
        public string obor_nazev { get; set; }
        public string profeseCzIsco_uniweb { get; set; }
        public string pracovnePravniVztahy_uniweb { get; set; }
        public string pozadovaneVzdelani_uniweb { get; set; }
        public string jazykovaZnalost_jazyk_uniweb { get; set; }
        public int code { get; set; }
        public int state { get; set; }
        public string title_url { get; set; }
        public int company { get; set; }
        public int tarif { get; set; }

        PropertyInfo[] propertyInfos { get; set; }
        public string columns;
        public string values;
        public string update;

        public FItem()
        {
            propertyInfos = typeof(FItem).GetProperties();
            
        }
        public void CreateInsertQuery()
        {
            
            foreach (PropertyInfo f in propertyInfos)
            {
                if(f.GetValue(this) != null)
                {
                    columns += f.Name + ",";
                    
                    if (f.PropertyType == typeof(DateTime) || f.PropertyType == typeof(DateTime?))
                    {
                        DateTime formDate = (DateTime)f.GetValue(this);                        
                        values += "'" + formDate.ToString("yyyy-MM-dd") + "',";
                    }
                    else if (f.PropertyType == typeof(float?) || f.PropertyType == typeof(float))
                    {
                       
                        values += f.GetValue(this).ToString().Replace(",", ".") + ",";
                    }
                    else
                    {
                        values += "'" + f.GetValue(this).ToString().Replace("'", "") + "',";
                    }                    
                }                
            }
            columns = columns.Remove(columns.Length -1);
            values = values.Remove(values.Length - 1);        
        }

        public void CreateUpdateQuery()
        {

            foreach (PropertyInfo f in propertyInfos)
            {
                if (f.GetValue(this) != null)
                {
                    
                    update += f.Name + "=";

                    if (f.PropertyType == typeof(DateTime) || f.PropertyType == typeof(DateTime?))
                    {
                        DateTime formDate = (DateTime)f.GetValue(this);
                        update += "'" + formDate.ToString("yyyy-MM-dd") + "',";
                    }
                    else if (f.PropertyType == typeof(float?) || f.PropertyType == typeof(float))
                    {

                        update += f.GetValue(this).ToString().Replace(",", ".") + ",";
                    }
                    else
                    {
                        update += "'" + f.GetValue(this).ToString().Replace("'", "") + "',";
                    }

                }
            }
            update = update.Remove(update.Length - 1);
            
        }


    }
}
