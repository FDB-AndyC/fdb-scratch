// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITicketWriter.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ITicketWriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    using System.Collections.Generic;

    public interface ITicketWriter
    {
        void PostStories(IEnumerable<IStory> stories);
    }
}
