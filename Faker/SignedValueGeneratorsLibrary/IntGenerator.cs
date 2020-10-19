using System;
using GeneratorsInterfacesLibrary;

namespace SignedValueGeneratorsLibrary
{
    public class IntGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от Int32.MinValue до Int32.MaxValue
            return (random.Next(Int32.MinValue, Int32.MaxValue)) + (random.Next(0, 2));
        }

        public Type GetObjectType()
        {
            return typeof(int);
        }
    }
}
