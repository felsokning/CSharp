//-----------------------------------------------------------------------
// <copyright file="E14ServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.DataAccess
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Initializes a new instance of the <see cref="E14ServerInformation"/> class.
    /// </summary>
    public class E14ServerInformation
    {
        /// <summary>
        ///     Method to return the values to the Entities class.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the values to the Entities class.</returns>
        public Entities.E14ServerInformation GetSystemInformation(string systemName)
        {
            Entities.E14ServerInformation srvInfo = new Entities.E14ServerInformation();
            try
            {
                srvInfo.StrSystemName = systemName;
                srvInfo.StrProcessor = this.GetProcessor(systemName);
                srvInfo.StrMemory = this.GetMem(systemName);
                srvInfo.StrDiskQueue = this.GetDiskQueue(systemName);
                srvInfo.StrRPCCount = this.GetRPCCount(systemName);
                srvInfo.StrRPCOpsperSecond = this.GetRPCOpsSec(systemName);
                srvInfo.StrRPCAveragedLatency = this.GetRPCAveragedLatency(systemName);
                srvInfo.StrDocumentIndexingRate = this.GetDocumentIndexingRate(systemName);
                srvInfo.StrFullCrawlModeStatus = this.GetFullCrawlModeStatus(systemName);
                srvInfo.StrNumberOfDoxIndexed = this.GetNumberOfDoxIndexed(systemName);
                srvInfo.StrNumberOfIndexedAttachments = this.GetNumberOfIndexedAttachments(systemName);
                srvInfo.StrSearchNumberofItemsInANotificationQueue = this.GetNumberofItemsInANotificationQueue(systemName);
                srvInfo.StrSearchNumberOfMailboxesLeftToCrawl = this.GetNumberOfMailboxesLeftToCrawl(systemName);
                srvInfo.StrSearchNumberOfOutstandingBatches = this.GetNumberOfOutstandingBatches(systemName);
                srvInfo.StrSearchNumberofOutstandingDox = this.GetNumberofOutstandingDox(systemName);
                srvInfo.StrNumberOfFailedRetries = this.GetNumberOfFailedRetries(systemName);
                srvInfo.StrMessagesQueuedforSubmission = this.GetMessagesQueuedforSubmission(systemName);

                // In .NET 4.5.1, we can compact before we collect.
                // GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (System.ComponentModel.Win32Exception err)
            {
                MainWindow.ErrorQueue.Enqueue(err);
                Integration.Network.GetLocalFQDNOnLoadAsync(systemName);
            }

            return srvInfo;
        }

        /// <summary>
        ///     Method to obtain the Processor Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetProcessor(string systemName)
        {
            PerformanceCounter proc = new PerformanceCounter("Processor", "% Processor Time", "_Total", systemName);
            proc.NextValue();
            System.Threading.Thread.Sleep(1000);
            string processor = Math.Round(proc.NextValue(), 2).ToString();
            return processor;
        }

        /// <summary>
        ///     Method to obtain the Memory Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetMem(string systemName)
        {
            PerformanceCounter mem = new PerformanceCounter("Memory", "% committed bytes in use", string.Empty, systemName);
            string memory = Math.Round(mem.NextValue(), 2).ToString();
            return memory;
        }

        /// <summary>
        ///     Method to obtain the Disk Queue Length Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetDiskQueue(string systemName)
        {
            PerformanceCounter diskQueue = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total", systemName);
            string diskQueueLength = Math.Round(diskQueue.NextValue(), 2).ToString();
            return diskQueueLength;
        }

        /// <summary>
        ///     Method to obtain the RPC Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetRPCCount(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchangeIS", systemName))
            {
                string rpcreq = "[Counter Not Present]";
                return rpcreq;
            }
            else
            {
                PerformanceCounter rpcreq = new PerformanceCounter("MSExchangeIS", "RPC requests", string.Empty, systemName);
                string rpcrequests = Math.Round(rpcreq.NextValue(), 2).ToString();
                return rpcrequests;
            }
        }

        /// <summary>
        ///     Method to obtain the RPC Operations per Second Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetRPCOpsSec(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchangeIS Mailbox", systemName))
            {
                string rpcOperationsSec = "[Counter Not Present]";
                return rpcOperationsSec;
            }
            else
            {
                PerformanceCounter rpcOpsSec = new PerformanceCounter("MSExchangeIS Mailbox", "RPC Operations/sec", "_Total", systemName);
                rpcOpsSec.NextValue();
                System.Threading.Thread.Sleep(1000);
                string rpcOperationsSec = Math.Round(rpcOpsSec.NextValue(), 2).ToString();
                return rpcOperationsSec;
            }
        }

        /// <summary>
        ///     Method to obtain the RPC Averaged Latency Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetRPCAveragedLatency(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchangeIS Client", systemName))
            {
                string rpcal = "[Counter Not Present]";
                return rpcal;
            }
            else
            {
                PerformanceCounter rpcAveragedLatency = new PerformanceCounter("MSExchangeIS Client", "RPC Average Latency", "_Total", systemName);
                string rpcal = Math.Round(rpcAveragedLatency.NextValue(), 2).ToString();
                return rpcal;
            }
        }

        /// <summary>
        ///     Method to obtain the Document Indexing Rate Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetDocumentIndexingRate(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string dir = "[Counter Not Present]";
                return dir;
            }
            else
            {
                PerformanceCounter searchDIR = new PerformanceCounter("MSExchange Search Indices", "Document Indexing Rate", "_total", systemName);
                searchDIR.NextValue();
                System.Threading.Thread.Sleep(1000);
                string dir = Math.Round(searchDIR.NextValue(), 2).ToString();
                return dir;
            }
        }

        /// <summary>
        ///     Method to obtain the Full Crawl Mode Status Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetFullCrawlModeStatus(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string fcms = "[Counter Not Present]";
                return fcms;
            }
            else
            {
                PerformanceCounter searchFCMS = new PerformanceCounter("MSExchange Search Indices", "Full Crawl Mode Status", "_Total", systemName);
                searchFCMS.NextValue();
                System.Threading.Thread.Sleep(1000);
                string fcms = Math.Round(searchFCMS.NextValue(), 2).ToString();
                return fcms;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Documents Indexed Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberOfDoxIndexed(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string ndsi = "[Counter Not Present]";
                return ndsi;
            }
            else
            {
                PerformanceCounter searchNDSI = new PerformanceCounter("MSExchange Search Indices", "Number of Documents Successfully Indexed", "_Total", systemName);
                string ndsi = Math.Round(searchNDSI.NextValue(), 2).ToString();
                return ndsi;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Indexed Attachments Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberOfIndexedAttachments(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string noIA = "[Counter Not Present]";
                return noIA;
            }
            else
            {
                PerformanceCounter searchNoIA = new PerformanceCounter("MSExchange Search Indices", "Number of Indexed Attachments", "_Total", systemName);
                string noIA = Math.Round(searchNoIA.NextValue(), 2).ToString();
                return noIA;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Items in the Notification Queue Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberofItemsInANotificationQueue(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string noINQ = "[Counter Not Present]";
                return noINQ;
            }
            else
            {
                PerformanceCounter searchNoINQ = new PerformanceCounter("MSExchange Search Indices", "Number of Items in a Notification Queue", "_Total", systemName);
                string noINQ = Math.Round(searchNoINQ.NextValue(), 2).ToString();
                return noINQ;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Items in the Notification Queue Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberOfMailboxesLeftToCrawl(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string noML2C = "[Counter Not Present]";
                return noML2C;
            }
            else
            {
                PerformanceCounter searchNoML2C = new PerformanceCounter("MSExchange Search Indices", "Number of Mailboxes Left to Crawl", "_Total", systemName);
                searchNoML2C.NextValue();
                System.Threading.Thread.Sleep(1000);
                string noML2C = Math.Round(searchNoML2C.NextValue(), 2).ToString();
                return noML2C;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Outstanding Batches Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberOfOutstandingBatches(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string searchNoOB = "[Counter Not Present]";
                return searchNoOB;
            }
            else
            {
                PerformanceCounter searchNoOBatches = new PerformanceCounter("MSExchange Search Indices", "Number of Outstanding Batches", "_Total", systemName);
                string searchNoOB = Math.Round(searchNoOBatches.NextValue(), 2).ToString();
                return searchNoOB;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Outstanding Documents Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberofOutstandingDox(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string searchNoOD = "[Counter Not Present]";
                return searchNoOD;
            }
            else
            {
                PerformanceCounter searchNoODoc = new PerformanceCounter("MSExchange Search Indices", "Number of Outstanding Documents", "_Total", systemName);
                string searchNoOD = Math.Round(searchNoODoc.NextValue(), 2).ToString();
                return searchNoOD;
            }
        }

        /// <summary>
        ///     Method to obtain the Number of Failed Retries Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetNumberOfFailedRetries(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchange Search Indices", systemName))
            {
                string searchNoFR = "[Counter Not Present]";
                return searchNoFR;
            }
            else
            {
                PerformanceCounter searchNoFRetries = new PerformanceCounter("MSExchange Search Indices", "Number of Failed Retries", "_Total", systemName);
                string searchNoFR = Math.Round(searchNoFRetries.NextValue(), 2).ToString();
                return searchNoFR;
            }
        }

        /// <summary>
        ///     Method to obtain the Messages Queued for Submission Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll from.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetMessagesQueuedforSubmission(string systemName)
        {
            if (!PerformanceCounterCategory.Exists("MSExchangeTransport Queues", systemName))
            {
                string mqfs = "[Counter Not Present]";
                return mqfs;
            }
            else
            {
                PerformanceCounter queuedMSG = new PerformanceCounter("MSExchangeTransport Queues", "Submission Queue Length", "_Total", systemName);
                queuedMSG.NextValue();
                System.Threading.Thread.Sleep(1000);
                string mqfs = Math.Round(queuedMSG.NextValue(), 2).ToString();
                return mqfs;
            }
        }
    }
}