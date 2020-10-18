using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class BoolGenerator : IGenerator
    {
        private static Random random = new Random();
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
