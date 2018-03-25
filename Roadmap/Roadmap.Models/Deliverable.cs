using System;

namespace RoadmapModels
{
    public class Deliverable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Category Category { get; set; }
    }
}
