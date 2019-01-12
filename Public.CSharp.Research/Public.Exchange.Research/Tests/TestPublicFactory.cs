//-----------------------------------------------------------------------
// <copyright file="TestPublicFactory.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System;
    using System.Management.Automation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objects;

    /// <summary>
    ///     Test internal factory.
    /// </summary>
    [TestClass]
    public class TestPublicFactory
    {
        /// <summary>
        ///     Tests null three parameters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullThreeParameters()
        {
            PublicFactory.StartAutomation(Commands.SetActiveSyncVirtualDirectory, null, null, null);
        }

        /// <summary>
        ///     Tests null three parameters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandNotFoundException))]
        public void TestNullTwoParameters()
        {
            PublicFactory.StartAutomation(Commands.SetActiveSyncVirtualDirectory, new[] { "server1" }, null, null);
        }

        /// <summary>
        ///     Tests null three parameters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandNotFoundException))]
        public void TestNullOneParameter()
        {
            PublicFactory.StartAutomation(Commands.SetActiveSyncVirtualDirectory, new[] { "server1" }, "Parameter", null);
        }

        /// <summary>
        ///     Tests null three parameters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandNotFoundException))]
        public void TestAllParameters()
        {
            PublicFactory.StartAutomation(Commands.SetActiveSyncVirtualDirectory, new[] { "server1" }, "Parameter", "Value");
        }

        /// <summary>
        ///     Tests No Command.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CommandNotFoundException))]
        public void TestNoneCommand()
        {
            PublicFactory.StartAutomation(Commands.None, new[] { "server1" }, "Parameter", "Value");
        }
    }
}