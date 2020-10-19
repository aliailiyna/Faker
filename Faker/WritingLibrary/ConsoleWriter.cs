using System;

namespace WritingLibrary
{
    public class ConsoleWriter: IWriter
    {
        public void Write(string str, string name)
        {
            Console.WriteLine(name + "\n");
            Console.WriteLine(str);
        }
    }
}
