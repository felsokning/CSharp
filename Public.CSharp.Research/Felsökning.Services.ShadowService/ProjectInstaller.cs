//-----------------------------------------------------------------------
// <copyright file="ProjectInstaller.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Services.ShadowService
{
    using System.ComponentModel;
    using System.Configuration.Install;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProjectInstaller"/> class.
    /// </summary>
    /// <inheritdoc cref="Installer" />
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}