using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsTrainer
{
    public class FileManager
    {
         public List<Word> GetWordsInFile(string fileName)
         {
             var file = Directory.GetFiles("words", fileName + ".txt", SearchOption.TopDirectoryOnly).FirstOrDefault();
             if (file == null)
             {
                 Console.WriteLine("No such file");
                 return new List<Word>();
             }
             var lines = File.ReadAllLines(file);
             return lines.Select(s =>
                              {
                                  var pair = s.Split(new[] {','}).ToList();
                                  return new Word
                                             {
                                                 Original = pair[0].Trim(), 
                                                 Translation = pair[1].Trim()
                                             };
                              }).ToList();
             
         }
    }
}