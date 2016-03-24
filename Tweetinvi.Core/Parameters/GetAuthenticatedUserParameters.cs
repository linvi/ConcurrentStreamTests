﻿namespace Tweetinvi.Core.Parameters
{
    public interface IGetAuthenticatedUserParameters : ICustomRequestParameters
    {
        /// <summary>
        /// Include user entities.
        /// </summary>
        bool IncludeEntities { get; set; }

        /// <summary>
        /// Include the email of the user. This is only available if the application 
        /// has been verified and approved by Twitter.
        /// </summary>
        bool IncludeEmail { get; set; }

        /// <summary>
        /// Do not included the latest tweets of the user.
        /// </summary>
        bool SkipStatus { get; set; }
    }

    public class GetAuthenticatedUserParameters : CustomRequestParameters, IGetAuthenticatedUserParameters
    {
        public GetAuthenticatedUserParameters()
        {
            IncludeEntities = true;
            IncludeEmail = true;
            SkipStatus = true;
        }

        public bool IncludeEntities { get; set; }
        public bool IncludeEmail { get; set; }
        public bool SkipStatus { get; set; }
    }
}