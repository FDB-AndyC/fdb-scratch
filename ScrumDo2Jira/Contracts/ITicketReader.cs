﻿// -------------------------public -------------------------------------------------------------------------------------------
// <copyright file="ITicketReader.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ITicketReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    using System.Collections.Generic;

    public interface ITicketReader
    {
        IEnumerable<IOrganisation> GetOrganisations();

        IEnumerable<IProject> GetProjects(IOrganisation organisation);

        IEnumerable<IStory> GetStories(IProject project);

        IEnumerable<IIteration> GetIterations(IProject project);
    }
}
