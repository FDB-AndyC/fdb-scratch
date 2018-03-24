﻿using System;
using System.Collections.Generic;

namespace Roadmap.Models
{
    public partial class Deliverable
    {
        public Guid Id { get; set; }
        public Guid RoadmapId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SwimlaneId { get; set; }

        public Category Category { get; set; }
        public Roadmap Roadmap { get; set; }
        public Swimlane Swimlane { get; set; }
    }
}
