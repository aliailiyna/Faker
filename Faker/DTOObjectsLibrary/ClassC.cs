using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassC
    {
        // standard
        public List<bool> listBool;
        public ushort uShort;

        public string stringFirst;
        public string stringSecond;

        public List<byte> ListByte { get; }
        public uint UInt { set; get; }
        public ulong ULong { set; get; }

        // only private constructor
        private ClassC(List<byte> ListByte)
        {
            this.ListByte = ListByte;
        }
    }
}
