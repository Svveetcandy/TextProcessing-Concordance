using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TextProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            const string FileConcordance = "Corcondance.txt";
            const string FileText = "TextRus.txt";



            bool escape = false;
            

            while (!escape)
            {
                Symbol symbols = new Symbol();
                symbols.ReadText(FileText);
                Word word = new Word(symbols.SymblolsToWord());
                Sentence sentence = new Sentence();
                Punctuation punct = new Punctuation();
                punct.InputPunctuation(word.SymbToPunct());
                sentence.InputSentence(word.WordsToSentence(sentence, punct.NumberOfSentences()));
                int length;

                Console.Clear();
                Console.WriteLine("1. Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.");
                Console.WriteLine("2. Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.");
                Console.WriteLine("3. Из текста удалить все слова заданной длины, начинающиеся на согласную букву.");
                Console.WriteLine("4. В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.");
                Console.WriteLine("5. Конкорданс.");
                Console.WriteLine("6. Выход");
                Console.Write("\n\nВыберите задание:");
                int.TryParse(Console.ReadLine(), out int taskNum);
                switch (taskNum)
                {
                    case 1:
                        int[] arr = sentence.OutputSortedSentences();
                        using (StreamWriter writer = new StreamWriter(File.Open("FileForTasks(1-4).txt", FileMode.Create)))
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                for (int j = 0; j < sentence.sentence[arr[i]].Count; j++)
                                {
                                    writer.Write(sentence.sentence[arr[i]][j]);
                                }
                                writer.Write("\r\n");
                            }
                        }
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Введите длину слова: ");
                        int.TryParse(Console.ReadLine(), out length);
                        List<string> wordsFromVopros = new List<string>();
                        wordsFromVopros = sentence.voprosSentences(punct.GetNumVopros(), length);
                        foreach (string i in wordsFromVopros)
                        {
                            Console.WriteLine(i);
                        }
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Введите длину слова: ");
                        int.TryParse(Console.ReadLine(), out length);
                        sentence.removeWords(length);
                        using (StreamWriter writer = new StreamWriter(File.Open("FileForTasks(1-4).txt", FileMode.Create)))
                        {
                            for (int i = 0; i < sentence.sentence.Length; i++)
                            {
                                foreach (string j in sentence.sentence[i])
                                {
                                    writer.Write(j);
                                }
                            }
                        }

                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Введите номер предложения: ");
                        int.TryParse(Console.ReadLine(), out int numOfSent);
                        Console.Write("Введите длину слова: ");
                        int.TryParse(Console.ReadLine(), out length);
                        Console.Write("Введите новое слово: ");
                        sentence.replaceWords(numOfSent, length, Console.ReadLine());
                        using (StreamWriter writer = new StreamWriter(File.Open("FileForTasks(1-4).txt", FileMode.Create)))
                        {
                            for (int i = 0; i < sentence.sentence.Length; i++)
                            {
                                foreach (string j in sentence.sentence[i])
                                {
                                    writer.Write(j);
                                }
                            }
                        }

                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;
                    case 5:
                        word.Concordance(FileConcordance);
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;
                    case 6:
                        escape = true;
                        break;
                    default:
                        Console.WriteLine("Такого задания нет. Выберите задание снова. ");
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        break;

                }
            }
        }
    }
}
