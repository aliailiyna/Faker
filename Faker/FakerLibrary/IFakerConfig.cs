using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public interface IFakerConfig
    {
        public void Add<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> 
            fieldGetExpr, IGenerator generator);

        public List<(Type, string, Type, IGenerator)> GetConfigGenerators();
    }
}
