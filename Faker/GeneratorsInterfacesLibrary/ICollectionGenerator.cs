using System;
using System.Collections.Generic;
using System.Text;
using FakerInterfaceLibrary;

namespace GeneratorsInterfacesLibrary
{
    public interface ICollectionGenerator
    {
        object GenetateCollection(Type elementType, IFaker faker);

        Type GetCollectionType();
    }
}
