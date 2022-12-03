using System;
using System.Text.Json.Serialization;

namespace Timelogger.Entities
{
    public class TimeRegistration
    {
        public int TimeRegistrationId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int TimeSpent { get; set; }
    }
}
