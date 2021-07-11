using System;

namespace IISTestApplication.Models.MapperModels
{
    public class CalendarEvent
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }
    }

    public class CalendarEventForm
    {
        public DateTime EventDate { get; set; }

        public int EventHour { get; set; }

        public int EventMinute { get; set; }

        public string Title { get; set; }
    }
}
