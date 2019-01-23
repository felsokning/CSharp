//-----------------------------------------------------------------------
// <copyright file="GetSystemUptime.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.PowerShell.Research
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Management.Automation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GetSystemUptime"/> class.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SystemUptime")]
    public class GetSystemUptime : Cmdlet
    {
        /// <summary>
        ///     Gets or sets the <see cref="ComputerNames"/> value.
        /// </summary>
        [Parameter(HelpMessage = "Systems to target for the call.")]
        public string[] ComputerNames { get; set; }

        /// <summary>
        ///     The overridden method <see cref="ProcessRecord"/> inherited from <see cref="Cmdlet"/>
        /// </summary>
        protected override void ProcessRecord()
        {
            Collection<PSObject> returnObjects = new Collection<PSObject>();
            if (ComputerNames == null)
            {
                PSObject newPsObject = new PSObject();
                PerformanceCounter uptimeCounter = new PerformanceCounter("System", "System Up Time");
                uptimeCounter.NextValue();
                newPsObject.Members.Add(new PSNoteProperty("ComputerName", Environment.MachineName));
                newPsObject.Members.Add(new PSNoteProperty("Up Time", TimeSpan.FromSeconds(uptimeCounter.NextValue())));
                returnObjects.Add(newPsObject);
            }
            else
            {
                // We could do a parallel for each but thread safety is a concern and given that this will probably have
                // low use there's currently no drive to change the architecture.
                ComputerNames.ToList().ForEach(c => 
                {
                    PSObject newPsObject = new PSObject();
                    PerformanceCounter uptimeCounter = new PerformanceCounter("System", "System Up Time", null, c);
                    uptimeCounter.NextValue();
                    newPsObject.Members.Add(new PSNoteProperty("ComputerName", Environment.MachineName));
                    newPsObject.Members.Add(new PSNoteProperty("Up Time", TimeSpan.FromSeconds(uptimeCounter.NextValue())));
                    returnObjects.Add(newPsObject);
                });
            }

            WriteObject(returnObjects);
        }
    }
}