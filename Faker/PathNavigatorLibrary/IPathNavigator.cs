using System;
using System.Collections.Generic;
using System.Text;

namespace PathNavigatorLibrary
{
    public interface IPathNavigator
    {
        string GetRootDirectory();

        string GetWriteResultDirectory();

        string GetStandardDirectory();

        string GetPluginsDirectory();
    }
}
