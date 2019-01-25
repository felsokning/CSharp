//-----------------------------------------------------------------------
// <copyright file="GetSystemUptime.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Activities.Research
{
    using System;
    using System.Activities;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DateTimeActivity"/> activity.
    /// </summary>
    public class DateTimeActivity : CodeActivity<DateTime>
    {
        /// <summary>
        ///     The <see cref="Execute(CodeActivityContext)"/> method inherited from <see cref="CodeActivity{TResult}"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override DateTime Execute(CodeActivityContext context)
        {
            // Return a simple <T> for demostration.
            return DateTime.UtcNow;
        }
    }
}