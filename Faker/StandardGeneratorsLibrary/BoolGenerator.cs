using System;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class BoolGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от 0 до 1
            return Convert.ToBoolean(random.Next(0, 2));
        }

        public Type GetObjectType()
        {
            return typeof(bool);
        }
    }
}
