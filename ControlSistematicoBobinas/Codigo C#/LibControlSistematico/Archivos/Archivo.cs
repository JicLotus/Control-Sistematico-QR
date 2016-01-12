using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public abstract class Archivo
    {
        protected string pathFile;

        public abstract Dictionary<string, int> LeerArchivo(bool cargaTipo);

        public void GuardarArchivo(string valor)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(pathFile, true))
                {
                    wr.WriteLine(valor);
                }
            }
            catch (Exception e) { }
        }

        public void BorrarArchivo()
        {
            if (File.Exists(pathFile)) { File.Delete(pathFile); }
        }

    }
}
