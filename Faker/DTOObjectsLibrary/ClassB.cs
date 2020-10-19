using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassB
    {
        // cyclical links
        public ClassB classB;

        // plugins 
        public int intNumber;
        public long longNumber;
        public short ShortNumber { set; get; }
        public sbyte SByteNumber { set; get; }

        // no generator
        public decimal decimalNumber;
        public double doubleNumber;
        public float FloatNumber;
    }
}
