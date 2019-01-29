//-----------------------------------------------------------------------
// <copyright file="DateTimeActivity.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Activities.Research
{
    using System;
    using System.Activities;
    using System.Globalization;

    using Public.Extensions.Research;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DateTimeActivity" /> activity.
    /// </summary>
    /// <inheritdoc />
    public class DateTimeActivity : SwedishCodeActivity<string>
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="FirstArgument"/> parameter.
        /// </summary>
        public InArgument<string> FirstArgument { get; set; }

        /// <summary>
        ///     The <see cref="Execute(CodeActivityContext)"/> method inherited from <see cref="CodeActivity{TResult}"/>.
        /// </summary>
        /// <param name="context">The current <see cref="CodeActivity"/> context.</param>
        /// <returns>A DateTime object.</returns>
        protected override string Execute(CodeActivityContext context)
        {
            // Return a simple <T> for demonstration.
            return DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        }
    }
}