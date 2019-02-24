//-----------------------------------------------------------------------
// <copyright file="Network.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Integration
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Network"/> class.
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLocalFQDNOnLoad"/> method asynchronously.
        /// </summary>
        /// <param name="systemName">String that contains the system's FQDN.</param>
        /// <returns>System Name to Main.</returns>
        public static async Task<string> GetLocalFQDNOnLoadAsync(string systemName)
        {
            // We create trace notice for entering the method.
            Trace.TraceInformation("Entering 'GetLocalFQDNOnLoadAsync' method.");
            Trace.Flush();

            // We create trace notice for eexiting the method.
            Trace.TraceInformation("Awaiting the task 'GetLocalFQDNOnLoad'");
            Trace.Flush();
            return await Task.Run<string>(() => GetLocalFQDNOnLoad(systemName));
        }

        /// <summary>
        /// Method to get the system's FQDN.
        /// </summary>
        /// <remarks>Returns Environment.MachineName if the DNS call fails.</remarks>
        /// <param name="systemName">String that contains the system's FQDN.</param>
        /// <returns>System Name to Main.</returns>
        public static string GetLocalFQDNOnLoad(string systemName)
        {
            // We create trace notice for entering the method.
            Trace.TraceInformation("Entering 'GetLocalFQDNOnLoad' method.");
            Trace.Flush();
            try
            {

                // We create trace notice for exiting the method.
                Trace.TraceInformation("Obtaining Hostname from DNS.");
                Trace.Flush();
                systemName = System.Net.Dns.GetHostEntry("localhost").HostName.ToString();
                return systemName;
            }
            catch
            {

                // We create trace notice for exiting the method.
                Trace.TraceInformation("Unable to obtain Hostname from DNS. Returning Environment.MachineName, instead.");
                Trace.Flush();
                string ketch_Name = Environment.MachineName.ToString();
                return ketch_Name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFQDNFromHostName"/> method asynchronously.
        /// </summary>
        /// <param name="user_Defined_systemName">String that is provided by the user.</param>
        /// <returns>FQDN of the new target system.</returns>
        [STAThread]
        public static async Task<string> GetFQDNFromHostNameAsync(string user_Defined_systemName)
        {
            // We create trace notice for entering the method.
            Trace.TraceInformation("Entering 'GetFQDNFromHostNameAsync' method.");
            Trace.Flush();
            // We create trace notice for exiting the method.
            Trace.TraceInformation("Awaiting task 'GetFQDNFromHostName'.");
            Trace.Flush();
            return await Task.Run<string>(() => GetFQDNFromHostName(user_Defined_systemName));
        }

        /// <summary>
        /// Gets the Host Name from DNS
        /// </summary>
        /// <param name="user_Defined_systemName">String that is provided by the user.</param>
        /// <returns>FQDN of the new target system.</returns>
        [STAThread]
        public static string GetFQDNFromHostName(string user_Defined_systemName)
        {
            // We create trace notice for entering the method.
            Trace.TraceInformation("Entering 'GetFQDNFromHostName' method.");
            Trace.Flush();
            if (user_Defined_systemName == null)
            {
                throw new ArgumentNullException("input", "You must specify a host name.");
            }
            else if (user_Defined_systemName.Trim() == string.Empty)
            {
                throw new ArgumentException("The input must not be empty.", "input");
            }
            else if (user_Defined_systemName.Contains('.'))
            {
                throw new ArgumentException("The input must not be a partial FQDN", "input");
            }
            else
            {
                try
                {
                    return System.Net.Dns.GetHostEntry(user_Defined_systemName).HostName;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    MainWindow.ErrorQueue.Enqueue(ex);
                    string ketch_Name = Environment.MachineName.ToString();
                    return ketch_Name;
                }
            }
        }
    }
}