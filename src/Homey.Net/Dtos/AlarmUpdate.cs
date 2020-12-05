namespace Homey.Net.Dtos
{
    public class AlarmUpdate
    {
        public bool Enabled { get; set; }

        public Repetition Repetition { get; set; }

        public string Time { get; set; }
    }
}
