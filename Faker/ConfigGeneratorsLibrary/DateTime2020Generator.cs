using System;
using GeneratorsInterfacesLibrary;

namespace ConfigGeneratorsLibrary
{
    public class DateTime2020Generator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            int year = 2020;
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            int hour = random.Next(24);
            int minute = random.Next(60);
            int second = random.Next(60);
            DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
            return dateTime;
        }

        public Type GetObjectType()
        {
            return typeof(DateTime);
        }
    }
}
