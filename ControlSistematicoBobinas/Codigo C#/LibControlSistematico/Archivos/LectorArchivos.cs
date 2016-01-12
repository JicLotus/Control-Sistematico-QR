using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace LibControlSistematico
{
    public class LectorArchivos : Archivo
    {
        List<string> turnos;
        
        public LectorArchivos(string path)
        {
            pathFile = path;
        }

        public override Dictionary<string, int> LeerArchivo(bool cargaTipo)
        {

            Dictionary<string, int> dictionary=null;

            if (File.Exists(pathFile)) 
            {
                dictionary = new Dictionary<string, int>();
                turnos = new List<string>();


                using (StreamReader sr = new StreamReader(pathFile)) 
                {
                    string s = sr.ReadLine();

                    while (s != null) 
                    {

                        string [] split = s.Split(';');

                        string dato;
                        int id;
                        
                        if (split.Length >= 4) turnos.Add(split[3]);
                        
                        
                        id = Convert.ToInt32(split[0]);
                        dato = split[1];

                        if (cargaTipo) {
                            if (split[2] != "0") 
                            { 
                                dato = split[1] + " " + split[2] + "Mts";
                            }
                        }

                        if (dictionary.ContainsKey(dato) == false) { 
                            dictionary.Add(dato, id);
                        }

                        s = sr.ReadLine();
                    }
                }
            }

            return dictionary;
        }


        public List<String> leerArchivoConsulta() 
        {
            List<String> laLista= new List<String>();

            if (File.Exists(pathFile)) 
            {
                using (StreamReader sr = new StreamReader(pathFile)) 
                {
                    string s = sr.ReadLine();
                    //string s = sr.ReadToEnd();
                    while (s != null) 
                    {
                        laLista.Add(s);
                        s = sr.ReadLine();
                    
                    }
                }

            }
            return laLista;
        }


        public int leerCantBobs() 
        {
            int cantBobs = 0;
            if (File.Exists(pathFile))
            {

                using (StreamReader sr = new StreamReader(pathFile))
                {
                    string s = sr.ReadLine();
                    cantBobs = Convert.ToInt32(s);
                }
            }
            return cantBobs;
        }

        public List<string> getTurnos() 
        {
            return turnos;
        }

    }
}
