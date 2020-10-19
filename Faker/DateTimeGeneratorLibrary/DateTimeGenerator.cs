using System;
using GeneratorsInterfacesLibrary;

namespace DateTimeGeneratorLibrary
{
    public class DateTimeGenerator : IGenerator
    {
        private static Random random = new Random();
        public object GenerateObject()
        {
            int year = random.Next(1950, 2051);
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
