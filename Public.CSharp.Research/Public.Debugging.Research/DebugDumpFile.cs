//-----------------------------------------------------------------------
// <copyright file="DebugDumpFile.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Debugging.Research
{
    using System;
    using System.Linq;
    using System.Management.Automation;
    using Microsoft.Diagnostics.Runtime;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DebugDumpFile"/> class.
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Debug, "DumpFile")]
    public class DebugDumpFile : Cmdlet
    {
        /// <summary>
        ///     Gets or sets the string parameter used for the caller to specify where the dump file is.
        /// </summary>
        [Parameter(HelpMessage = "Path to the Dump File to Debug.")]
        public string FilePath { get; set; }

        /// <summary>
        ///     Overrides <see cref="ProcessRecord"/>, leveraging ClrMD to analyze the given dump file.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(this.FilePath))
            {
                throw new ParameterBindingException("FilePath cannot be null or empty.");
            }

            using (DataTarget target = DataTarget.LoadCrashDump(this.FilePath, CrashDumpReader.ClrMD))
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
                    Console.WriteLine($"No CLR Versions found for { FilePath }. Dump is probably native. No can has with those maths.");
                }
            }
        }
    }
}