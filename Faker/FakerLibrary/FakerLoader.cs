using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using GeneratorsInterfacesLibrary;
using PathNavigatorLibrary;
using System.IO;
using System.Linq.Expressions;

namespace FakerLibrary
{
    internal class FakerLoader : IFakerLoader
    {
        private Expression<Func<Type, bool>> lambdaGenerator = type => type.GetInterfaces().Contains(typeof(IGenerator)) && type.IsClass && !type.IsAbstract;
        private Expression<Func<Type, bool>> lambdaCollectionGenerator = type => type.GetInterfaces().Contains(typeof(ICollectionGenerator)) && type.IsClass && !type.IsAbstract;
        private IPathNavigator pathNavigator = new PathNavigator();
        private Dictionary<Type, IGenerator> standardGeneratorsDictionary = new Dictionary<Type, IGenerator>();
        private Dictionary<Type, ICollectionGenerator> standardCollectionGeneratorsDictionary = new Dictionary<Type, ICollectionGenerator>();
        private Dictionary<Type, IGenerator> pluginGeneratorsDictionary = new Dictionary<Type, IGenerator>();
        private Dictionary<Type, ICollectionGenerator> pluginCollectionGeneratorsDictionary = new Dictionary<Type, ICollectionGenerator>();
        private Dictionary<ConfigKey, IGenerator> configGeneratorsDictionary = new Dictionary<ConfigKey, IGenerator>();
        private Dictionary<ConfigKey, ICollectionGenerator> configCollectionGeneratorsDictionary = new Dictionary<ConfigKey, ICollectionGenerator>();

        public FakerLoader()
        {
            this.LoadStandardGenerators();
            this.LoadPluginGenerators();
        }

        // загрузка стандартных генераторов
        private void LoadStandardGenerators()
        {
            Assembly standardGeneratorsAssembly = Assembly.LoadFile(pathNavigator.GetStandardDirectory());
            
            Console.WriteLine("Стандартные генераторы");

            // IGenerator
            Type[] standardGeneratorsTypes = standardGeneratorsAssembly.GetTypes().Where(lambdaGenerator.Compile()).ToArray();
            IGenerator generator;
            foreach (Type type in standardGeneratorsTypes)
            {
                generator = standardGeneratorsAssembly.CreateInstance(type.FullName) as IGenerator;
                standardGeneratorsDictionary.TryAdd(generator.GetObjectType(), generator);
                Console.WriteLine(type.Name);
            }

            // ICollectionGenerator
            Type[] standardCollectionGeneratorsTypes = standardGeneratorsAssembly.GetTypes().Where(lambdaCollectionGenerator.Compile()).ToArray();
            ICollectionGenerator collectionGenerator;
            foreach (Type type in standardCollectionGeneratorsTypes)
            {
                collectionGenerator = standardGeneratorsAssembly.CreateInstance(type.FullName) as ICollectionGenerator;
                standardCollectionGeneratorsDictionary.TryAdd(collectionGenerator.GetCollectionType(), collectionGenerator);
                Console.WriteLine(type.Name);
            }
        }

        // загрузка генераторов из плагинов
        private void LoadPluginGenerators()
        {
            string pluginsPath = pathNavigator.GetPluginsDirectory();
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginsPath);

            if (!pluginDirectory.Exists)
            {
                pluginDirectory.Create();
            }

            string[] pluginFiles = Directory.GetFiles(pluginsPath, pathNavigator.GetPluginExtension());

            Console.WriteLine("Генераторы из плагинов");

            foreach (var file in pluginFiles)
            {
                Assembly asm = Assembly.LoadFile(file);

                // IGenerator
                Type[] pluginGeneratorsTypes = asm.GetTypes().Where(lambdaGenerator.Compile()).ToArray();

                IGenerator generator;
                foreach (Type type in pluginGeneratorsTypes)
                {
                    generator = asm.CreateInstance(type.FullName) as IGenerator;
                    pluginGeneratorsDictionary.TryAdd(generator.GetObjectType(), generator);
                    Console.WriteLine(type.Name);
                }

                // ICollectionGenerator
                Type[] pluginCollectionGeneratorsTypes = asm.GetTypes().Where(lambdaCollectionGenerator.Compile()).ToArray();

                ICollectionGenerator collectionGenerator;
                foreach (Type type in pluginCollectionGeneratorsTypes)
                {
                    collectionGenerator = asm.CreateInstance(type.FullName) as ICollectionGenerator;
                    pluginCollectionGeneratorsDictionary.TryAdd(collectionGenerator.GetCollectionType(), collectionGenerator);
                    Console.WriteLine(type.Name);
                }
            }
        }

        public Dictionary<Type, IGenerator> getStandardGenerators()
        {
            return standardGeneratorsDictionary;
        }

        public Dictionary<Type, ICollectionGenerator> getStandardCollectionGenerators()
        {
            return standardCollectionGeneratorsDictionary;
        }

        public Dictionary<Type, IGenerator> getPluginGenerators()
        {
            return pluginGeneratorsDictionary;
        }

        public Dictionary<Type, ICollectionGenerator> getPluginCollectionGenerators()
        {
            return pluginCollectionGeneratorsDictionary;
        }

        public Dictionary<ConfigKey, IGenerator> getConfigGenerators(IFakerConfig fakerConfig)
        {
            return configGeneratorsDictionary;
        }

        public Dictionary<ConfigKey, ICollectionGenerator> getConfigCollectionGenerators(IFakerConfig fakerConfig)
        {
            return configCollectionGeneratorsDictionary;
        }
    }
}
