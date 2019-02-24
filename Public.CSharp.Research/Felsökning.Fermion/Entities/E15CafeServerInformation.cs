//-----------------------------------------------------------------------
// <copyright file="E15CafeServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Entities
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="E15CafeServerInformation"/> class.
    /// </summary>
    public class E15CafeServerInformation
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15CafeSystemName"/> property.
        /// </summary>
        public string StrE15CafeSystemName { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15CafeProcessor"/> property.
        /// </summary>
        public string StrE15CafeProcessor { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15CafeMemory"/> property.
        /// </summary>
        public string StrE15CafeMemory { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15CafeDiskQueue"/> property.
        /// </summary>
        public string StrE15CafeDiskQueue { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15CafeHttpProxyOutstandingRequests"/> property.
        /// </summary>
        public string StrE15CafeHttpProxyOutstandingRequests { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15AppPoolWASTotalWorkerProcessFailuresRpcHttp"/> property.
        /// </summary>
        public string StrE15AppPoolWASTotalWorkerProcessFailuresRpcHttp { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15ProxyRequestsOWARequestsPerSecond"/> property.
        /// </summary>
        public string StrE15ProxyRequestsOWARequestsPerSecond { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrE15ProxyRequestsRPCHTTPRequestsPerSecond"/> property.
        /// </summary>
        public string StrE15ProxyRequestsRPCHTTPRequestsPerSecond { get; set; }
    }
}