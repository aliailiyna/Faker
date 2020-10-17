using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using GeneratorsInterfacesLibrary;
using RandomGeneratorLibrary;

namespace StandardGeneratorsLibrary
{
    public class BoolGenerator : IGenerator
    {
        public object GenetateObject()
        {
            // генерация случайного числа от 0 до 1
            return Convert.ToBoolean(RandomGenerator.random.Next(0, 2));
        }

        public Type GetObjectType()
        {
            return typeof(bool);
        }
    }
}
