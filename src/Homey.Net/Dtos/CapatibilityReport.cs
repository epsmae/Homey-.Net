using System;
using System.Collections.Generic;

namespace Homey.Net.Dtos
{
    public class CapatibilityReport
    {
        public IList<TimeValue> Values { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Step { get; set; }
        public string Uri { get; set; }
        public string Id { get; set; }
        public int LastValue { get; set; }
        public int UpdatesIn { get; set; }
    }
}
