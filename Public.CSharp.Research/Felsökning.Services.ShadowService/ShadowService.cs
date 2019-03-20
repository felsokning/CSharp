//-----------------------------------------------------------------------
// <copyright file="ShadowService.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Services.ShadowService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ShadowService"/> class.
    /// </summary>
    /// <inheritdoc cref="ServiceBase"/>
    public partial class ShadowService : ServiceBase
    {
        /// <summary>
        ///     Gets or sets the <see cref="serviceAutoResetEvent"/> value, 
        ///     which is used for signaling the Timer.
        /// </summary>
        private AutoResetEvent serviceAutoResetEvent { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="encryptFilesAfterCopy"/> value, 
        ///     which is used to signify if the files should be encrypted.
        /// </summary>
        private bool encryptFilesAfterCopy { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="firstRun"/> value,
        ///     which is used to signify if this is the first timer firing
        ///     since the service started.
        /// </summary>
        private bool firstRun { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="serviceEventLog"/> value,
        ///     which is used for the service to log events to the Application log.
        /// </summary>
        private EventLog serviceEventLog { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="tasks"/> value,
        ///     which is used to store the Tasks for tracking.
        /// </summary>
        private List<Task> tasks { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="sourceDirectory"/> value, 
        ///     which is used to monitor for files to copy.
        /// </summary>
        private string sourceDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="targetDirectories"/> value, 
        ///     which is used to target where to copy the files to.
        /// </summary>
        private string targetDirectories { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="targetDirectoryStrings"/> value, 
        ///     which is used to store the multi-valued target directories.
        /// </summary>
        private string[] targetDirectoryStrings { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="serviceTimerCallback"/> value, 
        ///     which is used by the Timer on signal.
        /// </summary>
        private TimerCallback serviceTimerCallback { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="serviceTimer"/> value, 
        ///     which is used to periodically signal work to be done.
        /// </summary>
        private Timer serviceTimer { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Service1"/> class.
        /// </summary>
        public ShadowService()
        {

            // This is a hack to use the Visual Studio local debugger instance, as it doesn't
            // leverage the System's service runner to execute; so, OnStart is never called (otherwise).
#if DEBUG
            this.OnStart(null);
#endif
            InitializeComponent();

            // Initialise all the things not initialised in OnStart().
            this.serviceAutoResetEvent = new AutoResetEvent(false);
            this.serviceTimerCallback = new TimerCallback(StartShadowCopyWork);
            this.serviceTimer = new Timer(
                this.serviceTimerCallback,
                serviceAutoResetEvent,
                (int)TimeSpan.FromSeconds(1).TotalMilliseconds,

                // To prevent aggressive disk-thrashing.
                Timeout.Infinite);
        }

        /// <summary>
        ///     Overrides the <see cref="OnStart(string[])"/> methond inherited.
        /// </summary>
        /// <param name="args">The arguments passed on the start of the service.</param>
        protected override void OnStart(string[] args)
        {
            this.encryptFilesAfterCopy = bool.Parse(ConfigurationManager.AppSettings["EncryptionEnabled"]);
            this.firstRun = true;
            this.serviceEventLog = new EventLog("Application");
            this.serviceEventLog.Source = this.ServiceName;
            this.sourceDirectory = ConfigurationManager.AppSettings["SourceLocation"];
            this.targetDirectories = ConfigurationManager.AppSettings["TargetLocations"];
            this.tasks = new List<Task>(0);
            if (targetDirectories.Contains(';'))
            {
                targetDirectoryStrings = targetDirectories.Split(';');
            }

            else
            {
                targetDirectoryStrings = new string[] { targetDirectories };
            }
        }

        /// <summary>
        ///     Overrides the <see cref="OnStop"/> method inheritied.
        /// </summary>
        protected override void OnStop()
        {
            if (serviceTimer.Change(int.MaxValue, int.MaxValue))
            {
                sourceDirectory = string.Empty;
                targetDirectories = string.Empty;
                serviceAutoResetEvent.Dispose();
                GC.Collect();
            }

            serviceTimer.Dispose();
        }

        /// <summary>
        ///     Starts the actions to copy the items from the source to the destination[s].
        /// </summary>
        /// <param name="stateInfo">An object containing application-specific information relevant to the method invoked.</param>
        private void StartShadowCopyWork(object stateInfo)
        {
            // If the source directory doesn't exist, what's the point?
            if (!Directory.Exists(sourceDirectory))
            {
                throw new ArgumentException("The source directory does not exist.", sourceDirectory);
            }

            if (this.firstRun)
            {
                this.serviceEventLog.WriteEntry($"Entered 'StartShadowCopyWork' at {DateTime.UtcNow}");
            }

            this.tasks = new List<Task>(0);
            List<string> directories = Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories).ToList();
            if (directories.Count > 0)
            {
                this.StartRootFolderCopyWork(sourceDirectory);
                Parallel.ForEach(
                    directories,
                    d =>
                    {
                        string subfolder = d.Replace(sourceDirectory, string.Empty);
                        Parallel.ForEach(
                            targetDirectoryStrings,
                            targetDirectory =>
                            {
                                // Keep the user from shooting us in the foot.
                                if (!Directory.Exists(targetDirectory))
                                {
                                    Directory.CreateDirectory(targetDirectory);
                                }

                                string targetSubfolder = targetDirectory + subfolder;
                                if (!Directory.Exists(targetSubfolder))
                                {
                                    Directory.CreateDirectory(targetSubfolder);
                                }

                                List<string> files = Directory.EnumerateFiles(d).ToList();
                                Parallel.ForEach(
                                    files,
                                    f =>
                                    {
                                        FileInfo sourceFileInfo = new FileInfo(f);
                                        string scrubbedString = f.Replace(sourceDirectory, string.Empty);
                                        string targetFilePath = targetDirectory + scrubbedString;
                                        if (File.Exists(targetFilePath))
                                        {
                                            // LastAccess is bunk: 
                                            // https://blogs.technet.microsoft.com/filecab/2006/11/07/disabling-last-access-time-in-windows-vista-to-improve-ntfs-performance/
                                            // LastWriteTime is bunk: 
                                            // https://stackoverflow.com/questions/9992223/file-getlastwritetime-seems-to-be-returning-out-of-date-value
                                            FileInfo targetFileInfo = new FileInfo(targetFilePath);

                                            // So, we compare sizes and prevent loss of data from a smaller file size.
                                            if (!(targetFileInfo.Length == sourceFileInfo.Length) && !(sourceFileInfo.Length < targetFileInfo.Length))
                                            {
                                                lock (((ICollection)this.tasks).SyncRoot)
                                                {
                                                    this.tasks.Add(Task.Factory.StartNew(() => File.Copy(f, targetFilePath, true)));
                                                }

                                                // No sense in encrypting the file, if it's already encrypted by someone else.
                                                if (!sourceFileInfo.Attributes.HasFlag(FileAttributes.Encrypted))
                                                {
                                                    if (this.encryptFilesAfterCopy)
                                                    {
                                                        lock (((ICollection)this.tasks).SyncRoot)
                                                        {
                                                            this.tasks.Add(
                                                            Task.Factory.StartNew(
                                                                () =>
                                                                {
                                                                    // Due to thread scheduling, the file could not be copied by the time that we call to encrypt.
                                                                    while (!File.Exists(targetFilePath))
                                                                    {
                                                                        Thread.Sleep((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                                                                    }

                                                                    File.Encrypt(targetFilePath);
                                                                }));
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else
                                        {
                                            lock (((ICollection)this.tasks).SyncRoot)
                                            {
                                                this.tasks.Add(
                                                    Task.Factory.StartNew(
                                                        () =>
                                                        {
                                                            File.Copy(f, targetFilePath);
                                                        }));
                                            }

                                            // No sense in encrypting, if it's already been done.
                                            if (!sourceFileInfo.Attributes.HasFlag(FileAttributes.Encrypted))
                                            {
                                                if (this.encryptFilesAfterCopy)
                                                {
                                                    lock (((ICollection)this.tasks).SyncRoot)
                                                    {
                                                        this.tasks.Add(
                                                        Task.Factory.StartNew(
                                                            () =>
                                                            {
                                                                // Due to thread scheduling, the file could not be copied by the time that we call to encrypt.
                                                                while (!File.Exists(targetFilePath))
                                                                {
                                                                    Thread.Sleep((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                                                                }

                                                                File.Encrypt(targetFilePath);
                                                            }));
                                                    }
                                                }
                                            }
                                        }
                                    });
                            });
                    });
            }

            // No sub-directories, just files.
            else
            {
                this.StartRootFolderCopyWork(sourceDirectory);
            }

            // Block the main thread to allow parallelisation to catch-up.
            Thread.Sleep((int)TimeSpan.FromMinutes(2).TotalMilliseconds);

            if (tasks.Count > 0)
            {
                try
                {
                    // Block until all of the tasks are done.
                    this.serviceEventLog.WriteEntry($"Now blocking and waiting on {tasks.Count} tasks at {DateTime.UtcNow}.");
                    Task.WaitAll(this.tasks.ToArray());
                }
                catch (ObjectDisposedException)
                {
                    // We expect some tasks to be completed by the time we get here.
                    // That's totally fine, given we may be dealing with GBs of data.
                }
            }

            if (this.firstRun)
            {
                if (this.serviceTimer.Change(
                    (int)TimeSpan.FromMinutes(1).TotalMilliseconds,
                    (int)TimeSpan.FromMinutes(1).TotalMilliseconds))
                {
                    this.serviceEventLog.WriteEntry(
                        "Successfully changed the timer value to 1 minute.",
                        EventLogEntryType.Information);
                }

                // We've had our initial run, let's let everything get back to homeostasis for normal runs.
                this.firstRun = false;
            }
        }

        /// <summary>
        ///     Starts copying files from the root of the top-most directory.
        /// </summary>
        /// <param name="sourceDirectory">The directory to copy files from.</param>
        private void StartRootFolderCopyWork(string sourceDirectory)
        {
            List<string> files = Directory.EnumerateFiles(sourceDirectory).ToList();
            Parallel.ForEach(
                files,
                f =>
                {
                    FileInfo sourceFileInfo = new FileInfo(f);
                    string scrubbedString = f.Replace(sourceDirectory, string.Empty);
                    Parallel.ForEach(
                        targetDirectoryStrings,
                        targetDirectory =>
                        {
                            string targetFilePath = targetDirectory + scrubbedString;

                            // Keep the user from shooting us in the foot.
                            if (!Directory.Exists(targetDirectory))
                            {
                                Directory.CreateDirectory(targetDirectory);
                            }

                            if (File.Exists(targetFilePath))
                            {
                                // LastAccess is bunk: https://blogs.technet.microsoft.com/filecab/2006/11/07/disabling-last-access-time-in-windows-vista-to-improve-ntfs-performance/
                                // LastWriteTime is bunk: https://stackoverflow.com/questions/9992223/file-getlastwritetime-seems-to-be-returning-out-of-date-value
                                FileInfo targetFileInfo = new FileInfo(targetFilePath);

                                /// So, we compare sizes and prevent loss of data from a smaller file size.
                                if (!(targetFileInfo.Length == sourceFileInfo.Length) && !(sourceFileInfo.Length < targetFileInfo.Length))
                                {
                                    lock (((ICollection)this.tasks).SyncRoot)
                                    {
                                        this.tasks.Add(Task.Factory.StartNew(() => File.Copy(f, targetFilePath, true)));
                                    }

                                    // No sense in encrypting the file, if it's already encrypted by someone else.
                                    if (!sourceFileInfo.Attributes.HasFlag(FileAttributes.Encrypted))
                                    {
                                        if (this.encryptFilesAfterCopy)
                                        {
                                            lock (((ICollection)this.tasks).SyncRoot)
                                            {
                                                this.tasks.Add(
                                                Task.Factory.StartNew(
                                                    () =>
                                                    {
                                                        // Due to thread scheduling, the file could not be copied by the time that we call to encrypt.
                                                        while (!File.Exists(targetFilePath))
                                                        {
                                                            Thread.Sleep((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                                                        }

                                                        File.Encrypt(targetFilePath);
                                                    }));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lock (((ICollection)this.tasks).SyncRoot)
                                {
                                    this.tasks.Add(Task.Factory.StartNew(() => File.Copy(f, targetFilePath)));
                                }

                                // No sense in encrypting the file, if it's already been done.
                                if (!sourceFileInfo.Attributes.HasFlag(FileAttributes.Encrypted))
                                {
                                    if (this.encryptFilesAfterCopy)
                                    {
                                        lock (((ICollection)this.tasks).SyncRoot)
                                        {
                                            this.tasks.Add(
                                            Task.Factory.StartNew(
                                                () =>
                                                {
                                                    // Due to thread scheduling, the file could not be copied by the time that we call to encrypt.
                                                    while (!File.Exists(targetFilePath))
                                                    {
                                                        Thread.Sleep((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                                                    }

                                                    File.Encrypt(targetFilePath);
                                                }));
                                        }
                                    }
                                }
                            }
                        });
                });
        }
    }

}
