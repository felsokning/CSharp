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
        /// <param name="passedCommand">Passed command.</param>
        /// <param name="servers">Servers.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="value">Value.</param>
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
                throw e.InnerException;
            }
        }
    }
}