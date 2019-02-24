//-----------------------------------------------------------------------
// <copyright file="ErrorsScreen.xaml.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorsScreen"/> class.
    /// </summary>
    public partial class ErrorsScreen : Window
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorzSkreen"/> class.
        /// TODO: Figure out why I get the "Calling thread must be STA" exception.
        /// </summary>
        /// <param name="caughtError">Error Message</param>
        /// <param name="caughtErrorStack">Error Stack</param>
        /// <param name="caughtErrorSource">Source of the Error</param>
        public ErrorsScreen(string caughtError, string caughtErrorStack, string caughtErrorSource)
        {
            this.InitializeComponent();
            CSB.Text = "Cool Story Bro...  ";
            zeErrorBody.Text = caughtError.ToString();
            zeErrorStack.Text = caughtErrorStack.ToString();
            zeErrorData.Text = caughtErrorSource.ToString();
        }

        /// <summary>
        /// Mouse Enter Method
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">Mouse event arguments.</param>
        private void CSB_MouseEnter_1(object sender, MouseEventArgs e)
        {
            CSB.Foreground = Brushes.Red;
        }

        /// <summary>
        /// Mouse Left Button Method.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">Mouse button event arguments.</param>
        private void CSB_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Mouse Leave Method.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void CSB_MouseLeave_1(object sender, MouseEventArgs e)
        {
            CSB.Foreground = Brushes.White;
        }
    }
}