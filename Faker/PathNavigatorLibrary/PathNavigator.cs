using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PathNavigatorLibrary
{
    public class PathNavigator : IPathNavigator
    {
        private static readonly string rootDirectory;
        private static readonly string writeResultDirectory;
        private static readonly string standardDirectory;
        private static readonly string pluginsDirectory;
        private const string writeResultDirectoryAdd = @"\Result";
        private const string standardDirectoryAdd = @"\Standard\StandardGeneratorsLibrary.dll";
        private const string pluginsDirectoryAdd = @"\Plugins";
        private const string PLUGIN_EXTENSION = "*.dll";

        // нахождение путей
        static PathNavigator()
        {
            string path = Directory.GetCurrentDirectory();
            for (int i = 0; i < 4; i++)
            {
                path = Directory.GetParent(path).FullName;
            }
            rootDirectory = path;
            writeResultDirectory = String.Concat(rootDirectory, writeResultDirectoryAdd);
            standardDirectory = String.Concat(rootDirectory, standardDirectoryAdd);
            pluginsDirectory = String.Concat(rootDirectory, pluginsDirectoryAdd);
        }

        // получение путя к папке с проектом
        public string GetRootDirectory()
        {
            return rootDirectory;
        }

        // получение путя к папке для записи результатов
        public string GetWriteResultDirectory()
        {
            return writeResultDirectory;
        }

        // получение путя к папке со стандартными генераторами
        public string GetStandardDirectory()
        {
            return standardDirectory;
        }

        // получение путя к папке с плагинами
        public string GetPluginsDirectory()
        {
            return pluginsDirectory;
        }

        // получение шаблона расширения плагинов
        public string GetPluginExtension()
        {
            return PLUGIN_EXTENSION;
        }
    }
}
