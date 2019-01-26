//-----------------------------------------------------------------------
// <copyright file="WebStringActivity.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Activities.Research
{
    using System.Activities;
    using System.Net;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WebStringActivity"/> class.
    /// </summary>
    public class WebStringActivity : CodeActivity<string>
    {
        /// <summary>
        ///     The <see cref="Execute(CodeActivityContext)"/> method inherited from <see cref="CodeActivity{TResult}"/>.
        /// </summary>
        /// <param name="context">The current <see cref="CodeActivity"/> context.</param>
        /// <returns>A string response from the request.</returns>
        protected override string Execute(CodeActivityContext context)
        {
            WebClient newWebClient = new WebClient();
            return newWebClient.DownloadString("https://www.linkedin.com/li/track ");
        }
    }
}