namespace FileGenerator
{
    using System;
    using System.IO;
    using System.Text;

    class Program
    {
        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            var phrase = ReadString("Phrase: ");
            var phraseAmount = ReadInt("Phrase amount: ");
            var stringAmount = ReadInt("String amount: ");

            if (phraseAmount >= stringAmount)
            {
                throw new ArgumentException("PhraseAmount cannot be more or equal StringAmount");
            }

            if (phraseAmount == 0 || stringAmount == 0)
            {
                throw new ArgumentException("PhraseAmount and StringAmount cannot be zero");
            }

            using (var writer = new StreamWriter(".\\data.txt"))
            {
                var frequnce = (double) stringAmount / (double) phraseAmount;
                var freqCount = 0.0;
                for (var i = 0; i < stringAmount; i++)
                {
                    string line;
                    if (i > freqCount)
                    {
                        line = GenerateLine(phrase);
                        freqCount += frequnce;
                    }
                    else
                    {
                        line = GenerateLine();
                    }

                    writer.WriteLine(line);
                }
            }
        }

        private static string GenerateLine(string content = null)
        {
            const int lineLength = 42;
            if (content == null)
            {
                return GenerateSubstring(lineLength);
            }

            if (content.Length > lineLength)
            {
                return content.Substring(0, lineLength);
            }
            
            var position = Random.Next(0, 3);

            if (position == 0)
            {
                return content + GenerateSubstring(lineLength - content.Length);
            }

            if (position == 1)
            {
                return GenerateSubstring(lineLength - content.Length) + content;
            }

            var substringLength1 = (lineLength - content.Length) / 2;
            var reminder = (lineLength - content.Length) % 2;
            var substringLength2 = substringLength1 + reminder;

            return GenerateSubstring(substringLength1) + content + GenerateSubstring(substringLength2);
        }

        private static string GenerateSubstring(int length = 42)
        {
            if (length < 0)
            {
                throw new ArgumentException("length cannot be less than 0");
            }

            const string alphabet = "qwertyuiopasdfghjklzxcvbnm";
            var alphabetLength = alphabet.Length;
            
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var randomIndex = Random.Next(0, alphabetLength - 1);
                sb.Append(alphabet[randomIndex]);
            }

            return sb.ToString();
        }

        private static int ReadInt(string prompt = null)
        {
            Console.Write(prompt);
            var result = Convert.ToInt32(Console.ReadLine());
            return result;
        }

        private static string ReadString(string prompt = null)
        {
            Console.Write(prompt);
            var result = Console.ReadLine();
            return result;
        }
    }
}
