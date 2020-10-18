using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace ConfigGeneratorsLibrary
{
    public class NumberGenerator : IGenerator
    {
        private static Random random = new Random();
        public object GenerateObject()
        {
            // генерация случайного числа от 1 до 5
            return Convert.ToByte(random.Next(1, 6));
        }

        public Type GetObjectType()
        {
            return typeof(byte);
        }
    }
}
