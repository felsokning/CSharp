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
        /// <summary>
        ///     No command was specified.
        /// </summary>
        None,

        /// <summary>
        ///     Set Active Sync Virtual Directory was specified.
        /// </summary>
        SetActiveSyncVirtualDirectory,

        /// <summary>
        ///     Set Autodiscover Virtual Directory was specified.
        /// </summary>
        SetAutodiscoverVirtualDirectory,

        /// <summary>
        ///     Set Ecp Virtual Directory was specified.
        /// </summary>
        SetEcpVirtualDirectory,

        /// <summary>
        ///     Set Mapi Virtual Directory was specified.
        /// </summary>
        SetMapiVirtualDirectory,

        /// <summary>
        ///     Set PowerShell Virtual Directory was specified.
        /// </summary>
        SetPowerShellVirtualDirectory,

        /// <summary>
        ///     Set OAB Virtual Directory was specified.
        /// </summary>
        SetOabVirtualDirectory,

        /// <summary>
        ///     Set OWA Virtual Directory was specified.
        /// </summary>
        SetOwaVirtualDirectory,

        /// <summary>
        ///     Set Web Services Virtual Directory was specified.
        /// </summary>
        SetWebServicesVirtualDirectory,

        /// <summary>
        ///     Used in testing.
        /// </summary>
        Testing
    }
}