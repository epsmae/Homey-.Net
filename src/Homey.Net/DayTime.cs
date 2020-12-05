using System;

namespace Homey.Net
{
    public class DayTime
    {
        private readonly int _hour;
        private readonly int _minutes;

        public DayTime(int hour, int minutes)
        {
            if (hour < 0 || hour > 24 || minutes < 0 || minutes > 60)
            {
                throw new ArgumentException("invalid time or minutes");
            }

            _hour = hour;
            _minutes = minutes;
        }

        public int Minutes
        {
            get
            {
                return _minutes;
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }
        }

        public override string ToString()
        {
            return $"{Hour}:{Minutes}";
        }
    }
}
