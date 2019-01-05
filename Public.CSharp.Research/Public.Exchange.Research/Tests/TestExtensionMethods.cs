//-----------------------------------------------------------------------
// <copyright file="TestExtensionMethods.cs" company="None">
//     Copyright (c) John Bailey. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Methods;
    using Objects;

    /// <summary>
    ///     Initializes a new instance of <see cref="TestExtensionMethods"/> class
    ///     to test the extension methods we've implemented.
    /// </summary>
    [TestClass]
    public class TestExtensionMethods
    {
        /// <summary>
        ///     Tests the none enumeration.
        ///     This should ALWAYS throw not implemented.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestNoneEnumeration()
        {
            Commands.None.GetStringValue();
        }

        /// <summary>
        ///     Tests the none enumeration.
        ///     This should ALWAYS throw not implemented.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestNoneEnumerationAgain()
        {
            Commands.None.GetIdentityString("server1");
        }

        /// <summary>
        ///     Tests the default enumeration.
        ///     This should ALWAYS throw not implemented.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestDefaultEnumeration()
        {
            Commands commands = (Commands)10000;
            commands.GetStringValue();
        }

        /// <summary>
        ///     Tests the default enumeration.
        ///     This should ALWAYS throw not implemented.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestDefaultEnumerationAgain()
        {
            Commands commands = (Commands)10000;
            commands.GetIdentityString("server1");
        }

        /// <summary>
        ///     Tests the first enumeration.
        /// </summary>
        [TestMethod]
        public void TestFirstEnumeration()
        {
            string returnedString = Commands.SetActiveSyncVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-ActiveSyncVirtualDirectory"));
            string identityString = Commands.SetActiveSyncVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("ActiveSync"));
        }

        /// <summary>
        ///     Tests the second enumeration.
        /// </summary>
        [TestMethod]
        public void TestSecondEnumeration()
        {
            string returnedString = Commands.SetAutodiscoverVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-AutodiscoverVirtualDirectory"));
            string identityString = Commands.SetAutodiscoverVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Autodiscover"));
        }

        /// <summary>
        ///     Tests the third enumeration.
        /// </summary>
        [TestMethod]
        public void TestThirdEnumeration()
        {
            string returnedString = Commands.SetEcpVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-EcpVirtualDirectory"));
            string identityString = Commands.SetEcpVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Ecp"));
        }

        /// <summary>
        ///     Tests the fourth enumeration.
        /// </summary>
        [TestMethod]
        public void TestFourthEnumeration()
        {
            string returnedString = Commands.SetMapiVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-MapiVirtualDirectory"));
            string identityString = Commands.SetMapiVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Mapi"));
        }

        /// <summary>
        ///     Tests the fifth enumeration.
        /// </summary>
        [TestMethod]
        public void TestFifthEnumeration()
        {
            string returnedString = Commands.SetOabVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-OabVirtualDirectory"));
            string identityString = Commands.SetOabVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Oab"));
        }

        /// <summary>
        ///     Tests the sixth enumeration.
        /// </summary>
        [TestMethod]
        public void TestSixthEnumeration()
        {
            string returnedString = Commands.SetOwaVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-OwaVirtualDirectory"));
            string identityString = Commands.SetOwaVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Owa"));
        }

        /// <summary>
        ///     Tests the seventh enumeration.
        /// </summary>
        [TestMethod]
        public void TestSeventhEnumeration()
        {
            string returnedString = Commands.SetPowerShellVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-PowerShellVirtualDirectory"));
            string identityString = Commands.SetPowerShellVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("PowerShell"));
        }

        /// <summary>
        ///     Tests the eighth enumeration.
        /// </summary>
        [TestMethod]
        public void TestEighthEnumeration()
        {
            string returnedString = Commands.SetWebServicesVirtualDirectory.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Set-WebServicesVirtualDirectory"));
            string identityString = Commands.SetWebServicesVirtualDirectory.GetIdentityString("server1");
            Assert.IsFalse(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Contains("Web"));
        }

        /// <summary>
        ///     Tests the testing enumeration.
        /// </summary>
        [TestMethod]
        public void TestTestingEnumeration()
        {
            string returnedString = Commands.Testing.GetStringValue();
            Assert.IsFalse(string.IsNullOrWhiteSpace(returnedString));
            Assert.IsTrue(returnedString.Contains("Command"));
            string identityString = Commands.Testing.GetIdentityString(string.Empty);
            Assert.IsTrue(string.IsNullOrWhiteSpace(identityString));
            Assert.IsTrue(identityString.Equals(string.Empty));
        }
    }
}