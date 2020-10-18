using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    [ToUse]
    public class ClassA
    {
        //public byte number;
        public byte ConfigNumber { get; }
        public ClassA(byte ConfigNumber)
        {
            this.ConfigNumber = ConfigNumber;
        }
    }
}
