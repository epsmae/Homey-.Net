namespace Homey.Net.Dtos
{
    public class Condition
    {
        public string Uri { get; set; }
        public string Id { get; set; }
        public string Group { get; set; }
        public Args Args { get; set; }
        public object Delay { get; set; }
        public object Duration { get; set; }
    }
}
