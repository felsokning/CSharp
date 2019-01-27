//-----------------------------------------------------------------------
// <copyright file="GetFileSizes.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.PowerShell.Research
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GetFileSizes"/> class.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "FileSizes")]
    public class GetFileSizes : Cmdlet
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="Path"/> parameter
        /// </summary>
        [Parameter(HelpMessage = "Path to query for the file sizes.")]
        public string Path { get; set; }

        /// <summary>
        ///     Gets or sets the value of <see cref="SizesInMb"/>
        /// </summary>
        [Parameter(HelpMessage = "Should sizes be in MB")]
        public SwitchParameter SizesInMb { get; set; }

        /// <summary>
        ///     External call into WinAPI, as exposed by Kernel32.
        /// </summary>
        /// <param name="fileName">The file to analyze.</param>
        /// <param name="fileSizeHigh">The out integer to store the file's high size.</param>
        /// <returns>The out integer to store the file's low size.</returns>
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "GetCompressedFileSize")]
        public static extern uint GetCompressedFileSizeAPI(string fileName, out uint fileSizeHigh);

        /// <summary>
        ///     The overridden method <see cref="ProcessRecord"/> inherited from <see cref="Cmdlet"/>
        /// </summary>
        protected override void ProcessRecord()
        {
            string[] files = Directory.GetFiles(this.Path);
            Collection<PSObject> returnObjects = new Collection<PSObject>();
            files.ToList().ForEach(f => 
            {
                uint lowOrder = GetCompressedFileSizeAPI(f, out var highOrder);
                int error = Marshal.GetLastWin32Error();
                if (highOrder == 0 && lowOrder == 0xFFFFFFFF && error != 0)
                {
                    throw new Win32Exception(error);
                }

                if (this.SizesInMb)
                {
                    ulong size = ((((ulong)highOrder << 32) + lowOrder) / 1024) / 1024;
                    PSObject responseObject = new PSObject();
                    responseObject.Members.Add(new PSNoteProperty("File", f));
                    responseObject.Members.Add(new PSNoteProperty("Size (MB)", size));
                    returnObjects.Add(responseObject);
                }

                else
                {
                    ulong size = ((ulong)highOrder << 32) + lowOrder;
                    PSObject responseObject = new PSObject();
                    responseObject.Members.Add(new PSNoteProperty("File", f));
                    responseObject.Members.Add(new PSNoteProperty("Size (Bytes)", size));
                    returnObjects.Add(responseObject);
                }
            });

            this.WriteObject(returnObjects);
        }
    }
}