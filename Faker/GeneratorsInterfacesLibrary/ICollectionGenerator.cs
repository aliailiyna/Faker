using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorsInterfacesLibrary
{
    public interface ICollectionGenerator
    {
        object GenetateCollection(Type elementType);

        Type GetCollectionType();
    }
}
