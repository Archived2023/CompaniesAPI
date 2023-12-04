using Companies.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Companies.API.Controllers
{
    public class ResponseController : ControllerBase
    {
        public IActionResult HandleErrors(BaseResponse baseResponse)
        {
            return baseResponse switch
            {
                NotFoundResponse => Problem
               (
                   detail: ((NotFoundResponse)baseResponse).Message,
                   statusCode: StatusCodes.Status404NotFound,
                   title: "Not found"
               ),
                _ => throw new NotImplementedException()

            };
        }
    }
}