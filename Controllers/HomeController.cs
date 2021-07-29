using MPSVJson.CodeLists;
using MPSVJson.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace MPSVJson.Controllers
{

    class HomeController
    {
        private DBQueries query;
        private JSONHandler jsonHandler;
        private JobsPackage jobsPackage;
        private List<CodeList> codeLists;
        private int[] ids;
        private int[] portalIds;
        private Dictionary<string, string> portaly;
        private Dictionary<string, string> hosts;
        private List<Dictionary<string, object>> companies;
        DateTime today;
        List<string> log;
        private string log_path = @"D:\exe\jobs\logs\";
        public HomeController(DBQueries query, JSONHandler jsonHandler)
        {
            this.query = query;
            this.jsonHandler = jsonHandler;
            today = DateTime.Now.Date;
            log = new List<string>();
        }

        public void LoadJsons()
        {
            Console.Write("Stahuji data...");
            try
            {
                jobsPackage = jsonHandler.GetJsonFromUrl();
            }
            catch (Exception e)
            {
                log.Add("Nepodařilo se stáhnout data.");
                log.Add(e.ToString());
                log.Add("!--------------------------------------------------------------!\n");
                MakeLog(log);
            }


            try
            {
                codeLists = jsonHandler.LoadCodeLists();
            }
            catch (Exception e)
            {
                log.Add("Nepodařilo se načíst číselníky.");
                log.Add(e.ToString());
                log.Add("!--------------------------------------------------------------!\n");
                MakeLog(log);
            }

            Console.WriteLine("OK");
        }

        public void Initialize()
        {
            ids = query.SelectInts("code", "jobs2");
            portalIds = query.SelectInts("portalId", "jobs2");
            GetCompanies();
            log.Add("Počet inzerátů = " + jobsPackage.polozky.Count);
        }

        public string UpdateData()
        {
            int counter = 0;
            int upd = 0;
            int ins = 0;
            int code;
            if (ids.Length != 0)
                code = ids.Max() + 1;
            else
                code = 1;
            int max = jobsPackage.polozky.Count;

            Console.WriteLine("Probíhá aktualizace DB...");
            Console.Write("Progres :");
            foreach (Item x in jobsPackage.polozky)
            {
                FItem fitem = new FItem();
                try
                {
                    fitem = x.Format(codeLists, portaly, hosts, companies);
                    Console.Write("\r{0}/{1}         ", counter, max);
                    //Console.WriteLine("Formating item...OK("+counter.ToString() + ")");
                }
                catch (Exception e)
                {
                    log.Add(e.ToString());
                    Console.WriteLine("Formating item...ERROR(" + counter.ToString() + ")");
                }


                bool check = Array.Exists(portalIds, y => y == x.portalId);

                try
                {
                    if (check)
                    {
                        if (x.datumZmeny >= today)
                        {
                            //UPDATE
                            fitem.CreateUpdateQuery();
                            query.Update("jobs2", fitem.update, "portalID ='" + x.portalId.ToString() + "'");
                            upd++;

                        }
                    }
                    else
                    {
                        //INSERT
                        fitem.code = code;
                        fitem.CreateInsertQuery();
                        query.Insert("jobs2", fitem.columns, fitem.values);
                        ins++;
                        code++;
                    }
                }
                catch (Exception e)
                {
                    log.Add("Nepodařilo zařadit položku " + x.portalId.ToString());
                    log.Add(e.ToString());
                    log.Add("!--------------------------------------------------------------!\n");
                }
                counter++;
            }
            Console.WriteLine("OK");

            //DELETE
            Console.Write("Probíhá mazání starých dat...");
            query.Delete("jobs2", "expirace = '" + today.ToString("yyyy - MM - dd") + "'");
            Console.WriteLine("OK");
            Console.WriteLine("Hotovo");
            Console.WriteLine("");
            Console.WriteLine("Počet aktualizovaných inzerátů = " + upd.ToString());
            Console.WriteLine("Počet nově vložených inzerátů = " + ins.ToString());
            log.Add("Počet aktualizovaných inzerátů = " + upd.ToString());
            log.Add("Počet nově vložených inzerátů = " + ins.ToString());
            MakeLog(log);

            Thread.Sleep(5000);
            return "Nalezeno " + counter + " záznamů.";
        }


        public void GetPortaly()
        {
            portaly = query.SelectDict("nutz,portal", "portaly", "not nutz = ''");
        }
        public void GetHosts()
        {
            hosts = query.SelectDict("psc,oblast_tags", "okresy", "id > '0'");
        }
        public void GetCompanies()
        {
            companies = query.SelectAll("creg,code,tarif", "companies", "");
        }
        private void MakeLog(List<string> log)
        {
            string fileName = log_path + "log_" + today.ToString("dd/MM/yyyy") + ".txt";

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    foreach (string s in log)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
