//-----------------------------------------------------------------------
// <copyright file="Commands.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Objects
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Commands"/> enumeration.
    /// </summary>
    public enum Commands
    {
        None,
        SetActiveSyncVirtualDirectory,
        SetAutodiscoverVirtualDirectory,
        SetEcpVirtualDirectory,
        SetMapiVirtualDirectory,
        SetPowerShellVirtualDirectory,
        SetOabVirtualDirectory,
        SetOwaVirtualDirectory,
        SetWebServicesVirtualDirectory,
        Testing
    }
}