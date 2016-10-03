// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrumdoOrganisation.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoOrganisation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScrumDoExtractor
{
    using Contracts;

    public class ScrumdoOrganisation : IOrganisation
    {
        public string ShortName { get; private set; }

        public string Description { get; private set; }

        public static ScrumdoOrganisation Create(dynamic source)
        {
            return new ScrumdoOrganisation
                       {
                           ShortName = source.slug,
                           Description = source.description
                       };
        }
    }
}
