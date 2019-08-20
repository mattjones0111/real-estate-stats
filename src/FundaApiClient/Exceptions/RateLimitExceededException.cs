using System;
using System.Runtime.Serialization;

namespace FundaApiClient.Exceptions
{
    [Serializable]
    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException()
            : base("The request rate limit for this Api has been exceeded.")
        {
        }

        protected RateLimitExceededException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
