using System;
using GeneratorsInterfacesLibrary;

namespace ConfigGeneratorsLibrary
{
    public class RussianStringGenerator : RandomGenerator, IGenerator
    {
        private static readonly string CHARSET = " АБВГД ЕЁЖЗИЙ КЛМНО ПРСТУФ ХЦЧШЩ ЪЫЬЭЮЯ абвгд еёжзий клмно прстуф хцчшщ ъыьэюя ";
        private static readonly int MIN_LENGTH = 0;
        private static readonly int MAX_LENGTH = 20;
        public object GenerateObject()
        {
            int length = random.Next(MIN_LENGTH, MAX_LENGTH + 1);
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += CHARSET[random.Next(CHARSET.Length)];
            }
            return result;
        }

        public Type GetObjectType()
        {
            return typeof(string);
        }
    }
}
