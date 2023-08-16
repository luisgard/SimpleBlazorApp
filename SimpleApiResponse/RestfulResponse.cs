using Microsoft.AspNetCore.Mvc;
using SimpleModelos;

namespace SimpleApiResponse
{
    public class RestfulResponse : ControllerBase
    {
        public IActionResult Result { get; set; }

        public IActionResult GetResponse(object response)
        {
            switch (((ResponseBase)response).ResponseType)
            {
                case ResponseType.BadRequest:
                    Result = BadRequest(response);
                    break;

                case ResponseType.Okay:
                    Result = Ok(response);
                    break;

                case ResponseType.NotAuthorized:
                    Result = Unauthorized(response);
                    break;

                case ResponseType.InternalError:
                    Result = StatusCode(500, response);
                    break;

                case ResponseType.TooManyRequest:
                    Result = StatusCode(429, response);
                    break;

                case ResponseType.NotAcceptable:
                    Result = StatusCode(406, response);
                    break;

                default:
                    break;
            }
            return Result;
        }
    }
}