using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLibrary
{
    public abstract class AbstractCollectionGenerator
    {
        private readonly IFaker faker;
        public AbstractCollectionGenerator(IFaker faker)
        {
            this.faker = faker;
        }
    }
}
