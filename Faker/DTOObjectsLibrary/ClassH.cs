using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassH
    {
        // standard
        public byte Number1 { set; get; }

        // cyclical links
        public ClassI classI;
    }
}
