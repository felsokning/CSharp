//-----------------------------------------------------------------------
// <copyright file="PowerShellWrapper.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Objects
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PowerShellWrapper"/> class.
    /// </summary>
    public class PowerShellWrapper : IDisposable
    {
        /// <summary>
        ///     The PowerShell object we intend to wrap.
        /// </summary>
        private PowerShell newPowerShell;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PowerShellWrapper"/> class.
        /// </summary>
        public PowerShellWrapper()
        {
            this.newPowerShell = PowerShell.Create();
        }

        /// <summary>
        ///     Adds the command to the PowerShell instance.
        /// </summary>
        /// <param name="cmdlet">Add a command to construct a command pipeline.</param>
        public void AddCommand(string cmdlet)
        {
            this.newPowerShell.AddCommand(cmdlet);
        }

        /// <summary>
        ///     Adds the parameter to the command.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">Value for the parameter.</param>
        public void AddParameter(string parameterName, object value)
        {
            this.newPowerShell.AddParameter(parameterName, value);
        }

        /// <summary>
        ///     Initiates a new instance of the <see cref="PowerShellWrapper"/> class.
        /// </summary>
        /// <returns>The create.</returns>
        public PowerShell Create()
        {
            this.newPowerShell = PowerShell.Create();
            return this.newPowerShell;
        }

        /// <summary>
        ///     Invoke the command on the PowerShell instance.
        /// </summary>
        /// <returns>Result from the invocation (almost always void).</returns>
        public Collection<PSObject> Invoke()
        {
            return this.newPowerShell.Invoke();
        }

        /// <summary>
        ///     Releases all resource used by the <see cref="PowerShellWrapper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="PowerShellWrapper"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="PowerShellWrapper"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="PowerShellWrapper"/> so the garbage collector can reclaim the
        /// memory that the <see cref="PowerShellWrapper"/> was occupying.</remarks>
        public void Dispose()
        {
            this.newPowerShell.Dispose();
            this.Dispose(true);
        }

        /// <summary>
        ///     Releases all resource used by the <see cref="PowerShellWrapper"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="PowerShellWrapper"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="PowerShellWrapper"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="PowerShellWrapper"/> so the garbage collector can reclaim the
        /// memory that the <see cref="PowerShellWrapper"/> was occupying.</remarks>
        /// <param name="all">If all should be disposed.</param>
        protected virtual void Dispose(bool all)
        {
            this.newPowerShell.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}