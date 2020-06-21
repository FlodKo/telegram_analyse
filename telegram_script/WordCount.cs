using System.Collections.Generic;
namespace telegram_script
{
    /// <summary>
    /// Definiert die Methoden, die für die Liste wichtig sind
    /// </summary>
    public class WordCount
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
        public static void AddList(ref List<WordCount> list, WordCount element)
        {
            if (list.Exists(x =>x.Name.Equals(element._name)))
            {
                list.Find(x => x.Name.Equals(element._name)).Count += element._count;
            }
            else if (element._name != "")
            {
                list.Add(new WordCount(element._name, element._count));
            }
        }
        public static void AddList(ref List<WordCount> list, string name)
        {
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
}