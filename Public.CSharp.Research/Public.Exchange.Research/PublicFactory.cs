//-----------------------------------------------------------------------
// <copyright file="PublicFactory.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System;

    using Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PublicFactory"/> class.
    /// </summary>
    public static class PublicFactory
    {
        /// <summary>
        ///     This is the public entry point for the PowerShell Automation.
        /// </summary>
        /// <param name="passedCommand">Command to be run.</param>
        /// <param name="servers">The servers to run the commands against.</param>
        /// <param name="parameter">Name of the Parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public static void StartAutomation(Commands passedCommand, string[] servers, string parameter, string value)
        {
            try
            {
                InternalFactory newInternalFactory = new InternalFactory();
                newInternalFactory.DoAutomationWork(passedCommand, false, servers, parameter, value);
            }
            catch (AggregateException e)
            {
                // As the exception is wrapped, we only care about the original one.
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
            }
        }
    }
}