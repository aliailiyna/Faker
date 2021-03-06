﻿using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassD
    {
        public string russianString;
        public string numberString;
        public byte Number1 { get; }
        public byte Number2 { get; }
        public byte Number3 { get; }
        public byte Number4 { get; }

        // some private constructors
        private ClassD(byte number1)
        {
            Number1 = number1;
        }

        private ClassD(byte number1, byte number2)
        {
            Number1 = number1;
            Number2 = number2;
        }

        private ClassD(byte number1, byte number3, byte number4)
        {
            Number1 = number1;
            Number3 = number3;
            Number4 = number4;
        }
    }
}
