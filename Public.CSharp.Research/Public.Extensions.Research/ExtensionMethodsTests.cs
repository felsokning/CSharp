//-----------------------------------------------------------------------
// <copyright file="ExtensionMethodsTests.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Extensions.Research
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExtensionMethodsTests"/> class.
    /// </summary>
    [TestClass]
    public class ExtensionMethodsTests
    {
        /// <summary>
        ///     Validates that the method returns a value that is not zero.
        /// </summary>
        [TestMethod]
        public void ValidateVeckanUtcNow()
        {
            DateTime dateTime = DateTime.UtcNow;
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
        }

        /// <summary>
        ///     Validates that the method returns the correct value for the date.
        /// </summary>
        [TestMethod]
        public void ValidateVeckan1990()
        {
            DateTime dateTime = new DateTime(1990, 10, 10);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 41);
        }

        /// <summary>
        ///     Validates that the method returns the correct value for the date of FREEDOM.
        /// </summary>
        [TestMethod]
        public void ValidateVeckanFreedom()
        {
            DateTime dateTime = new DateTime(1776, 07, 04);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 27);
        }

        /// <summary>
        ///     Validates that the method returns the correct value for the date of the Battle of Åsle.
        /// </summary>
        [TestMethod]
        // ReSharper disable once StyleCop.SA1650 -- Already in the dictionary. Y U HATE ME ReSharper!?
        public void ValidateBattleOfÅsle()
        {
            DateTime dateTime = new DateTime(1389, 02, 24);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 9);
        }

        /// <summary>
        ///     Validates that the method fails for all zeroes (as it should).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateAllZeroes()
        {
            DateTime dateTime = new DateTime(0, 0, 0);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 9);
        }
    }
}