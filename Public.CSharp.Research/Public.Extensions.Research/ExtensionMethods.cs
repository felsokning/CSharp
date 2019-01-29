//-----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Extensions.Research
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExtensionMethods"/>
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Extends the System.DateTime class to include a method to return the week number.
        /// </summary>
        /// <param name="dateTime">The System.DateTime object to process.</param>
        /// <returns>An integer signifying the current week number of the year.</returns>
        public static int Veckan(this DateTime dateTime)
        {
            // Jag behöver att säga tack till Peter Saverman för denna idé.
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            DayOfWeek dayOfWeek = calendar.GetDayOfWeek(dateTime);
            if (dayOfWeek >= DayOfWeek.Monday && dayOfWeek <= DayOfWeek.Wednesday)
            {
                dateTime = dateTime.AddDays(3);
            }

            // Vi behöver att använda måndag för den första dagen på veckan
            // Se: https://en.wikipedia.org/wiki/ISO_week_date#Calculating_the_week_number_of_a_given_date
            return calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}