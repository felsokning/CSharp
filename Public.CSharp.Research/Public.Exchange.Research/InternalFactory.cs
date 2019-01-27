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
        /// <param name="passedCommand">Command to be run.</param>
        /// <param name="testing">Boolean to determine if this is being called by a test method.</param>
        /// <param name="servers">The servers to run the commands against.</param>
        /// <param name="parameter">Name of the Parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public void DoAutomationWork(Commands passedCommand, bool testing, string[] servers, string parameter, string value)
        {
            // As the number of servers could be in the hundreds,
            // we parallel the execution so that the tasks may be performed
            // "simultaneously". See the reference source for Parallel.ForEach
            // for details on how "simultaneous" may not be the case.
            Parallel.ForEach(
                servers, 
                (x) =>
            {
                PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
                this.BeginPowerShellWork(newPowerShellWrapper.Create, testing, passedCommand, x, parameter, value);
            });
        }

        /// <summary>
        ///     Creates PowerShell instance and runs the command.
        /// </summary>
        /// <param name="creator">The function that instantiates the instance.</param>
        /// <param name="testing">Boolean to determine if this is being called by a test method.</param>
        /// <param name="command">Command to be run.</param>
        /// <param name="server">The servers to run the commands against.</param>
        /// <param name="parameter">Name of the Parameter.</param>
        /// <param name="value">Value of the parameter.</param>
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