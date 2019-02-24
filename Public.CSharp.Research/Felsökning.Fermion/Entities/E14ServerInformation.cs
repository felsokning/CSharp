//-----------------------------------------------------------------------
// <copyright file="E14ServerInformation.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion.Entities
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="E14ServerInformation"/> class.
    /// </summary>
    public class E14ServerInformation
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="StrSystemName"/> property.
        /// </summary>
        public string StrSystemName { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrProcessor"/> property.
        /// </summary>
        public string StrProcessor { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrMemory"/> property.
        /// </summary>
        public string StrMemory { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrDiskQueue"/> property.
        /// </summary>
        public string StrDiskQueue { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrRPCCount"/> property.
        /// </summary>
        public string StrRPCCount { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrRPCOpsperSecond"/> property.
        /// </summary>
        public string StrRPCOpsperSecond { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrRPCAveragedLatency"/> property.
        /// </summary>
        public string StrRPCAveragedLatency { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrDocumentIndexingRate"/> property.
        /// </summary>
        public string StrDocumentIndexingRate { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrFullCrawlModeStatus"/> property.
        /// </summary>
        public string StrFullCrawlModeStatus { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrNumberOfDoxIndexed"/> property.
        /// </summary>
        public string StrNumberOfDoxIndexed { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrNumberOfIndexedAttachments"/> property.
        /// </summary>
        public string StrNumberOfIndexedAttachments { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrSearchNumberofItemsInANotificationQueue"/> property.
        /// </summary>
        public string StrSearchNumberofItemsInANotificationQueue { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrSearchNumberOfMailboxesLeftToCrawl"/> property.
        /// </summary>
        public string StrSearchNumberOfMailboxesLeftToCrawl { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrSearchNumberOfOutstandingBatches"/> property.
        /// </summary>
        public string StrSearchNumberOfOutstandingBatches { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrSearchNumberofOutstandingDox"/> property.
        /// </summary>
        public string StrSearchNumberofOutstandingDox { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrNumberOfFailedRetries"/> property.
        /// </summary>
        public string StrNumberOfFailedRetries { get; set; }

        /// <summary>
        ///     Gets or sets the value of the <see cref="StrMessagesQueuedforSubmission"/> property.
        /// </summary>
        public string StrMessagesQueuedforSubmission { get; set; }
    }
}