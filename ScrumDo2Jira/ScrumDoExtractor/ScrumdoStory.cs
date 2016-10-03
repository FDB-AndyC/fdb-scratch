// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrumdoStory.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoStory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScrumDoExtractor
{
    using Contracts;

    public class ScrumdoStory : IStory
    {
        public IProject Project { get; private set; }

        public int IterationId { get; private set; }

        public int Points { get; private set; }

        public string Summary { get; private set; }

        public string Detail { get; private set; }

        public static ScrumdoStory Create(IProject project, dynamic source)
        {
            return new ScrumdoStory
                       {
                           Project = project,
                           IterationId = source.iteration_id,
                           Points = source.points,
                           Summary = source.summary,
                           Detail = source.detail
                       };
        }
    }
}
