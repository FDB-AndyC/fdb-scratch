namespace Roadmap.Models
{
    using System;

    public class Milestone
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EventDate { get; set; }
    }
}
