// -------------------------public -------------------------------------------------------------------------------------------
// <copyright file="IOrganisation.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the IOrganisation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Contracts
{
    public interface IOrganisation
    {
        string ShortName { get; }

        string Description { get; }
    }
}