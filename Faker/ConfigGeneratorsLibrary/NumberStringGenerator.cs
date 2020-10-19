using System;
using GeneratorsInterfacesLibrary;

namespace ConfigGeneratorsLibrary
{
    public class NumberStringGenerator : RandomGenerator, IGenerator
    {
        private static readonly string CHARSET = "0123456789";
        private static readonly int MIN_LENGTH = 0;
        private static readonly int MAX_LENGTH = 10;
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
