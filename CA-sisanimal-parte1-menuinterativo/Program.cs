
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using sysanimalview_csa;



namespace CA_sisanimal_parte1_menuinterativo
{


    internal class Program
    {



        static void Main(string[] args)
        {

            Menu menu = new Menu();
            menu.exibir(); 
        }
    }
}