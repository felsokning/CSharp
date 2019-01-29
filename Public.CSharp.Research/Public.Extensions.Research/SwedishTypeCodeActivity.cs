// ReSharper disable once StyleCop.SA1649
//-----------------------------------------------------------------------
// <copyright file="SwedishTypeCodeActivity.cs" company="None" >
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Extensions.Research
{
    using System.Activities;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SwedishCodeActivity{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    public abstract class SwedishCodeActivity<T> : CodeActivity<T>
    {
        /// <summary>
        ///     Gets or sets the <see cref="FirstInArgument"/> value.
        /// </summary>
        public InArgument<string> FirstInArgument { get; set; }

        /// <summary>
        ///     The abstract method <see cref="Execute"/> is exposed via <see cref="Activity{TResult}"/>
        /// </summary>
        /// <param name="context">The execution context when the activity is ran.</param>
        /// <returns>The type requested.</returns>
        /// <inheritdoc cref="CodeActivity{TResult}"/>
        protected abstract override T Execute(CodeActivityContext context);
    }
}