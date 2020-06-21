using System;
using System.IO;
using System.Collections.Generic;

namespace telegram_script
{
    class Program
    {
        // ruft alles auf, was für die Linkanalyse nötig ist. messagenew braucht es, um durch die verschiednen messges zu iterieren
        public static void Links()
        {
            string messages="";
            string auswertung = "";
            int anzahl = Dokument.Verzeichnis(ref messages, ref auswertung, "Links");
            if (anzahl > 0)
            {
                string messagesnew = messages + "messages.html";
                List<WordCount> linkList = new List<WordCount>();
                linkList = GetLinks.AuslesenLinks(messagesnew, linkList);
                for (int i = 2; i <= anzahl; i++)
                {
                    messagesnew = messages+$"messages{i}.html";
                    linkList = GetLinks.AuslesenLinks(messagesnew, linkList);
                }
                Dokument.Schreiben(auswertung, linkList,"Links");
                Console.Write("Fertig!\n");
                Console.Write("Fertig!\n");
            }
            else
            {
                Console.WriteLine("Keine Message-Dateien gefunden, stelle sicher, dass sie sich im selben Verzeichnis wie das Programm befinden\n");
            }
        }

        public static void Youtube()
        {
            string messages="";
            string auswertung = "";
            int anzahl = Dokument.Verzeichnis(ref messages, ref auswertung, "Youtube");
            if (anzahl > 0)
            {
                string messagesnew = messages + "messages.html";
                List<WordCount> youtubeList = new List<WordCount>();
                youtubeList = GetYoutube.AuslesenYoutube(messagesnew, youtubeList);
                for (int i = 2; i <= anzahl; i++)
                {
                    messagesnew = messages+$"messages{i}.html";
                    youtubeList = GetYoutube.AuslesenYoutube(messagesnew, youtubeList);
                }
                Dokument.Schreiben(auswertung, youtubeList,"Youtube");
                Console.Write("Fertig!\n");
            }
            else
            {
                Console.WriteLine("Keine Message-Dateien gefunden, stelle sicher, dass sie sich im selben Verzeichnis wie das Programm befinden\n");
            }
        }
        // selbes wie bei Link
        public static void Sender()
        {
            string messages="";
            string auswertung = "";
            int anzahl = Dokument.Verzeichnis(ref messages, ref auswertung, "Sender");
            if (anzahl > 0)
            {
                string messagesnew = messages + "messages.html";
                List<WordCount> senderList = new List<WordCount>();
                senderList = GetSender.AuslesenSender(messagesnew, senderList);
                for (int i = 2; i <= anzahl; i++)
                {
                    messagesnew = messages+$"messages{i}.html";
                    senderList = GetSender.AuslesenSender(messagesnew, senderList);
                }
                Dokument.Schreiben(auswertung, senderList,"Sender");
                Console.Write("Fertig!\n");
            }
            else
            {
                Console.WriteLine("Keine Message-Dateien gefunden, stelle sicher, dass sie sich im selben Verzeichnis wie das Programm befinden\n");
            }
        }
        // Ruft das Programm aus und bietet auswahlfunktion
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Du kannst verschiedene Dinge auswerten lassen\n \nUm dir anzeigen zu lassen, wer wie viel" +
                                  " geschrieben hat drücke die 1\nUm dir anzeigen zu lassen welche Links verschickt wurden" +
                                  ", drücke die 2. \nUm Youtube nach Kanälen aufzuschlüsseln drücke die 3 \n \nUm Beides zu " +
                                  "Analysieren drücke die 4");
                int identify = Convert.ToInt32(Console.ReadLine());
                switch (identify)
                {
                    case 1:
                        Sender();
                        break;
                    case 2:
                        Links();
                        break;
                    case 3:
                        Youtube();
                        break;
                    case 4:
                        Sender();
                        Links();
                        Youtube();
                        break;
                    default:
                        Console.WriteLine("Du hast keine Aktion ausgewählt.");
                        break;
                }
                Console.WriteLine("Wenn sie noch etwas analysieren wollen, drücken sie Y, wenn sie fertig sind, drücken sie eine beliebige andere Tase");
                string again = Convert.ToString(Console.ReadLine());
                if (again != "Y" && again != "y")
                {
                    loop = false;
                }
            }
        }
    }
}