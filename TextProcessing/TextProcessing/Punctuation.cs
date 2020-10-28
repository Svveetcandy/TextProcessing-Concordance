using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessing
{
    class Punctuation
    {
        public List<string> Punct { get; } = new List<string>();
        public void InputPunctuation(List<string> punct)
        {
            foreach(string i in punct)
            {
                Punct.Add(i);
            }
        }
        public int NumberOfSentences()
        {
            return int.Parse(Punct[Punct.Count - 2]);
        }
        public int[] GetNumVopros()
        {
            List<int> NumVopros = new List<int>();
            for(int i=0; i<Punct.Count; i++)
            {
                if (Punct[i] == "?") NumVopros.Add(int.Parse(Punct[i-1]));
            }
            int[] num = NumVopros.ToArray();
            return num;
        }
    }
}
