//-----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.ExtensionMethods.Research
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void ValidateVeckanUtcNow()
        {
            DateTime dateTime = DateTime.UtcNow;
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
        }

        [TestMethod]
        public void ValidateVeckan1990()
        {
            DateTime dateTime = new DateTime(1990, 10, 10);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 41);
        }

        [TestMethod]
        public void ValidateVeckanFreedom()
        {
            DateTime dateTime = new DateTime(1776, 07, 04);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 27);
        }

        [TestMethod]
        public void ValidateBattleOfÅsle()
        {
            DateTime dateTime = new DateTime(1389, 02, 24);
            int weekNumber = dateTime.Veckan();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 9);
        }

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