using System.IO;
using PathNavigatorLibrary;

namespace WritingLibrary
{
    public class FileWriter: IWriter
    {
        public void Write(string str, string name)
        {
            PathNavigator pathNavigator = new PathNavigator();
            string path = pathNavigator.GetWriteResultDirectory();

            using (StreamWriter sw = new StreamWriter(path + @"\" + name, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(str);
            }
        }
    }
}
