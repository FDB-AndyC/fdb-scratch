namespace Roadmap.Models
{
    using System;
    using System.Collections.Generic;

    public class Roadmap
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Swimlane> Swimlanes { get; set; }

        public ICollection<Milestone> Milestones { get; set; }
    }
}
