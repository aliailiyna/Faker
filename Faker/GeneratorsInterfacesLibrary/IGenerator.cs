using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorsInterfacesLibrary
{
    public interface IGenerator
    {
        object GenerateObject();

        Type GetObjectType();
    }
}
