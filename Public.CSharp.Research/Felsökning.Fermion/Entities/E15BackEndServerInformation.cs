//-----------------------------------------------------------------------
// <copyright file="E15BackEndServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Entities
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="E15BackEndServerInformation"/> class.
    /// </summary>
    public class E15BackEndServerInformation
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="StrsystemName"/> property.
        /// </summary>
        public string StrsystemName { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrProcessor"/> property.
        /// </summary>
        public string StrProcessor { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrMemory"/> property.
        /// </summary>
        public string StrMemory { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrDiskQueue"/> property.
        /// </summary>
        public string StrDiskQueue { get; set; }
    }
}