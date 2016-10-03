// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JiraInjector.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the JiraInjector type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRAInjector
{
    using System.Collections.Generic;

    using Contracts;

    public class JiraInjector : ITicketWriter
    {
        public string ProjectKey { get; private set; }

        public JiraInjector(string projectKey)
        {
            this.ProjectKey = projectKey;
        }

        public void PostStories(IEnumerable<IStory> stories)
        {
            throw new System.NotImplementedException();
        }
    }
}
