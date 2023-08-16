using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleModelos
{
    public class ResponseBase
    {
        /// <summary>
        /// Description of response
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Description of response
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Description of error
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Is Success
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// HTTP Error Code
        /// </summary>
        public ResponseType ResponseType { get; set; }
    }

    public enum ResponseType
    {
        BadRequest = 400,
        Okay = 200,
        NotAuthorized = 401,
        InternalError = 500,
        TooManyRequest = 429,
        NotAcceptable = 406,
    }
}
