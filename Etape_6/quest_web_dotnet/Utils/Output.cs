using Microsoft.AspNetCore.Mvc;

namespace quest_web_dotnet.Utils
{
    public class Output
    {
        public BadRequestResult BadRequestResult (string? message)
        {
            return BadRequestResult(message ?? "Une erreur s'est produite");
        }

        //public virtual ObjectResult Unauthorize(string? message)
        //{
        //    return ObjectResult(message ?? "Vous n'avez pas les droits") {
        //        StatusCodes = 403
        //    };
        //}
    }
}
