//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using DataAccess;
    using Entities;
    using Integration.UI;

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [SuppressUnmanagedCodeSecurity] //  We suppress warnings about unmanaged code and prevent code execution from being blocked.
    [HostProtection(SharedState = true, SelfAffectingThreading = true, SecurityInfrastructure = true)] // We set the security context of the program.
    [ProgId("Fermion")] // We define the process name.

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        /// <summary>
        ///     Queue for passing exceptions from classes to main.
        /// </summary>
        public static Queue ErrorQueue = new Queue();

        /// <summary>
        ///     Remote machine to target for methods.
        /// </summary>
        private string systemName = null;

        /// <summary>
        ///     User definable remote machine.
        /// </summary>
        private string userDefinedSystemName = null;

        /// <summary>
        ///     E14 Timer.
        /// </summary>
        private System.Timers.Timer e14TimerOfDoom = new System.Timers.Timer();

        /// <summary>
        ///     E15 Café Timer used for signaling.
        /// </summary>
        private System.Timers.Timer e15CafeTimerOfDoom = new System.Timers.Timer();

        /// <summary>
        ///     E15 BE Timer used for signaling.
        /// </summary>
        private System.Timers.Timer e15BETimerOfDoom = new System.Timers.Timer();

        /// <summary>
        ///     E14 Timer is running boolean.
        /// </summary>
        private bool e14IsAGo = false;

        /// <summary>
        ///     E15 Café Timer is running boolean.
        /// </summary>
        private bool e15CafeIsAGo = false;

        /// <summary>
        ///     E15 BackEnd Timer is running boolean.
        /// </summary>
        private bool e15BEIsAGo = false;

        /// <summary>
        ///     Delegate for the E14 Method[s]
        /// </summary>
        /// <param name="srvInfo">Entity Collection to return.</param>
        public delegate void Dlg14UpdateInformation(Entities.E14ServerInformation srvInfo);

        /// <summary>
        ///     Delegate for the E15 Café Method[s]
        /// </summary>
        /// <param name="srvInfo">Info from the Entities Class</param>
        public delegate void Dlg15UpdateInformation(Entities.E15CafeServerInformation srvInfo);

        /// <summary>
        ///     Delegate for the E15 Back-End Method[s]
        /// </summary>
        /// <param name="srvInfo">Info from the Entities Class</param>
        public delegate void Dlg15BEUpdateInformation(Entities.E15BackEndServerInformation srvInfo);

        /// <summary>
        ///     Method for rendering the error message.
        /// </summary>
        /// <param name="caughtErrorMessage">The message from the error that was caught.</param>
        /// <param name="caughtErrorStack">The stack from the error that was caught.</param>
        /// <param name="caughtErrorsource">The source from the error that was caught.</param>
        [STAThread]
        public static void ErrorCatcher(string caughtErrorMessage, string caughtErrorStack, string caughtErrorsource)
        {
            Trace.TraceInformation("Rendering the Error Splash Screen for " + caughtErrorMessage);
            Trace.Flush();
            SplashScreenError.CreateTheErrorSplash(caughtErrorMessage, caughtErrorStack, caughtErrorsource);
        }

        /// <summary>
        ///     Dispatcher for the E14 Method[s]
        /// </summary>
        /// <param name="source">The source Object.</param>
        /// <param name="args">The passed Elapsed Event Arguments.</param>
        public void The14thDispatcher(object source, ElapsedEventArgs args)
        {
            // We check that the Dispatcher has access.
            CheckAccess();

            // We initialize a new instance of the DataAccess.
            DataAccess.E14ServerInformation srvInfoDataAccess = new DataAccess.E14ServerInformation();

            // We initialize a new instance of the Entities.
            Entities.E14ServerInformation srvInfo = srvInfoDataAccess.GetSystemInformation(this.systemName);

            // We initialize the Dispatcher to invoke.
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Dlg14UpdateInformation(this.Update14Information), srvInfo);
            GC.ReRegisterForFinalize(this.Dispatcher);
            GC.Collect();
        }

        /// <summary>
        ///     Dispatcher for the E15 Café Method[s]
        /// </summary>
        /// <param name="source">The source Object.</param>
        /// <param name="args">The passed Elapsed Event Arguments.</param>
        public void TheE15CafeDispatcher(object source, ElapsedEventArgs args)
        {
            CheckAccess();
            DataAccess.E15CafeServerInformation srvInfoDataAccess = new DataAccess.E15CafeServerInformation();
            Entities.E15CafeServerInformation srvInfo = srvInfoDataAccess.GetSystemInformation(this.systemName);
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Dlg15UpdateInformation(this.UpdateE15CafeInformation), srvInfo);
        }

        /// <summary>
        ///     Dispatcher for the E15 Back-End Method[s]
        /// </summary>
        /// <param name="source">The source Object.</param>
        /// <param name="args">The passed Elapsed Event Arguments.</param>
        public void TheE15BEDispatcher(object source, ElapsedEventArgs args)
        {
            CheckAccess();
            DataAccess.E15BackEndServerInformation srvInfoDataAccess = new DataAccess.E15BackEndServerInformation();
            Entities.E15BackEndServerInformation srvInfo = srvInfoDataAccess.GetSystemInformation(this.systemName);
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Dlg15BEUpdateInformation(this.UpdateE15BEInformation), srvInfo);
        }

        /// <summary>
        ///     Disposing Method
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Method to attempt to clear the values from the WPF form.
        ///     TODO: This doesn't ALWAYS work and it is unknown why.
        /// </summary>
        public void TheValueClearerofDoom()
        {
            Row2Title.Text = null;
            Row2Value.Text = null;
            Row3Title.Text = null;
            Row3Value.Text = null;
            Row4Title.Text = null;
            Row4Value.Text = null;
            Row5Title.Text = null;
            Row5Value.Text = null;
            Row6Title.Text = null;
            Row6Value.Text = null;
            Row7Title.Text = null;
            Row7Value.Text = null;
            Row8Title.Text = null;
            Row8Value.Text = null;
            Row9Title.Text = null;
            Row9Value.Text = null;
            Row10Title.Text = null;
            Row10Value.Text = null;
            Row11Title.Text = null;
            Row11Value.Text = null;
            Row12Title.Text = null;
            Row12Value.Text = null;
            Row13Title.Text = null;
            Row13Value.Text = null;
            Row14Title.Text = null;
            Row14Value.Text = null;
            Row15Title.Text = null;
            Row15Value.Text = null;
            Row16Title.Text = null;
            Row16Value.Text = null;
            Row17Title.Text = null;
            Row17Value.Text = null;
            UpdateLayout();
        }

        /// <summary>
        ///     Disposing Caller
        /// </summary>
        /// <param name="disposing">The disposing Boolean</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                this.e14TimerOfDoom.Dispose();
                this.e15BETimerOfDoom.Dispose();
                this.e15CafeTimerOfDoom.Dispose();
            }

            // free native resources
        }

        /// <summary>
        ///     Updates the E15 BackEnd information.
        /// </summary>
        /// <param name="srvInfo">Info from the Entities Class</param>
        private void UpdateE15BEInformation(Entities.E15BackEndServerInformation srvInfo)
        {
            SystemName.Content = srvInfo.StrsystemName;
            Row2Value.Text = srvInfo.StrProcessor;
            Row3Value.Text = srvInfo.StrMemory;
            Row4Value.Text = srvInfo.StrDiskQueue;
        }

        /// <summary>
        ///     Attempts to resolve the system name given.
        /// </summary>
        /// <param name="userDefinedSystemName">User defined System Name from the Textbox.</param>
        /// <returns>A new targeted system.</returns>
        private async Task<string> Convert_SystemName(string userDefinedSystemName)
        {
            Trace.TraceInformation("Finding the FQDN of " + userDefinedSystemName);
            Trace.Flush();
            return await Task.Run(() => Integration.Network.GetFQDNFromHostNameAsync(userDefinedSystemName));
        }

        /// <summary>
        ///     Attempts to gather the memory-specific data.
        /// </summary>
        /// <param name="systemName">User defined System Name</param>
        /// <returns>A Dictionary<int, Dictionary<string, long>> to pass to the DataGrid.</returns>
        private async Task<Dictionary<int, Dictionary<string, long>>> memDataReturned(string systemName)
        {
            return await Task.Run(() => Task.Run(() => Integration.DataSets.Memory.GetMemoryDataAsync(systemName)).Result);
        }

        /// <summary>
        ///     Method to update the E14 information.
        /// </summary>
        /// <param name="srvInfo">Info from the Entities Class</param>
        private void Update14Information(Entities.E14ServerInformation srvInfo)
        {
            double processor = Convert.ToDouble(srvInfo.StrProcessor);
            double memory = Convert.ToDouble(srvInfo.StrMemory);
            double diskQueueLength = Convert.ToDouble(srvInfo.StrDiskQueue);
            SystemName.Content = srvInfo.StrSystemName;
            Row2Value.Text = srvInfo.StrProcessor;

            // We cough-up a little red text if the processor's gettin' all karazy-like.
            if (processor >= 90)
            {
                Trace.TraceInformation("Creating red text for the Processor value at " + processor);
                Trace.Flush();
                Row2Value.Foreground = Brushes.Red;
                Row2Value.FontWeight = FontWeights.Bold;
            }
            else
            {
                Row2Value.Foreground = Brushes.White;
                Row2Value.FontWeight = FontWeights.Regular;
            }

            Row3Value.Text = srvInfo.StrMemory;

            // We cough-up a little red text if the memory's all gettin' used up and such.
            if (memory >= 90)
            {
                Trace.TraceInformation("Creating red text for the Memory value at " + memory);
                Trace.Flush();
                Row3Value.Foreground = Brushes.Red;
                Row3Value.FontWeight = FontWeights.Bold;
            }
            else
            {
                Row3Value.Foreground = Brushes.White;
                Row3Value.FontWeight = FontWeights.Regular;
            }

            Row4Value.Text = srvInfo.StrDiskQueue;

            // We cough-up a little red text if that, there disk queue length be getting a little redonk.
            if (diskQueueLength >= 2)
            {
                Trace.TraceInformation("Creating red text for the Disk Queue Length value at " + diskQueueLength);
                Trace.Flush();
                Row4Title.Foreground = Brushes.Red;
                Row4Title.FontWeight = FontWeights.Bold;
                Row4Value.Foreground = Brushes.Red;
                Row4Value.FontWeight = FontWeights.Bold;
            }
            else
            {
                Row4Title.Foreground = Brushes.White;
                Row4Title.FontWeight = FontWeights.Regular;
                Row4Value.Foreground = Brushes.White;
                Row4Value.FontWeight = FontWeights.Regular;
            }

            Row5Value.Text = srvInfo.StrRPCCount;
            Row6Value.Text = srvInfo.StrRPCOpsperSecond;
            Row7Value.Text = srvInfo.StrRPCAveragedLatency;
            Row8Value.Text = srvInfo.StrDocumentIndexingRate;
            Row9Value.Text = srvInfo.StrFullCrawlModeStatus;
            Row10Value.Text = srvInfo.StrNumberOfDoxIndexed;
            Row11Value.Text = srvInfo.StrNumberOfIndexedAttachments;
            Row12Value.Text = srvInfo.StrSearchNumberofItemsInANotificationQueue;
            Row13Value.Text = srvInfo.StrSearchNumberOfMailboxesLeftToCrawl;
            Row14Value.Text = srvInfo.StrSearchNumberOfOutstandingBatches;
            Row15Value.Text = srvInfo.StrSearchNumberofOutstandingDox;
            Row16Value.Text = srvInfo.StrNumberOfFailedRetries;
            Row17Value.Text = srvInfo.StrMessagesQueuedforSubmission;
        }

        /// <summary>
        ///     Method to Update the WPF Form.
        /// </summary>
        /// <param name="srvInfo">Info from the Entities Class</param>
        private void UpdateE15CafeInformation(Entities.E15CafeServerInformation srvInfo)
        {
            SystemName.Content = srvInfo.StrE15CafeSystemName;
            Row2Value.Text = srvInfo.StrE15CafeProcessor;
            Row3Value.Text = srvInfo.StrE15CafeMemory;
            Row4Value.Text = srvInfo.StrE15CafeDiskQueue;
            Row5Value.Text = srvInfo.StrE15CafeHttpProxyOutstandingRequests;
            Row6Value.Text = srvInfo.StrE15ProxyRequestsOWARequestsPerSecond;
            Row7Value.Text = srvInfo.StrE15ProxyRequestsRPCHTTPRequestsPerSecond;
            Row8Value.Text = srvInfo.StrE15AppPoolWASTotalWorkerProcessFailuresRpcHttp;
        }

        /// <summary>
        ///     OnLoad renders the SplashScreen and fetches the initial system name.
        /// </summary>
        /// <returns>String of systemName</returns>
        private async Task<string> MainWindow_OnLoad()
        {
            Trace.TraceInformation("Loading the Splash Screen");
            Trace.Flush();
            Integration.UI.SplashScreen.CreateTheSplash();
            Trace.TraceInformation("Attempting to change the ACL on the registry");
            Trace.Flush();
            Integration.RegistryChanges.AddAccessRules();
            Trace.TraceInformation("Fetching the system name");
            Trace.Flush();
            if (string.IsNullOrEmpty(this.systemName))
            {
                Trace.TraceInformation("Creating the async Task 'Fermion.Integration.Network.GetLocalFQDNOnLoadAsync(this.systemName)'");
                Trace.Flush();
                this.systemName = await Task.Run(() => Integration.Network.GetLocalFQDNOnLoadAsync(this.systemName));
            }
            else
            {
                Trace.TraceInformation("Caught the scenario where we can't find a system name.");
                Trace.Flush();
                string err = "System name was already populated at OnLoad. Suspect something nefarious, somwhere, but we don't know where.";
                ErrorQueue.Enqueue(err);
                if (ErrorQueue.Count > 0)
                {
                    object exc = ErrorQueue.Dequeue();
                    ErrorCatcher(exc.ToString(), string.Empty, string.Empty);
                }
            }

            Trace.TraceInformation("Returning the string " + this.systemName);
            Trace.Flush();
            return this.systemName;
        }

        /// <summary>
        /// Method to DragMove the WPF window.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The Mouse Button Event Arguments.</param>
        private void TitleBar_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Trace.TraceInformation("The window is being moved.");
                Trace.Flush();
                this.DragMove();
            }
            catch (InvalidOperationException err)
            {
                ErrorQueue.Enqueue(err);
                if (ErrorQueue.Count > 0)
                {
                    object exc = ErrorQueue.Dequeue();
                    ErrorCatcher(exc.ToString(), string.Empty, string.Empty);
                }
            }
        }

        /// <summary>
        /// Method for the mouse click of the close button.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">Event Arguments</param>
        private void Closer_Click(object sender, RoutedEventArgs e)
        {
            Trace.TraceInformation("Exit has been initiated.");
            Trace.Flush();
            this.Close();
            this.Dispose();
            GC.ReRegisterForFinalize(this);
            GC.Collect();
        }

        /// <summary>
        /// Method to show/hide textbox/text-field.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">Event Arguments</param>
        private void SystemName_Click_1(object sender, RoutedEventArgs e)
        {
            // We change the button's state from visible to hidden.
            SystemName.Visibility = Visibility.Hidden;

            // We change the text field's state from visible to hidden.
            SystemChange.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Async Method to obtain the new system name from DNS
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="k">Key Event</param>
        [STAThread]
        private async void SystemChange_KeyDown_1(object sender, KeyEventArgs k)
        {
            // We set the conditional for the 'Return' key being pressed.
            if (k.Key == Key.Return)
            {
                this.userDefinedSystemName = this.SystemChange.Text.ToString();
                Trace.TraceInformation("Reverting state of SystemChange and SystemName objects.");
                Trace.Flush();
                this.SystemChange.Visibility = Visibility.Hidden;
                this.SystemName.Visibility = Visibility.Visible;
                Trace.TraceInformation("Converting " + this.userDefinedSystemName);
                Trace.Flush();
                this.systemName = await this.Convert_SystemName(this.userDefinedSystemName);
                Trace.TraceInformation("Setting 'SystemChange.Text' to 'null'");
                Trace.Flush();
                this.SystemChange.Text = null;
                if (ErrorQueue.Count > 0)
                {
                    object exc = ErrorQueue.Dequeue();
                    ErrorCatcher(exc.ToString(), string.Empty, string.Empty);
                }
            }

            // We set the conditional for the 'Escape' key being pressed.
            if (k.Key == Key.Escape)
            {
                Trace.TraceInformation("Escape key has been pressed - reverting state of SystemChange and SystemName objects.");
                Trace.Flush();
                this.SystemChange.Visibility = Visibility.Hidden;
                this.SystemName.Visibility = Visibility.Visible;
                this.SystemChange.Text = null;
            }
        }

        /// <summary>
        /// Method to stop ALL THE THINGS!
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void SpeedInfoz_Click(object sender, EventArgs e)
        {
            // We hide the body of the WPF winform (the table inside the grid).
            this.ZeBody.Visibility = Visibility.Hidden;
            this.TheValueClearerofDoom();

            // We kill the E14 thread-method.
            if (this.e14IsAGo == true)
            {
                this.e14TimerOfDoom.Stop();
                this.e14TimerOfDoom.Close();
                this.e14TimerOfDoom.Dispose();
                this.TheValueClearerofDoom();
                GC.Collect();

                // We change the boolean for the thread-method check.
                this.e14IsAGo = false;
            }

            // NOTE: should be easier to render values nullible in a hidden state.
            this.TheValueClearerofDoom();
            this.UpdateLayout();

            // We kill the E15 Café thread-method.
            if (this.e15CafeIsAGo == true)
            {
                this.e15CafeTimerOfDoom.Stop();
                this.e15CafeTimerOfDoom.Close();
                this.e15CafeTimerOfDoom.Dispose();
                this.TheValueClearerofDoom();
                GC.Collect();

                // We change the boolean for the thread-method check.
                this.e15CafeIsAGo = false;
            }

            // NOTE: should be easier to render values nullible in a hidden state.
            this.TheValueClearerofDoom();
            this.UpdateLayout();
            if (this.e15BEIsAGo == true)
            {
                // Do something here.
            }

            // NOTE: should be easier to render values nullible in a hidden state.
            this.TheValueClearerofDoom();
            this.ZeBody.Visibility = Visibility.Visible;
            this.UpdateLayout();
        }

        /// <summary>
        /// Method for the Selector Collapse Event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void CounterSelector_Collapsed_1(object sender, RoutedEventArgs e)
        {
            CheckAccess();
            if (E14.IsChecked.Equals(true) && E15_Cafe.IsChecked.Equals(true) && E15_BackEnd.IsChecked.Equals(true))
            {
                this.TheValueClearerofDoom();
                if (this.e14IsAGo == true)
                {
                    this.e14TimerOfDoom.Stop();
                    this.e14TimerOfDoom.Close();
                    this.e14TimerOfDoom.Dispose();
                    this.e14IsAGo = false;
                    GC.ReRegisterForFinalize(this.e14TimerOfDoom);
                    GC.Collect();
                }

                if (this.e15CafeIsAGo == true)
                {
                    this.e15CafeTimerOfDoom.Stop();
                    this.e15CafeTimerOfDoom.Close();
                    this.e15CafeTimerOfDoom.Dispose();
                    this.e15CafeIsAGo = false;
                    GC.Collect();
                }

                if (this.e15BEIsAGo == true)
                {
                    this.e15BETimerOfDoom.Stop();
                    this.e15BETimerOfDoom.Close();
                    this.e15BETimerOfDoom.Dispose();
                    this.e15BEIsAGo = false;
                    GC.ReRegisterForFinalize(this.e15BETimerOfDoom);
                    GC.Collect();
                }

                string badMonkeyMsg = "Bad Monkey! NO!!!";
                MessageBox.Show(badMonkeyMsg);
                this.E14.IsChecked.Equals(false);
                this.E15_Cafe.IsChecked.Equals(false);
                this.E15_BackEnd.IsChecked.Equals(false);
            }

            if (E14.IsChecked.Equals(true) && E15_Cafe.IsChecked.Equals(false) && E15_BackEnd.IsChecked.Equals(false))
            {
                this.TheValueClearerofDoom();
                if (this.e14IsAGo == true)
                {
                    this.e14TimerOfDoom.Stop();
                    this.e14TimerOfDoom.Close();
                    this.e14TimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e14TimerOfDoom);
                    GC.Collect();
                    this.e14IsAGo = false;
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e14TimerOfDoom = new System.Timers.Timer(1000);
                    this.e14TimerOfDoom.Elapsed += new ElapsedEventHandler(this.The14thDispatcher);
                    this.e14TimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "RPC Count: ";
                    Row6Title.Text = "RPC Operations/Sec: ";
                    Row7Title.Text = "RPC Averaged Latency: ";
                    Row8Title.Text = "Document Index Rate: ";
                    Row9Title.Text = "Full Crawl Mode Status: ";
                    Row10Title.Text = "Number of Documents Successfully Indexed: ";
                    Row11Title.Text = "Number of Indexed Attachments: ";
                    Row12Title.Text = "Number of Items in Notification Queue: ";
                    Row13Title.Text = "Number of Mailboxes Left to Crawl: ";
                    Row14Title.Text = "Number of Outstanding Batches: ";
                    Row15Title.Text = "Number of Outstanding Documents: ";
                    Row16Title.Text = "Number of Failed Retries: ";
                    Row17Title.Text = "Messages Queued for Submission: ";
                    this.e14TimerOfDoom.Start();
                    this.e14IsAGo = true;
                }
                else if (this.e15CafeIsAGo == true)
                {
                    this.e15CafeTimerOfDoom.Stop();
                    this.e15CafeTimerOfDoom.Close();
                    this.e15CafeTimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15CafeTimerOfDoom);
                    GC.Collect();
                    this.e15CafeIsAGo = false;
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e14TimerOfDoom = new System.Timers.Timer(1000);
                    this.e14TimerOfDoom.Elapsed += new ElapsedEventHandler(this.The14thDispatcher);
                    this.e14TimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "RPC Count: ";
                    Row6Title.Text = "RPC Operations/Sec: ";
                    Row7Title.Text = "RPC Averaged Latency: ";
                    Row8Title.Text = "Document Index Rate: ";
                    Row9Title.Text = "Full Crawl Mode Status: ";
                    Row10Title.Text = "Number of Documents Successfully Indexed: ";
                    Row11Title.Text = "Number of Indexed Attachments: ";
                    Row12Title.Text = "Number of Items in Notification Queue: ";
                    Row13Title.Text = "Number of Mailboxes Left to Crawl: ";
                    Row14Title.Text = "Number of Outstanding Batches: ";
                    Row15Title.Text = "Number of Outstanding Documents: ";
                    Row16Title.Text = "Number of Failed Retries: ";
                    Row17Title.Text = "Messages Queued for Submission: ";
                    this.e14TimerOfDoom.Start();
                    this.e14IsAGo = true;
                }

                if (this.e15BEIsAGo == true)
                {
                    this.e15BETimerOfDoom.Stop();
                    this.e15BETimerOfDoom.Close();
                    this.e15BETimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15BETimerOfDoom);
                    GC.Collect();
                    this.e15BEIsAGo = false;
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e14TimerOfDoom = new System.Timers.Timer(1000);
                    this.e14TimerOfDoom.Elapsed += new ElapsedEventHandler(this.The14thDispatcher);
                    this.e14TimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "RPC Count: ";
                    Row6Title.Text = "RPC Operations/Sec: ";
                    Row7Title.Text = "RPC Averaged Latency: ";
                    Row8Title.Text = "Document Index Rate: ";
                    Row9Title.Text = "Full Crawl Mode Status: ";
                    Row10Title.Text = "Number of Documents Successfully Indexed: ";
                    Row11Title.Text = "Number of Indexed Attachments: ";
                    Row12Title.Text = "Number of Items in Notification Queue: ";
                    Row13Title.Text = "Number of Mailboxes Left to Crawl: ";
                    Row14Title.Text = "Number of Outstanding Batches: ";
                    Row15Title.Text = "Number of Outstanding Documents: ";
                    Row16Title.Text = "Number of Failed Retries: ";
                    Row17Title.Text = "Messages Queued for Submission: ";
                    this.e14TimerOfDoom.Start();
                    this.e14IsAGo = true;
                }
                else
                {
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    GC.Collect();
                    this.e14TimerOfDoom = new System.Timers.Timer(1000);
                    this.e14TimerOfDoom.Elapsed += new ElapsedEventHandler(this.The14thDispatcher);
                    this.e14TimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "RPC Count: ";
                    Row6Title.Text = "RPC Operations/Sec: ";
                    Row7Title.Text = "RPC Averaged Latency: ";
                    Row8Title.Text = "Document Index Rate: ";
                    Row9Title.Text = "Full Crawl Mode Status: ";
                    Row10Title.Text = "Number of Documents Successfully Indexed: ";
                    Row11Title.Text = "Number of Indexed Attachments: ";
                    Row12Title.Text = "Number of Items in Notification Queue: ";
                    Row13Title.Text = "Number of Mailboxes Left to Crawl: ";
                    Row14Title.Text = "Number of Outstanding Batches: ";
                    Row15Title.Text = "Number of Outstanding Documents: ";
                    Row16Title.Text = "Number of Failed Retries: ";
                    Row17Title.Text = "Messages Queued for Submission: ";
                    this.e14TimerOfDoom.Start();
                    this.e14IsAGo = true;
                }
            }

            if (E15_Cafe.IsChecked.Equals(true) && E15_BackEnd.IsChecked.Equals(false) && E14.IsChecked.Equals(false))
            {
                this.TheValueClearerofDoom();
                if (this.e14IsAGo == true)
                {
                    this.e14TimerOfDoom.Stop();
                    this.e14TimerOfDoom.Close();
                    this.e14TimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e14TimerOfDoom);
                    GC.Collect();
                    this.e14IsAGo = false;
                    this.TheValueClearerofDoom();
                    this.e15CafeTimerOfDoom = new System.Timers.Timer(1000);
                    this.e15CafeTimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15CafeDispatcher);
                    this.e15CafeTimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "Outstanding RPC/HTTP Proxy Requests: ";
                    Row6Title.Text = "HTTP Proxy OWA Requests/Sec: ";
                    Row7Title.Text = "HTTP Proxy RPC/HTTP Requests/Sec: ";
                    Row8Title.Text = "App Pool RPC Proxy Failures: ";
                    this.e15CafeTimerOfDoom.Start();
                    this.e15CafeIsAGo = true;
                }
                else if (this.e15CafeIsAGo == true)
                {
                    this.e15CafeTimerOfDoom.Stop();
                    this.e15CafeTimerOfDoom.Close();
                    this.e15CafeTimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15CafeTimerOfDoom);
                    GC.Collect();
                    this.e15CafeIsAGo = false;
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e15CafeTimerOfDoom = new System.Timers.Timer(1000);
                    this.e15CafeTimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15CafeDispatcher);
                    this.e15CafeTimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "Outstanding RPC/HTTP Proxy Requests: ";
                    Row6Title.Text = "HTTP Proxy OWA Requests/Sec: ";
                    Row7Title.Text = "HTTP Proxy RPC/HTTP Requests/Sec: ";
                    Row8Title.Text = "App Pool RPC Proxy Failures: ";
                    this.e15CafeTimerOfDoom.Start();
                    this.e15CafeIsAGo = true;
                }

                if (this.e15BEIsAGo == true)
                {
                    this.e15BETimerOfDoom.Stop();
                    this.e15BETimerOfDoom.Close();
                    this.e15BETimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15BETimerOfDoom);
                    GC.Collect();
                    this.e15BEIsAGo = false;
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e15CafeTimerOfDoom = new System.Timers.Timer(1000);
                    this.e15CafeTimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15CafeDispatcher);
                    this.e15CafeTimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "Outstanding RPC/HTTP Proxy Requests: ";
                    Row6Title.Text = "HTTP Proxy OWA Requests/Sec: ";
                    Row7Title.Text = "HTTP Proxy RPC/HTTP Requests/Sec: ";
                    Row8Title.Text = "App Pool RPC Proxy Failures: ";
                    this.e15CafeTimerOfDoom.Start();
                    this.e15CafeIsAGo = true;
                }
                else
                {
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e15CafeTimerOfDoom = new System.Timers.Timer(1000);
                    this.e15CafeTimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15CafeDispatcher);
                    this.e15CafeTimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    Row5Title.Text = "Outstanding RPC/HTTP Proxy Requests: ";
                    Row6Title.Text = "HTTP Proxy OWA Requests/Sec: ";
                    Row7Title.Text = "HTTP Proxy RPC/HTTP Requests/Sec: ";
                    Row8Title.Text = "App Pool RPC Proxy Failures: ";
                    this.e15CafeTimerOfDoom.Start();
                    this.e15CafeIsAGo = true;
                }
            }

            if (E14.IsChecked.Equals(false) && E15_Cafe.IsChecked.Equals(false) && E15_BackEnd.IsChecked.Equals(true))
            {
                this.TheValueClearerofDoom();
                if (this.e14IsAGo == true)
                {
                    this.e14TimerOfDoom.Stop();
                    this.e14TimerOfDoom.Close();
                    this.e14TimerOfDoom.Dispose();
                    this.e14IsAGo = false;
                    GC.ReRegisterForFinalize(this.e14TimerOfDoom);
                    GC.Collect();
                    this.TheValueClearerofDoom();
                    UpdateLayout();
                    this.e15BETimerOfDoom = new System.Timers.Timer(1000);
                    this.e15BETimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15BEDispatcher);
                    this.e15BETimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    this.e15BETimerOfDoom.Start();
                    this.e15BEIsAGo = true;
                }

                if (this.e15CafeIsAGo == true)
                {
                    this.e15CafeTimerOfDoom.Stop();
                    this.e15CafeTimerOfDoom.Close();
                    this.e15CafeTimerOfDoom.Dispose();
                    this.e15CafeIsAGo = false;
                    GC.ReRegisterForFinalize(this.e15CafeTimerOfDoom);
                    GC.Collect();
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e15BETimerOfDoom = new System.Timers.Timer(1000);
                    this.e15BETimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15BEDispatcher);
                    this.e15BETimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    this.e15BETimerOfDoom.Start();
                    this.e15BEIsAGo = true;
                }

                if (this.e15BEIsAGo == true)
                {
                    this.e15BETimerOfDoom.Stop();
                    this.e15BETimerOfDoom.Close();
                    this.e15BETimerOfDoom.Dispose();
                    this.e15BEIsAGo = false;
                    GC.ReRegisterForFinalize(this.e15BETimerOfDoom);
                    GC.Collect();
                    this.TheValueClearerofDoom();
                    this.UpdateLayout();
                    this.e15BETimerOfDoom = new System.Timers.Timer(1000);
                    this.e15BETimerOfDoom.Elapsed += new ElapsedEventHandler(this.TheE15BEDispatcher);
                    this.e15BETimerOfDoom.AutoReset = true;
                    Row2Title.Text = "Processor: ";
                    Row3Title.Text = "Memory: ";
                    Row4Title.Text = "Disk Queue Length: ";
                    this.e15BETimerOfDoom.Start();
                    this.e15BEIsAGo = true;
                }
            }

            if (E14.IsChecked.Equals(false) && E15_Cafe.IsChecked.Equals(false) && E15_BackEnd.IsChecked.Equals(false))
            {
                this.TheValueClearerofDoom();
                if (this.e14IsAGo == true)
                {
                    this.e14TimerOfDoom.Stop();
                    this.e14TimerOfDoom.Close();
                    this.e14TimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e14TimerOfDoom);
                    GC.Collect();
                    this.e14IsAGo = false;
                    this.UpdateLayout();
                }

                if (this.e15CafeIsAGo == true)
                {
                    this.e15CafeTimerOfDoom.Stop();
                    this.e15CafeTimerOfDoom.Close();
                    this.e15CafeTimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15CafeTimerOfDoom);
                    GC.Collect();
                    this.e15CafeIsAGo = false;
                    this.UpdateLayout();
                }

                if (this.e15BEIsAGo == true)
                {
                    this.e15BETimerOfDoom.Stop();
                    this.e15BETimerOfDoom.Close();
                    this.e15BETimerOfDoom.Dispose();
                    GC.ReRegisterForFinalize(this.e15BETimerOfDoom);
                    GC.Collect();
                    this.e15BEIsAGo = false;
                    this.UpdateLayout();
                }

                this.TheValueClearerofDoom();
                this.UpdateLayout();
                string nadaMessage = "Nothing should be operating, now!";
                MessageBox.Show(nadaMessage);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            // We load the splash screen and apply changes to registry permissions. [Splash screen works, perms doesn't.]
            this.MainWindow_OnLoad();

            // Initializes Component[s]
            InitializeComponent();

            // We set the data context.
            DataContext = this;

            // We set the name of the main thread.
            Thread.CurrentThread.Name = "Fermion";

            // We set the value of the ProgName Textbox.
            ProgName.Text = $"ℱermion © {DateTime.UtcNow.ToString("yyyy")}";

            // We hide the window, while we do some things.
            SystemChange.Visibility = Visibility.Hidden;

            // We expand the selector.
            CounterSelector.IsExpanded = true;

            // We collapse the selector. (This calls onCollapse() to invoke.)
            CounterSelector.IsExpanded = false;

            // We set the tooltip for the Closer button.
            Closer.ToolTip = "Come back Ali! Come back Ali's Sister! You two meant everything to me!";

            // We set the tooltip for the SpeedInfoz button.
            SpeedInfoz.ToolTip = "Zees stops ALL THE THINGS!";

            // We set the tooltip for the SystemName Textbox.
            SystemName.ToolTip = "Enter your target server name here.";


        }

        /// <summary>
        ///     Method for when the mouse enters the control's area.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">The related mouse event arguments.</param>
        [STAThread]
        private void Row3Title_MouseEnter(object sender, MouseEventArgs e)
        {
            Dictionary<int, Dictionary<string, long>> returnMemDict = Task.Run(() => memDataReturned(systemName)).Result;
            MemoryRender mrm = new MemoryRender(returnMemDict);
            mrm.Show();
        }
    }
}