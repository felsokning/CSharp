//-----------------------------------------------------------------------
// <copyright file="InternalFactory.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    using Methods;
    using Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InternalFactory"/> class.
    /// </summary>
    public sealed class InternalFactory
    {
        /// <summary>
        ///     Internal entry into the automation framework.
        /// </summary>
        /// <param name="passedCommand">Passed command.</param>
        /// <param name="servers">Servers.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="value">Value.</param>
        public void DoAutomationWork(Commands passedCommand, bool testing, string[] servers, string parameter, string value)
        {
            // As the number of servers could be in the hundreds,
            // we parallel the execution so that the tasks may be performed
            // "simultaneously". See the reference source for Parallel.ForEach
            // for details on how "simultaneous" may not be the case.
            Parallel.ForEach(servers, (x) =>
            {
                PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
                this.BeginPowerShellWork(newPowerShellWrapper.Create, testing, passedCommand, x, parameter, value);
            });
        }

        /// <summary>
        ///     Creates PowerShell instance via Func<T> and runs the command.
        /// </summary>
        /// <param name="creator">Creator.</param>
        /// <param name="command">Command.</param>
        /// <param name="server">Server.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="value">Value.</param>
        private void BeginPowerShellWork(Func<PowerShell> creator, bool testing, Commands command, string server, string parameter, string value)
        {
            // Tested in PowerShell in Unix. Daring, I'm aware...
            using (PowerShell newPowerShell = creator())
            {
                // We add the PSSnap-In for this instance.
                if (!testing)
                {
                    newPowerShell.AddCommand("Add-PSSnapIn Microsoft.Exchange.Management.PowerShell.E2010");
                    newPowerShell.Invoke();
                }

                newPowerShell.AddCommand(command.GetStringValue());

                if (!testing)
                {
                    newPowerShell.AddParameter("-Identity", command.GetIdentityString(server));
                    newPowerShell.AddParameter(parameter, value);
                }

                // Now, we do the maths.
                newPowerShell.Invoke();
            }
        }
    }
}