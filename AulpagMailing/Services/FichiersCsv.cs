using AulpagMailing.Models;
using FileHelpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AulpagMailing.Services
{
    class ImportExportData
    {

       public static void WriteCSVFile(List<Destinataires_export> dataSource)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "(*.csv)|*.csv";
            if (true == dlg.ShowDialog())
            {

                string filename = dlg.FileName;
                FileHelperEngine engine = new FileHelperEngine(typeof(Destinataires_export));
                List<Destinataires_export> csv = new List<Destinataires_export>();

                engine.HeaderText = engine.GetFileHeader();

                //convert any datasource to csv based object
                foreach (var item in dataSource)
                {
                    Destinataires_export temp = new Destinataires_export();
                    temp.nom = item.nom;
                    temp.prenom = item.prenom;
                    temp.civilité = item.civilité;
                    temp.email = item.email;
                    temp.adherent = item.adherent;
                    temp.categorie = item.categorie;
                    temp.titre = item.titre;
                    csv.Add(temp);

                } // end foreach

                //give file a name and header text
                //   engine.HeaderText = "Nom;Prénom;Civilité;Email;Adhérent;Catégorie;titre";

                //save file locally
                engine.WriteFile(filename, csv);
            }
        }

        public static IEnumerable<Destinataires_export> ReadCSV(string fileName)
        {
            List<Destinataires_export> table = new List<Destinataires_export>();

           var t = File.ReadLines(Path.ChangeExtension(fileName, ".csv"),Encoding.Default).Skip(1);
            foreach(var item  in t)
            {
                string[] data = item.Split(';');
                table.Add(  new Destinataires_export(data[0], data[1], data[2], data[3], Convert.ToInt32(data[4]), Convert.ToBoolean(data[5]), data[6]));
            }
            return table;         
        }
    }
}
