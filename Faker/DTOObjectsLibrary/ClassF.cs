using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassF
    {
        // standard
        public byte Number1 { set; get; }
        public byte Number2 { set; get; }
        public byte Number3 { set; get; }
        public byte Number4 { set; get; }

        // cyclical links + setting in constructor
        public ClassG G { get; }

        // only private constructor
        private ClassF(ClassG G)
        {
            this.G = G;
        }

    }
}
