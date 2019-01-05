//-----------------------------------------------------------------------
// <copyright file="TestObjects.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Objects;

    /// <summary>
    ///     Test the objects created.
    /// </summary>
    [TestClass]
    public class TestObjects
    {
        /// <summary>
        ///     Tests None Enumeration.
        /// </summary>
        [TestMethod]
        public void TestNoneCommand()
        {
            Commands testing = Commands.None;
            Assert.AreEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests ActiveSync Enumeration.
        /// </summary>
        [TestMethod]
        public void TestActiveSyncCommand()
        {
            Commands testing = Commands.SetActiveSyncVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests Autodiscover Enumeration.
        /// </summary>
        [TestMethod]
        public void TestAutodiscoverCommand()
        {
            Commands testing = Commands.SetAutodiscoverVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests Ecp Enumeration.
        /// </summary>
        [TestMethod]
        public void TestEcpCommand()
        {
            Commands testing = Commands.SetEcpVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests Mapi Enumeration.
        /// </summary>
        [TestMethod]
        public void TestMapiCommand()
        {
            Commands testing = Commands.SetMapiVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests OAB Enumeration.
        /// </summary>
        [TestMethod]
        public void TestOabCommand()
        {
            Commands testing = Commands.SetOabVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests OWA Enumeration.
        /// </summary>
        [TestMethod]
        public void TestOwaCommand()
        {
            Commands testing = Commands.SetOwaVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests PowerShell Enumeration.
        /// </summary>
        [TestMethod]
        public void TestPowerShellCommand()
        {
            Commands testing = Commands.SetPowerShellVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }

        /// <summary>
        ///     Tests Web Services Enumeration.
        /// </summary>
        [TestMethod]
        public void TestWebServicesCommand()
        {
            Commands testing = Commands.SetWebServicesVirtualDirectory;
            Assert.AreEqual(testing, Commands.SetWebServicesVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.None);
            Assert.AreNotEqual(testing, Commands.SetActiveSyncVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetAutodiscoverVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetEcpVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetMapiVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOabVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetOwaVirtualDirectory);
            Assert.AreNotEqual(testing, Commands.SetPowerShellVirtualDirectory);
            Assert.IsInstanceOfType(testing, typeof(Commands));
        }
    }
}