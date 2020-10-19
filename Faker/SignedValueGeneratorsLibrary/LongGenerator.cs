using System;
using GeneratorsInterfacesLibrary;

namespace SignedValueGeneratorsLibrary
{
    public class LongGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от Int64.MinValue до Int64.MaxValue
            long a = (random.Next(Int32.MinValue, Int32.MaxValue) + random.Next(0, 2)) *
                (random.Next(Int32.MinValue, Int32.MaxValue) + random.Next(0, 2));
            long b = (random.Next(Int32.MinValue, Int32.MaxValue) + random.Next(0, 2)) *
                (random.Next(Int32.MinValue, Int32.MaxValue) + random.Next(0, 2));
            if ((a == (long)Int32.MaxValue + 1) && (b == (long)Int32.MaxValue + 1))
            {
                b--;
            }
            return (long)(a + b);
        }

        public Type GetObjectType()
        {
            return typeof(long);
        }

    }
}
