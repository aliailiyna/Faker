using System;
using GeneratorsInterfacesLibrary;

namespace DayOfWeekGeneratorLibrary
{
    public class DayOfWeekGenerator : IGenerator
    {
        private static Random random = new Random();
        public object GenerateObject()
        {
            DayOfWeek dayOfWeek = (DayOfWeek)random.Next(7);
            return dayOfWeek;
        }

        public Type GetObjectType()
        {
            return typeof(DayOfWeek);
        }
    }
}
