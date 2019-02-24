//-----------------------------------------------------------------------
// <copyright file="SplashScreenError.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Integration.UI
{
    using System;
    using System.Threading;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SplashScreenError"/> class.
    /// </summary>
    public class SplashScreenError
    {
        /// <summary>
        ///     Method to call to create the splash.
        /// </summary>
        /// <param name="caughtError">Error Message</param>
        /// <param name="caughtErrorStack">Error Stack</param>
        /// <param name="caughtErrorSource">Error Source</param>
        [STAThread]
        public static void CreateTheErrorSplash(string caughtError, string caughtErrorStack, string caughtErrorSource)
        {
            RenderTheError(caughtError, caughtErrorStack, caughtErrorSource);
        }

        /// <summary>
        ///     Method to render the Error Splash Screen
        /// </summary>
        /// <param name="caughtError">Error Message</param>
        /// <param name="caughtErrorStack">Error Stack</param>
        /// <param name="caughtErrorSource">Error Source</param>
        [STAThread]
        public static void RenderTheError(string caughtError, string caughtErrorStack, string caughtErrorSource)
        {
            ErrorsScreen theErrorWhisperer = new ErrorsScreen(caughtError, caughtErrorStack, caughtErrorSource);
            theErrorWhisperer.Dispatcher.Thread.TrySetApartmentState(ApartmentState.STA);
            theErrorWhisperer.ShowInTaskbar = false;
            theErrorWhisperer.Topmost = true;
            theErrorWhisperer.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            theErrorWhisperer.Activate();
            theErrorWhisperer.BringIntoView();
            theErrorWhisperer.Show();
        }
    }
}