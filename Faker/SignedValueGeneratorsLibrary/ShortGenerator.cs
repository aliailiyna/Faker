using System;
using GeneratorsInterfacesLibrary;

namespace SignedValueGeneratorsLibrary
{
    class ShortGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от Int16.MinValue до Int16.MaxValue
            return Convert.ToInt16(random.Next(Int16.MinValue, Int16.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(short);
        }
    }
}
