using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class Email
    {
        private readonly int _numbers = 5;
        private readonly char[] qwertyInputs = new char[]
            {
                // Letters
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',

                // Numbers
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',

                // Special characters
                '!', '@', '#', '$', '%', '&', '*', '(', ')', '?', '=', '+',
                '[', '{', ']', '}', ';', ':', ',', '.', '/', '-'
            };
        private readonly int arrayLength = 83;

        public string GenerateSecretEmailKey()
        {
            Random random = new Random();
            HashSet<char> secretKeySet = new HashSet<char>();

            // Keep generating random unique characters until we have 5 distinct ones
            while (secretKeySet.Count < _numbers)
            {
                char randomChar = qwertyInputs[random.Next(0, arrayLength)];
                // HashSet prevents duplicates
                secretKeySet.Add(randomChar); 
            }

            return string.Concat(secretKeySet);
        }
    }
}