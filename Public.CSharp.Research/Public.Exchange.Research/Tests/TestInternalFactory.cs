//-----------------------------------------------------------------------
// <copyright file="TestInternalFactory.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TestInternalFactory"/> class.
    /// </summary>
    [TestClass]
    public class TestInternalFactory
    {
        [TestMethod]
        public void TestClassInstantiation()
        {
            InternalFactory newInternalFactory = new InternalFactory();
            Assert.IsNotNull(newInternalFactory);
            Assert.IsInstanceOfType(newInternalFactory, typeof(InternalFactory));
        }

        [TestMethod]
        public void TestBeginPowerShellMethod()
        {
            InternalFactory newInternalFactory = new InternalFactory();
            Assert.IsNotNull(newInternalFactory);
            Assert.IsInstanceOfType(newInternalFactory, typeof(InternalFactory));
            newInternalFactory.DoAutomationWork(Commands.Testing, true, new[] { "Test" }, "", "");
        }
    }
}