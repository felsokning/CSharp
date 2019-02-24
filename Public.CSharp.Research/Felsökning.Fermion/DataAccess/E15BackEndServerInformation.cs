//-----------------------------------------------------------------------
// <copyright file="E15BackEndServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.DataAccess
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Initializes a new instance of the <see cref="E15BackEndServerInformation"/> class.
    /// </summary>
    public class E15BackEndServerInformation
    {
        /// <summary>
        ///     Obtains the information and passes to the Entities class.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns values to the Entities class.</returns>
        public Entities.E15BackEndServerInformation GetSystemInformation(string systemName)
        {
            Entities.E15BackEndServerInformation e15BESrvInfo = new Entities.E15BackEndServerInformation();
            try
            {
                e15BESrvInfo.StrsystemName = systemName;
                e15BESrvInfo.StrProcessor = this.E15BEGetProcessor(systemName);
                e15BESrvInfo.StrMemory = this.GetE15BEMem(systemName);
                e15BESrvInfo.StrDiskQueue = this.GetE15BEDiskQueue(systemName);
            }
            catch
            {
                throw;
            }

            return e15BESrvInfo;
        }

        /// <summary>
        ///     Obtains the Processor Counter from the Performance Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string E15BEGetProcessor(string systemName)
        {
            PerformanceCounter e15CafeProcessor = new PerformanceCounter("Processor", "% Processor Time", "_total", systemName);
            e15CafeProcessor.NextValue();

            // We let the zero-call complete, before polling for our datasets.
            System.Threading.Thread.Sleep(1000);
            string processorpc = Math.Round(e15CafeProcessor.NextValue(), 2).ToString();
            return processorpc;
        }

        /// <summary>
        ///     Obtains the Memory Counter from the Performance Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15BEMem(string systemName)
        {
            PerformanceCounter e15CafeMem = new PerformanceCounter("Memory", "% committed bytes in use", string.Empty, systemName);
            string memorypc = Math.Round(e15CafeMem.NextValue(), 2).ToString();
            return memorypc;
        }

        /// <summary>
        ///     Obtains the Physical Disk Queue Counter from the Performance Counter.
        /// </summary>
        /// <param name="systemName">The target system to poll.</param>
        /// <returns>Returns the Cooked Value of the Counter.</returns>
        private string GetE15BEDiskQueue(string systemName)
        {
            PerformanceCounter e15CafeDiskQueue = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total", systemName);
            string diskQueuepc = Math.Round(e15CafeDiskQueue.NextValue(), 2).ToString();
            return diskQueuepc;
        }
    }
}