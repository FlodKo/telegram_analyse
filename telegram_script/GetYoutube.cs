using System;
using System.Collections.Generic;
using System.IO;

namespace telegram_script
{
    public class GetYoutube
    {
        public static List<WordCount> AuslesenYoutube(string messages, List<WordCount> list)
        {
            StreamReader sr = new StreamReader(messages);
            string zeile = "";
            List<string> videoID = new List<string>();
            while (!sr.EndOfStream)
            {    
                string name = "";
                zeile = sr.ReadLine();
                if (zeile.Contains("www.youtube.com") && zeile.Contains("/user"))
                {
                    name = getYoutubeName(zeile);

                }
                else if (zeile.Contains("www.youtube.com")&& zeile.Contains("laylist"))
                {
                    Console.WriteLine("Playlist");
                }
                else if (zeile.Contains("www.youtube.com"))
                {
                    videoID.Add(FilterYoutubeID(zeile));
                }
                if (name != "")
                {
                    WordCount.AddList(ref list, name); 
                }
            }
            List<WordCount> channelList = YoutubeAPI.get_Channel_from_ID(videoID);
            foreach (var channel in channelList)
            {
                WordCount.AddList(ref list, channel);
            }
            list.Sort((x,y)=> y.Count.CompareTo(x.Count));
            sr.Close();
            return list;
        }
        public static string FilterYoutubeID(string zeile)
        {
            string youtubeID = "";
            bool inID = false;
            int counter = 0;
            for (int i = 3; i < zeile.Length; i++)
            {
                if (zeile[i-2] == 'v'&& zeile[i-1] == '=')
                {
                    inID = true;
                }

                if (counter == 11)
                {
                    break;
                }

                if (inID)
                {
                    youtubeID += zeile[i];
                    counter++;
                }
            }
            return youtubeID;
        }

        static public string getYoutubeName(string zeile)
        {
            string name = "";
            bool inName = false;
            for (int i = 4; i < zeile.Length; i++)
            {
                if (zeile[i-4] == 's'&& zeile[i-3] == 'e'&& zeile[i-2] == 'r'&& zeile[i-1] == '/')
                {
                    inName = true;
                }

                if (zeile[i] == '"'&& inName)
                {
                    break;
                }

                if (inName)
                {
                    name += zeile[i];
                }
            }
            return name;
        }
    }
}