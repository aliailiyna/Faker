using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public interface IFakerConfig
    {
        public void AddGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> 
            fieldGetExpr, IGenerator generator);

        public void AddCollectionGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>>
            fieldGetExpr, ICollectionGenerator generator);

        public Dictionary<Type, Dictionary<(string, Type), IGenerator>> GetConfigGenerators();
        public Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>> GetConfigCollectionGenerators();
    }
}
