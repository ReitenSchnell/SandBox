using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Name of file: ");
            var fileName = Console.ReadLine();
            var words = new FileManager().GetWordsInFile(fileName);
            var badwords = new List<Word>();
            words.OrderBy(word => Guid.NewGuid()).ToList().ForEach(word =>
                                                                       {
                                                                           Console.Write(word.Translation + ": ");
                                                                           var result = Console.ReadLine();
                                                                           Console.WriteLine(result == word.Original ? "perfect" : word.Original);
                                                                           if (result != word.Original)
                                                                               badwords.Add(word);
                                                                       });
            while (badwords.Any())
            {
                Console.ReadLine();
                Console.Clear();
                var newbadwords = new List<Word>();
                badwords.OrderBy(word => Guid.NewGuid()).ToList().ForEach(word =>
                {
                    Console.Write(word.Translation + ": ");
                    var result = Console.ReadLine();
                    Console.WriteLine(result == word.Original ? "perfect" : word.Original);
                    if (result != word.Original)
                        newbadwords.Add(word);
                });
                badwords = newbadwords;
            }
            Console.ReadLine();
        }
    }
}
