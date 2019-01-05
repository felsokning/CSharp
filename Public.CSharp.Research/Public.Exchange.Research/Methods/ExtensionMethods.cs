//-----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Methods
{
    using System;

    using Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExtensionMethods"/> class.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Returns a string representing the enumeration's equivalent command.
        /// </summary>
        /// <returns>The string value.</returns>
        /// <param name="passedCommand">Passed command.</param>
        public static string GetStringValue(this Commands passedCommand)
        {
            switch (passedCommand)
            {
                case Commands.SetActiveSyncVirtualDirectory:
                    return "Set-ActiveSyncVirtualDirectory";
                case Commands.SetAutodiscoverVirtualDirectory:
                    return "Set-AutodiscoverVirtualDirectory";
                case Commands.SetEcpVirtualDirectory:
                    return "Set-EcpVirtualDirectory";
                case Commands.SetMapiVirtualDirectory:
                    return "Set-MapiVirtualDirectory";
                case Commands.SetOabVirtualDirectory:
                    return "Set-OabVirtualDirectory";
                case Commands.SetOwaVirtualDirectory:
                    return "Set-OwaVirtualDirectory";
                case Commands.SetPowerShellVirtualDirectory:
                    return "Set-PowerShellVirtualDirectory";
                case Commands.SetWebServicesVirtualDirectory:
                    return "Set-WebServicesVirtualDirectory";
                case Commands.Testing:
                    return "Get-Command";
                case Commands.None:
                    throw new NotImplementedException("Enum was not implemented correctly.");
                default:
                    throw new NotImplementedException("Enum was not implemented correctly.");
            }
        }

        /// <summary>
        ///     Gets the identity string for the command.
        /// </summary>
        /// <returns>The identity string.</returns>
        /// <param name="passedCommand">Passed command.</param>
        /// <param name="server">Server.</param>
        public static string GetIdentityString(this Commands passedCommand, string server)
        {
            switch (passedCommand)
            {
                case Commands.SetActiveSyncVirtualDirectory:
                    return $"{server}\\ActiveSync (Default Web Site)";
                case Commands.SetAutodiscoverVirtualDirectory:
                    return $"{server}\\Autodiscover (Default Web Site)";
                case Commands.SetEcpVirtualDirectory:
                    return $"{server}\\Ecp";
                case Commands.SetMapiVirtualDirectory:
                    return $"{server}\\Mapi";
                case Commands.SetOabVirtualDirectory:
                    return $"{server}\\Oab";
                case Commands.SetOwaVirtualDirectory:
                    return $"{server}\\Owa";
                case Commands.SetPowerShellVirtualDirectory:
                    return $"{server}\\PowerShell";
                case Commands.SetWebServicesVirtualDirectory:
                    return $"{server}\\Web Services";
                case Commands.Testing:
                    return string.Empty;
                case Commands.None:
                    throw new NotImplementedException("Enum was not implemented correctly.");
                default:
                    throw new NotImplementedException("Enum was not implemented correctly.");
            }
        }
    }
}