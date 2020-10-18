using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    [ToUse]
    public class ClassE
    {
        public byte Number1 { get; }
        public byte Number2 { get; }
        public byte Number3 { get; }
        public byte Number4 { get; }
        
        /*private ClassE(byte number1)
        {
            Number1 = number1;
        }

        private ClassE(byte number1, byte number2)
        {
            Number1 = number1;
            Number2 = number2;
        }

        private ClassE(byte number1, byte number3, byte number4)
        {
            Number1 = number1;
            Number3 = number3;
            Number4 = number4;
        }*/

        public ClassE()
        {

        }

        public ClassE(byte number1)
        {
            Number1 = number1;
        }

        public ClassE(byte number1, byte number2)
        {
            Number1 = number1;
            Number2 = number2;
        }

        public ClassE(byte number1, byte number3, byte number4)
        {
            Number1 = number1;
            Number3 = number3;
            Number4 = number4;
        }

    }
}
