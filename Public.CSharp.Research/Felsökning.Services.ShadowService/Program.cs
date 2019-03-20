//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Services.ShadowService
{
    using System.ServiceProcess;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Program"/> class.
    /// </summary>
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ShadowService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}