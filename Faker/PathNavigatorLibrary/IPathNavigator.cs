using System;
using System.Collections.Generic;
using System.Text;

namespace PathNavigatorLibrary
{
    public interface IPathNavigator
    {
        // получение путя к папке с проектом
        string GetRootDirectory();

        // получение путя к папке для записи результатов
        string GetWriteResultDirectory();

        // получение путя к папке со стандартными генераторами
        string GetStandardDirectory();

        // получение путя к папке с плагинами
        string GetPluginsDirectory();

        // получение шаблона расширения плагинов
        string GetPluginExtension();
    }
}
