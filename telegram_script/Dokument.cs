using System.IO;
using System;
using System.Collections.Generic;

namespace telegram_script
{
    public static class Dokument
    {
        //Verzeichnis liest den Pfad des Ordners aus (fallunterscheidung weile die exe sich direkt im Ordner befindet,
        // wenn man es über die ide ausführt aber im bin/debug.
        // geht auch durch wie viele messeage dateien es gibt und returnt den int
        public static int Verzeichnis(ref string messages, ref string auswertung, string identify)
        {
            string verzeichnis  = Directory.GetCurrentDirectory();
            string[] files;
            if (verzeichnis.Contains("bin/Debug"))
            {
                messages = "../../../";
                auswertung = $"../../../auswertung{identify}.txt";
                files = Directory.GetFiles("../../../");
            }
            else
            {
                messages= "";
                auswertung = $"auswertung{identify}.txt";
                files = Directory.GetFiles(verzeichnis);
            }
            int anzahl = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("messages"))
                {
                    anzahl++;
                }
            }
            return anzahl;
        }
        // schreibt die Liste in das dokument
        public static void Schreiben(string auswertung, List<WordCount> list, string identify)
        {
            string print = "";
            if (!File.Exists(auswertung))
            {
                FileStream fs = File.Create(auswertung);
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(auswertung);
            foreach (WordCount word in list)
            {
                if (identify == "Sender")
                {
                    print = $"Name: {word.ToString()}";
                }
                else if (identify == "Links")
                {
                    print = $"Quelle: {word.ToString()}";
                }
                else if (identify == "Youtube")
                {
                    print = $"Kanalname: {word.ToString()}";
                }
                sw.WriteLine(print);
            }
            sw.Close();
        }
    }
}