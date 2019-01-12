//-----------------------------------------------------------------------
// <copyright file="TestPowerShellWrapper.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objects;

    [TestClass]
    public class TestPowerShellWrapper
    {
        [TestMethod]
        public void TestInstantiation()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            Assert.IsNotNull(newPowerShellWrapper);
            Assert.IsInstanceOfType(newPowerShellWrapper, typeof(PowerShellWrapper));
        }

        [TestMethod]
        public void TestAddCommands()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
        }

        [TestMethod]
        public void TestAddParameters()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
            newPowerShellWrapper.AddParameter("Test", "Test");
        }

        [TestMethod]
        public void TestInvokeMethod()
        {
            PowerShellWrapper newPowerShellWrapper = new PowerShellWrapper();
            newPowerShellWrapper.AddCommand("Get-Command");
            newPowerShellWrapper.AddParameter("Test", "Test");
            Collection<PSObject> returnedObjects = newPowerShellWrapper.Invoke();
            Assert.IsTrue(returnedObjects.Count > 0);
        }

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