using System;
using GeneratorsInterfacesLibrary;

namespace ConfigGeneratorsLibrary
{
    public class NumberGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от 1 до 2
            return Convert.ToByte(random.Next(1, 3));
        }

        public Type GetObjectType()
        {
            return typeof(byte);
        }
    }
}
