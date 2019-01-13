//-----------------------------------------------------------------------
// <copyright file="DebugLiveProcess.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Debugging.Research
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Management.Automation;
    using Microsoft.Diagnostics.Runtime;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DebugDumpFile"/> class.
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Debug, "LiveProcess")]
    public class DebugLiveProcess : Cmdlet
    {
        /// <summary>
        ///     Gets or sets the string parameter used for the caller to specify the process to analyze.
        /// </summary>
        [Parameter(HelpMessage = "Path to the Dump File to Debug.")]
        public string Process { get; set; }

        /// <summary>
        ///     Overrides <see cref="ProcessRecord"/>, leveraging ClrMD to analyze the given dump file.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(this.Process))
            {
                throw new ParameterBindingException("Process cannot be null or empty.");
            }

            // Determine if we were given a process name or process id.
            bool isPid = int.TryParse(this.Process, out int pid);

            if (!isPid)
            {
                Process[] processes = System.Diagnostics.Process.GetProcessesByName(this.Process);
                if (processes.Count() > 1)
                {
                    throw new ArgumentException("Multiple processes found with that name. Please specify the PID.");
                }
                else
                {
                    pid = processes[0].Id;
                }
            }

            using (DataTarget target = DataTarget.AttachToProcess(pid, (uint)TimeSpan.FromSeconds(5).TotalMilliseconds))
            {
                if (target.ClrVersions.Count > 0)
                {
                    // Use the first CLR Runtime available due to SxS.
                    ClrRuntime clrRuntime = target.ClrVersions.SingleOrDefault().CreateRuntime();

                    // Set the symbol file path, so we can debug the dump.
                    target.SymbolLocator.SymbolPath = "SRV*https://msdl.microsoft.com/download/symbols";
                    target.SymbolLocator.SymbolCache = "C:\\Symbols\\";

                    foreach (ClrThread thread in clrRuntime.Threads)
                    {
                        if (!thread.IsAlive)
                        {
                            continue;
                        }

                        // If the thread's single frame is WaitForSingleObject, we probably don't care.
                        if (thread.StackTrace.Count > 1)
                        {
                            Console.WriteLine("{0:X}", thread.OSThreadId);
                            foreach (ClrStackFrame frame in thread.StackTrace)
                            {
                                Console.WriteLine("{0,12:x} {1,12:x} {2} {3}", frame.StackPointer, frame.InstructionPointer, frame.ModuleName, frame.ToString());
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No CLR Versions found for { this.Process }. Process is probably native. No can has with those maths.");
                }
            }
        }
    }
}