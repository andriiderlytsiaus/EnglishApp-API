using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.WordModels
{
    public class Definition
    {
        public string Word { get; set; } = null!;
        public string Phonetic { get; set; } = null!;
        public Meaning[] Meanings { get; set; } = null!;

        public void Display()
        {
            Console.WriteLine($"Word: {Word}");
            Console.WriteLine($"Phonetic: {Phonetic}");

            if (Meanings != null)
            {
                Console.WriteLine("Meanings:");
                foreach (var meaning in Meanings)
                {
                    DisplayMeaning(meaning);
                }
            }
        }

        private void DisplayMeaning(Meaning meaning)
        {
            Console.WriteLine($"  Part of Speech: {meaning.PartOfSpeech}");

            if (meaning.Definitions != null)
            {
                Console.WriteLine("  Definitions:");
                foreach (var definitionInfo in meaning.Definitions)
                {
                    DisplayDefinitionInfo(definitionInfo);
                }
            }
        }

        private void DisplayDefinitionInfo(DefinitionInfo definitionInfo)
        {
            Console.WriteLine($"    Definition: {definitionInfo.Definition}");
            Console.WriteLine($"    Example: {definitionInfo.Example}");
        }
    }
}
