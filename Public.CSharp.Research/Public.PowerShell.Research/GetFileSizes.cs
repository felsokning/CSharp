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
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "GetCompressedFileSize")]
        static extern uint GetCompressedFileSizeAPI(string lpFileName, out uint lpFileSizeHigh);

        [Parameter(HelpMessage = "Path to query for the file sizes.")]
        public string Path { get; set; }

        [Parameter(HelpMessage = "Should sizes be in MB")]
        public SwitchParameter SizesInMB { get; set; }

        protected override void ProcessRecord()
        {
            string[] files = Directory.GetFiles(this.Path);
            Collection<PSObject> returnObjects = new Collection<PSObject>();
            files.ToList().ForEach(f => 
            {
                uint HighOrder;
                uint LowOrder;
                LowOrder = GetCompressedFileSizeAPI(f, out HighOrder);
                int error = Marshal.GetLastWin32Error();
                if (HighOrder == 0 && LowOrder == 0xFFFFFFFF && error != 0)
                    throw new Win32Exception(error);

                if (SizesInMB)
                {
                    ulong size = ((((ulong)HighOrder << 32) + LowOrder) / 1024) / 1024;
                    PSObject responseObject = new PSObject();
                    responseObject.Members.Add(new PSNoteProperty("File", f));
                    responseObject.Members.Add(new PSNoteProperty("Size (MB)", size));
                    returnObjects.Add(responseObject);
                }
                else
                {
                    ulong size = ((ulong)HighOrder << 32) + LowOrder;
                    PSObject responseObject = new PSObject();
                    responseObject.Members.Add(new PSNoteProperty("File", f));
                    responseObject.Members.Add(new PSNoteProperty("Size (Bytes)", size));
                    returnObjects.Add(responseObject);
                }
            });

            WriteObject(returnObjects);
        }
    }
}