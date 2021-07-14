using MPSVJson.Contollers;
using MPSVJson.Models;
using System;

namespace MPSVJson
{
    class Program
    {
        static void Main(string[] args)
        {

            HomeController home = new HomeController(new DBQueries(),new JSONHandler());

            DBConn.Connect("mistniportal");
            home.GetPortaly();
            home.GetHosts();
            DBConn.Disconnect();
            DBConn.Connect("uniwebset");
            //home.TestMeth();
            home.LoadJsons();
            home.Initialize();
            home.UpdateData();
            DBConn.Disconnect();
        }
    }
}
