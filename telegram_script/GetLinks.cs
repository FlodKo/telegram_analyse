using System.Collections.Generic;
using System.IO;

namespace telegram_script
{
    public class GetLinks
    {
        // Selbes wie bei Sender, nur mit anderen Auswahlkriterien. Link wird erkannt durch "a href" darauf folgend
        // dann rausgeschnitten alles ab ww bis zum ersten "/"

        public static string FilterLink(string zeile)
        {
            string name = "";
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
            return name;
        }
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
                    name = GetLinks.FilterLink(zeile);
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
            list.Sort((x,y)=> y.Count.CompareTo(x.Count));
            sr.Close();
            return list;
        }
    }
}