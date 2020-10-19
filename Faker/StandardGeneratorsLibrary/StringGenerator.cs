using System;
using System.Linq;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class StringGenerator : RandomGenerator, IGenerator
    {
        private static readonly string CHARSET = " ABCDEF GHIJKLM NOPQRS TUVWXYZ abcdef ghijklm nopqrs tuvwxyz ";
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
