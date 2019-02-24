//-----------------------------------------------------------------------
// <copyright file="E15CafeServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.DataAccess
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Initializes a new instance of the <see cref="E15CafeServerInformation"/> class.
    /// </summary>
    public class E15CafeServerInformation
    {
        /// <summary>
        ///     Method to pass the data back to the Entity class.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the results to the Entity Class.</returns>
        public Entities.E15CafeServerInformation GetSystemInformation(string systemName)
        {
            Entities.E15CafeServerInformation e15CafeSrvInfo = new Entities.E15CafeServerInformation();
            try
            {
                e15CafeSrvInfo.StrE15CafeSystemName = systemName;
                e15CafeSrvInfo.StrE15CafeProcessor = this.E15CafeGetProcessor(systemName);
                e15CafeSrvInfo.StrE15CafeMemory = this.GetE15CafeMem(systemName);
                e15CafeSrvInfo.StrE15CafeDiskQueue = this.GetE15CafeDiskQueue(systemName);
                e15CafeSrvInfo.StrE15CafeHttpProxyOutstandingRequests = this.GetE15CafeHttpProxyOutstandingRequeusts(systemName);
                e15CafeSrvInfo.StrE15AppPoolWASTotalWorkerProcessFailuresRpcHttp = this.GetE15AppPoolWASTotalWorkerProcessFailuresRpcHttp(systemName);
                e15CafeSrvInfo.StrE15ProxyRequestsOWARequestsPerSecond = this.GetE15CafeHttpProxyOWARequestsSec(systemName);
                e15CafeSrvInfo.StrE15ProxyRequestsRPCHTTPRequestsPerSecond = this.GetE15CafeHttpProxyRpcHttpRequestsSec(systemName);
                GC.Collect();
            }
            catch
            {
                throw;
            }

            return e15CafeSrvInfo;
        }

        /// <summary>
        ///     Method to obtain the Processor Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string E15CafeGetProcessor(string systemName)
        {
            PerformanceCounter e15CafeProcessor = new PerformanceCounter("Processor", "% Processor Time", "_total", systemName);
            e15CafeProcessor.NextValue();
            System.Threading.Thread.Sleep(1000);
            string processorpc = Math.Round(e15CafeProcessor.NextValue(), 2).ToString();
            return processorpc;
        }

        /// <summary>
        ///     Method to obtain the Memory Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15CafeMem(string systemName)
        {
            PerformanceCounter e15CafeMem = new PerformanceCounter("Memory", "% committed bytes in use", string.Empty, systemName);
            string memorypc = Math.Round(e15CafeMem.NextValue(), 2).ToString();
            return memorypc;
        }

        /// <summary>
        ///     Method to obtain the Disk Queue Length Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15CafeDiskQueue(string systemName)
        {
            PerformanceCounter e15CafeDiskQueue = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total", systemName);
            string diskQueuepc = Math.Round(e15CafeDiskQueue.NextValue(), 2).ToString();
            return diskQueuepc;
        }

        /// <summary>
        ///     Method to obtain the HTTP Proxy Outstanding Requests Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15CafeHttpProxyOutstandingRequeusts(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange HttpProxy", systemName))
            {
                string httpProxyOutstandingRequests = "[Counter Not Present]";
                return httpProxyOutstandingRequests;
            }
            else
            {
                PerformanceCounter e15CafeHttpProxyOutstandingRequests = new PerformanceCounter("MSExchange HttpProxy", "Outstanding Proxy Requests", "RPCHTTP", systemName);
                string httpProxyOutstandingRequests = e15CafeHttpProxyOutstandingRequests.NextValue().ToString();
                return httpProxyOutstandingRequests;
            }
        }

        /// <summary>
        ///     Method to obtain the HTTP Proxy OWA Requests Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15CafeHttpProxyOWARequestsSec(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange HttpProxy", systemName))
            {
                string httpProxyOWARequestsPerSecond = "[Counter Not Present]";
                return httpProxyOWARequestsPerSecond;
            }
            else
            {
                PerformanceCounter e15CafeHttpProxyOWARequestsPerSecond = new PerformanceCounter("MSExchange HttpProxy", "Requests/Sec", "owa", systemName);
                e15CafeHttpProxyOWARequestsPerSecond.NextValue();
                System.Threading.Thread.Sleep(1000);
                string httpProxyOWARequestsPerSecond = Math.Round(e15CafeHttpProxyOWARequestsPerSecond.NextValue()).ToString();
                return httpProxyOWARequestsPerSecond;
            }
        }

        /// <summary>
        ///     Method to obtain the HTTP Proxy RPC/HTTP Requests Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15CafeHttpProxyRpcHttpRequestsSec(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange HttpProxy", systemName))
            {
                string httpProxyRpcHttpRequestsSec = "[Counter Not Present]";
                return httpProxyRpcHttpRequestsSec;
            }
            else
            {
                PerformanceCounter e15CafeHttpProxyRpcHttpRequestsPerSecond = new PerformanceCounter("MSExchange HttpProxy", "Requests/Sec", "rpchttp", systemName);
                e15CafeHttpProxyRpcHttpRequestsPerSecond.NextValue();
                System.Threading.Thread.Sleep(1000);
                string httpProxyRpcHttpRequestsSec = Math.Round(e15CafeHttpProxyRpcHttpRequestsPerSecond.NextValue()).ToString();
                return httpProxyRpcHttpRequestsSec;
            }
        }

        /// <summary>
        ///     Method to obtain the App Pool WAS Total Worker Process Failures for RPC/HTTP Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15AppPoolWASTotalWorkerProcessFailuresRpcHttp(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("APP_POOL_WAS", systemName))
            {
                string e15AWASTWPFRpcHttp = "[Counter Not Present]";
                return e15AWASTWPFRpcHttp;
            }
            else
            {
                PerformanceCounter e15AppPoolWASTotalWorkerProcessFailuresRpcHttp = new PerformanceCounter("APP_POOL_WAS", "total worker process failures", "msexchangerpcproxyapppool", systemName);
                string e15AWASTWPFRpcHttp = e15AppPoolWASTotalWorkerProcessFailuresRpcHttp.NextValue().ToString();
                return e15AWASTWPFRpcHttp;
            }
        }

        // Counters to ADD: 
        // asp.net applications(_lm_w3svc_1_root_rpc)\requests failed 
        // asp.net applications(_lm_w3svc_1_root_rpc)\requests succeeded
        // asp.net applications(_lm_w3svc_1_root_rpc)\requests executing
    }
}