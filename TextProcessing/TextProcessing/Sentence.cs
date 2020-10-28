using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextProcessing
{
    class Sentence
    {
        public List<string>[] sentence { get; set; }
        public void InputSentence(string[][] sentence)
        {
            this.sentence = new List<string>[sentence.Length];
            for (int i = 0; i < sentence.Length; i++)
            {
                this.sentence[i] = sentence[i].ToList();
            }

        }
        public int[] OutputSortedSentences()
        {
            List<string>[] sortedSent = sentence;
            int[] length = new int[sortedSent.Length];
            int[] numOfSent = new int[sortedSent.Length];
            int count = 0;
            for (int i = 0; i < sortedSent.Length; i++)
            {
                for (int j = 0; j < sortedSent[i].Count; j++) {
                    if(!(sortedSent[i][j]== " " || sortedSent[i][j] == "!" || sortedSent[i][j] == "?" || sortedSent[i][j] == "." || sortedSent[i][j] == "," || sortedSent[i][j] == ":" || sortedSent[i][j] == "\r\n" || sortedSent[i][j] == ";" || sortedSent[i][j] == "(" || sortedSent[i][j] == ")" || sortedSent[i][j] == "—" || sortedSent[i][j] == "..."))
                    {
                        count++;
                    }
                }
                numOfSent[i] = i;
                length[i] =count;
                count = 0;
            }
            for (int i = 0; i < sortedSent.Length - 1; i++)
            {
                for (int j = i + 1; j < sortedSent.Length; j++)
                {
                    if (length[i] > length[j])
                    {
                        Swap(ref length[i], ref length[j]);
                        Swap(ref numOfSent[i], ref numOfSent[j]);
                    }
                }
            }
            return numOfSent;
        }
        public List<string> voprosSentences(int[] numOfSent, int length)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < numOfSent.Length; i++)
            {
                for (int j = 0; j < sentence[numOfSent[i] - 1].Count; j++)
                {
                    if (sentence[numOfSent[i] - 1][j].Length == length)
                    {
                        if (!result.Contains(sentence[numOfSent[i] - 1][j]))
                        {
                            result.Add(sentence[numOfSent[i] - 1][j]);
                        }
                    }
                }
            }
            return result;
        }
        public void removeWords(int length)
        {
            char[] consonants = { 'Б', 'В', 'Г', 'Д', 'Ж', 'З', 'Й', 'К', 'Л', 'М', 'Н', 'П', 'Р', 'С', 'Т', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
            for (int i = 0; i < sentence.Length; i++)
            {
                for (int j = 0; j < sentence[i].Count; j++)
                {
                    if (sentence[i][j].Length == length)
                    {
                        for (int k = 0; k < consonants.Length; k++)
                        {
                            if (sentence[i][j].StartsWith(consonants[k]))
                            {
                                sentence[i].RemoveAt(j);
                            }
                        }

                    }
                }
            }
        }
        public void replaceWords(int numOfSent, int length, string newWord)
        {
            numOfSent = numOfSent - 1;
            for (int i = 0; i < sentence[numOfSent].Count; i++)
            {
                if (sentence[numOfSent][i].Length == length)
                {
                    sentence[numOfSent][i]=newWord;
                }
            }
        }
        void Swap(ref int item1, ref int  item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }
}
