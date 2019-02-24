//-----------------------------------------------------------------------
// <copyright file="RegistryChanges.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Integration
{
    using System.Diagnostics;
    using System.Security.AccessControl;
    using System.Security.Principal;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RegistryChanges"/> class.
    /// </summary>
    public class RegistryChanges
    {
        /// <summary>
        ///     Initializes RegistrySecurity as 'regSec'.
        /// </summary>
        private static RegistrySecurity regSec = new RegistrySecurity();

        /// <summary>
        ///     Add Access Rules for Registry Security
        /// </summary>
        public static void AddAccessRules()
        {
            // We add trace notification that we're entering the method.
            Trace.TraceInformation("Entering 'AddAccessRules' method.");
            Trace.Flush();

            using (WindowsIdentity winid = WindowsIdentity.GetCurrent())
            {
                // Get the sid of the Local System
                SecurityIdentifier localSystemSid = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, winid.User.AccountDomainSid);

                // Get the sid of BuildinAdministrators
                SecurityIdentifier builtinAdministratorsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, winid.User.AccountDomainSid);

                // Add a rule that grants FullControl right for local system. 
                RegistryAccessRule localSystemRule = new RegistryAccessRule(localSystemSid, RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);

                // Add the rules using RegistrySecurity
                regSec.AddAccessRule(localSystemRule);

                // Add a rule that grants FullControl right for BuildinAdministrators. 
                RegistryAccessRule builtinAdministratorsRule = new RegistryAccessRule(builtinAdministratorsSid, RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);

                // Adds the rule using RegistrySecurity
                regSec.AddAccessRule(builtinAdministratorsRule);
            }

            // We add trace notification that we're exiting the method.
            Trace.TraceInformation("Exiting 'AddAccessRules' method.");
            Trace.Flush();
        }
    }
}