// -------------------------public -------------------------------------------------------------------------------------------
// <copyright file="IProject.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the IProject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    using System.Collections.Generic;

    public interface IProject
    {
        IOrganisation Organisation { get; }

        int Id { get; }

        string Category { get; }

        string Description { get; }

        IEnumerable<string> Tags { get; }

        string ShortName { get; }

        string LongName { get; }
    }
}
