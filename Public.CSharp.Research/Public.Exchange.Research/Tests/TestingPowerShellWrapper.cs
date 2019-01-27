//-----------------------------------------------------------------------
// <copyright file="TestingPowerShellWrapper.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Tests
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Public.Exchange.Research.Objects;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TestingPowerShellWrapper"/> class.
    /// </summary>
    [TestClass]
    public class TestingPowerShellWrapper
    {
        /// <summary>
        ///     Tests that the class instantiates successfully.
        /// </summary>
        [TestMethod]
        public void TestInstantiation()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            Assert.IsNotNull(newPowerShellWrapper);
            Assert.IsInstanceOfType(newPowerShellWrapper, typeof(PowerShellWrapper));
        }

        /// <summary>
        ///     Tests that adding commands is successful.
        /// </summary>
        [TestMethod]
        public void TestAddCommands()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
        }

        /// <summary>
        ///     Tests that adding parameters is successful.
        /// </summary>
        [TestMethod]
        public void TestAddParameters()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
            newPowerShellWrapper.AddParameter("Test", "Test");
        }

        /// <summary>
        ///     Tests that the invoke method executes successfully.
        /// </summary>
        [TestMethod]
        public void TestInvokeMethod()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
            newPowerShellWrapper.AddParameter("Test", "Test");
            Collection<PSObject> returnedObjects = newPowerShellWrapper.Invoke();
            Assert.IsTrue(returnedObjects.Count > 0);
        }

        /// <summary>
        ///     Tests that the dispose method executes successfully.
        /// </summary>
        [TestMethod]
        public void TestDisposeMethod()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
            newPowerShellWrapper.AddParameter("Test", "Test");
            newPowerShellWrapper.Dispose();
        }
    }
}