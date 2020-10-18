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
        public string GetRootDirectory()
        {
            return rootDirectory;
        }

        public string GetWriteResultDirectory()
        {
            return writeResultDirectory;
        }

        public string GetStandardDirectory()
        {
            return standardDirectory;
        }


        public string GetPluginsDirectory()
        {
            return pluginsDirectory;
        }

        public string GetPluginExtension()
        {
            return PLUGIN_EXTENSION;
        }
    }
}
