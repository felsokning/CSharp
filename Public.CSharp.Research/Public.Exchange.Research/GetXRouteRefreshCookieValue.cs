//-----------------------------------------------------------------------
// <copyright file="PublicFactory.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research
{
    using System;
    using System.Management.Automation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GetXRouteRefreshCookieValue"/> class.
    /// </summary>
    /// <inheritdoc cref="PSCmdlet"/>
    [Cmdlet(VerbsCommon.Get, "XRouteRefreshCookieValue")]
    public class GetXRouteRefreshCookieValue : PSCmdlet
    {
        [Parameter(Position = 0, HelpMessage = "The X-RouteRefreshCookie Value", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string CookieValue { get; set; }

        /// <summary>
        ///     Overrides the inherited <see cref="ProcessRecord"/> method.
        /// </summary>
        /// <inheritdoc cref="PSCmdlet"/>
        protected override void ProcessRecord()
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(this.CookieValue);
                for (int i = 0; i < buffer.Length; ++i)
                {
                    buffer[i] ^= (byte)0xFF;
                }

                this.WriteObject(System.Text.Encoding.ASCII.GetString(buffer));
            }
            catch (ArgumentException ae)
            {
                this.WriteError(new ErrorRecord(ae, "0", ErrorCategory.InvalidOperation, null));
            }
            catch (FormatException fe)
            {
                this.WriteError(new ErrorRecord(fe, "0", ErrorCategory.InvalidOperation, null));
            }
        }
    }
}