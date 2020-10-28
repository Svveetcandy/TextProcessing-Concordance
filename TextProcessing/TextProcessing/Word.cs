using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;

namespace TextProcessing
{
    class Word
    {
        public List<string> Words { get; } = new List<string>();

        public List<string> wordsConcordanceFinal { get; set; } = new List<string>();

        public List<string> numOfLinesFinal { get; set; } = new List<string>();
        public Word(List<string> Words)
        {
            this.Words = Words;
        }
        public List<string> SymbToPunct()
        {
            List<string> punct = new List<string>();
            int count = 0, numOfSentence = 1;
            foreach (string i in Words)
            {
                switch (i)
                {
                    case ".":
                        punct.Add(numOfSentence.ToString());
                        punct.Add(i);
                        numOfSentence++;
                        break;
                    case "!":
                        punct.Add(numOfSentence.ToString());
                        punct.Add(i);
                        numOfSentence++;
                        break;
                    case "?":
                        punct.Add(numOfSentence.ToString());
                        punct.Add(i);
                        numOfSentence++;
                        break;
                    case "...":
                        punct.Add(numOfSentence.ToString());
                        punct.Add(i);
                        numOfSentence++;
                        break;
                }
                count++;
            }
            return punct;
        }
        public string[][] WordsToSentence(Sentence Sent, int num)
        {
            List<string> words = new List<string>();
            string[][] array = new string[num][];
            bool sentenceAccept = false;
            num = 0;
            int count = 0;
            foreach (string i in Words)
            {
                words.Add(i);
                if (sentenceAccept)
                {

                    array[num] = words.ToArray();
                    num++;
                    words.Clear();
                    sentenceAccept = false;
                }
                if (i == "." || i == "!" || i == "?" || i == "...")
                {
                    sentenceAccept = true;
                }
                if (count == Words.Count - 1)
                {
                    array[num] = words.ToArray();
                    num++;
                    words.Clear();
                }
                count++;
            }
            return array;
        }
        public void Concordance(string FileName)
        {
            Console.Write("Введите количество строк в странице: ");
            int linesInStranica = int.Parse(Console.ReadLine());
            List<string> wordsConcordance = new List<string>();
            List<int> numOfStranic = new List<int>();
            List<int> locationOfHits = new List<int>();
            string line;
            int index, count = 0, numOfLine = 1, numOfHits = 0;
            foreach (string i in Words)
            {
                if (i != "." && i != "," && i != "!" && i != ":" && i != " " && i != "-" && i != "?" && i != "" && i != "\r\n" && i != ";" && i != "(" && i != ")" && i != "—" && i != "...")
                {
                    wordsConcordance.Add(i.ToLower());
                    numOfStranic.Add((numOfLine / linesInStranica) + 1);
                }
                count = i.Length + count;
                if (count == 1024) numOfLine++;
                else
                {
                    if (i == "\r\n") numOfLine++;
                }
            }
            string word;
            for (int i = 0; i < wordsConcordance.Count - 1; i++)
            {
                i = 0;
                word = wordsConcordance[i];

                if (wordsConcordanceFinal.Count != 0)
                {
                    if (!wordsConcordanceFinal.Contains(word))
                    {
                        wordsConcordanceFinal.Add(word);
                    }
                }
                else
                {
                    wordsConcordanceFinal.Add(word);
                }
                do
                {
                    numOfHits++;
                    index = wordsConcordance.IndexOf(word);
                    locationOfHits.Add(numOfStranic[index]);
                    wordsConcordance.RemoveAt(index);                           // Подсчет количества повторений слов, и их расположение в тексте.
                    numOfStranic.RemoveAt(index);
                } while (wordsConcordance.IndexOf(word) != -1);
                locationOfHits.Sort();
                var q = locationOfHits.Distinct();
                line = "...................................." + numOfHits + ":";
                foreach (int k in q)
                {
                    line = line + k + " ";
                }
                numOfLinesFinal.Add(line);
                locationOfHits.Clear();
                numOfHits = 0;
            }

            //sortList;
            string bufWord, bufNum;

            for (int i = 0; i < wordsConcordanceFinal.Count - 1; i++)
            {
                for (int j = i + 1; j < wordsConcordanceFinal.Count; j++)
                {

                    if (string.Compare(wordsConcordanceFinal[i], wordsConcordanceFinal[j])==1)//wordsConcordanceFinal[i] > wordsConcordanceFinal[j])
                    {

                        bufWord = wordsConcordanceFinal[i];
                        wordsConcordanceFinal[i] = wordsConcordanceFinal[j];
                        wordsConcordanceFinal[j] = bufWord;

                        bufNum = numOfLinesFinal[i];
                        numOfLinesFinal[i] = numOfLinesFinal[j];
                        numOfLinesFinal[j] = bufNum;
                    }
                }
            }
            WriteConcordance(FileName);
        }
        void WriteConcordance(string FileName)
        {
            char firstLetter='A';
            using (StreamWriter writer = new StreamWriter(File.Open(FileName, FileMode.Create)))
            {
                for (int i = 0; i < wordsConcordanceFinal.Count; i++)
                {
                    if (i == 0)
                    {
                        firstLetter = wordsConcordanceFinal[i][0];
                        writer.Write(firstLetter.ToString().ToUpper() + "\r\n\r\n");
                    }
                    if(firstLetter!= wordsConcordanceFinal[i][0])
                    {
                        firstLetter = wordsConcordanceFinal[i][0];
                        writer.Write(firstLetter.ToString().ToUpper() + "\r\n\r\n");
                    }
                    writer.Write(wordsConcordanceFinal[i] + numOfLinesFinal[i] + "\r\n\r\n");
                }
            }
        }
    }
}
