using System;
using System.Collections.Generic;
using System.Text;
using DTOAttributeLibrary;

namespace DTOObjectsLibrary
{
    [DTO]
    public class ClassA
    {
        // plugins
        public DateTime dateTime;
        public DayOfWeek dayOfWeek;
        public List<DateTime> dateTimeList;
        public List<DayOfWeek> DayOfWeekList { set; get; }

        // config
        public List<string> catStringList;
        public List<string> DogStringList { set;  get; }

        // config + setting in constructor
        public DateTime DateTime2020 { get; }
        public byte Number { get; }

        public ClassA(DateTime DateTime2020)
        {
            this.DateTime2020 = DateTime2020;
        }
        public ClassA(DateTime DateTime2020, byte Number)
        {
            this.DateTime2020 = DateTime2020;
            this.Number = Number;
        }
    }
}
