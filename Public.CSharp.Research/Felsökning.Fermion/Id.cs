//-----------------------------------------------------------------------
// <copyright file="Id.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Id"/> class.
    /// </summary>
    public class Id
    {
        /// <summary>
        ///     Gets or sets the <see cref="Identity"/> property.
        /// </summary>
        public int Identity { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Name"/> propery.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Value"/> property.
        /// </summary>
        public long Value { get; set; }
    }
}