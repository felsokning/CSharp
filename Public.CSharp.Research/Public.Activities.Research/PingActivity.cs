//-----------------------------------------------------------------------
// <copyright file="PingActivity.cs" company="None" >
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Activities.Research
{
    using System.Activities;
    using System.Net.NetworkInformation;

    using Public.Extensions.Research;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PingActivity"/> class.
    /// </summary>
    /// <inheritdoc cref="SwedishCodeActivity{T}"/>
    public class PingActivity : SwedishCodeActivity<string>
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="FirstArgument"/> parameter.
        /// </summary>
        public InArgument<string> FirstArgument { get; set; }

        /// <summary>
        ///     Overrides the <see cref="Execute"/> method exposed by the <see cref="SwedishCodeActivity{T}"/> class.
        /// </summary>
        /// <param name="context">The execution context passed when invoked.</param>
        /// <returns>A string back to the caller.</returns>
        protected override string Execute(CodeActivityContext context)
        {
            this.FirstArgument = this.FirstInArgument;
            string target = context.GetValue(this.FirstArgument);
            bool reached = false;
            Ping newPing = new Ping();

            try
            {
                PingReply reply = newPing.Send(target, 1000);
                reached = reply?.Status == IPStatus.Success;
            }
            catch (PingException e)
            {
                newPing?.Dispose();
                return e.Message;
            }

            newPing?.Dispose();
            return reached.ToString();
        }
    }
}