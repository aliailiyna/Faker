﻿using System;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class ByteGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от Byte.MinValue до Byte.MaxValue
            return Convert.ToByte(random.Next(Byte.MinValue, Byte.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(byte);
        }
    }
}
