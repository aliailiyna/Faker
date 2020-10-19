using System;
using System.Collections.Generic;
using System.Text;

namespace FakerInterfaceLibrary
{
    public interface IFaker
    {
        // используется для создания объектов
        public type Create<type>();

        // используется в реализации ICollectionGenerator
        public object Create(Type objectType);
    }
}
