// --------------------------------public ------------------------------------------------------------------------------------
// <copyright file="ScrumdoProject.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoProject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScrumDoExtractor
{
    using System.Collections.Generic;

    using Contracts;

    public class ScrumdoProject : IProject
    {
        public IOrganisation Organisation { get; }

        public int Id { get; private set; }

        public string Category { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<string> Tags { get; private set; }

        public string ShortName { get; private set; }

        public string LongName { get; private set; }

        public static ScrumdoProject Create(IOrganisation organisation, dynamic project)
        {
            return new ScrumdoProject
                       {
                           Category = project.category,
                           Id = project.id,
                           Description = project.description,
                           ShortName = project.slug,
                           LongName = project.name
                       };
        }
    }
}
