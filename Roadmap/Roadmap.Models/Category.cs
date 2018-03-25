namespace Roadmap.Models
{
    using System;

    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ColourIndex { get; set; }
    }
}
