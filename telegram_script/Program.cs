using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;


namespace telegram_script
{
    /// <summary>
    /// Definiert die Methoden, die für die Liste wichtig sind
    /// </summary>
    class WordCount
    {
        private string _name;
        private int _count;

        // Getter und Setter
        public int Count
        {
            get { return _count; }
            set { _count = value; }

        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        //Weiß nicht mehr wofür der da ist, tut eigentlich das selbe wie der für Count
        private int count { get; set; }

        //Konstruktor
        public WordCount(string name, int count = 1)
        {
            this._name = name;
            this._count = count;
        }

        //To-String überlagern für Ausgabe als String
        public override string ToString()
        {
            return $"{this.Name,-30}Anzahl: {this._count}";
        }
        
        //Vergliecht Listenelemente (für Sortierung)
        public int CompareTo(WordCount compareWordCount)
        {
            if (compareWordCount == null)
            {
                return 1;
            }
            else
            {
                return this.count.CompareTo(compareWordCount.count);
            }
        }
        //Vergleicht Listenelemente (für's Anzahl zählen)
        public bool Equals(WordCount other)
        {
            if (other == null) return false;
            return (this.count.Equals(other.count));
        }
        public override int GetHashCode()
        {
            return count;
        }
    }
    class Program
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
        // Selbes wie bei Sender, nur mit anderen Auswahlkriterien. Link wird erkannt durch "a href" darauf folgend
        // dann rausgeschnitten alles ab ww bis zum ersten "/"
        public static List<WordCount> AuslesenLinks(string messages, List<WordCount> list)
        {
            StreamReader sr = new StreamReader(messages);
            string zeile = "";
            
            while (!sr.EndOfStream)
            {    
                string name = "";
                zeile = sr.ReadLine();
                if( zeile.Contains("a href"))
                {
                    bool inLink = false;
                    for (int i = 0; i < zeile.Length; i++)
                    {
                        if (zeile[i] == 'w'&& zeile[i+1] == 'w')
                        {
                            inLink = true;
                        }

                        if ((zeile[i] == '/') && inLink)
                        {
                            break;
                        }

                        if (inLink)
                        {
                            name += zeile[i];
                        }
                    }
                    
                    if (list.Exists(x =>x.Name.Equals(name)))
                    {
                        list.Find(x => x.Name.Equals(name)).Count++;
                    }
                    else if (name != "")
                    {
                        list.Add(new WordCount(name));
                    }
                }
            }
            list.Sort((x,y)=> y.Count.CompareTo(x.Count));
            sr.Close();
            return list;
        }
        //Verzeichnis liest den Pfad des Ordners aus (fallunterscheidung weile die exe sich direkt im Ordner befindet,
        // wenn man es über die ide ausführt aber im bin/debug.
        // geht auch durch wie viele messeage dateien es gibt und returnt den int
        static int Verzeichnis(ref string messages, ref string auswertung, string identify)
        {
            string verzeichnis  = Directory.GetCurrentDirectory();
            string[] files;
            Console.WriteLine(verzeichnis);
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
        static void Schreiben(string auswertung, List<WordCount> list, string identify)
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
                sw.WriteLine(print);
            }
            sw.Close(); 
        }
        // ruft alles auf, was für die Linkanalyse nötig ist. messagenew braucht es, um durch die verschiednen messges zu iterieren
        public static void Links()
        {
            string messages="";
            string auswertung = "";
            int anzahl = Verzeichnis(ref messages, ref auswertung, "Links");
            string messagesnew = messages + "messages.html";
            List<WordCount> linkList = new List<WordCount>();
            linkList = AuslesenLinks(messagesnew, linkList);
            for (int i = 2; i <= anzahl; i++)
            {
                messagesnew = messages+$"messages{i}.html";
                linkList = AuslesenLinks(messagesnew, linkList);
            }
            Schreiben(auswertung, linkList,"Links");
        }
        // selbes wie bei Link
        public static void Sender()
        {
            string messages="";
            string auswertung = "";
            int anzahl = Verzeichnis(ref messages, ref auswertung, "Sender");
            string messagesnew = messages + "messages.html";
            List<WordCount> senderList = new List<WordCount>();
            senderList = AuslesenSender(messagesnew, senderList);
            for (int i = 2; i <= anzahl; i++)
            {
                messagesnew = messages+$"messages{i}.html";
                senderList = AuslesenSender(messagesnew, senderList);
            }
            Schreiben(auswertung, senderList,"Sender");
        }
        // Ruft das Programm aus und bietet auswahlfunktion
        static void Main(string[] args)
        {
            Console.WriteLine("Du kannst verschiedene Dinge auswerten lassen\n Um dir anzeigen zu lassen, wer wie viel" +
                              " geschrieben hat drücke die 1\n Um dir anzeigen zu lassen welche Links verschickt wurden" +
                              ", drücke die 2. \n Um Beides zu Analysieren drücke die 3");
            int identify = Console.Read()-48;
            switch (identify)
            {
                case 1:
                    Sender();
                    break;
                case 2:
                    Links();
                    break;
                case 3:
                    Sender();
                    Links();
                    break;
                default:
                    Console.WriteLine("Du hast keine Aktion ausgewählt.");
                    break;
            }
            
            

        }
    }
}