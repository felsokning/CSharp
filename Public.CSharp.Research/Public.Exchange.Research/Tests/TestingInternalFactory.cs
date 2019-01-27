//-----------------------------------------------------------------------
// <copyright file="TestingInternalFactory.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Public.Exchange.Research.Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TestingInternalFactory"/> class.
    /// </summary>
    [TestClass]
    public class TestingInternalFactory
    {
        /// <summary>
        ///     Test that the class instantiates successfully.
        /// </summary>
        [TestMethod]
        public void TestClassInstantiation()
        {
            InternalFactory newInternalFactory = new InternalFactory();
            Assert.IsNotNull(newInternalFactory);
            Assert.IsInstanceOfType(newInternalFactory, typeof(InternalFactory));
        }

        /// <summary>
        ///     Tests that the method call works.
        /// </summary>
        [TestMethod]
        public void TestBeginPowerShellMethod()
        {
            InternalFactory newInternalFactory = new InternalFactory();
            Assert.IsNotNull(newInternalFactory);
            Assert.IsInstanceOfType(newInternalFactory, typeof(InternalFactory));
            newInternalFactory.DoAutomationWork(Commands.Testing, true, new[] { "Test" }, string.Empty, string.Empty);
        }
    }
}