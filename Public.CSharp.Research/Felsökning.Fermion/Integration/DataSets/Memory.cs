//-----------------------------------------------------------------------
// <copyright file="Memory.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Integration.DataSets
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Memory"/> class.
    /// </summary>
    public class Memory
    {
        /// <summary>
        ///     Returnts a <see cref="Task{TResult}"/> for the Memory WorkingSet.
        /// </summary>
        /// <param name="systemName">System to poll for the data.</param>
        /// <returns>Dictionary containing the data.</returns>
        public static async Task<Dictionary<int, Dictionary<string, long>>> GetMemoryDataAsync(string systemName)
        {
            return await Task.Run(() => Task.Run(() => GetMemoryData(systemName)).Result);
        }

        /// <summary>
        ///     Obtains the Memory Working Set from all of the processes.
        /// </summary>
        /// <param name="systemName">System to poll for the data.</param>
        /// <returns>Dictionary containing the data.</returns>
        private static Dictionary<int, Dictionary<string, long>> GetMemoryData(string systemName)
        {
            Dictionary<int, Dictionary<string, long>> dictOfDict = new Dictionary<int, Dictionary<string, long>>();
            Process[] processDetails = Process.GetProcesses(systemName);
            foreach (Process _process in processDetails)
            {
                Dictionary<string, long> tempDict = new Dictionary<string, long>();
                tempDict.Add(_process.ProcessName, _process.WorkingSet64);
                dictOfDict.Add(_process.Id, tempDict);
            }

            return dictOfDict;
        }
    }
}