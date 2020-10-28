using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;

namespace TextProcessing
{
    class Symbol
    {
        List<char> symbols = new List<char>();
        List<string> symbolsOfWord = new List<string>();

        //string text;
        public void ReadText(string filename)
        {
            using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
            {
                string text = sr.ReadToEnd();
                text=ReplaceTabulationSpacesByOneSpace(text);
                for (int i = 0; i < text.Length; i++)
                {
                    symbols.Add(text[i]);
                }
            }
        }
        public List<string> SymblolsToWord()
        {
            List<char> symbToWord = new List<char>();
            List<string> Words = new List<string>();
            string punctWord="", enterWord, spaceWord;
            for (int i = 0; i < symbols.Count; i++)
            {
                symbToWord.Add(symbols[i]);
                string word = new string(symbToWord.ToArray());
                switch (word[word.Length - 1])
                {
                    case ' ':
                        word = word.Substring(0, word.Length - 1);
                        separateWordPunct(word, punctWord,  Words);
                        symbToWord.Clear();
                        spaceWord = " ";
                        Words.Add(spaceWord);

                        break;
                    case '\n':
                        word = word.Substring(0, word.Length - 1);
                        word = word.Substring(0, word.Length - 1);
                        separateWordPunct(word, punctWord,  Words);
                        symbToWord.Clear();
                        enterWord = "\r\n";
                        Words.Add(enterWord);

                        break;
                }
                if (i == symbols.Count - 1)
                {
                    separateWordPunct(word, punctWord, Words);
                }

            }
            return Words;
        }
        void separateWordPunct(string word, string punctWord, List<string>Words)
        {
            bool skobka = false, punct;
            int indexOfSkobka=0;
            if (word != "")
            {
                if (word[0] == '(')
                {
                    word = word.Remove(0, 1);
                    Words.Add("(");
                }
                if (word.Contains(")"))
                {
                    indexOfSkobka = word.IndexOf(")");
                    word = word.Remove(word.IndexOf(")"), 1);
                    skobka = true;
                }
                switch (word[word.Length - 1])
                {
                    case '.':
                        if (word[word.Length - 2] == '.' && word[word.Length - 3] == '.')
                        {
                            punctWord = "...";
                            word = word.Substring(0, word.Length - 3);
                        }
                        else {
                            punctWord = ".";
                            word = word.Substring(0, word.Length - 1);
                        }
                        punct = true;
                        break;
                    case '!':
                        punctWord = "!";
                        word = word.Substring(0, word.Length - 1);
                        punct = true;
                        break;
                    case '?':
                        punctWord = "?";
                        word = word.Substring(0, word.Length - 1);
                        punct = true;
                        break;
                    case ',':
                        punctWord = ",";
                        word = word.Substring(0, word.Length - 1);
                        punct = true;
                        break;
                    case ':':
                        word = word.Substring(0, word.Length - 1);
                        punctWord = ":";
                        punct = true;
                        break;
                    case ';':
                        word = word.Substring(0, word.Length - 1);
                        punctWord = ";";
                        punct = true;
                        break;
                    default:
                        punct = false;
                        break;
                }
                Words.Add(word);
                if (punct)
                {
                    if (indexOfSkobka == word.Length)
                    {
                        Words.Add(")");
                        Words.Add(punctWord);
                    }
                    else
                    {
                        Words.Add(punctWord);
                        if(skobka) Words.Add(")");

                    }

                }
            }
        }
        string ReplaceTabulationSpacesByOneSpace(string tempo)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            tempo = regex.Replace(tempo, " ");
            return tempo;
        }
    }
}