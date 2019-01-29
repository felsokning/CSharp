//-----------------------------------------------------------------------
// <copyright file="WebStringActivity.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Activities.Research
{
    using System.Activities;
    using System.Net;

    using Public.Extensions.Research;

    /// <inheritdoc />
    /// <summary>
    ///     Initializes a new instance of the <see cref="WebStringActivity" /> class.
    /// </summary>
    public class WebStringActivity : SwedishCodeActivity<string>
    {
        /// <summary>
        ///     Gets or sets the value of the <see cref="FirstArgument"/> parameter.
        /// </summary>
        public InArgument<string> FirstArgument { get; set; }

        /// <summary>
        ///     The <see cref="Execute(CodeActivityContext)"/> method inherited from <see cref="CodeActivity{TResult}"/>.
        /// </summary>
        /// <param name="context">The current <see cref="CodeActivity"/> context.</param>
        /// <returns>A string response from the request.</returns>
        protected override string Execute(CodeActivityContext context)
        {
            this.FirstArgument = this.FirstInArgument;
            string target = context.GetValue(this.FirstArgument);
            WebClient newWebClient = new WebClient();
            return newWebClient.DownloadString(target);
        }
    }
}