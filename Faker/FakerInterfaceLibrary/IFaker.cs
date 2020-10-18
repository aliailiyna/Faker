using System;
using System.Collections.Generic;
using System.Text;

namespace FakerInterfaceLibrary
{
    public interface IFaker
    {
        public type Create<type>();

        public object Create(Type objectType);
    }
}
