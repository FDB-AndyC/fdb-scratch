// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIteration.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the IIteration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    using System;

    public interface IIteration
    {
        IProject Project { get; }

        string Name { get; }

        int IterationId { get; }

        string Detail { get; }

        int IterationType { get; }

        DateTime? Start { get; }

        DateTime? End { get; }

        bool IsHidden { get; }

        bool IsDefault { get; }
    }
}
