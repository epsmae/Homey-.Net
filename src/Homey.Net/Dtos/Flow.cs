using System.Collections.Generic;

namespace Homey.Net.Dtos
{
    public class Flow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public object Folder { get; set; }
        public int Order { get; set; }
        public Trigger Trigger { get; set; }
        public IList<Condition> Conditions { get; set; }
        public IList<Action> Actions { get; set; }
        public bool Broken { get; set; }
        public bool Triggerable { get; set; }
        public int TriggerCount { get; set; }
    }
}
