using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace Sat.Recruitment.Configuration.Utilities
{
    public class FileReader
    { 

        public static List<dynamic> ReadFromFile(char delimiter, string fileRoute)
        {
            List<dynamic> listaObjetos = new List<dynamic>();
            var path = Directory.GetCurrentDirectory() + fileRoute;

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                { 
                    string[] elementos = line.Split(delimiter); 
                    listaObjetos.Add(elementos);
                }
            }

            return listaObjetos;
        }
    }
}
