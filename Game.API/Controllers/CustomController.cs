using Game.Domain.Resources;
using Game.Infra.Data.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.API.Controllers
{
    public abstract class CustomResultController : ControllerBase
    {
        protected virtual IActionResult GenerateResponse(CustomResult? result)
        {
            if (result == null)
                return BadRequest(Messages.BASE_ERROR_MESSAGE);

            if(result.Failure)
            {
                if(result.CurrentHttpStatusCode == null)
                    return BadRequest(result);
                else
                {
                    return result.CurrentHttpStatusCode switch
                    {
                        HttpStatusCode.NotFound => NotFound(result),
                        HttpStatusCode.InternalServerError => Problem(detail: Messages.BASE_ERROR_MESSAGE, statusCode: (int)HttpStatusCode.InternalServerError),

                        _ => BadRequest(result)
                    };
                }
            }
            else
            {
                return result.CurrentHttpStatusCode switch
                {
                    HttpStatusCode.Created => Created(string.Empty, result),
                    HttpStatusCode.NoContent => NoContent(),
                    HttpStatusCode.Accepted => Accepted(),

                    _ => Ok(result)
                };
            }
        }
    }
}
