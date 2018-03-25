namespace Roadmap.Models
{
    using System;
    using System.Collections.Generic;

    public class Swimlane
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public ICollection<Deliverable> Deliverables { get; set; }

    }
}
