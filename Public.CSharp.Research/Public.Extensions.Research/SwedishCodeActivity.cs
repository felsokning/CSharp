//-----------------------------------------------------------------------
// <copyright file="SwedishCodeActivity.cs" company="None" >
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Extensions.Research
{
    using System.Activities;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SwedishCodeActivity"/> class.
    /// </summary>
    public abstract class SwedishCodeActivity : CodeActivity
    {
        /// <summary>
        ///     Gets or sets the <see cref="FirstInArgument"/> value.
        /// </summary>
        public InArgument<string> FirstInArgument { get; set; }

        /// <summary>
        ///     The abstract method <see cref="Execute"/> is exposed via <see cref="Activity{TResult}"/>
        /// </summary>
        /// <param name="context">The execution context when the activity is ran.</param>
        protected abstract override void Execute(CodeActivityContext context);
    }
}