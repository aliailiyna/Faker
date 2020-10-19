using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassI
    {
        // standard
        public byte Number1 { set; get; }

        // cyclical links
        public ClassG classG;
    }
}
