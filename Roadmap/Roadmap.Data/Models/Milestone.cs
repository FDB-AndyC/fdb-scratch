using System;
using System.Collections.Generic;

namespace Roadmap.Data.Models
{
    public partial class Milestone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public Guid RoadmapId { get; set; }

        public Roadmap Roadmap { get; set; }
    }
}
