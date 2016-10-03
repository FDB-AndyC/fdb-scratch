// --------------------------------public ------------------------------------------------------------------------------------
// <copyright file="ScrumdoIteration.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoIteration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScrumDoExtractor
{
    using System;

    using Contracts;

    public class ScrumdoIteration : IIteration
    {
        public IProject Project { get; private set; }

        public string Name { get; private set; }

        public int IterationId { get; private set; }

        public string Detail { get; private set; }

        public int IterationType { get; private set; }

        public DateTime? Start { get; private set; }

        public DateTime? End { get; private set; }

        public bool IsHidden { get; private set; }

        public bool IsDefault { get; private set; }

        public static ScrumdoIteration Create(IProject project, dynamic source)
        {
            return new ScrumdoIteration
                       {
                           Project = project,
                           Name = source.name,
                           IterationId = source.id,
                           Detail = source.detail,
                           IterationType = source.iteration_type,
                           Start = source.start_date,
                           End = source.end_date,
                           IsHidden = source.hidden,
                           IsDefault = source.default_iteration
                       };
        }
    }
}
