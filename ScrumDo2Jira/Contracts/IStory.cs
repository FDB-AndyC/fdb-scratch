// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStory.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the IStory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    public interface IStory
    {
        IProject Project { get; }

        int IterationId { get; }

        int Points { get; }

        string Summary { get; }

        string Detail { get; }
    }
}