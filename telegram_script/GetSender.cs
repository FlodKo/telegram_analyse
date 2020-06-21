using System.Collections.Generic;
using System.IO;

namespace telegram_script
{
    public static class GetSender
    {
        //Bekommt eine Liste und einen String zur Message-Datei überliefert. Liest die Datei aus und gibt Liste mit Namen zurück
        public static List<WordCount> AuslesenSender(string messages, List<WordCount> list)
        {
            StreamReader sr = new StreamReader(messages);
            string zeile = "";
            string vorZeile ="";
            while (!sr.EndOfStream)
            {
                // sr liest aus, falls <span in der Zeile mit drin steht wird das ausgeschnitten, sonst einfach die ganze Zeile zur Liste hinzugügen.
                zeile = sr.ReadLine();
                if( vorZeile.Contains("from_name") && !(zeile.Contains("<span")))
                {
                    if (list.Exists(x =>x.Name.Equals(zeile)))
                    {
                        list.Find(x => x.Name.Equals(zeile)).Count++;
                    }
                    else
                    {
                        list.Add(new WordCount(zeile));
                    }
                }

                if (vorZeile.Contains("from_name") && zeile.Contains("<span"))
                {
                    string name = "";
                    for (int i = 0; i < zeile.Length; i++)
                    {
                        if (zeile[i] !='<')
                        {
                            name += zeile[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (list.Exists(x =>x.Name.Equals(name)))
                    {
                        list.Find(x => x.Name.Equals(name)).Count++;
                    }
                    else
                    {
                        list.Add(new WordCount(name));
                    }
                    
                }
                vorZeile = sr.ReadLine();
            }
            // Liste wird Sortiert
            list.Sort((x,y)=> y.Count.CompareTo(x.Count));
            sr.Close();
            return list;
        }
    }
}