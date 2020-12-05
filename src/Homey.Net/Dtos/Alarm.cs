using System;

namespace Homey.Net.Dtos
{
    public class Alarm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public bool Enabled { get; set; }

        public DateTime? NextOccurance { get; set; }

        public Repetition Repetition { get; set; }
    }
}
