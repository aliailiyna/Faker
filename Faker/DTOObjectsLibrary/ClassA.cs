using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassA
    {
        //public byte number;

        public List<string> configList;
        public byte ConfigNumber { set; get; }
        //public ClassA(byte ConfigNumber)
        //{
            //this.ConfigNumber = ConfigNumber;
        //}
    }
}
