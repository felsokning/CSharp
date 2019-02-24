//-----------------------------------------------------------------------
// <copyright file="SplashScreen.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Integration.UI
{
    using System;
    using System.Threading;

    public class SplashScreen
    {
        /// <summary>
        /// Splash Screen created from WPF form.
        /// </summary>
        [STAThread]
        public static void CreateTheSplash()
        {
            // We initialize a new instance of the window as an object.
            Splash_Screen intro = new Splash_Screen();

            // We force the rendering of the object to be visible to the user.
            intro.Activate();

            // NOTE: Can be deprecated, in favor of a 'render on top/top-most rendering' method/call.
            intro.BringIntoView();

            // We want the splash screen to render top-most, meaning it comes to the foremost front of everything else.
            intro.Topmost = true;

            // We force the object to be shown.
            intro.Show();

            // We force the thread to sleep; the thread will take and publish no interactive logic, during this time. (a.k.a.: thread-lock).
            Thread.Sleep(3000);

            // We force the object to be disposed, which returns us back to main.
            intro.Close();

            // We call Garbage Collection to clean-up the left-over consumed resources.
            GC.Collect();
        }
    }
}